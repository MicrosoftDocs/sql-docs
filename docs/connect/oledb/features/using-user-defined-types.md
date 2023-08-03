---
title: "Using User-Defined Types"
description: The OLE DB Driver for SQL Server supports user-defined types as binary types with metadata information, which allows you to manage them as objects.
author: David-Engel
ms.author: v-davidengel
ms.date: "06/12/2018"
ms.service: sql
ms.subservice: connectivity
ms.topic: "reference"
helpviewer_keywords:
  - "DBPROPSET_DATASOURCEINFO property set"
  - "UDTs [OLE DB Driver for SQL Server]"
  - "user-defined types [SQL Server], OLE DB Driver for SQL Server"
  - "OLE DB Driver for SQL Server, user-defined types"
  - "DBPROPSET_SQLSERVERPARAMETER property set"
  - "IColumnsRowset interface"
  - "MSOLEDBSQL, user-defined types"
  - "data access [OLE DB Driver for SQL Server], user-defined types"
  - "ISSCommandWithParameters interface"
---
# Using User-Defined Types
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

  [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)] introduced user-defined types (UDTs). UDTs extend the SQL type system by allowing you to store objects and custom data structures in a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] database. UDTs can contain multiple data types and can have behaviors, differentiating them from the traditional alias data types that consist of a single [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] system data type. UDTs are defined using any of the languages supported by the .NET common language runtime (CLR) that produce verifiable code. This includes Microsoft Visual C#<sup>®</sup> and Visual Basic<sup>®</sup> .NET. The data is exposed as fields and properties of a .NET class or structure, and behaviors are defined by methods of the class or structure.  
  
 A UDT can be used as the column definition of a table, as a variable in a [!INCLUDE[tsql](../../../includes/tsql-md.md)] batch, or as an argument of a [!INCLUDE[tsql](../../../includes/tsql-md.md)] function or stored procedure.  
  
## OLE DB Driver for SQL Server  
 The OLE DB Driver for SQL Server supports UDTs as binary types with metadata information, which allows you to manage UDTs as objects. UDT columns are exposed as DBTYPE_UDT, and their metadata are exposed through the core OLE DB interface **IColumnRowset**, and the new [ISSCommandWithParameters](../../oledb/ole-db-interfaces/isscommandwithparameters-ole-db.md) interface.  
  
> [!NOTE]  
>  The **IRowsetFind::FindNextRow** method does not work with the UDT data type. DB_E_BADCOMPAREOP is returned if the UDT is used as a search column type.  
  
### Data Bindings and Coercions  
 The following table describes the binding and coercion that occurs when using the listed data types with a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] UDT. UDT columns are exposed through the OLE DB Driver for SQL Server as DBTYPE_UDT. You can get metadata through the appropriate schema rowsets so you can manage your own defined types as objects.  
  
|Data type|To Server<br /><br /> **UDT**|To Server<br /><br /> **non-UDT**|From Server<br /><br /> **UDT**|From Server<br /><br /> **non-UDT**|  
|---------------|---------------------------|--------------------------------|-----------------------------|----------------------------------|  
|DBTYPE_UDT|Supported<sup>6</sup>|Error<sup>1</sup>|Supported<sup>6</sup>|Error<sup>5</sup>|  
|DBTYPE_BYTES|Supported<sup>6</sup>|N/A<sup>2</sup>|Supported<sup>6</sup>|N/A<sup>2</sup>|  
|DBTYPE_WSTR|Supported<sup>3,6</sup>|N/A<sup>2</sup>|Supported<sup>4,6</sup>|N/A<sup>2</sup>|  
|DBTYPE_BSTR|Supported<sup>3,6</sup>|N/A<sup>2</sup>|Supported<sup>4</sup>|N/A<sup>2</sup>|  
|DBTYPE_STR|Supported<sup>3,6</sup>|N/A<sup>2</sup>|Supported<sup>4,6</sup>|N/A<sup>2</sup>|  
|DBTYPE_IUNKNOWN|Not supported|N/A<sup>2</sup>|Not supported|N/A<sup>2</sup>|  
|DBTYPE_VARIANT (VT_UI1 &#124; VT_ARRAY)|Supported<sup>6</sup>|N/A<sup>2</sup>|Supported<sup>4</sup>|N/A<sup>2</sup>|  
|DBTYPE_VARIANT (VT_BSTR)|Supported<sup>3,6</sup>|N/A<sup>2</sup>|N/A|N/A<sup>2</sup>|  
  
 <sup>1</sup>If a server type other than DBTYPE_UDT is specified with **ICommandWithParameters::SetParameterInfo** and the accessor type is DBTYPE_UDT, an error occurs when the statement is executed (DB_E_ERRORSOCCURRED; the parameter status is DBSTATUS_E_BADACCESSOR). Otherwise the data is sent to the server, but the server returns an error indicating that there is no implicit conversion from UDT to the parameter's data type.  
  
 <sup>2</sup>Beyond the scope of this article.  
  
 <sup>3</sup> Data conversion from hex string to binary data occurs.  
  
 <sup>4</sup> Data conversion from binary data to hex string occurs.  
  
 <sup>5</sup>Validation can occur at create accessor time, or at fetch time, the error is DB_E_ERRORSOCCURRED, binding status set to DBBINDSTATUS_UNSUPPORTEDCONVERSION.  
  
 <sup>6</sup>BY_REF may be used.  
  
 DBTYPE_NULL and DBTYPE_EMPTY can be bound for input parameters but not for output parameters or results. When bound for input parameters, the status must be set to DBSTATUS_S_ISNULL or DBSTATUS_S_DEFAULT.  
  
 DBTYPE_UDT can also be converted to DBTYPE_EMPTY and DBTYPE_NULL, but DBTYPE_NULL and DBTYPE_EMPTY cannot be converted to DBTYPE_UDT. This is consistent with DBTYPE_BYTES.  
  
> [!NOTE]  
>  A new interface is used for dealing with UDTs as parameters, **ISSCommandWithParameters**, which inherits from **ICommandWithParameters**. Applications must use this interface to set at least the SSPROP_PARAM_UDT_NAME of the DBPROPSET_SQLSERVERPARAMETER property set for UDT parameters. If this is not done, **ICommand::Execute** will return DB_E_ERRORSOCCURRED. This interface and property set is described later in this article.  
  
 If a user-defined type is inserted into a column that is not large enough to hold all its data, **ICommand::Execute** will return S_OK with a status of DB_E_ERRORSOCCURRED.  
  
 Data conversions supplied by OLE DB core services (**IDataConvert**) are not applicable to DBTYPE_UDT. No other bindings are supported.  
  
### OLE DB Rowset Additions and Changes  
 OLE DB Driver for SQL Server adds new values or changes to many of the core OLE DB schema rowsets.  
  
#### The PROCEDURE_PARAMETERS Schema Rowset  
 The following additions have been made to the PROCEDURE_PARAMETERS schema rowset.  
  
|Column name|Type|Description|  
|-----------------|----------|-----------------|  
|SS_UDT_CATALOGNAME|DBTYPE_WSTR|The three-part name identifier.|  
|SS_UDT_SCHEMANAME|DBTYPE_WSTR|The three-part name identifier.|  
|SS_UDT_NAME|DBTYPE_WSTR|The three-part name identifier.|  
|SS_UDT_ASSEMBLY_TYPENAME|DBTYPE_WSTR|The Assembly Qualified Name, which includes the type name and all the assembly identification necessary to be referenced by the CLR.|  
  
#### The SQL_ASSEMBLIES Schema Rowset  
 The OLE DB Driver for SQL Server exposes a new provider-specific schema rowset that describes the registered UDTs. The ASSEMBLY server may be specified as a DBTYPE_WSTR, but is not present in the rowset. If not specified, the rowset will default to the current server. The SQL_ASSEMBLIES schema rowset is defined in the following table:  
  
|Column name|Type|Description|  
|-----------------|----------|-----------------|  
|ASSEMBLY_CATALOG|DBTYPE_WSTR|The catalog name of the assembly that contains the type.|  
|ASSEMBLY_SCHEMA|DBTYPE_WSTR|The schema name, or owner name, of the assembly that contains the type. Although assemblies are scoped by database and not by schema, they still have an owner which is reflected here.|  
|ASSEMBLY_NAME|DBTYPE_WSTR|The name of the assembly that contains the type.|  
|ASSEMBLY_ID|DBTYPE_UI4|The object ID of the assembly that contains the type.|  
|PERMISSION_SET|DBTYPE_WSTR|A value that indicates the scope of access for the assembly. Values include "SAFE", "EXTERNAL_ACCESS", and "UNSAFE".|  
|ASSEMBLY_BINARY|DBTYPE_BYTES|The binary representation of the assembly.|  
  
#### The SQL_ASSEMBLIES_ DEPENDENCIES Schema Rowset  
 OLE DB Driver for SQL Server exposes a new provider-specific schema rowset that describes the assembly dependencies for a specified server. ASSEMBLY_SERVER may be specified by the caller as a DBTYPE_WSTR, but is not present in the rowset. If not specified, the rowset will default to the current server. The SQL_ASSEMBLY_DEPENDENCIES schema rowset is defined in the following table:  
  
|Column name|Type|Description|  
|-----------------|----------|-----------------|  
|ASSEMBLY_CATALOG|DBTYPE_WSTR|The catalog name of the assembly that contains the type.|  
|ASSEMBLY_SCHEMA|DBTYPE_WSTR|The schema name, or owner name, of the assembly that contains the type. Although assemblies are scoped by database and not by schema, they still have an owner, which is reflected here.|  
|ASSEMBLY_ID|DBTYPE_UI4|The object ID of the assembly.|  
|REFERENCED_ASSEMBLY_ID|DBTYPE_UI4|The object ID of the referenced assembly.|  
  
#### The SQL_USER_TYPES Schema Rowset  
 OLE DB Driver for SQL Server exposes new schema rowset, SQL_USER_TYPES, that describes when the registered UDTs for a specified server are added. UDT_SERVER must be specified as a DBTYPE_WSTR by the caller but is not present in the rowset. The SQL_USER_TYPES schema rowset is defined in the following table.  
  
|Column name|Type|Description|  
|-----------------|----------|-----------------|  
|UDT_CATALOGNAME|DBTYPE_WSTR|For UDT columns, this property is a string specifying the name of the catalog where the UDT is defined.|  
|UDT_SCHEMANAME|DBTYPE_WSTR|For UDT columns, this property is a string specifying the name of the schema where the UDT is defined.|  
|UDT_NAME|DBTYPE_WSTR|The name of the assembly containing the UDT class.|  
|UDT_ASSEMBLY_TYPENAME|DBTYPE_WSTR|Full type name (AQN) includes type name prefixed by namespace if applicable.|  
  
#### The COLUMNS Schema Rowset  
 Additions to the COLUMNS schema rowset include the following columns:  
  
|Column name|Type|Description|  
|-----------------|----------|-----------------|  
|SS_UDT_CATALOGNAME|DBTYPE_WSTR|For UDT columns, this property is a string specifying the name of the catalog where the UDT is defined.|  
|SS_UDT_SCHEMANAME|DBTYPE_WSTR|For UDT columns, this property is a string specifying the name of the schema where the UDT is defined.|  
|SS_UDT_NAME|DBTYPE_WSTR|The name of the UDT|  
|SS_UDT_ASSEMBLY_TYPENAME|DBTYPE_WSTR|Full type name (AQN) includes type name prefixed by namespace if applicable.|  
  
### OLE DB Property Set Additions and Changes  
 OLE DB Driver for SQL Server adds new values or changes to many of the core OLE DB property sets.  
  
#### The DBPROPSET_SQLSERVERPARAMETER Property Set  
 In order to support UDTs through OLE DB, OLE DB Driver for SQL Server implements the new DBPROPSET_SQLSERVERPARAMETER property set, which contains the following values:  
  
|Name|Type|Description|  
|----------|----------|-----------------|  
|SSPROP_PARAM_UDT_CATALOGNAME|DBTYPE_WSTR|The three-part name identifier.<br /><br /> For UDT parameters, this property is a string that specifies the name of the catalog where the user-defined type is defined.|  
|SSPROP_PARAM_UDT_SCHEMANAME|DBTYPE_WSTR|The three-part name identifier.<br /><br /> For UDT parameters, this property is a string that specifies the name of the schema where the user-defined type is defined.|  
|SSPROP_PARAM_UDT_NAME|DBTYPE_WSTR|The three-part name identifier.<br /><br /> For UDT columns, this property is a string that specifies the single part name of the user-defined type.|  
  
 SSPROP_PARAM_UDT_NAME is mandatory. SSPROP_PARAM_UDT_CATALOGNAME and SSPROP_PARAM_UDT_SCHEMANAME are optional. If any of the properties are specified incorrectly, DB_E_ERRORSINCOMMAND will be returned. If both SSPROP_PARAM_UDT_CATALOGNAME and SSPROP_PARAM_UDT_SCHEMANAME are not specified, then the UDT must be defined in the same database and schema as the table. If the UDT definition is not in the same schema as the table (but is in the same database), then SSPROP_PARAM_UDT_SCHEMANAME must be specified. If the UDT definition is in a different database, then both SSPROP_PARAM_UDT_CATALOGNAME and SSPROP_PARAM_UDT_SCHEMANAME must be specified.  
  
#### The DBPROPSET_SQLSERVERCOLUMN Property Set  
 To support the creation of tables in the **ITableDefinition** interface, OLE DB Driver for SQL Server adds the following three new columns to the DBPROPSET_SQLSERVERCOLUMN property set.  
  
|Name|Description|Type|Description|  
|----------|-----------------|----------|-----------------|  
|SSPROP_COL_UDT_CATALOGNAME|UDT_CATALOGNAME|VT_BSTR|For columns of type DBTYPE_UDT, this property is a string specifying the name of the catalog where the UDT is defined.|  
|SSPROP_COL_UDT_SCHEMANAME|UDT_SCHEMANAME|VT_BSTR|For columns of type DBTYPE_UDT, this property is a string specifying the name of the schema where the UDT is defined.|  
|SSPROP_COL_UDT_NAME|UDT_NAME|VT_BSTR|For columns of type DBTYPE_UDT, this property is a string specifying the single part name of the UDT. For other column types, this property returns an empty string.|  
  
> [!NOTE]  
>  UDTs do not appear in the PROVIDER_TYPES schema rowset. All columns have read and write access.  
  
 ADO will refer to these properties by using the corresponding entry in the Description column.  
  
 SSPROP_COL_UDTNAME is mandatory. SSPROP_COL_UDT_CATALOGNAME and SSPROP_COL_UDT_SCHEMANAME are optional. If any of the properties are specified incorrectly, **DB_E_ERRORSINCOMMAND** will be returned.  
  
 If neither SSPROP_COL_UDT_CATALOGNAME nor SSPROP_COL_UDT_SCHEMANAME is specified, the UDT must be defined in the same database and schema as the table.  
  
 If the UDT definition is not in the same schema as the table (but is in the same database), SSPROP_COL_UDT_SCHEMANAME must be specified.  
  
 If the UDT definition is in a different database, both SSPROP_COL_UDT_CATALOGNAME and SSPROP_COL_UDT_SCHEMANAME must be specified.  
  
### OLE DB Interface Additions and Changes  
 OLE DB Driver for SQL Server adds new values or changes to many of the core OLE DB interfaces.  
  
#### The ISSCommandWithParameters Interface  
 To support UDTs through OLE DB, OLE DB Driver for SQL Server implements a number of changes, including the addition of the **ISSCommandWithParameters** interface. This new interface inherits from the core OLE DB interface **ICommandWithParameters**. In addition to the three methods inherited from **ICommandWithParameters**; **GetParameterInfo**, **MapParameterNames**, and **SetParameterInfo**; **ISSCommandWithParameters** provides the **GetParameterProperties** and **SetParameterProperties** methods that are used to handle-server specific data types.  
  
> [!NOTE]  
>  The **ISSCommandWithParameters** interface also makes use of the new SSPARAMPROPS structure.  
  
#### The IColumnsRowset Interface  
 In addition to the **ISSCommandWithParameters** interface, OLE DB Driver for SQL Server also adds new values to the rowset returned from calling the **IColumnsRowset::GetColumnRowset** method including the following.  
  
|Column Name|Type|Description|  
|-----------------|----------|-----------------|  
|DBCOLUMN_SS_UDT_CATALOGNAME|DBTYPE_WSTR|A UDT catalog name identifier.|  
|DBCOLUMN_SS_UDT_SCHEMANAME|DBTYPE_WSTR|A UDT schema name identifier.|  
|DBCOLUMN_SS_UDT_NAME|DBTYPE_WSTR|A UDT name identifier.|  
|DBCOLUMN_SS_ASSEMBLY_TYPENAME|DBTYPE_WSTR|The assembly qualified name, which includes the type name and all the assembly identification necessary to be referenced by the CLR.|  
  
 You can differentiate a server UDT column from other binary types when the DBCOLUMN_TYPE is set to DBTYPE_UDT by looking at the added UDT metadata specified in the preceding table. If that data is partially complete, the server type is a UDT. For non-UDT server types, these columns are always returned as NULL.  
 
  
## See Also  
 [OLE DB Driver for SQL Server Features](../../oledb/features/oledb-driver-for-sql-server-features.md)    
 [ISSCommandWithParameters &#40;OLE DB&#41;](../../oledb/ole-db-interfaces/isscommandwithparameters-ole-db.md)  
  
  
