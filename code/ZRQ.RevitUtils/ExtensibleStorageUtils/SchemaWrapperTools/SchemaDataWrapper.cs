﻿using Autodesk.Revit.DB;
using Autodesk.Revit.DB.ExtensibleStorage;
using System;
using System.Collections.Generic;

namespace ZRQ.RevitUtils.ExtensibleStorageUtils.SchemaWrapperTools
{
    /// <summary>
    /// A class to store a list of FieldData objects as well as the top level data (name, access levels, SchemaId, etc..)
    /// of an Autodesk.Revit.DB.ExtensibleStorage.Schema
    /// </summary>
    [Serializable]
    public class SchemaDataWrapper
    {
        #region Constructors
        /// <summary>
        /// For serialization only -- Do not use.
        /// </summary>
        internal SchemaDataWrapper() { }

        /// <summary>
        /// Create a new SchemaDataWrapper
        /// </summary>
        /// <param name="schemaId">The Guid of the Schema</param>
        /// <param name="readAccess">The access level for read permission</param>
        /// <param name="writeAccess">The access level for write permission</param>
        /// <param name="vendorId">The user-registered vendor ID string</param>
        /// <param name="applicationId">The application ID from the application manifest</param>
        /// <param name="name">The name of the schema</param>
        /// <param name="documentation">Descriptive details on the schema</param>
        public SchemaDataWrapper(Guid schemaId, AccessLevel readAccess, AccessLevel writeAccess, string vendorId, string applicationId, string name, string documentation)
        {
            DataList = new List<FieldData>();
            SchemaId = schemaId.ToString();
            ReadAccess = readAccess;
            WriteAccess = writeAccess;
            VendorId = vendorId;
            ApplicationId = applicationId;
            Name = name;
            Documentation = documentation;
        }
        #endregion

        #region Data addition

#if (REVIT2016 || REVIT2017 || REVIT2018 || REVIT2019 || REVIT2020 || REVIT2021)

        /// <summary>
        /// Adds a new field to the wrapper's list of fields.
        /// </summary>
        /// <param name="name">the name of the field</param>
        /// <param name="typeIn">the data type of the field</param>
        /// <param name="unit">The unit type of the Field (set to UT_Undefined for non-floating point types</param>
        /// <param name="subSchema">The SchemaWrapper of the field's subSchema, if the field is of type "Entity"</param>
        public void AddData(string name, System.Type typeIn, UnitType unit, SchemaWrapper subSchema)
        {
            m_DataList.Add(new FieldData(name, typeIn.FullName, unit, subSchema));
        }
#else
        public void AddData(string name, Type typeIn, ForgeTypeId unit, SchemaWrapper subSchema)
        {
            m_DataList.Add(new FieldData(name, typeIn.FullName, unit, subSchema));
        }
#endif

        #endregion

        #region Properties
        /// <summary>
        /// The list of FieldData objects in the wrapper
        /// </summary>
        public List<FieldData> DataList
        {
            get { return m_DataList; }
            set { m_DataList = value; }
        }


        /// <summary>
        /// The schemaId Guid of the Schema
        /// </summary>
        public string SchemaId
        {
            get { return m_schemaId; }
            set { m_schemaId = value; }
        }


        /// <summary>
        /// The read access of the Schema
        /// </summary>
        public AccessLevel ReadAccess
        {
            get { return m_ReadAccess; }
            set { m_ReadAccess = value; }
        }

        /// <summary>
        /// The write access of the Schema
        /// </summary>
        public AccessLevel WriteAccess
        {
            get { return m_WriteAccess; }
            set { m_WriteAccess = value; }
        }

        /// <summary>
        /// Vendor Id
        /// </summary>
        public string VendorId
        {
            get { return m_vendorId; }
            set
            {
                m_vendorId = value;
            }
        }


        /// <summary>
        /// Application Id
        /// </summary>
        public string ApplicationId
        {
            get { return m_applicationId; }
            set { m_applicationId = value; }
        }

        /// <summary>
        /// The documentation string for the schema
        /// </summary>
        public string Documentation
        {
            get { return m_Documentation; }
            set
            {
                m_Documentation = value;
            }

        }

        /// <summary>
        /// The name of the schema
        /// </summary>
        public string Name
        {
            get { return m_Name; }
            set
            {
                m_Name = value;
            }
        }

        #endregion

        #region Data
        private AccessLevel m_ReadAccess;
        private AccessLevel m_WriteAccess;
        private List<FieldData> m_DataList;
        private string m_applicationId;
        private string m_schemaId;
        private string m_vendorId;
        private string m_Name;
        private string m_Documentation;
        #endregion
    }
}