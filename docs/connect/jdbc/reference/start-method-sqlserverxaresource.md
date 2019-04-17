---
title: "start Method (SQLServerXAResource) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerXAResource.start"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 33c90213-92f7-416b-b2fa-67a1afe64e97
author: MightyPen
ms.author: genemi
manager: craigg
---
# start Method (SQLServerXAResource)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Starts work on behalf of a transaction branch that is specified in the Xid object.  
  
## Syntax  
  
```  
  
public void start(javax.transaction.xa.Xid xid,  
                  int flags)  
```  
  
#### Parameters  
 *xid*  
  
 An Xid object.  
  
 *flags*  
  
 An **int** value.  
  
## Exceptions  
 javax.transaction.xa.XAException  
  
## Remarks  
 This start method is specified by the start method in the javax.transaction.xa.XAResource interface.  
  
## See Also  
 [SQLServerXAResource Methods](../../../connect/jdbc/reference/sqlserverxaresource-methods.md)   
 [SQLServerXAResource Members](../../../connect/jdbc/reference/sqlserverxaresource-members.md)   
 [SQLServerXAResource Class](../../../connect/jdbc/reference/sqlserverxaresource-class.md)  
  
  
