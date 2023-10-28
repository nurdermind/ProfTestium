from django.shortcuts import render
import json
from pprint import pprint
from django.http import *
from django.views.decorators.csrf import csrf_exempt

class UTF8JsonResponse(JsonResponse):
    def __init__(self, *args, json_dumps_params=None, **kwargs):
        json_dumps_params = {"ensure_ascii": False, **(json_dumps_params or {})}
        super().__init__(*args, json_dumps_params=json_dumps_params, **kwargs)

def index(request):
    return render(request, "index.html", {
        "title": "quiz - главная",
        "admin": False
    })
    
def signin(request):
    return render(request, "signin.html", {})
    
def signup(request):
    return render(request, "sugnup.html", {})
    
def profile(request, profile_id):
    return render(request, "profile.html", {
        "admin" : False,
        "user_id" : "",
        "profile_id" : profile_id
    }) # TODO

def test_view(request, test_id):
    return render(request, "test_view.html", {
        "admin": False,
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
    return render(request, "admin/index.html", {})

def admin_tests(request):
    # TODO : 403
    return render(request, "admin/tests.html", {
        "test_list" : []
    })

def admin_users(request):
    # TODO : 403
    return render(request, "admin/users.html", {
        "user_list" : []
    })