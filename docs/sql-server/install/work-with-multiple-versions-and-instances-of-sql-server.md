---
title: "Work with Multiple Versions and Instances of SQL Server | Microsoft Docs"
ms.custom: ""
ms.date: "08/25/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: install
ms.topic: conceptual
helpviewer_keywords: 
  - "concurrent installations [SQL Server]"
  - "versions [SQL Server], multiple"
  - "side-by-side installations [SQL Server]"
  - "multiple SQL Server component versions"
  - "installing SQL Server, side-by-side installations"
  - "Setup [SQL Server], side-by-side installations"
  - "64-bit edition [SQL Server]"
  - "32-bit edition [SQL Server]"
  - "editions [SQL Server], side-by-side installations"
ms.assetid: 93acefa8-bb41-4ccc-b763-7801f51134e0
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Work with Multiple Versions and Instances of SQL Server

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

You can install multiple instances of SQL Server, or install SQL Server on a computer where earlier SQL Server versions are already installed.

The following SQL Server-related items are compatible with the installation of multiple instances on the same computer:

- Database Engine

- Analysis Services

::: moniker range="<=sql-server-2016||=sqlallproducts-allversions"
- Reporting Services
::: moniker-end

You can upgrade earlier versions of SQL Server on a computer where other SQL Server versions are already installed. For supported upgrade scenarios, see [Supported Version and Edition Upgrades](../../database-engine/install-windows/supported-version-and-edition-upgrades.md).
  
## Version Components and Numbering

 The following concepts are useful in understanding the behavior of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] for side-by-side instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].
  
 The standard product version format for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is MM.nn.bbbb.rr where each segment is defined as:
  
 MM - Major version  
  
 nn - Minor version  
  
 bbbb - Build number  
  
 rr - Build revision number  
  
 In each major or minor release of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], there is an increment to the version number to differentiate it from earlier versions. This change to the version is used for many purposes. This includes displaying version information in the user interface, controlling how files are replaced during upgrade, applying service packs, and also as a mechanism for functional differentiation between the successive versions.
  
### Components shared by all versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]

 Certain components are shared by all instances of all installed versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. When you install different versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] side by side on the same machine, these components are automatically upgraded to the latest version. Such components are usually uninstalled automatically when the last instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is uninstalled.
  
 Examples: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser and Microsoft [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] VSS Writer.
  
### Components shared across all instances of the same major version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]

[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] versions that have the same major version share some components across all instances. If the shared components are selected during upgrade, the existing components are upgraded to the latest version.
  
Examples: [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)], [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)], [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.
  
### Components shared across minor versions

[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] versions that have the same major.minor version shared components.
  
Example: Setup support files.
  
### Components specific to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]

Some [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] components or services are specific to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. These are also known as instance-aware. They share the same version as the instance that hosts them, and are used exclusively for that instance.
  
Examples: [!INCLUDE[ssDE](../../includes/ssde-md.md)], [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], and [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)].  
  
### Components that are independent of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] versions

Certain components are installed during [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] setup, but are independent of the versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. They may be shared across major versions or by all [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] versions.  

Examples: Microsoft Sync Framework, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Compact.  
  
For more information about [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Compact installation, see [Install SQL Server 2016 from the Installation Wizard &#40;Setup&#41;](../../database-engine/install-windows/install-sql-server-from-the-installation-wizard-setup.md). For more information about how to uninstall [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Compact, see [Uninstall an Existing Instance of SQL Server &#40;Setup&#41;](../../sql-server/install/uninstall-an-existing-instance-of-sql-server-setup.md).  
  
## Using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Side-By-Side with Previous Versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]

You can install [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on a computer that is already running instances of an earlier [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] version. If a default instance already exists on the computer, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] must be installed as a named instance.  
  
> [!CAUTION]  
> [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] SysPrep does not support side by side installation of prepared instances of [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] with earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on the same computer. For example, you cannot prepare a [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] instance side by side with a prepared instance of [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)]. However, you can install multiple prepared instances of the same major version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] side by side on the same computer. For more information, see [Considerations for Installing SQL Server Using SysPrep](../../database-engine/install-windows/considerations-for-installing-sql-server-using-sysprep.md).  
>
> [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] cannot be installed side-by-side with earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on a computer that is running Windows Server 2008 R2 Server Core SP1. For more information on Server Core installations, see [Install SQL Server 2016 on Server Core](../../database-engine/install-windows/install-sql-server-on-server-core.md).  
  
The following table shows side-by-side support for [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]:
  
|Existing instance of [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]|side by side support|  
|--------------------------------------------------|----------------------------|  
|[!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] (64-bit) [!INCLUDE[vcprx64](../../includes/vcprx64-md.md)]|[!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] (32-bit)<br /><br /> [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] (64-bit) [!INCLUDE[vcprx64](../../includes/vcprx64-md.md)]<br /><br /> [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] (32-bit)<br /><br /> [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] (64-bit) [!INCLUDE[vcprx64](../../includes/vcprx64-md.md)]<br /><br /> [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] (32-bit)<br /><br /> [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] (64-bit) [!INCLUDE[vcprx64](../../includes/vcprx64-md.md)]<br /><br /> [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] (32-bit)<br /><br /> [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] (64-bit) [!INCLUDE[vcprx64](../../includes/vcprx64-md.md)]<br /><br /> [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] (32-bit)<br /><br /> [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] (64-bit) [!INCLUDE[vcprx64](../../includes/vcprx64-md.md)] <br /><br /> [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)]|  

The following table shows side-by-side support for [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] with previous versions:

|Existing instance of [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)]|Side-by-side support for previous versions|  
|--------------------------------------------------|----------------------------|  
|[!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] [!INCLUDE[vcprx64](../../includes/vcprx64-md.md)]|[!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] (32-bit)<br /><br /> [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] (64-bit) [!INCLUDE[vcprx64](../../includes/vcprx64-md.md)]<br /><br /> [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] (32-bit)<br /><br /> [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] (64-bit) [!INCLUDE[vcprx64](../../includes/vcprx64-md.md)]<br /><br /> [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] (32-bit)<br /><br /> [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] (64-bit) [!INCLUDE[vcprx64](../../includes/vcprx64-md.md)]<br /><br /> [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] (32-bit)<br /><br /> [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] (64-bit) [!INCLUDE[vcprx64](../../includes/vcprx64-md.md)]<br /><br /> [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] (32-bit)<br /><br /> [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] (64-bit) [!INCLUDE[vcprx64](../../includes/vcprx64-md.md)]|  

## Preventing IP Address Conflicts

When a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Failover Cluster Instance is installed side by side with a standalone instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)], take care to avoid TCP port number conflicts on the IP addresses. Conflicts usually occur when two instances of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] are both configured to use the default TCP port (1433). To avoid conflicts, configure one instance to use a non-default fixed port. Configuring a fixed port is usually easiest on the standalone instance. Configuring the [!INCLUDE[ssDE](../../includes/ssde-md.md)] to use different ports will prevent an unexpected IP Address/TCP port conflict that blocks an instance startup when a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Failover Cluster Instance fails to the standby node.
  
## See Also

* [Hardware and Software Requirements for Installing SQL Server](../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server.md)
* [Install SQL Server from the Installation Wizard &#40;Setup&#41;](../../database-engine/install-windows/install-sql-server-from-the-installation-wizard-setup.md)
* [Supported Version and Edition Upgrades](../../database-engine/install-windows/supported-version-and-edition-upgrades.md)
* [Upgrade SQL Server](../../database-engine/install-windows/upgrade-sql-server.md)
* [Editions and supported features of SQL Server 2017](../../sql-server/editions-and-components-of-sql-server-2017.md)
* [Editions and supported features of SQL Server 2016](../../sql-server/editions-and-components-of-sql-server-2016.md)
* [Backward Compatibility_deleted](https://msdn.microsoft.com/library/15d9117e-e2fa-4985-99ea-66a117c1e9fd)