---
title: "What are Extended Security Updates?"
description: Learn about Extended Security Updates enabled by Azure Arc, for your end-of-support and end-of-life SQL Server products such as SQL Server 2012.
author: rwestMSFT
ms.author: randolphwest
ms.date: 08/17/2023
ms.service: sql
ms.subservice: install
ms.topic: conceptual
ms.custom: references_regions
monikerRange: ">=sql-server-2016"
---
# What are Extended Security Updates for SQL Server?

[!INCLUDE [esu-table](includes/esu-table.md)]

Extended Security Updates (ESUs) are available for [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)].

This article provides information how to receive Extended Security Updates (ESUs) for versions of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] that are out of extended support.

ESUs are made available **if needed**, once a security vulnerability is discovered and is rated as **Critical** by the [Microsoft Security Response Center (MSRC)](https://portal.msrc.microsoft.com). Therefore, there's no regular release cadence for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] ESUs.

ESUs don't include:

- New features
- Functional improvements
- Customer-requested fixes

[!INCLUDE [SQL Server end of support](../../includes/applies-to-version/sql-migration-end-of-support.md)]

For information about ESU pricing, see [Plan your Windows Server 2012/2012 R2 and SQL Server 2012 end-of-support](https://www.microsoft.com/windows-server/extended-security-updates).

For more information about other options, see [End of support options](sql-server-end-of-support-overview.md).

You can also review the [Frequently asked questions](extended-security-updates-frequently-asked-questions.md).

## Overview

Once [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] has reached the end of its support lifecycle, you can sign up for an Extended Security Update (ESU) subscription for your servers and remain protected for up to three years, until you're ready to upgrade to a newer version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or [migrate to Azure SQL](/azure/azure-sql/migration-guides/).

The method of receiving Extended Security Updates depends on where your [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is running.

- **SQL Server on Azure Virtual Machines.** ESUs are free and enabled by default.

- **SQL Server on-premises or a hosted environment.** ESUs are free and enabled by default on the following Azure services:

  - Azure Stack HCI

  - Azure Stack Hub

- **SQL Server on-premises or a hosted environment, and connected to Azure Arc.** You can use Extended Security Updates enabled by Azure Arc to enable ESU as a monthly subscription. The updates are automatically installed when they're available. You also benefit from the features that [Azure Arc-enabled SQL Server](../azure-arc/overview.md) provides. If you migrate your [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to Azure or upgrade the subscription, charges then automatically stop. You can cancel the ESU subscription manually at any time.

- **SQL Server on-premises or in a hosted environment, and not connected to Azure Arc.** You can purchase the ESU SKU through the Volume Licensing Service Center (VLSC), and manually register your [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instances on the Azure portal to receive the patches. For more information, see  [Register disconnected SQL Server instances for ESUs](#register-instances-for-esus) later in this article.

  > [!NOTE]  
  > Connecting or registering instances is free of charge. Both *connected* and *registered* instances do not incur additional charges when downloading ESUs, which are delivered through the Azure portal.

[!INCLUDE [msCoName](../../includes/msconame-md.md)] recommends applying ESU patches as soon as they're available to keep your [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance protected. For detailed information about ESUs, see the [ESU FAQ page](https://www.microsoft.com/cloud-platform/extended-security-updates).

## Support

ESUs don't include technical support, but you can use an active support contract, such as [Software Assurance](https://www.microsoft.com/licensing/licensing-programs/software-assurance-default?activetab=software-assurance-default-pivot%3aprimaryr3), or Premier/Unified Support, to get technical support on workloads covered by ESUs if you choose to stay on-premises. Alternatively, if you're hosting on Azure, you can use an Azure Support plan to get technical support.

[!INCLUDE [msCoName](../../includes/msconame-md.md)] can't provide technical support for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instances (both on-premises, and in hosting environments) that aren't covered with an ESU subscription.

## ESU availability and deployment

ESUs are available to customers running their workload in Azure, on-premises, or hosted environments.

### Azure workloads

If you migrate your workloads to an Azure service (for more information, see the [Overview](#overview) section), you'll have access to ESUs for [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] for up to three years after the End of Support, at **no additional charge** above the cost of running the Azure service. You don't need Software Assurance to receive ESUs in Azure.

Azure services running [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] receive ESUs automatically through existing [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] update channels or Windows Update. You don't need to install the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] IaaS Agent extension to download ESU patches on an Azure SQL Virtual Machine.

> [!NOTE]  
> For [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] workloads deployed to Nutanix Cloud Clusters, which operate on Azure bare-metal infrastructure, or for Azure Stack, you must follow the same process as on-premises or hosted environments not connected to Azure Arc.

### On-premises or hosted environments connected to Azure Arc

If you have Software Assurance, you can use Extended Security Updates enabled by Azure Arc, to subscribe to ESUs for up to three years after the End of Support date, under one of the following agreements:

- Enterprise Agreement (EA)
- Enterprise Agreement Subscription (EAS)
- Server and Cloud Enrollment (SCE)
- Enrollment for Education Solutions (EES)

You are billed through your Azure subscription only for the servers that you enabled for ESUs, and only if they run an eligible version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. See instructions on how to [subscribe to Extended Security Updates enabled by Azure Arc](#subscribe-to-extended-security-updates-enabled-by-azure-arc) later in this article.

### On-premises or hosted environments not connected to Azure Arc

If you have Software Assurance, you can purchase an Extended Security Update (ESU) plan for up to three years after the End of Support date, under one of the following agreements:

- Enterprise Agreement (EA)
- Enterprise Agreement Subscription (EAS)
- Server and Cloud Enrollment (SCE)
- Enrollment for Education Solutions (EES)

You can purchase ESUs only for the servers you need to cover. ESUs can be purchased directly from [!INCLUDE [msCoName](../../includes/msconame-md.md)] or a [!INCLUDE [msCoName](../../includes/msconame-md.md)] licensing partner.

Customers covered by ESU agreements must follow these steps to download and deploy an ESU patch:

- Register [disconnected SQL Server instances](#register-instances-for-esus) for ESUs.
- Once registered, whenever ESU patches are released, a download link is provided in the Azure portal to download the package.
- The downloaded package can be deployed to your on-premises or hosted environments manually, or through the update orchestration solution you use in your organization, such as Microsoft Endpoint Configuration Manager.

For more information, see the [Extended Security Updates frequently asked questions](https://www.microsoft.com/cloud-platform/extended-security-updates).

## Subscribe to Extended Security Updates enabled by Azure Arc

If your on-premises or hosted environment [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instances are connected to Azure Arc, you can enable ESUs as a subscription, which provides you with the flexibility to cancel at any time without having to separately purchase an ESU SKU. The ESU subscription enables automated deployment of the patches as they are released.

You can subscribe to Extended Security Updates by modifying [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] configuration. See [Manage SQL Server Configuration](../azure-arc/manage-configuration.md)[Manage SQL Server Configuration].

### [Azure portal](#tab/portal)

The following steps subscribe to ESUs using the Azure portal:

1. When you connect your [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance to Azure Arc, you can see the **ESU status** option in the **Overview** pane. The default for all new instances is **N/A**.

   :::image type="content" source="media/sql-server-extended-security-updates/extended-security-updates-not-applicable.png" alt-text="Screenshot showing the Overview pane for a SQL Server instance. ESU status is highlighted.":::

1. Select **N/A**, and navigate to the **SQL Server Configuration** pane. In the **License Type** section, select **License with Software Assurance**.

   :::image type="content" source="media/sql-server-extended-security-updates/extended-security-updates-license-with-software-assurance.png" alt-text="Screenshot showing the option to select for Software Assurance." lightbox="media/sql-server-extended-security-updates/extended-security-updates-license-with-software-assurance.png":::

1. Select **Subscribe to Extended Security Updates**, and select **Save**.

   :::image type="content" source="media/sql-server-extended-security-updates/extended-security-updates-subscribe-save.png" alt-text="Screenshot showing the Subscribe to ESU option highlighted." lightbox="media/sql-server-extended-security-updates/extended-security-updates-subscribe-save.png":::

> [!NOTE]  
> To subscribe to Extended Security Updates, you must have License type set to Pay-as-you-go or License with Software assurance. Otherwise, the **Extended Security Updates** option will be disabled.

### [Azure PowerShell](#tab/powershell)

The following command enables the ESU subscription using Azure PowerShell. Replace the following values for your own environment:

- `<resource_group>`
- `<machine_name>`
- `<azure_region>`

```powershell
# Updated settings object
$Settings = @{ SqlManagement = @{ IsEnabled = $true }; enableSecurityUpdates = $true }
New-AzConnectedMachineExtension -Name "WindowsAgent.SqlServer" -ResourceGroupName { <resource_group> } -MachineName { <machine_name> } -Location { <azure_region> } -Publisher "Microsoft.AzureData" -Settings $Settings -ExtensionType "WindowsAgent.SqlServer"
```

> [!WARNING]  
> The update command overwrites all settings. If your extension settings have a list of excluded [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instances, you must specify the full exclusion list with the update command.

If you have multiple [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instances eligible for ESUs, you can subscribe in bulk using the [Modify License Type](https://github.com/microsoft/sql-server-samples/tree/master/samples/manage/azure-arc-enabled-sql-server/modify-license-type) PowerShell script, which allows you to configure the ESU setting for one of:

- all Azure Arc-enabled machines a specific resource group,
- an Azure subscription, or
- all Azure subscriptions your Azure account has access to.

The script preserves all the existing settings. It is published as an open source [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] sample and includes step-by-step instructions.

### [Azure CLI](#tab/cli)

The following command enables the ESU subscription using Azure CLI. Replace the following values for your own environment:

- `<machine_name>`
- `<resource_group>`

```azurecli
az connectedmachine extension update --machine-name "<machine_name>" -g "<resource_group>" --name "WindowsAgent.SqlServer" --type "WindowsAgent.SqlServer" --publisher "Microsoft.AzureData" --settings '{ enableSecurityUpdates=$true, "SqlManagement": {"IsEnabled":true} }'
```

> [!WARNING]  
> The update command overwrites all settings. If your extension settings have a list of excluded [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instances, you must specify the full exclusion list with the update command.

---

> [!IMPORTANT]  
> If you disconnect your [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance from Azure Arc, the ESU charges stop, and you won't have access to the new ESUs. If you haven't manually canceled your ESU subscription using Azure portal or API, the access to ESUs are immediately restored once you reconnect your [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance to Azure Arc, and the ESU charges resume. These charges include the time of disconnection. For more information about what happens when you disconnect your [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instances, see [Frequently asked questions](extended-security-updates-frequently-asked-questions.md).

## Cancel Extended Security Updates enabled by Azure Arc

You can cancel Extended Security Updates enabled by Azure Arc at any time using the Azure portal, Azure PowerShell, or Azure CLI. The cancellation immediately stops the ESU charges.

### [Azure portal](#tab/portal)

The following steps cancel the ESU subscription using the Azure portal:

1. Navigate to your connected [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance. The ESU status option in the Overview pane shows the value **Enabled**.

1. Select **Enabled**, and navigate to the **SQL Server Configuration** pane.

1. In the **Extended Security Updates** section, select **Unsubscribe from to Extended Security Updates**, then select **Save**.

### [Azure PowerShell](#tab/powershell)

The following command cancels the ESU subscription using Azure PowerShell. Replace the following values for your own environment:

- `<resource_group>`
- `<machine_name>`
- `<azure_region>`

```powershell
# Updated settings object
$Settings = @{ SqlManagement = @{ IsEnabled = $true }; enableSecurityUpdates = $false }
New-AzConnectedMachineExtension -Name "WindowsAgent.SqlServer" -ResourceGroupName { <resource_group> } -MachineName { <machine_name> } -Location { <azure_region> } -Publisher "Microsoft.AzureData" -Settings $Settings -ExtensionType "WindowsAgent.SqlServer"
```

> [!WARNING]  
> The update command overwrites all settings. If your extension settings have a list of excluded [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instances, you must specify the full exclusion list with the update command.

If you have multiple [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instances eligible for ESUs, you can cancel in bulk using the [Modify License Type](https://github.com/microsoft/sql-server-samples/tree/master/samples/manage/azure-arc-enabled-sql-server/modify-license-type) PowerShell script, which allows you to configure the ESU setting for one of:

- all Azure Arc-enabled machines a specific resource group,
- an Azure subscription, or
- all Azure subscriptions your Azure account has access to.

The script preserves all the existing settings. It is published as an open source [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] sample and includes step-by-step instructions.

### [Azure CLI](#tab/cli)

The following command cancels the ESU subscription using Azure CLI. Replace the following values for your own environment:

- `<machine_name>`
- `<resource_group>`

```azurecli
az connectedmachine extension update --machine-name "<machine_name>" -g "<resource_group>" --name "WindowsAgent.SqlServer" --type "WindowsAgent.SqlServer" --publisher "Microsoft.AzureData" --settings '{ enableSecurityUpdates=$false, "SqlManagement": {"IsEnabled":true} }'
```

> [!WARNING]  
> The update command overwrites all settings. If your extension settings have a list of excluded [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instances, you must specify the full exclusion list with the update command.

---

> [!IMPORTANT]
> Don't cancel Extended Security Updates enabled by Azure Arc before or after migrating to Azure. When you migrate your on-premises [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instances to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] on Azure Virtual Machines or Azure VMware Solutions, the ESU charges will stop automatically, but you will continue to have full access to the Extended Security Updates. For more information, see [Extended Security Updates: frequently asked questions](extended-security-updates-frequently-asked-questions.md).

## <a id="register-instances-for-esus"></a> Register Extended Security Updates purchased through volume licensing

If you purchased an ESU product through volume licensing (VL), you must register it to enable access to previous or future Extended Security Updates. If you purchased the ESU product for the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instances that are not connected to Azure Arc, you must first register these servers on the Azure portal. If you purchased the ESU product for the Arc-enabled [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instances, you don't need to register these servers as they are already connected to Azure Arc. To finalize the registration of the ESU VL product, you must link the ESU invoice.

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

   :::image type="content" source="media/sql-server-extended-security-updates/extended-security-updates-empty-list.png" alt-text="Screenshot of an empty list of SQL Servers list on the Azure Arc portal." lightbox="media/sql-server-extended-security-updates/extended-security-updates-empty-list.png":::

1. Select **Register Servers** to add a disconnected [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance.

   :::image type="content" source="media/sql-server-extended-security-updates/extended-security-updates-add-connected-or-registered.png" alt-text="Screenshot of the two options for adding connected or registered servers.":::

1. On the next screen, you can choose to add a single or multiple [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instances. The option for **Single SQL Instance** is selected by default.

   :::image type="content" source="media/sql-server-extended-security-updates/extended-security-updates-add-sql-registration-options.png" alt-text="Screenshot of the Add SQL Registrations options.":::

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

Now you can continue to the [Confirmation](#confirmation) section.

### Multiple SQL Server instances in bulk

Multiple [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instances can be registered in bulk by uploading a .CSV file. Once your [.CSV file has been formatted correctly](#formatting-requirements-for-csv-file), you can follow these steps to bulk register your [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instances with Azure Arc:

1. Sign into the [Azure portal](https://portal.azure.com).

1. Navigate to **Azure Arc** and select **Infrastructure** > **SQL Servers**.

1. To register a disconnected machine, select **Add** from the menu at the top of the screen.

   :::image type="content" source="media/sql-server-extended-security-updates/extended-security-updates-empty-list.png" alt-text="Screenshot of an empty list of SQL Servers list on the Azure Arc portal." lightbox="media/sql-server-extended-security-updates/extended-security-updates-empty-list.png":::

1. Select **Register Servers** to add a disconnected [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance.

   :::image type="content" source="media/sql-server-extended-security-updates/extended-security-updates-add-connected-or-registered.png" alt-text="Screenshot of the two options for adding connected or registered servers.":::

1. On this screen, you can choose to add a single or multiple [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instances. Select the option for **Multiple SQL Instances**.

   :::image type="content" source="media/sql-server-extended-security-updates/extended-security-updates-multiple-sql-instances.png" alt-text="Screenshot of the Multiple SQL Instances option.":::

1. Select the Browse icon to upload the CSV file containing multiple disconnected [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instances.

1. You must confirm that you have the rights to receive Extended Security Updates, using the checkbox provided.

Now you can continue to the [Confirmation](#confirmation) section.

### Confirmation

1. We recommend you use a year-specific tag to link your [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instances to your ESU invoice number for easy reference. For example:

   - First year: `Year1OrderID`
   - Second year: `Year2OrderID`
   - Third year: `Year3OrderID`

   > [!NOTE]  
   > If you use Azure services such as Azure Dedicated Host, Azure VMware Solution, Azure Nutanix Solution, and Azure Stack (Hub, Edge, and HCI), you can set the ESU invoice number to `InvoiceNotNeeded`.

   The `Year2EntitlementConfirmed` tag is automatically filled in.

   :::image type="content" source="media/sql-server-extended-security-updates/extended-security-updates-tags.png" alt-text="Screenshot of confirmation tags.":::

1. Before you can add your [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instances, you must agree to the terms of use and privacy policy.

   :::image type="content" source="media/sql-server-extended-security-updates/extended-security-updates-terms.png" alt-text="Screenshot of the terms of use.":::

1. Once you've added your [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instances, you'll see them in the portal after a few minutes. Because they were added manually, they always show in a disconnected state, with the description **Registered**.

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

You can use the **Purchase Order Number** under Invoice Summary in their Microsoft invoice (as shown in the following screenshot) to link the ESU purchase with the registration of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instances.

:::image type="content" source="media/sql-server-extended-security-updates/extended-security-updates-invoice-sample.png" alt-text="Sample invoice with Purchase Order Number highlighted.":::

Follow these steps to link an ESU invoice to your Azure Arc [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instances to get access to extended updates. This example includes both **Connected** and **Registered** servers.

1. Sign into the [Azure portal](https://portal.azure.com).

1. Navigate to **Azure Arc** and select **SQL Server instances**.

1. Use the checkboxes next to each SQL server you would like to link, and then select **Link ESU invoice**.

   :::image type="content" source="media/sql-server-extended-security-updates/extended-security-updates-invoice-select.png" alt-text="Screenshot of all SQL Server instances on the Azure Arc section." lightbox="media/sql-server-extended-security-updates/extended-security-updates-invoice-select.png":::

1. Fill in the ESU invoice number in the **Invoice ID** section, and then select **Link invoice**.

   :::image type="content" source="media/sql-server-extended-security-updates/extended-security-updates-invoice-save.png" alt-text="Screenshot of the invoice ID on the Link ESU invoice page." lightbox="media/sql-server-extended-security-updates/extended-security-updates-invoice-save.png":::

1. The servers you linked to the ESU invoice now show a valid ESU expiration date.

   :::image type="content" source="media/sql-server-extended-security-updates/extended-security-updates-invoice-linked.png" alt-text="Screenshot of SQL Server instances with a valid ESU expiration value." lightbox="media/sql-server-extended-security-updates/extended-security-updates-invoice-linked.png":::

> [!IMPORTANT]
> If you purchased an ESU VL product for disconnected SQL Servers, you should only select the instances with the **Status** = `Registered`. If you purchased an ESU VL product for Arc-enabled SQL Servers, you should only select the instances with the **Status** = `Connected`.

## Download ESUs

Once your [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instances have been registered with Azure Arc, you can download the Extended Security Update packages using the link found in the Azure portal, if and when they're made available.

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

For the full list of frequently asked questions, review the [ESU frequently asked questions](extended-security-updates-frequently-asked-questions.md).

## See also

- [SQL Server 2012 lifecycle page](/lifecycle/products/microsoft-sql-server-2012)
- [SQL Server end of support page](sql-server-end-of-support-overview.md?WT.mc_id=akamseos)
- [Extended Security Updates frequently asked questions (FAQ)](https://aka.ms/sqleosfaq)
- [Microsoft Security Response Center (MSRC)](https://portal.msrc.microsoft.com/security-guidance/summary)
- [Manage Windows updates by using Azure Automation](/azure/automation/update-management/overview)
- [SQL Server VM automated patching](/azure/azure-sql/virtual-machines/windows/automated-patching)
- [Microsoft Data Migration Guide](/data-migration/)
- [Azure migrate: lift-and-shift options to move your current SQL Server into an Azure VM](https://azure.microsoft.com/services/azure-migrate/)
- [Cloud adoption framework for SQL migration](/azure/cloud-adoption-framework/migrate/expanded-scope/sql-migration)
- [ESU-related scripts on GitHub](https://github.com/microsoft/sql-server-samples/tree/master/samples/manage/sql-server-extended-security-updates/scripts)
