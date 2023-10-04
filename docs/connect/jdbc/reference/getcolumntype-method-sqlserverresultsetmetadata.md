---
title: "getColumnType Method (SQLServerResultSetMetaData)"
description: "getColumnType Method (SQLServerResultSetMetaData)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerResultSetMetaData.getColumnType"
apitype: "Assembly"
---
# getColumnType Method (SQLServerResultSetMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the SQL type of the designated column.  
  
## Syntax  
  
```  
  
public int getColumnType(int column)  
```  
  
#### Parameters  
 *column*  
  
 An **int** that indicates the column index.  
  
## Return Value  
 An **int** that indicates the JDBC type as defined in java.sql.Types.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getColumnType method is specified by the getColumnType method in the java.sql.ResultSetMetaData interface.  
  
 [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] JDBC Driver 3.0 has behavior changes in the DATA_TYPE column. See [SQLServerDatabaseMetaData.getColumns](../../../connect/jdbc/reference/getcolumns-method-sqlserverdatabasemetadata.md) for more information.  
  
## See Also  
 [SQLServerResultSetMetaData Members](../../../connect/jdbc/reference/sqlserverresultsetmetadata-members.md)   
 [SQLServerResultSetMetaData Class](../../../connect/jdbc/reference/sqlserverresultsetmetadata-class.md)  
  
  
