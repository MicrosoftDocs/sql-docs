---
title: "rowInserted Method (SQLServerResultSet)"
description: "rowInserted Method (SQLServerResultSet)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerResultSet.rowInserted"
apitype: "Assembly"
---
# rowInserted Method (SQLServerResultSet)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves whether the current row has had an insertion.  
  
## Syntax  
  
```  
  
public boolean rowInserted()  
```  
  
## Return Value  
 **true** if a row has had an insertion and insertions are detected. Otherwise, **false**.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This rowUpdated method is specified by the rowUpdated method in the java.sql.ResultSet interface.  
  
 The value that is returned depends on whether this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object can detect visible inserts.  
  
> [!NOTE]  
>  [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] does not detect inserted rows for any cursor type.  
  
## See Also  
 [SQLServerResultSet Members](../../../connect/jdbc/reference/sqlserverresultset-members.md)   
 [SQLServerResultSet Class](../../../connect/jdbc/reference/sqlserverresultset-class.md)  
  
  
