using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    /// <summary>
    /// 處理或創建測試幾臺交互文件
    /// </summary>
    public class FileHelper
    {

        /// <summary>
        /// 創建句柄信息文件
        /// </summary>
        /// <param name="testLogPath">測試目錄</param>
        /// <param name="handle">應用句柄</param>
        public static void GeneralHandleFile(string testLogPath, int handle)
        {
            DirectoryInfo dInfo = new DirectoryInfo(testLogPath);
            FileInfo[] fInfo = dInfo.GetFiles("*.txt");

            Array.ForEach(fInfo, p => { p.Delete(); });

            string fileName = testLogPath + "\\Handle.txt";
            FileStream fs = null;
            StreamWriter sw = null;
            try
            {
                fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
                sw = new StreamWriter(fs, Encoding.ASCII);
                sw.WriteLine("Handle=" + Convert.ToString(handle));                
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (sw != null) sw.Close();
                if (fs != null) fs.Close();
            }
        }
        
        /// <summary>
        /// 創建WIP檢查記錄及數據信息文件，供測試程式讀取
        /// </summary>
        /// <param name="status">結果</param>
        /// <param name="TestLogPath">測試文件路徑</param>
        /// <param name="fixture">治具編號</param>
        /// <param name="myHandle">句柄</param>
        /// <param name="userInfo">操作員</param>
        public static void GeneralWipInfo(string status,string TestLogPath,string pid,string fixture, int myHandle, UserInfo userInfo,IList<TextValueInfo> keys)
        {
            string fileName = TestLogPath + "\\" + fixture + "_WIP_INFO.txt";
            FileStream fs = null;
            StreamWriter sw = null;
            try
            {
                fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
                sw = new StreamWriter(fs, Encoding.UTF8);
                sw.WriteLine("PERMISSION=" + status);
                sw.WriteLine("HANDLE=" + myHandle.ToString());
                sw.WriteLine("WIP_ID=" + fixture);
                sw.WriteLine("WIP_NO=" + pid);
                //sw.WriteLine("LINE_ID=" + Convert.ToString(station.Line.ID));
                //sw.WriteLine("STATION_ID=" + Convert.ToString(station.Group.ID));
                //sw.WriteLine("STATION_NO=" + Convert.ToString(station.ID));
                sw.WriteLine("USER_NAME=" + userInfo.UserCode);  
                //寫如Key值
                if (keys != null)
                {
                    foreach (TextValueInfo tvi in keys)
                    {
                        sw.WriteLine(tvi.Text + "=" + tvi.Value);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (sw != null) sw.Close();
                if (fs != null) fs.Close();
            }
        }

        /// <summary>
        /// 讀取測試產生的文件內容
        /// </summary>
        /// <param name="testLogPath"></param>
        /// <param name="testUnit"></param>
        /// <param name="wipno"></param>
        /// <param name="handle"></param>
        /// <param name="strImei"></param>
        /// <returns></returns>
        public static string ReadTestWip(string testLogPath, int testUnit, out string wipno, out int handle, out IList<TextValueInfo> keys)
        {
            string wipFile = testLogPath + "\\" + Convert.ToString(testUnit) + "_WIP.txt";
            string strReturn = "TRUE^";            
            wipno = "";
            handle = 0;
            keys = new List<TextValueInfo>();
            if (File.Exists(wipFile))
            {
                FileStream fs = null;
                StreamReader sw = null;
                try
                {
                    fs = new FileStream(wipFile, FileMode.Open, FileAccess.Read);
                    sw = new StreamReader(fs, Encoding.Default);

                    string temp = sw.ReadLine();
                    while (!string.IsNullOrEmpty(temp))
                    {
                        temp = temp.ToLower().Trim();
                        if (temp.StartsWith("wip_no"))
                        {
                            wipno = temp.Split('=')[1].ToUpper();
                            if (string.IsNullOrWhiteSpace(wipno))
                            { strReturn = "FALSE^wipno is null!"; }
                        }
                        else if (temp.StartsWith("handle"))
                        {
                            handle = Convert.ToInt32(temp.Split('=')[1]);
                        }
                        else
                        {
                            string[] temps = temp.Split('=');
                            keys.Add(new TextValueInfo { Text = temps[0].ToUpper(), Value = temps[1].ToUpper() }); 
                        }
                        temp = sw.ReadLine();
                    }
                    sw.Close();
                    fs.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (sw != null) sw.Close();
                    if (fs != null) fs.Close();

                    //刪除文件                    
                    //File.Delete(wipFile);
                }
            }
            else
            {
                strReturn = "FALSE^Wip File does not exist.";
            }
            return strReturn;
        }
    }
}
