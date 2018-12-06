---
title: "getTransactionIsolation Method (SQLServerConnection) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerConnection.getTransactionIsolation"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 179772e9-6572-4ce5-83c5-ab2b196cee67
author: MightyPen
ms.author: genemi
manager: craigg
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
  
## See Also  
 [SQLServerConnection Members](../../../connect/jdbc/reference/sqlserverconnection-members.md)   
 [SQLServerConnection Class](../../../connect/jdbc/reference/sqlserverconnection-class.md)  
  
  
