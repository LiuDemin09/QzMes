using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

public class ShowSubMenu
{
    public string menuid;
    public string menuname;
    public string icon;
    public string url;
    public ShowSubMenu()
    {
        menuid = string.Empty;
        menuname = string.Empty;
        icon = "icon-nav";
        url = string.Empty;
    }
}
/// <summary>
/// ShowMenu 的摘要说明
/// </summary>
public class ShowMenu
{
    public string menuid;
    public string icon;
    public string menuname;
    public List<ShowSubMenu> menus;
    public ShowMenu()
    {
        menuid = string.Empty;
        icon = "icon-sys";
        menuname = string.Empty;
        menus = new List<ShowSubMenu>();
    }
}