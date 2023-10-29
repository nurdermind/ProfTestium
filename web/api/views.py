from io import BytesIO

import webuiapi
from PIL import Image
from django.http import HttpResponse
from rest_framework import viewsets
from rest_framework.decorators import action
from rest_framework.response import Response

from django.views.decorators.csrf import csrf_exempt

from api.serializers.question_serializers import QuestionSerializer, AnswerVariantSerializer, \
    OpenQuestionAnswerSerializer, QuestionAnswerSerializer
from api.serializers.sessions_serializers import SessionUserSerializer
from api.serializers.tests_serializer import TestSerializer
from helpers.question_helper import QuestionHelper
from helpers.session_helper import SessionHelper
from helpers.tests_helper import TestsHelper


class SessionViewSet(viewsets.GenericViewSet):
    lookup_url_kwarg = 'session_id'

    def list(self, request):
        sessions = self._session_helper.list()
        return Response(data=SessionUserSerializer(sessions, many=True).data)

    def retrieve(self, request, session_id):
        session = self._session_helper.retrieve(session_id)
        return Response(data=SessionUserSerializer(session).data)

    def create(self, request, session_id):
        user_id = self.kwargs['user_id']
        # TODO: Вызов self._session_helper.create(data=serializer.validated_data)

    @property
    def _session_helper(self):
        user_id = self.kwargs['user_id']
        return SessionHelper(user_id=user_id)


class TestsViewSet(viewsets.GenericViewSet):
    lookup_url_kwarg = 'test_id'

    def list(self, request):
        tests = TestsHelper().list()
        return Response(data=TestSerializer(tests, many=True).data)

    def retrieve(self, request, test_id):
        test = TestsHelper().detail(test_id)
        return Response(data=TestSerializer(test).data)


class QuestionViewSet(viewsets.GenericViewSet):
    lookup_url_kwarg = 'question_id'

    def list(self, request):
        questions = QuestionHelper(test_id=self.kwargs['test_id']).list()
        return Response(data=QuestionSerializer(questions).data)

    def retrieve(self, request, question_id):
        answer_variants = QuestionHelper(test_id=self.kwargs['test_id']).variant_answer(question_id)
        return Response(data=AnswerVariantSerializer(answer_variants, many=True).data)

    @action(detail=False, methods=['GET'])
    def list_open_question_answers(self, request):
        user_id = request.query_params.get('user_id')
        if not user_id:
            return Response(status=400)
        list_open_question_answers = QuestionHelper(test_id=self.kwargs['test_id']).list_open_question_answers(user_id)
        return Response(data=OpenQuestionAnswerSerializer(list_open_question_answers, many=True).data)

    @action(detail=False, methods=['GET'])
    def list_answers_user(self, request):
        user_id = request.query_params.get('user_id')
        if not user_id:
            return Response(status=400)

        answers = QuestionHelper(test_id=self.kwargs['test_id']).list_answers_user(user_id)
        return Response(data=QuestionAnswerSerializer(answers, many=True).data)


@csrf_exempt
def generate_images(request):
    api = webuiapi.WebUIApi(host='10.244.0.1', port=7860)

    prompt = request.POST.get('prompt')
    input_image_file = request.FILES.get('input_image')

    if input_image_file:
        prompt = prompt or ''
        # Загрузите изображение с использованием PIL
        input_image_pil = Image.open(input_image_file)
        image_response = api.img2img(
            images=[input_image_pil],
            steps=40,
            prompt='RAW photo, ' + prompt,
            negative_prompt='black and white photo',
            cfg_scale=8,
            denoising_strength=0.6,
            sampler_name='DPM++ 2M SDE Karras'
        )
    elif prompt:
        image_response = api.txt2img(
            steps=40,
            prompt='RAW photo, ' + prompt,
            negative_prompt='black and white photo',
            cfg_scale=8,
            denoising_strength=1,
            sampler_name='DPM++ 2M SDE Karras'
        )
    else:
        return Response(status=400, data={'message': 'Please enter prompt or input_image'})


    # Предполагается, что image_response.image является объектом PIL.Image
    # Сохраните его в байты для отправки в ответе
    response_image_io = BytesIO()
    image_response.image.save(response_image_io, format="PNG")  # Замените "JPEG" на нужный формат, если это не JPEG

    return HttpResponse(response_image_io.getvalue(), content_type="image/png")
