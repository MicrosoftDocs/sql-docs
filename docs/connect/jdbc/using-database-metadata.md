---
title: "Using Database Metadata | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: 8b048371-e912-4ed1-afd7-436978f48888
author: MightyPen
ms.author: genemi
manager: craigg
---

# Using Database Metadata

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

To query a database for information about what it supports, the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] implements the [SQLServerDatabaseMetaData](../../connect/jdbc/reference/sqlserverdatabasemetadata-class.md) class. This class contains numerous methods that return information in the form of a single value, or as a result set.

To create a SQLServerDatabaseMetaData object, you can use the [getMetaData](../../connect/jdbc/reference/getmetadata-method-sqlserverconnection.md) method of the [SQLServerConnection](../../connect/jdbc/reference/sqlserverconnection-class.md) class to get information about the database that it is connected to.

In the following example, an open connection to the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal_md.md)] sample database is passed in to the function, the getMetaData method of the SQLServerConnection class is used to return a SQLServerDatabaseMetadata object, and then various methods of the SQLServerDatabaseMetaData object are used to display information about the driver, driver version, database name, and database version.

[!code[JDBC#UsingDBMetaData1](../../connect/jdbc/codesnippet/Java/using-database-metadata_1.java)]

## See Also

[Handling Metadata with the JDBC Driver](../../connect/jdbc/handling-metadata-with-the-jdbc-driver.md)
