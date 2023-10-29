import grpc
import helpers.proto.testlistsender_pb2 as s_pb2
import helpers.proto.testlistsender_pb2_grpc as s_pb2_grpc
import helpers.proto.detailtestsender_pb2 as d_pb2
import helpers.proto.detailtestsender_pb2_grpc as d_pb2_grpc


class TestsHelper:

    def list(self):
        with grpc.insecure_channel('localhost:50051') as channel:
            stub = s_pb2_grpc.TestsListSenderStub(channel)
            response = stub.GetTestsList(s_pb2.TestsListSenderRequest())
            return response

    def detail(self,test_id):
        with grpc.insecure_channel('localhost:50051') as channel:
            stub = d_pb2_grpc.DetailTestSenderStub(channel)
            response = stub.GetSetailTest(d_pb2.DetailListSenderRequest(TestID=test_id))
            return response

    def create(self, data):
        pass
