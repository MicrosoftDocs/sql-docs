---
title: "Database Engine: Breaking changes"
titleSuffix: "SQL Server 2017"
description: "Learn about changes that might break applications, scripts, or functionalities that are based on earlier versions of SQL Server."
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 07/25/2022
ms.service: sql
ms.subservice: release-landing
ms.topic: conceptual
ms.custom: seo-lt-2019
helpviewer_keywords:
  - "breaking changes 2017 [SQL Server]"
monikerRange: ">=sql-server-2017||>=sql-server-linux-2017"
---
# Breaking changes to Database Engine features in [!INCLUDE[sssql17-md](../includes/sssql17-md.md)]

[!INCLUDE[SQL Server 2017](../includes/applies-to-version/sqlserver2017.md)]

This article describes breaking changes in the [!INCLUDE[sssql17-md](../includes/sssql17-md.md)] [!INCLUDE[ssDE](../includes/ssde-md.md)]. These changes might break applications, scripts, or functionalities that are based on earlier versions of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. You might encounter these issues when you upgrade.

## Breaking changes in [!INCLUDE[sssql17-md](../includes/sssql17-md.md)] [!INCLUDE[ssDE](../includes/ssde-md.md)]

- CLR uses Code Access Security (CAS) in the .NET Framework, which is no longer supported as a security boundary. Beginning with [!INCLUDE[sssql17-md](../includes/sssql17-md.md)][!INCLUDE[ssDE](../includes/ssde-md.md)], an `sp_configure` option called `clr strict security` is introduced to enhance the security of CLR assemblies. clr strict security is enabled by default, and treats `SAFE` and `EXTERNAL_ACCESS` CLR assemblies as if they were marked `UNSAFE`. The `clr strict security` option can be disabled for backward compatibility, but this isn't recommended. When `clr strict security` is disabled, a CLR assembly created with `PERMISSION_SET = SAFE` may be able to access external system resources, call unmanaged code, and acquire **sysadmin** privileges. After enabling strict security, any assemblies that aren't signed will fail to load. Also, if a database has `SAFE` or `EXTERNAL_ACCESS` assemblies, `RESTORE` or `ATTACH DATABASE` statements can complete, but the assemblies may fail to load.

  To load the assemblies, you must either alter or drop and recreate each assembly so that it's signed with a certificate or asymmetric key that has a corresponding login with the `UNSAFE ASSEMBLY` permission on the server. For more information, see [CLR strict security](../database-engine/configure-windows/clr-strict-security.md).

- The MD2, MD4, MD5, SHA, and SHA1 algorithms are deprecated in [!INCLUDE[sssql15-md](../includes/sssql16-md.md)]. Up to [!INCLUDE[sssql15-md](../includes/sssql16-md.md)], a self-signed certificate is created using SHA1. Starting with [!INCLUDE[ssSQL17](../includes/sssql17-md.md)], a self-signed certificate is created using SHA2_256.

## Previous versions

- [Breaking Changes to Database Engine Features in SQL Server 2016](../database-engine/breaking-changes-to-database-engine-features-in-sql-server-2016.md)

- [Breaking Changes to Database Engine Features in SQL Server 2014](/previous-versions/sql/2014/database-engine/breaking-changes-to-database-engine-features-in-sql-server-2016?view=sql-server-2014&preserve-view=true#SQL14)

#### Archived documentation for old versions of SQL Server

[!INCLUDE[Archived documentation for old versions of SQL Server](../includes/paragraph-content/previous-versions-archive-documentation-sql-server.md)]

## See also

- [Deprecated Database Engine Features in SQL Server 2016](../database-engine/deprecated-database-engine-features-in-sql-server-2016.md)
- [Discontinued Database Engine Functionality in SQL Server 2016](./discontinued-database-engine-functionality-in-sql-server.md)
- [SQL Server Database Engine Backward Compatibility](./discontinued-database-engine-functionality-in-sql-server.md)
- [ALTER DATABASE Compatibility Level &#40;Transact-SQL&#41;](../t-sql/statements/alter-database-transact-sql-compatibility-level.md)
