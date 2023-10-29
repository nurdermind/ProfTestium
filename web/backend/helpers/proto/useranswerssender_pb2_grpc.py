# Generated by the gRPC Python protocol compiler plugin. DO NOT EDIT!
"""Client and server classes corresponding to protobuf-defined services."""
import grpc

import useranswerssender_pb2 as useranswerssender__pb2


class UserAnswersSenderStub(object):
    """Missing associated documentation comment in .proto file."""

    def __init__(self, channel):
        """Constructor.

        Args:
            channel: A grpc.Channel.
        """
        self.GetUserAnswers = channel.unary_unary(
                '/greet.UserAnswersSender/GetUserAnswers',
                request_serializer=useranswerssender__pb2.GetUserAnswersRequest.SerializeToString,
                response_deserializer=useranswerssender__pb2.GetUserAnswerReply.FromString,
                )


class UserAnswersSenderServicer(object):
    """Missing associated documentation comment in .proto file."""

    def GetUserAnswers(self, request, context):
        """�������� ����� ������������ �� �������� ������(����� �� �������� ���������� � detailtestlist)
        """
        context.set_code(grpc.StatusCode.UNIMPLEMENTED)
        context.set_details('Method not implemented!')
        raise NotImplementedError('Method not implemented!')


def add_UserAnswersSenderServicer_to_server(servicer, server):
    rpc_method_handlers = {
            'GetUserAnswers': grpc.unary_unary_rpc_method_handler(
                    servicer.GetUserAnswers,
                    request_deserializer=useranswerssender__pb2.GetUserAnswersRequest.FromString,
                    response_serializer=useranswerssender__pb2.GetUserAnswerReply.SerializeToString,
            ),
    }
    generic_handler = grpc.method_handlers_generic_handler(
            'greet.UserAnswersSender', rpc_method_handlers)
    server.add_generic_rpc_handlers((generic_handler,))


 # This class is part of an EXPERIMENTAL API.
class UserAnswersSender(object):
    """Missing associated documentation comment in .proto file."""

    @staticmethod
    def GetUserAnswers(request,
            target,
            options=(),
            channel_credentials=None,
            call_credentials=None,
            insecure=False,
            compression=None,
            wait_for_ready=None,
            timeout=None,
            metadata=None):
        return grpc.experimental.unary_unary(request, target, '/greet.UserAnswersSender/GetUserAnswers',
            useranswerssender__pb2.GetUserAnswersRequest.SerializeToString,
            useranswerssender__pb2.GetUserAnswerReply.FromString,
            options, channel_credentials,
            insecure, call_credentials, compression, wait_for_ready, timeout, metadata)
