---
title: Release notes
description: Latest release notes for SQL Server enabled by Azure Arc
author: anosov1960
ms.author: sashan
ms.reviewer: mikeray, randolphwest
ms.date: 07/09/2024
ms.topic: conceptual
ms.custom: ignite-2023
---

# Release notes - SQL Server enabled by Azure Arc

[!INCLUDE [sqlserver](../../includes/applies-to-version/sqlserver.md)]

This article lists:

- Release dates
- Extension version numbers
- High level feature descriptions
- Links to additional feature documentation

Extension versions are cumulative. Higher extension versions include all of the updates from previous versions. A release may include internal features. If the version release notes don't describe features, then the updates were internal.

## July  09, 2024

**Extension version**: `1.1.2735.199`

### Extended Security Updates enabled for SQL Server 2014

Extended Security Updates (ESU) subscription for [!INCLUDE [sssql14-md](../../includes/sssql14-md.md)] will automatically start billing when SQL Server 2014 ESU program starts. Requires [!INCLUDE [sssql14-md](../../includes/sssql14-md.md)] be enabled for ESU.

### Extended Security Updates using unlimited virtualization

Extended Security Updates (ESU) subscription for [!INCLUDE [sssql11-md](../../includes/sssql11-md.md)] and [!INCLUDE [sssql14-md](../../includes/sssql14-md.md)] can be activated using ESU p-core license with unlimited virtualization. For details, see [Manage unlimited virtualization benefit for SQL Server ESU subscription](manage-configuration.md#manage-pcore-esu-license).

> [!NOTE]
> Billing for the ESU p-core licenses will be activated in the next monthly release, but the full ESU costs will be reflected using a back-bill meter.

### Azure extension for SQL Server

`SqlServerExtensionPermissionProvider` task no longer runs hourly. The task is triggered by specific events. For details, review [Roles](permissions-granted-agent-extension.md#roles). 

## June  14, 2024

**Extension version**: `1.1.2717.190`

### Licensing and billing

Support Extended Security Updates (ESU) subscription for [!INCLUDE [sssql11-md](../../includes/sssql11-md.md)] with an option to bill back to year 1 of extended support (for managed accounts only).

### Business continuity

- Inventory and manual failover of availability groups is now generally available. For details, review [Always On availability groups inventory and status](manage-availability-group.md).
- Inventory of failover cluster instance is now generally available. Failover cluster instance in portal now shows instance name, instance type, network name, active node, and passive nodes. For details, review [View Always On failover cluster instances in Azure Arc](support-for-fci.md).

### Migration

Run assessment on demand (preview): The SQL Server migration assessment runs every Sunday around 11:00 PM. Beginning with this release, you can initiate the SQL Server migration assessment whenever you want. This immediate assessment shows readiness evaluations and Azure SQL configuration assessments right away. For details, review [Assess migration readiness](migration-assessment.md).

## May 15, 2024

**Extension version**: `1.1.2689.159`

This version reintroduces features previously released in version `1.1.2656.138` (April 9, 2024).

### Move resources

- Move instances or databases to a different subscription or resource group (preview). Review [Move SQL Server enabled by Azure Arc resources to a new resource group or subscription - preview](move-resources.md).

### Updated licensing and configuration support

- Support licensing SQL Server by physical cores with unlimited virtualization
- Support ESU subscriptions for [!INCLUDE [sssql14-md](../../includes/sssql14-md.md)]
- Database inventory feature for all license types

For details, review:

- [Manage licensing and billing of SQL Server enabled by Azure Arc](manage-license-billing.md).
- [Configure SQL Server enabled by Azure Arc](manage-configuration.md)

### Updated performance dashboard

- New built-in role and action available to manage access to the performance dashboard
  - Review [Monitor [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)] (preview)](sql-monitoring.md).

## April 16, 2024

**Extension version**: `1.1.2656.138`

 This version is no longer available.

## April 9, 2024

**Extension version**: `1.1.2647.136`

 This version is no longer available. The features in this version are updated and released in [May 15, 2024](#may-15-2024).

## March 12, 2024

**Extension version**: `1.1.2620.127`

### Backup management

- Schedule backup at instance level or database level. For details, review [Backup schedule level](backup-local.md#backup-schedule-level).

### Streamlined network endpoints

Prior to this release, Azure Arc data processing endpoint was at `san-af-<region>-prod.azurewebsites.net`.

Beginning with this release both Azure Arc data processing, and Azure Arc data telemetry use `*.<region>.arcdataservices.com`.

## February 13, 2024

**Extension version**: `1.1.2594.118`

### Azure SQL migration readiness assessment (preview)

SQL Server enabled by Azure Arc automatically generates Azure SQL migration assessments.  

A migration assessment:

- Evaluates the readiness of your SQL Server instances for migration to Azure SQL
- Recommends the optimal size for the Azure SQL destination
- Identifies any migration blockers or issues that you need to resolve before moving to Azure SQL
- Provides step-by-step guidance on how to mitigate any issues

Use migration assessments to ensure a successful migration.

For details, review [View SQL Server migration assessment - SQL Server enabled by Azure Arc](migration-assessment.md).

### Create Azure SQL Managed Instance

You can create an Azure SQL Managed Instance from the portal. Available from Azure Arc | SQL Server instances in the portal.

### Additional feature updates for SQL Server enabled by Azure Arc

- Support for TLS 1.3
- Improved prompt for feedback in Azure portal
- Monitoring | Show monitoring upload status on Arc SQL Server overview in portal
- For SQL Server 2012, updates through Microsoft Update are automatically applied if Extended Security Updates (ESU) is enabled

#### Region availability

The following regions are now available for SQL Server enabled by Azure Arc, and Data Services enabled by Azure Arc:

- Sweden Central
- Norway East
- UK West

For a complete list of regions, see [Supported Azure regions](overview.md#supported-azure-regions).

## January 16, 2024

**Extension version**: `1.1.2566.109`

## December 12, 2023

**Extension version**: `1.1.2526.108`

### Azure extension for SQL Server

* Data processing service (DPS) connectivity available in Azure portal
* Performance dashboard shows
  * IOPS
  * Queue latency storage IO

  For information, review [Storage I/O](sql-monitoring.md#storage-io).

## December 1, 2023

**Extension version**: `1.1.2512.104`

## November 14, 2023

**Extension version**: `1.1.2504.99`

### Azure extension for SQL Server

#### Setup

- Track the provision state and (extension service) status of Azure extension for SQL Server - general availability.
  - Beginning with this release, you can track the provisioning status of Azure Arc extension for SQL Server and Azure Arc guest agent in the properties tab for Arc enabled SQL Server.

For information about server monitoring capabilities, review [Automatically connect your SQL Server to Azure Arc](automatically-connect.md).

For information about database status and inventory capabilities, review [View SQL Server databases - Azure Arc](view-databases.md).

Creates a server role and a database role, maps logins, and grants permissions. For details, see [Roles created by Azure Extension for SQL Server installation](permissions-granted-agent-extension.md).

#### Back up and restore

- Configure backups at instance level using custom schedule for [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)] instances for both portal and CLI - public preview.
  - Configure Automated Backups with a custom schedule and custom retention period, on an Arc enabled SQL Server.
  
  For more information, review [Manage automated backups - [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)]](backup-local.md).

- Point-in-time-restore using Azure CLI and Azure portal - public preview.
  - Restore a database to a point-in-time restore of their databases, if automatic backups are enabled. Restore can be done either from Azure portal or via az CLI.

  For more information, review [Restore to a point-in-time](point-in-time-restore.md).

#### Monitoring

- Performance dashboards of an individual [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)] instance in the Azure portal - public preview.

For more information, review [Monitor [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)]](sql-monitoring.md).

#### High availability

- Manage Always On availability group - manual failover - preview.
  - Perform a planned, manual failover on an availability group replica, using Azure portal.
- Availability group status - Track the availability upload status | public preview.
  - Beginning with this release, track the status and see the last time that the availability group inventory data is updated.  The portal shows two new properties, **Upload status** and **Last collected time** in the **Availability Groups** tab of the Arc-enabled SQL Server.

For more information, review [Always On availability groups inventory and status](manage-availability-group.md).

##### Networking

- Support for separate proxy bypass value for Arc SQL Server only - general availability.

For information, review [Proxy bypass for private endpoints](/azure/azure-arc/servers/manage-agent#proxy-bypass-for-private-endpoints).

##### Least privilege

Support for least privilege available for Arc SQL Server only - preview.

With SQL Server enabled by Azure Arc, you can run the agent extension service with least privilege. To configure the service to run with least privilege, follow the steps in this article [Configure least privilege](configure-least-privilege.md).

## October 13, 2023

**Extension version**: `1.1.2474.69` - Enables failover cluster instance discovery.

### Azure extension for SQL Server

- Enable extended support updates (ESU) for failover clusters (general availability).

- [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)] failover cluster (preview).

  - Features include:
    - Render failover cluster instances in Azure portal.
    - Inventory databases
    - Defender

  For details, see [View Always On failover cluster instances in Azure Arc](support-for-fci.md).

## October 10, 2023

**Extension version**: `1.1.2467.69`

## September 19, 2023

**Extension version**: `1.1.2451.59`

## September 13, 2023

**Extension version**: `1.1.2440.53`

### Azure extension for SQL Server

- Automatic updates generally available

## August 10, 2023

**Extension version**: `1.1.2406.45`

### Azure extension for SQL Server

- Support to automatically rotate certificates for Microsoft Entra ID authentication. Review [Rotate certificates](rotate-certificates.md).

   [!INCLUDE [entra-id](../../includes/entra-id.md)]

- Configure automatic updates in Azure portal. Review [Configure in the Azure portal](update.md#configure-in-the-azure-portal).

## July 13, 2023

**Extension version**: `1.1.2384.34`

### Azure extension for SQL Server

- Proxy bypass is now supported for Arc SQL Server Extension. Starting this release, you can also specify services which should not use the specified proxy server. For examples and technical information, see [Proxy bypass for private endpoints](/azure/azure-arc/servers/manage-agent?tabs=windows#proxy-bypass-for-private-endpoints).

## June 13, 2023

**Extension version**: `1.1.2355.20`

### Azure extension for SQL Server

- Support for being able to view your SQL Server configuration is now available. See [Manage SQL Server license and billing options](manage-configuration.md).
  - View SQL Server instances
  - Modify host level properties like license type
  - Subscribe to Extended Security Update (ESU)
  - Skip instances

## May 09, 2023

**Extension version**: `1.1.2313.14`

### Azure extension for SQL Server

- Support for automated backups for all supported versions of SQL Server. For information, review [Configure automatic backups](point-in-time-restore.md).
- Provide a name for the server that hosts a [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)] instance when you enable SQL Server for Azure Arc. Use parameter `--machineName <"ArcServerName">`. For information, see [Connect your SQL Server to Azure Arc with installer (.msi)](connect-with-installer.md), or [Connect SQL Server machines at scale with a Configuration Manager custom task sequence](onboard-configuration-manager-custom-task.md).

### Additional improvements

- Arc virtual machines on-boarded from AWS use AWS resource name rather than host computer name. Fix allows customers to provide a name when onboarding using script.
- Fixed a bug where the setting to exclude instances in portal throws an incorrect error about excluding SQL instance name separated by space.
- Fixed a bug where the Purview governance status does not report correctly upon on-demand refresh intermittently.

## April 10, 2023

**Extension version**: `1.1.2284.7`

> [!NOTE]
> This release is only available for SQL Server on Windows. This release is not available for SQL Server on Linux.

### Azure extension for SQL Server

- Backups | Configure Automatic Backups for Arc SQL Server with a default schedule.
- Automatic built-in backups with default schedule of weekly full, daily diff, and transaction logs every 5 min for every database.
- Configure backup file retention with the `--retention-days` parameter. Values from 0 to 35 days. Default is 0 days.
- Azure Policy to enable best practices assessment at scale. For details, see [Configure SQL best practices assessment](assess.md).

## March 9, 2023

**Extension version**: `1.1.2256.66`

### Azure extension for SQL Server

- [Best practices assessment](assess.md) supports multiple instances of SQL Server on a physical or virtual server host.
- Support for Microsoft Update to update an Arc-enabled SQL Server. Automatically installs updates marked as Critical or Important. Doesn't automatically install other updates.

## February 17, 2023

**Extension version**: `1.1.2231.59`

### Azure extension for SQL Server

- [Best practices assessment](assess.md) (BPA) on Arc-enabled SQL Server requires Software Assurance or [Azure pay-as-you-go license](manage-configuration.md).
- [Viewing SQL Server databases - Azure Arc](view-databases.md) on Arc-enabled SQL Server requires Software Assurance or [Azure pay-as-you-go license](manage-configuration.md).

## January 17, 2023

**Extension version**: `1.1.2202.47`

### Azure extension for SQL Server

- [Best practices assessment](assess.md) (BPA) on Arc-enabled SQL Server

  The SQL best practices assessment feature of the Azure portal:

  - Identifies possible performance issues
  - Evaluates that your [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)] is configured to follow best practices
  - Provides comprehensive mitigation guidance
  - To experience best practices assessment, upgrade to the latest extension version.
- Azure Arc-enabled Database resource populates the earliest restore time and last backup time for database resources as a resource
- Pay-as-you-go (PAYG) licensing option is now extended to SQL Server 2012 and above
- Ability to set the licensing type during onboarding Arc-enabled SQL Server.
- Ability to skip instances during onboarding to Azure

## December 13, 2022

**Extension version**: `1.1.2167.41`

### Azure extension for SQL Server

- Support to view databases as a resource added for [!INCLUDE [sssql14-md](../../includes/sssql14-md.md)], and [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)]. Review [View SQL Server databases - Azure Arc](view-databases.md).
- Support for Web and Express editions.


