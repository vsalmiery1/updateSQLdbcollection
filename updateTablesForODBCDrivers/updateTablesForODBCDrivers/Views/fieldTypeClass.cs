

using System;
using System.Collections.Generic;
using System.Text;
using Contensive.BaseClasses;

namespace updateTablesForODBCDrivers.Views
{
    //
    // this sample creates an addon collection (a group off addons that install together)
    // -- Change the namespace to the collection name
    // 2) Change this class name to the addon name
    // 3) Create a Contensive Addon record with the namespace apCollectionName.ad
    // 3) add reference to CPBase.DLL, typically installed in c:\program files\kma\contensive\
    //
    public class fieldTypeClass : Contensive.BaseClasses.AddonBaseClass
    {
        //
        // -- Contensive calls the execute method of your addon class
        public override object Execute(Contensive.BaseClasses.CPBaseClass cp)
        {
            string result = "";
            
            try
            {
                //
                // 
               List < Models.contentFieldsModel> contentFieldList=Models.contentFieldsModel.createList(cp,"(type=3) or (Type=21)"  );
                var convertedFieldList = new List<string>();

                foreach (Models.contentFieldsModel contentField in contentFieldList) {
                    Models.contentModel content = Models.contentModel.create(cp, contentField.ContentID);
                    string key = (content.ContentTableID + "-" + contentField.name).ToLower() ;
                    if ( !convertedFieldList.Contains(key)) {
                        convertedFieldList.Add(key);
                        cp.Utils.AppendLog("tableUpdates.log", contentField.id + "contentField.ID");
                        cp.Utils.AppendLog("tableUpdates.log", contentField.name + "contentField.name");
                        cp.Utils.AppendLog("tableUpdates.log", contentField.ContentID + "contentField.ContentID");
                        Models.tableModel table = Models.tableModel.create(cp, content.ContentTableID);
                        cp.Utils.AppendLog("tableUpdates.log", content.ContentTableID + "content.ContentTableID");
                        string sqlAddField = "alter table " + table.name + " add " + contentField.name + "new" + " text";
                        cp.Utils.AppendLog("tableUpdates.log", sqlAddField + "new text field");
                        cp.Db.ExecuteSQL(sqlAddField);
                        string sqlCopyData = "Update " + table.name + " set " + contentField.name + "new = " + contentField.name;
                        cp.Utils.AppendLog("tableUpdates.log", sqlCopyData + "copy data to New column");
                        cp.Db.ExecuteSQL(sqlCopyData);
                        string sqlRenameOldField = "sp_rename  '" + table.name + "." + contentField.name + "', '" + contentField.name + "Old2', 'COLUMN'";
                        cp.Utils.AppendLog("tableUpdates.log", sqlRenameOldField + "rename old column");
                        cp.Db.ExecuteSQL(sqlRenameOldField);
                        string sqlRenameNewField = "sp_rename  '" + table.name + "." + contentField.name + "new'" + ", '" + contentField.name + "', 'COLUMN'";
                        cp.Utils.AppendLog("tableUpdates.log", sqlRenameNewField + "rename new column");
                        cp.Db.ExecuteSQL(sqlRenameNewField);
                        result = result + "</br>" + contentField.name + ", sql [" + sqlAddField + ", </br></br>" + sqlCopyData + ", </br></br>" + sqlRenameOldField + ", </br></br>" + sqlRenameNewField + "</br></br>]";
                    }
                }
            }
            catch (Exception ex)
            {
               cp.Site.ErrorReport(ex);
                result =  "error report" + ex;
            }
            return result;
        }
    }
}
