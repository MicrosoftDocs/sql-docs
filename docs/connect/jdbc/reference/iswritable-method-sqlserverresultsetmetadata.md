---
title: "isWritable Method (SQLServerResultSetMetaData)"
description: "isWritable Method (SQLServerResultSetMetaData)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerResultSetMetaData.isWritable"
apitype: "Assembly"
---
# isWritable Method (SQLServerResultSetMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Indicates whether it is possible for a write on the designated column to succeed.  
  
## Syntax  
  
```  
  
public boolean isWritable(int column)  
```  
  
#### Parameters  
 *column*  
  
 An **int** that indicates the column index.  
  
## Return Value  
 **true** if writes will succeed on the column. Otherwise, **false**.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This isWritable method is specified by the isWritable method in the java.sql.ResultSetMetaData interface.  
  
## See Also  
 [SQLServerResultSetMetaData Methods](../../../connect/jdbc/reference/sqlserverresultsetmetadata-methods.md)   
 [SQLServerResultSetMetaData Members](../../../connect/jdbc/reference/sqlserverresultsetmetadata-members.md)   
 [SQLServerResultSetMetaData Class](../../../connect/jdbc/reference/sqlserverresultsetmetadata-class.md)  
  
  
