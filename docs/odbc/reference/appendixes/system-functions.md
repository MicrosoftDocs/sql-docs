---
description: "System Functions"
title: "System Functions | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords: 
  - "system functions [ODBC]"
  - "functions [ODBC], system functions"
ms.assetid: 36614b4c-e037-43ef-8692-67f4861b144d
author: David-Engel
ms.author: v-davidengel
---
# System Functions
The following table lists system functions that are included in the ODBC scalar function set. By calling **SQLGetInfo** with an *information type* of SQL_SYSTEM_FUNCTIONS, an application can determine which system functions are supported by a driver.  
  
 Arguments denoted as *exp* can be the name of a column, the result of another scalar function, or a literal, where the underlying data type could be represented as SQL_NUMERIC, SQL_DECIMAL, SQL_TINYINT, SQL_SMALLINT, SQL_INTEGER, SQL_BIGINT, SQL_FLOAT, SQL_REAL, SQL_DOUBLE, SQL_TYPE_DATE, SQL_TYPE_TIME, or SQL_TYPE_TIMESTAMP.  
  
 Arguments denoted as *value* can be a literal constant, where the underlying data type can be represented as SQL_NUMERIC, SQL_DECIMAL, SQL_TINYINT, SQL_SMALLINT, SQL_INTEGER, SQL_BIGINT, SQL_FLOAT, SQL_REAL, SQL_DOUBLE, SQL_TYPE_DATE, SQL_TYPE_TIME, or SQL_TYPE_TIMESTAMP.  
  
 Values returned are represented as ODBC data types.  
  
|Function|Description|  
|--------------|-----------------|  
|**DATABASE( )**  (ODBC 1.0)|Returns the name of the database corresponding to the connection handle. (The name of the database is also available by calling **SQLGetConnectOption** with the SQL_CURRENT_QUALIFIER connection option.)|  
|**IFNULL(** _exp_,_value_**)**  (ODBC 1.0)|If *exp* is null, *value* is returned. If *exp* is not null, *exp* is returned. The possible data type or types of *value* must be compatible with the data type of *exp*.|  
|**USER( )**  (ODBC 1.0)|Returns the user name in the DBMS. (The user name is also available by way of **SQLGetInfo** by specifying the information type: SQL_USER_NAME.) This can be different than the login name.|
