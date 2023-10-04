---
title: "updateClob Method (java.lang.String, java.sql.Clob)"
description: "updateClob Method (java.lang.String, java.sql.Clob)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerResultSet.updateClob (java.lang.String, java.sql.Clob)"
apitype: "Assembly"
---
# updateClob Method (java.lang.String, java.sql.Clob)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Updates the designated column with a java.sql.Clob value given the column name.  
  
## Syntax  
  
```  
  
public void updateClob(java.lang.String columnName,  
                       java.sql.Clob clobValue)  
```  
  
#### Parameters  
 *columnName*  
  
 A **String** that contains the column name.  
  
 *clobValue*  
  
 A Clob object.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This updateClob method is specified by the updateClob method in the java.sql.ResultSet interface.  
  
## See Also  
 [updateClob Method &#40;SQLServerResultSet&#41;](../../../connect/jdbc/reference/updateclob-method-sqlserverresultset.md)   
 [SQLServerResultSet Members](../../../connect/jdbc/reference/sqlserverresultset-members.md)   
 [SQLServerResultSet Class](../../../connect/jdbc/reference/sqlserverresultset-class.md)  
  
  
