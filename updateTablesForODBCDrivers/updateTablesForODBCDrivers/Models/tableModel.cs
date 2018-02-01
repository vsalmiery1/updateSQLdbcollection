
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;
using Contensive.BaseClasses;

namespace updateTablesForODBCDrivers.Models
{
    public class tableModel : baseModel
    {
        //
        //====================================================================================================
        //-- const
        //------ set content name
        public const string contentName = "tables";
        //------ set to tablename for the primary content (used for cache names)
        public const string contentTableName = "ccTables";
        //
        //====================================================================================================
        // -- instance properties
        public int ContentCategoryID { get; set; }
        public int DataSourceID { get; set; }
        //
        //====================================================================================================
        public static tableModel @add(CPBaseClass cp)
        {
            return @add<tableModel>(cp);
        }
        //
        //====================================================================================================
        public static tableModel create(CPBaseClass cp, int recordId)
        {
            return create<tableModel>(cp, recordId);
        }
        //
        //====================================================================================================
        public static tableModel create(CPBaseClass cp, string recordGuid)
        {
            return create<tableModel>(cp, recordGuid);
        }
        //
        //====================================================================================================
        public static tableModel createByName(CPBaseClass cp, string recordName)
        {
            return createByName<tableModel>(cp, recordName);
        }
        //
        //====================================================================================================
        public void save(CPBaseClass cp)
        {
            base.save(cp);
        }
        //
        //====================================================================================================
        public static void delete(CPBaseClass cp, int recordId)
        {
            delete<tableModel>(cp, recordId);
        }
        //
        //====================================================================================================
        public static void delete(CPBaseClass cp, string ccGuid)
        {
            delete<tableModel>(cp, ccGuid);
        }
        //
        //====================================================================================================
        public static List<tableModel> createList(CPBaseClass cp, string sqlCriteria, string sqlOrderBy = "id")
        {
            return createList<tableModel>(cp, sqlCriteria, sqlOrderBy);
        }
        //
        //====================================================================================================
        public static string getRecordName(CPBaseClass cp, int recordId)
        {
            return baseModel.getRecordName<tableModel>(cp, recordId);
        }
        //
        //====================================================================================================
        public static string getRecordName(CPBaseClass cp, string ccGuid)
        {
            return baseModel.getRecordName<tableModel>(cp, ccGuid);
        }
        //
        //====================================================================================================
        public static int getRecordId(CPBaseClass cp, string ccGuid)
        {
            return baseModel.getRecordId<tableModel>(cp, ccGuid);
        }
    }
}
