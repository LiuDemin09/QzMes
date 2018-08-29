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
public partial class Pages_PrintManage_TemplateUpload : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            
        }
        BindTemplateType();
    }

    protected void BindTemplateType()
    {
        WsSystem ws = new WsSystem();

        TextValueInfo[] vts = ws.ListBindTemplateType();
        
        for (var i = 0; i < vts.Length; i++)
        {
            DropDownList1.Items.Add(new ListItem(vts[i].Text, vts[i].Value));
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
            string savepath = fileName+ WebHelper.GetExtension(file.FileName);
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
            if (SaveToDB(strNewPath, savepath))
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

    protected bool SaveToDB(string path,string savepath)
    {
        try
        {
            string templatetype = DropDownList1.SelectedItem.Value;
            string templatetypename = DropDownList1.SelectedItem.Text;
            WsSystem ws = new WsSystem();
            PrintSet bb = new PrintSet();
            bb.ID = "";
            bb.TemplatePath = savepath;
            bb.TemplateType = templatetype;
            bb.MEMO = templatetypename;
            bb.ACTIVE = "0";
            ws.SavePrintLabelTemplate(bb);
            return true;
        }
        catch (Exception ex)
        {
            Label1.Text = ex.ToString();
            return false;
        }
    }
}