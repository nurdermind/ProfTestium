from rest_framework import serializers


class TestsSenderReplySerializer(serializers.Serializer):
    TestID = serializers.IntegerField()
    TestName = serializers.CharField()


class SessionUserSerializer(serializers.Serializer):
    SessionID = serializers.IntegerField()
    testssenderReply = TestsSenderReplySerializer()

    SessionDate = serializers.DateTimeField()
    DurationSession = serializers.DurationField()

    IsSuccessful = serializers.BooleanField()
    FailureReason = serializers.CharField()
    Score = serializers.IntegerField()
    MaxScore = serializers.IntegerField()
