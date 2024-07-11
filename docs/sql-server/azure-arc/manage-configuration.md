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

#### <a id="set-license-type"></a> Set license type property

Choose one of the license types. See [License types](manage-license-billing.md#license-types) for descriptions.

  
#### <a id="use-physical-core-license"></a>  Use physical core license

Select this checkbox if you're configuring a virtual machine, and you're using the unlimited virtualization benefit for licensing the SQL Server software or for your SQL subscription. It will set the Host configuration property `UsePhysicalCoreLicense` to `True`. If selected, the p-core license takes precedence, and the SQL Server software costs will be nullified.

> [!IMPORTANT]
> If the physical core license is configured with a pay-as-you-go billing plan, the selected **License type** should be set as pay-as-you-go. This will not trigger additional charges at the VM level, but it will ensure the uninterrupted licensing and billing in an event of the p-core license's de-activation or deletion. 
#### <a id="subscribe-esu"></a> Subscribe to Extended Security Updates

This section allows you to subscribe to Extended Security Updates (ESU) for the individual host. To qualify for ESU subscription, the host must have **License type** set to **Pay-as-you-go** or **License with Software assurance**. This option allows you to subscribe using vCPUs (v-cores) when the host is a virtual machine, or physical cores when the host is a physical server running without using virtual machines. Select **Subscribe to Extended Security Updates**. It will set the Host configuration property `EnabelExtendedSecurityUpdates` to `True`. The subscription will be activated after you click **Save**.

For more information about ESU licensing options, see [Subscribe to Extended Security Updates in production environment](extended-security-updates.md#subscribe-to-extended-security-updates-in-production-environment). 

> [!NOTE]
- Unlike the p-core ESU license, when subscribing to ESU for a host you don't need to define the number of billable cores for each machine. Azure extension for SQL Server detects the size and type of the host (virtual or physical), SQL Server edition, and bills according to these parameters.
> - Once ESU is enabled, **License Type** of the host cannot be changed to **License only** until the ESU subscription is canceled.
#### <a id="use-physical-core-esu-license"></a> Use physical core ESU license

Select this checkbox if you're configuring a virtual machine, and you're using the unlimited virtualization benefit when enabling the ESU subscription. It sets `UseEsuPhysicalCoreLicense` to `true`. If selected, the p-core license takes precedence, and the SQL Server ESU charges at the VM level will be nullified.

#### <a id="unsubscribe-esu"></a> Unsubscribe from Extended Security Updates

You can cancel Extended Security Updates enabled by Azure Arc at any time. The cancellation immediately stops the ESU charges. Select **Unsubscribe from to Extended Security Updates**. The subscription will be terminated after you click **Save**.

#### <a id="add-excluded"></a> Add to the excluded instances list

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

> [!WARNING]  
> The update command overwrites all settings. E.g., if your extension settings have a list of excluded [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instances, you must specify the full exclusion list with the update command.

If you have multiple [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instances eligible for ESUs, you can cancel in bulk using the script to [Modify License Type](https://github.com/microsoft/sql-server-samples/tree/master/samples/manage/azure-arc-enabled-sql-server/modify-license-type). This script allows you to configure the ESU setting for one of:

- All Azure Arc-enabled machines a specific resource group.
- An Azure subscription.
- All Azure subscriptions your Azure account has access to.

The script preserves all the existing settings. It's published as an open source [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] sample and includes step-by-step instructions.

### [Azure CLI](#tab/az)

The following command sets the license type to "PAYG":

```azurecli
az connectedmachine extension update --machine-name "simple-vm" -g "<resource-group>" --name "WindowsAgent.SqlServer" --type "WindowsAgent.SqlServer" --publisher "Microsoft.AzureData" --settings '{"LicenseType":"PAYG", "SqlManagement": {"IsEnabled":true}}'
```

The update command overwrites all settings. If your extension settings have a list of excluded SQL Server instances, make sure to specify the full exclusion list with the update command.

If you already have an older version of the Azure extension installed, make sure to upgrade it first, and then use one the modify methods to set the correct license type. For details, see [How to upgrade a machine extension](/azure/azure-arc/servers/manage-automatic-vm-extension-upgrade) for details.

---

> [!IMPORTANT]  
> The unlimited virtualization benefit for SQL Server software or SQL Server ESU subscription is not supported on the listed providers' infrastructure. If you are running SQL Server in a listed provider's VM and select this option, your intent will be ignored and you'll be charged for the v-cores of the VM. See [Listed providers](https://aka.ms/listedproviders) for details.

## <a id="subscribe-esu-via-policy"></a> Subscribe to Extended Security Updates at scale using Azure Policy

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
| where type == "microsoft.hybridcompute/machines"
| where properties.detectedProperties.mssqldiscovered == "true"
| extend machineIdHasSQLServerDiscovered = id
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

#### List Arc-enabled SQL Server instances subscribed to ESU

The following example shows how you can view all eligible [!INCLUDE [sssql11-md](../../includes/sssql11-md.md)] or [!INCLUDE [sssql14-md](../../includes/sssql14-md.md)] instances and their ESU subscription status.

```kusto
resources
| where type == 'microsoft.azurearcdata/sqlserverinstances'
| extend Version = properties.version
| extend Edition = properties.edition
| extend containerId = tolower(tostring (properties.containerResourceId))
| where Version in ("2012", "2014")
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

#### List Arc-enabled servers that host a billable SQL Server instance

This query identifies the machines (virtual or physical) that host SQL Server instances that are billable or require a license for SQL Server software. It provides the details of the SQL Server configuration, including the license type, ESU setting, size in v-core or p-cores and other relevant parameters.

```kusto
resources
| where type =~ 'Microsoft.HybridCompute/machines'
| extend status = tostring(properties.status)
| where status =~ 'Connected'
| extend machineID = tolower(id)
| extend VMbyManufacturer = toboolean(iff(properties.detectedProperties.manufacturer in (
        "VMware",
        "QEMU",
        "Amazon EC2",
        "OpenStack",
        "Hetzner",
        "Mission Critical Cloud",
        "DigitalOcean",
        "UpCloud",
        "oVirt",
        "Alibaba",
        "KubeVirt",
        "Parallels",
        "XEN"
    ), 1, 0))
| extend VMbyModel = toboolean(iff(properties.detectedProperties.model in (
        "OpenStack",
        "Droplet",
        "oVirt",
        "Hypervisor",
        "Virtual",
        "BHYVE",
        "KVM"
    ), 1, 0))
| extend GoogleVM = toboolean(iff((properties.detectedProperties.manufacturer =~ "Google") and (properties.detectedProperties.model =~ "Google Compute Engine"), 1, 0))
| extend NutanixVM = toboolean(iff((properties.detectedProperties.manufacturer =~ "Nutanix") and (properties.detectedProperties.model =~ "AHV"), 1, 0))
| extend MicrosoftVM = toboolean(iff((properties.detectedProperties.manufacturer =~ "Microsoft Corporation") and (properties.detectedProperties.model =~ "Virtual Machine"), 1, 0))
| extend billableCores = iff(VMbyManufacturer or VMbyModel or GoogleVM or NutanixVM or MicrosoftVM, properties.detectedProperties.logicalCoreCount, properties.detectedProperties.coreCount)        
| join kind = leftouter // Join Extension
        (
        resources
        | where type =~ 'Microsoft.HybridCompute/machines/extensions'
        | where name == 'WindowsAgent.SqlServer' or name == 'LinuxAgent.SqlServer'
        | extend extMachineID = substring(id, 0, indexof(id, '/extensions'))
        | extend extensionId = id
        )
        on $left.id == $right.extMachineID
        | join kind = inner       // Join SQL Arc
            (
            resources
            | where type =~ 'microsoft.azurearcdata/sqlserverinstances'
            | extend sqlVersion = tostring(properties.version)
            | extend sqlEdition = tostring(properties.edition) 
            | extend is_Enterprise = toint(iff(sqlEdition == "Enterprise", 1, 0))
            | extend sqlStatus = tostring(properties.status)
            | extend licenseType = tostring(properties.licenseType)
            | where sqlEdition in ('Enterprise', 'Standard') 
            | where licenseType !~ 'HADR'
            | where sqlStatus =~ "Connected"
            | extend ArcServer = tolower(tostring(properties.containerResourceId))
            | order by sqlEdition
            )
            on $left.machineID == $right.ArcServer
            | where isnotnull(extensionId)
            | summarize Edition = iff(sum(is_Enterprise) > 0, "Enterprise", "Standard") by machineID
            , name
            , resourceGroup
            , subscriptionId
            , Model = tostring(properties.detectedProperties.model)
            , Manufacturer = tostring(properties.detectedProperties.manufacturer)
            , License_Type = tostring(properties1.settings.LicenseType)
            , OS = tostring(properties.osName)
            , Uses_UV = tostring(properties1.settings.UsePhysicalCoreLicense.IsApplied)
            , Cores = tostring(billableCores)
            , Version = sqlVersion
            | project-away machineID
            | order by Edition, name asc
   ```

## <a id="manage-pcore-license"></a> Manage unlimited virtualization benefit for SQL Server

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

### <a id="change-license-resource"></a> Update SQL Server license resource

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

### <a id="manage-license-resources"></a> Manage resources in scope of the p-core license

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

## <a id="manage-pcore-esu-license"></a> Manage unlimited virtualization benefit for SQL Server ESU subscription

To enable unlimited virtualization for ESU subscription, SQL Server enabled by Azure Arc supports a special resource type: *SQLServerEsuLicense*. This resource allows you to enable an ESU subscription for a set of physical hosts with unlimited number of virtual machines running the out-of-support SQL Server instances. For details of the licensing model, see [Subscribe to SQL Server ESUs by physical cores with unlimited virtualization](extended-security-updates.md#unlimited-virtualization).

### Prerequisites

Your RBAC role includes the following permissions:

- `Microsoft.AzureArcData/SqlLicenses/read`
- `Microsoft.AzureArcData/SqlLicenses/write`
- `Microsoft.Management/managementGroups/read`
- `Microsoft.Resources/subscriptions/read`
- `Microsoft.Resources/subscriptions/resourceGroups/read`
- `Microsoft.Support/supporttickets/write`

### <a id="create-esu-license-resource"></a> Create SQL Server ESU license resource

To create the SQL Server ESU license resource, use one of the following methods:

### [Azure portal](#tab/azure)

1. Select **Azure Arc**
1. Under **Data Services**, select **SQL Server ESU licenses**
1. Select **+Create**
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
$uri = "https://management.azure.com/subscriptions/{0}/resourceGroups/{1}/providers/Microsoft.AzureArcData/SqlServerEsuLicenses/{2}?api-version={3}" -f $subscriptionId, $resourceGroupName, $serverName, $apiVersion

az rest --method put --uri "$uri" --body "@$templateFile" --headers "Content-Type=application/json"
```

Here's an example of `sqlserverlicense.json` that creates a deactivated license.

```json
"location": "westeurope",
"properties": {
    "billingPlan": "PAYG",
    "physicalCores": 32,
    "activationState": "Active",
    "scopeType": "ResourceGroup",
    "version": "2014"
}
```

---

### <a id="change-esu-license-resource"></a> Update SQL Server ESU license resource

To change the SQL Server ESu license properties, for example terminate the subscription, use one of the following methods:

### [Azure portal](#tab/azure)

1. Select **Azure Arc**
1. Under **Data Services**, select **SQL Server ESU licenses**
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
$templateFile="terminate.json"

$resourceGroupName="<rg-name>"
$uri = "https://management.azure.com/subscriptions/{0}/resourceGroups/{1}/providers/Microsoft.AzureArcData/SqlServerEsuLicenses/{2}?api-version={3}" -f $subscriptionId, $resourceGroupName, $serverName, $apiVersion

az rest --method patch --uri "$uri" --body "@$templateFile" --headers "Content-Type=application/json"
```

Here's the content of `terminate.json` to activate the license.

```json
{
  "activationState": "Terminated"
}
```

---

### <a id="manage-esu-license-resources"></a> Manage resources in scope of the ESU p-core license


You can manage the resources in scope of a specific SQL Server ESU license using the following steps:

### [Azure portal](#tab/azure)

1. Select **Azure Arc**
1. Under **Data Services**, select **SQL Server ESU licenses**
1. Select on the license in question
1. Select **Resources in scope** under **Management**

This view will only show the connected machines in scope that host an out-of-service SQL Server instance with the version that matches the version of the p-core ESU license you are managing.   If the specific resources aren't configured to use this license (**Physical core license applied** column displays "NO"), you can change that:

1. Select the specific resources on the list
1. Select the **Subscribe to ESUs** tab to subscribe or the **Unsubscribe from ESUs** to unsubscribe.
1. Read the disclaimer and select **Confirm**

### [PowerShell](#tab/powershell)

Instructions aren't available in PowerShell.

### [Azure CLI](#tab/az)

Instructions aren't available for `az`.

---

### List Arc-enabled servers in scope of the SQL Server ESU license

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
        | project id, name, properties.status, resourceGroup, subscriptionId, Model = properties.detectedProperties.model, Manufacturer = properties.detectedProperties.manufacturer, kind, OSE = properties.osName, License_applied = properties1.settings.UseEsuPhysicalCoreLicense.IsApplied
        |order by name asc
```

## Related content

- [Manage licensing and billing of SQL Server enabled by Azure Arc](manage-license-billing.md)
- [SQL Server 2022 Pricing](https://www.microsoft.com/sql-server/sql-server-2022-pricing)
- [Install SQL Server 2022 using the pay-as-you-go activation option](../../database-engine/install-windows/install-sql-server.md)
- [What are Extended Security Updates for SQL Server?](../end-of-support/sql-server-extended-security-updates.md)
- [Frequently asked questions](faq.yml#billing)

- [Configure automatic updates for SQL Server instances enabled for Azure Arc](update.md)
