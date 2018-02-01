
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
    public class contentFieldsModel : baseModel
    {
        //
        //====================================================================================================
        //-- const
        //------ set content name
        public const string contentName = "Content Fields";
        //------ set to tablename for the primary content (used for cache names)
        public const string contentTableName = "ccFields";
        //
        //====================================================================================================
        // -- instance properties
        public bool AdminOnly { get; set; }
        public bool Authorable { get; set; }
        public string Caption { get; set; }
        public int ContentCategoryID { get; set; }
        public int ContentID { get; set; }
        public bool createResourceFilesOnRoot { get; set; }
        public string DefaultValue { get; set; }
        public bool DeveloperOnly { get; set; }
        public int editorAddonID { get; set; }
        public int EditSortPriority { get; set; }
        public string EditTab { get; set; }
        public bool HTMLContent { get; set; }
        public int IndexColumn { get; set; }
        public int IndexSortDirection { get; set; }
        public int IndexSortPriority { get; set; }
        public string IndexWidth { get; set; }
        public int InstalledByCollectionID { get; set; }
        public bool IsBaseField { get; set; }
        public int LookupContentID { get; set; }
        public string LookupList { get; set; }
        public int ManyToManyContentID { get; set; }
        public int ManyToManyRuleContentID { get; set; }
        public string ManyToManyRulePrimaryField { get; set; }
        public string ManyToManyRuleSecondaryField { get; set; }
        public int MemberSelectGroupID { get; set; }
        public bool NotEditable { get; set; }
        public bool Password { get; set; }
        public string prefixForRootResourceFiles { get; set; }
        public bool ReadOnly { get; set; }
        public int RedirectContentID { get; set; }
        public string RedirectID { get; set; }
        public string RedirectPath { get; set; }
        public bool Required { get; set; }
        public bool RSSDescriptionField { get; set; }
        public bool RSSTitleField { get; set; }
        public bool Scramble { get; set; }
        public bool TextBuffered { get; set; }
        public int Type { get; set; }
        public bool UniqueName { get; set; }
        //
        //====================================================================================================
        public static contentFieldsModel @add(CPBaseClass cp)
        {
            return @add<contentFieldsModel>(cp);
        }
        //
        //====================================================================================================
        public static contentFieldsModel create(CPBaseClass cp, int recordId)
        {
            return create<contentFieldsModel>(cp, recordId);
        }
        //
        //====================================================================================================
        public static contentFieldsModel create(CPBaseClass cp, string recordGuid)
        {
            return create<contentFieldsModel>(cp, recordGuid);
        }
        //
        //====================================================================================================
        public static contentFieldsModel createByName(CPBaseClass cp, string recordName)
        {
            return createByName<contentFieldsModel>(cp, recordName);
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
            delete<contentFieldsModel>(cp, recordId);
        }
        //
        //====================================================================================================
        public static void delete(CPBaseClass cp, string ccGuid)
        {
            delete<contentFieldsModel>(cp, ccGuid);
        }
        //
        //====================================================================================================
        public static List<contentFieldsModel> createList(CPBaseClass cp, string sqlCriteria, string sqlOrderBy = "id")
        {
            return createList<contentFieldsModel>(cp, sqlCriteria, sqlOrderBy);
        }
        //
        //====================================================================================================
        public static string getRecordName(CPBaseClass cp, int recordId)
        {
            return baseModel.getRecordName<contentFieldsModel>(cp, recordId);
        }
        //
        //====================================================================================================
        public static string getRecordName(CPBaseClass cp, string ccGuid)
        {
            return baseModel.getRecordName<contentFieldsModel>(cp, ccGuid);
        }
        //
        //====================================================================================================
        public static int getRecordId(CPBaseClass cp, string ccGuid)
        {
            return baseModel.getRecordId<contentFieldsModel>(cp, ccGuid);
        }
    }
}
