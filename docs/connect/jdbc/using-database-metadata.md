---
title: Using database metadata
description: Learn how to access and navigate database metadata when using the Microsoft JDBC Driver for SQL Server.
author: David-Engel
ms.author: v-davidengel
ms.date: 03/26/2021
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---

# Using database metadata

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

To query a database for information about what it supports, the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] implements the [SQLServerDatabaseMetaData](reference/sqlserverdatabasemetadata-class.md) class. This class contains many methods that return information in the form of a single value, or as a result set.

To create a SQLServerDatabaseMetaData object, you can use the [getMetaData](reference/getmetadata-method-sqlserverconnection.md) method of the [SQLServerConnection](reference/sqlserverconnection-class.md) class to get information about the database that it's connected to.

In the following example, an open connection to the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] sample database is passed in to the function. Then the getMetaData method of the SQLServerConnection class is used to return a SQLServerDatabaseMetadata object. Finally, various SQLServerDatabaseMetaData methods are used to display information about the driver, driver version, database name, and database version.

[!code[JDBC#UsingDBMetaData1](codesnippet/Java/using-database-metadata_1.java)]

## See also

[Handling metadata with the JDBC driver](handling-metadata-with-the-jdbc-driver.md)
