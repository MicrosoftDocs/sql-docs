---
title: "getBinaryStream Method (java.lang.String)"
description: "getBinaryStream Method (java.lang.String)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerResultSet.getBinaryStream (java.lang.String)"
apitype: "Assembly"
---
# getBinaryStream Method (java.lang.String)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the value of the designated column name in the current row of this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object as a binary stream of uninterpreted bytes.  
  
## Syntax  
  
```  
  
public java.io.InputStream getBinaryStream(java.lang.String columnName)  
```  
  
#### Parameters  
 *columnName*  
  
 A **String** that contains the column name.  
  
## Return Value  
 An InputStream object.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getBinaryStream method is specified by the getBinaryStream method in the java.sql.ResultSet interface.  
  
 This method can be used only with [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] data types of binary, varbinary, varbinary(max), and image. Trying to use it with other data types will cause an exception to be thrown.  
  
 After this method gets the value as a stream, the value can then be read in chunks from the stream. This method is particularly suitable for retrieving large LONGVARBINARY values.  
  
> [!NOTE]  
>  All the data in the returned stream must be read before getting the value of any other column. The next call to a getter method implicitly closes the stream. Also, a stream can return 0 when the method InputStream.available is called, whether there is data available or not.  
  
## See Also  
 [getBinaryStream Method &#40;SQLServerResultSet&#41;](../../../connect/jdbc/reference/getbinarystream-method-sqlserverresultset.md)   
 [SQLServerResultSet Members](../../../connect/jdbc/reference/sqlserverresultset-members.md)   
 [SQLServerResultSet Class](../../../connect/jdbc/reference/sqlserverresultset-class.md)  
  
  
