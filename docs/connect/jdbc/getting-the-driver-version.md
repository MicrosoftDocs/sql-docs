---
title: Getting the driver version
description: Learn how and where to find the version of the Microsoft JDBC Driver for SQL Server.
author: David-Engel
ms.author: v-davidengel
ms.date: 05/03/2021
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Getting the driver version

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

The version of the installed [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] can be found in the following ways:

- Call the [SQLServerDatabaseMetaData](reference/sqlserverdatabasemetadata-class.md) methods [getDriverMajorVersion](reference/getdrivermajorversion-method-sqlserverdatabasemetadata.md), [getDriverMinorVersion](reference/getdriverminorversion-method-sqlserverdatabasemetadata.md), or [getDriverVersion](reference/getdriverversion-method-sqlserverdatabasemetadata.md).

- The version is displayed in the readme.txt file of the product distribution.

Also, the JDBC driver name can be returned from the [getDriverName](reference/getdrivername-method-sqlserverdatabasemetadata.md) method call on the SQLServerDatabaseMetaData class. It will return, for example, "Microsoft JDBC Driver 6.4 for SQL Server".

The following lines are example output from calls to the methods of the SQLServerDatabaseMetaData class:

`getDriverName` = Microsoft JDBC Driver 6.4 for SQL Server

`getDriverMajorVersion` = 9

`getDriverMinorVersion` = 2

`getDriverVersion` = 9.2.xxx.x (Where "xxx.x" is the final version number)

## See also

[Diagnosing problems with the JDBC driver](diagnosing-problems-with-the-jdbc-driver.md)
