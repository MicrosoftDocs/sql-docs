---
title: "getSchemaName Method (SQLServerResultSetMetaData)"
description: "getSchemaName Method (SQLServerResultSetMetaData)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerResultSetMetaData.getSchemaName"
apitype: "Assembly"
---
# getSchemaName Method (SQLServerResultSetMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Gets the table schema name for the designated column.  
  
## Syntax  
  
```  
  
public java.lang.String getSchemaName(int column)  
```  
  
#### Parameters  
 *column*  
  
 An **int** that indicates the column index.  
  
## Return Value  
 A **String** that contains the schema name.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getSchemaName method is specified by the getSchemaName method in the java.sql.ResultSetMetaData interface.  
  
## See Also  
 [SQLServerResultSetMetaData Methods](../../../connect/jdbc/reference/sqlserverresultsetmetadata-methods.md)   
 [SQLServerResultSetMetaData Members](../../../connect/jdbc/reference/sqlserverresultsetmetadata-members.md)   
 [SQLServerResultSetMetaData Class](../../../connect/jdbc/reference/sqlserverresultsetmetadata-class.md)  
  
  
