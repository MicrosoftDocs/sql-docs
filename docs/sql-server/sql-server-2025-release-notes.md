---
title: SQL Server 2025 Release Notes
description: Find information about SQL Server 2025 (17.x) limitations, known issues, help resources, and other release notes.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: randolphwest
ms.date: 11/18/2025
ms.service: sql
ms.subservice: release-landing
ms.topic: release-notes
ms.custom:
  - ignite-2025
monikerRange: ">=sql-server-2017"
---

# SQL Server 2025 release notes

[!INCLUDE [sqlserver2025](../includes/applies-to-version/sqlserver2025.md)]

This article describes requirements, and limitations for [!INCLUDE [sssql25-md](../includes/sssql25-md.md)].

## Upgrade

This release of [!INCLUDE [sssql25-md](../includes/sssql25-md.md)] supports upgrading from previous versions of SQL Server. The operating system environment must meet the requirements in [Hardware and software requirements for SQL Server 2025](install/hardware-and-software-requirements-for-installing-sql-server-2025.md).

## Preview features

Explore preview development features with the new `PREVIEW_FEATURES` database scoped configuration. This configuration lets you try select features in preview even after SQL Server reaches general availability. These features will become generally available in future cumulative updates. When a cumulative update provides general availability for a feature, the database-scoped configuration is no longer necessary for that feature.

To use these features, enable the `PREVIEW_FEATURES` [database scoped configuration](../t-sql/statements/alter-database-scoped-configuration-transact-sql.md#preview-features).

[!INCLUDE [preview-features](../includes/paragraph-content/preview-features.md)]

The status of all other features described in the [What's new in SQL Server 2025](what-s-new-in-sql-server-2025.md) article is aligned with the release status of [!INCLUDE [sssql25-md](../includes/sssql25-md.md)]. They don't require enabling the preview feature database scoped configuration.

For more information, review [Opt in for preview features - FAQ](preview-features-faq.md).

## Breaking changes

[!INCLUDE [sssql25-md](../includes/sssql25-md.md)] introduces breaking changes to a few [!INCLUDE [ssde-md](../includes/ssde-md.md)] features. To learn more, review [Breaking changes to Database Engine features in SQL Server 2025](../database-engine/breaking-changes-to-database-engine-features-in-sql-server-2025.md).

## Build number

| Build | Version number | Date |
| --- | --- | --- |
| Generally available (GA) | 17.0.1000.7 | November 18, 2025 |

## Related content

- [What's new in SQL Server 2025](what-s-new-in-sql-server-2025.md)
- [SQL Server 2025 known issues](sql-server-2025-known-issues.md)
- [Hardware and software requirements for SQL Server 2025](install/hardware-and-software-requirements-for-installing-sql-server-2025.md)
