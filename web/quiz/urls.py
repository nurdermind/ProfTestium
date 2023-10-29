from django.contrib import admin
from django.urls import path, include

urlpatterns = [
    path('hidden_admin/', admin.site.urls),
    path("api/", include("api.urls")),
    path("", include("quizsite.urls"))
]
