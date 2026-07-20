---
title: SQL Server Installation Guide
description: An index of content that helps you install SQL Server and associated components using options such as the installation wizard, command prompt, or sysprep.
author: rwestMSFT
ms.author: randolphwest
ms.date: 11/18/2025
ms.service: sql
ms.subservice: install
ms.topic: install-set-up-deploy
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "AdventureWorks sample database"
  - "installing SQL Server, preparing to install"
  - "installation [SQL Server]"
monikerRange: ">=sql-server-2017"
---
# SQL Server installation guide

[!INCLUDE [SQL Server - Windows Only](../../includes/applies-to-version/sql-windows-only.md)]

This article is an index of content that provides guidance for installing [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] on Windows.

For other deployment scenarios, see:

- [Installation guidance for SQL Server on Linux](../../linux/install-upgrade/setup.md)
- [Deploy and connect to SQL Server Linux containers](../../linux/containers/deploy.md)

Beginning with [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)], [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] is only available as a 64-bit application. Here are important details about how to get [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] and how to install it.

## Get started

> [!IMPORTANT]  
> Beginning with [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)], Data Quality Services (DQS), Master Data Services (MDS), Azure Synapse Link, and Reporting Services are removed. Azure Synapse Link and Reporting Services were deprecated in previous versions.

- **Editions and features**: Review the supported features for the different editions and versions of [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] to determine which best suits your business needs.

  - [SQL Server 2025](../../sql-server/editions-and-components-of-sql-server-2025.md)
  - [SQL Server 2022](../../sql-server/editions-and-components-of-sql-server-2022.md)
  - [SQL Server 2019](../../sql-server/editions-and-components-of-sql-server-2019.md)
  - [SQL Server 2017](../../sql-server/editions-and-components-of-sql-server-2017.md)
  - [SQL Server 2016](../../sql-server/editions-and-components-of-sql-server-2016.md)

- **Requirements**: Review hardware and software installation requirements:

  - [SQL Server 2025](../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server-2025.md)
  - [SQL Server 2022](../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server-2022.md)
  - [SQL Server 2019](../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server-2019.md)
  - [SQL Server 2017](../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server-2017.md)
  - [SQL Server on Linux](../../linux/install-upgrade/setup.md)

  You must also review system configuration checks, and security considerations in [Plan a SQL Server installation](../../sql-server/install/planning-a-sql-server-installation.md).

- **Sample databases and sample code** aren't installed as part of [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] Setup by default, but can be installed for non-Express editions of [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)]. For more information, see [SQL samples](../../samples/sql-samples-where-are.md).

## Installation media

The download location for [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] depends on the edition:

- **[!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] Enterprise, Standard, and Express editions** are licensed for production use. For the Enterprise and Standard Editions, contact your software vendor for the installation media. You can find purchasing information and a directory of Microsoft partners on the [Microsoft licensing page](https://www.microsoft.com/licensing/product-licensing/sql-server).
- If you have a volume licensing agreement, for example an [Enterprise Agreement](https://www.microsoft.com/licensing/licensing-programs/enterprise), you can download software from the [Microsoft 365 admin center](https://go.microsoft.com/fwlink/p/?linkid=2024339). The software's activation wizard automatically detects an embedded product key during installation.
- [Free versions](https://www.microsoft.com/sql-server/sql-server-downloads).

Other [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] components can be found here:

- [Latest updates and version history for SQL Server](/troubleshoot/sql/releases/download-and-install-latest-updates)
- [SQL Server Reporting Services](https://www.microsoft.com/download/details.aspx?id=104502).
- [SQL Server Management Studio](/ssms/sql-server-management-studio-ssms)
- [MSSQL extension for Visual Studio Code](../../tools/visual-studio-code-extensions/mssql/mssql-extension-visual-studio-code.md)

## Considerations

[!INCLUDE [upgrade-warning](includes/upgrade-warning.md)]

- Installation fails if you launch setup through Remote Desktop Connection with the media on a local resource in the RDC client. To install remotely the media must be on a network share or local to the physical or virtual machine. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] installation media could be either on a network share, a mapped drive, a local drive, or presented as an ISO to a virtual machine.

- [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Setup installs the following software components required by the product:

  - Microsoft ODBC Driver for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]
  - Microsoft OLE DB Driver for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]
  - [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Setup support files

## SQL Server installation

| Article | Description |
| --- | --- |
| [Install SQL Server from the Installation Wizard (Setup)](install-sql-server-from-the-installation-wizard-setup.md) | Install [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] using the Installation Wizard GUI launched from the setup.exe setup media. |
| [Install, configure, or uninstall SQL Server on Windows from the command prompt](install-sql-server-from-the-command-prompt.md) | Sample syntax and installation parameters for running a [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] installation from the command prompt. |
| [Install SQL Server on Server Core](install-sql-server-on-server-core.md) | Install [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] on Windows Server Core. |
| [Check parameters for the System Configuration Checker](check-parameters-for-the-system-configuration-checker.md) | Discusses the function of the System Configuration Checker (SCC). |
| [Install SQL Server using a configuration file](install-sql-server-using-a-configuration-file.md) | Sample syntax and installation parameters for running Setup through a configuration file. |
| [Slipstream installation for SQL Server](install-sql-server-using-slipstream.md) | Sample syntax and installation parameters for installing SQL Server with the latest cumulative update. |
| [Install SQL Server with SysPrep](install-sql-server-using-sysprep.md) | Sample syntax and installation parameters for running Setup through SysPrep. |
| [Add Features to an Instance of SQL Server (Setup)](add-features-to-an-instance-of-sql-server-setup.md) | Update components of an existing instance of [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)]. |
| [SQL Server failover cluster installation](../../sql-server/failover-clusters/install/sql-server-failover-cluster-installation.md) | Install a SQL Server failover cluster instance. |
| [Repair a failed SQL Server installation](repair-a-failed-sql-server-installation.md) | Repair a corrupt [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] installation. |
| [Rename a computer that hosts a stand-alone instance of SQL Server](rename-a-computer-that-hosts-a-stand-alone-instance-of-sql-server.md) | Update system metadata that is stored in `sys.servers` after the hostname of a computer hosting a stand-alone instance of SQL Server has been renamed. |
| [Install SQL Server servicing updates](install-sql-server-servicing-updates.md) | Install updates for [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)]. |
| [View and read SQL Server Setup log files](view-and-read-sql-server-setup-log-files.md) | View and read the errors in the SQL Server setup log files. |
| [Validate a SQL Server installation](validate-a-sql-server-installation.md) | Review the use of the SQL Discovery report to verify the version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] and the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] features installed on the computer. |

## Individual component installation

| Article | Description |
| --- | --- |
| [Install SQL Server Database Engine](install-sql-server-database-engine.md) | Install and configure the [[!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)]](../sql-database-engine.md). |
| [Install SQL Server replication](install-sql-server-replication.md) | Install and configure [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Replication. |
| [Install Distributed Replay](../../tools/distributed-replay/install-distributed-replay.md)<sup>1</sup> | Lists articles to install the Distributed Replay feature. |
| [SQL Server Management Tools](/ssms/install/install) | Install and configure [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] management tools. |
| [SQL Server PowerShell](/powershell/sql-server/download-sql-server-ps-module) | Considerations for installing [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] PowerShell components. |

<sup>1</sup> Distributed Replay is deprecated in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)].

## SQL Server configuration

| Article | Description |
| --- | --- |
| [Configure the Windows Firewall to allow SQL Server access](../../sql-server/install/configure-the-windows-firewall-to-allow-sql-server-access.md) | Overview of firewall configuration and how to configure the Windows Firewall to allow access to [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)]. |
| [Configure the Windows Firewall (SSAS)](/analysis-services/instances/configure-the-windows-firewall-to-allow-analysis-services-access) | Configure both port and firewall settings to allow access to [!INCLUDE [ssASnoversion](../../includes/ssasnoversion-md.md)] or [!INCLUDE [power-pivot-md](../../includes/power-pivot-md.md)] for SharePoint. |
| [Configure a multi-homed computer for SQL Server access](../../sql-server/install/configure-a-multi-homed-computer-for-sql-server-access.md) | Configure [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] and Windows Firewall with Advanced Security to provide for network connections to an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] in a multi-homed environment. |

## Related content

- [Upgrade SQL Server](upgrade-sql-server.md)
- [Uninstall SQL Server](../../sql-server/install/uninstall-sql-server.md)
- [Install and configure SQL Server Reporting Services](../../reporting-services/install-windows/install-reporting-services.md)
- [Install SQL Server Analysis  Services (SSAS)](/analysis-services/instances/install-windows/install-analysis-services)
- [Install SQL Server Business Intelligence Features](../../sql-server/install/install-sql-server-business-intelligence-features.md)
- [Business continuity and database recovery - SQL Server](../sql-server-business-continuity-dr.md)
