---
title: Enable billing through Microsoft Azure
titleSuffix: Azure Arc-enabled SQL Server
description: Explains how Azure Arc-enabled SQL Server is billed by Microsoft.
author: anosov1960
ms.author: sashan
ms.reviewer: mikeray, randolphwest
ms.date: 01/17/2023
ms.service: sql
ms.topic: conceptual
---

# SQL Server licensing and billing options

[!INCLUDE [sqlserver2022](../../includes/applies-to-version/sqlserver2022.md)]

You can use Arc-enabled SQL Server to accurately track your usage of the SQL Server software by your applications. You may also elect to pay for that usage directly through Microsoft Azure as a pay-as-you-go billing option. This option is an alternative to using a traditional license agreement. SQL Server 2022 (16.x) introduces this option in setup and allows you to activate your instance for use in production without supplying a product key. For other versions of SQL Server you can control how you pay for SQL Server software through Azure Portal or API.

## Prerequisites

* You're in a [Contributor role](/azure/role-based-access-control/built-in-roles#contributor) in at least one of the Azure subscriptions your organization created. Learn how to [create a new billing subscription](/azure/cloud-adoption-framework/ready/azure-best-practices/initial-subscriptions).
* You're in a [Contributor role](/azure/role-based-access-control/built-in-roles#contributor) for the resource group in which the SQL Server instance will be registered. See [Managed Azure resource groups](/azure/azure-resource-manager/management/manage-resource-groups-portal) for details.
* The **Microsoft.AzureArcData** and **Microsoft.HybridCompute** resource providers are registered in each  subscription you use for SQL Server pay-as-you-go billing.
* You select pay-as-you-go activation option in [SQL 2022 setup wizard](../../database-engine/install-windows/install-sql-server-from-the-installation-wizard-setup.md) or [command prompt](../../database-engine/install-windows/install-sql-server-from-the-command-prompt.md).

To register the resource providers, use one of the methods below:  

# [Azure portal](#tab/azure)

1. Select **Subscriptions** 
2. Choose your subscription
3. Under **Settings**, select **Resource providers**
4. Search for `Microsoft.AzureArcData` and `Microsoft.HybridCompute` and select **Register**

# [PowerShell](#tab/powershell)

Run:

```powershell
Register-AzResourceProvider -ProviderNamespace Microsoft.AzureArcData
```

# [Azure CLI](#tab/az)

Run:

```azurecli
az provider register --namespace 'Microsoft.AzureArcData'
```

---

## Overview

Host license type is a configuration setting of Azure Extension for SQL Server that defines how you prefer to pay for the usage of SQL Server software installed on the physical or virtual machine. It allows you to track software usage Cost Management + Billing portal and ensure you are compliant with SQL Server license requirements. License type is a required parameter when you install Azure Extension for SQL Server. Every supported onboarding method includes the license type options. See [SQL Server privacy supplement]() for details of how we use that information. 

The following license types are supported:

| License type | Descripton  | Short form   |  
|---|---|---|
| PAYG | Standard or Enterprise edition with pay-as-you-go billing through Microsoft Azure | Pay-as-you-go |
| Paid | Standard or Enterprise edition license with Software Assurance or SQL Subscription  | License with software assurance |
| LicenseOnly | Developer, Evaluation, Express, Web, Standard or Enterprise edition license only without Software Assurance | License only |


If "PAYG” is configured it enables paying for your SQL Server software usage on a pay-as-you-go basis though Microsoft Azure. See SQL Server pay-as-you-go prices here. “Paid” and “LicenseOnly” types mean that you are using an existing license agreement and already have the necessary licenses. In those cases, your software usage will be reported to you using $0 meters. You can analyze your usage in Cost Management + Billing to make sure you have enough licenses for all your installed SQL Server instances.

The billing granularity is one hour. Pay-as-you-go charges are calculated based on the SQL Server edition and the size of the hosting server at any time during that hour. The size is measured in cores if the SQL Server instance is installed on a physical server, and logical cores (vCores) if the SQL Server instance is installed on a virtual machine. When multiple instances of SQL Server are installed on the same OS, only one instance requires to be licensed for the full size of the host, subject to minimum core size. See [SQL Server licensing guide](https://www.microsoft.com/licensing/docs/view/SQL-Server) for details. The following rules apply:

- The instance with the highest edition of all instances installed on the same operating system determines the required license.
- If two instances are installed with same edition but one instance is configured to use pay-as-you-go billing and the other is installed using a product key, the pay-as-you-go instance is ignored because it is included in the customer's product key.
- If two instances are installed with pay-as-you-go billing and same editions, the first instance in alphabetical order is billed.

In addition to billing differences, license type determines what features will be available to your Arc-enabled SQL Server. The following features are included with PAYG and Paid license type and not available if you are using a LicenseOnly  SQL Server. 

- Licensing benefit for fail-over servers. Azure extension for SQL Server supports free fail-over servers by automatically detecting if the instance is a replica in an availability group and reporting the usage with a separate meter. You can track the usage of the DR benefit in Cost Management + Billing. See SQL Server licensing guide for details of this benefit.
- Detailed database inventory. You can manage your SQL database inventory in Azure portal. See <article> for details.
- Azure active directory authentication. You can manage access to your SQL Sedrver databases using ADD credentials. This feature is only available in SQL Server 2022.
- Best practices assessment. You can generate best practices reports and recommendations by periodic scans of your SQL Server configurations. See [Configure your SQL Server instance for Best practices assessment](assess.md)

The following table shows the meters that are used to track usage and billing for different license types and SQL Server editions:

| Installed edition | Projected edition | License type | AG replica | Meter SKU |
|--|--|--|--|--|
| Enterprise Core | Enterprise | PAYG | No | Ent edition - PAYG |
|  Enterprise Core | Enterprise | Paid | No | Ent edition - AHB |
| Standard | Standard | PAYG | No | Std edition - PAYG |
|  Standard | Standard | Paid | No | Std edition - AHB |
| Enterprise Core | Enterprise | LicenseOnly | Yes or No | Ent edition - License only |
| Enterprise (Server/CAL) | Enterprise | LicenseOnly | Yes or No | n/a |
|  Standard | Standard | LicenseOnly | No | Std edition - License only |
| Enterprise Core | Enterprise | PAYG or Paid | Yes | Ent edition - DR replica |
| Standard | Standard | PAYG or Paid | Yes | Std edition - DR replica |
| Evaluation | Evaluation | LicenseOnly | Yes or No | Eval edition |
| Developer | Developer | LicenseOnly | Yes or No | Dev edition |
| Web | Web | LicenseOnly | n/a | Web edition |
| Express | Express | LicenseOnly | n/a | Express edition |

Enterprise Server/CAL is alowed to connect but we will emit any meters because it is not a core-based license.

## License types

Any installed instance of SQL server can be connected to Azure with the exception of Express and Web editions and SQL Server instances with a Serever/CAL license. When they are connected, their license type is visible in Azure portal as the `licenseType` property of the `SQL Server - Azure Arc` resource. One of the benefits of connecting your SQL Server instances to Azure is that you can manage the usage of the different licenses in the *Cost Management + Billing* portal but only the pay-as-you-go instances generate actual charges. This way you can manage your license position and maintain compliance. The following table shows the different license types.

| Installed edition | Activation choice  | License type  |  
|---|---|---|
| Enterprise Core | Product key with Software Assurance or Subscription | Paid |
| Enterprise Core | Pay-as-you-go | PAYG | 
| Standard | Product key with Software Assurance or Subscription | Paid | 
| Standard | Pay-as-you-go | PAYG |
| Enterprise Core | Product key without Software Assurance or Subscription | LicenseOnly | 
| Standard | Product key without Software Assurance or Subscription | LicenseOnly | 
| Evaluation | Free edition | Free | 
| Developer | Free edition | Free | 
|||

Example of the instance properties of [!INCLUDE[sql-server-2022](../../includes/sssql22-md.md)] in JSON:

```json
    "properties": {
        "version": "SQL Server 2022",
        "edition": "Enterprise",
        ...
        "vCore": "8",
        "status": "Connected",
        ...
        "licenseType": "PAYG",
        ...
    }
```

One of the benefits of Software Assurance or SQL subscription is free fail-over servers for high availability and disaster recovery as long as they are passive replicas. See  [SQL Server licensing guide](https://www.microsoft.com/licensing/docs/view/SQL-Server) for details of this benefit.  Azure extension for SQL Server supports free fail-over servers by automatically detecting if the instance is a replica in an availability group. In that case, it reports it with a license type `HADR`, which does not require a separate license. The  following table shows the conditions when this license type is set.

| Installed edition | Activation choice  | AG replica | License type  |  
|---|---|---|---|
| Enterprise Core | Product key with Software Assurance or SQL subscription | Yes | HADR | 
| Enterprise Core | Pay-as-you-go | Yes | HADR | 
| Enterprise Core | Product key without Software Assurance or SQL subscription | Yes | LicenseOnly | 
| Standard | Product key with Software Assurance or SQL subscription | Yes | HADR | 
| Standard | Pay-as-you-go | Yes | HADR | 
| Standard | Product key without Software Assurance or SQL subscription| Yes | LicenseOnly | 
|||

## Next steps

- [Review SQL Server 2022 Pricing](https://www.microsoft.com/sql-server/sql-server-2022-pricing)
- [Install SQL Server 2022 using the pay-as-you-go activation option](../../database-engine/install-windows/install-sql-server.md)
- [Frequently asked questions](faq.yml#billing)
