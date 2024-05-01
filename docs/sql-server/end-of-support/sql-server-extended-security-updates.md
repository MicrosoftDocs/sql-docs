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

When [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] reaches the end of its support lifecycle, you can sign up for an Extended Security Update (ESU) subscription for your servers and remain protected for up to three years, until you're ready to upgrade to a newer version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or [migrate to Azure SQL](/azure/azure-sql/migration-guides/).

The method of receiving Extended Security Updates depends on where your [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is running.

### Azure workloads

If you migrate your workloads to an Azure service (for more information, see the [Overview](#overview) section), you'll have access to ESUs for [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] and [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] for up to three years after the end of support, at **no additional charge** above the cost of running the Azure service.

Azure services running [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] receive ESUs automatically through existing [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] update channels or Windows Update. You don't need to install the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] IaaS Agent extension to download ESU patches on an Azure SQL Virtual Machine.

For [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] workloads deployed to Nutanix Cloud Clusters, which operate on Azure bare-metal infrastructure, or for Azure Stack, follow the same process as on-premises or hosted environments not connected to Azure Arc.

If you deploy your SQL Server instances to an Azure service, you can access ESUs for [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] and [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] for up to three years after the end of support, at no additional charge above the cost of running the Azure service. Services include SQL Server on Azure VMs, Azure VMware Solution (AVS), Azure Stack Hub, or Azure Stack HCI.

On Azure VMware Solution, to receive free ESUs, you need to:

1. Deploy a resource bridge to manage AVS through Arc
1. Associate the Arc server with that AVS resource bridge

For details see:

- [Deploy Arc-enabled VMware vSphere for Azure VMware Solution private cloud](/azure/azure-vmware/deploy-arc-for-azure-vmware-solution)
- [Enable additional capabilities on Arc-enabled Server machines by linking to vCenter](/azure/azure-arc/vmware-vsphere/enable-virtual-hardware)

On Azure Stack HCI, to receive free ESUs [enable Azure benefits](/azure-stack/hci/manage/azure-benefits?#enable-azure-benefits).

### On-premises or hosted environments

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

If your on-premises or hosted environment [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instances are connected to Azure Arc, you can enable ESUs as a subscription, which provides you with the flexibility to cancel at any time without having to separately purchase an ESU subscription. The ESU subscription enables automated deployment of the patches as they're released.

You can subscribe to Extended Security Updates by modifying [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] configuration. For more information, see [Configure SQL Server enabled by Azure Arc](../azure-arc/manage-configuration.md).

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

The script preserves all the existing settings. It's published as an open source [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] sample and includes step-by-step instructions.

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
> If you disconnect your [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance from Azure Arc, the ESU charges stop, and you won't have access to the new ESUs. If you haven't manually canceled your ESU subscription using Azure portal or API, the access to ESUs are immediately restored once you reconnect your [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance to Azure Arc, and the ESU charges resume. These charges include the time of disconnection. For more information about what happens when you disconnect your [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instances, see [Extended Security Updates: Frequently asked questions](extended-security-updates-frequently-asked-questions.md).

## Subscribe to Extended Security Updates at scale using Azure Policy

You can activate the ESU subscription on multiple Arc-enabled machines using an Azure policy definition called [Subscribe eligible Arc-enabled SQL Servers instances to Extended Security Updates](https://portal.azure.com/#view/Microsoft_Azure_Policy/PolicyDetail.ReactView/id/%2Fproviders%2FMicrosoft.Authorization%2FpolicyDefinitions%2Ff692cc79-76fb-4c61-8861-467e454ac6f8). When you create an assignment of this policy definition to a scope of your choice, it enables ESU on all Arc-enabled machines that have the Azure extension for SQL Server installed. If any of these machines have a qualified [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance, the ESU subscription is activated immediately.

Use the following steps to activate this policy:

1. Navigate to **Azure Policy** in the Azure portal and choose **Definitions**.
1. Search for *[Subscribe eligible Arc-enabled SQL Servers instances to Extended Security Updates](https://portal.azure.com/#view/Microsoft_Azure_Policy/PolicyDetail.ReactView/id/%2Fproviders%2FMicrosoft.Authorization%2FpolicyDefinitions%2Ff692cc79-76fb-4c61-8861-467e454ac6f8)* and right-click on the policy.
1. Select **Assign policy**.
1. Select a subscription and optionally a resource group as a scope.
1. Make sure the policy enforcement is set to **Enabled**.
1. On the **Parameters** tab, set the value of *Enable Extended Security Updates* to **True**.
1. On the **Remediation** tab:
   1. Select **Create remediation task** for this policy to be applied to existing resources. If not selected, the policy is applied to the newly created resources only.
   1. Select **Create a Managed Identity** and choose **System assigned managed identity** (recommended) or **User assigned managed identity**, which has *Azure Extension for SQL Server Deployment* and *Reader* permissions.
   1. Select the identity's location.
1. Select **Review + Create**.
1. Select **Create**.

## Understand ESU subscription billing

The ESU license extends support for critical updates for up to three more years. If you start the subscription after the end of support date, you must purchase the volume licensing offer or ESU subscription to cover any previous years. With ESU subscriptions, you have the additional benefit of canceling the subscription and all future charges without penalty at any time.

### Billing for SQL Server 2012 ESUs

Because the ESU subscription option was introduced in Year 2 of the [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] extended support period, you must have purchased the Year 1 Volume Licensing ESU offer, before signing up for the ESU subscription in Year 2. You can sign up for the ESU subscription at any time within year 2, and your bill reflects the cost of continuous ESU coverage. After you sign up for the ESU subscription, your next monthly bill includes a one-time billback charge for each machine hosting a [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] instance or instances with an active ESU subscription, from July 12, 2023, to the date of activation.

From this point, you're billed for each machine on an hourly basis. Both billback and regular hourly charges use the hourly rate *(core count) x (100% of year 2 ESU license price) / 730*. So, the size of the billback charge depends on how much time has passed since July 12, 2023, until the activation time. The following billing rules apply:

- If you install a [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] instance or instances on a virtual machine, and don't use the unlimited virtualization benefit, you're billed for the total number of virtual cores of the machine, with a minimum of four cores. If the virtual machine is eligible to receive failover rights, the virtual cores of that machine aren't billable.

- If you install a [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] instance or instances on a physical server without using virtual machines, you're billed for all physical cores of the machine, with a minimum of four cores. If the physical server is eligible to receive failover rights (subject to the SQL Server - Failover rights clause), the physical cores of that server aren't billable. For more information, see the [product terms](https://www.microsoft.com/licensing/terms/productoffering/MicrosoftAzure/eaeas#ServiceSpecificTerms).

For more information about [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] ESU pricing, see [Plan your Windows Server 2012/2012 R2 and SQL Server 2012 end-of-support](https://www.microsoft.com/windows-server/extended-security-updates).

### Billing for SQL Server 2014 ESUs

The ESU subscription for [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] is available from year 1 of the extended support period, which starts on July 10, 2024. You can sign up for it at any time before or after that date. If you sign up before that date, you only see the hourly ESU charges starting at midnight on July 10, 2024. If you sign up after July 10, 2024, your next month's bill includes a billback charge from July 10, 2024, to the date of activation. The following billing rules apply:

- If you install a [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] instance or instances on a virtual machine, and don't use the unlimited virtualization benefit, you're billed for the total number of virtual cores of the machine, with a minimum of four cores. If the virtual machine is eligible to receive failover rights, the virtual cores of that machine aren't billable.

- If you install a [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] instance or instances on a physical server without using virtual machines, you're billed for all physical cores of the machine, with a minimum of four cores. If the physical server is eligible to receive failover rights (subject to the SQL Server - Failover rights clause), the physical cores of that server aren't billable. For more information, see the [product terms](https://www.microsoft.com/licensing/terms/productoffering/MicrosoftAzure/eaeas#ServiceSpecificTerms).

- If you install both instances of [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] and [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] on the same physical or virtual machine, you're billed for the total number of physical or virtual cores of the machine, for both [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] and [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] ESU, with a minimum of four cores. The billing for each version is based on the ESU price for that version. If the virtual machine is eligible to receive failover rights, the virtual cores of that machine aren't billable.

For more information about [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] ESU pricing, see [Plan your Windows Server and SQL Server end of support](https://www.microsoft.com/windows-server/extended-security-updates).

### Billing during the connectivity loss and other disruptions

If your [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance loses connectivity, the billing stops, and the subscription is suspended. To make sure that intermittent disconnection doesn't negatively affect your ESU coverage, we automatically reactivate it if the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance reconnects within 30 days, without penalty. In that case, you see an additional billback charge for the days since the last day your server was connected. If you manually terminate the ESU subscription, and then reactivate it within 30 days, there's also no penalty. Your bill includes an additional charge for the time since you canceled the subscription. If the server reconnects after 30 days of disconnection, the subscription is terminated. To resume the ESU coverage, you need to activate a new ESU subscription and pay all the associated billback charges.

> [!IMPORTANT]  
> The billback charges are recorded within the first hour of the ESU subscription, and look like single hourly charges for the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instances that have the ESU subscriptions enabled. Because the amount reflects the accumulated costs since July 11, 2023 for [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)], or July 10, 2024 for [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)], it's much higher than the regular hourly ESU charges. This is expected, and it should be a one-time charge. During the following months you should only see the regular hourly charges. Additional billback charges could be added in cases of the connectivity disruptions, but they are typically much smaller amounts.

## View ESU subscriptions

You can use [Azure Resource Graph](/azure/governance/resource-graph/overview) to query the ESU subscriptions. The following example shows how you can view all eligible [!INCLUDE [sssql11-md](../../includes/sssql11-md.md)] instances and their ESU subscription status.

```kusto
resources
| where type == 'microsoft.azurearcdata/sqlserverinstances'
| extend Version = properties.version
| extend Edition = properties.edition
| extend containerId = tolower(tostring (properties.containerResourceId))
| where Version contains "2012"
| where Edition in ("Enterprise", "Standard")
| where isnotempty(containerId)
| project containerId, SQL_instance = name, Version, Edition
| join kind=inner (
    resources
    | where type == "microsoft.hybridcompute/machines"
    | extend machineId = tolower(tostring(id))
    | project machineId, Machine_name = name
)
on $left.containerId == $right.machineId
| join kind=inner (
    resources
    | where type == "microsoft.hybridcompute/machines/extensions"
    | where properties.type in ("WindowsAgent.SqlServer","LinuxAgent.SqlServer")
    | extend machineIdHasSQLServerExtensionInstalled = tolower(iff(id contains "/extensions/WindowsAgent.SqlServer" or id contains "/extensions/LinuxAgent.SqlServer", substring(id, 0, indexof(id, "/extensions/")), ""))
    | project machineIdHasSQLServerExtensionInstalled, Extension_State = properties.provisioningState, License_Type = properties.settings.LicenseType, ESU = iff(notnull(properties.settings.enableExtendedSecurityUpdates), iff(properties.settings.enableExtendedSecurityUpdates == true,"enabled","disabled"), ""), Extension_Version = properties.instanceView.typeHandlerVersion
)
on $left.machineId == $right.machineIdHasSQLServerExtensionInstalled
| project-away machineId, containerId, machineIdHasSQLServerExtensionInstalled
```

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

The script preserves all the existing settings. It's published as an open source [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] sample and includes step-by-step instructions.

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
> Don't cancel Extended Security Updates enabled by Azure Arc before or after migrating to Azure. When you migrate your on-premises [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instances to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] on Azure Virtual Machines or Azure VMware Solutions, the ESU charges will stop automatically, but you continue to have full access to the Extended Security Updates. For more information, see [Extended Security Updates: Frequently asked questions](extended-security-updates-frequently-asked-questions.md).

## <a id="register-instances-for-esus"></a> Register Extended Security Updates purchased through volume licensing

If you purchased an ESU product through volume licensing (VL), you must register the purchased product on the Azure portal to enable access to previous or future Extended Security Updates. If you purchased the ESU product for the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instances that aren't connected to Azure Arc, you must first register these servers on the Azure portal. If you purchased the ESU product for the Arc-enabled [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instances, you don't need to register these servers, because they're already connected to Azure Arc. To finalize the registration of the ESU VL product, you must link the ESU invoice.

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
> If you purchased an ESU VL product for disconnected [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] servers, you should only select the instances with the **Status** of `Registered`. If you purchased an ESU VL product for [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instances enabled by Azure Arc, you should only select the instances with the **Status** of `Connected`.

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
