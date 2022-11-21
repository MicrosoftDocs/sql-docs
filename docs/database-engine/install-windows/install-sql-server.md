---
title: "SQL Server installation guide"
description: An index of content that helps you install SQL Server and associated components using options such as the installation wizard, command prompt, or sysprep.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: randolphwest
ms.date: 10/20/2022
ms.service: sql
ms.subservice: install
ms.topic: conceptual
helpviewer_keywords:
  - "AdventureWorks sample database"
  - "installing SQL Server, preparing to install"
  - "installation [SQL Server]"
monikerRange: ">=sql-server-2016"
---
# SQL Server installation guide

[!INCLUDE [SQL Server - Windows Only](../../includes/applies-to-version/sql-windows-only.md)]

This article is an index of content that provides guidance for installing [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] on Windows.

For other deployment scenarios, see:

- [Linux](../../linux/sql-server-linux-setup.md)
- [Docker containers](../../linux/sql-server-linux-docker-container-deployment.md)
- [Kubernetes - Big Data Clusters](../../big-data-cluster/deploy-get-started.md) ([!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] only)

Beginning with [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)], [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] is only available as a 64-bit application. Here are important details about how to get [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] and how to install it.

## Get started

- **Editions and features**: Review the supported features for the different editions and versions of [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] to determine which best suits your business needs.

  - [[!INCLUDE[sssql22-md](../../includes/sssql22-md.md)]](../../sql-server/editions-and-components-of-sql-server-2022.md)
  - [[!INCLUDE[sssql19-md](../../includes/sssql19-md.md)]](../../sql-server/editions-and-components-of-sql-server-2019.md)
  - [[!INCLUDE[sssql17-md](../../includes/sssql17-md.md)]](../../sql-server/editions-and-components-of-sql-server-2017.md)
  - [[!INCLUDE[sssql16-md](../../includes/sssql16-md.md)]](../../sql-server/editions-and-components-of-sql-server-2016.md)
  - [[!INCLUDE[ss2014](../../includes/sssql14-md.md)]](/previous-versions/sql/2014/getting-started/features-supported-by-the-editions-of-sql-server-2014)

- **Requirements**: Review hardware and software installation requirements for [SQL Server 2016 and SQL Server 2017](../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server.md), [SQL Server 2019](../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server.md), [SQL Server 2022](../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server.md), or [SQL Server on Linux](../../linux/sql-server-linux-setup.md), as well as system configuration checks, and security considerations in [Planning a SQL Server Installation](../../sql-server/install/planning-a-sql-server-installation.md)

- **Sample databases and sample code** aren't installed as part of [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] Setup by default, but can be installed for non-Express editions of [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)]. For more information, see [Microsoft SQL samples](../../samples/sql-samples-where-are.md).

## Installation media

The download location for [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] depends on the edition:

- **[!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] Enterprise, Standard, and Express editions** are licensed for production use. For the Enterprise and Standard Editions, contact your software vendor for the installation media. You can find purchasing information and a directory of Microsoft partners on the [Microsoft licensing page](https://www.microsoft.com/licensing/product-licensing/sql-server).
- If you have a volume licensing agreement, for example an [Enterprise Agreement](https://www.microsoft.com/licensing/licensing-programs/enterprise), you can download software from the [Volume Licensing Service Center (VLSC)](https://www.microsoft.com/licensing/servicecenter/default.aspx).
- [Free versions](https://www.microsoft.com/sql-server/sql-server-downloads).

Other [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] components can be found here:

- [All cumulative updates](https://sqlserverbuilds.blogspot.com/)
- [SQL Server Reporting Services](https://www.microsoft.com/download/details.aspx?id=100122).
- [SQL Server Management Studio](https://aka.ms/ssmsfullsetup)
- [Azure Data Studio](https://go.microsoft.com/fwlink/?linkid=2109256)

## Considerations

- Installation fails if you launch setup through Remote Desktop Connection with the media on a local resource in the RDC client. To install remotely the media must be on a network share or local to the physical or virtual machine. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installation media may be either on a network share, a mapped drive, a local drive, or presented as an ISO to a virtual machine.

- [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup installs the following software components required by the product:

  - [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client
  - [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup support files

## SQL Server installation

|Article|Description|
|-----------|-----------------|
|[Installation Wizard](install-sql-server-from-the-installation-wizard-setup.md)|Install [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] using the Installation Wizard GUI launched from the setup.exe setup media. |
|[Command Prompt](install-sql-server-from-the-command-prompt.md)|Sample syntax and installation parameters for running a [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] installation from the command prompt. |
|[Server Core](install-sql-server-on-server-core.md)|Install [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] on Windows Server Core.|
|[Check Parameters for the System Configuration Checker](check-parameters-for-the-system-configuration-checker.md)|Discusses the function of the System Configuration Checker (SCC).|
|[Configuration File](install-sql-server-using-a-configuration-file.md)|Sample syntax and installation parameters for running Setup through a configuration file.|
|[SysPrep](install-sql-server-using-sysprep.md)|Sample syntax and installation parameters for running Setup through SysPrep.|
|[Add Features to an Instance](add-features-to-an-instance-of-sql-server-setup.md)|Update components of an existing instance of [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)].|
|[SQL Server Failover Cluster Installation](../../sql-server/failover-clusters/install/sql-server-failover-cluster-installation.md)| Install a SQL Server failover cluster instance.  |
|[Repair a Failed SQL Server Installation](repair-a-failed-sql-server-installation.md)|Repair a corrupt [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] installation.|
|[Rename a computer with SQL Server](rename-a-computer-that-hosts-a-stand-alone-instance-of-sql-server.md)|Update system metadata that is stored in `sys.servers` after the hostname of a computer hosting a stand-alone instance of SQL Server has been renamed. |
|[Install SQL Server Servicing Updates](install-sql-server-servicing-updates.md)|Install updates for [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)].|
|[Setup Log Files](view-and-read-sql-server-setup-log-files.md)| View and read the errors in the SQL Server setup log files. |
|[Validate an Installation](validate-a-sql-server-installation.md)|Review the use of the SQL Discovery report to verify the version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] features installed on the computer.|

## Individual component installation

|Article|Description|
|-----------|-----------------|
|[SQL Server Database Engine](install-sql-server-database-engine.md)|Install and configure the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)].|
|[SQL Server Replication](install-sql-server-replication.md)|Install and configure [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Replication.|
|[Distributed Replay](../../tools/distributed-replay/install-distributed-replay.md)<sup>1</sup>|Lists articles to install the Distributed Replay feature.|
|[SQL Server Management Tools with SSMS](../../ssms/download-sql-server-management-studio-ssms.md)|Install and configure [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] management tools.|
|[SQL Server PowerShell](install-sql-server-powershell.md)|Considerations for installing [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] PowerShell components.|

<sup>1</sup> Distributed Replay is deprecated in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)].

## SQL Server configuration

|Article|Description|
|-----------|-----------------|
|[Configure Windows Firewall (SQL Server)](../../sql-server/install/configure-the-windows-firewall-to-allow-sql-server-access.md)|Overview of firewall configuration and how to configure the Windows firewall to allow access to [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)].|
|[Configure the Windows Firewall (SSAS)](/analysis-services/instances/configure-the-windows-firewall-to-allow-analysis-services-access)|Configure both port and firewall settings to allow access to [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] or [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for SharePoint.|
|[Configure a Multi-Homed Computer](../../sql-server/install/configure-a-multi-homed-computer-for-sql-server-access.md)|Configure [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and Windows Firewall with Advanced Security to provide for network connections to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in a multi-homed environment.|

## See also

- [Upgrade SQL Server](upgrade-sql-server.md)
- [Uninstall SQL Server](../../sql-server/install/uninstall-sql-server.md)
- [Install SQL Server Reporting Services (SSRS)](../../reporting-services/install-windows/install-reporting-services.md)
- [Install SQL Server Analysis  Services (SSAS)](/analysis-services/instances/install-windows/install-analysis-services)
- [Install SQL Server Business Intelligence Features](../../sql-server/install/install-sql-server-business-intelligence-features.md)
- [Business continuity and high availability solutions (SQL Server)](../sql-server-business-continuity-dr.md)
