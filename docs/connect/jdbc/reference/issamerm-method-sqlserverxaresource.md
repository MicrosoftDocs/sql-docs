---
title: "isSameRM Method (SQLServerXAResource) | Microsoft Docs"
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
  - "SQLServerXAResource.isSameRM"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: bfa24c46-b7cf-470a-afa1-52301847a448
caps.latest.revision: 7
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# isSameRM Method (SQLServerXAResource)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Determines if the resource manager instance that is represented by the target object is the same as the resource manager instance that is represented by the given XAResource object.  
  
## Syntax  
  
```  
  
public boolean isSameRM(javax.transaction.xa.XAResource xares)  
```  
  
#### Parameters  
 *xares*  
  
 An XAResource object.  
  
## Return Value  
 **true** if the instances are the same. Otherwise, **false**.  
  
## Exceptions  
 javax.transaction.xa.XAException  
  
## Remarks  
 This commit method is specified by the commit method in the javax.transaction.xa.XAResource interface.  
  
## See Also  
 [SQLServerXAResource Methods](../../../connect/jdbc/reference/sqlserverxaresource-methods.md)   
 [SQLServerXAResource Members](../../../connect/jdbc/reference/sqlserverxaresource-members.md)   
 [SQLServerXAResource Class](../../../connect/jdbc/reference/sqlserverxaresource-class.md)  
  
  