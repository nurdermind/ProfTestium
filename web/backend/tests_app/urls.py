from django.urls import path, include
from rest_framework.routers import DefaultRouter

from tests_app.views import SessionViewSet


session_router = DefaultRouter()
session_router.register('sessions/', SessionViewSet)


urlpatterns = [
    path('<int:user_id>/', include(session_router.urls))
]
