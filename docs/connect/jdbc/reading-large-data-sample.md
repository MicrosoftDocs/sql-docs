---
title: "Reading Large Data Sample | Microsoft Docs"
ms.custom: ""
ms.date: "07/31/2018"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: 6c986144-3854-4352-8331-e79eccbefc28
author: MightyPen
ms.author: genemi
manager: craigg
---

# Reading Large Data Sample

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

This [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] sample application demonstrates how to retrieve a large single-column value from a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database by using the [getCharacterStream](../../connect/jdbc/reference/getcharacterstream-method-sqlserverresultset.md) method.

The code file for this sample is named ReadLargeData.java, and it can be found in the following location:

```bash
\<installation directory>\sqljdbc_<version>\<language>\samples\adaptive
```

## Requirements

To run this sample application, you'll need access to the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal_md.md)] sample database. You must also set the classpath to include the mssql-jdbc jar file. For more information about how to set the classpath, see [Using the JDBC Driver](../../connect/jdbc/using-the-jdbc-driver.md).

> [!NOTE]  
> The [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] provides mssql-jdbc class library files to be used depending on your preferred Java Runtime Environment (JRE) settings. For more information about which JAR file to choose, see [System Requirements for the JDBC Driver](../../connect/jdbc/system-requirements-for-the-jdbc-driver.md).

## Example

In the following example, the sample code makes a connection to the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal_md.md)] database. Next, the sample code creates sample data and updates the Production.Document table by using a parameterized query.

In addition, the sample code demonstrates how to get the adaptive buffering mode by using the [getResponseBuffering](../../connect/jdbc/reference/getresponsebuffering-method-sqlserverstatement.md) method of the [SQLServerStatement](../../connect/jdbc/reference/sqlserverstatement-class.md) class. Note that starting with the JDBC driver version 2.0 release, the responseBuffering connection property is set to "adaptive" by default.

Then, using an SQL statement with the [SQLServerStatement](../../connect/jdbc/reference/sqlserverstatement-class.md) object, the sample code runs the SQL statement and places the data that it returns into a [SQLServerResultSet](../../connect/jdbc/reference/sqlserverresultset-class.md) object.

Finally, the sample code iterates through the rows of data that are in the result set, and uses the [getCharacterStream](../../connect/jdbc/reference/getcharacterstream-method-sqlserverresultset.md) method to access some of the data.

[!code[JDBC#UsingAdaptiveBuffering1](../../connect/jdbc/codesnippet/Java/reading-large-data-sample_1.java)]

## See Also

[Working with Large Data](../../connect/jdbc/working-with-large-data.md)
