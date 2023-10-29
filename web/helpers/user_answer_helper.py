import grpc

import proto.useranswerssender_pb2 as s_pb2
import proto.useranswerssender_pb2_grpc as s_pb2_grpc


class UserAnswerHelper:

    def __init__(self, user_id):
        self.user_id = user_id

    def retrieve(self, question_id):
        with grpc.insecure_channel('localhost:50051') as channel:
            stub = s_pb2_grpc.UserAnswersSenderStub(channel)
            response = stub.GetUserAnswers(s_pb2.GetUserAnswersRequest(QuestionID=question_id, UserID=self.user_id))
            return response

    def create(self, data):
        pass
