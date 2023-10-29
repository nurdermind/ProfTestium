import grpc

import helpers.proto.detailtestslist_pb2 as s_pb2
import helpers.proto.detailtestslist_pb2_grpc as s_pb2_grpc


class QuestionHelper:

    def __init__(self, test_id):
        self.test_id = test_id

    def list(self):
        with grpc.insecure_channel('localhost:50051') as channel:
            stub = s_pb2_grpc.DetailTestsListSenderStub(channel)
            response = stub.GetQuestionsList(s_pb2.GetQuestionsListRequest(TestID=self.test_id))
            return response

    def list_open_question_answers(self, user_id):
        with grpc.insecure_channel('localhost:50051') as channel:
            stub = s_pb2_grpc.DetailTestsListSenderStub(channel)
            response = stub.GetOpenQuestionAnswer(s_pb2.GetOpenQuestionAnswersOfUserRequest(UserID=user_id))
            return response

    def variant_answer(self, question_id):
        with grpc.insecure_channel('localhost:50051') as channel:
            stub = s_pb2_grpc.DetailTestsListSenderStub(channel)
            response = stub.GetCloseQuestionAnswer(s_pb2.GetVariantsAnswareOfCloseTypeQuestion(QuestionID=question_id))
            return response

    def list_answers_user(self, user_id):
        with grpc.insecure_channel('localhost:50051') as channel:
            stub = s_pb2_grpc.DetailTestsListSenderStub(channel)
            response = stub.GetUserAnswers(s_pb2.GetUserAnswersRequest(UserID=user_id))
            return response

    def create(self, data):
        pass
