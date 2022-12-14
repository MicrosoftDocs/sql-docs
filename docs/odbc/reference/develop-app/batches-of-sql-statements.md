---
description: "Batches of SQL Statements"
title: "Batches of SQL Statements | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "batches [ODBC]"
  - "SQL statements [ODBC], batches"
  - "batches [ODBC], about batches"
ms.assetid: 766488cc-450c-434c-9c88-467f6c57e17c
author: David-Engel
ms.author: v-davidengel
---
# Batches of SQL Statements
A batch of SQL statements is a group of two or more SQL statements or a single SQL statement that has the same effect as a group of two or more SQL statements. In some implementations, the entire batch statement is executed before any results are available. This is often more efficient than submitting statements separately, because network traffic can often be reduced and the data source can sometimes optimize execution of a batch of SQL statements. In other implementations, calling **SQLMoreResults** triggers the execution of the next statement in the batch. ODBC supports the following types of batches:  
  
-   **Explicit Batches** An *explicit batch* is two or more SQL statements separated by semicolons (;). For example, the following batch of SQL statements opens a new sales order. This requires inserting rows into both the Orders and Lines tables. Note that there is no semicolon after the last statement.  
  
    ```  
    INSERT INTO Orders (OrderID, CustID, OpenDate, SalesPerson, Status)  
       VALUES (2002, 1001, {fn CURDATE()}, 'Garcia', 'OPEN');  
    INSERT INTO Lines (OrderID, Line, PartID, Quantity)  
       VALUES (2002, 1, 1234, 10);  
    INSERT INTO Lines (OrderID, Line, PartID, Quantity)  
       VALUES (2002, 2, 987, 8);  
    INSERT INTO Lines (OrderID, Line, PartID, Quantity)  
       VALUES (2002, 3, 566, 17);  
    INSERT INTO Lines (OrderID, Line, PartID, Quantity)  
       VALUES (2002, 4, 412, 500)  
    ```  
  
-   **Procedures** If a procedure contains more than one SQL statement, it is considered to be a batch of SQL statements. For example, the following SQL Server-specific statement creates a procedure that returns a result set containing information about a customer and a result set listing all the open sales orders for that customer:  
  
    ```  
    CREATE PROCEDURE GetCustInfo (@CustomerID INT) AS  
       SELECT * FROM Customers WHERE CustID = @CustomerID  
       SELECT OrderID FROM Orders  
          WHERE CustID = @CustomerID AND Status = 'OPEN'  
    ```  
  
     The **CREATE PROCEDURE** statement itself is not a batch of SQL statements. However, the procedure it creates is a batch of SQL statements. No semicolons separate the two **SELECT** statements because the **CREATE PROCEDURE** statement is specific to SQL Server, and SQL Server does not require semicolons to separate multiple statements in a **CREATE PROCEDURE** statement.  
  
-   **Arrays of Parameters** Arrays of parameters can be used with a parameterized SQL statement as an effective way to perform bulk operations. For example, arrays of parameters can be used with the following **INSERT** statement to insert multiple rows into the Lines table while executing only a single SQL statement:  
  
    ```  
    INSERT INTO Lines (OrderID, Line, PartID, Quantity)  
       VALUES (?, ?, ?, ?)  
    ```  
  
     If a data source does not support arrays of parameters, the driver can emulate them by executing the SQL statement once for each set of parameters. For more information, see [Statement Parameters](../../../odbc/reference/develop-app/statement-parameters.md) and [Arrays of Parameter Values](../../../odbc/reference/develop-app/arrays-of-parameter-values.md), later in this section.  
  
 The different types of batches cannot be mixed in an interoperable manner. That is, how an application determines the result of executing an explicit batch that includes procedure calls, an explicit batch that uses arrays of parameters, and a procedure call that uses arrays of parameters is driver-specific.  
  
 This section contains the following topics.  
  
-   [Result-Generating and Result-Free Statements](../../../odbc/reference/develop-app/result-generating-and-result-free-statements.md)  
  
-   [Executing Batches](../../../odbc/reference/develop-app/executing-batches.md)  
  
-   [Errors and Batches](../../../odbc/reference/develop-app/errors-and-batches.md)
