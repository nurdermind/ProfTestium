from rest_framework import serializers


class PictureSerializer(serializers.Serializer):
    PictureID = serializers.IntegerField()
    PathPicture = serializers.CharField()


class QuestionsTypeReplySerializer(serializers.Serializer):
    TypeID = serializers.IntegerField()
    TypeName = serializers.IntegerField


class QuestionSerializer(serializers.Serializer):
    QuestionID = serializers.IntegerField()
    QuestionText = serializers.CharField()
    MaxScore = serializers.IntegerField()
    TypeQuestion = QuestionsTypeReplySerializer()
    Picture = PictureSerializer()


class AnswerVariantSerializer(serializers.Serializer):
    CodeofAnswer = serializers.IntegerField()
    IsCorrect = serializers.BooleanField()
    QuestionID = serializers.IntegerField()
    TextVariant = serializers.CharField()


class OpenQuestionAnswerSerializer(serializers.Serializer):
    AnswerID = serializers.IntegerField()
    UserAnswerText = serializers.CharField()
    CorrectAnswer = serializers.CharField()
    UserID = serializers.IntegerField()
    Score = serializers.IntegerField()
    QuestionID = serializers.IntegerField()


class QuestionAnswerSerializer(serializers.Serializer):
    CodeofAnswer = serializers.IntegerField()
    IsCorrect = serializers.BooleanField()
    QuestionID = serializers.IntegerField()
    TextVariant = serializers.CharField()
