---
title: "setAsciiStream Method  to input stream bytes - int | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerCallableStatement.setAsciiStream"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 6ea23386-201f-41af-8232-225de3476765
author: MightyPen
ms.author: genemi
manager: craigg
---
# setAsciiStream Method  (java.lang.String, java.io.InputStream, int)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Sets the designated parameter to the given input stream, which will have the specified number of bytes.  
  
## Syntax  
  
```  
  
public void setAsciiStream(java.lang.String parameterName,  
                           java.io.InputStream value,  
                           int length)  
```  
  
#### Parameters  
 *parameterName*  
  
 A **String** that contains the parameter name.  
  
 *value*  
  
 An InputStream object.  
  
 *length*  
  
 An **int** that indicates the length in number of bytes.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This setAsciiStream method is specified by the setAsciiStream method in the java.sql.CallableStatement interface.  
  
 If the length of the stream is different than that specified in the *length* parameter, the JDBC driver throws an exception when the row is updated or inserted.  
  
 If the length of the stream is unknown, the *length* parameter may be set to -1 to indicate that the driver should accept the stream regardless of its length. With sqljdbc4.jar, we recommend that you use the JDBC 4.0 method [setAsciiStream Method (java.lang.String, java.io.InputStream)](../../../connect/jdbc/reference/setasciistream-method-java-lang-string-java-io-inputstream.md) when the application wants to update the column from a stream whose length is unknown.  
  
## See Also  
 [SQLServerCallableStatement Members](../../../connect/jdbc/reference/sqlservercallablestatement-members.md)   
 [SQLServerCallableStatement Class](../../../connect/jdbc/reference/sqlservercallablestatement-class.md)  
  
  
