---
title: Server level roles created by Azure extension for SQL Server
description: Explains server level roles create when you install Azure extension for SQL Server. Used by SQL Server enabled by Azure Arc
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: nikitatakru
ms.date: 02/06/2024
ms.topic: reference
---

# Roles created by Azure extension for SQL Server installation

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article lists the server and database roles and mappings that the installation of Azure extension for SQL Server creates.

## Roles

[!INCLUDE [arc-enabled-roles](../../includes/arc-enabled-roles.md)]

## Permissions

|  Feature | Permission | Level | Role |
|------ |------ |------ |------ |------ |------ |------ |------ |
| Default | VIEW SERVER STATE | Server Level | SQLArcExtensionServerRole |
| | CONNECT SQL | Server Level | SQLArcExtensionServerRole |
| | VIEW ANY DEFINITION | Server Level | SQLArcExtensionServerRole |
| | VIEW ANY DATABASE | Server Level | SQLArcExtensionServerRole |
| | CONNECT ANY DATABASE | Server Level | SQLArcExtensionServerRole |
| | SELECT dbo.sysjobactivity | msdb | SQLArcExtensionUserRole |
| | SELECT dbo.sysjobs | msdb | SQLArcExtensionUserRole |
| | SELECT dbo.syssessions | msdb | SQLArcExtensionUserRole |
| | SELECT dbo.sysjobHistory | msdb | SQLArcExtensionUserRole |
| | SELECT dbo.sysjobSteps | msdb | SQLArcExtensionUserRole |
| | SELECT dbo.syscategories | msdb | SQLArcExtensionUserRole |
| | SELECT dbo.sysoperators | msdb | SQLArcExtensionUserRole |
| | SELECT dbo.suspectpages | msdb | SQLArcExtensionUserRole |
| | SELECT dbo.backupset | msdb | SQLArcExtensionUserRole |
| | SELECT dbo.backupmediaset | msdb | SQLArcExtensionUserRole |
| | SELECT dbo.backupmediafamily | msdb | SQLArcExtensionUserRole |
| | SELECT dbo.backupfile | msdb | SQLArcExtensionUserRole |
| Backup | CREATE ANY DATABASE | Server Level | SQLArcExtensionServerRole |
| | db_backupoperator role | All databases | SQLArcExtensionUserRole |
| | dbcreator | Server Level | SQLArcExtensionServerRole |
| Azure Control Plane | CREATE TABLE | msdb | SQLArcExtensionUserRole |
| | ALTER ANY SCHEMA | msdb | SQLArcExtensionUserRole |
| | CREATE TYPE | msdb | SQLArcExtensionUserRole |
| | EXECUTE | msdb | SQLArcExtensionUserRole |
| | db_datawriter role | msdb | SQLArcExtensionUserRole |
| | db_datareader role | msdb | SQLArcExtensionUserRole |
| Availability Group Discovery | VIEW ANY DEFINITION | Server Level | SQLArcExtensionServerRole |
| Purview | SELECT | All databases | SQLArcExtensionUserRole |
| | EXECUTE | All databases | SQLArcExtensionUserRole |
| Migration Assessment | EXECUTE dbo.agent_datetime | msdb | SQLArcExtensionUserRole |
| | SELECT dbo.sysjobs | msdb | SQLArcExtensionUserRole |
| | SELECT dbo.sysmail_account | msdb | SQLArcExtensionUserRole |
| | SELECT dbo.sysmail_profile | msdb | SQLArcExtensionUserRole |
| | SELECT dbo.sysmail_profileaccount | msdb | SQLArcExtensionUserRole |
| | SELECT dbo.syssubsystems | msdb | SQLArcExtensionUserRole |
| | SELECT sys.sql_expression_dependencies | All databases | SQLArcExtensionUserRole |

## Run with least privilege

To run Azure extension for SQL Server with least privilege, follow the instructions at [Operate SQL Server enabled by Azure Arc with least privilege (preview)](configure-least-privilege.md).

At this time, the least privilege configuration is not the default.

## Related content

[Configure Windows service accounts and permissions](../../database-engine/configure-windows/configure-windows-service-accounts-and-permissions.md)
