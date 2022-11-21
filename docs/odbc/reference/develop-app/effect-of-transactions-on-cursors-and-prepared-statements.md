---
description: "Effect of Transactions on Cursors and Prepared Statements"
title: "Effect of Transactions on Cursors and Prepared Statements | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "rolling back transactions [ODBC]"
  - "committing transactions [ODBC]"
  - "transactions [ODBC], rolling back"
  - "cursors [ODBC], transaction commits or roll backs"
  - "prepared statements [ODBC]"
  - "transactions [ODBC], cursors"
ms.assetid: 523e22a2-7b53-4c25-97c1-ef0284aec76e
author: David-Engel
ms.author: v-davidengel
---
# Effect of Transactions on Cursors and Prepared Statements
Committing or rolling back a transaction has one of the following effects on cursors and access plans:  
  
-   All cursors are closed, and access plans for prepared statements on that connection are deleted, or  
  
-   All cursors are closed, and access plans for prepared statements on that connection remain intact, or 
  
-   All cursors remain open, and access plans for prepared statements on that connection remain intact.  
  
 For example, suppose a data source exhibits the first behavior in this list, the most restrictive of these behaviors. Now suppose an application does the following:  
  
1.  Sets the commit mode to manual-commit.  
  
2.  Creates a result set of sales orders on statement 1.  
  
3.  Creates a result set of the lines in a sales order on statement 2, when the user highlights that order.  
  
4.  Calls **SQLExecute** to execute a positioned update statement that has been prepared on statement 3, when the user updates a line.  
  
5.  Calls **SQLEndTran** to commit the positioned update statement.  
  
 Because of the data source's behavior, the call to **SQLEndTran** in step 5 causes it to close the cursors on statements 1 and 2 and to delete the access plan on all statements. The application must reexecute statements 1 and 2 to re-create the result sets and reprepare the statement on statement 3.  
  
 In auto-commit mode, functions other than **SQLEndTran** commit transactions:  
  
-   **SQLExecute** or **SQLExecDirect** In the preceding example, the call to **SQLExecute** in step 4 commits a transaction. This causes the data source to close the cursors on statements 1 and 2 and delete the access plan on all statements on that connection.  
  
-   **SQLBulkOperations** or **SQLSetPos** In the preceding example, suppose that in step 4 the application calls **SQLSetPos** with the SQL_UPDATE option on statement 2, instead of executing a positioned update statement on statement 3. This commits a transaction and causes the data source to close the cursors on statements 1 and 2, and discards all access plans on that connection.  
  
-   **SQLCloseCursor** In the preceding example, suppose that when the user highlights a different sales order, the application calls **SQLCloseCursor** on statement 2 before creating a result of the lines for the new sales order. The call to **SQLCloseCursor** commits the **SELECT** statement that created the result set of lines and causes the data source to close the cursor on statement 1, and then discards all access plans on that connection.  
  
 Applications, especially screen-based applications in which the user scrolls around the result set and updates or deletes rows, must be careful to code around this behavior.  
  
 To determine how a data source behaves when a transaction is committed or rolled back, an application calls **SQLGetInfo** with the SQL_CURSOR_COMMIT_BEHAVIOR and SQL_CURSOR_ROLLBACK_BEHAVIOR options.
