---
title: "updateArray Method (java.lang.String, java.sql.Array)"
description: "updateArray Method (java.lang.String, java.sql.Array)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerResultSet.updateArray (java.lang.String, java.sql.Array)"
apitype: "Assembly"
---
# updateArray Method (java.lang.String, java.sql.Array)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Updates the designated column with an Array object given the column name.  
  
## Syntax  
  
```  
  
public void updateArray(java.lang.String columnName,  
                        java.sql.Array x)  
```  
  
#### Parameters  
 *columnName*  
  
 A **String** that contains the column name.  
  
 *x*  
  
 An Array object.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This updateArray method is specified by the updateArray method in the java.sql.ResultSet interface.  
  
## See Also  
 [updateArray Method &#40;SQLServerResultSet&#41;](../../../connect/jdbc/reference/updatearray-method-sqlserverresultset.md)   
 [SQLServerResultSet Members](../../../connect/jdbc/reference/sqlserverresultset-members.md)   
 [SQLServerResultSet Class](../../../connect/jdbc/reference/sqlserverresultset-class.md)  
  
  
