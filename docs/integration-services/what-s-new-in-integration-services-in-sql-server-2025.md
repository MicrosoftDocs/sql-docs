---
title: "What's New in Integration Services in SQL Server 2025"
description: What's New in Integration Services in SQL Server 2025
ms.reviewer: randolphwest
ms.date: 11/18/2025
ms.service: sql
ms.subservice: integration-services
ms.topic: whats-new
ms.custom:
  - intro-whats-new
  - ignite-2025
---

# What's New in SQL Server 2025 Integration Services

This article describes the features that have been added or updated in [!INCLUDE [sssql25-md](../includes/sssql25-md.md)] Integration Services.

## New features

ADO.NET connection manager now supports Microsoft SqlClient Data Provider. For more information, see [ADO.NET connection manager](connection-manager/ado-net-connection-manager.md).

## Breaking changes

### .NET API [Microsoft.SqlServer.Dts.Runtime Namespace](/dotnet/api/microsoft.sqlserver.dts.runtime)

When upgrading to SSIS 2025, if you use .NET API [Microsoft.SqlServer.Dts.Runtime Namespace](/dotnet/api/microsoft.sqlserver.dts.runtime), your projects need to update the references and rebuild when SSIS Package includes Execute SQL Task or SSIS package has certain SSIS tasks that rely on SQL Server Management Objects (SMO).

## Deprecated features in Integration Services

The following section describes deprecated features in [!INCLUDE [sssql25-md](../includes/sssql25-md.md)] Integration Services.

### Legacy Integration Services Service

This [**legacy Integration Services** Service](service/integration-services-service-ssis-service.md#manage-the-service) available in **SQL Server Management Studio** is deprecated in [!INCLUDE [sssql25-md](../includes/sssql25-md.md)] Integration Services.

**SQL Server Management Studio** can connect to an instance of the legacy Integration Services Service to monitor the packages that are stored in the SSIS Package Store. The SSIS Package Store can be either the `msdb` database in an instance of SQL Server or the designated folders in the file system. This feature is deprecated in [!INCLUDE [sssql25-md](../includes/sssql25-md.md)] Integration Services.

### Integration Services 32-bit mode

Integration Services 32-bit mode is deprecated in [!INCLUDE [sssql25-md](../includes/sssql25-md.md)] Integration Services engine. Tools including SQL Server Management Studio 21 and SQL Server Integration Services Projects 2022 supports 64-bit only in current version and onward.

### SqlClient Data Provider (SDS) connection type

SDS connection type is deprecated.
When editing SSIS package in SSIS Visual Studio extension, in Maintenance Tasks (Including Back Up Database Task, Check Database Integrity Task, Execute SQL Server Agent Job Task, Execute T-SQL Statement Task, History Cleanup Task, and Maintenance Cleanup Task) and For Each loop container (including ADO.NET Schema Rowset Enumerator and Foreach SMO Enumerator), you can explicitly choose this connection type "SqlClient Data Provider" when "New" a connection. The SDS connection type is deprecated. You're recommended to migrate to ADO.NET connection type.

## Features moved out of SQL Server 2025 Integration Services

The following section describes removed features in [!INCLUDE [sssql25-md](../includes/sssql25-md.md)] Integration Services.

### Change Data Capture (CDC) components by Attunity and Change Data Capture (CDC) service for Oracle by Attunity

Change Data Capture (CDC) components by Attunity and Change Data Capture (CDC) service for Oracle by Attunity were deprecated and are no longer available from [!INCLUDE [sssql25-md](../includes/sssql25-md.md)] Integration Services and onward. Details refer to [the announcement](https://www.microsoft.com/sql-server/blog/2024/02/28/sql-server-integration-services-ssis-change-data-capture-attunity-feature-deprecations).

### Microsoft Connector for Oracle

Microsoft Connector for Oracle was deprecated and are no longer available from [!INCLUDE [sssql25-md](../includes/sssql25-md.md)] Integration Services and onward. Details refer to [the announcement](https://www.microsoft.com/sql-server/blog/2025/01/21/sql-server-integration-services-ssis-microsoft-connector-for-oracle-deprecation).

### Hadoop related components

Hadoop Hive Task, Hadoop Pig Task, and Hadoop File System Task components are moved out of [!INCLUDE [sssql25-md](../includes/sssql25-md.md)] Integration Services and onward.

## Related content

- [Integration Services features supported by the editions of SQL Server](integration-services-features-supported-by-the-editions-of-sql-server.md)
