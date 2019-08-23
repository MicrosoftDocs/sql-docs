---
title: "Table-Valued Parameter Metadata for Prepared Statements | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "table-valued parameters (ODBC), metadata for prepared statements"
ms.assetid: fd2fc705-2e98-4011-9822-c7e6cca4a535
author: MightyPen
ms.author: genemi
manager: craigg
---
# Table-Valued Parameter Metadata for Prepared Statements
  An application can obtain metadata for a prepared procedure call through SQLNumParams and SQLDescribeParam. For table-valued parameters, *DataTypePtr* is set to SQL_SS_TABLE. Additional metadata is available through SQLGetDescField for SQL_CA_SS_TYPE_NAME, SQL_CA_SS_CATALOG_NAME, and SQL_CA_SS_SCHEMA_NAME.  
  
 SQL_CA_SS_TYPE_NAME, SQL_CA_SS_CATALOG_NAME, and SQL_CA_SS_SCHEMA_NAME can be used with SQLColumns to obtain column metadata for table types associated with table-valued parameters. In this case, SQL_SOPT_SS_NAME_SCOPE must be set to SQL_SS_NAME_SCOPE_TABLE_TYPE before SQLColumns is called. SQL_SOPT_SS_NAME_SCOPE should then be set back to the default value, SQL_SS_NAME_SCOPE_TABLE, when the application has finished retrieving table-valued parameter column metadata.  
  
 SQL_CA_SS_TYPE_NAME , SQL_CA_SS_CATALOG_NAME, and SQL_CA_SS_SCHEMA_NAME can also be used with CLR user-defined type parameters.  
  
 You cannot obtain table-valued parameter metadata for prepared statements that are not stored procedure calls. If you try to do this, the application returns SQL_ERROR with SQLSTATE 42000 and the message "Syntax error or access violation".  
  
## See Also  
 [Table-Valued Parameters &#40;ODBC&#41;](table-valued-parameters-odbc.md)  
  
  
