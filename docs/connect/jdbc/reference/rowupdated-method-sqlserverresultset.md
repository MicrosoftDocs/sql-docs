---
title: "rowUpdated Method (SQLServerResultSet)"
description: "rowUpdated Method (SQLServerResultSet)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerResultSet.rowUpdated"
apitype: "Assembly"
---
# rowUpdated Method (SQLServerResultSet)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves whether the current row has been updated.  
  
## Syntax  
  
```  
  
public boolean rowUpdated()  
```  
  
## Return Value  
 **true** if both the row has been visibly updated by the owner or another user, and updates are detected. Otherwise, **false**.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This rowUpdated method is specified by the rowUpdated method in the java.sql.ResultSet interface.  
  
 The value that is returned depends on whether or not the result set can detect updates.  
  
> [!NOTE]  
>  [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] does not detect updated rows for any cursor type.  
  
## See Also  
 [SQLServerResultSet Members](../../../connect/jdbc/reference/sqlserverresultset-members.md)   
 [SQLServerResultSet Class](../../../connect/jdbc/reference/sqlserverresultset-class.md)  
  
  
