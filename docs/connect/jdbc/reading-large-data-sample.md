---
title: Reading large data sample
description: This Microsoft JDBC Driver for SQL Server sample application demonstrates how to retrieve large column values from a database using the getCharacterStream method.
author: David-Engel
ms.author: v-davidengel
ms.date: 08/12/2019
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---

# Reading large data sample

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

This [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] sample application demonstrates how to retrieve a large single-column value from a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database by using the [getCharacterStream](reference/getcharacterstream-method-sqlserverresultset.md) method.

The code file for this sample is named ReadLargeData.java, and it can be found in the following location:

```bash
\<installation directory>\sqljdbc_<version>\<language>\samples\adaptive
```

## Requirements

To run this sample application, you'll need access to the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] sample database. You must also set the classpath to include the mssql-jdbc jar file. For more information about how to set the classpath, see [Using the JDBC Driver](using-the-jdbc-driver.md).

> [!NOTE]
> The [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] provides mssql-jdbc class library files to be used depending on your preferred Java Runtime Environment (JRE) settings. For more information about which JAR file to choose, see [System Requirements for the JDBC Driver](system-requirements-for-the-jdbc-driver.md).

## Example

In the following example, the sample code makes a connection to the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database. Next, the sample code creates sample data and updates the Production.Document table by using a parameterized query.

In addition, the sample code demonstrates how to get the adaptive buffering mode by using the [getResponseBuffering](reference/getresponsebuffering-method-sqlserverstatement.md) method of the [SQLServerStatement](reference/sqlserverstatement-class.md) class. Note that starting with the JDBC driver version 2.0 release, the responseBuffering connection property is set to "adaptive" by default.

Then, using an SQL statement with the [SQLServerStatement](reference/sqlserverstatement-class.md) object, the sample code runs the SQL statement and places the data that it returns into a [SQLServerResultSet](reference/sqlserverresultset-class.md) object.

Finally, the sample code iterates through the rows of data that are in the result set, and uses the [getCharacterStream](reference/getcharacterstream-method-sqlserverresultset.md) method to access some of the data.

[!code[JDBC#UsingAdaptiveBuffering1](codesnippet/Java/reading-large-data-sample_1.java)]

## See also

[Working with large data](working-with-large-data.md)
