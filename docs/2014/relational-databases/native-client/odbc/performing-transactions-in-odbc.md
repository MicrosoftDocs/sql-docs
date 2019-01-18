---
title: "Transactions in ODBC | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "SQL Server Native Client ODBC driver, transactions"
  - "transactions [ODBC]"
  - "ODBC, transactions"
ms.assetid: c5a87fa5-827a-4e6f-a0d9-924bac881eb0
author: MightyPen
ms.author: genemi
manager: craigg
---
# Transactions in ODBC
  Transactions in ODBC are managed at the connection level. When an application completes a transaction, it commits or rolls back all work completed through all statement handles on that connection. To commit or roll back a transaction, applications should call [SQLEndTran](../../native-client-odbc-api/sqlendtran.md) instead of submitting a COMMIT or ROLLBACK statement.  
  
 An application calls [SQLSetConnectAttr](../../native-client-odbc-api/sqlsetconnectattr.md) to switch between the two ODBC modes of managing transactions:  
  
-   Autocommit mode  
  
     Each statement is automatically committed when it is completed successfully. When you run in autocommit mode, no other transaction management functions are required.  
  
-   Manual-commit mode  
  
     All executed statements are included in the same transaction until it is specifically stopped by calling **SQLEndTran**.  
  
 Autocommit mode is the default transaction mode for ODBC. When a connection is made, it is in autocommit mode until **SQLSetConnectAttr** is called to switch to manual-commit mode by setting autocommit mode off. When an application turns autocommit off, the next statement sent to the database starts a transaction. The transaction then remains in effect until the application calls **SQLEndTran** with either the SQL_COMMIT or SQL_ROLLBACK options. The command sent to the database after **SQLEndTran** starts the next transaction.  
  
 If an application switches from manual-commit to autocommit mode, the driver commits any transactions currently open on the connection.  
  
 ODBC applications should not use Transact-SQL transaction statements such as BEGIN TRANSACTION, COMMIT TRANSACTION, or ROLLBACK TRANSACTION because this can cause indeterminate behavior in the driver. An ODBC application should run in autocommit mode and not use any transaction management functions or statements, or run in manual-commit mode and use the ODBC **SQLEndTran** function to either commit or roll back transactions.  
  
## See Also  
 [Performing Transactions &#40;ODBC&#41;](../../../database-engine/dev-guide/performing-transactions-odbc.md)  
  
  
