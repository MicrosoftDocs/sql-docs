---
title: "prepare Method (SQLServerXAResource) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "drivers"
ms.tgt_pltfrm: ""
ms.topic: "article"
apiname: 
  - "SQLServerXAResource.prepare"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: f800c966-3fae-41b3-963a-464988f80da3
caps.latest.revision: 6
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# prepare Method (SQLServerXAResource)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Requests that the resource manager prepare for a transaction commit of the transaction specified by the given Xid object.  
  
## Syntax  
  
```  
  
public int prepare(javax.transaction.xa.Xid xid)  
```  
  
#### Parameters  
 *xid*  
  
 An Xid object.  
  
## Return Value  
 An **int** value.  
  
## Exceptions  
 javax.transaction.xa.XAException  
  
## Remarks  
 This prepare method is specified by the prepare method in the javax.transaction.xa.XAResource interface.  
  
## See Also  
 [SQLServerXAResource Methods](../../../connect/jdbc/reference/sqlserverxaresource-methods.md)   
 [SQLServerXAResource Members](../../../connect/jdbc/reference/sqlserverxaresource-members.md)   
 [SQLServerXAResource Class](../../../connect/jdbc/reference/sqlserverxaresource-class.md)  
  
  