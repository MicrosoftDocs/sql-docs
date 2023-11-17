---
author: MikeRayMSFT
ms.author: mikeray
ms.date: 10/03/2023
ms.topic: include
ms.custom: ignite-2023
---

Azure Arc-enabled [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] doesn't currently support the following configurations:

- [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] running in containers.
- [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] roles other than the Database Engine, such as Analysis Services (SSAS), Reporting Services (SSRS), or Integration Services (SSIS).
- [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] editions: Business Intelligence.
- Private Link connections to the Azure Arc data processing service at the `san-af-<region>-prod.azurewebsites.net` endpoint used for inventory and usage upload.
- [!INCLUDE [sql2008-md](../../../includes/sql2008-md.md)], [!INCLUDE [sql2008r2-md](../../../includes/sql2008r2-md.md)], and older versions.
- Installing the Arc agent and [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] extension can't be done as part of sysprep image creation.
- Multiple instances of [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] installed on the same host operating system with the same instance name.
- [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] in Azure Virtual Machines.
- [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] Always On availability group on failover cluster instances.
- The rare combination of availability group on failover cluster instances.

> [!NOTE]  
> Azure extension for [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] fully supports VMware clusters outside of Azure. Although the [!INCLUDE [sssql22-md](../../../includes/sssql22-md.md)] Setup Installation Wizard does not support installation of the Azure extension for [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)], this component can be installed from the command line in quiet mode, or by connecting [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] to Azure Arc. For more information, see [Install and connect to Azure](../../../database-engine/install-windows/install-sql-server-from-the-command-prompt.md#install-and-connect-to-azure) and [Automatically connect your SQL Server to Azure Arc](../automatically-connect.md).
