from django.urls import path, include
from rest_framework.routers import DefaultRouter

from api.views import SessionViewSet, generate_images, TestsViewSet, QuestionViewSet

session_router = DefaultRouter()
session_router.register('sessions/', SessionViewSet, basename='session')

tests_router = DefaultRouter()
tests_router.register('tests', TestsViewSet, 'tests')

question_router = DefaultRouter()
question_router.register('questions', QuestionViewSet, 'questions')


urlpatterns = [
    path('generate_images/', generate_images, name='image-generator'),
    path('<int:user_id>/', include(session_router.urls)),
    path('<int:test_id>/', include(question_router.urls)),
    path('', include(tests_router.urls)),
]
