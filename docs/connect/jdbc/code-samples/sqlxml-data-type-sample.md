---
title: "SQLXML Data Type Sample | Microsoft Docs"
ms.custom: ""
ms.date: "07/31/2018"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: 8f2ff25b-71fd-46d7-b6de-d656095d2aad
author: MightyPen
ms.author: genemi
manager: craigg
---

# SQLXML Data Type Sample

[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

This [!INCLUDE[jdbcNoVersion](../../../includes/jdbcnoversion_md.md)] sample application demonstrates how to store XML data in a relational database, how to retrieve XML data from a database, and how to parse XML data with the **SQLXML** Java data type.

The code examples in this section use a Simple API for XML (SAX) parser. The SAX is a publicly developed standard for the events-based parsing of XML documents. It also provides an application programming interface for working with XML data. Note that the applications can use any other XML parser as well, such as the Document Object Model (DOM) or the Streaming API for XML (StAX), or so on.

The Document Object Model (DOM) provides a programmatic representation of XML documents, fragments, nodes, or node-sets. It also provides an application programming interface for working with XML data. Similarly, the Streaming API for XML (StAX) is a Java-based API for pull-parsing XML.

> [!IMPORTANT]  
> In order to use the SAX parser API, you must import the standard SAX implementation from the javax.xml package.

The code file for this sample is named SqlXmlDataType.java, and it can be found in the following location:

```bash
\<installation directory>\sqljdbc_<version>\<language>\samples\datatypes
```

## Requirements

To run this sample application, you must set the classpath to include the sqljdbc4.jar file. If the classpath is missing an entry for sqljdbc4.jar, the sample application throws the "Class not found" exception. For more information about how to set the classpath, see [Using the JDBC Driver](../../../connect/jdbc/using-the-jdbc-driver.md).

In addition, you need access to the [!INCLUDE[ssSampleDBnormal](../../../includes/sssampledbnormal_md.md)] sample database to run this sample application.

## Example

In the following example, the sample code makes a connection to the [!INCLUDE[ssSampleDBnormal](../../../includes/sssampledbnormal_md.md)] database and then calls the createSampleTables method.

The createSampleTables method drops the test tables, TestTable1, and TestTable2, if they exist. Then, it inserts two rows into TestTable1.

In addition, the code sample includes the following three methods and one additional class, which is named ExampleContentHandler.

The ExampleContentHandler class implements a custom content handler, which defines methods for parser events.

The showGetters method demonstrates how to parse the data in the SQLXML object by using the SAX, ContentHandler, and XMLReader. First, the code sample creates an instance of a custom content handler, which is ExampleContentHandler. Next, it creates and executes an SQL statement that returns a set of data from TestTable1. Then, the code example gets a SAX parser and parses the XML data.

The showSetters method demonstrates how to set the **xml** column by using the SAX, ContentHandler, and ResultSet. First, it creates an empty SQLXML object by using the [createSQLXML](../../../connect/jdbc/reference/createsqlxml-method-sqlserverconnection.md) method of the Connection class. Then, it gets an instance of a content handler to write the data into the SQLXML object. Next, the code example writes the data to TestTable1. Finally, the sample code iterates through the rows of data that are in the result set, and uses the [getSQLXML](../../../connect/jdbc/reference/getsqlxml-method-sqlserverresultset.md) method to read the XML data.

The showTransformer method demonstrates how to get an XML data from one table and insert that XML data into another table by using the SAX and the Transformer. First, it retrieves the source SQLXML object from the TestTable1. Then, it creates an empty destination SQLXML object by using the [createSQLXML](../../../connect/jdbc/reference/createsqlxml-method-sqlserverconnection.md) method of the Connection class. Next, it updates the destination SQLXML object and writes the XML data to TestTable2. Finally, the sample code iterates through the rows of data that are in the result set, and uses the [getSQLXML](../../../connect/jdbc/reference/getsqlxml-method-sqlserverresultset.md) method to read the XML data in TestTable2.

[!code[JDBC#UsingSQLXML1](../../../connect/jdbc/codesnippet/Java/sqlxml-data-type-sample_1.java)]

## See Also

[Working with Data Types &#40;JDBC&#41;](../../../connect/jdbc/code-samples/working-with-data-types-jdbc.md)
