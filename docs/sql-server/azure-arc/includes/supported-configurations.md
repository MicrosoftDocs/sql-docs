---
author: MashaMSFT
ms.author: mathoma
ms.date: 01/24/2024
ms.topic: include
---

## Supported configurations

### SQL Server version

[!INCLUDE [sssql14-md](../../../includes/sssql14-md.md)] and later versions.

> [!NOTE]
> Only 64-bit SQL Server versions are supported.
 
### Operating systems

- Windows 10 and 11
- [!INCLUDE [winserver2016-md](../../../includes/winserver2016-md.md)] and later versions
- Ubuntu 20.04 (x64)
- Red Hat Enterprise Linux (RHEL) 8 (x64)
- SUSE Linux Enterprise Server (SLES) 15 (x64)

### .NET Framework

On Windows, .NET Framework 4.7.2 and later.

This requirement starts with extension version `1.1.2504.99` (November, 14 2023 release). Without this version, the extension might not function as intended.

### Support on VMware

You can deploy SQL Server enabled by Azure Arc in VMware VMs running:

- On-premises
- In VMware solutions, for example:
  - Azure VMware Solution (AVS)

    [!INCLUDE [azure-vmware](azure-vmware.md)]

  - VMware Cloud on AWS
  - Google Cloud VMware Engine

#### VMware packaging and support scope

SQL Server enabled by Azure Arc supports SQL Server instances running on virtual machines hosted in VMware vSphere–based environments, including Azure VMware Solution.

Support doesn't depend on specific VMware commercial bundles, editions, or packaging. The following requirements determine support:

- The supported guest operating system
- The supported SQL Server version
- Azure Arc Connected Machine agent requirements

VMware (Broadcom) defines VMware packaging, licensing, and lifecycle policies and may change them independently of Azure Arc.
