---
title: "getNString Method (java.lang.String) (SQLServerResultSet)"
description: "getNString Method (java.lang.String) (SQLServerResultSet)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
---
# getNString Method (java.lang.String) (SQLServerResultSet)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the value of the designated column in the current row of the [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object as a java.lang.String object.  
  
## Syntax  
  
```  
  
public java.lang.String getNString(java.lang.String columnLabel)  
```  
  
#### Parameters  
 *columnLabel*  
  
 A String that contains the column label.  
  
## Return Value  
 A String object.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getNString method is specified by the getNString method in the java.sql.SQLServerResultSet interface.  
  
 This method can be used to retrieve the value of an **nvarchar**, **nchar**, **nvarchar(max)**, **ntext**, or **xml** column in the current row of this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object. If you try to use this method to retrieve values of other data types, an exception will be thrown.  
  
## See Also  
 [getNString Method &#40;SQLServerResultSet&#41;](../../../connect/jdbc/reference/getnstring-method-sqlserverresultset.md)   
 [SQLServerResultSet Members](../../../connect/jdbc/reference/sqlserverresultset-members.md)  
  
  
