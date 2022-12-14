---
title: "SQL Server 2019: Hardware & software requirements"
description: A list of hardware, software, and operating system requirements for installing and running SQL Server 2019. 
ms.custom: sqlfreshmay19
ms.date: 09/16/2021
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
# SQL Server 2019: Hardware and software requirements
[!INCLUDE [SQL Server -Windows Only](../../includes/applies-to-version/sql-windows-only.md)]

The article lists the minimum hardware and software requirements to install and run [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] on the Windows operating system.

For hardware and software requirements for other versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] , see:
- [[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 2016 and 2017](hardware-and-software-requirements-for-installing-sql-server.md)
- [[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]  on Linux](../../linux/sql-server-linux-setup.md#system)
- [Big data cluster](../../big-data-cluster/deployment-guidance.md)

##  <a name="pmosr"></a> Hardware requirements  
 The following memory and processor requirements apply to all editions of [!INCLUDE[ssCurrent](../../includes/ssnoversion-md.md)]:  
  
|Component|Requirement|  
|---------------|-----------------|  
|Hard Disk|[!INCLUDE[ssCurrent](../../includes/ssnoversion-md.md)] requires a minimum of 6 GB of available hard-disk space.<br/><br/> Disk space requirements will vary with the [!INCLUDE[ssCurrent](../../includes/ssnoversion-md.md)] components you install. For more information, see [Hard Disk Space Requirements](../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server.md#HardDiskSpace) later in this article. For information on supported storage types for data files, see [Storage Types for Data Files](../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server.md#StorageTypes).|  
|Monitor|[!INCLUDE[ssCurrent](../../includes/ssnoversion-md.md)] requires Super-VGA (800x600) or higher resolution monitor.|  
|Internet|Internet functionality requires Internet access (fees may apply).|  
|Memory \*|**Minimum:**<br/><br/> Express Editions: 512 MB<br/><br/> All other editions: 1 GB<br/><br/> **Recommended:**<br/><br/> Express Editions: 1 GB<br/><br/> All other editions: At least 4 GB and should be increased as database size increases to ensure optimal performance.|  
|Processor Speed|**Minimum:** x64 Processor: 1.4 GHz<br/><br/> **Recommended:** 2.0 GHz or faster|  
|Processor Type|x64 Processor: AMD Opteron, AMD Athlon 64, Intel Xeon with Intel EM64T support, Intel Pentium IV with EM64T support|  
  
> [!NOTE]  
> Installation of [!INCLUDE[ssCurrent](../../includes/ssnoversion-md.md)] is supported on x64 processors only. It is no longer supported on x86 processors.  
  
 \* The minimum memory required for installing the [!INCLUDE[ssDQSServer](../../includes/ssdqsserver-md.md)] component in [!INCLUDE[ssDQSnoversion](../../includes/ssdqsnoversion-md.md)] (DQS) is 2 GB of RAM, which is different from the [!INCLUDE[ssCurrent](../../includes/ssnoversion-md.md)] minimum memory requirement. For information about installing DQS, see [Install Data Quality Services](../../data-quality-services/install-windows/install-data-quality-services.md).  


##  <a name="hwswr"></a> Software requirements  

The following requirements apply to all installations:  
  
|Component|Requirement|  
|---------------|-----------------|  
|Operating system|Windows 10 TH1 1507 or greater<br/><br>Windows Server 2016 or greater<br/><br/>
|.NET Framework|Minimum operating systems includes minimum .NET framework.|  
|Network Software|Supported operating systems for [!INCLUDE[ssCurrent](../../includes/ssnoversion-md.md)] have built-in network software. Named and default instances of a stand-alone installation support the following network protocols: Shared memory, Named Pipes, and TCP/IP.<br/><br/> |  

[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup installs the following software components required by the product:  
  
   - [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client    
   - [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup support files  


> [!IMPORTANT]
> There are additional hardware and software requirements for the PolyBase feature. For more information, see [Get started with PolyBase](../../relational-databases/polybase/polybase-guide.md).  
  

## Operating system support

The following table shows which editions of [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] are compatible with which versions of Windows:  
  

| [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] edition:               | Enterprise | Developer | Standard | Web | Express |  
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
| Windows 11 IoT Enterprise         |    No      |    Yes    |    Yes   | No  |   Yes   |
| Windows 11 Enterprise             |    No      |    Yes    |    Yes   | No  |   Yes   |
| Windows 11 Professional           |    No      |    Yes    |    Yes   | No  |   Yes   |
| Windows 11 Home                   |    No      |    Yes    |    Yes   | No  |   Yes   |
| Windows 10 IoT Enterprise         |    No      |    Yes    |    Yes   | No  |   Yes   |
| Windows 10 Enterprise             |    No      |    Yes    |    Yes   | No  |   Yes   |
| Windows 10 Professional           |    No      |    Yes    |    Yes   | No  |   Yes   |
| Windows 10 Home                   |    No      |    Yes    |    Yes   | No  |   Yes   |

### Server core support

Installing [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] on Server Core mode is supported by the following editions of Windows Server:

:::row:::
    :::column:::
        Windows Server 2022 Core
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        Windows Server 2019 Core
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        Windows Server 2016 Core
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
    :::column-end:::
:::row-end:::

For more information on installing [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]  on Server Core, see [Install [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]  on Server Core](../../database-engine/install-windows/install-sql-server-on-server-core.md). 

> [!NOTE]  
> Installing SQL Server on a Windows OS on which case sensitivity is enabled is not supported. For more information review [SQL Server is not supported on a Windows operating system on which case sensitivity is enabled](/troubleshoot/sql/install/sql-server-not-supported-in-windows-os-where-case-sensitivity-enabled)

##  <a name="CrossLanguageSupport"></a> Cross-language support  
 For more information about cross-language support and considerations for installing [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in localized languages, see [Local Language Versions in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ](../../sql-server/install/local-language-versions-in-sql-server.md).  
  
##  <a name="HardDiskSpace"></a>  Disk space requirements  
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
  
##  <a name="StorageTypes"></a> Storage types for data files  
 The supported storage types for data files are:  
  
- Local Disk 
    - [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] currently supports disk drives that have standard native sector sizes of 512 bytes and 4 KB.  Hard disks with sector sizes larger than 4 KB may cause errors when attempting to store [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data files on them.  See [Hard disk drive sector-size support boundaries in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ](https://support.microsoft.com/kb/926930) for more information on hard disk sector-size support in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [Troubleshoot errors related to system disk sector size greater than 4 KB](/troubleshoot/sql/admin/troubleshoot-os-4kb-disk-sector-size).
    - [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] failover cluster installation supports Local Disk only for installing the tempdb files. Ensure that the path specified for the tempdb data and log files is valid on all the cluster nodes. During failover, if the tempdb directories are not available on the failover target node, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] resource will fail to come online.
- Shared Storage  
- [Storage Spaces Direct \(S2D\)](/windows-server/storage/storage-spaces/storage-spaces-direct-overview)  
- SMB File Share  
    - SMB storage is not supported for [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] data files for either standalone or clustered installations. Use direct attached storage, a storage area network, or S2D instead. 
    - SMB storage can be hosted by a Windows File Server or a third-party SMB storage device. If Windows File Server is used, the Windows File Server version should be 2008 or later. For more information about installing [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using SMB file share as a storage option, see [Install [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]  with SMB Fileshare as a Storage Option](../../database-engine/install-windows/install-sql-server-with-smb-fileshare-as-a-storage-option.md).  
  
  
  
##  <a name="DC_support"></a> Installing [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on a domain controller  
 For security reasons, we recommend that you do not install [!INCLUDE[ssCurrent](../../includes/ssnoversion-md.md)] on a domain controller. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup will not block installation on a computer that is a domain controller, but the following limitations apply:  
  
- You cannot run [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] services on a domain controller under a local service account.    
- After [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is installed on a computer, you cannot change the computer from a domain member to a domain controller. You must uninstall [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] before you change the host computer to a domain controller.    
- After [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is installed on a computer, you cannot change the computer from a domain controller to a domain member. You must uninstall [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] before you change the host computer to a domain member.   
- [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] failover cluster instances are not supported where cluster nodes are domain controllers.   
- [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is not supported on a read-only domain controller. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup cannot create security groups or provision [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service accounts on a read-only domain controller. In this scenario, Setup will fail. 
- A [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] failover cluster instance is not supported in an environment where only a read-only domain controller is accessible. 
  
## Installation media

You can get relevant installation media from the following locations: 
  
- [[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]  evaluation center](https://www.microsoft.com/evalcenter/evaluate-sql-server-2019)
- [Most recent cumulative updates](../../database-engine/install-windows/latest-updates-for-microsoft-sql-server.md)

Alternatively, you can create an [Azure virtual machine already running [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ](/azure/azure-sql/virtual-machines/windows/sql-vm-create-portal-quickstart) though [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]  on a virtual machine will be slower than running natively because of the overhead of virtualization.


## Next steps

Once you've reviewed the hardware and software requirements for installing [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] , you can start to [Plan a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]  Installation](../../sql-server/install/planning-a-sql-server-installation.md) or review the [Security considerations for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ](../../sql-server/install/security-considerations-for-a-sql-server-installation.md).
