---
title: Enable billing through Microsoft Azure
titleSuffix: Azure Arc-enabled SQL Server
description: Explains how Azure Arc-enabled SQL Server is billed by Microsoft. Use to enable pay as you go licensing.
author: anosov1960
ms.author: sashan
ms.reviewer: mikeray, randolphwest
ms.date: 02/07/2023
ms.service: sql
ms.topic: conceptual
---

# SQL Server licensing and billing options

You can use Arc-enabled SQL Server to accurately track your usage of the SQL Server software and manage your license compliance. You may also elect to pay for the SQL software usage directly through Microsoft Azure using a pay-as-you-go billing option. You can control how you pay for SQL Server software through Azure portal or API. SQL Server 2022 (16.x) allows you to select a pay-as-you-go billing option during setup.

## Prerequisites

* You're in a [Contributor role](/azure/role-based-access-control/built-in-roles#contributor) in at least one of the Azure subscriptions your organization created. Learn how to [create a new billing subscription](/azure/cloud-adoption-framework/ready/azure-best-practices/initial-subscriptions).
* You're in a [Contributor role](/azure/role-based-access-control/built-in-roles#contributor) for the resource group in which the SQL Server instance will be registered. See [Managed Azure resource groups](/azure/azure-resource-manager/management/manage-resource-groups-portal) for details.
* The **Microsoft.AzureArcData** and **Microsoft.HybridCompute** resource providers are registered in each  subscription you use for SQL Server pay-as-you-go billing.

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
## Overview

License type is a configuration setting of Azure Extension for SQL Server that defines how you prefer to pay for the usage of SQL Server software installed on the physical or virtual machine. It also allows you to track software usage in the Cost Management + Billing portal and ensure you are compliant with SQL Server license requirements. License type is a required parameter when you install Azure Extension for SQL Server and each supported onboarding method includes the license type options. 

The following license types are supported:

| License type | Description  | Short form   |  
|---|---|---|
| PAYG | Standard or Enterprise edition with pay-as-you-go billing through Microsoft Azure | Pay-as-you-go |
| Paid | Standard or Enterprise edition license with Software Assurance or SQL Subscription  | License with software assurance |
| LicenseOnly | Developer, Evaluation, Express, Web, Standard or Enterprise edition license only without Software Assurance | License only |











If "PAYG” is configured it enables paying for your SQL Server software usage on a pay-as-you-go basis through Microsoft Azure. See SQL Server pay-as-you-go prices [here](https://www.microsoft.com/sql-server/sql-server-2022-pricing). “Paid” and “LicenseOnly” types mean that you are using an existing license agreement and already have the necessary licenses. In those cases, your software usage will be reported to you using $0 meters. You can analyze your usage in the Cost Management + Billing portal to make sure you have enough licenses for all your installed SQL Server instances.

The billing granularity is one hour. Pay-as-you-go charges are calculated based on the SQL Server edition and the size of the hosting server at any time during that hour. The size is measured in cores if the SQL Server instance is installed on a physical server, and logical cores (vCores) if the SQL Server instance is installed on a virtual machine. When multiple instances of SQL Server are installed on the same OS, only one instance must be licensed for the full size of the host, subject to minimum core size. See [SQL Server licensing guide](https://www.microsoft.com/licensing/docs/view/SQL-Server) for details. The following rules apply:

- The instance with the highest edition of all instances installed on the same operating system determines the required license.
- If two or more instances of the same edition are installed, the first instance in alphabetical order is billed.
- The configured license type defines how the winning instance is billed.
   
In addition to billing differences, license type determines what features will be available to your Arc-enabled SQL Server. The following features are not included the LicenseOnly license type.  

- Licensing benefit for fail-over servers. Azure extension for SQL Server supports free fail-over servers by automatically detecting if the instance is a replica in an availability group and reporting the usage with a separate meter. You can track the usage of the DR benefit in Cost Management + Billing. See [SQL Server licensing guide](https://www.microsoft.com/licensing/docs/view/SQL-Server) for details.
- Detailed database inventory. You can manage your SQL database inventory in Azure portal. See [View databases](view-databases.md) for details.
- Managing automatic updates of SQL Server from Azure.
- Azure active directory authentication. You can manage access to your SQL Server databases using ADD credentials. This feature requires that your instance is upgraded to SQL Server  2022. For more information, see [Azure Active Directory authentication for SQL Server 2022](https://cloudblogs.microsoft.com/sqlserver/2022/07/28/azure-active-directory-authentication-for-sql-server-2022/).
- Best practices assessment. You can generate best practices reports and recommendations by periodic scans of your SQL Server configurations. See [Configure your SQL Server instance for Best practices assessment](assess.md)

The following table shows the meters that track usage and billing for different license types and SQL Server editions:

| Installed edition | Projected edition | License type | AG replica | Meter SKU |
|--|--|--|--|--|
| Enterprise Core | Enterprise | PAYG | No | Ent edition - PAYG |
|  Enterprise Core | Enterprise | Paid | No | Ent edition - AHB |
| Standard | Standard | PAYG | No | Std edition - PAYG |
|  Standard | Standard | Paid | No | Std edition - AHB |
| Enterprise Core | Enterprise | LicenseOnly | Yes or No | Ent edition - License only |
| Enterprise (Server/CAL) | Enterprise | LicenseOnly | Yes or No | n/a¹ |
|  Standard | Standard | LicenseOnly | No | Std edition - License only |
| Enterprise Core | Enterprise | PAYG or Paid | Yes | Ent edition - DR replica |
| Standard | Standard | PAYG or Paid | Yes | Std edition - DR replica |
| Evaluation | Evaluation | LicenseOnly | Yes or No | Eval edition |
| Developer | Developer | LicenseOnly | Yes or No | Dev edition |
| Web | Web | LicenseOnly | n/a | Web edition |
| Express | Express | LicenseOnly | n/a | Express edition |

¹ Enterprise Server/CAL is allowed to connect but usage meters are not emitted because it is not a core-based license.

## Selecting license type

License type is a property of Azure extension for SQL Server. Only one instance of the extension can be installed on each machine. It manages all SQL Server instances installed on that machine. To select license type, use one of the following methods:

If your machine is already connected to an Arc-enabled Server, follow the steps in [When the machine is already connected to an Arc-enabled Server](connect-already-enabled.md)

If your machine isn’t connected to an Arc-enabled Server, follow the steps in [When the machine isn't connected to an Arc-enabled Server](connect.md#onboard-the-server-to-azure-arc)

- If your machine is already connected to an Arc-enabled Server, follow the steps in [When the machine is already connected to an Arc-enabled Server](connect.md#when-the-machine-is-already-connected-to-an-arc-enabled-server)
- If your machine isn’t connected to an Arc-enabled Server, follow the steps in [When the machine isn't connected to an Arc-enabled Server](connect.md#when-the-machine-isnt-connected-to-an-arc-enabled-server)
- If you are installing SQL Server 2022, you have the option to select the license type with [SQL 2022 setup wizard](../../database-engine/install-windows/install-sql-server-from-the-installation-wizard-setup.md) or [command prompt](../../database-engine/install-windows/install-sql-server-from-the-command-prompt.md).
   
The license type value is shared by all SQL Server instances and for your convenience, it is visible in the overview blade of Arc-enabled SQL Server as shown.

![Screenshot of Azure Arc-enabled SQL Server instance configured for pay-as-you-go licensing.](media/billing/overview-of-sql-server-azure-arc.png)


## Modify license type

You can change the license type of an installed Azure extension for SL Server by using one of the following methods.

### [Azure portal](#tab/azure)

To modify the license type, use [PowerShell](billing.md?tabs=powershell&preserve-view=true#modify-license-type) or [Azure CLI](billing.md?tabs=az&preserve-view=true#modify-license-type).

### [PowerShell](#tab/powershell)

The following command will set the license type to "PAYG":

```powershell
//Updated settings object
$Settings = @{ SqlManagement = @{ IsEnabled = $true }; ExcludedSqlInstances = @( "Foo","Bar">); LicenseType="PAYG"}

// Command stays the same as before, only settings is changed above:
New-AzConnectedMachineExtension -Name "WindowsAgent.SqlServer" -ResourceGroupName {your resource group name} -MachineName {your machine name} -Location {azure region} -Publisher "Microsoft.AzureData" -Settings $Settings -ExtensionType "WindowsAgent.SqlServer"

```

### [Azure CLI](#tab/az)

The following command will set the license type to "PAYG":

```azurecli
az connectedmachine extension update --machine-name "simple-vm" -g "arceeBilling" --name "WindowsAgent.SqlServer" --type "WindowsAgent.SqlServer" --publisher "Microsoft.AzureData" --settings '{"LicenseType":"PAYG", "SqlManagement": {"IsEnabled":true}}'    
```

---
> [!IMPORTANT]  
>
> - The update command overwrites all settings. If your extension settings have a list of excluded SQL Server instances, make sure to specify the full exclusion list with the update command.
> - If you already have an older version of the Azure extension installed, make sure to upgrade it first, and then use one the modify methods to set the correct license type. For details, see [How to upgrade a mchine extenstion](/azure/azure-arc/servers/manage-automatic-vm-extension-upgrade) for details. 

## Next steps

- [Review SQL Server 2022 Pricing](https://www.microsoft.com/sql-server/sql-server-2022-pricing)
- [Install SQL Server 2022 using the pay-as-you-go activation option](../../database-engine/install-windows/install-sql-server.md)
- [Frequently asked questions](faq.yml#billing)

