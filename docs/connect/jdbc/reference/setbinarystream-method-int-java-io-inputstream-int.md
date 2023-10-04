---
title: "setBinaryStream Method (int, java.io.InputStream, int)"
description: "setBinaryStream Method (int, java.io.InputStream, int)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerPreparedStatement.setBinaryStream"
apitype: "Assembly"
---
# setBinaryStream Method (int, java.io.InputStream, int)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Sets the designated parameter to the specified input stream, which will have the specified number of bytes.  
  
## Syntax  
  
```  
  
public final void setBinaryStream(int n,  
                                  java.io.InputStream x,  
                                  int length)  
```  
  
#### Parameters  
 *n*  
  
 An **int** that indicates the parameter number.  
  
 *x*  
  
 An InputStream object.  
  
 *length*  
  
 An **int** that indicates the number of bytes.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This setBinaryStream method is specified by the setBinaryStream method in the java.sql.PreparedStatement interface.  
  
 If the length of the stream is different from what is specified in the *length* parameter, the JDBC driver throws an exception when the row is updated or inserted.  
  
 If the length of the stream is unknown, the *length* parameter may be set to -1 to indicate that the driver should accept the stream regardless of its length. With sqljdbc4.jar, we recommend that you use the JDBC 4.0 method [setBinaryStream Method &#40;int, java.io.InputStream&#41;](../../../connect/jdbc/reference/setbinarystream-method-int-java-io-inputstream.md) when the application wants to update the column from a stream whose length is unknown.  
  
## See Also  
 [setBinaryStream Method &#40;SQLServerPreparedStatement&#41;](../../../connect/jdbc/reference/setbinarystream-method-sqlserverpreparedstatement.md)   
 [SQLServerPreparedStatement Members](../../../connect/jdbc/reference/sqlserverpreparedstatement-members.md)  
  
  
