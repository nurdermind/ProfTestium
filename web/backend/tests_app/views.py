from rest_framework import viewsets
from rest_framework.response import Response

from tests_app.serializers.sessions_serializers import SessionUserSerializer


class SessionViewSet(viewsets.GenericViewSet):
    lookup_url_kwarg = 'session_id'

    def list(self, request):
        user_id = self.kwargs['user_id']
        # TODO: Вызов SessionHelper(user_id=user_id).list()
        sessions = []
        return Response(data=SessionUserSerializer(sessions, many=True).data)

    def retrieve(self, request, session_id):
        user_id = self.kwargs['user_id']
        # TODO: Вызов self._session_helper.retrieve(session_id=session_id)
        session = ...
        return Response(data=SessionUserSerializer(session).data)

    def create(self, request, session_id):
        user_id = self.kwargs['user_id']
        # TODO: Вызов self._session_helper.create(data=serializer.validated_data)

    @property
    def _session_helper(self):
        user_id = self.kwargs['user_id']
        # TODO: return SessionHelper(user_id=user_id)
        return None
