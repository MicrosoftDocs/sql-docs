---
title: "updateDate Method (java.lang.String, java.sql.Date)"
description: "updateDate Method (java.lang.String, java.sql.Date)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerResultSet.updateDate (java.lang.String, java.sql.Date)"
apitype: "Assembly"
---
# updateDate Method (java.lang.String, java.sql.Date)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Updates the designated column with a date value given the column name.  
  
## Syntax  
  
```  
  
public void updateDate(java.lang.String columnName,  
                       java.sql.Date x)  
```  
  
#### Parameters  
 *columnName*  
  
 A **String** that contains the column name.  
  
 *x*  
  
 A date value.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This updateDate method is specified by the updateDate method in the java.sql.ResultSet interface.  
  
## See Also  
 [updateDate Method &#40;SQLServerResultSet&#41;](../../../connect/jdbc/reference/updatedate-method-sqlserverresultset.md)   
 [SQLServerResultSet Members](../../../connect/jdbc/reference/sqlserverresultset-members.md)   
 [SQLServerResultSet Class](../../../connect/jdbc/reference/sqlserverresultset-class.md)  
  
  
