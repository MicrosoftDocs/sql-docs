---
title: "getBytes Method (java.lang.String) (SQLServerResultSet)"
description: "getBytes Method (java.lang.String) (SQLServerResultSet)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerResultSet.getBytes (java.lang.String)"
apitype: "Assembly"
---
# getBytes Method (java.lang.String) (SQLServerResultSet)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the value of the designated column name in the current row of this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object as a **byte** array in the Java programming language.  
  
## Syntax  
  
```  
  
public byte[] getBytes(java.lang.String columnName)  
```  
  
#### Parameters  
 *columnName*  
  
 A **String** that contains the column name.  
  
## Return Value  
 An array of **byte** values.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getBytes method is specified by the getBytes method in the java.sql.ResultSet interface.  
  
 This method supports retrieving all columns as a raw read of bytes from the server. It returns an array of bytes directly from the server, in the format that is stored on the server.  
  
 In a previous version of the [!INCLUDE[jdbcNoVersion](../../../includes/jdbcnoversion_md.md)], you could use SQLServerResultSet.getBytes to convert values between byte arrays and [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] data type **date**, **time**, **datetime2**, or **datetimeoffset**. Now, using this method with those data types will cause an exception indicating that the conversion is not supported.  
  
## See Also  
 [getBytes Method &#40;SQLServerResultSet&#41;](../../../connect/jdbc/reference/getbytes-method-sqlserverresultset.md)   
 [SQLServerResultSet Members](../../../connect/jdbc/reference/sqlserverresultset-members.md)   
 [SQLServerResultSet Class](../../../connect/jdbc/reference/sqlserverresultset-class.md)  
  
  
