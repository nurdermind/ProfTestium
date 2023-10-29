from django.shortcuts import render
import json
from pprint import pprint
from django.http import *
from django.views.decorators.csrf import csrf_exempt

class UTF8JsonResponse(JsonResponse):
    def __init__(self, *args, json_dumps_params=None, **kwargs):
        json_dumps_params = {"ensure_ascii": False, **(json_dumps_params or {})}
        super().__init__(*args, json_dumps_params=json_dumps_params, **kwargs)

sessions = [] # туты описываются сессии
profiles = [
    {
        "name": "debug",
        "admin": True,
        "test_editing": True,
        "profile_id": 0,
        "created_tests": [],
        "completed_tests": []
    }
] # туты описываются профили (кеш) TODO данные туту не имеют отношение к бд и это на исправить

def http_error(request,e_id:int):
    return render(request, "error.html", {
        "errorcode": e_id,
    })

def get_session(request):
    jstr = request.body.decode('utf8')
    data = json.loads()

def index(request):
    return render(request, "index.html", {
        "title": "Quiz",
        "admin": False
    })
    
def signin(request):
    return render(request, "signin.html", {
        "title": "Quiz - Войти в систему"
    })
    
def signup(request):
    return render(request, "signup.html", {
        "title": "Quiz - Регистрация"
    })
    
def profile(request, profile_id):
    return render(request, "profile.html", {
        "admin" : False,
        "title": "Quiz - Профиль "+profile_id, # TODO: показать здесь имя пользователя а не id
        "user_id" : "",
        "profile_id" : profile_id
    }) # TODO

def test_view(request, test_id):
    return render(request, "test_view.html", {
        "admin": False,
        "title": "Quiz - Тест "+"ЗАБЫЛИ НАЗВАНИЕ ТЕСТА",
        "test_name" : "",
        "status" : "",
        "score" : "",
        "questions" : "",
        "time" : "",
        "solved_list" : []
    }) # TODO

def test_editor(request, test_id):
    return render(request, "test.html", {
        "admin" : False,
        "editor" : True,
        "problem_list" : []
    }) # TODO

def test_solve(request, test_id):
    return render(request, "test.html", {
        "admin" : False,
        "editor" : False,
        "problem_list" : []
    }) # TODO

def admin(request):
    # TODO : 403
    return render(request, "admin/index.html", {
        "title": "Quiz - Администратор"
    })

def admin_tests(request):
    # TODO : 403
    return render(request, "admin/tests.html", {
        "title": "Quiz - Тесты",
        "test_list" : []
    })

def admin_users(request):
    # TODO : 403
    return render(request, "admin/users.html", {
        "title": "Quiz - Пользователи",
        "user_list" : []
    })