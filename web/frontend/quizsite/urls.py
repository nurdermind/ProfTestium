from django.urls import path
from . import views

# TODO: сайт ссылки
'''
urlpatterns = [
    path("", views.index, name="index"),
    path("items", views.item_list, name="item_list"),
    path("items_v2", views.item_list_v2, name="item_list_v2"),
    path("item/craft_tree/<str:item>/<int:recipe_id>", views.item_craft_tree),
    path("item/performance/<str:item>", views.item_performance),
    path("item", views.item),
    path("info", views.info, name="info"),
    #------------
    path("api/item_info.json", views.api_getItemStats),
    path("api/item_sinfo.json", views.api_getItemShortInfo),
    path("api/item.html", views.api_getItemHTML),
    path("api/item_list.json", views.api_itemList)
]'''