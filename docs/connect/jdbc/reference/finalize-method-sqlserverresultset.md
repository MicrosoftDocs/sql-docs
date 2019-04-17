---
title: "finalize Method (SQLServerResultSet) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerResultSet.finalize"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 49bc879d-822b-42da-bc20-2394865f1f0f
author: MightyPen
ms.author: genemi
manager: craigg
---
# finalize Method (SQLServerResultSet)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Explicitly closes this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object.  
  
## Syntax  
  
```  
  
public void finalize()  
```  
  
## Remarks  
 Closes the result set if the application does not. This method exists only to conform to the JDBC specification. Because the Java Virtual Machine (JVM) does not guarantee when a finalizer will have a chance to run, applications that neglect to explicitly close their result sets could still deadlock on another statement that is using the same connection and is blocked on a common server resource, such as row locks.  
  
## See Also  
 [SQLServerResultSet Members](../../../connect/jdbc/reference/sqlserverresultset-members.md)   
 [SQLServerResultSet Class](../../../connect/jdbc/reference/sqlserverresultset-class.md)  
  
  
