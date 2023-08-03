---
title: Handling metadata
description: Learn what metadata is available in the different JDBC objects when using the JDBC Driver for SQL Server.
author: David-Engel
ms.author: v-davidengel
ms.date: 04/20/2021
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Handling metadata with the JDBC driver

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

The [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] can be used to work with metadata in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database in various ways. The JDBC driver can be used to get metadata about the database, a result set, or parameters.

The JDBC driver provides three classes for retrieving metadata from a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database:

- [SQLServerDatabaseMetaData](reference/sqlserverdatabasemetadata-class.md): Used to return information about the database that is currently connected.
- [SQLServerResultSetMetaData](reference/sqlserverresultsetmetadata-class.md): Used to return information about a result set.
- [SQLServerParameterMetaData](reference/sqlserverparametermetadata-class.md): Used to return information about the parameters of prepared and callable statements.

The articles in this section describe how you can use each of the three metadata classes to work with metadata in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database.

> [!NOTE]
> The metadata methods discussed in this section are generally expensive in terms of application performance, so care should be taken with their usage.

## In this section

|Article|Description|
|-----------|-----------------|
|[Using database metadata](using-database-metadata.md)|Describes how to retrieve metadata information about the currently connected database.|
|[Using result set metadata](using-result-set-metadata.md)|Describes how to retrieve metadata information about the current result set.|
|[Using parameter metadata](using-parameter-metadata.md)|Describes how to retrieve metadata information about the parameters of prepared and callable statements.|

## See also

[Overview of the JDBC driver](overview-of-the-jdbc-driver.md)
