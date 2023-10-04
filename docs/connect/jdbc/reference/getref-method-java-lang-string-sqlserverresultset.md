---
title: "getRef Method (java.lang.String) (SQLServerResultSet)"
description: "getRef Method (java.lang.String) (SQLServerResultSet)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerResultSet.getRef (java.lang.String)"
apitype: "Assembly"
---
# getRef Method (java.lang.String) (SQLServerResultSet)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the value of the designated column name in the current row of this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object as a Ref object in the Java programming language.  
  
## Syntax  
  
```  
  
public java.sql.Ref getRef(java.lang.String colName)  
```  
  
#### Parameters  
 *colName*  
  
 A **String** that contains the column name.  
  
## Return Value  
 A Ref object.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getRef method is specified by the getRef method in the java.sql.ResultSet interface.  
  
## See Also  
 [getRef Method &#40;SQLServerResultSet&#41;](../../../connect/jdbc/reference/getref-method-sqlserverresultset.md)   
 [SQLServerResultSet Members](../../../connect/jdbc/reference/sqlserverresultset-members.md)   
 [SQLServerResultSet Class](../../../connect/jdbc/reference/sqlserverresultset-class.md)  
  
  
