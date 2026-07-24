---
title: Extended Security Updates (ESUs)
description: Extended Security Updates (ESUs) for SQL Server on Azure Virtual Machines.
author: dplessMSFT
ms.author: dpless
ms.reviewer: mathoma, randolphwest
ms.date: 05/01/2026
ms.service: azure-vm-sql-server
ms.subservice: management
ms.topic: concept-article
ms.custom: references_regions
tags: azure-service-management
---
# Extended security updates (ESUs) for SQL Server on Azure Virtual Machines

[!INCLUDE[appliesto-sqlvm](../../includes/appliesto-sqlvm.md)]

This article describes how you can receive Extended Security Updates (ESUs) for SQL Server on Azure VM workloads.

Both [!INCLUDE [sssql14-md](../../../docs/includes/sssql14-md.md)] and [!INCLUDE [sssql16-md](../../../docs/includes/sssql16-md.md)] have reached the end of their life cycle:
- [SQL Server 2014 end of its support (EOS) life cycle](/lifecycle/products/sql-server-2014).
- [SQL Server 2016 end of its support (EOS) life cycle](/lifecycle/products/sql-server-2016). 

## Changes to SQL Server 2016 ESUs

Starting with [!INCLUDE [sssql16-md](../../../docs/includes/sssql16-md.md)], ESUs are available through a paid subscription. Migrating to SQL Server on Azure VMs no longer provides free access to ESUs for [!INCLUDE [sssql16-md](../../../docs/includes/sssql16-md.md)] instances.

While ESUs for SQL Server 2014 VMs remain free of charge, the ESU subscription process is the same for every version of SQL Server.

> [!TIP]
> Alternatively, upgrade your workload to SQL Server 2017 or later versions, or migrate to either Azure SQL Managed Instance or the Hyperscale service tier of Azure SQL Database to avoid the need for ESUs. For more information, see [SQL Server end of support options](/sql/sql-server/end-of-support/sql-server-end-of-support-overview).

## Provisioning

To subscribe to ESUs, customers who use an earlier version of SQL Server need to either self-install or upgrade to SQL Server 2014 or SQL Server 2016. Likewise, customers who use an earlier version of Windows Server need to either deploy their VM from a custom VHD or upgrade to Windows Server 2012 R2 or a later version.

Images deployed through Azure Marketplace come with the SQL IaaS Agent extension preinstalled. Customers who deploy self-installed VMs need to manually register with the SQL IaaS Agent extension: 
- [Register on Windows](sql-agent-extension-manually-register-single-vm.md)
- [Register on Linux](../linux/sql-iaas-agent-extension-register-vm-linux.md)

## Subscribe to ESUs

For ESU access, register your SQL Server VM instance with the [SQL IaaS Agent extension](sql-agent-extension-manually-register-single-vm.md) starting with version `2.0.227.1` or later.

After you register with the SQL IaaS Agent extension, you can subscribe to receive ESUs.

To subscribe to ESUs in the Azure portal, follow these steps:

1. Go to the [virtual machines](https://portal.azure.com/#view/Microsoft_Azure_ComputeHub/ComputeHubMenuBlade/~/virtualMachinesBrowse) pane in the Azure portal.

1. Select your SQL Server VM from the list:

   :::image type="content" source="media/extended-security-updates-sql-vm/select-vm.png" alt-text="Screenshot of a list of servers, with one server highlighted." lightbox="media/extended-security-updates-sql-vm/select-vm.png":::

1. Under **Settings**, select **SQL Server configuration** where you can select the **Subscribe to Extended Security Updates** option:

   :::image type="content" source="media/extended-security-updates-sql-vm/extended-security-updates-available-updates.png" alt-text="Screenshot of available security updates." lightbox="media/extended-security-updates-sql-vm/extended-security-updates-available-updates.png":::

1. Select **Save**.

When you subscribe, the VM is enabled for ESU patch installation. You can deliver patches through any patching product or service that installs Microsoft patches, such as Microsoft Update, Windows Update, System Center Configuration Manager, or Azure Update Manager. VMs that have automatic updates enabled through the SQL IaaS Agent extension receive ESU updates automatically.

You can also manually download ESUs from the **Extended Security Updates** pane for your [SQL Server instance](https://portal.azure.com/#servicemenu/SqlAzureExtension/AzureSqlHub/SqlServerInstance) in the Azure portal.

## Licensing

For SQL Server VM licensing details, review the [SQL Server licensing guide](https://www.microsoft.com/licensing/guidance/SQL?msockid=115cad88d78e69e1137fbbebd6016861). 

## Supported Azure regions

You can subscribe to ESUs for SQL Server on Azure VMs only in the supported regions listed in this section. To subscribe to ESUs in an unsupported region, contact Microsoft Support to determine the appropriate ESU acquisition path.

You can get ESUs for SQL Server on Azure VMs in the following supported regions:

#### [Americas](#tab/americas)

- Brazil South
- Canada Central
- Canada East
- Central US
- East US
- East US 2
- North Central US
- South Central US
- West Central US
- West US
- West US 2
- West US 3

#### [Asia Pacific](#tab/asia)

- Australia East
- Central India
- Japan East
- Korea Central
- Southeast Asia

#### [Europe, the Middle East, and Africa](#tab/emea)

- France Central
- North Europe
- Norway East
- South Africa North
- Sweden Central
- Switzerland North
- UAE North
- UK South
- UK West
- West Europe

---

## Related content

To learn more, see [end of support](/sql/sql-server/end-of-support/sql-server-end-of-support-overview) options and [Extended Security Updates](/sql/sql-server/end-of-support/sql-server-extended-security-updates).

- [Migration guide: SQL Server to SQL Server on Azure Virtual Machines](../../migration-guides/virtual-machines/sql-server-to-sql-on-azure-vm-individual-databases-guide.md)
- [Create a SQL Server VM in the Azure portal](sql-vm-create-portal-quickstart.md)
- [FAQ for SQL Server on Azure Virtual Machines](frequently-asked-questions-faq.yml)


