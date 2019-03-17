---
title: "rollback Method (SQLServerXAResource) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerXAResource.rollback"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 93d9d7e6-54b6-4d86-8f8c-386c6057e85e
author: MightyPen
ms.author: genemi
manager: craigg
---
# rollback Method (SQLServerXAResource)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Requests that the resource manager roll back work done on behalf of a transaction branch.  
  
## Syntax  
  
```  
  
public void rollback(javax.transaction.xa.Xid xid)  
```  
  
#### Parameters  
 *xid*  
  
 An Xid object.  
  
## Exceptions  
 javax.transaction.xa.XAException  
  
## Remarks  
 This rollback method is specified by the rollback method in the javax.transaction.xa.XAResource interface.  
  
## See Also  
 [SQLServerXAResource Methods](../../../connect/jdbc/reference/sqlserverxaresource-methods.md)   
 [SQLServerXAResource Members](../../../connect/jdbc/reference/sqlserverxaresource-members.md)   
 [SQLServerXAResource Class](../../../connect/jdbc/reference/sqlserverxaresource-class.md)  
  
  
