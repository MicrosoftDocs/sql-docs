---
title: SQLXML data type sample
description: This JDBC Driver for SQL Server sample application demonstrates how to store, retrieve, and parse XML data from a database with the **SQLXML** Java data type.
author: David-Engel
ms.author: v-davidengel
ms.date: 04/20/2021
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---

# SQLXML data type sample

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

This [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] sample application demonstrates how to store XML data in a relational database, how to retrieve XML data from a database, and how to parse XML data with the **SQLXML** Java data type.

The code examples in this section use a Simple API for XML (SAX) parser. The SAX is a publicly developed standard for the events-based parsing of XML documents. It also provides an application programming interface for working with XML data. Applications can use any other XML parser as well, such as the Document Object Model (DOM) or the Streaming API for XML (StAX), or so on.

The Document Object Model (DOM) provides a programmatic representation of XML documents, fragments, nodes, or node-sets. It also provides an application programming interface for working with XML data. Similarly, the Streaming API for XML (StAX) is a Java-based API for pull-parsing XML.

> [!IMPORTANT]
> In order to use the SAX parser API, you must import the standard SAX implementation from the javax.xml package.

The code file for this sample is named SqlXmlDataType.java, and it can be found in the following location:

```bash
\<installation directory>\sqljdbc_<version>\<language>\samples\datatypes
```

## Requirements

To run this sample application, you must set the classpath to include the sqljdbc4.jar file. If the classpath is missing an entry for sqljdbc4.jar, the sample application throws the "Class not found" exception. For more information about how to set the classpath, see [Using the JDBC Driver](using-the-jdbc-driver.md).

You also need access to the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] sample database to run this sample application.

## Example

In the following example, the sample code makes a connection to the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database and then calls the createSampleTables method.

The createSampleTables method drops the test tables, TestTable1, and TestTable2, if they exist. Then, it inserts two rows into TestTable1.

Also, the code sample includes the following three methods and one other class, which is named ExampleContentHandler.

The ExampleContentHandler class implements a custom content handler, which defines methods for parser events.

The showGetters method demonstrates how to parse the data in the SQLXML object by using the SAX, ContentHandler, and XMLReader. First, the code sample creates an instance of a custom content handler, which is ExampleContentHandler. Next, it creates and executes an SQL statement that returns a set of data from TestTable1. Then, the code example gets a SAX parser and parses the XML data.

The showSetters method demonstrates how to set the **xml** column by using the SAX, ContentHandler, and ResultSet. First, it creates an empty SQLXML object by using the [createSQLXML](reference/createsqlxml-method-sqlserverconnection.md) method of the Connection class. Then, it gets an instance of a content handler to write the data into the SQLXML object. Next, the code example writes the data to TestTable1. Finally, the sample code iterates through the rows of data that are in the result set, and uses the [getSQLXML](reference/getsqlxml-method-sqlserverresultset.md) method to read the XML data.

The showTransformer method demonstrates how to get XML data from one table and insert that XML data into another table by using the SAX and the Transformer. First, it retrieves the source SQLXML object from the TestTable1. Then, it creates an empty destination SQLXML object by using the [createSQLXML](reference/createsqlxml-method-sqlserverconnection.md) method of the Connection class. Next, it updates the destination SQLXML object and writes the XML data to TestTable2. Finally, the sample code iterates through the rows of data that are in the result set, and uses the [getSQLXML](reference/getsqlxml-method-sqlserverresultset.md) method to read the XML data in TestTable2.

:::code language="java" source="codesnippet/Java/sqlxml-data-type-sample_1.java":::

## See also

[Working with data types &#40;JDBC&#41;](working-with-data-types-jdbc.md)
