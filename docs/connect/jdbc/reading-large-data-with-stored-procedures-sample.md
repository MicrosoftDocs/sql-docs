---
title: Reading large data with stored procedures sample
description: This JDBC Driver sample demonstrates how to retrieve a large OUT parameter from a stored procedure.
author: David-Engel
ms.author: v-davidengel
ms.date: 04/20/2021
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---

# Reading large data with stored procedures sample

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

This [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] sample application demonstrates how to retrieve a large OUT parameter from a stored procedure.

The code file for this sample is named ExecuteStoredProcedure.java, and can be found in the following location:

```bash
\<installation directory>\sqljdbc_<version>\<language>\samples\adaptive
```

## Requirements

To run this sample application, you'll need access to the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] sample database. Set the classpath to include the mssql-jdbc jar file. For more information about how to set the classpath, see [Using the JDBC Driver](using-the-jdbc-driver.md).

> [!NOTE]
> The [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] provides mssql-jdbc class library files to be used depending on your preferred Java Runtime Environment (JRE) settings. For more information about which JAR file to choose, see [System Requirements for the JDBC Driver](system-requirements-for-the-jdbc-driver.md).

The sample would create the required stored procedure in the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] sample database:

## Example

This sample code:

1. Makes a connection to the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database.
1. Creates sample data and updates the `Production.Document` table by using a parameterized query. Finally, the sample code gets the adaptive buffering mode by using the [getResponseBuffering](reference/getresponsebuffering-method-sqlserverstatement.md) method of the [SQLServerStatement](reference/sqlserverstatement-class.md) class and executes the `GetLargeDataValue` stored procedure. Starting with the JDBC driver version 2.0 release, the `responseBuffering` connection property is set to "adaptive" by default.

Finally, the sample code displays the data returned with the OUT parameters and also demonstrates how to use the `mark` and `reset` methods on the stream to re-read any portion of the data.

:::code language="java" source="codesnippet/Java/reading-large-data-with-_1_1.java":::

## See also

[Working with large data](working-with-large-data.md)
