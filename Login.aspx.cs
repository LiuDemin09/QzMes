using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using BLL;
using Freeworks.ORM;
using System.Data;
public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!this.Page.IsPostBack)
        {
            //DataContext DBContext = DataServiceFactory.Create("MesLF");
            //ICollection<SysBllConfig> objs = DBContext.LoadArray<SysBllConfig>();

            //ICollection<SysBllConfig> obj2s = DBContext.FindArray<SysBllConfig>((SysBllConfig.Meta.ID != "1"));
            //string sql = @" select USER_CODE, USER_NAME, DEPT_NAME, PHONE mobile from BASEDATA.SYS_USER where USER_CODE not in(
            //                      select USER_CODE from MES_MASTER.SYS_USER)  order by USER_CODE";
            //sql = " select USER_CODE, USER_NAME, DEPT_NAME from MES_MASTER.V_SYS_USER";
            //DataSet ds = DBContext.ExcuteSql(sql).ToDataSet();
            //if (ds != null)
            //{
            //    string s = "4";
            //}
            //int i = DateTime.Compare(DateTime.Now, DateTime.Today.AddDays(5));
           // TimeSpan tempTaskTime =TimeSpan.ParseExact("00:00:34.5403364", @"hh\:mm\:ss\.fffffff", null);
            //TimeSpan dt1 = DateTime.Now - Convert.ToDateTime("2017/06/22 10:00:00");
            //TimeSpan d3 = tempTaskTime + dt1;
            //00:00:34.5403364
        }
    } 
}