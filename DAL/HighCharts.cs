using System;
using System.Collections.Generic;
using Freeworks.ORM.Core;

namespace DAL
{
    //[Serializable()]
    //[OrmClassAttribute(TableName = "MES_MASTER.ORDER_HEAD", IsView = false, PrimaryKeys = "ORDER_NO",
    //PrimaryProperties = "OrderNo")]
    public class HighCharts : ICloneable
    {
        #region Member Variables		
        public static HighCharts Meta = new HighCharts();
        #endregion

        #region constructor
        public HighCharts()
        {
            ///Initialize Child collection objects
        }
        #endregion

        #region Property Variables		
      
        public string name { set; get; }

      
        public double[] data { set; get; }

       

        #endregion
        #region "Child properties"
        #endregion

        #region extension methods
        public Object Clone()
        {
            HighCharts obj = new HighCharts();

            obj.name = this.name;

            obj.data = this.data;


            return obj;
        }

        public void CopyTo(HighCharts obj)
        {
            obj.name = this.name;
            obj.data = this.data;
     
        }
        #endregion
    }


}

