---
title: "setTransactionIsolation Method (SQLServerConnection) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerConnection.setTransactionIsolation"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 6a8fa4d3-5237-40f8-8a02-b40a3d7a1131
author: MightyPen
ms.author: genemi
manager: craigg
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
  
  
