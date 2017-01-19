---
title: "isClosed Method (SQLServerResultSet) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "drivers"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 6081aa34-fc88-4dd0-9a3f-05e8488219dc
caps.latest.revision: 11
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# isClosed Method (SQLServerResultSet)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Indicates whether this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object has been closed.  
  
## Syntax  
  
```  
  
public boolean isClosed()  
```  
  
## Return Value  
 **true** if this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object is closed, **false** if it is still open.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This isClosed method is specified by the isClosed method in the java.sql.ResultSet interface.  
  
## See Also  
 [SQLServerResultSet Members](../../../connect/jdbc/reference/sqlserverresultset-members.md)   
 [SQLServerResultSet Class](../../../connect/jdbc/reference/sqlserverresultset-class.md)  
  
  