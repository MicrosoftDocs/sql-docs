---
author: MikeRayMSFT
ms.author: mikeray
ms.date: 01/24/2024
ms.topic: include
---

[!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] enabled by Azure Arc supports [!INCLUDE [sssql11-md](../../../includes/sssql11-md.md)] and later versions, running on one of the following versions of the Windows or Linux operating system:

- [!INCLUDE [winserver2012-md](../../../includes/winserver2012-md.md)] and later versions
- Ubuntu 20.04 (x64)
- Red Hat Enterprise Linux (RHEL) 8 (x64)
- SUSE Linux Enterprise Server (SLES) 15 (x64)

Support for Azure Arc-enabled [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] failover cluster instances (FCI) is introduced as preview in October, 2023.

> [!IMPORTANT]  
> Windows Server 2012 and Windows Server 2012 R2 support ended on October 10, 2023. For more information, see [SQL Server 2012 and Windows Server 2012/2012 R2 end of support](/lifecycle/announcements/sql-server-2012-windows-server-2012-2012-r2-end-of-support).

> [!NOTE]
> Since extension version `1.1.2504.99`, the minimum required version of the .NET Framework to run the extension is 4.7.2. Without this version, the extension might not function as intended. Note that Windows Server 2012 R2 does not come with .NET Framework 4.7.2 by default and must be updated accordingly.

### Support on VMware

You can deploy SQL Server enabled by Azure Arc in VMware VMs running:

- On-premises
- In VMware solutions, for example:
  - Azure VMware Solution (AVS)

    [!INCLUDE [azure-vmware](azure-vmware.md)]

  - VMware Cloud on AWS
  - Google Cloud VMware Engine
