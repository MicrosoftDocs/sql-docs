---
title: "getTransactionIsolation Method (SQLServerConnection)"
description: "getTransactionIsolation Method (SQLServerConnection)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerConnection.getTransactionIsolation"
apitype: "Assembly"
---
# getTransactionIsolation Method (SQLServerConnection)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the current transaction isolation level of this [SQLServerConnection](../../../connect/jdbc/reference/sqlserverconnection-class.md) object.  
  
## Syntax  
  
```  
  
public int getTransactionIsolation()  
```  
  
## Return Value  
 An **int** value that contains one of the following isolation levels:  
  
 TRANSACTION_NONE  
  
 TRANSACTION_READ_UNCOMMITTED  
  
 TRANSACTION_READ_COMMITTED  
  
 TRANSACTION_REPEATABLE_READ  
  
 TRANSACTION_SERIALIZABLE  
  
 TRANSACTION_SNAPSHOT = 0x1000  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getTransactionIsolation method is specified by the getTransactionIsolation method in the java.sql.Connection interface.  
  
## Related content

- [SQLServerConnection Members](sqlserverconnection-members.md)
- [SQLServerConnection Class](sqlserverconnection-class.md)
