---
title: "What are Extended Security Updates?"
description: Learn about Extended Security Updates enabled by Azure Arc, for your end-of-support and end-of-life SQL Server products such as SQL Server 2012.
author: rwestMSFT
ms.author: randolphwest
ms.date: 04/26/2024
ms.service: sql
ms.subservice: install
ms.topic: conceptual
ms.custom:
  - references_regions
monikerRange: ">=sql-server-2016"
---

# What are Extended Security Updates for SQL Server?

[!INCLUDE [SQL Server end of support](../../includes/applies-to-version/sql-migration-end-of-support.md)]

This article provides information how to receive Extended Security Updates (ESUs) for versions of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] that are out of extended support.

Extended Security Updates (ESUs) are available for [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)].

[!INCLUDE [esu-table](includes/esu-table.md)]

ESUs are made available **if needed**, once a security vulnerability is discovered and is rated as **Critical** by the [Microsoft Security Response Center (MSRC)](https://msrc.microsoft.com/update-guide). Therefore, there's no regular release cadence for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] ESUs.

ESUs don't include:

- New features
- Functional improvements
- Customer-requested fixes

For information about ESU pricing, see [Plan your Windows Server and SQL Server end of support](https://www.microsoft.com/windows-server/extended-security-updates).

For more information about other options, see [SQL Server end of support options](sql-server-end-of-support-overview.md).

You can also review the [Frequently asked questions](extended-security-updates-frequently-asked-questions.md).

## Overview

When [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] reaches the end of its support lifecycle, you can sign up for an Extended Security Update (ESU) subscription. The subscription protects your servers for up to three years after the support lifecycle ends. Keep the subscription until you're ready to upgrade to a newer version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or [migrate to Azure SQL](/azure/azure-sql/migration-guides/).

The method of receiving Extended Security Updates depends on where your [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is running.

### Azure

If you migrate to an Azure service (for more information, see the [Overview](#overview) section), you'll have access to ESUs for [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] and [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] for up to three years after the end of support, at **no additional charge** above the cost of running the Azure service.

Azure services running [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] receive ESUs automatically through existing [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] update channels or Windows Update. You don't need to install the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] IaaS Agent extension to download ESU patches on an Azure SQL Virtual Machine.

Services include:

- SQL Server on Azure VMs
- Azure VMware Solution (AVS)
- Azure Stack Hub
- Azure Stack HCI

### On-premises or hosted environments

If you deploy your SQL Server instances to an Azure service, you can access ESUs for [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] and [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] for up to three years after the end of support, at no additional charge above the cost of running the Azure service. This applies to SQL Server on Azure VMs, Azure VMware Solution, Azure Stack Hub, or Azure Stack HCI.

> [!NOTE]  
> Azure Stack HCI customers must [enable Azure benefits](/azure-stack/hci/manage/azure-benefits?#enable-azure-benefits) to receive free ESUs.

To configure ESUs in Azure VMware Solution, review [ESUs for SQL Server and Windows Server in Azure VMware Solution VMs](/azure/azure-vmware/extended-security-updates-windows-sql-server).

In all other cases, you can purchase Extended Security Updates if you qualify. To qualify for receiving Extended Security Updates (ESU), you must have Software Assurance under one of the following agreements:

- Enterprise Agreement (EA)
- Enterprise Agreement Subscription (EAS)
- Server and Cloud Enrollment (SCE)
- Enrollment for Education Solutions (EES)

You can also qualify by connecting your [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] and [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] instances to Azure Arc, and enable a pay-as-you-go billing option. For more information, see [Automatically connect your SQL Server to Azure Arc](../azure-arc/automatically-connect.md).

There are two ways to purchase ESUs:

- You can purchase an Extended Security Update plan for up to three years after the end of support date directly from [!INCLUDE [msCoName](../../includes/msconame-md.md)] or a [!INCLUDE [msCoName](../../includes/msconame-md.md)] licensing partner. For more information, see [Register Extended Security Updates purchased through volume licensing](#register-instances-for-esus).

- If your [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] and [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] instances are connected to Azure Arc, you can enable ESUs as a subscription. For more information, see [Subscribe to Extended Security Updates enabled by Azure Arc](#subscribe-instances-for-esus).

The following table shows the differences between the two options:

| Option | How to purchase | Key features |
| --- | --- | --- |
| **ESU plan** | Volume licensing center | - Supports SQL Server instances both connected and not connected to Azure Arc<br />- Each year of coverage must be purchased separately, must be paid in full, and is differently priced<br />- Requires registration on Azure portal<br />- Supports manual installation of patches |
| **ESU subscription** | Microsoft Azure | - The covered SQL Server instances must be connected to Azure Arc<br />- Continuous coverage until canceled<br />- Billed by Azure on an hourly basis<br />- Can be manually canceled at any time<br />- Automatic cancellation when migrated to Azure or upgraded to a supported version<br />- Supports automatic and manual installation of patches |

> [!NOTE]  
> Connecting or registering instances is free of charge. Both *connected* and *registered* instances don't incur additional charges when downloading ESUs, which are delivered through the Azure portal.

For more information, see the [Extended Security Updates frequently asked questions](https://www.microsoft.com/windows-server/extended-security-updates).

## Support

ESUs don't include technical support for either on-premises or hosted environments. For on-premises environments, you can receive technical support on workloads covered by ESUs through additional active support contracts such as [Software Assurance](https://www.microsoft.com/licensing/licensing-programs/software-assurance-default) or Premier/Unified Support. Alternatively, if you're hosting on Azure, you can use an Azure Support plan to get technical support.

## <a id="subscribe-instances-for-esus"></a> Subscribe to Extended Security Updates enabled by Azure Arc

SQL Server enabled by Azure Arc supports a method of receiving ESUs as a subscription billed through Azure. For details, see [SQL Server ESU subscriptions enabled by Azure Arc](../azure-arc/extended-security-updates.md).

## <a id="register-instances-for-esus"></a> Register Extended Security Updates purchased through volume licensing

If you purchased an ESU product through volume licensing (VL) for the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instances that aren't connected to Azure Arc, you must first register these servers on the Azure portal and  link the ESU invoice as proof of purchase.

## <a id="register-instances-on-azure-portal"></a> Register disconnected SQL Server instances on Azure portal

If your on-premises or hosted environment [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instances can't be connected to Azure Arc, you can manually register your [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instances in the Azure portal to enable access to the ESUs. If you prefer to take advantage of the flexibility of Extended Security Updates enabled by Azure Arc, connect your server to Azure Arc. To connect, follow the steps in [Automatically connect your SQL Server to Azure Arc](../azure-arc/automatically-connect.md).

The following example shows how to manually register your [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instances in a disconnected state, in the Azure portal.

### Prerequisites

1. If you don't already have an Azure subscription, you can create an account using one of the following methods:

   - [Create a Microsoft Customer Agreement subscription](/azure/cost-management-billing/manage/create-subscription)
   - [Create an Enterprise Agreement subscription](/azure/cost-management-billing/manage/create-enterprise-subscription)
   - [Create an Azure account with pay-as-you-go pricing](https://azure.microsoft.com/pricing/purchase-options/pay-as-you-go/)
   - [Create a free Azure account](https://azure.microsoft.com/free/)

1. The user creating disconnected Arc-enabled [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] resources must have the following permissions:

   - `Microsoft.AzureArcData/sqlServerInstances/read`
   - `Microsoft.AzureArcData/sqlServerInstances/write`

   Users can be assigned to the `Azure Connected SQL Server Onboarding` role to get those specific permissions, or they can be assigned to built-in roles such as Contributor or Owner that have these permissions. For more information, see [Assign Azure roles using the Azure portal](/azure/role-based-access-control/role-assignments-portal).

1. Register the `Microsoft.AzureArcData` resource provider in your Azure subscription:

   - Sign in to the Azure portal.

   - Navigate to your subscription, and select **Resource providers**.

   - If the `Microsoft.AzureArcData` resource provider isn't listed, you can add it to your subscription using the **Register** option.

1. If you use Azure policies that only allow the creation of specific resource types, you need to allow the `Microsoft.AzureArcData/sqlServerInstances` resource type. If it isn't allowed, the `SQLServerInstances_Update` operation fails with a **'deny' Policy action** log entry in the activity log of the subscription.

You can either register a [single SQL Server instance](#single-sql-server-instance), or upload a CSV file to register [multiple SQL Server instances in bulk](#multiple-sql-server-instances-in-bulk).

### Single SQL Server instance

1. Sign into the [Azure portal](https://portal.azure.com).

1. Navigate to **Azure Arc** and select **Infrastructure** > **SQL Servers**.

1. To register a disconnected machine, select **Add** from the menu at the top of the screen.

   :::image type="content" source="media/sql-server-extended-security-updates/extended-security-updates-empty-list.png" alt-text="Screenshot of an empty list of SQL Servers on the Azure Arc portal." lightbox="media/sql-server-extended-security-updates/extended-security-updates-empty-list.png":::

1. Select **Register Servers** to add a disconnected [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance.

   :::image type="content" source="media/sql-server-extended-security-updates/extended-security-updates-add-connected-or-registered.png" alt-text="Screenshot of the two options for adding connected or registered servers." lightbox="media/sql-server-extended-security-updates/extended-security-updates-add-connected-or-registered.png":::

1. On the next screen, you can choose to add a single or multiple [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instances. The option for **Single SQL Instance** is selected by default.

   :::image type="content" source="media/sql-server-extended-security-updates/extended-security-updates-add-sql-registration-options.png" alt-text="Screenshot of the Add SQL Registrations options." lightbox="media/sql-server-extended-security-updates/extended-security-updates-add-sql-registration-options.png":::

1. Choose the **Subscription** and **Resource group** for your registered [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance.

1. Provide the required information as is detailed in this table, and then select **Next**:

   | Value | Description | Additional information |
   | --- | --- | --- |
   | **Instance Name** | Enter the output of command `SELECT @@SERVERNAME`, such as `MyServer\Instance01`. | If you have a named instance, you must replace the backslash (`\`) with a hyphen (`-`). For example, `MyServer\Instance01` becomes `MyServer-Instance01`. |
   | **SQL Server Version** | Select your version from the dropdown list. | |
   | **Edition** | Select the applicable edition from the dropdown list: Datacenter, Developer (free to deploy if purchased ESUs), Enterprise, Standard, Web, Workgroup. | |
   | **Cores** | Enter the number of cores for this instance | |
   | **Host Type** | Select the applicable host type from the dropdown list: Virtual machine (on-premises), Physical Server (on-premises), Azure Virtual Machine, Amazon EC2, Google Compute Engine, Other. | |

1. You must confirm that you have the rights to receive Extended Security Updates, using the checkbox provided. The ESU checkbox is only visible when you select [!INCLUDE [sssql11-md](../../includes/sssql11-md.md)].

### Multiple SQL Server instances in bulk

Multiple [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instances can be registered in bulk by uploading a .CSV file. Once your [.CSV file is formatted correctly](#formatting-requirements-for-csv-file), you can follow these steps to bulk register your [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instances with Azure Arc:

1. Sign into the [Azure portal](https://portal.azure.com).

1. Navigate to **Azure Arc** and select **Infrastructure** > **SQL Servers**.

1. To register a disconnected machine, select **Add** from the menu at the top of the screen.

   :::image type="content" source="media/sql-server-extended-security-updates/extended-security-updates-empty-list.png" alt-text="Screenshot of an empty list of SQL Servers on the Azure Arc portal." lightbox="media/sql-server-extended-security-updates/extended-security-updates-empty-list.png":::

1. Select **Register Servers** to add a disconnected [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance.

   :::image type="content" source="media/sql-server-extended-security-updates/extended-security-updates-add-connected-or-registered.png" alt-text="Screenshot of the two options for adding connected or registered servers." lightbox="media/sql-server-extended-security-updates/extended-security-updates-add-connected-or-registered.png":::

1. On this screen, you can choose to add a single or multiple [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instances. Select the option for **Multiple SQL Instances**.

   :::image type="content" source="media/sql-server-extended-security-updates/extended-security-updates-multiple-sql-instances.png" alt-text="Screenshot of the Multiple SQL Instances option." lightbox="media/sql-server-extended-security-updates/extended-security-updates-multiple-sql-instances.png":::

1. Select the Browse icon to upload the CSV file containing multiple disconnected [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instances.

1. You must confirm that you have the rights to receive Extended Security Updates, using the checkbox provided.

Once you add your [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instances, you'll see them in the portal after a few minutes. Because they were added manually, they always show in a disconnected state, with the description **Registered**.

   :::image type="content" source="media/sql-server-extended-security-updates/extended-security-updates-connected-servers.png" alt-text="Screenshot of two registered SQL Server instances on the Azure Arc portal." lightbox="media/sql-server-extended-security-updates/extended-security-updates-connected-servers.png":::

### Formatting requirements for CSV file

- Values are comma-separated
- Values aren't single or double-quoted
- Values can include letters, numbers, hyphens (`-`), and underscores (`_`). No other special characters can be used. If you have a named instance, you must replace the backslash (`\`) with a hyphen (`-`). For example, `MyServer\Instance01` becomes `MyServer-Instance01`.
- Column names are case-sensitive and must be named as follows:

  - name
  - version
  - edition
  - cores
  - hostType

#### Example CSV file

The CSV file should look like this:

```csv
name,version,edition,cores,hostType
Server1-SQL2012,SQL Server 2012,Enterprise,12,Other Physical Server
Server2-SQL2012,SQL Server 2012,Enterprise,24,Other Physical Server
Server3-SQL2012,SQL Server 2012,Enterprise,12,Azure Virtual Machine
Server4-SQL2012,SQL Server 2012,Standard,8,Azure VMware Solution
```

## Link ESU invoice

You can use the **Purchase Order Number** under Invoice Summary in their Microsoft invoice (as shown in the following screenshot) for the Invoice ID value to link the ESU purchase with the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instances.

:::image type="content" source="media/sql-server-extended-security-updates/extended-security-updates-invoice-sample.png" alt-text="Screenshot of Sample invoice with Purchase Order Number highlighted.":::

Follow these steps to link an ESU invoice to your Azure Arc [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instances to get access to extended updates. This example includes both **Connected** and **Registered** servers.

1. Sign into the [Azure portal](https://portal.azure.com).

1. Navigate to **Azure Arc** and select **SQL Server instances**.

1. Use the checkboxes next to each [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance you would like to link, and then select **Link ESU invoice**.

   :::image type="content" source="media/sql-server-extended-security-updates/extended-security-updates-invoice-select.png" alt-text="Screenshot of all SQL Server instances on the Azure Arc section." lightbox="media/sql-server-extended-security-updates/extended-security-updates-invoice-select.png":::

1. Fill in the ESU invoice number in the **Invoice ID** section, and then select **Link invoice**.

   :::image type="content" source="media/sql-server-extended-security-updates/extended-security-updates-invoice-save.png" alt-text="Screenshot of the invoice ID on the Link ESU invoice page." lightbox="media/sql-server-extended-security-updates/extended-security-updates-invoice-save.png":::

1. The servers you linked to the ESU invoice now show a valid ESU expiration date.

   :::image type="content" source="media/sql-server-extended-security-updates/extended-security-updates-invoice-linked.png" alt-text="Screenshot of SQL Server instances with a valid ESU expiration value." lightbox="media/sql-server-extended-security-updates/extended-security-updates-invoice-linked.png":::

> [!IMPORTANT]  
> When registering an ESU VL product for disconnected [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] servers, you should only select the instances with the **Status** of `Registered`.

## Download ESUs

Once your [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instances are registered with Azure Arc, you can download the Extended Security Update packages using the link found in the Azure portal, if and when they're made available.

To download ESUs, follow these steps:

1. Sign into the [Azure portal](https://portal.azure.com).

1. Navigate to **Azure Arc** and select **SQL Server instances**.

1. Select a server from the list.

   :::image type="content" source="media/sql-server-extended-security-updates/extended-security-updates-list-of-servers.png" alt-text="Screenshot of a list of servers, with one server highlighted." lightbox="media/sql-server-extended-security-updates/extended-security-updates-list-of-servers.png":::

1. Download security updates from here, if and when they're made available.

   :::image type="content" source="media/sql-server-extended-security-updates/extended-security-updates-available-updates.png" alt-text="Screenshot of available security updates." lightbox="media/sql-server-extended-security-updates/extended-security-updates-available-updates.png":::

## Supported regions

[!INCLUDE [azure-arc-data-regions](../azure-arc/includes/azure-arc-data-regions.md)]

Government regions aren't supported. For more information, see [Can customers get free Extended Security Updates on Azure Government regions?](extended-security-updates-frequently-asked-questions.md#can-i-get-free-extended-security-updates-on-azure-government-regions)

## Frequently asked questions

For the full list of frequently asked questions, review the [Extended Security Updates: Frequently asked questions](extended-security-updates-frequently-asked-questions.md).

## Related content

- [SQL Server 2012 lifecycle page](/lifecycle/products/microsoft-sql-server-2012)
- [SQL Server 2014 lifecycle page](/lifecycle/products/sql-server-2014)
- [SQL Server end of support page](sql-server-end-of-support-overview.md?WT.mc_id=akamseos)
- [Extended Security Updates frequently asked questions (FAQ)](/lifecycle/faq/extended-security-updates)
- [Microsoft Security Response Center (MSRC)](https://msrc.microsoft.com/security-guidance/summary)
- [Update Management overview](/azure/automation/update-management/overview)
- [Automated Patching for SQL Server on Azure virtual machines](/azure/azure-sql/virtual-machines/windows/automated-patching)
- [Microsoft Data Migration Guide](/data-migration/)
- [Azure migrate: lift-and-shift options to move your current SQL Server into an Azure VM](https://azure.microsoft.com/services/azure-migrate/)
- [Cloud adoption framework for SQL migration](/azure/cloud-adoption-framework/migrate/expanded-scope/sql-migration)
- [ESU-related scripts on GitHub](https://github.com/microsoft/sql-server-samples/tree/master/samples/manage/sql-server-extended-security-updates/scripts)
