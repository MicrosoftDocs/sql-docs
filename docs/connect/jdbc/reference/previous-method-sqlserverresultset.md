---
title: "previous Method (SQLServerResultSet) | Microsoft Docs"
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
  - "SQLServerResultSet.previous"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 66eb4e10-c375-4b31-ac46-3ba1d9dbf6a0
caps.latest.revision: 8
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# previous Method (SQLServerResultSet)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Moves the cursor to the previous row in this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object.  
  
## Syntax  
  
```  
  
public boolean previous()  
```  
  
## Return Value  
 **true** if the new current row is valid. **false** if there are no more rows to process.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This previous method is specified by the previous method in the java.sql.ResultSet interface.  
  
## See Also  
 [SQLServerResultSet Members](../../../connect/jdbc/reference/sqlserverresultset-members.md)   
 [SQLServerResultSet Class](../../../connect/jdbc/reference/sqlserverresultset-class.md)  
  
  