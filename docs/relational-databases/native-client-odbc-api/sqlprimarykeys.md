---
description: "SQLPrimaryKeys"
title: "SQLPrimaryKeys | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: native-client
ms.topic: "reference"
apitype: "DLLExport"
helpviewer_keywords: 
  - "SQLPrimaryKeys function"
ms.assetid: bc61cd5b-d2f4-4f87-abc7-743cf9ea772d
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# SQLPrimaryKeys
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  A table might have a column or columns that can serve as unique row identifiers, and tables created without a PRIMARY KEY constraint return an empty result set to SQLPrimaryKeys. The ODBC function [SQLSpecialColumns](../../relational-databases/native-client-odbc-api/sqlspecialcolumns.md) reports row identifier candidates for tables without primary keys.  
  
 SQLPrimaryKeys returns SQL_SUCCESS whether or not values exist for *CatalogName*, *SchemaName*, or *TableName* parameters. SQLFetch returns SQL_NO_DATA when invalid values are used in these parameters.  
  
 SQLPrimaryKeys can be executed on a static server cursor. An attempt to execute SQLPrimaryKeys on an updatable (dynamic or keyset) cursor will return SQL_SUCCESS_WITH_INFO indicating that the cursor type has been changed.  
  
 The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver supports reporting information for tables on linked servers by accepting a two-part name for the *CatalogName* parameter: *Linked_Server_Name.Catalog_Name*.  
  
## SQLPrimaryKeys and Table-Valued Parameters  
 If the statement attribute SQL_SOPT_SS_NAME_SCOPE has the value SQL_SS_NAME_SCOPE_TABLE_TYPE, rather than its default value of SQL_SS_NAME_SCOPE_TABLE, SQLPrimaryKeys will return information about primary key columns of table types. For more information on SQL_SOPT_SS_NAME_SCOPE, see [SQLSetStmtAttr](../../relational-databases/native-client-odbc-api/sqlsetstmtattr.md).  
  
 For more information about table-valued parameters, see [Table-Valued Parameters &#40;ODBC&#41;](../../relational-databases/native-client-odbc-table-valued-parameters/table-valued-parameters-odbc.md).  
  
## See Also  
 [SQLPrimaryKeys Function](../../odbc/reference/syntax/sqlprimarykeys-function.md)   
 [ODBC API Implementation Details](../../relational-databases/native-client-odbc-api/odbc-api-implementation-details.md)  
  
