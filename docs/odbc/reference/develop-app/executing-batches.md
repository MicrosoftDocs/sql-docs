---
title: "Executing Batches | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "batches [ODBC], executing"
  - "SQL statements [ODBC], batches"
ms.assetid: f082c717-4f82-4820-a2fa-ba607d8fd872
author: MightyPen
ms.author: genemi
manager: craigg
---
# Executing Batches
Before an application executes a batch of statements, it should first check whether they are supported. To do this, the application calls **SQLGetInfo** with the SQL_BATCH_SUPPORT, SQL_PARAM_ARRAY_ROW_COUNTS, and SQL_PARAM_ARRAY_SELECTS options. The first option returns whether row count-generating and result set-generating statements are supported in explicit batches and procedures, while the latter two options return information about the availability of row counts and result sets in parameterized execution.  
  
 Batches of statements are executed through **SQLExecute** or **SQLExecDirect**. For example, the following call executes an explicit batch of statements to open a new sales order.  
  
```  
SQLCHAR *BatchStmt =  
   "INSERT INTO Orders (OrderID, CustID, OpenDate, SalesPerson, Status)"  
      "VALUES (2002, 1001, {fn CURDATE()}, 'Garcia', 'OPEN');"  
   "INSERT INTO Lines (OrderID, Line, PartID, Quantity) VALUES (2002, 1, 1234, 10);"  
   "INSERT INTO Lines (OrderID, Line, PartID, Quantity) VALUES (2002, 2, 987, 8);"  
   "INSERT INTO Lines (OrderID, Line, PartID, Quantity) VALUES (2002, 3, 566, 17);"  
   "INSERT INTO Lines (OrderID, Line, PartID, Quantity) VALUES (2002, 4, 412, 500)";  
  
SQLExecDirect(hstmt, BatchStmt, SQL_NTS);  
```  
  
 When a batch of result-generating statements is executed, it returns one or more row counts or result sets. For information about how to retrieve these, see [Multiple Results](../../../odbc/reference/develop-app/multiple-results.md).  
  
 If a batch of statements includes parameter markers, these are numbered in increasing parameter order as they are in any other statement. For example, the following batch of statements has parameters numbered from 1 through 21; those in the first **INSERT** statement are numbered 1 through 5 and those in the last **INSERT** statement are numbered 18 through 21.  
  
```  
INSERT INTO Orders (OrderID, CustID, OpenDate, SalesPerson, Status)  
   VALUES (?, ?, ?, ?, ?);  
INSERT INTO Lines (OrderID, Line, PartID, Quantity) VALUES (?, ?, ?, ?);  
INSERT INTO Lines (OrderID, Line, PartID, Quantity) VALUES (?, ?, ?, ?);  
INSERT INTO Lines (OrderID, Line, PartID, Quantity) VALUES (?, ?, ?, ?);  
INSERT INTO Lines (OrderID, Line, PartID, Quantity) VALUES (?, ?, ?, ?);  
```  
  
 For more information about parameters, see [Statement Parameters](../../../odbc/reference/develop-app/statement-parameters.md), later in this section.
