---
title: "getXAResource Method (SQLServerXAConnection) | Microsoft Docs"
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
  - "SQLServerXAConnection.getXAResource"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: e1d2828f-fd20-44b0-b796-dc70f77c5b03
caps.latest.revision: 6
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# getXAResource Method (SQLServerXAConnection)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves a [SQLServerXAResource](../../../connect/jdbc/reference/sqlserverxaresource-class.md) object that the transaction manager will use to manage this [SQLServerXAConnection](../../../connect/jdbc/reference/sqlserverxaconnection-class.md) object's participation in a distributed transaction.  
  
## Syntax  
  
```  
  
public javax.transaction.xa.XAResource getXAResource()  
```  
  
## Return Value  
 An XAResource object.  
  
## Exceptions  
 java.sql.SQLException  
  
## Remarks  
 This getXAResource method is specified by the getXAResource method in the javax.sql.XAConnection interface.  
  
## See Also  
 [SQLServerXAConnection Methods](../../../connect/jdbc/reference/sqlserverxaconnection-methods.md)   
 [SQLServerXAConnection Members](../../../connect/jdbc/reference/sqlserverxaconnection-members.md)   
 [SQLServerXAConnection Class](../../../connect/jdbc/reference/sqlserverxaconnection-class.md)  
  
  