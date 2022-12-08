---
title: "getString Method (int) (SQLServerResultSet)"
description: "getString Method (int) (SQLServerResultSet)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerResultSet.getString (int)"
apitype: "Assembly"
---
# getString Method (int) (SQLServerResultSet)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the value of the designated column index in the current row of this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object as a **String** in the Java programming language.  
  
## Syntax  
  
```  
  
public java.lang.String getString(int columnIndex)  
```  
  
#### Parameters  
 *columnIndex*  
  
 An **int** that indicates the column index.  
  
## Return Value  
 A **String** value.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getString method is specified by the getString method in the java.sql.ResultSet interface.  
  
 All columns in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] can be returned as a String. This means that a **String** representation of all number-based and character-based types, and a hex-string representation of binary columns such as binary, varbinary, varbinary(max), image, timestamp, and uniqueidentifier, can be returned.  
  
 Location-sensitive types such as money, smallmoney, datetime, smalldatetime, float, real, decimal, and numeric will return the canonical toString() format for the underlying value of the type.  
  
 User-defined types are returned as hexadecimal **String** values.  
  
## See Also  
 [getString Method &#40;SQLServerResultSet&#41;](../../../connect/jdbc/reference/getstring-method-sqlserverresultset.md)   
 [SQLServerResultSet Members](../../../connect/jdbc/reference/sqlserverresultset-members.md)   
 [SQLServerResultSet Class](../../../connect/jdbc/reference/sqlserverresultset-class.md)  
  
  
