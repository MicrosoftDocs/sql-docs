---
title: "getColumnClassName Method (SQLServerResultSetMetaData)"
description: "getColumnClassName Method (SQLServerResultSetMetaData)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerResultSetMetaData.getColumnClassName"
apitype: "Assembly"
---
# getColumnClassName Method (SQLServerResultSetMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Returns the fully-qualified name of the Java class whose instances are manufactured if the [getObject](../../../connect/jdbc/reference/getobject-method-sqlserverresultset.md) method of the [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) class is called to retrieve a value from the column.  
  
## Syntax  
  
```  
  
public java.lang.String getColumnClassName(int column)  
```  
  
#### Parameters  
 *column*  
  
 An **int** that indicates the column index.  
  
## Return Value  
 A **String** that contains the fully-qualified name of the class.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getColumnClassName method is specified by the getColumnClassName method in the java.sql.ResultSetMetaData interface.  
  
## See Also  
 [SQLServerResultSetMetaData Methods](../../../connect/jdbc/reference/sqlserverresultsetmetadata-methods.md)   
 [SQLServerResultSetMetaData Members](../../../connect/jdbc/reference/sqlserverresultsetmetadata-members.md)   
 [SQLServerResultSetMetaData Class](../../../connect/jdbc/reference/sqlserverresultsetmetadata-class.md)  
  
  
