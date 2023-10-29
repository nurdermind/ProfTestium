from django.urls import path, include
from rest_framework.routers import DefaultRouter

from api.views import SessionViewSet


session_router = DefaultRouter()
session_router.register('sessions/', SessionViewSet, basename='session')


urlpatterns = [
    path('<int:user_id>/', include(session_router.urls))
]
