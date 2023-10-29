import grpc
import proto.sessionsender_pb2 as s_pb2
import proto.sessionsender_pb2_grpc as s_pb2_grpc
import proto.detailsessionsender_pb2 as sd_pb2
import proto.detailsessionsender_pb2_grpc as sd_pb2_grpc


class SessionHelper:

    def __init__(self, user_id):
        self.user_id = user_id

    def list(self):
        with grpc.insecure_channel('localhost:50051') as channel:
            stub = s_pb2_grpc.SessionSenderStub(channel)
            response = stub.GetSessionList(s_pb2.SessionSenderRequest(UserID=self.user_id))
            return response

    def retrieve(self, session_id):
        with grpc.insecure_channel('localhost:50051') as channel:
            stub = sd_pb2_grpc.DetailSessionSenderStub(channel)
            response = stub.GetDetailSession(sd_pb2.DetailSessionSenderRequest(SessionID=session_id))
            return response

    def create(self, data):
        pass
