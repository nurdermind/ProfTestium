from rest_framework import serializers



class TestSerializer(serializers.Serializer):
    TestID = serializers.IntegerField()
    TestName = serializers.CharField()