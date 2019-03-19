---
title: "SQLColumns | Microsoft Docs"
ms.custom: ""
ms.date: "03/17/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
apitype: "DLLExport"
helpviewer_keywords: 
  - "SQLColumns function"
ms.assetid: 69d3af44-8196-43ab-8037-cdd06207b171
author: MightyPen
ms.author: genemi
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# SQLColumns
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
[!INCLUDE[SNAC_Deprecated](../../includes/snac-deprecated.md)]

  **SQLColumns** returns SQL_SUCCESS whether or not values exist for the *CatalogName*, *TableName*, or *ColumnName* parameters. **SQLFetch** returns SQL_NO_DATA when invalid values are used in these parameters.  
  
> [!NOTE]  
>  For large value types, all length parameters will be returned with a value of SQL_SS_LENGTH_UNLIMITED.  
  
 **SQLColumns** can be executed on a static server cursor. An attempt to execute **SQLColumns** on an updatable (dynamic or keyset) cursor will return SQL_SUCCESS_WITH_INFO indicating that the cursor type has been changed.  
  
 The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver supports reporting information for tables on linked servers by accepting a two-part name for the *CatalogName* parameter: *Linked_Server_Name.Catalog_Name*.  
  
 For ODBC 2.*x* applications not using wildcards in *TableName*, **SQLColumns** returns information about any tables whose names match *TableName* and are owned by the current user. If the current user owns no table whose name matches the *TableName* parameter, **SQLColumns** returns information about any tables owned by other users where the table name matches the *TableName* parameter. For ODBC 2.*x* applications using wildcards, **SQLColumns** returns all tables whose names match *TableName*. For ODBC 3.*x* applications **SQLColumns** returns all tables whose names match *TableName* regardless of owner or whether wildcards are used.  
  
 The following table lists the columns returned by the result set:  
  
|Column name|Description|  
|-----------------|-----------------|  
|DATA_TYPE|Returns SQL_VARCHAR, SQL_VARBINARY, or SQL_WVARCHAR for the **varchar(max)** data types.|  
|TYPE_NAME|Returns "varchar", "varbinary", or "nvarchar" for the **varchar(max)**, **varbinary(max)**, and **nvarchar(max)** data types.|  
|COLUMN_SIZE|Returns SQL_SS_LENGTH_UNLIMITED for **varchar(max)** data types indicating that the size of the column is unlimited.|  
|BUFFER_LENGTH|Returns SQL_SS_LENGTH_UNLIMITED for **varchar(max)** data types indicating that the size of the buffer is unlimited.|  
|SQL_DATA_TYPE|Returns SQL_VARCHAR, SQL_VARBINARY, or SQL_WVARCHAR for the **varchar(max)** data types.|  
|CHAR_OCTET_LENGTH|Returns the maximum length of a char or binary column. Returns 0 to indicate that the size is unlimited.|  
|SS_XML_SCHEMACOLLECTION_CATALOG_NAME|Returns the name of the catalog where an XML schema collection name is defined. If the catalog name cannot be found, then this variable contains an empty string.|  
|SS_XML_SCHEMACOLLECTION_SCHEMA_NAME|Returns the name of the schema where an XML schema collection name is defined. If the schema name cannot be found, then this variable contains an empty string.|  
|SS_XML_SCHEMACOLLECTION_NAME|Returns the name of an XML schema collection. If the name cannot be found, then this variable contains an empty string.|  
|SS_UDT_CATALOG_NAME|The name of the catalog containing the UDT (user-defined type).|  
|SS_UDT_SCHEMA_NAME|The name of the schema containing the UDT.|  
|SS_UDT_ASSEMBLY_TYPE_NAME|The assembly-qualified name of the UDT.|  
  
 For UDTs, the existing TYPE_NAME column is used to indicate the name of the UDT; therefore no additional column for it should be added to the result set of **SQLColumns** or [SQLProcedureColumns](../../relational-databases/native-client-odbc-api/sqlprocedurecolumns.md). The DATA_TYPE for a UDT column or parameter is SQL_SS_UDT.  
  
 For the UDT of parameters, you can use the new driver-specific descriptors defined above to get or set the extra metadata properties of a UDT, should the server return or require this information.  
  
 When a client connects to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and calls SQLColumns, using NULL or wild card values for the catalog input parameter will not return information from other catalogs. Instead, only information about the current catalog will be returned. The client can first call SQLTables to determine in which catalog the desired table is located. The client can then use that catalog value for the catalog input parameter in its call to SQLColumns to retrieve information about the columns in that table.  
  
## SQLColumns and Table-Valued Parameters  
 The result set returned by SQLColumns depends on the setting of SQL_SOPT_SS_NAME_SCOPE. For more information, see [SQLSetStmtAttr](../../relational-databases/native-client-odbc-api/sqlsetstmtattr.md). The following columns have been added for table-valued parameters:  
  
|Column name|Data type|Contents|  
|-----------------|---------------|--------------|  
|SS_IS_COMPUTED|Smallint|For a column in a TABLE_TYPE, this is SQL_TRUE if the column is a computed column; otherwise, SQL_FALSE.|  
|SS_IS_IDENTITY|Smallint|SQL_TRUE if the column is an identity column; otherwise, SQL_FALSE.|  
  
 For more information about table-valued parameters, see [Table-Valued Parameters &#40;ODBC&#41;](../../relational-databases/native-client-odbc-table-valued-parameters/table-valued-parameters-odbc.md).  
  
## SQLColumns Support for Enhanced Date and Time Features  
 For information about the values returned for date/time types, see [Catalog Metadata](../../relational-databases/native-client-odbc-date-time/metadata-catalog.md).  
  
 For more information, see [Date and Time Improvements &#40;ODBC&#41;](../../relational-databases/native-client-odbc-date-time/date-and-time-improvements-odbc.md).  
  
## SQLColumns Support for Large CLR UDTs  
 **SQLColumns** supports large CLR user-defined types (UDTs). For more information, see [Large CLR User-Defined Types &#40;ODBC&#41;](../../relational-databases/native-client/odbc/large-clr-user-defined-types-odbc.md).  
  
## SQLColumns Support for Sparse Columns  
 Two [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] specific columns have been added to the result set for SQLColumns:  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|SS_IS_SPARSE|**Smallint**|If the column is a sparse column, this is SQL_TRUE; otherwise, SQL_FALSE.|  
|SS_IS_COLUMN_SET|**Smallint**|If the column is the **column_set** column, this is SQL_TRUE; otherwise, SQL_FALSE.|  
  
 In conformance with the ODBC specification, SS_IS_SPARSE and SS_IS_COLUMN_SET appear before all driver-specific columns that were added to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] versions earlier than [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)], and after all columns mandated by ODBC itself.  
  
 The result set returned by SQLColumns depends on the setting of SQL_SOPT_SS_NAME_SCOPE. For more information, see [SQLSetStmtAttr](../../relational-databases/native-client-odbc-api/sqlsetstmtattr.md).  
  
 For more information about sparse columns in ODBC, see [Sparse Columns Support &#40;ODBC&#41;](../../relational-databases/native-client/odbc/sparse-columns-support-odbc.md).  
  
## See Also  
 [SQLColumns Function](https://go.microsoft.com/fwlink/?LinkId=59336)   
 [ODBC API Implementation Details](../../relational-databases/native-client-odbc-api/odbc-api-implementation-details.md)  
  
  
