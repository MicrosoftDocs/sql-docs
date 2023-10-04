---
title: updateCharacterStream method (java.lang.String, java.io.Reader, long)
description: "updateCharacterStream Method (java.lang.String, java.io.Reader, long)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
---
# updateCharacterStream Method (java.lang.String, java.io.Reader, long)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Updates the designated column with a character stream value, which will have the specified number of characters.  
  
## Syntax  
  
```  
  
public void updateCharacterStream(java.lang.String columnLabel,  
                                  java.io.Reader reader,  
                                  long length)  
```  
  
#### Parameters  
 *columnLabel*  
  
 A **String** that contains the column label.  
  
 *reader*  
  
 A Reader object.  
  
 *length*  
  
 The length of the stream.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This updateCharacterStream method is specified by the updateCharacterStream method in the java.sql.ResultSet interface.  
  
 This method passes Unicode characters from a Reader object to selected text and binary columns. This includes all text columns and binary, varbinary, varbinary(max), image, and XML columns, but not UDT columns.  
  
 If the length of the stream is different than what is specified in the *length* parameter, the JDBC driver throws an exception when the row is updated or inserted.  
  
 If the length of the stream is unknown, the *length* parameter may be set to -1 to indicate that the driver should accept the stream regardless of its length. With sqljdbc4.jar, we recommend that you use the JDBC 4.0 method [updateCharacterStream Method &#40;java.lang.String, java.io.Reader&#41;](../../../connect/jdbc/reference/updatecharacterstream-method-java-lang-string-java-io-reader.md) when the application wants to update the column from a stream whose length is unknown.  
  
## See Also  
 [updateCharacterStream Method &#40;SQLServerResultSet&#41;](../../../connect/jdbc/reference/updatecharacterstream-method-sqlserverresultset.md)   
 [SQLServerResultSet Members](../../../connect/jdbc/reference/sqlserverresultset-members.md)   
 [SQLServerResultSet Class](../../../connect/jdbc/reference/sqlserverresultset-class.md)  
  
  
