---
title: "Was a Result Set Created? | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "result sets [ODBC], determining if created"
ms.assetid: 4a83b8cb-2d57-4e64-b497-80bd587ee1f9
author: MightyPen
ms.author: genemi
manager: craigg
---
# Was a Result Set Created?
In most situations, application programmers know whether the statements their application executes will create a result set. This is the case if the application uses hard-coded SQL statements written by the programmer. It is usually the case when the application constructs SQL statements at run time: The programmer can easily include code that flags whether a **SELECT** statement or an **INSERT** statement is being constructed. In a few situations, the programmer cannot possibly know whether a statement will create a result set. This is true if the application provides a way for the user to enter and execute an SQL statement. It is also true when the application constructs a statement at run time to execute a procedure.  
  
 In such cases, the application calls **SQLNumResultCols** to determine the number of columns in the result set. If this is 0, the statement did not create a result set; if it is any other number, the statement did create a result set.  
  
 The application can call **SQLNumResultCols** at any time after the statement is prepared or executed. However, because some data sources cannot easily describe the result sets that will be created by prepared statements, performance will suffer if **SQLNumResultCols** is called after a statement is prepared but before it is executed.  
  
 Some data sources also support determining the number of rows that an SQL statement returns in a result set. To do so, the application calls **SQLRowCount**. Exactly what the row count represents is indicated by the setting of the SQL_DYNAMIC_CURSOR_ATTRIBUTES2, SQL_FORWARD_ONLY_CURSOR_ATTRIBUTES2, SQL_KEYSET_CURSOR_ATTRIBUTES2, or SQL_STATIC_CURSOR_ATTRIBUTES2 option (depending on the type of the cursor) returned by a call to **SQLGetInfo**. This bitmask indicates for each cursor type whether the row count returned is exact, approximate, or is not available at all. Whether row counts for static or keyset-driven cursors are affected by changes made through **SQLBulkOperations** or **SQLSetPos**, or by positioned update or delete statements, depends on other bits returned by the same option arguments listed previously. For more information, see the [SQLGetInfo](../../../odbc/reference/syntax/sqlgetinfo-function.md) function description.
