---
title: Manage SQL Server configuration
titleSuffix: Azure Arc-enabled SQL Server
description: Explains how to manage SQL Server licensing options. Also demonstrates how Azure Arc-enabled SQL Server can be billed from Microsoft Azure. Use to enable pay as you go licensing.
author: anosov1960
ms.author: sashan
ms.reviewer: mikeray, randolphwest
ms.date: 08/17/2023
ms.topic: conceptual
---
# SQL Server Configuration

Each Arc-enabled server includes a set of properties that apply to all SQL Server instances installed in that server. These properties can be configured once the Azure extension for SQL Server is installed on the machine, but they only take effect if a SQL Server instance or instances are installed. The Arc-enabled SQL Server overview blade will reflect how the SQL Server Configuration effects a particular instance. 

SQL Server Configuration allows you to perform the following management tasks:

1. Configure SQL Server license type
1. Subscribe to Extended Security Updates
1. Exclude SQL Server instances from onboarding to Azure Arc

## Prerequisites

* You're in a [Contributor role](/azure/role-based-access-control/built-in-roles#contributor) in at least one of the Azure subscriptions your organization created. Learn how to [create a new billing subscription](/azure/cloud-adoption-framework/ready/azure-best-practices/initial-subscriptions).
* You're in a [Contributor role](/azure/role-based-access-control/built-in-roles#contributor) for the resource group in which the SQL Server instance will be registered. See [Managed Azure resource groups](/azure/azure-resource-manager/management/manage-resource-groups-portal) for details.
* The **Microsoft.AzureArcData** and **Microsoft.HybridCompute** resource providers are registered in each subscription you use for SQL Server pay-as-you-go billing.

To register the resource providers, use one of the methods below:  

### [Azure portal](#tab/azure)

1. Select **Subscriptions** 
2. Choose your subscription
3. Under **Settings**, select **Resource providers**
4. Search for `Microsoft.AzureArcData` and `Microsoft.HybridCompute` and select **Register**

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

## License types

[!INCLUDE [sqlserver](../../includes/applies-to-version/sqlserver.md)]

SQL Server license type identifies the type of license for SQL Server instances on a specific virtual machine or physical server. It includes an option to pay for the SQL software usage directly through Microsoft Azure using a pay-as-you-go billing.

**License type** is a required parameter when you install Azure Extension for SQL Server and each supported onboarding method includes the license type options. It allows you to track your SQL Server license inventory from using Azure Resource Graph. You can also track the software usage in the Cost Management + Billing portal.

For your convenience, the overview blade of each Arc-enabled SQL Server resource shows the license type under **Host License Type**.

> [!NOTE]
> [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] allows you to select a pay-as-you-go billing option during setup.

The following license types are supported:

| License type | Long description | Short description |  
|---|---|---|
| PAYG | Standard or Enterprise edition with pay-as-you-go billing through Microsoft Azure | Pay-as-you-go |
| Paid | Standard or Enterprise edition license with Software Assurance or SQL Subscription  | License with software assurance |
| LicenseOnly | Developer, Evaluation, Express, Web, Standard or Enterprise edition license only without Software Assurance | License only |

* **PAYG**: Pay for your SQL Server software usage through Microsoft Azure. See [SQL Server prices and licensing](https://www.microsoft.com/sql-server/sql-server-2022-pricing).

  > [!IMPORTANT]
  > For correct billing, servers that use **PAYG** license type should stay continuously connected to Azure. 
  >
  > Intermittent connectivity disruptions are tolerated with built-in resilience.

* **Paid** and **LicenseOnly**: Use an existing license agreement. Usage implies that you already have the necessary licenses. In these cases, your software usage will be reported to you using a free meter. You can analyze your usage in the [Cost Management + Billing portal](/azure/cost-management-billing/) to make sure you have enough licenses for all your installed SQL Server instances.

[!INCLUDE [license-types](includes/license-types.md)]

### Billing for SQL Server software

The value of **License Type** indicates if you already have a SQL Server license or prefer paying for it with a pay-as-you-go method. If you already have a license or use a free SQL Server edition, the software usage will be reported using a free meter. If you selected the pay-as-you-go method, a non-zero pay-as-you-go meter will be used.  

The billing granularity is one hour. Pay-as-you-go charges are calculated based on the SQL Server edition and the size of the hosting server during that hour. The size is measured in cores if the SQL Server instance is installed on a physical server, and logical cores (vCores) if the SQL Server instance is installed on a virtual machine. When multiple instances of SQL Server are installed on the same OS, the following rules apply:

 * Only one instance must be licensed per OS for the full size of the host, subject to minimum core size. See [SQL Server licensing guide](https://www.microsoft.com/licensing/docs/view/SQL-Server) for details. The following rules apply:

* The instance with the highest edition defines what license is required.
* If two or more instances of the same edition are installed, the first instance in alphabetical order is billed.
* The combination of the **Host License Type** and the winning SQL Server edition defines which billing meters will be sent every hour. 


The next table shows the meter SKUs that are used for different license types and SQL Server editions:

| Installed edition | Projected edition | License type | AG replica | Meter SKU |
|--|--|--|--|--|
| Enterprise Core | Enterprise | PAYG | No | Ent edition - PAYG |
| Enterprise Core | Enterprise | Paid | No | Ent edition - AHB |
| Enterprise Core | Enterprise | LicenseOnly | Yes or No | Ent edition - License only |
| Enterprise Core | Enterprise | PAYG or Paid | Yes | Ent edition - DR replica |
| Enterprise <sup>1</sup> | Enterprise | PAYG | No | Ent edition - PAYG |
| Enterprise <sup>1</sup>| Enterprise | Paid | No | Ent edition - AHB |
| Enterprise <sup>1</sup>| Enterprise | LicenseOnly | Yes or No | Ent edition - License only |
| Enterprise <sup>1</sup>| Enterprise | PAYG or Paid | Yes | Ent edition - DR replica |
| Standard | Standard | PAYG | No | Std edition - PAYG |
|  Standard | Standard | Paid | No | Std edition - AHB |
|  Standard | Standard | LicenseOnly | No | Std edition - License only |
| Standard | Standard | PAYG or Paid | Yes | Std edition - DR replica |
| Evaluation | Evaluation | LicenseOnly | Yes or No | Eval edition |
| Developer | Developer | LicenseOnly | Yes or No | Dev edition |
| Web | Web | LicenseOnly | n/a | Web edition |
| Express | Express | LicenseOnly | n/a | Express edition |

<sup>1</sup> When Enterprise edition is installed, it indicates that the Server/CAL licensing model is used. Because the conversion to the core-based licensing model does not require an upgrade to the  Enterprise Core, we treat this edition as Enterprise Core. The instances that have not converted to the core-based model and use a Server/CAL license must set the license type to LicenseOnly.

In addition to billing differences, license type determines what features will be available to your Arc-enabled SQL Server. The following features are not included in the LicenseOnly license type:

* Licensing benefit for fail-over servers. Azure extension for SQL Server supports free fail-over servers by automatically detecting if the instance is a replica in an availability group and reporting the usage with a separate meter. You can track the usage of the DR benefit in Cost Management + Billing. See [SQL Server licensing guide](https://www.microsoft.com/licensing/docs/view/SQL-Server) for details.
* Detailed database inventory. You can manage your SQL database inventory in Azure portal. See [View databases](view-databases.md) for details.
* Managing automatic updates of SQL Server from Azure.
* Best practices assessment. You can generate best practices reports and recommendations by periodic scans of your SQL Server configurations. See [Configure your SQL Server instance for Best practices assessment](assess.md).

## Subscribe to Extended Security Updates

Extended Security Updates (ESU) is available for qualified SQL Server instances that use License with Software assurance or Pay-as-you-go as the license type. If the  license type is license only, the option to activate the ESU subscription is disabled. See [Extended Security Updates for SQL Server](../end-of-support/sql-server-extended-security-updates.md).  

> [!NOTE]
> If ESU is enabled **License Type** cannot be changed to `LicenseOnly` until the ESU subscrition is cancelled. 

## Exclude instances

You can exclude certain instances from the at-scale onboarding operations driven by Azure policy or by automatic onboarding processes. To exclude specific instances from these operations, add the instance names to the **Skip Instances** list. For details about at-scale onboarding options, see [Alternate deployment options for Azure Arc-enabled SQL Server](deployment-options.md).

## Modifying SQL Server Configuration

You can use Azure portal, PowerShell or CLI to change all or some configuration settings on a specific Arc-enabled server to the desired state.

To modify the SQL Server Configuration for a larger scope, such as a resource group, subscription, or multiple subscriptions with a single command, use the [Modify SQL Server Configuration](https://github.com/microsoft/sql-server-samples/tree/master/samples/manage/azure-arc-enabled-sql-server/modify-license-type) PowerShell script. It is published as an open source SQL Server sample and includes the step-by-step instructions.

> [!TIP]  
> Run the script from Azure Cloud shell as it has the required Azure PowerShell modules pre-installed and you will be automatically authenticated. For details, see [Running the script using Cloud Shell](https://github.com/microsoft/sql-server-samples/tree/master/samples/manage/azure-arc-enabled-sql-server/modify-license-type#running-the-script-using-cloud-shell).


### [Azure portal](#tab/azure)

There are two ways to open the SQL Server Configuration blade.

1. Open the Arc-enabled Server overview page and click **SQL Server Configuration** as shown.

   :::image type="content" source="media/billing/overview-of-sql-server-azure-arc.png" alt-text="Screenshot of the Azure Arc-enabled SQL Server in Azure portal.":::
  

1. Open the Arc-enabled SQL Server overview page and click on `Host license type` or `ESU status` values as shown.

   :::image type="content" source="media/billing/sql-server-instance-configuration.png" alt-text="Screenshot of Azure portal SQL Server instance configuration.":::

#### Set **License Type** property

Choose one of the license types. See [License types](#license-types) for descriptions.

#### Set the **Extended Security Updates** property

You can enable or disable ESU. This setting is optional and only applies to the qualified versions of SQL Server. To learn more, see [What are Extended Security Updates for SQL Server?](../end-of-support/sql-server-extended-security-updates.md).

> [!NOTE]
> To activate an ESU subscription, the license type must be set to Pay-as-you-go or License with Software assurance.  If it is set to License only, the Extended Security Updates options will be disabled. 

#### Add to the **Exclude instances** list

If you want to exclude specific instances from the at-scale onboarding operations driven by Azure policy or automated onboarding processes, add those instances under **Skip Instances**. This setting is optional.

#### Save the updated configuration

After you verify the license type, ESU setting, and any instance to exclude, select **Save** to apply changes.

### [PowerShell](#tab/powershell)

The following command will set the license type to "PAYG", enable the ESU subscription and add two instances to the  exclusion list.

```powershell
//Updated settings object
$Settings = @{ 
  SqlManagement = @{ IsEnabled = $true };
  ExcludedSqlInstances = @( "Foo","Bar"); 
  LicenseType="PAYG";
  enableExtendedSecurityUpdates = $True;
  esuLastUpdatedTimestamp = [DateTime]::UtcNow.ToString('yyyy-MM-ddTHH:mm:ss.fffZ')
}

// Command stays the same as before, only settings is changed above:
New-AzConnectedMachineExtension -Name "WindowsAgent.SqlServer" -ResourceGroupName {your resource group name} -MachineName {your machine name} -Location {azure region} -Publisher "Microsoft.AzureData" -Settings $Settings -ExtensionType "WindowsAgent.SqlServer"
```
> [!IMPORTANT]  
> - The update command overwrites all settings. If your extension settings have a list of excluded SQL Server instances, make sure to specify the full exclusion list with the update command.
> - If you already have an older version of the Azure extension installed, make sure to upgrade it first, and then use one the modify methods to set the correct license type. For details, see [How to upgrade a machine extension](/azure/azure-arc/servers/manage-automatic-vm-extension-upgrade) for details. 



### [Azure CLI](#tab/az)

The following command will set the license type to "PAYG":

```azurecli
az connectedmachine extension update --machine-name "simple-vm" -g "<resource-group>" --name "WindowsAgent.SqlServer" --type "WindowsAgent.SqlServer" --publisher "Microsoft.AzureData" --settings '{"LicenseType":"PAYG", "SqlManagement": {"IsEnabled":true}}'    
```
> [!IMPORTANT]  
> - The update command overwrites all settings. If your extension settings have a list of excluded SQL Server instances, make sure to specify the full exclusion list with the update command.
> - If you already have an older version of the Azure extension installed, make sure to upgrade it first, and then use one the modify methods to set the correct license type. For details, see [How to upgrade a machine extension](/azure/azure-arc/servers/manage-automatic-vm-extension-upgrade) for details. 

---


## Query SQL Server Configuration

You can use [Azure Resource Graph](/azure/governance/resource-graph/overview) to query the SQL Server Configuration settings within a selected scope. See the following examples.

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

This query identifies many details about each instance, including the license type, ESU setting and enabled features.

```kusto
resources
| where type == "microsoft.hybridcompute/machines/extensions"
| where properties.type in ("WindowsAgent.SqlServer","LinuxAgent.SqlServer")
| project name, resourceGroup, subscriptionId,
    Provisioning_State = properties.provisioningState,
    License_Type = properties.settings.LicenseType,
    ESU_enabled = properties.settings.enableExtendedSecurityUpdates,
    Message = properties.instanceView.status.message,
    Version = properties.instanceView.typeHandlerVersion,
    Exlcuded_instaces = properties.ExcludedSqlInstances,
    iff(notnull(properties.settings.ExternalPolicyBasedAuthorization),"Purview enabled",""),
    iff(notnull(properties.settings.AzureAD),"Azure AD enabled",""),
    iff(notnull(properties.settings.AssessmentSettings),"BPA enabled","")

```

For more examples of Azure Resource Graph Queries, see [Starter Resource Graph queries](/azure/governance/resource-graph/samples/starter).

## Next steps

- [Review SQL Server 2022 Pricing](https://www.microsoft.com/sql-server/sql-server-2022-pricing)
- [Install SQL Server 2022 using the pay-as-you-go activation option](../../database-engine/install-windows/install-sql-server.md)
- [Learn about Extended Security Updates for SQL Server](../end-of-support/sql-server-extended-security-updates.md).  
- [Frequently asked questions](faq.yml#billing)
- [Configure automated patching for Arc-enabled SQL Servers preview](patch.md)

