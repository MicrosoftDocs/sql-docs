---
title: "getTransactionTimeout Method (SQLServerXAResource) | Microsoft Docs"
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
  - "SQLServerXAResource.getTransactionTimeout"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: ed0a37e9-1132-4d3f-b88f-8be674e852b1
caps.latest.revision: 7
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# getTransactionTimeout Method (SQLServerXAResource)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Obtains the current transaction timeout value set for this [SQLServerXAResource](../../../connect/jdbc/reference/sqlserverxaresource-class.md) object.  
  
## Syntax  
  
```  
  
public int getTransactionTimeout()  
```  
  
## Exceptions  
 javax.transaction.xa.XAException  
  
## Remarks  
 This getTransactionTimeout method is specified by the getTransactionTimeout method in the javax.transaction.xa.XAResource interface.  
  
## See Also  
 [SQLServerXAResource Methods](../../../connect/jdbc/reference/sqlserverxaresource-methods.md)   
 [SQLServerXAResource Members](../../../connect/jdbc/reference/sqlserverxaresource-members.md)   
 [SQLServerXAResource Class](../../../connect/jdbc/reference/sqlserverxaresource-class.md)  
  
  