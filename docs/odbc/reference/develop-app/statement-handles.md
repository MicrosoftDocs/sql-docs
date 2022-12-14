---
description: "Statement Handles"
title: "Statement Handles | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "statement handles [ODBC]"
  - "handles [ODBC], statement"
ms.assetid: 65d6d78b-a8c8-489a-9dad-f8d127a44882
author: David-Engel
ms.author: v-davidengel
---
# Statement Handles
A *statement* is most easily thought of as an SQL statement, such as **SELECT \* FROM Employee**. However, a statement is more than just an SQL statement - it consists of all of the information associated with that SQL statement, such as any result sets created by the statement and parameters used in the execution of the statement. A statement does not even need to have an application-defined SQL statement. For example, when a catalog function such as **SQLTables** is executed on a statement, it executes a predefined SQL statement that returns a list of table names.  
  
 Each statement is identified by a statement handle. A statement is associated with a single connection, and there can be multiple statements on that connection. Some drivers limit the number of active statements they support; the SQL_MAX_CONCURRENT_ACTIVITIES option in **SQLGetInfo** specifies how many active statements a driver supports on a single connection. A statement is defined to be *active* if it has results pending, where results are either a result set or the count of rows affected by an **INSERT**, **UPDATE**, or **DELETE** statement, or data is being sent with multiple calls to **SQLPutData**.  
  
 Within a piece of code that implements ODBC (the Driver Manager or a driver), the statement handle identifies a structure that contains statement information, such as:  
  
-   The statement's state  
  
-   The current statement-level diagnostics  
  
-   The addresses of the application variables bound to the statement's parameters and result set columns  
  
-   The current settings of each statement attribute  
  
 Statement handles are used in most ODBC functions. Notably, they are used in the functions to bind parameters and result set columns (**SQLBindParameter** and **SQLBindCol**), prepare and execute statements (**SQLPrepare**, **SQLExecute**, and **SQLExecDirect**), retrieve metadata (**SQLColAttribute** and **SQLDescribeCol**), fetch results (**SQLFetch**), and retrieve diagnostics (**SQLGetDiagField** and **SQLGetDiagRec**). They are also used in catalog functions (**SQLColumns**, **SQLTables**, and so on) and a number of other functions.  
  
 Statement handles are allocated with **SQLAllocHandle** and freed with **SQLFreeHandle**.
