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
| | SELECT dbo.sysjobactivity | msdb | SQLArceExtensionUserRole |
| | SELECT dbo.sysjobs | msdb | SQLArceExtensionUserRole |
| | SELECT dbo.syssessions | msdb | SQLArceExtensionUserRole |
| | SELECT dbo.sysjobHistory | msdb | SQLArceExtensionUserRole |
| | SELECT dbo.sysjobSteps | msdb | SQLArceExtensionUserRole |
| | SELECT dbo.syscategories | msdb | SQLArceExtensionUserRole |
| | SELECT dbo.sysoperators | msdb | SQLArceExtensionUserRole |
| | SELECT dbo.suspectpages | msdb | SQLArceExtensionUserRole |
| | SELECT dbo.backupset | msdb | SQLArceExtensionUserRole |
| | SELECT dbo.backupmediaset | msdb | SQLArceExtensionUserRole |
| | SELECT dbo.backupmediafamily | msdb | SQLArceExtensionUserRole |
| | SELECT dbo.backupfile | msdb | SQLArceExtensionUserRole |
| Backup | CREATE ANY DATABASE | Server Level | SQLArcExtensionServerRole |
| | db_backupoperator role | All databases | SQLArceExtensionUserRole |
| | dbcreator | Server Level | SQLArcExtensionServerRole |
| Azure Control Plane | CREATE TABLE | msdb | SQLArceExtensionUserRole |
| | ALTER ANY SCHEMA | msdb | SQLArceExtensionUserRole |
| | CREATE TYPE | msdb | SQLArceExtensionUserRole |
| | EXECUTE | msdb | SQLArceExtensionUserRole |
| | db_datawriter role | msdb | SQLArceExtensionUserRole |
| | db_datareader role | msdb | SQLArceExtensionUserRole |
| Availability Group Discovery | VIEW ANY DEFINITION | Server Level | SQLArcExtensionServerRole |
| Purview | SELECT | All databases | SQLArceExtensionUserRole |
| | EXECUTE | All databases | SQLArceExtensionUserRole |
| Migration Assessment | EXECUTE dbo.agent_datetime | msdb | SQLArceExtensionUserRole |
| | SELECT dbo.sysjobs | msdb | SQLArceExtensionUserRole |
| | SELECT dbo.sysmail_account | msdb | SQLArceExtensionUserRole |
| | SELECT dbo.sysmail_profile | msdb | SQLArceExtensionUserRole |
| | SELECT dbo.sysmail_profileaccount | msdb | SQLArceExtensionUserRole |
| | SELECT dbo.syssubsystems | msdb | SQLArceExtensionUserRole |
| | SELECT sys.sql_expression_dependencies | All databases | SQLArceExtensionUserRole |

## Run with least privilege

To run Azure extension for SQL Server with least privilege, follow the instructions at [Operate SQL Server enabled by Azure Arc with least privilege (preview)](configure-least-privilege.md).

At this time, the least privilege configuration is not the default.

## Related content

[Configure Windows service accounts and permissions](../../database-engine/configure-windows/configure-windows-service-accounts-and-permissions.md)
