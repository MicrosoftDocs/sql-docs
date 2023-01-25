---
description: "Additional Table-Valued Parameter Metadata"
title: "Additional Table-Valued Parameter Metadata | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "table-valued parameters (ODBC), catalog functions to retrieve metadata"
  - "table-valued parameters (ODBC), metadata"
ms.assetid: 6c193188-5185-4373-9a0d-76cfc150c828
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Additional Table-Valued Parameter Metadata
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  To retrieve metadata for a table-valued parameter, an application calls SQLProcedureColumns. For a table-valued parameter, SQLProcedureColumns returns a single row. Two additional [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]-specific columns, SS_TYPE_CATALOG_NAME and SS_TYPE_SCHEMA_NAME, have been added to provide schema and catalog information for table types associated with table-valued parameters. In conformance with the ODBC specification, SS_TYPE_CATALOG_NAME and SS_TYPE_SCHEMA_NAME appear before all driver-specific columns added in earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], and after all columns mandated by ODBC itself.  
  
 The following table lists columns that are significant for table-valued parameters.  
  
|Column name|Data type|Value/comments|  
|-----------------|---------------|---------------------|  
|DATA_TYPE|Smallint not NULL|SQL_SS_TABLE|  
|TYPE_NAME|WVarchar(128) not NULL|The type name of the table-valued parameter.|  
|COLUMN_SIZE|Integer|NULL|  
|BUFFER_LENGTH|Integer|0|  
|DECIMAL_DIGITS|Smallint|NULL|  
|NUM_PREC_RADIX|Smallint|NULL|  
|NULLABLE|Smallint not NULL|SQL_NULLABLE|  
|REMARKS|Varchar|NULL|  
|COLUMN_DEF|WVarchar(4000)|NULL|  
|SQL_DATA_TYPE|Smallint not NULL|SQL_SS_TABLE|  
|SQL_DATETIME_SUB|Smallint|NULL|  
|CHAR_OCTET_LENGTH|Integer|NULL|  
|ORDINAL_POSITION|Integer not NULL|The ordinal position of the parameter.|  
|IS_NULLABLE|Varchar|"YES"|  
|SS_TYPE_CATALOG_NAME|WVarchar(128) not NULL|The catalog that contains the type definition for the table type of the table-valued parameter.|  
|SS_TYPE_SCHEMA_NAME|WVarchar(128) not NULL|The schema that contains the type definition for the table type of the table-valued parameter.|  
  
 The WVarchar columns are defined as Varchar in the ODBC specification, but are actually returned as WVarchar in all recent [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ODBC drivers. This change was made when Unicide support was added to the ODBC 3.5 specification, but not called out explicitly.  
  
 To obtain additional metadata for table-valued parameters, an application uses the catalog functions SQLColumns and SQLPrimaryKeys. Before these functions are called for table-valued parameters, the application must set the statement attribute SQL_SOPT_SS_NAME_SCOPE to SQL_SS_NAME_SCOPE_TABLE_TYPE. This value indicates that the application requires metadata for a table type rather than an actual table. The application then passes the TYPE_NAME of the table-valued parameter as the *TableName* parameter. SS_TYPE_CATALOG_NAME and SS_TYPE_SCHEMA_NAME are used with the *CatalogName* and *SchemaName* parameters, respectively, to identify the catalog and schema for the table-valued parameter. When an application has finished retrieving metadata for table-valued parameters, it must set SQL_SOPT_SS_NAME_SCOPE back to its default value of SQL_SS_NAME_SCOPE_TABLE.  
  
 When SQL_SOPT_SS_NAME_SCOPE is set to SQL_SS_NAME_SCOPE_TABLE, queries to linked servers fail. Calls to SQLColumns or SQLPrimaryKeys with a catalog that contains a server component will fail.  
  
## See Also  
 [Table-Valued Parameters &#40;ODBC&#41;](../../relational-databases/native-client-odbc-table-valued-parameters/table-valued-parameters-odbc.md)  
  
  
