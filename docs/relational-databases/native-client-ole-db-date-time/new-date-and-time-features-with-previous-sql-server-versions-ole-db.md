---
title: "New Date and Time Features with Previous SQL Server Versions (OLE DB) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: 

ms.topic: "reference"
ms.assetid: 96976bac-018c-47cc-b1b2-fa9605eb55e5
author: MightyPen
ms.author: genemi
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# New Date and Time Features with Previous SQL Server Versions (OLE DB)
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
[!INCLUDE[SNAC_Deprecated](../../includes/snac-deprecated.md)]

  This topic describes the expected behavior when a client application that uses enhanced date and time features communicates with a version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] earlier than [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)], and when a client compiled with a version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client earlier than [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] sends commands to a server that supports enhanced date and time features.  
  
## Down-Level Client Behavior  
 Client applications that use a version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client earlier than [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] see the new date/time types as **nvarchar** columns. The column contents are literal representations. For more information, see the "Data Formats: Strings and Literals" section of [Data Type Support for OLE DB Date and Time Improvements](../../relational-databases/native-client-ole-db-date-time/data-type-support-for-ole-db-date-and-time-improvements.md). The column size is the maximum literal length for the precision specified for the column.  
  
 Catalog APIs will return metadata consistent with the down-level data type code returned to the client (for example, **nvarchar**) and the associated down-level representation (for example, the appropriate literal format). However, the data type name returned will be the real [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] type name.  
  
 When a down-level client application runs against a [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] (or later) server on which schema changes to date/time types have been made, the expected behavior is as follows:  
  
|OLE DB client type|SQL Server 2005 type|SQL Server 2008 (or later) type|Result conversion (server to client)|Parameter conversion (client to server)|  
|------------------------|--------------------------|---------------------------------------|--------------------------------------------|-----------------------------------------------|  
|DBTYPE_DBDATE|Datetime|Date|OK|OK|  
|DBTYPE_DBTIMESTAMP|||Time fields set to zero.|IRowsetChange will fail due to string truncation if the time field is non-zero.|  
|DBTYPE_DBTIME||Time(0)|OK|OK|  
|DBTYPE_DBTIMESTAMP|||Date fields set to current date.|IRowsetChange will fail due to string truncation if fractional seconds are non-zero.<br /><br /> Date is ignored.|  
|DBTYPE_DBTIME||Time(7)|Fails - invalid time literal.|OK|  
|DBTYPE_DBTIMESTAMP|||Fails - invalid time literal.|OK|  
|DBTYPE_DBTIMESTAMP||Datetime2(3)|OK|OK|  
|DBTYPE_DBTIMESTAMP||Datetime2(7)|OK|OK|  
|DBTYPE_DBDATE|Smalldatetime|Date|OK|OK|  
|DBTYPE_DBTIMESTAMP|||Time fields set to zero.|IRowsetChange will fail due to string truncation if time field is non-zero.|  
|DBTYPE_DBTIME||Time(0)|OK|OK|  
|DBTYPE_DBTIMESTAMP|||Date fields set to current date.|IRowsetChange will fail due to string truncation if fractional seconds are non-zero.<br /><br /> Date is ignored.|  
|DBTYPE_DBTIMESTAMP||Datetime2(0)|OK|OK|  
  
 OK means that if it worked with [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)], it should continue to work with [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] (or later).  
  
 Only the following common schema changes have been considered:  
  
-   Using a new type where logically an application requires only a date or time value. However, the application was forced to use **datetime** or **smalldatetime** because separate date and time types were not available.  
  
-   Using a new type to gain additional fractional seconds precision or accuracy.  
  
-   Switching to **datetime2** because this is the preferred data type for date and time.  
  
 Applications that use server metadata obtained through ICommandWithParameters::GetParameterInfo or schema rowsets to set parameter type information through ICommandWithParameters::SetParameterInfo will fail during client conversions where the string representation of a source type is larger than the string representation of the destination type. For example, if a client binding uses DBTYPE_DBTIMESTAMP and the server column is date, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client will convert the value to "yyyy-dd-mm hh:mm:ss.fff", but server metadata will be returned as **nvarchar(10)**. The resulting overflow causes DBSTATUS_E_CATCONVERTVALUE. Similar problems arise with data conversions by IRowsetChange, because the rowset metadata is set from the resultset metadata.  
  
### Parameter and Rowset Metadata  
 This section describes metadata for parameters, result columns, and schema rowsets for clients that are compiled with a version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client earlier than [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)].  
  
#### ICommandWithParameters::GetParameterInfo  
 The DBPARAMINFO structure returns the following information through the *prgParamInfo* parameter:  
  
|Parameter type|wType|ulParamSize|bPrecision|bScale|  
|--------------------|-----------|-----------------|----------------|------------|  
|date|DBTYPE_WSTR|10|~0|~0|  
|time|DBTYPE_WSTR|8, 10..16|~0|~0|  
|smalldatetime|DBTYPE_DBTIMESTAMP|16|16|0|  
|datetime|DBTYPE_DBTIMESTAMP|16|23|3|  
|datetime2|DBTYPE_WSTR|19,21..27|~0|~0|  
|datetimeoffset|DBTYPE_WSTR|26,28..34|~0|~0|  
  
 Notice that some of these value ranges are not continuous; for example, 9 is missing in 8,10..16. This is due to the addition of a decimal point when fractional precision is greater than zero.  
  
#### IColumnsRowset::GetColumnsRowset  
 The following columns are returned:  
  
|Column type|DBCOLUMN_TYPE|DBCOLUMN_COLUMNSIZE|DBCOLUMN_PRECISION|DBCOLUMN_SCALE, DBCOLUMN_DATETIMEPRECISION|  
|-----------------|--------------------|--------------------------|-------------------------|--------------------------------------------------|  
|date|DBTYPE_WSTR|10|NULL|NULL|  
|time|DBTYPE_WSTR|8, 10..16|NULL|NULL|  
|smalldatetime|DBTYPE_DBTIMESTAMP|16|16|0|  
|datetime|DBTYPE_DBTIMESTAMP|16|23|3|  
|datetime2|DBTYPE_WSTR|19,21..27|NULL|NULL|  
|datetimeoffset|DBTYPE_WSTR|26,28..34|NULL|NULL|  
  
#### ColumnsInfo::GetColumnInfo  
 The DBCOLUMNINFO structure returns the following information:  
  
|Parameter Type|wType|ulColumnSize|bPrecision|bScale|  
|--------------------|-----------|------------------|----------------|------------|  
|date|DBTYPE_WSTR|10|~0|~0|  
|time(1..7)|DBTYPE_WSTR|8, 10..16|~0|~0|  
|smalldatetime|DBTYPE_DBTIMESTAMP|16|16|0|  
|datetime|DBTYPE_DBTIMESTAMP|16|23|3|  
|datetime2|DBTYPE_WSTR|19,21..27|~0|~0|  
|datetimeoffset|DBTYPE_WSTR|26,28..34|~0|~0|  
  
### Schema Rowsets  
 This section discusses metadata for parameters, result columns, and schema rowsets for new data types. This information is useful is you have a client provider developed using tools earlier than [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client.  
  
#### COLUMNS Rowset  
 The following column values are returned for date/time types:  
  
|Column type|DATA_TYPE|CHARACTER_MAXIMUM_LENGTH|CHARACTER_OCTET_LENGTH|DATETIME_PRECISION|  
|-----------------|----------------|--------------------------------|------------------------------|-------------------------|  
|date|DBTYPE_WSTR|10|20|NULL|  
|time|DBTYPE_WSTR|8, 10..16|16,20..32|NULL|  
|smalldatetime|DBTYPE_DBTIMESTAMP|NULL|NULL|0|  
|datetime|DBTYPE_DBTIMESTAMP|NULL|NULL|3|  
|datetime2|DBTYPE_WSTR|19,21..27|38,42..54|NULL|  
|datetimeoffset|DBTYPE_WSTR|26,28..34|52, 56..68|NULL|  
  
#### PROCEDURE_PARAMETERS Rowset  
 The following column values are returned for date/time types:  
  
|Column type|DATA_TYPE|CHARACTER_MAXIMUM_LENGTH|CHARACTER_OCTET_LENGTH|TYPE_NAME<br /><br /> LOCAL_TYPE_NAME|  
|-----------------|----------------|--------------------------------|------------------------------|--------------------------------------|  
|date|DBTYPE_WSTR|10|20|date|  
|time|DBTYPE_WSTR|8, 10..16|16,20..32|time|  
|smalldatetime|DBTYPE_DBTIMESTAMP|NULL|NULL|smalldatetime|  
|datetime|DBTYPE_DBTIMESTAMP|NULL|NULL|datetime|  
|datetime2|DBTYPE_WSTR|19,21..27|38,42..54|datetime2|  
|datetimeoffset|DBTYPE_WSTR|26,28..34|52, 56..68|datetimeoffset|  
  
#### PROVIDER_TYPES Rowset  
 The following rows are returned for date/time types:  
  
|Type -><br /><br /> Column|date|time|smalldatetime|datetime|datetime2|datetimeoffset|  
|--------------------------|----------|----------|-------------------|--------------|---------------|--------------------|  
|TYPE_NAME|date|time|smalldatetime|datetime|datetime2|datetimeoffset|  
|DATA_TYPE|DBTYPE_WSTR|DBTYPE_WSTR|DBTYPE_DBTIMESTAMP|DBTYPE_DBTIMESTAMP|DBTYPE_WSTR|DBTYPE_WSTR|  
|COLUMN_SIZE|10|16|16|23|27|34|  
|LITERAL_PREFIX|'|'|'|'|'|'|  
|LITERAL_SUFFIX|'|'|'|'|'|'|  
|CREATE_PARAMS|NULL|NULL|NULL|NULL|NULL|NULL|  
|IS_NULLABLE|VARIANT_TRUE|VARIANT_TRUE|VARIANT_TRUE|VARIANT_TRUE|VARIANT_TRUE|VARIANT_TRUE|  
|CASE_SENSITIVE|VARIANT_FALSE|VARIANT_FALSE|VARIANT_FALSE|VARIANT_FALSE|VARIANT_FALSE|VARIANT_FALSE|  
|SEARCHABLE|DB_SEARCHABLE|DB_SEARCHABLE|DB_SEARCHABLE|DB_SEARCHABLE|DB_SEARCHABLE|DB_SEARCHABLE|  
|UNSIGNED_ATTRIBUTE|NULL|NULL|NULL|NULL|NULL|NULL|  
|FIXED_PREC_SCALE|VARIANT_FALSE|VARIANT_FALSE|VARIANT_FALSE|VARIANT_FALSE|VARIANT_FALSE|VARIANT_FALSE|  
|AUTO_UNIQUE_VALUE|VARIANT_FALSE|VARIANT_FALSE|VARIANT_FALSE|VARIANT_FALSE|VARIANT_FALSE|VARIANT_FALSE|  
|LOCAL_TYPE_NAME|date|time|smalldatetime|datetime|datetime2|datetimeoffset|  
|MINIMUM_SCALE|NULL|NULL|NULL|NULL|NULL|NULL|  
|MAXIMUM_SCALE|NULL|NULL|NULL|NULL|NULL|NULL|  
|GUID|NULL|NULL|NULL|NULL|NULL|NULL|  
|TYPELIB|NULL|NULL|NULL|NULL|NULL|NULL|  
|VERSION|NULL|NULL|NULL|NULL|NULL|NULL|  
|IS_LONG|VARIANT_FALSE|VARIANT_FALSE|VARIANT_FALSE|VARIANT_FALSE|VARIANT_FALSE|VARIANT_FALSE|  
|BEST_MATCH|VARIANT_FALSE|VARIANT_FALSE|VARIANT_FALSE|VARIANT_TRUE|VARIANT_FALSE|VARIANT_FALSE|  
|IS_FIXEDLENGTH|VARIANT_FALSE|VARIANT_FALSE|VARIANT_FALSE|VARIANT_FALSE|VARIANT_FALSE|VARIANT_FALSE|  
  
## Down-Level Server Behavior  
 When connected to a server of an earlier version than [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)], any attempt to use the new server type names (for example, with ICommandWithParameters::SetParameterInfo or ITableDefinition::CreateTable) will result in DB_E_BADTYPENAME.  
  
 If new types are bound for parameters or results without the use of a type name, and either the new type is used to specify the server type implicitly or there is no valid conversion from the server type to the client type, DB_E_ERRORSOCCURRED is returned, and DBBINDSTATUS_UNSUPPORTED_CONVERSION is set as the binding status for the accessor used at Execute.  
  
 If there is a supported client conversion from the buffer type to the server type for the server version on the connection, all client buffer types can be used. In this context, *server type* means the type specified by ICommandWithParameters::SetParameterInfo, or implied by the buffer type if ICommandWithParameters::SetParameterInfo has not been called. This means that DBTYPE_DBTIME2 and DBTYPE_DBTIMESTAMPOFFSET can be used with down-level servers, or when DataTypeCompatibility=80, if the client conversion to a supported server type succeeds. Of course, if the server type is incorrect, an error might still be reported by the server if it cannot perform an implicit conversion to the actual server type.  
  
## SSPROP_INIT_DATATYPECOMPATIBILITY Behavior  
 When SSPROP_INIT_DATATYPECOMPATIBILITY is set to SSPROPVAL_DATATYPECOMPATIBILITY_SQL2000, the new date/time types and associated metadata appear to clients as they appear for down-level clients, as described in [Bulk Copy Changes for Enhanced Date and Time Types &#40;OLE DB and ODBC&#41;](../../relational-databases/native-client-odbc-date-time/bulk-copy-changes-for-enhanced-date-and-time-types-ole-db-and-odbc.md).  
  
## Comparability for IRowsetFind  
 All comparison operators are allowed for the new date/time types, because they appear as string types rather than date/time types.  
  
## See Also  
 [Date and Time Improvements &#40;OLE DB&#41;](../../relational-databases/native-client-ole-db-date-time/date-and-time-improvements-ole-db.md)  
  
  
