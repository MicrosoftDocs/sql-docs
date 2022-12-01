---
title: "setTransactionIsolation Method (SQLServerConnection)"
description: "setTransactionIsolation Method (SQLServerConnection)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerConnection.setTransactionIsolation"
apitype: "Assembly"
---
# setTransactionIsolation Method (SQLServerConnection)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Tries to change the transaction isolation level for this [SQLServerConnection](../../../connect/jdbc/reference/sqlserverconnection-class.md) object to the one given.  
  
## Syntax  
  
```  
  
public void setTransactionIsolation(int level)  
```  
  
#### Parameters  
 *level*  
  
 An **int** value that contains one of the following isolation levels:  
  
 TRANSACTION_READ_UNCOMMITTED  
  
 TRANSACTION_READ_COMMITTED  
  
 TRANSACTION_REPEATABLE_READ  
  
 TRANSACTION_SERIALIZABLE  
  
 TRANSACTION_SNAPSHOT = 0x1000  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This setTransactionIsolation method is specified by the setTransactionIsolation method in the java.sql.Connection interface.  
  
 Transactions are not committed if this method is called in the middle of a transaction.  
  
## See Also  
 [SQLServerConnection Members](../../../connect/jdbc/reference/sqlserverconnection-members.md)   
 [SQLServerConnection Class](../../../connect/jdbc/reference/sqlserverconnection-class.md)  
  
  
