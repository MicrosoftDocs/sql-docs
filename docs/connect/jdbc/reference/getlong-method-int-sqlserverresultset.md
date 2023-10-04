---
title: "getLong Method (int) (SQLServerResultSet)"
description: "getLong Method (int) (SQLServerResultSet)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerResultSet.getLong (int)"
apitype: "Assembly"
---
# getLong Method (int) (SQLServerResultSet)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the value of the designated column index in the current row of this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object as a **long** in the Java programming language.  
  
## Syntax  
  
```  
  
public long getLong(int columnIndex)  
```  
  
#### Parameters  
 *columnIndex*  
  
 An **int** that indicates the column index.  
  
## Return Value  
 A **long** value.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getLong method is specified by the getLong method in the java.sql.ResultSet interface.  
  
 This method is supported only on [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] data types that can safely return an integer value such as bigint, int, smallint, tinyint, and bit. Using this method on any other data types will cause an exception to be thrown.  
  
## See Also  
 [getLong Method &#40;SQLServerResultSet&#41;](../../../connect/jdbc/reference/getlong-method-sqlserverresultset.md)   
 [SQLServerResultSet Members](../../../connect/jdbc/reference/sqlserverresultset-members.md)   
 [SQLServerResultSet Class](../../../connect/jdbc/reference/sqlserverresultset-class.md)  
  
  
