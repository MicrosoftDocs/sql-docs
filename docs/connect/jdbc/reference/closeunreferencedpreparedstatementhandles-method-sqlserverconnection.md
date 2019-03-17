---
title: "closeUnreferencedPreparedStatementHandles Method (SQLServerConnection) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2018"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerConnection.closeUnreferencedPreparedStatementHandles"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid:
author: MightyPen
ms.author: genemi
manager: craigg
---
# closeUnreferencedPreparedStatementHandles Method (SQLServerConnection)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

 Forces the un-prepare requests for any outstanding discarded prepared statements to be executed.

## Syntax  
  
```  
  
public void closeUnreferencedPreparedStatementHandles()  
```  


## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  

## Remarks  
 This method is available from JDBC driver version 6.4 and onward.
 
## See Also  
 [SQLServerConnection Members](../../../connect/jdbc/reference/sqlserverconnection-members.md)   
 [SQLServerConnection Class](../../../connect/jdbc/reference/sqlserverconnection-class.md)  
  
  
