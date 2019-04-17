---
title: "Programming with SQLXML | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: 4d2cc57c-7293-4d92-b8b1-525e2b35f591
author: MightyPen
ms.author: genemi
manager: craigg
---
# Programming with SQLXML
[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

  This section describes how to use the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] API methods to store and retrieve an XML document in and from a relational database with **SQLXML** objects.  
  
 This section also contains information about the types of SQLXML objects and provides a list of important guidelines and limitations when using SQLXML objects.  
  
## Reading and Writing XML Data with SQLXML Objects  
 The following list describes how to use the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] API methods to read and write XML data with SQLXML objects:  
  
-   To create a SQLXML object, use the [createSQLXML](../../connect/jdbc/reference/createsqlxml-method-sqlserverconnection.md) method of the [SQLServerConnection](../../connect/jdbc/reference/sqlserverconnection-class.md) class. Note that this method creates a SQLXML object without any data. To add **xml** data to SQLXML object, call one of the following methods that are specified in the SQLXML interface: setResult, setCharacterStream, setBinaryStream, or setString.  
  
-   To retrieve the SQLXML object itself, use the getSQLXML methods of the [SQLServerResultSet](../../connect/jdbc/reference/sqlserverresultset-class.md) class or the [SQLServerCallableStatement](../../connect/jdbc/reference/sqlservercallablestatement-class.md) class.  
  
-   To retrieve the **xml** data from a SQLXML object, use one of the following methods that are specified in the SQLXML interface: getSource, getCharacterStream, getBinaryStream, or getString.  
  
-   To update the **xml** data in a SQLXML object, use the [updateSQLXML](../../connect/jdbc/reference/updatesqlxml-method-sqlserverresultset.md) method of the [SQLServerResultSet](../../connect/jdbc/reference/sqlserverresultset-class.md) class.  
  
-   To store a SQLXML object in a database table column of type **xml**, use the setSQLXML methods of the [SQLServerPreparedStatement](../../connect/jdbc/reference/sqlserverpreparedstatement-class.md) class or the [SQLServerCallableStatement](../../connect/jdbc/reference/sqlservercallablestatement-class.md) class.  
  
 The example code in [SQLXML Data Type Sample](../../connect/jdbc/sqlxml-data-type-sample.md) demonstrates how to perform these common API tasks.  
  
## Readable and Writable SQLXML Objects  
 The following table lists which types of SQLXML objects are supported by the setter, getter, and updater methods provided by the JDBC API. The columns in the table refer to the following:  
  
-   The **Method Name** column lists the supported getter, setter, and updater methods in the JDBC API.  
  
-   The **Getter SQLXML Object** column represents a SQLXML object, which is created by either the [getSQLXML](../../connect/jdbc/reference/getsqlxml-method-sqlservercallablestatement.md) method of the [SQLServerCallableStatement](../../connect/jdbc/reference/sqlservercallablestatement-class.md) class or the [getSQLXML](../../connect/jdbc/reference/getsqlxml-method-sqlserverresultset.md) method of the [SQLServerResultSet](../../connect/jdbc/reference/sqlserverresultset-class.md) class.  
  
-   The **Setter SQLXML Object** column represents a SQLXML object, which is created by the [createSQLXML](../../connect/jdbc/reference/createsqlxml-method-sqlserverconnection.md) method of the [SQLServerConnection](../../connect/jdbc/reference/sqlserverconnection-class.md) class. Note that the setter methods below accept only a SQLXML object created by the [createSQLXML](../../connect/jdbc/reference/createsqlxml-method-sqlserverconnection.md) method.  
  
|Method Name|Getter SQLXML Object<br /><br /> (Readable)|Setter SQLXML Object<br /><br /> (Writable)|  
|-----------------|-------------------------------------------|-------------------------------------------|  
|CallableStatement.setSQLXML()|Not Supported|Supported|  
|CallableStatement.setObject()|Not Supported|Supported|  
|PreparedStatement.setSQLXML()|Not Supported|Supported|  
|PreparedStatement.setObject()|Not Supported|Supported|  
|ResultSet.updateSQLXML()|Not Supported|Supported|  
|ResultSet.updateObject()|Not Supported|Supported|  
|ResultSet.getSQLXML()|Supported|Not Supported|  
|CallableStatement.getSQLXML()|Supported|Not Supported|  
  
 As shown in the table above, the setter SQLXML methods will not work with the readable SQLXML objects; similarly, the getter methods will not work with the writable SQLXML objects.  
  
 If the application invokes the setObject method by specifying a scale or a length parameter with a SQLXML object, the scale or length parameter is ignored.  
  
## Guidelines and Limitations when Using SQLXML Objects  
 Applications can use SQLXML objects to read and write the XML data from and to the database. The following list provides information about specific limitations and guidance when using SQLXML objects:  
  
-   A SQLXML object can be valid only for the duration of the transaction in which it was created.  
  
-   A SQLXML object received from a getter method can only be used to read data.  
  
-   A SQLXML object created by the connection object can only be used to write data.  
  
-   The application can invoke only one getter method on a readable SQLXML object to read data. After the getter method is invoked, all other getter or setter methods on the same SQLXML object fail.  
  
-   The application can invoke only the free method on the SQLXML object after it is read or written to. However, it is still possible to process the returned stream or source as long as the underlying column or parameter is active. If the underlying column or parameter becomes inactive, the stream or source associated with the SQLXML object will be closed. If the underlying column or parameter is no longer valid, the underlying data will not be available for the Stream, Simple API for XML (SAX), and Streaming API for XML (StAX) getters.  
  
-   The application can invoke only one setter method on a writable SQLXML object. After the setter method is invoked, all other setter or getter methods on the same SQLXML object fail.  
  
-   To set data on the SQLXML object, the application must use the appropriate setter method and the functions in the returned object.  
  
-   The getSQLXML methods of the [SQLServerCallableStatement](../../connect/jdbc/reference/sqlservercallablestatement-class.md) class and the [SQLServerResultSet](../../connect/jdbc/reference/sqlserverresultset-class.md) class returns **null** data if the underlying column is **null**.  
  
-   The setter objects can be valid through the connection they are created within.  
  
-   Applications are not allowed to set a **null** value by using the setter methods provided by the SQLXML interface. The applications can set an empty string ("") by using the setter methods provided in the SQLXML interface. To set a **null** value, the applications should call one of the following:  
  
    -   The setNull methods of the [SQLServerCallableStatement](../../connect/jdbc/reference/sqlservercallablestatement-class.md) class and [SQLServerPreparedStatement](../../connect/jdbc/reference/sqlserverpreparedstatement-class.md) class.  
  
    -   The setObject methods of the [SQLServerCallableStatement](../../connect/jdbc/reference/sqlservercallablestatement-class.md) class and [SQLServerPreparedStatement](../../connect/jdbc/reference/sqlserverpreparedstatement-class.md) class.  
  
    -   The setSQLXML methods of the [SQLServerCallableStatement](../../connect/jdbc/reference/sqlservercallablestatement-class.md) class and [SQLServerPreparedStatement](../../connect/jdbc/reference/sqlserverpreparedstatement-class.md) class with a **null** parameter value.  
  
-   When working with XML documents, we recommend using Simple API for XML (SAX) and Streaming API for XML (StAX) parsers instead of Document Object Model (DOM) parsers for performance reasons.  
  
 XML parsers cannot handle empty values. However, SQL Server allows applications to retrieve and store empty values from and to database columns of the XML data type. That means that when parsing the XML data, if the underlying value is empty, an exception is thrown by the parser. For DOM outputs, the JDBC driver catches that exception and throws an error. For SAX and Stax outputs, the error comes from the parser directly.  
  
## Adaptive Buffering and SQLXML Support  
 The binary and character streams returned by the SQLXML object obey the adaptive or full buffering modes. On the other hand, if the XML parsers are not streams, they will not obey the adaptive or full settings. For more information about adaptive buffering, see [Using Adaptive Buffering](../../connect/jdbc/using-adaptive-buffering.md).  
  
## See Also  
 [Supporting XML Data](../../connect/jdbc/supporting-xml-data.md)  
  
  
