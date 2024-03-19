---
author: MikeRayMSFT
ms.author: mikeray
ms.date: 01/24/2024
ms.topic: include
ms.custom: ignite-2023
---

Azure Arc-enabled [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] doesn't currently support the following configurations:

- [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] running in containers.
- [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] roles other than the Database Engine, such as Analysis Services (SSAS), Reporting Services (SSRS), or Integration Services (SSIS).
- [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] editions: Business Intelligence.
- Private Link connections to the Azure Arc data processing service at the `<region>.arcdataservices.com` endpoint used for inventory and usage upload.
- [!INCLUDE [sql2008-md](../../../includes/sql2008-md.md)], [!INCLUDE [sql2008r2-md](../../../includes/sql2008r2-md.md)], and older versions.
- Installing the Arc agent and [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] extension can't be done as part of sysprep image creation.
- Multiple instances of [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] installed on the same host operating system with the same instance name.
- [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] in Azure Virtual Machines.
- An Always On availability group where one or more replicas is on a failover cluster instance.
