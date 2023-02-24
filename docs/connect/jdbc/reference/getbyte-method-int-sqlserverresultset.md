---
title: "getByte Method (int) (SQLServerResultSet)"
description: "getByte Method (int) (SQLServerResultSet)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerResultSet.getByte (int)"
apitype: "Assembly"
---
# getByte Method (int) (SQLServerResultSet)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the value of the designated column index in the current row of this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object as a **byte** in the Java programming language.  
  
## Syntax  
  
```  
  
public byte getByte(int columnIndex)  
```  
  
#### Parameters  
 *columnIndex*  
  
 An **int** that indicates the column index.  
  
## Return Value  
 A **byte** value.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getByte method is specified by the getByte method in the java.sql.ResultSet interface.  
  
 This method is supported only on [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] data types that can safely return a byte value, such as tinyint and bit. All other data types will cause an exception to be thrown.  
  
## See Also  
 [getByte Method &#40;SQLServerResultSet&#41;](../../../connect/jdbc/reference/getbyte-method-sqlserverresultset.md)   
 [SQLServerResultSet Members](../../../connect/jdbc/reference/sqlserverresultset-members.md)   
 [SQLServerResultSet Class](../../../connect/jdbc/reference/sqlserverresultset-class.md)  
  
  
