---
title: "last Method (SQLServerResultSet) | Microsoft Docs"
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
  - "SQLServerResultSet.last"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: ac9bef59-8c31-437b-a183-619cc778fe7a
caps.latest.revision: 8
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# last Method (SQLServerResultSet)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Moves the cursor to the last row in this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object.  
  
## Syntax  
  
```  
  
public boolean last()  
```  
  
## Return Value  
 **true** if the new current row is valid. **false** if there are no more rows to process.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This last method is specified by the last method in the java.sql.ResultSet interface.  
  
## See Also  
 [SQLServerResultSet Members](../../../connect/jdbc/reference/sqlserverresultset-members.md)   
 [SQLServerResultSet Class](../../../connect/jdbc/reference/sqlserverresultset-class.md)  
  
  