---
title: "SQL Server 2016 & 2017: Hardware & software requirements"
description: A list of hardware, software, and operating system requirements for installing and running SQL Server 2016 and SQL Server 2017. 
ms.custom: "seo-lt-2019"
ms.date: "09/16/2021"
ms.service: sql
ms.reviewer: ""
ms.subservice: release-landing
ms.topic: conceptual
helpviewer_keywords: 
  - "Setup [SQL Server], software"
  - "software [SQL Server]"
  - "installing SQL Server, software"
  - "operating systems [SQL Server], SQL Server requirements"
  - "Setup [SQL Server], cross-language support"
  - "operating systems [SQL Server], cross-language support"
  - "network connections [SQL Server], requirements"
  - "disk space [SQL Server], SQL Server installations"
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
  - "localized SQL Server versions"
ms.author: mikeray
author: MikeRayMSFT
---
# SQL Server 2016 and 2017: Hardware and software requirements

[!INCLUDE [SQL Server -Windows Only](../../includes/applies-to-version/sql-windows-only.md)]

The article lists the minimum hardware and software requirements to install and run SQL Server 2016 and SQL Server 2017 on the Windows operating system.  

For hardware and software requirements for other versions of SQL Server, see:

- [SQL Server 2022](hardware-and-software-requirements-for-installing-sql-server-2022.md)
- [SQL Server 2019](hardware-and-software-requirements-for-installing-sql-server-2019.md)
- [SQL Server on Linux](../../linux/sql-server-linux-setup.md#system)

## Hardware requirements

 The following hardware requirements apply to SQL Server 2016 and SQL Server 2017: 
  
|Component|Requirement|  
|---------------|-----------------|  
|Hard Disk|[!INCLUDE[ssCurrent](../../includes/ssnoversion-md.md)] requires a minimum of 6 GB of available hard-disk space.<br /><br />Disk space requirements will vary with the [!INCLUDE[ssCurrent](../../includes/ssnoversion-md.md)] components you install. For more information, see [Hard Disk Space Requirements](../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server.md#HardDiskSpace) later in this article. For information on supported storage types for data files, see [Storage Types for Data Files](../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server.md#StorageTypes).<br /><br />Installing SQL Server on computers with the NTFS or ReFS file formats is recommended. The FAT32 file system is supported but not recommended as it is less secure than the NTFS or ReFS file systems.<br /><br />Read-only, mapped, or compressed drives are blocked during installation. |  
|Drive|A DVD drive, as appropriate, is required for installation from disc.  |  
|Monitor|[!INCLUDE[ssCurrent](../../includes/ssnoversion-md.md)] requires Super-VGA (800x600) or higher resolution monitor.|  
|Internet|Internet functionality requires Internet access (fees may apply).|  
|Memory \*|**Minimum:**<br /><br />Express Editions: 512 MB<br /><br />All other editions: 1 GB<br /><br />**Recommended:**<br /><br />Express Editions: 1 GB<br /><br />All other editions: At least 4 GB and should be increased as database size increases to ensure optimal performance.|  
|Processor Speed|**Minimum:** x64 Processor: 1.4 GHz<br /><br />**Recommended:** 2.0 GHz or faster|  
|Processor Type|x64 Processor: AMD Opteron, AMD Athlon 64, Intel Xeon with Intel EM64T support, Intel Pentium IV with EM64T support|  
  
> [!NOTE]  
> Installation of [!INCLUDE[ssCurrent](../../includes/ssnoversion-md.md)] is supported on x64 processors only. It is no longer supported on x86 processors.  
  
 \* The minimum memory required for installing the [!INCLUDE[ssDQSServer](../../includes/ssdqsserver-md.md)] component in [!INCLUDE[ssDQSnoversion](../../includes/ssdqsnoversion-md.md)] (DQS) is 2 GB of RAM, which is different from the [!INCLUDE[ssCurrent](../../includes/ssnoversion-md.md)] minimum memory requirement. For information about installing DQS, see [Install Data Quality Services](../../data-quality-services/install-windows/install-data-quality-services.md).  
  
  
##  <a name="hwswr"></a> Software requirements  

The table in this section lists the minimum software requirements for running SQL Server. There are also recommended configuration options for [optimal performance](https://support.microsoft.com/help/4465518/recommended-updates-and-configurations-for-sql-server). 

The following software requirements apply to all installations:  
  
|Component|Requirement|  
|---------------|-----------------|  
|.NET Framework|[!INCLUDE[sql2016](../../includes/sssql16-md.md)] and later require [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] 4.6 for the Database Engine, Master Data Services, or  Replication. SQL Server setup automatically installs [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)]. You can also manually install [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] from [Microsoft .NET Framework 4.6 (Web Installer) for Windows](https://support.microsoft.com/kb/3045560).<br /><br />For more information, recommendations, and guidance about [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] 4.6 see [.NET Framework Deployment Guide for Developers](https://msdn.microsoft.com/library/ee942965\(v=vs.110\).aspx).<br /><br />[!INCLUDE[win81](../../includes/win81-md.md)], and [!INCLUDE[winserver2012r2](../../includes/winserver2012r2-md.md)] require [KB2919355](https://support.microsoft.com/kb/2919355) before installing [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] 4.6.<br /><br />**Note:** Support for .NET Framework 4.5.2, 4.6, and 4.6.1 ended on April 26, 2022.<ul><li>SQL Server 2016 (13.x) and later require .NET Framework 4.6 for Database Engine, Master Data Services, or Replication (SQL Server setup automatically installs .NET Framework). You can upgrade to .NET 4.8 Framework or directly install .NET 4.8 Framework. All frameworks with major version 4 do an in-place upgrade, and they are backward compatible. For more information, check [Download .NET Framework 4.8 \| Free official downloads (microsoft.com)](https://dotnet.microsoft.com/download/dotnet-framework/net48).</li><li>SQL Server 2014 and SQL Server 2012 use .NET Framework 3.5 SP1, which is supported till 2029, so this retirement doesn't impact them.</li></ul>|  
|Network Software|Supported operating systems for [!INCLUDE[ssCurrent](../../includes/ssnoversion-md.md)] have built-in network software. Named and default instances of a stand-alone installation support the following network protocols: Shared memory, Named Pipes, TCP/IP, and VIA.<br /><br />**Note:** VIA protocol is not supported on failover clusters. Clients or applications running on the same node of the failover cluster as the SQL Server instance, can use Shared Memory protocol to connect to SQL Server using its local pipe address. However this type of connection is not cluster-aware and will fail after an instance failover. It is therefore not recommended and should only be used in very specific scenarios.<br /><br />**Important:** The VIA protocol is deprecated. [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)]<br /><br />For more information about Network Protocols and Network Libraries, see [Network Protocols and Network Libraries](../../sql-server/install/network-protocols-and-network-libraries.md).|  

[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup installs the following software components required by the product:  
  
   - [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client    
   - [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup support files  

  
  
> [!IMPORTANT]
> There are additional hardware and software requirements for the PolyBase feature. For more information, see [Get started with PolyBase](../../relational-databases/polybase/polybase-guide.md).  
  



## Operating System support for SQL Server 2017

The following table shows which editions of SQL Server 2017 are compatible with which versions of Windows:  
  
| SQL Server edition:               | Enterprise | Developer | Standard | Web | Express |  
| :------------------------         | :--------- | :-------- | :------- | :-- | :------ | 
| Windows Server 2022 Datacenter    |    Yes     |    Yes    |    Yes   | Yes |   Yes   |
| Windows Server 2022 Datacenter: Azure Edition|    Yes     |    Yes    |    Yes   | Yes |   Yes   |
| Windows Server 2022 Standard      |    Yes     |    Yes    |    Yes   | Yes |   Yes   |
| Windows Server 2019 Datacenter    |    Yes     |    Yes    |    Yes   | Yes |   Yes   |
| Windows Server 2019 Standard      |    Yes     |    Yes    |    Yes   | Yes |   Yes   |
| Windows Server 2019 Essentials    |    Yes     |    Yes    |    Yes   | Yes |   Yes   |
| Windows Server 2016 Datacenter    |    Yes     |    Yes    |    Yes   | Yes |   Yes   |
| Windows Server 2016 Standard      |    Yes     |    Yes    |    Yes   | Yes |   Yes   |
| Windows Server 2016 Essentials    |    Yes     |    Yes    |    Yes   | Yes |   Yes   |
| Windows Server 2012 R2 Datacenter |    Yes     |    Yes    |    Yes   | Yes |   Yes   |
| Windows Server 2012 R2 Standard   |    Yes     |    Yes    |    Yes   | Yes |   Yes   |
| Windows Server 2012 R2 Essentials |    Yes     |    Yes    |    Yes   | Yes |   Yes   |
| Windows Server 2012 R2 Foundation |    Yes     |    Yes    |    Yes   | Yes |   Yes   |
| Windows Server 2012 Datacenter    |    Yes     |    Yes    |    Yes   | Yes |   Yes   |
| Windows Server 2012 Standard      |    Yes     |    Yes    |    Yes   | Yes |   Yes   |
| Windows Server 2012 Essentials    |    Yes     |    Yes    |    Yes   | Yes |   Yes   |
| Windows Server 2012 Foundation    |    Yes     |    Yes    |    Yes   | Yes |   Yes   |
| Windows 11 IoT Enterprise         |    No      |    Yes    |    Yes   | No  |   Yes   |
| Windows 11 Enterprise             |    No      |    Yes    |    Yes   | No  |   Yes   |
| Windows 11 Professional           |    No      |    Yes    |    Yes   | No  |   Yes   |
| Windows 11 Home                   |    No      |    Yes    |    Yes   | No  |   Yes   |
| Windows 10 IoT Enterprise         |    No      |    Yes    |    Yes   | No  |   Yes   |
| Windows 10 Enterprise             |    No      |    Yes    |    Yes   | No  |   Yes   |
| Windows 10 Professional           |    No      |    Yes    |    Yes   | No  |   Yes   |
| Windows 10 Home                   |    No      |    Yes    |    Yes   | No  |   Yes   |
| Windows 8.1 Enterprise            |    No      |    Yes    |    Yes   | No  |   Yes   |
| Windows 8.1 Pro                   |    No      |    Yes    |    Yes   | No  |   Yes   |
| Windows 8.1 Enterprise            |    No      |    Yes    |    Yes   | No  |   Yes   |
| Windows 8 Pro                     |    No      |    Yes    |    Yes   | No  |   Yes   |
| Windows 8                         |    No      |    Yes    |    Yes   | No  |   Yes   | 

### Server Core support for SQL Server 2017

Installing SQL Server 2017 on Server Core mode is supported by the following editions of Windows Server:

:::row:::
    :::column:::
        Windows Server 2022 Standard
    :::column-end:::
    :::column:::
        Windows Server 2022 Datacenter
    :::column-end:::
    :::column:::
        Windows Server 2022 Datacenter: Azure edition
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        Windows Server 2019 Standard
    :::column-end:::
    :::column:::
        Windows Server 2019 Datacenter
    :::column-end:::
    :::column:::
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        Windows Server 2016 Standard
    :::column-end:::
    :::column:::
        Windows Server 2016 Datacenter
    :::column-end:::
    :::column:::
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        Windows Server 2012 R2 Standard
    :::column-end:::
    :::column:::
        Windows Server 2012 R2  Datacenter
    :::column-end:::
    :::column:::
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        Windows Server 2012 Standard
    :::column-end:::
    :::column:::
        Windows Server 2012 Datacenter
    :::column-end:::
    :::column:::
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
    :::column-end:::
    :::column:::
    :::column-end:::
:::row-end:::

For more information on installing SQL Server on Server Core, see [Install SQL Server on Server Core](../../database-engine/install-windows/install-sql-server-on-server-core.md).  

> [!NOTE]  
> Installing SQL Server on a Windows OS on which case sensitivity is enabled is not supported. For more information review [SQL Server is not supported on a Windows operating system on which case sensitivity is enabled](/troubleshoot/sql/install/sql-server-not-supported-in-windows-os-where-case-sensitivity-enabled)

## Operating System support for SQL Server 2016

The following table shows which editions of SQL Server 2016 are compatible with which versions of Windows:  

| SQL Server edition:               | Enterprise | Developer | Standard | Web | Express |  
| :------------------------         | :--------- | :-------- | :------- | :-- | :------ |
| Windows Server 2022 Datacenter    |    No     |    No    |    No   | No |   No   |
| Windows Server 2022 Datacenter: Azure Edition|    No     |    No    |    No   | No |   No   | 
| Windows Server 2022 Standard      |    No     |    No    |    No   | No |   No   |
| Windows Server 2019 Datacenter    |    Yes     |    Yes    |    Yes   | Yes |   Yes   |
| Windows Server 2019 Standard      |    Yes     |    Yes    |    Yes   | Yes |   Yes   |
| Windows Server 2019 Essentials    |    Yes     |    Yes    |    Yes   | Yes |   Yes   |
| Windows Server 2016 Datacenter    |    Yes     |    Yes    |    Yes   | Yes |   Yes   |
| Windows Server 2016 Standard      |    Yes     |    Yes    |    Yes   | Yes |   Yes   |
| Windows Server 2016 Essentials    |    Yes     |    Yes    |    Yes   | Yes |   Yes   |
| Windows Server 2012 R2 Datacenter |    Yes     |    Yes    |    Yes   | Yes |   Yes   |
| Windows Server 2012 R2 Standard   |    Yes     |    Yes    |    Yes   | Yes |   Yes   |
| Windows Server 2012 R2 Essentials |    Yes     |    Yes    |    Yes   | Yes |   Yes   |
| Windows Server 2012 R2 Foundation |    Yes     |    Yes    |    Yes   | Yes |   Yes   |
| Windows Server 2012 Datacenter    |    Yes     |    Yes    |    Yes   | Yes |   Yes   |
| Windows Server 2012 Standard      |    Yes     |    Yes    |    Yes   | Yes |   Yes   |
| Windows Server 2012 Essentials    |    Yes     |    Yes    |    Yes   | Yes |   Yes   |
| Windows Server 2012 Foundation    |    Yes     |    Yes    |    Yes   | Yes |   Yes   |
| Windows 11 IoT Enterprise         |    No      |    No     |    No    | No  |   No    |
| Windows 11 Enterprise             |    No      |    No     |    No    | No  |   No    |
| Windows 11 Professional           |    No      |    No     |    No    | No  |   No    |
| Windows 11 Home                   |    No      |    No     |    No    | No  |   No    |
| Windows 10 IoT Enterprise         |    No      |    Yes    |    Yes   | No  |   Yes   |
| Windows 10 Enterprise             |    No      |    Yes    |    Yes   | No  |   Yes   |
| Windows 10 Professional           |    No      |    Yes    |    Yes   | No  |   Yes   |
| Windows 10 Home                   |    No      |    Yes    |    Yes   | No  |   Yes   |
| Windows 8.1 Enterprise            |    No      |    Yes    |    Yes   | No  |   Yes   |
| Windows 8.1 Pro                   |    No      |    Yes    |    Yes   | No  |   Yes   |
| Windows 8.1 Enterprise            |    No      |    Yes    |    Yes   | No  |   Yes   |
| Windows 8 Pro                     |    No      |    Yes    |    Yes   | No  |   Yes   |
| Windows 8                         |    No      |    Yes    |    Yes   | No  |   Yes   | 

For minimum version requirements to install [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on [!INCLUDE[winserver2012](../../includes/winserver2012-md.md)] or [!INCLUDE[win8](../../includes/win8-md.md)], see [Installing SQL Server on Windows Server 2012 or Windows 8](https://support.microsoft.com/kb/2681562). 

### Server Core support for SQL Server 2016

Installing SQL Server 2016 on Server Core mode is supported by the following editions of Windows Server:

:::row:::
    :::column:::
        Windows Server 2019 Standard
    :::column-end:::
    :::column:::
        Windows Server 2019 Datacenter
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        Windows Server 2016 Standard
    :::column-end:::
    :::column:::
        Windows Server 2016 Datacenter
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        Windows Server 2012 R2 Standard
    :::column-end:::
    :::column:::
        Windows Server 2012 R2  Datacenter
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        Windows Server 2012 Standard
    :::column-end:::
    :::column:::
        Windows Server 2012 Datacenter
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
    :::column-end:::
    :::column:::
    :::column-end:::
:::row-end:::

For more information on installing SQL Server on Server Core, see [Install SQL Server on Server Core](../../database-engine/install-windows/install-sql-server-on-server-core.md).  

> [!NOTE]  
> Installing SQL Server on a Windows OS on which case sensitivity is enabled is not supported. For more information review [SQL Server is not supported on a Windows operating system on which case sensitivity is enabled](/troubleshoot/sql/install/sql-server-not-supported-in-windows-os-where-case-sensitivity-enabled)

### WOW64 support
  
 WOW64 (Windows 32-bit on Windows 64-bit) is a feature of 64-bit editions of Windows that enables 32-bit applications to run natively in 32-bit mode. Applications function in 32-bit mode, even though the underlying operating system is a 64-bit operating system. WOW64 is not supported for [!INCLUDE[ssCurrent](../../includes/ssnoversion-md.md)] installations. However, Management Tools are supported in WOW64.  

  
### Features supported on 32-bit client Operating Systems 
 Windows client operating systems, for example Windows 10 and Windows 8.1 are available as 32-bit or 64-bit architectures.   All SQL Server features are supported on 64-bit client operating systems. On supported 32-bit client operating systems Microsoft supports the following features:  
  
-   Data Quality Client
-   Client Tools Connectivity
-   Integration Services
-   Client Tools Backwards Compatibility
-   Client Tools SDK
-   Documentation Components
-   Distributed Replay Components
-   Distributed Replay Controller
-   Distributed Replay Client
-   SQL Client Connectivity SDK
  
 [!INCLUDE[winserver2008r2](../../includes/winserver2008r2-md.md)] and later server operating systems are not available as 32-bit architectures. All supported server operating systems are only available as 64-bit. All features are supported on 64-bit server operating systems.  


##  <a name="CrossLanguageSupport"></a> Cross-Language support  
 For more information about cross-language support and considerations for installing [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in localized languages, see [Local Language Versions in SQL Server](../../sql-server/install/local-language-versions-in-sql-server.md).  
  
##  <a name="HardDiskSpace"></a> Disk space requirements  
 During installation of [!INCLUDE[ssCurrent](../../includes/ssnoversion-md.md)], Windows Installer creates temporary files on the system drive. Before you run Setup to install or upgrade [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], verify that you have at least 6.0 GB of available disk space on the system drive for these files. This requirement applies even if you install [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] components to a non-default drive.  
  
 Actual hard disk space requirements depend on your system configuration and the features that you decide to install. The following table provides disk space requirements for [!INCLUDE[ssCurrent](../../includes/ssnoversion-md.md)] components.  
  
|**Feature**|**Disk space requirement**|  
|-----------------|--------------------------------|  
|[!INCLUDE[ssDE](../../includes/ssde-md.md)] and data files, Replication, Full-Text Search, and Data Quality Services|1480 MB|  
|[!INCLUDE[ssDE](../../includes/ssde-md.md)] (as above) with R Services (In-Database)|2744 MB|  
|[!INCLUDE[ssDE](../../includes/ssde-md.md)] (as above) with PolyBase Query Service for External Data|4194 MB|  
|[!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] and data files|698 MB|  
|[!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]|967 MB|  
|[!INCLUDE[rsql_platform](../../includes/rsql-platform-md.md)] (Standalone)|280 MB|  
|[!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] - SharePoint|1203 MB|  
|[!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Add-in for SharePoint Products|325 MB|  
|[!INCLUDE[ssDQSClient](../../includes/ssdqsclient-md.md)]|121 MB|  
|Client Tools Connectivity|328 MB|  
|[!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)]|306 MB|  
|Client Components (other than [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online components and Integration Services tools)|445 MB|  
|[!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)]|280 MB|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online Components to view and manage help content*|27 MB|  
|All Features|8030 MB|  
  
 *The disk space requirement for downloaded Books Online content is 200 MB.  
  
##  <a name="StorageTypes"></a> Storage Types for Data Files  
 The supported storage types for data files are:  
  
-   Local Disk 
    - [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] currently supports disk drives that have standard native sector sizes of 512 bytes and 4 KB.  Hard disks with sector sizes larger than 4 KB may cause errors when attempting to store [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data files on them.  See [Hard disk drive sector-size support boundaries in SQL Server](https://support.microsoft.com/kb/926930) for more information on hard disk sector-size support in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [Troubleshoot errors related to system disk sector size greater than 4 KB](/troubleshoot/sql/admin/troubleshoot-os-4kb-disk-sector-size).
    - [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] failover cluster installation supports Local Disk only for installing the tempdb files. Ensure that the path specified for the tempdb data and log files is valid on all the cluster nodes. During failover, if the tempdb directories are not available on the failover target node, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] resource will fail to come online.
-   Shared Storage  
-   [Storage Spaces Direct \(S2D\)](/windows-server/storage/storage-spaces/storage-spaces-direct-overview)  
-   SMB File Share  
    - SMB storage is not supported for [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] data files for either standalone or clustered installations. Use direct attached storage, a storage area network, or S2D instead. 
    - SMB storage can be hosted by a Windows File Server or a third-party SMB storage device. If Windows File Server is used, the Windows File Server version should be 2008 or later. For more information about installing [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using SMB file share as a storage option, see [Install SQL Server with SMB Fileshare as a Storage Option](../../database-engine/install-windows/install-sql-server-with-smb-fileshare-as-a-storage-option.md).  
  
  
  
##  <a name="DC_support"></a> Installing [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on a Domain Controller  
 For security reasons, we recommend that you do not install [!INCLUDE[ssCurrent](../../includes/ssnoversion-md.md)] on a domain controller. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup will not block installation on a computer that is a domain controller, but the following limitations apply:  
  
-   You cannot run [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] services on a domain controller under a local service account.    
-   After [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is installed on a computer, you cannot change the computer from a domain member to a domain controller. You must uninstall [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] before you change the host computer to a domain controller.    
-   After [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is installed on a computer, you cannot change the computer from a domain controller to a domain member. You must uninstall [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] before you change the host computer to a domain member.   
-   [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] failover cluster instances are not supported where cluster nodes are domain controllers.   
- [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is not supported on a read-only domain controller. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup cannot create security groups or provision [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service accounts on a read-only domain controller. In this scenario, Setup will fail. 

  > [!NOTE]
  > This restriction also applies to installations on domain member nodes.

- A [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] failover cluster instance is not supported in an environment where only a read-only domain controller is accessible. 

  > [!NOTE]
  > This restriction also applies to installations on domain member nodes.

## Installation media

You can get relevant installation media from the following locations: 
  
- [SQL Server evaluation center](https://www.microsoft.com/evalcenter/evaluate-sql-server-2017-rtm)
- [Most recent cumulative updates](../../database-engine/install-windows/latest-updates-for-microsoft-sql-server.md)

Alternatively, you can create an [Azure virtual machine already running SQL Server](/azure/azure-sql/virtual-machines/windows/sql-vm-create-portal-quickstart) though SQL Server on a virtual machine will be slower than running natively because of the overhead of virtualization.
  
  
## Next steps

Once you've reviewed the hardware and software requirements for installing SQL Server, you can start to [Plan a SQL Server Installation](../../sql-server/install/planning-a-sql-server-installation.md) or review the [Security considerations for SQL Server](../../sql-server/install/security-considerations-for-a-sql-server-installation.md).
