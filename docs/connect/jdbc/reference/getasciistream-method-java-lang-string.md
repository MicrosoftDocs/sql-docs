---
title: "getAsciiStream Method (java.lang.String)"
description: "getAsciiStream Method (java.lang.String)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerResultSet.getAsciiStream (java.lang.String)"
apitype: "Assembly"
---
# getAsciiStream Method (java.lang.String)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the value of the designated column name in the current row of this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object as a stream of ASCII characters.  
  
## Syntax  
  
```  
  
public java.io.InputStream getAsciiStream(java.lang.String columnName)  
```  
  
#### Parameters  
 *columnName*  
  
 A **String** that contains the column name.  
  
## Return Value  
 An InputStream object.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getAsciiStream method is specified by the getAsciiStream method in the java.sql.ResultSet interface.  
  
## See Also  
 [getAsciiStream Method &#40;SQLServerResultSet&#41;](../../../connect/jdbc/reference/getasciistream-method-sqlserverresultset.md)   
 [SQLServerResultSet Members](../../../connect/jdbc/reference/sqlserverresultset-members.md)   
 [SQLServerResultSet Class](../../../connect/jdbc/reference/sqlserverresultset-class.md)  
  
  
