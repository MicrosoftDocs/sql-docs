---
title: "Work with multiple versions and instances"
description: You can install multiple instances of SQL Server or install SQL Server on a computer where earlier SQL Server versions are already installed.
author: rwestMSFT
ms.author: randolphwest
ms.date: 09/27/2023
ms.service: sql
ms.subservice: install
ms.custom: linux-related-content
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
---
# Work with multiple versions and instances of SQL Server

[!INCLUDE [SQL Server -Windows Only](../../includes/applies-to-version/sql-windows-only.md)]

You can install multiple instances of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], or install [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] on a computer where earlier [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] versions are already installed.

> [!NOTE]  
> If you need to install multiple instances of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] on Linux, see [Deploy and connect to SQL Server Linux containers](../../linux/sql-server-linux-docker-container-deployment.md).

The following [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]-related items are compatible with the installation of multiple instances on the same computer:

- Database Engine
- Analysis Services
- Reporting Services <sup>1</sup>

<sup>1</sup> Starting with [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)], [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Reporting Services (SSRS) is installed separately.

You can upgrade earlier versions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] on a computer where other [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] versions are already installed. For supported upgrade scenarios, see [Supported Version and Edition Upgrades](../../database-engine/install-windows/supported-version-and-edition-upgrades.md).

## Version components and numbering

The following concepts are useful in understanding the behavior of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] for side-by-side instances of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

The standard product version format for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is `MM.nn.bbbb.rr`, where each segment is defined as:

- `MM` - Major version  
- `nn` - Minor version  
- `bbbb` - Build number  
- `rr` - Build revision number

In each major or minor release of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], there's an increment to the version number to differentiate it from earlier versions. This change to the version is used for many purposes, including:

- displaying version information in the user interface
- controlling how files are replaced during upgrade
- applying service packs and cumulative updates
- as a mechanism for functional differentiation between the successive versions

### Components shared by all versions of SQL Server

Certain components are shared by all instances of all installed versions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. When you install different versions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] side by side on the same machine, these components are automatically upgraded to the latest version. Such components are usually uninstalled automatically when the last instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is uninstalled.

Examples: [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Browser and Microsoft [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] VSS Writer.

### Components shared across all instances of the same major version of SQL Server

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] versions that have the same major version share some components across all instances. If the shared components are selected during upgrade, the existing components are upgraded to the latest version.

Examples: [!INCLUDE [ssISnoversion](../../includes/ssisnoversion-md.md)], [!INCLUDE [ssMDSshort](../../includes/ssmdsshort-md.md)], [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)], [!INCLUDE [ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], and [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.

### Components shared across minor versions

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] versions that have the same major.minor version shared components.

Example: Setup support files.

### Components specific to an instance of SQL Server

Some [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] components or services are specific to an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] (instance-aware). They share the same version as the instance that hosts them, and are used exclusively for that instance.

Examples: [!INCLUDE [ssDE](../../includes/ssde-md.md)], [!INCLUDE [ssASnoversion](../../includes/ssasnoversion-md.md)], and [!INCLUDE [ssRSnoversion](../../includes/ssrsnoversion-md.md)].

[!INCLUDE [editions-supported-features-windows](../../includes/editions-supported-features-windows.md)]

[!INCLUDE [editions-supported-features-linux](../../includes/editions-supported-features-linux.md)]

### Components that are independent of the SQL Server versions

Certain components are installed during [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] setup, but are independent of the versions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. They may be shared across major versions or by all [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] versions.

Examples: Microsoft Sync Framework, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Compact.

For more information about [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Compact installation, see [Install SQL Server from the Installation Wizard (Setup)](../../database-engine/install-windows/install-sql-server-from-the-installation-wizard-setup.md). For more information about how to uninstall [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Compact, see [Uninstall an existing instance of SQL Server (Setup)](uninstall-an-existing-instance-of-sql-server-setup.md).

## Use SQL Server side-by-side with previous versions of SQL Server

You can install [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] on a computer that is already running instances of an earlier [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] version. If a default instance already exists on the computer, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] must be installed as a named instance.

The following table shows side-by-side support for each version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] on commonly supported versions of Windows with required versions of .NET installed:

| Existing instance | Side by side support |
| --- | --- |
| [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] | [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)] through [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] |
| [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] | [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)] through [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] |
| [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] | [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)] through [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] |
| [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] | [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)] through [!INCLUDE [sssql14-md](../../includes/sssql14-md.md)] |

For more information, see [Using SQL Server in Windows 8 and later](https://support.microsoft.com/help/2681562/).

### SysPrep considerations

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] SysPrep doesn't support side by side installation of prepared instances of [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] with earlier versions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] on the same computer. However, you can install multiple prepared instances of the same major version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] side by side on the same computer. For more information, see [Considerations for Installing SQL Server Using SysPrep](../../database-engine/install-windows/considerations-for-installing-sql-server-using-sysprep.md).  

[!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later versions can't be installed side-by-side with earlier versions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] on a computer that is running [!INCLUDE [winserver2008r2-md](../../includes/winserver2008r2-md.md)] Server Core SP 1. For more information on Server Core installations, see [Install SQL Server on Server Core](../../database-engine/install-windows/install-sql-server-on-server-core.md).

## Prevent IP address conflicts

When a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Failover Cluster Instance is installed side by side with a standalone instance of the [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)], take care to avoid TCP port number conflicts on the IP addresses. Conflicts usually occur when two instances of the [!INCLUDE [ssDE](../../includes/ssde-md.md)] are both configured to use the default TCP port (1433).

To avoid conflicts, configure one instance to use a non-default fixed port. Configuring a fixed port is usually easiest on the standalone instance. Configuring the [!INCLUDE [ssDE](../../includes/ssde-md.md)] to use different ports prevents an unexpected IP address/TCP port conflict that blocks an instance startup when a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Failover Cluster Instance fails to the standby node.

## Related content

- [Hardware and Software Requirements for Installing SQL Server](hardware-and-software-requirements-for-installing-sql-server.md)
- [Install SQL Server from the Installation Wizard (Setup)](../../database-engine/install-windows/install-sql-server-from-the-installation-wizard-setup.md)
- [Supported Version and Edition Upgrades](../../database-engine/install-windows/supported-version-and-edition-upgrades.md)
- [Upgrade SQL Server](../../database-engine/install-windows/upgrade-sql-server.md)
- [Backward Compatibility](/previous-versions/sql/sql-server-2016/cc280407(v=sql.130))
