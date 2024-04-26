---
title: Configure
description: Explains how to manage SQL Server enabled by Azure Arc configuration options.
author: anosov1960
ms.author: sashan
ms.reviewer: mikeray, randolphwest
ms.date: 04/26/2024
ms.topic: conceptual
---

# Configure SQL Server enabled by Azure Arc

[!INCLUDE [sqlserver](../../includes/applies-to-version/sqlserver.md)]

Each Azure Arc-enabled server includes a set of properties that apply to all SQL Server instances installed in that server. You can configure these properties after the Azure extension for SQL Server is installed on the machine. However, the properties only take effect if a SQL Server instance or instances are installed. In Azure portal, the [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)] **Overview** reflects how the SQL Server Configuration affects a particular instance.

Azure portal SQL Server Configuration allows you to perform the following management tasks:

1. [Manage licensing and billing of SQL Server enabled by Azure Arc](manage-license-billing.md)
1. [Set the Extended Security Updates property](#set-the-extended-security-updates-property)
1. [Add to the excluded instances list](#add-to-the-excluded-instances-list)

## Prerequisites

- You're in a [Contributor role](/azure/role-based-access-control/built-in-roles#contributor) in at least one of the Azure subscriptions your organization created. Learn how to [create a new billing subscription](/azure/cloud-adoption-framework/ready/azure-best-practices/initial-subscriptions).

- You're in a [Contributor role](/azure/role-based-access-control/built-in-roles#contributor) for the resource group in which the SQL Server instance will be registered. See [Managed Azure resource groups](/azure/azure-resource-manager/management/manage-resource-groups-portal) for details.

- The `Microsoft.AzureArcData` and `Microsoft.HybridCompute` resource providers are registered in each subscription you use for SQL Server pay-as-you-go billing.

### Register resource providers

To register the resource providers, use one of the following methods:

### [Azure portal](#tab/azure)

1. Select **Subscriptions**
1. Choose your subscription
1. Under **Settings**, select **Resource providers**
1. Search for `Microsoft.AzureArcData` and `Microsoft.HybridCompute` and select **Register**

### [PowerShell](#tab/powershell)

Run:

```powershell
Register-AzResourceProvider -ProviderNamespace Microsoft.AzureArcData
```

### [Azure CLI](#tab/az)

Run:

```azurecli
az provider register --namespace 'Microsoft.AzureArcData'
```

---

## Modify SQL Server configuration

You can use Azure portal, PowerShell, or CLI to change all or some configuration settings on a specific Arc-enabled server to the desired state.

To modify the SQL Server Configuration for a larger scope, such as a resource group, subscription, or multiple subscriptions with a single command, use the [`modify-license-type.ps1`](https://github.com/microsoft/sql-server-samples/tree/master/samples/manage/azure-arc-enabled-sql-server/modify-license-type) PowerShell script. It's published as an open source SQL Server sample and includes the step-by-step instructions.

> [!TIP]  
> Run the script from Azure Cloud shell because:
>  
> - It has the required Azure PowerShell modules pre-installed
> - It automatically authenticates you
>  
> For details, see [Running the script using Cloud Shell](https://github.com/microsoft/sql-server-samples/tree/master/samples/manage/azure-arc-enabled-sql-server/modify-license-type#running-the-script-using-cloud-shell).

### [Azure portal](#tab/azure)

There are two ways to configure the SQL Server host in Azure portal.

- Open the Arc-enabled Server overview page and select **SQL Server Configuration** as shown.

   :::image type="content" source="media/billing/overview-of-sql-server-azure-arc.png" alt-text="Screenshot of the SQL Server enabled by Azure Arc in Azure portal." lightbox="media/billing/overview-of-sql-server-azure-arc.png":::

  Or

- Open the Arc-enabled SQL Server overview page, and select **Properties**. Under **SQL Server configuration**, select the setting you need to modify:

  - **License type**
  - **ESU subscription**
  - **Automated updates**

   :::image type="content" source="media/billing/sql-server-instance-configuration.png" alt-text="Screenshot of Azure portal SQL Server instance configuration." lightbox="media/billing/sql-server-instance-configuration.png":::

#### Set license type property

Choose one of the license types. See [License types](manage-license-billing.md#license-types) for descriptions.

#### Set the Extended Security Updates property

Extended Security Updates (ESU) is available for qualified SQL Server instances that use License with Software assurance or Pay-as-you-go as the license type. If the license type is license only, the option to activate the ESU subscription is disabled. See [What are Extended Security Updates for SQL Server?](../end-of-support/sql-server-extended-security-updates.md).

> [!NOTE]  
>
> - To activate an ESU subscription, the license type must be set to Pay-as-you-go or License with Software assurance. If it's set to License only, the Extended Security Updates options will be disabled.
> - If ESU is enabled **License Type** can't be changed to `LicenseOnly` until the ESU subscription is canceled.

#### Apply physical core license

Select this checkbox if you're configuring a virtual machine, and you're using the unlimited virtualization benefit for licensing the SQL Server software or for your SQL subscription. If selected, the p-core takes precedence, and the SQL Server software costs, or ESU costs associated with this VM, are nullified.

> [!IMPORTANT]  
>
> 1. The UV benefit isn't supported for the VMs running on the listed providers' infrastructure. If you select this option for such a VM, this intent will be ignored and you'll be charged for the v-cores of the VM. See [Listed providers](https://aka.ms/listedproviders) for details.
> 1. If you're configuring a VM that isn't subject to the above restriction, make sure the selected **License type** matches the **Billing plan** configured in the p-core license resource.

#### Add to the excluded instances list

You can exclude certain instances from the at-scale onboarding operations driven by Azure policy or by automatic onboarding processes. To exclude specific instances from these operations, add the instance names to the **Skip Instances** list. For details about at-scale onboarding options, see [Alternate deployment options for SQL Server enabled by Azure Arc](deployment-options.md).

> [!CAUTION]  
> SQL Server instances using Pay-as-you-go (PAYG) can't be excluded.

#### Save the updated configuration

After you verify the license type, ESU setting, and any instance to exclude, select **Save** to apply changes.

### [PowerShell](#tab/powershell)

The following command sets the license type to `PAYG`, which enables the ESU subscription and add two instances to the exclusion list.

```powershell
# Updated settings object
$Settings = @{
    SqlManagement = @{ IsEnabled = $true };
    ExcludedSqlInstances = @( "Foo", "Bar");
    LicenseType = "PAYG";
    enableExtendedSecurityUpdates = $True;
    esuLastUpdatedTimestamp = [DateTime]::UtcNow.ToString('yyyy-MM-ddTHH:mm:ss.fffZ')
}

# Command stays the same as before, only settings is changed previously:
New-AzConnectedMachineExtension -Name "WindowsAgent.SqlServer" -ResourceGroupName { your resource group name } -MachineName { your machine name } -Location { azure region } -Publisher "Microsoft.AzureData" -Settings $Settings -ExtensionType "WindowsAgent.SqlServer"
```

The update command overwrites all settings. If your extension settings have a list of excluded SQL Server instances, make sure to specify the full exclusion list with the update command.

If you already have an older version of the Azure extension installed, make sure to upgrade it first, and then use one the modify methods to set the correct license type. For details, see [How to upgrade a machine extension](/azure/azure-arc/servers/manage-automatic-vm-extension-upgrade) for details.

### [Azure CLI](#tab/az)

The following command sets the license type to "PAYG":

```azurecli
az connectedmachine extension update --machine-name "simple-vm" -g "<resource-group>" --name "WindowsAgent.SqlServer" --type "WindowsAgent.SqlServer" --publisher "Microsoft.AzureData" --settings '{"LicenseType":"PAYG", "SqlManagement": {"IsEnabled":true}}'
```

The update command overwrites all settings. If your extension settings have a list of excluded SQL Server instances, make sure to specify the full exclusion list with the update command.

If you already have an older version of the Azure extension installed, make sure to upgrade it first, and then use one the modify methods to set the correct license type. For details, see [How to upgrade a machine extension](/azure/azure-arc/servers/manage-automatic-vm-extension-upgrade) for details.

---

## Query SQL Server configuration

You can use [Azure Resource Graph](/azure/governance/resource-graph/overview) to query the SQL Server configuration settings within a selected scope. See the following examples.

### Count by license type

This example returns the count by license type.

```kusto
resources
| where type == "microsoft.hybridcompute/machines/extensions"
| where properties.type in ("WindowsAgent.SqlServer","LinuxAgent.SqlServer")
| extend licenseType = iff(properties.settings.LicenseType == '', 'Configuration needed', properties.settings.LicenseType)
| summarize count() by tostring(licenseType)
```

### Identify instances where license type is undefined

This query returns a list of instances where the license type is null.

```kusto
resources
| where type == "microsoft.hybridcompute/machines/extensions"
| where properties.type in ("WindowsAgent.SqlServer","LinuxAgent.SqlServer")
| where isnull(properties.settings.LicenseType)
| project ['id'], resourceGroup, subscriptionId
```

#### List configuration details for each SQL Server instance

This query identifies many details about each instance, including the license type, ESU setting, and enabled features.

```kusto
resources
| where type == "microsoft.hybridcompute/machines"| where properties.detectedProperties.mssqldiscovered == "true"| extend machineIdHasSQLServerDiscovered = id
| project name, machineIdHasSQLServerDiscovered, resourceGroup, subscriptionId
| join kind= leftouter (
    resources
    | where type == "microsoft.hybridcompute/machines/extensions"    | where properties.type in ("WindowsAgent.SqlServer","LinuxAgent.SqlServer")
    | extend machineIdHasSQLServerExtensionInstalled = iff(id contains "/extensions/WindowsAgent.SqlServer" or id contains "/extensions/LinuxAgent.SqlServer", substring(id, 0, indexof(id, "/extensions/")), "")
    | project Extension_State = properties.provisioningState,
    License_Type = properties.settings.LicenseType,
    ESU = iff(notnull(properties.settings.enableExtendedSecurityUpdates), iff(properties.settings.enableExtendedSecurityUpdates == true,"enabled","disabled"), ""),
    Extension_Version = properties.instanceView.typeHandlerVersion,
    Excluded_instances = properties.ExcludedSqlInstances,
    Purview = iff(notnull(properties.settings.ExternalPolicyBasedAuthorization),"enabled",""),
    Entra = iff(notnull(properties.settings.AzureAD),"enabled",""),
    BPA = iff(notnull(properties.settings.AssessmentSettings),"enabled",""),
    machineIdHasSQLServerExtensionInstalled)on $left.machineIdHasSQLServerDiscovered == $right.machineIdHasSQLServerExtensionInstalled
| where isnotempty(machineIdHasSQLServerExtensionInstalled)
| project-away machineIdHasSQLServerDiscovered, machineIdHasSQLServerExtensionInstalled
```

#### List Arc-enabled servers with instances of SQL Server

This query identifies Azure Arc-enabled servers with SQL Server instances discovered on them.

```kusto
resources
| where type == "microsoft.hybridcompute/machines"
| where properties.detectedProperties.mssqldiscovered == "true"
//| summarize count()
```

This query returns Azure Arc-enabled servers that have SQL Server instances, but the Arc SQL Server extension isn't installed. This query only applies to Windows servers.

```kusto
resources
| where type == "microsoft.hybridcompute/machines"
| where properties.detectedProperties.mssqldiscovered == "true"
| project machineIdHasSQLServerDiscovered = id
| join kind= leftouter (
    resources
    | where type == "microsoft.hybridcompute/machines/extensions"
    | where properties.type == "WindowsAgent.SqlServer"
    | project machineIdHasSQLServerExtensionInstalled = substring(id, 0, indexof(id, "/extensions/WindowsAgent.SqlServer")))
on $left.machineIdHasSQLServerDiscovered == $right.machineIdHasSQLServerExtensionInstalled
| where isempty(machineIdHasSQLServerExtensionInstalled)
| project machineIdHasSQLServerDiscoveredButNotTheExtension = machineIdHasSQLServerDiscovered
```

For more examples of Azure Resource Graph Queries, see [Starter Resource Graph query samples](/azure/governance/resource-graph/samples/starter).

## Manage unlimited virtualization

To enable unlimited virtualization, SQL Server enabled by Azure Arc supports a special resource type: *SQLServerLicense*. This resource allows you to license many virtual machines with the installed SQL Server instances. For details of the licensing model, see [licensing SQL Server instances with unlimited virtualization](manage-license-billing.md#unlimited-virtualization).

### Prerequisites

Your RBAC role includes the following permissions:

- `Microsoft.AzureArcData/SqlLicenses/read`
- `Microsoft.AzureArcData/SqlLicenses/write`
- `Microsoft.Management/managementGroups/read`
- `Microsoft.Resources/subscriptions/read`
- `Microsoft.Resources/subscriptions/resourceGroups/read`
- `Microsoft.Support/supporttickets/write`

### <a id="create-license-resource"></a> Create SQL Server license

To create the SQL Server license resource, use one of the following methods:

### [Azure portal](#tab/azure)

1. Select **Azure Arc**
1. Under **Data Services**, select **SQL Server licenses**
1. Select **+Create**
1. Select **SQL Server physical core license**
1. Complete the creation wizard

### [PowerShell](#tab/powershell)

Instructions aren't available in PowerShell.

### [Azure CLI](#tab/az)

Run:

```azurecli
$subscriptionId="<sub id>"
$apiVersion="2024-03-01-preview"
$templateFile="sqlserverlicense.json"

$resourceGroupName="<rg-name>"
$uri = "https://management.azure.com/subscriptions/{0}/resourceGroups/{1}/providers/Microsoft.AzureArcData/sqlserverlicenses/{2}?api-version={3}" -f $subscriptionId, $resourceGroupName, $serverName, $apiVersion

az rest --method put --uri "$uri" --body "@$templateFile" --headers "Content-Type=application/json"
```

Here's an example of `sqlserverlicense.json` that creates a deactivated license.

```json
"location": "westeurope",
"properties": {
    "billingPlan": "PAYG",
    "physicalCores": 32,
    "activationState": "Deactivated",
    "scopeType": "ResourceGroup",
    "licenseCategory": "Core"
}
```

---

### <a id="change-license-resource"></a> Change SQL Server license properties

To change the SQL Server license property, for example activate it at a later date, use one of the following methods:

### [Azure portal](#tab/azure)

1. Select **Azure Arc**
1. Under **Data Services**, select **SQL Server licenses**
1. Select on the license in question
1. Select **Configure** under **Management**
1. Make the changes and select **Apply**

### [PowerShell](#tab/powershell)

Instructions aren't available in PowerShell.

### [Azure CLI](#tab/az)

Run:

```azurecli
$subscriptionId="<sub id>"
$apiVersion="2024-03-01-preview"
$templateFile="activate.json"

$resourceGroupName="<rg-name>"
$uri = "https://management.azure.com/subscriptions/{0}/resourceGroups/{1}/providers/Microsoft.AzureArcData/sqlserverlicenses/{2}?api-version={3}" -f $subscriptionId, $resourceGroupName, $serverName, $apiVersion

az rest --method patch --uri "$uri" --body "@$templateFile" --headers "Content-Type=application/json"
```

Here's the content of `activate.json` to activate the license.

```json
{
  "activationState": "Activated"
}
```

---

### Manage resources in scope

You can manage the resources in scope of a specific SQL Server physical core license using the following steps:

### [Azure portal](#tab/azure)

1. Select **Azure Arc**
1. Under **Data Services**, select **SQL Server licenses**
1. Select on the license in question
1. Select **Resources in scope** under **Management**

If the specific resources aren't configured to use this license (**Apply physical core license** column displays "NO"), you can change that:

1. Select the specific resources on the list
1. Select the **Apply license** tab
1. Read the disclaimer and select **Confirm**

### [PowerShell](#tab/powershell)

Instructions aren't available in PowerShell.

### [Azure CLI](#tab/az)

Instructions aren't available for `az`.

---

### List Arc-enabled servers in scope of the SQL Server license

This query lists all Azure Arc-enabled servers in scope of the license and the relevant properties of each.

```kusto
resources
        | where type =~ 'Microsoft.HybridCompute/machines'
        | where ('${scopeType}'!= 'Subscription' or subscriptionId == '${subscription}')
        | where ('${scopeType}' != 'ResourceGroup' or (resourceGroup == '${resourceGroup.toLowerCase()}' and subscriptionId == '${subscription}'))
        | extend status = tostring(properties.status)
        | where status =~ 'Connected'
        | join kind = leftouter
        (
        resources
        | where type =~ 'Microsoft.HybridCompute/machines/extensions'
        | where name == 'WindowsAgent.SqlServer' or name == 'LinuxAgent.SqlServer'
        | extend machineId = substring(id, 0, indexof(id, '/extensions'))
        | extend extensionId = id
        )
        on $left.id == $right.machineId
        | where isnotnull(extensionId)
        | project id, name, properties.status, resourceGroup, subscriptionId, Model = properties.detectedProperties.model, Manufacturer = properties.detectedProperties.manufacturer, kind, OSE = properties.osName, License_applied = properties1.settings.UsePhysicalCoreLicense.IsApplied
        |order by name asc
```

## Related content

- [Manage licensing and billing of SQL Server enabled by Azure Arc](manage-license-billing.md)
- [SQL Server 2022 Pricing](https://www.microsoft.com/sql-server/sql-server-2022-pricing)
- [Install SQL Server 2022 using the pay-as-you-go activation option](../../database-engine/install-windows/install-sql-server.md)
- [What are Extended Security Updates for SQL Server?](../end-of-support/sql-server-extended-security-updates.md)
- [Frequently asked questions](faq.yml#billing)
- [Configure automatic updates for SQL Server instances enabled for Azure Arc](update.md)
