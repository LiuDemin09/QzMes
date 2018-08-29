using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BLL;
using DAL;
using MES;
public partial class Pages_TechnologyManage_AllocateTaskUpload : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
           // DoUpload();
        }
    }

    protected void DoUpload()
    {
        try
        {
            HttpPostedFile file = file1.PostedFile;

            DateTime dateTime = DateTime.Now;

            string fileName = dateTime.ToString("yyyyMMddhhmmssffff");
            if (!Directory.Exists(WebHelper.GetAttachmentTempPath()))
            {
                Directory.CreateDirectory(WebHelper.GetAttachmentTempPath());
            }

            string fullName = WebHelper.GetAttachmentTempPath() + "/" + fileName;

            string strNewPath = fullName + WebHelper.GetExtension(file.FileName);
            file.SaveAs(strNewPath);

            BasAttachmentInfo att = new BasAttachmentInfo();
            att.PhysicalPath = strNewPath;
            att.FileNo = fileName;
            att.OriginalName = Path.GetFileName(file.FileName);
            att.FriendlyKey = "N/A";
            att.ExtensionName = WebHelper.GetExtension(file.FileName);
            att.FileName = fileName + att.ExtensionName;
            att.FullPath = "../../Uploads" + "/" + fileName + WebHelper.GetExtension(file.FileName);
            att.FileType = 0;
            att.UpdatedDate = DateTime.Now;
            att.UpdatedBy = WebHelper.GetUserInfo().UserCode;
            PubHelper.GetHelper(WebHelper.GetDB(WebHelper.GetUserInfo())).SaveAttachment(att);
            if (SaveToDB(strNewPath, "AllocateTask"))
            {
                Label1.Text = "上传完毕";
            }
            WriteJs("uploadsuccess('" + att.FileNo + "','" + att.OriginalName + "','" + att.FullPath + "'); ");
            
        }
        catch
        {
            WriteJs("uploaderror();");
        }
    }

    protected void WriteJs(string jsContent)
    {
        this.Page.ClientScript.RegisterStartupScript(GetType(), "writejs", "<script type='text/javascript'>" + jsContent + "</script>");
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        DoUpload();
    }

    protected bool SaveToDB(string path,string tablename)
    {
        try
        {
            DataTable dt = ExcelManage.InputFromExcel(path, tablename);
            if (dt != null && dt.Rows.Count > 0)
            {
                WsSystem wbi = new WsSystem();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (!string.IsNullOrEmpty(dt.Rows[i][0].ToString()))
                    {
                        TechnologyWip bb = new TechnologyWip();
                        bb.PARTSDRAWINGNO = dt.Rows[i][0].ToString();// PubHelper.GetHelper().GetNextID("MES_MASTER.SEQ_ALL_ID").ToString(); 
                        bb.CustName = dt.Rows[i][1].ToString();
                        bb.ProductName = dt.Rows[i][2].ToString(); 
                        bb.ProcessName = dt.Rows[i][3].ToString();
                        bb.ProcessEngineer = wbi.FindUserCodeByUserName(bb.ProcessName);
                        bb.PlanDate = Convert.ToDateTime(dt.Rows[i][4].ToString());
                        bb.STATUS = 1;
                        bb.StatusMemo = "分派工艺"; 
                         wbi.SaveTechnologyInfoForImport(bb); 
                    }
                }
                return true;
            }
            else
            {
                Label1.Text = "上传完毕，但数据为空，保存失败";
            }
            return true;
        }
        catch (Exception ex)
        {
            Label1.Text = ex.ToString();
            return false;
        }
    }
}