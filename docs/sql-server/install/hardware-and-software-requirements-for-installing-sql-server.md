---
title: "SQL Server 2016 and 2017: Hardware and Software Requirements"
description: A list of hardware, software, and operating system requirements for installing and running SQL Server 2016 and SQL Server 2017.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 02/26/2026
ms.service: sql
ms.subservice: release-landing
ms.topic: checklist
helpviewer_keywords:
  - "Setup [SQL Server], software"
  - "software [SQL Server]"
  - "installing SQL Server, software"
  - "operating systems [SQL Server], SQL Server requirements"
  - "Setup [SQL Server], cross-language support"
  - "operating systems [SQL Server], cross-language support"
  - "network connections [SQL Server], requirements"
  - "disk space [SQL Server], SQL Server installations"
  - "drive space [SQL Server], SQL Server installations"
  - "WOW [SQL Server]"
  - "Setup [SQL Server], hardware"
  - "dependencies [SQL Server], SQL Server installations"
  - "cluster hardware requirements [SQL Server]"
  - "endpoints [SQL Server], SQL Server installations"
  - "Internet [SQL Server], SQL Server installations"
  - "hardware [SQL Server]"
  - "Windows on Windows [SQL Server]"
  - "installing SQL Server, hardware"
  - "Setup Configuration Checker"
  - "SCC [SQL Server]"
  - "operating systems [SQL Server]"
  - "space [SQL Server], SQL Server installations"
  - "system configuration checker"
  - "installing SQL Server, cross-language support"
  - "Internet [SQL Server]"
  - "space [SQL Server]"
  - "extended system support [SQL Server]"
  - "64-bit edition [SQL Server]"
  - "failover clustering [SQL Server]"
  - "failover clustering [SQL Server], hardware requirements"
  - "32-bit edition [SQL Server]"
  - "locales [SQL Server], SQL Server installations"
  - "cross-language support"
  - "disk space [SQL Server]"
  - "drive space [SQL Server]"
  - "localized SQL Server versions"
---

# Hardware and software requirements for SQL Server 2016 and SQL Server 2017

[!INCLUDE [SQL Server -Windows Only](../../includes/applies-to-version/sql-windows-only.md)]

This article lists the minimum hardware and software requirements to install and run [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] on the Windows operating system.

For hardware and software requirements for other versions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], see:

- [SQL Server 2025](hardware-and-software-requirements-for-installing-sql-server-2025.md)
- [SQL Server 2022](hardware-and-software-requirements-for-installing-sql-server-2022.md)
- [SQL Server 2019](hardware-and-software-requirements-for-installing-sql-server-2019.md)
- [SQL Server on Linux](../../linux/sql-server-linux-setup.md#system-requirements)

<a id="pmosr"></a>

## Hardware requirements

The following memory and processor requirements apply to all editions of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)]:

| Component | Requirement |
| --- | --- |
| **Storage** | [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] requires a minimum of 6 GB of available hard drive space.<br /><br />Drive space requirements vary with the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] components you install. For more information, see [Drive space requirements](#drive-space-requirements) later in this article. For information on supported storage types for data files, see [Storage types for data files](#storage-types-for-data-files).<br /><br />Installing SQL Server on computers with the NTFS or ReFS file formats is recommended. While the FAT32 file system is supported, it's less secure than NTFS or ReFS.<br /><br />Read-only, mapped, or compressed drives are blocked during installation. |
| **Drive** | A DVD drive, as appropriate, is required for installation from disk. |
| **Monitor** | [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] requires Super-VGA (800x600) or higher resolution monitor. |
| **Internet** | Internet functionality requires Internet access (fees can apply). |
| **Memory** <sup>1</sup> | |
| Minimum memory | - Express editions: 512 MB<br /><br />- All other editions: 1 GB |
| Recommended&nbsp;memory | - Express editions: 1 GB<br /><br />- All other editions: At least 4 GB, and should be increased as database size increases to ensure optimal performance. |
| **Processor** | |
| Processor type | x64 processor. All Intel and AMD x86-64 CPUs with [up to 64 cores per NUMA node](../compute-capacity-limits-by-edition-of-sql-server.md#numa-64). |
| Minimum speed | 1.4 GHz |
| Recommended speed | 2.0 GHz or faster |

<sup>1</sup> The minimum memory required for installing the [!INCLUDE [ssDQSServer](../../includes/ssdqsserver-md.md)] component in [!INCLUDE [ssDQSnoversion](../../includes/ssdqsnoversion-md.md)] (DQS) is 2 GB of RAM, which is different from the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] minimum memory requirement. For information about installing DQS, see [Install Data Quality Services](../../data-quality-services/install-windows/install-data-quality-services.md).

> [!NOTE]  
> Installation of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] is supported on x64 processors only. Installation on x86 processors is no longer supported.

<a id="hwswr"></a>

## Software requirements

The table in this section lists the minimum software requirements for running SQL Server. There are also recommended configuration options for [optimal performance](/troubleshoot/sql/database-engine/performance/recommended-updates-configuration-workloads).

The following requirements apply to all installations:

| Component | Requirement |
| --- | --- |
| Operating&nbsp;system | - Windows 8 or greater<br />- Windows Server 2012 or greater |
| .NET Framework | [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] requires [!INCLUDE [dnprdnshort](../../includes/dnprdnshort-md.md)] 4.6 for the Database Engine, Master Data Services, or Replication. SQL Server setup automatically installs [!INCLUDE [dnprdnshort](../../includes/dnprdnshort-md.md)]. See [.NET Framework installation](#net-framework-installation). |
| Network software | Supported operating systems for [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] have built-in network software. Named and default instances of a stand-alone installation support the following network protocols: Shared memory, Named Pipes, TCP/IP, and VIA<sup>1</sup>. |

<sup>1</sup> VIA protocol isn't supported on failover clusters. Clients or applications running on the same node of the failover cluster as the SQL Server instance, can use Shared Memory protocol to connect to SQL Server using its local pipe address. However this type of connection isn't cluster-aware and fails after an instance failover. It's therefore not recommended and should only be used in specific scenarios.

> [!IMPORTANT]  
> The VIA protocol is deprecated. [!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] For more information about network protocols and libraries, see [Network protocols and network libraries](network-protocols-and-network-libraries.md).

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Setup installs the following software components required by the product:

- [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Native Client
- [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Setup support files

> [!IMPORTANT]  
> The PolyBase feature has additional hardware and software requirements. For more information, see [Data virtualization with PolyBase in SQL Server](../../relational-databases/polybase/polybase-guide.md).

### .NET Framework installation

You can manually install [!INCLUDE [dnprdnshort](../../includes/dnprdnshort-md.md)] from [Microsoft .NET Framework 4.6 (Web Installer) for Windows](https://support.microsoft.com/help/3045560).

For more information, recommendations, and guidance about [!INCLUDE [dnprdnshort](../../includes/dnprdnshort-md.md)] 4.6 see [.NET Framework Deployment Guide for Developers](/dotnet/framework/deployment/deployment-guide-for-developers).

[!INCLUDE [win81](../../includes/win81-md.md)] and [!INCLUDE [winserver2012r2](../../includes/winserver2012r2-md.md)] require [KB2919355](https://support.microsoft.com/help/2919355) before installing [!INCLUDE [dnprdnshort](../../includes/dnprdnshort-md.md)] 4.6.

> [!NOTE]  
> Support for .NET Framework 4.5.2, 4.6, and 4.6.1 ended on April 26, 2022.

[!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] require .NET Framework 4.6 for Database Engine, Master Data Services, or Replication (SQL Server setup automatically installs .NET Framework).

You can upgrade to .NET 4.8 Framework or directly install .NET 4.8 Framework. All frameworks with major version 4.x do an in-place upgrade, and they are backward compatible.

For more information, check [Download .NET Framework 4.8 \| Free official downloads (microsoft.com)](https://dotnet.microsoft.com/download/dotnet-framework/net48).

## Operating system support for SQL Server 2017

The following table shows which editions of [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] are compatible with which versions of Windows. You can also use the support lifecycle information to see if your version of Windows is supported.

| [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] edition: | Enterprise | Developer | Standard | Web | Express |
| --- | --- | --- | --- | --- | --- |
| **Windows Server 2022** ([Support lifecycle](/lifecycle/products/windows-server-2022)) | | | | | |
| Windows Server 2022 Datacenter | Yes | Yes | Yes | Yes | Yes |
| Windows Server 2022 Datacenter: Azure Edition | Yes | Yes | Yes | Yes | Yes |
| Windows Server 2022 Standard | Yes | Yes | Yes | Yes | Yes |
| **Windows Server 2019** ([Support lifecycle](/lifecycle/products/windows-server-2019)) | | | | | |
| Windows Server 2019 Datacenter | Yes | Yes | Yes | Yes | Yes |
| Windows Server 2019 Standard | Yes | Yes | Yes | Yes | Yes |
| Windows Server 2019 Essentials | Yes | Yes | Yes | Yes | Yes |
| **Windows Server 2016** ([Support lifecycle](/lifecycle/products/windows-server-2016)) | | | | | |
| Windows Server 2016 Datacenter | Yes | Yes | Yes | Yes | Yes |
| Windows Server 2016 Standard | Yes | Yes | Yes | Yes | Yes |
| Windows Server 2016 Essentials | Yes | Yes | Yes | Yes | Yes |
| **Windows Server 2012 R2** ([Support lifecycle](/lifecycle/products/windows-server-2012-r2)) | | | | | |
| Windows Server 2012 R2 Datacenter | Yes | Yes | Yes | Yes | Yes |
| Windows Server 2012 R2 Standard | Yes | Yes | Yes | Yes | Yes |
| Windows Server 2012 R2 Essentials | Yes | Yes | Yes | Yes | Yes |
| Windows Server 2012 R2 Foundation | Yes | Yes | Yes | Yes | Yes |
| **Windows Server 2012** ([Support lifecycle](/lifecycle/products/windows-server-2012)) | | | | | |
| Windows Server 2012 Datacenter | Yes | Yes | Yes | Yes | Yes |
| Windows Server 2012 Standard | Yes | Yes | Yes | Yes | Yes |
| Windows Server 2012 Essentials | Yes | Yes | Yes | Yes | Yes |
| Windows Server 2012 Foundation | Yes | Yes | Yes | Yes | Yes |
| Windows 11 IoT Enterprise | No | Yes | Yes | No | Yes |
| Windows 11 Enterprise | No | Yes | Yes | No | Yes |
| Windows 11 Professional | No | Yes | Yes | No | Yes |
| Windows 11 Home | No | Yes | Yes | No | Yes |
| Windows 10 IoT Enterprise | No | Yes | Yes | No | Yes |
| Windows 10 Enterprise | No | Yes | Yes | No | Yes |
| Windows 10 Professional | No | Yes | Yes | No | Yes |
| Windows 10 Home | No | Yes | Yes | No | Yes |
| Windows 8.1 Enterprise | No | Yes | Yes | No | Yes |
| Windows 8.1 Pro | No | Yes | Yes | No | Yes |
| Windows 8.1 Enterprise | No | Yes | Yes | No | Yes |
| Windows 8 Pro | No | Yes | Yes | No | Yes |
| Windows 8 | No | Yes | Yes | No | Yes |

### Server Core support for SQL Server 2017

The following editions of Windows Server Core support installing [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)]:

- Windows Server 2022 Datacenter
- Windows Server 2022 Datacenter: Azure edition
- Windows Server 2022 Standard
- Windows Server 2019 Datacenter
- Windows Server 2019 Standard
- Windows Server 2016 Datacenter
- Windows Server 2016 Standard
- Windows Server 2012 R2 Datacenter
- Windows Server 2012 R2 Standard
- Windows Server 2012 Datacenter
- Windows Server 2012 Standard

For more information about installing [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] on Server Core, see [Install SQL Server on Server Core](../../database-engine/install-windows/install-sql-server-on-server-core.md).

> [!NOTE]  
> You can't install SQL Server on a Windows OS when case sensitivity is enabled. For more information, see [SQL Server isn't supported on a Windows operating system on which case sensitivity is enabled](/troubleshoot/sql/install/sql-server-not-supported-in-windows-os-where-case-sensitivity-enabled).

## Operating system support for SQL Server 2016

The following table shows which editions of [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] are compatible with which versions of Windows. You can also use the support lifecycle information to see if your version of Windows is supported.

| [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] edition: | Enterprise | Developer | Standard | Web | Express |
| --- | --- | --- | --- | --- | --- |
| **Windows Server 2019** ([Support lifecycle](/lifecycle/products/windows-server-2019)) | | | | | |
| Windows Server 2019 Datacenter | Yes | Yes | Yes | Yes | Yes |
| Windows Server 2019 Standard | Yes | Yes | Yes | Yes | Yes |
| Windows Server 2019 Essentials | Yes | Yes | Yes | Yes | Yes |
| **Windows Server 2016** ([Support lifecycle](/lifecycle/products/windows-server-2016)) | | | | | |
| Windows Server 2016 Datacenter | Yes | Yes | Yes | Yes | Yes |
| Windows Server 2016 Standard | Yes | Yes | Yes | Yes | Yes |
| Windows Server 2016 Essentials | Yes | Yes | Yes | Yes | Yes |
| **Windows Server 2012 R2** ([Support lifecycle](/lifecycle/products/windows-server-2012-r2)) | | | | | |
| Windows Server 2012 R2 Datacenter | Yes | Yes | Yes | Yes | Yes |
| Windows Server 2012 R2 Standard | Yes | Yes | Yes | Yes | Yes |
| Windows Server 2012 R2 Essentials | Yes | Yes | Yes | Yes | Yes |
| Windows Server 2012 R2 Foundation | Yes | Yes | Yes | Yes | Yes |
| **Windows Server 2012** ([Support lifecycle](/lifecycle/products/windows-server-2012)) | | | | | |
| Windows Server 2012 Datacenter | Yes | Yes | Yes | Yes | Yes |
| Windows Server 2012 Standard | Yes | Yes | Yes | Yes | Yes |
| Windows Server 2012 Essentials | Yes | Yes | Yes | Yes | Yes |
| Windows Server 2012 Foundation | Yes | Yes | Yes | Yes | Yes |
| Windows 11 IoT Enterprise | No | No | No | No | No |
| Windows 11 Enterprise | No | No | No | No | No |
| Windows 11 Professional | No | No | No | No | No |
| Windows 11 Home | No | No | No | No | No |
| Windows 10 IoT Enterprise | No | Yes | Yes | No | Yes |
| Windows 10 Enterprise | No | Yes | Yes | No | Yes |
| Windows 10 Professional | No | Yes | Yes | No | Yes |
| Windows 10 Home | No | Yes | Yes | No | Yes |
| Windows 8.1 Enterprise | No | Yes | Yes | No | Yes |
| Windows 8.1 Pro | No | Yes | Yes | No | Yes |
| Windows 8.1 Enterprise | No | Yes | Yes | No | Yes |
| Windows 8 Pro | No | Yes | Yes | No | Yes |
| Windows 8 | No | Yes | Yes | No | Yes |

For minimum version requirements to install [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] on [!INCLUDE [winserver2012](../../includes/winserver2012-md.md)] or [!INCLUDE [win8](../../includes/win8-md.md)], see [Version requirements for SQL Server in Windows operating system](/troubleshoot/sql/database-engine/install/windows/use-sql-server-in-windows).

### Server Core support for SQL Server 2016

The following editions of Windows Server Core support installing [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)]:

- Windows Server 2019 Datacenter
- Windows Server 2019 Standard
- Windows Server 2016 Datacenter
- Windows Server 2016 Standard
- Windows Server 2012 R2 Datacenter
- Windows Server 2012 R2 Standard
- Windows Server 2012 Datacenter
- Windows Server 2012 Standard

For more information about installing [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] on Server Core, see [Install SQL Server on Server Core](../../database-engine/install-windows/install-sql-server-on-server-core.md).

> [!NOTE]  
> You can't install SQL Server on a Windows OS when case sensitivity is enabled. For more information, see [SQL Server isn't supported on a Windows operating system on which case sensitivity is enabled](/troubleshoot/sql/install/sql-server-not-supported-in-windows-os-where-case-sensitivity-enabled).

### WOW64 support

WOW64 (Windows 32-bit on Windows 64-bit) is a feature of 64-bit editions of Windows that enables 32-bit applications to run natively in 32-bit mode. Applications function in 32-bit mode, even though the underlying operating system is a 64-bit operating system. WOW64 isn't supported for [!INCLUDE [ssCurrent](../../includes/ssnoversion-md.md)] installations. However, Management Tools are supported in WOW64.

### Features supported on 32-bit client Operating Systems

Windows client operating systems, for example Windows 10 and Windows 8.1 are available as 32-bit or 64-bit architectures.   All SQL Server features are supported on 64-bit client operating systems. On supported 32-bit client operating systems Microsoft supports the following features:

- Data Quality Client
- Client Tools Connectivity
- Integration Services
- Client Tools Backward Compatibility
- Client Tools SDK
- Documentation Components
- Distributed Replay Components
- Distributed Replay Controller
- Distributed Replay Client
- SQL Client Connectivity SDK

[!INCLUDE [winserver2008r2](../../includes/winserver2008r2-md.md)] and later server operating systems aren't available as 32-bit architectures. All supported server operating systems are only available as 64-bit. All features are supported on 64-bit server operating systems.

<a id="CrossLanguageSupport"></a>

## Cross-language support

For more information about cross-language support and considerations for installing [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] in localized languages, see [Local language versions in SQL Server](local-language-versions-in-sql-server.md).

<a id="HardDiskSpace"></a>

## Drive space requirements

During installation of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)], Windows Installer creates temporary files on the system drive. Before you run Setup to install or upgrade [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], verify that you have at least 6 GB of available drive space on the system drive for these files. This requirement applies even if you install [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] components to a non-default drive.

Actual hard drive space requirements depend on your system configuration and the features that you decide to install. The following table provides drive space requirements for [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] components.

| Feature | Drive space requirement |
| --- | ---: |
| [!INCLUDE [ssDE](../../includes/ssde-md.md)] and data files, Replication, Full-Text Search, and Data Quality Services | 1,480 MB |
| [!INCLUDE [ssDE](../../includes/ssde-md.md)] (as preceding row) with R Services (In-Database) | 2,744 MB |
| [!INCLUDE [ssDE](../../includes/ssde-md.md)] (as preceding row) with PolyBase Query Service for External Data | 4,194 MB |
| [!INCLUDE [ssASnoversion](../../includes/ssasnoversion-md.md)] and data files | 698 MB |
| [!INCLUDE [ssRSnoversion](../../includes/ssrsnoversion-md.md)] | 967 MB |
| [!INCLUDE [rsql_platform](../../includes/rsql-platform-md.md)] (Standalone) | 280 MB |
| [!INCLUDE [ssRSnoversion](../../includes/ssrsnoversion-md.md)] - SharePoint | 1,203 MB |
| [!INCLUDE [ssRSnoversion](../../includes/ssrsnoversion-md.md)] Add-in for SharePoint Products | 325 MB |
| [!INCLUDE [ssDQSClient](../../includes/ssdqsclient-md.md)] | 121 MB |
| Client Tools Connectivity | 328 MB |
| [!INCLUDE [ssISnoversion](../../includes/ssisnoversion-md.md)] | 306 MB |
| Client Components (other than [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Books Online components and Integration Services tools) | 445 MB |
| [!INCLUDE [ssMDSshort](../../includes/ssmdsshort-md.md)] | 280 MB |
| [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Books Online Components to view and manage help content <sup>1</sup> | 27 MB |
| All features | 8,030 MB |

<sup>1</sup> The drive space requirement for downloaded Books Online content is 200 MB.

<a id="StorageTypes"></a>

## Storage types for data files

The supported storage types for data files are:

- **Local disk**

  - [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] currently supports disk drives that have standard native sector sizes of 512 bytes and 4 KB. For more information about support for larger sector sizes and manufacturer implementations, see the section "4-KB disk sector sizes" in the white paper *SQLIOBasicsCh2.doc*. You can download the whitepaper from the [Download](/previous-versions/sql/sql-server-2005/administrator/cc917726(v=technet.10)#download) section of the [SQL Server I/O Basics, Chapter 2](/previous-versions/sql/sql-server-2005/administrator/cc917726(v=technet.10)) article.

    If you use advanced format disks that are physically formatted with 4,096 bytes, but expose a logical sector size of 512 bytes, you can read more about the behavior and recommendations in the Tech Community article [SQL Server - New drives use 4K sector size](https://techcommunity.microsoft.com/blog/sqlserversupport/sql-server---new-drives-use-4k-sector-size/316277).

    Hard drives with sector sizes larger than 4 KB might cause errors when you attempt to store [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] data files on them. For more information on hard drive sector-size support in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], see [Troubleshoot SQL Server errors related to system disk sector size greater than 4 KB](/troubleshoot/sql/database-engine/database-file-operations/troubleshoot-os-4kb-disk-sector-size). Currently, the `ForcedPhysicalSectorSizeInBytes` registry key is required to successfully install SQL Server on some newer storage devices with system disk sector size greater than 4 KB.

  - [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] failover cluster installation supports Local Disk only for installing the `tempdb` files. Ensure that the path specified for the `tempdb` data and log files is valid on all the cluster nodes. During failover, if the `tempdb` directories aren't available on the failover target node, the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] resource fails to come online.

- **Shared storage**

- **[Storage Spaces Direct (S2D)](/windows-server/storage/storage-spaces/storage-spaces-direct-overview)**

- **SMB file share**

  - SMB storage isn't supported for [!INCLUDE [ssASnoversion](../../includes/ssasnoversion-md.md)] data files for either standalone or clustered installations. Use direct attached storage, a storage area network, or S2D instead.

  - SMB storage can be hosted by a Windows File Server or a third-party SMB storage device. If you use Windows File Server, the version should be 2008 or later. For more information about installing [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] using SMB file share as a storage option, see [Install SQL Server with SMB fileshare storage](../../database-engine/install-windows/install-sql-server-with-smb-fileshare-as-a-storage-option.md).

<a id="DC_support"></a>

## Install SQL Server on a domain controller

For security reasons, don't install [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] on a domain controller. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Setup doesn't block installation on a computer that is a domain controller, but the following limitations apply:

- You can't run [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] services on a domain controller under a local service account.

- After you install [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] on a computer, you can't change the computer from a domain member to a domain controller. You must uninstall [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] before you change the host computer to a domain controller.

- After you install [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] on a computer, you can't change the computer from a domain controller to a domain member. You must uninstall [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] before you change the host computer to a domain member.

- [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] failover cluster instances aren't supported where cluster nodes are domain controllers.

- [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] isn't supported on a read-only domain controller. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Setup can't create security groups or [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] service accounts on a read-only domain controller. In this scenario, Setup fails.

  > [!NOTE]  
  > This restriction also applies to installations on domain member nodes.

- A [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] failover cluster instance isn't supported in an environment where only a read-only domain controller is accessible.

  > [!NOTE]  
  > This restriction also applies to installations on domain member nodes.

## Installation media

Get relevant installation media from the following locations:

- [SQL Server 2017 evaluation center](https://www.microsoft.com/evalcenter/evaluate-sql-server-2017-rtm)
- [Most recent cumulative updates](/troubleshoot/sql/releases/download-and-install-latest-updates?bc=%2fsql%2fbreadcrumb%2ftoc.json&toc=%2fsql%2ftoc.json)

Alternatively, you can deploy [SQL Server on an Azure virtual machine in the Azure portal](/azure/azure-sql/virtual-machines/windows/sql-vm-create-portal-quickstart). Because of the overhead of virtualization, virtual machines can be slower than running natively.

## Related content

- [Plan a SQL Server installation](planning-a-sql-server-installation.md)
- [Security considerations for a SQL Server installation](security-considerations-for-a-sql-server-installation.md)
