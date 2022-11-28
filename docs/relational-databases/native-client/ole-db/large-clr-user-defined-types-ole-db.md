---
description: "Large CLR User-Defined Types in SQL Server Native Client (OLE DB)"
title: "Large CLR User-Defined Types (OLE DB) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "large CLR user-defined types [OLE DB]"
ms.assetid: 4bf12058-0534-42ca-a5ba-b1c23b24d90f
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Large CLR User-Defined Types in SQL Server Native Client (OLE DB)
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

> [!IMPORTANT] 
> [!INCLUDE[snac-removed-oledb-only](../../../includes/snac-removed-oledb-only.md)]

  This topic discusses the changes to OLE DB in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client to support large common language runtime (CLR) user-defined types (UDTs).  
  
 For more information about support for large CLR UDTs in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client, see [Large CLR User-Defined Types](../../../relational-databases/native-client/features/large-clr-user-defined-types.md). For a sample, see [Use Large CLR UDTs &#40;OLE DB&#41;](../../../relational-databases/native-client-ole-db-how-to/use-large-clr-udts-ole-db.md).  
  
## Data Format  
 [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client uses ~0 to represent the length of values that are of unlimited size for large object (LOB) types. ~0 also represents the size of CLR UDTs that are larger than 8,000 bytes.  
  
 The following table shows data type mapping in parameters and rowsets:  
  
|SQL Server data type|OLE DB data type|Memory layout|Value|  
|--------------------------|----------------------|-------------------|-----------|  
|CLR UDT|DBTYPE_UDT|BYTE[](byte array\)|132 (oledb.h)|  
  
 UDT values are represented as byte arrays. Conversions to and from hex strings are supported. Literal values are represented as hex strings with a prefix of "0x". A hex string is the textual representation of binary data in base 16. A example is a conversion from server type **varbinary(10)** to DBTYPE_STR, which results in hexadecimal representation of 20 characters where every pair of characters represents a single byte.  
  
## Parameter Properties  
 The DBPROPSET_SQLSERVERPARAMETER property set supports UDTs through OLE DB. For more information, see [Using User-Defined Types](~/relational-databases/native-client/features/using-user-defined-types.md).  
  
## Column Properties  
 The DBPROPSET_SQLSERVERCOLUMN property set supports the creation of tables through OLE DB. For more information, see [Using User-Defined Types](~/relational-databases/native-client/features/using-user-defined-types.md).  
  
## Data Type Mapping in ITableDefinition::CreateTable  
 The following information is used in **DBCOLUMNDESC** structures used by ITableDefinition::CreateTable when UDT columns are required:  
  
|OLE DB data type (*wType*)|*pwszTypeName*|SQL Server data type|*rgPropertySets*|  
|----------------------------------|--------------------|--------------------------|----------------------|  
|DBTYPE_UDT|Ignored|UDT|Must include a DBPROPSET_SQLSERVERCOLUMN property set.|  
  
## ICommandWithParameters::GetParameterInfo  
 Information returned in the DBPARAMINFO structure through **prgParamInfo** is as follows:  
  
|Parameter type|*wType*|*ulParamSize*|*bPrecision*|*bScale*|*dwFlags* DBPARAMFLAGS_ISLONG|  
|--------------------|-------------|-------------------|------------------|--------------|------------------------------------|  
|DBTYPE_UDT<br /><br /> (length less than or equal to 8,000 bytes)|"DBTYPE_UDT"|*n*|undefined|undefined|clear|  
|DBTYPE_UDT<br /><br /> (length greater than 8,000 bytes)|"DBTYPE_UDT"|~0|undefined|undefined|set|  
  
## ICommandWithParameters::SetParameterInfo  
 The information provided in the DBPARAMBINDINFO structure must conform to the following:  
  
|Parameter type|*pwszDataSourceType*|*ulParamSize*|*bPrecision*|*bScale*|*dwFlags* DBPARAMFLAGS_ISLONG|  
|--------------------|--------------------------|-------------------|------------------|--------------|------------------------------------|  
|DBTYPE_UDT<br /><br /> (length less than or equal to 8,000 bytes)|DBTYPE_UDT|*n*|ignored|ignored|Must be set if the parameter is to be passed using DBTYPE_IUNKNOWN.|  
|DBTYPE_UDT<br /><br /> (length greater than 8,000 bytes)|DBTYPE_UDT|~0|ignored|ignored|ignored|  
  
## ISSCommandWithParameters  
 Applications use **ISSCommandWithParameters** to get and set the parameter properties defined in the Parameter Properties section.  
  
## IColumnsRowset::GetColumnsRowset  
 Columns returned are as follows:  
  
|Column type|DBCOLUMN_TYPE|DBCOLUMN_COLUMNSIZE|DBCOLUMN_PRECISION|DBCOLUMN_SCALE|DBCOLUMN_FLAGS_ISLONG|DBCOLUMNS_ISSEARCHABLE|DBCOLUMN_OCTETLENGTH|  
|-----------------|--------------------|--------------------------|-------------------------|---------------------|-----------------------------|-----------------------------|---------------------------|  
|DBTYPE_UDT<br /><br /> (length less than or equal to 8,000 bytes)|DBTYPE_UDT|*n*|NULL|NULL|Clear|DB_ALL_EXCEPT_LIKE|n|  
|DBTYPE_UDT<br /><br /> (length greater than 8,000 bytes)|DBTYPE_UDT|~0|NULL|NULL|Set|DB_ALL_EXCEPT_LIKE|0|  
  
 The following columns are also defined for UDTs:  
  
|Column Identifier|Type|Description|  
|-----------------------|----------|-----------------|  
|DBCOLUMN_UDT_CATALOGNAME|DBTYPE_WSTR|For UDT columns, the name of the catalog where the UDT is defined.|  
|DBCOLUMN_UDT_SCHEMANAME|DBTYPE_WSTR|For UDT columns, the name of the schema where the UDT is defined.|  
|DBCOLUMN_UDT_NAME|DBTYPE_WSTR|For UDT columns, the single part name of the UDT.|  
|DBCOLUMN_ASSEMBLY_TYPENAME|DBTYPE_WSTR|For UDT columns, the full type name of the UDT. The fully-qualified name of the assembly type lets you instantiate an object of that type using Type.GetType method.|  
  
## IColumnsInfo::GetColumnInfo  
 The information returned in the DBCOLUMNINFO structure is as follows:  
  
|Parameter type|*wType*|*ulColumnSize*|*bPrecision*|*bScale*|*dwFlags*<br /><br /> DBCOLUMNFLAGS_ISLONG|  
|--------------------|-------------|--------------------|------------------|--------------|-----------------------------------------|  
|DBTYPE_UDT<br /><br /> (length less than or equal to 8,000 bytes)|DBTYPE_UDT|*n*|~0|~0|Clear|  
|DBTYPE_UDT<br /><br /> (length greater than 8,000 bytes)|DBTYPE_UDT|~0|~0|~0|Set|  
  
## COLUMNS Rowset (Schema Rowsets)  
 The following column values are returned for UDT types:  
  
|Column type|DATA_TYPE|COLUMN_FLAGS, DBCOLUMFLAGS_ISLONG|CHARACTER_OCTET_LENGTH|  
|-----------------|----------------|-----------------------------------------|------------------------------|  
|DBTYPE_UDT<br /><br /> (length less than or equal to 8,000 bytes)|DBTYPE_UDT|Clear|*n*|  
|DBTYPE_UDT<br /><br /> (length greater than 8,000 bytes)|DBTYPE_UDT|Set|0|  
  
 The following additional columns are defined for UDTs:  
  
|Column identifier|Type|Description|  
|-----------------------|----------|-----------------|  
|SS_UDT_CATALOGNAME|DBTYPE_WSTR|For UDT columns, the name of the catalog where the UDT is defined.|  
|SS_UDT_SCHEMANAME|DBTYPE_WSTR|For UDT columns, the name of the schema where the UDT is defined.|  
|SS_UDT_NAME|DBTYPE_WSTR|For UDT columns, the single part name of the UDT.|  
|SS_ASSEMBLY_TYPENAME|DBTYPE_WSTR|For UDT columns, this is the full type name of the UDT. The fully-qualified name of the assembly type lets you instantiate an object of that type using Type.GetType method.|  
  
 Regarding the PROCEDURE_PARAMETERS rowset, DATA_TYPE contains the same values as the COLUMNS schema rowset and TYPE_NAME contains UDT. The same additional columns are also defined.  
  
 User-defined types will not appear in the PROVIDER_TYPES schema rowset.  
  
## Bindings and Conversions  
  
|Binding data tpe|UDT to server|Non-UDT to server|UDT from server|Non-UDT from server|  
|----------------------|-------------------|------------------------|---------------------|--------------------------|  
|DBTYPE_UDT|Supported (5)|Error (1)|Supported (5)|Error (4)|  
|DBTYPE_BYTES|Supported (5)|N/A|Supported (5)|N/A|  
|DBTYPE_WSTR|Supported (2), (5)|N/A|Supported (3), (5), (6)|N/A|  
|DBTYPE_BSTR|Supported (2), (5)|N/A|Supported (3), (5)|N/A|  
|DBTYPE_STR|Supported (2), (5)|N/A|Supported (3), (5)|N/A|  
|DBTYPE_IUNKNOWN|Supported (6)|N/A|Supported (6)|N/A|  
|DBTYPE_VARIANT (VT_UI1 &#124; VT_ARRAY)|Supported  (5)|N/A|Supported (3), (5)|N/A|  
|DBTYPE_VARIANT (VT_BSTR)|Supported (2), (5)|N/A|N/A|N/A|  
  
### Key to Symbols  
  
|Symbol|Meaning|  
|------------|-------------|  
|1|If a server type other than DBTYPE_UDT is specified with **ICommandWithParameters::SetParameterInfo** and the accessor type is DBTYPE_UDT, an error occurs when the statement is executed.  The error will be DB_E_ERRORSOCCURRED and the parameter status will be DBSTATUS_E_BADACCESSOR.<br /><br /> It is an error to specify a parameter of type UDT for a server parameter that is not a UDT.|  
|2|Data is converted from hex string to binary data.|  
|3|Data is converted from binary data to hex string.|  
|4|Validation can happen when using **CreateAccessor** or **GetNextRows**. The error is DB_E_ERRORSOCCURRED. Binding status is set to DBBINDSTATUS_UNSUPPORTEDCONVERSION.|  
|5|BY_REF may be used.|  
|6|UDT parameters can be bound as DBTYPE_IUNKNOWN in the DBBINDING. Binding to DBTYPE_IUNKNOWN indicates that the application wants to process the data as a stream using the ISequentialStream interface. When a consumer specifies *wType* in a binding as type DBTYPE_IUNKNOWN, and the corresponding column or output parameter of the stored procedure is a UDT, SQL Server Native Client will return ISequentialStream. For an input parameter, SQL Server Native Client will query for the for the ISequentialStream interface.<br /><br /> You can choose to not bind the length of UDT data while using DBTYPE_IUNKNOWN binding, in the case of large UDTs. However, the length must be bound for small UDTs. A DBTYPE_UDT parameter can be specified as a large UDT if one or more of the following is true:<br />*ulParamParamSize* is ~0.<br />DBPARAMFLAGS_ISLONG is set in the DBPARAMBINDINFO struct.<br /><br /> For row data, DBTYPE_IUNKNOWN binding is only allowed for large UDTs. You can find out whether a column is a large UDT type by using the IColumnsInfo::GetColumnInfo method on a Rowset or Command object's IColumnsInfo interface. A DBTYPE_UDT column is a large UDT column if one or more of the following is true:<br />DBCOLUMNFLAGS_ISLONG flag is set on *dwFlags* member of DBCOLUMNINFO structure. <br />*ulColumnSize* member of DBCOLUMNINFO is ~0.|  
  
 DBTYPE_NULL and DBTYPE_EMPTY can be bound for input parameters, but not for output parameters or results. When bound for input parameters, the status must be set to DBSTATUS_S_ISNULL for DBTYPE_NULL or DBSTATUS_S_DEFAULT for DBTYPE_EMPTY. DBTYPE_BYREF cannot be used with DBTYPE_NULL or DBTYPE_EMPTY.  
  
 DBTYPE_UDT can also be converted to DBTYPE_EMPTY and DBTYPE_NULL. However, DBTYPE_NULL and DBTYPE_EMPTY cannot be converted to DBTYPE_UDT. This is consistent with DBTYPE_BYTES. **ISSCommandWithParameters** is used to process UDTs as parameters.  
  
 Data conversions supplied by OLE DB core services (**IDataConvert**) are not applicable to DBTYPE_UDT.  
  
 No other bindings are supported.  
  
## Comparability for IRowsetFind  
 For UDT types, only the following comparisons are supported:  
  
-   EQ  
  
-   NE  
  
-   IGNORE  
  
 If any other comparison is attempted DB_E_BADCOMPAREOP is returned.  
  
## BCP Support for UDTs  
 UDT values can be imported and exported only as character or binary values.  
  
## Down-level Client Behavior for UDTs  
 UDTs are subject to type mapping with down-level clients, as follows:  
  
|Client version|DBTYPE_UDT<br /><br /> (length less than or equal to 8,000 bytes)|DBTYPE_UDT<br /><br /> (length greater than 8,000 bytes)|  
|--------------------|------------------------------------------------------------------|---------------------------------------------------------|  
|SQL Server 2005|UDT|varbinary(max)|  
|SQL Server 2008 and later|UDT|UDT|  
  
 When **DataTypeCompatibility** (SSPROP_INIT_DATATYPECOMPATIBILITY) is set to "80", large UDT types appear to clients in the same way that they appear for down-level clients.  
  
## See Also  
 [Large CLR User-Defined Types](~/relational-databases/native-client/features/large-clr-user-defined-types.md)  
  
  

