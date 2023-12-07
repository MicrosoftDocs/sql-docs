---
title: Release notes
description: Latest release notes for SQL Server enabled by Azure Arc
author: anosov1960
ms.author: sashan
ms.reviewer: mikeray, randolphwest
ms.date: 12/01/2023
ms.topic: conceptual
ms.custom: ignite-2023
---

# Release notes - SQL Server enabled by Azure Arc

[!INCLUDE [sqlserver](../../includes/applies-to-version/sqlserver.md)]

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

- Manage Always On availability group - manual failover - public preview.
  - Perform a planned, manual failover on an availability group replica, using Azure portal.
- Availability group status - Track the availability upload status | public preview.
  - Beginning with this release, track the status and see the last time that the availability group inventory data is updated.  The portal shows two new properties, **Upload status** and **Last collected time** in the **Availability Groups** tab of the Arc-enabled SQL Server.

For more information, review [Always On availability groups inventory and status](manage-availability-group.md).

##### Networking

- Support for separate proxy bypass value for Arc SQL Server only - general availability.

For information, review [Proxy bypass for private endpoints](/azure/azure-arc/servers/manage-agent#proxy-bypass-for-private-endpoints).

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

- Automatic patching generally available

## August 10, 2023

**Extension version**: `1.1.2406.45`

### Azure extension for SQL Server

- Support to automatically rotate certificates for Microsoft Entra ID authentication. Review [Rotate certificates](rotate-certificates.md).

   [!INCLUDE [entra-id](../../includes/entra-id.md)]

- Configure patching in Azure portal. Review [Configure in the Azure portal](patch.md#configure-in-the-azure-portal).

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
- Fixed a bug where the exclude instance setting in portal throws an incorrect error about excluding SQL instance named separated by space.
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
- Support for Microsoft Update to patch an Arc-enabled SQL Server. Automatically installs updates marked as Critical or Important. Doesn't automatically install other updates.

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

## November 13, 2022

**Extension version**: `1.1.2132.21`

> [!IMPORTANT]
> Billing for this extension will not be enabled until December 1, 2022.

### Azure extension for SQL Server

- Both Linux (`LinuxAgent.SqlServer`) and Windows (`WindowsAgent.SqlServer`) versions of Azure extension for SQL Server now support billing through Azure when pay-as-you-go activation is selected in [SQL 2022[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] setup wizard](../../database-engine/install-windows/install-sql-server-from-the-installation-wizard-setup.md) or [command prompt](../../database-engine/install-windows/install-sql-server-from-the-command-prompt.md).
- [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] includes the pay-as-you-go activation option that forces the installation of Azure extension for SQL Server during setup.
- Billing meters are introduced to support pay-as-you-go billing through Azure
- SQL Server Azure Arc instance now shows the databases registered to the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance. To view databases, navigate to the data management tab of the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Azure Arc resource. For more details, review [View databases](view-databases.md).

### Other changes

The *LicenseType* property of `SQL Server - Azure Arc` has been extended to provide more granular license information. The values now include:

| **Value** | **Description** |
|:--|:--|
|Paid|SQL Server instance is installed using a product key with Software Assurance or SQL subscription|
|LicenseOnly|SQL Server instance is installed using a product key without Software Assurance or SQL subscription|
|PAYG|SQL Server instance is installed using a pay-as-you-go activation option (new in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)])|
|Free|Indicates that the instance uses Evaluation or Developer edition of SQL Server|
|HADR|Indicates that the instance is a replica in an availability group. If it's covered by Software Assurance, it might not require a license. For more information, review [SQL Server Commercial Licensing Terms](https://www.microsoft.com/licensing/terms/productoffering/SQLServer/EAEAS).|

### Known issues and limitations

- Feature use requires Azure extension for SQL Server version `v1.1.2132.21` or higher.
- The pay-as-you-go billing is limited to SQL Server 2022[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)]
- Azure extension for SQL Server is not supported in the following environments:
  - SQL Server in Azure VMs. If a custom VM image is migrated to Azure VM, Azure extension for SQL Server will stop working. Delete your Arc-enabled SQL Server resource and enable automatic registration with SQL IaaS Agent extension. (This step is no longer required, beginning with April, 2023 release.)
  - SQL Server in Linux containers
  - SQL Server Azure VMware Solution
> [!NOTE]
> Azure extension for SQL Server fully supports VMware clusters outside of Azure.

## October 12, 2022

**Extension version**: `1.1.2104.14`

### Azure extension for SQL Server

SQL Server Onboarding Role is no longer needed for onboarding SQL servers onto Azure Arc.
