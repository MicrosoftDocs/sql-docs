---
title: "updateBinaryStream Method (java.io.InputStream, long)"
description: "updateBinaryStream Method (java.lang.String, java.io.InputStream, long)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
---
# updateBinaryStream Method (java.lang.String, java.io.InputStream, long)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Updates the designated column with a binary stream value, which will have the specified number of bytes.  
  
## Syntax  
  
```  
  
public void updateBinaryStream(java.lang.String columnLabel,  
                               java.io.InputStream x,  
                               long length)  
```  
  
#### Parameters  
 *columnLabel*  
  
 AStringthat contains the column label.  
  
 *x*  
  
 An InputStream object.  
  
 *length*  
  
 A **long** that indicates the length of the stream.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This updateBinaryStream method is specified by the updateBinaryStream method in the java.sql.ResultSet interface.  
  
 This method passes bytes from an InputStream object to selected [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] binary columns such as binary, varbinary, varbinary(max), image, xml, and udt. Updating character columns is not supported with this method. To update character columns with an InputStream, use the [updateAsciiStream](../../../connect/jdbc/reference/updateasciistream-method-sqlserverresultset.md) method.  
  
 If the length of the stream is different than what is specified in the *length* parameter, the JDBC driver throws an exception when the row is updated or inserted.  
  
 If the length of the stream is unknown, the *length* parameter may be set to -1 to indicate that the driver should accept the stream regardless of its length. With sqljdbc4.jar, we recommend that you use the JDBC 4.0 method [updateBinaryStream Method &#40;java.lang.String, java.io.InputStream&#41;](../../../connect/jdbc/reference/updatebinarystream-method-java-lang-string-java-io-inputstream.md) when the application wants to update the column from a stream whose length is unknown.  
  
## See Also  
 [updateBinaryStream Method &#40;SQLServerResultSet&#41;](../../../connect/jdbc/reference/updatebinarystream-method-sqlserverresultset.md)   
 [SQLServerResultSet Members](../../../connect/jdbc/reference/sqlserverresultset-members.md)   
 [SQLServerResultSet Class](../../../connect/jdbc/reference/sqlserverresultset-class.md)  
  
  
