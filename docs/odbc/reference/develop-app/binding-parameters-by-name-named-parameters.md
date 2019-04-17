---
title: "Binding Parameters by Name (Named Parameters) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "named parameters [ODBC]"
  - "binding parameters by name [ODBC]"
ms.assetid: e2c3da5a-6c10-4dd5-acf9-e951eea71a6b
author: MightyPen
ms.author: genemi
manager: craigg
---
# Binding Parameters by Name (Named Parameters)
Certain DBMSs allow an application to specify the parameters to a stored procedure by name instead of by position in the procedure call. Such parameters are called *named parameters*. ODBC supports the use of named parameters. In ODBC, named parameters are used only in calls to stored procedures and cannot be used in other SQL statements.  
  
 The driver checks the value of the SQL_DESC_UNNAMED field of the IPD to determine whether named parameters are used. If SQL_DESC_UNNAMED is not set to SQL_UNNAMED, the driver uses the name in the SQL_DESC_NAME field of the IPD to identify the parameter. To bind the parameter, an application can call **SQLBindParameter** to specify the parameter information and then can call **SQLSetDescField** to set the SQL_DESC_NAME field of the IPD. When named parameters are used, the order of the parameter in the procedure call is not important and the parameter's record number is ignored.  
  
 The difference between unnamed parameters and named parameters is in the relationship between the record number of the descriptor and the parameter number in the procedure. When unnamed parameters are used, the first parameter marker is related to the first record in the parameter descriptor, which in turn is related to the first parameter (in creation order) in the procedure call. When named parameters are used, the first parameter marker is still related to the first record of the parameter descriptor, but the relationship between the record number of the descriptor and the parameter number in the procedure does not exist anymore. Named parameters do not use the mapping of the descriptor record number to the procedure parameter position; instead, the descriptor record name is mapped to the procedure parameter name.  
  
> [!NOTE]  
>  If automatic population of the IPD is enabled, the driver will populate the descriptor such that the order of the descriptor records will match the order of the parameters in the procedure definition, even if named parameters are used.  
  
 If a named parameter is used, all parameters must be named parameters. If any parameter is not a named parameter, then none of the parameters ca be named parameters. If there were a mixture of named parameters and unnamed parameters, the behavior would be driver-dependent.  
  
 As an example of named parameters, suppose a SQL Server stored procedure has been defined as follows:  
  
```  
CREATE PROCEDURE test @title_id int = 1, @quote char(30) AS <blah>  
```  
  
 In this procedure, the first parameter, @title_id, has a default value of 1. An application can use the following code to invoke this procedure such that it specifies only one dynamic parameter. This parameter is a named parameter with the name "\@quote".  
  
```  
// Prepare the procedure invocation statement.  
SQLPrepare(hstmt, "{call test(?)}", SQL_NTS);  
  
// Populate record 1 of ipd.  
SQLBindParameter(hstmt, 1, SQL_PARAM_INPUT, SQL_C_CHAR, SQL_CHAR,  
                  30, 0, szQuote, 0, &cbValue);  
  
// Get ipd handle and set the SQL_DESC_NAMED and SQL_DESC_UNNAMED fields  
// for record #1.  
SQLGetStmtAttr(hstmt, SQL_ATTR_IMP_PARAM_DESC, &hIpd, 0, 0);  
SQLSetDescField(hIpd, 1, SQL_DESC_NAME, "@quote", SQL_NTS);  
  
// Assuming that szQuote has been appropriately initialized,  
// execute.  
SQLExecute(hstmt);  
```
