---
title: "Database Engine: Breaking changes"
titleSuffix: "SQL Server 2017"
description: "Learn about changes that might break applications, scripts, or functionalities that are based on earlier versions of SQL Server."
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 08/02/2024
ms.service: sql
ms.subservice: release-landing
ms.topic: conceptual
helpviewer_keywords:
  - "breaking changes 2017 [SQL Server]"
monikerRange: ">=sql-server-2017 || >=sql-server-linux-2017"
---
# Breaking changes to Database Engine features in SQL Server 2017

[!INCLUDE [SQL Server 2017](../includes/applies-to-version/sqlserver2017.md)]

This article describes breaking changes in the [!INCLUDE [sssql17-md](../includes/sssql17-md.md)] [!INCLUDE [ssDE](../includes/ssde-md.md)]. These changes might break applications, scripts, or functionalities that are based on earlier versions of [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)]. You might encounter these issues when you upgrade.

## Breaking changes in SQL Server 2017 Database Engine

[!INCLUDE [code-access-security](includes/code-access-security.md)]

The MD2, MD4, MD5, SHA, and SHA1 algorithms are deprecated in [!INCLUDE [sssql15-md](../includes/sssql16-md.md)]. Up to [!INCLUDE [sssql15-md](../includes/sssql16-md.md)], a self-signed certificate is created using SHA1. Starting with [!INCLUDE [ssSQL17](../includes/sssql17-md.md)], a self-signed certificate is created using SHA2_256.

## Previous versions

- [Breaking changes to Database Engine features in SQL Server 2016](breaking-changes-to-database-engine-features-in-sql-server-2016.md)
- [Breaking Changes to Database Engine Features in SQL Server 2014](/previous-versions/sql/2014/database-engine/breaking-changes-to-database-engine-features-in-sql-server-2016?view=sql-server-2014&preserve-view=true#SQL14)

#### Archived documentation for old versions of SQL Server

[!INCLUDE [previous-versions-archive-documentation-sql-server](includes/previous-versions-archive-documentation-sql-server.md)]

## Related content

- [Deprecated Database Engine features in SQL Server 2016 (13.x)](deprecated-database-engine-features-in-sql-server-2016.md)
- [Discontinued Database Engine functionality in SQL Server](discontinued-database-engine-functionality-in-sql-server.md)
- [ALTER DATABASE (Transact-SQL) compatibility level](../t-sql/statements/alter-database-transact-sql-compatibility-level.md)
