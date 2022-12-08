---
title: "updateNClob Method (java.lang.String, java.sql.NClob)"
description: "updateNClob Method (java.lang.String, java.sql.NClob)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
---
# updateNClob Method (java.lang.String, java.sql.NClob)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Updates the designated column with an **NClob** value.  
  
## Syntax  
  
```  
  
public void updateNClob(java.lang.String columnLabel,  
                        java.sql.NClob x)  
```  
  
#### Parameters  
 *columnLabel*  
  
 A **String** that indicates the column label.  
  
 *x*  
  
 An NClob object.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This updateNClob method is specified by the updateNClob method in the java.sql.ResultSet interface.  
  
 This method is supported only on **nvarchar(max)**, **ntext**, and **xml** columns. Using this method on any other data types will cause an exception to be thrown.  
  
## See Also  
 [updateNClob Method &#40;SQLServerResultSet&#41;](../../../connect/jdbc/reference/updatenclob-method-sqlserverresultset.md)   
 [SQLServerResultSet Members](../../../connect/jdbc/reference/sqlserverresultset-members.md)   
 [SQLServerResultSet Class](../../../connect/jdbc/reference/sqlserverresultset-class.md)  
  
  
