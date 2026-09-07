---
title: Azure Hybrid Benefit
titleSuffix: Azure SQL Database & SQL Managed Instance
description: Use existing SQL Server licenses for Azure SQL Database and SQL Managed Instance discounts.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: sashan
ms.date: 05/19/2025
ms.service: azure-sql
ms.subservice: service-overview
ms.topic: concept-article
ms.custom: sqldbrb=4
monikerRange: "=azuresql||=azuresql-db||=azuresql-mi"
---
# Azure Hybrid Benefit - Azure SQL Database & SQL Managed Instance
[!INCLUDE[appliesto-sqldb-sqlmi](includes/appliesto-sqldb-sqlmi.md)]

[Azure Hybrid Benefit](https://azure.microsoft.com/pricing/hybrid-benefit/) grants you the ability to allocate your SQL Server license to Azure SQL Database and Azure SQL Managed Instance. You can save up to 30 percent or more on SQL Database and SQL Managed Instance by using your Software Assurance-enabled SQL Server licenses on Azure. The [Pricing Calculator](https://azure.microsoft.com/pricing/calculator/) allows you to determine Azure Hybrid Benefit savings at a service level.

With Azure Hybrid Benefit, you get a discount on the allocation of SQL Server licenses to the SQL Server Database Engine. Enabling the benefit does not require any downtime.

:::image type="complex" source="media/azure-hybrid-benefit/pricing.png" alt-text="Diagram of vCore pricing structure for SQL Database.":::
Diagram of vCore pricing structure for SQL Database. 'License Included' pricing is made up of base compute and SQL license components. Azure Hybrid Benefit pricing is made up of base compute and software assurance components.
:::image-end:::


## Enable Azure Hybrid Benefit for Azure SQL Database

You can enable the Azure Hybrid Benefit for Azure SQL Database by using the Azure portal, PowerShell, Azure CLI, or REST API.

For Azure SQL Database, Azure Hybrid Benefit is only available when using the provisioned compute tier of the [vCore-based purchasing model](database/service-tiers-vcore.md). Azure Hybrid Benefit doesn't apply to the [DTU-based purchasing model](database/service-tiers-dtu.md) or the [serverless compute tier](database/serverless-tier-overview.md).

#### [Azure portal](#tab/azure-portal)

To set or update the license type to the Azure Hybrid Benefit by using the Azure portal:

- For new databases, during creation, select **Configure database** on the **Basics** tab and select the option to **Save money**.
- For existing databases, select **Compute + storage** in the **Settings** menu and select the option to **Save money**.

If you don't see the **Save money** option in the Azure portal, verify that you selected a service tier using the vCore-based purchasing model and the provisioned compute tier.

#### [PowerShell](#tab/azure-powershell)

To set or update the license type to the Azure Hybrid Benefit by using PowerShell, use the `BasePrice` value: 

- [New-AzSqlDatabase](/powershell/module/az.sql/new-azsqldatabase) with the -LicenseType parameter
- [Set-AzSqlDatabase](/powershell/module/az.sql/set-azsqldatabase) with the -LicenseType parameter

#### [Azure CLI](#tab/azure-cli)

To set or update the license type to the Azure Hybrid Benefit by using the Azure CLI, use the `BasePrice` value:

- [az sql db create](/cli/azure/sql/db#az-sql-db-create) with the --license-type parameter

#### [REST API](#tab/rest)

To set or update the license type to the Azure Hybrid Benefit by using the REST API, use the `BasePrice` value:

- [Create or update](/rest/api/sql/databases/create-or-update) with the properties.licenseType parameter
- [Update](/rest/api/sql/databases/update) with the properties.licenseType parameter

---

## Enable Azure Hybrid Benefit for Azure SQL Managed Instance

You can enable the Azure Hybrid Benefit for Azure SQL Managed Instance by using the Azure portal, PowerShell, Azure CLI, or REST API.

#### [Azure portal](#tab/azure-portal)

To set or update the license type to the Azure Hybrid Benefit by using the Azure portal:

- For new managed instances, during creation, select **Configure Managed Instance** on the **Basics** tab and select the option for **Azure Hybrid Benefit**.
- For existing managed instances, select **Compute + storage** in the **Settings** menu and select the option for **Azure Hybrid Benefit**.

#### [PowerShell](#tab/azure-powershell)

To set or update the license type to the Azure Hybrid Benefit by using PowerShell, use the `BasePrice` value:

- [New-AzSqlInstance](/powershell/module/az.sql/new-azsqlinstance) with the -LicenseType parameter.
- [Set-AzSqlInstance](/powershell/module/az.sql/set-azsqlinstance) with the -LicenseType parameter

#### [Azure CLI](#tab/azure-cli)

To set or update the license type to the Azure Hybrid Benefit by using the Azure CLI, use the `BasePrice` value:

- [az sql mi create](/cli/azure/sql/mi#az-sql-mi-create) with the --license-type parameter
- [az sql mi update](/cli/azure/sql/mi#az-sql-mi-update) with the --license-type parameter

#### [REST API](#tab/rest)

To set or update the license type to the Azure Hybrid Benefit by using the REST API, use the `BasePrice` value:

- [Create or update](/rest/api/sql/managed-instances/create-or-update) with the properties.licenseType parameter
- [Update](/rest/api/sql/managed-instances/update) with the properties.licenseType parameter

---

## Manage license types at scale

[!INCLUDE [manage-sql-license-types-at-scale](../docs/includes/manage-sql-license-types-at-scale.md)]

## Frequently asked questions

### Is there a migration allowance with Azure Hybrid Benefit for SQL Server?

You have 180 days of migration allowance for the license to ensure migrations are running seamlessly. After that 180-day period, you can only use the SQL Server license on Azure. You no longer have migration allowance on-premises and on Azure.

### How does Azure Hybrid Benefit for SQL Server differ from license mobility?

We offer license mobility benefits to SQL Server customers with Software Assurance. License mobility allows reassignment of their licenses to a partner's shared servers. You can use this benefit on Azure IaaS and AWS EC2.

Azure Hybrid Benefit for SQL Server differs from license mobility in two key areas:

- It provides economic benefits for moving highly virtualized workloads to Azure. SQL Server Enterprise Edition customers can get four cores in Azure in the General Purpose SKU for every core they own on-premises for highly virtualized applications. License mobility doesn't allow any special cost benefits for moving virtualized workloads to the cloud.
- It provides for a PaaS destination on Azure (SQL Managed Instance) that's highly compatible with SQL Server.

### What are the specific rights of the Azure Hybrid Benefit for SQL Server?

SQL Database and SQL Managed Instance customers have the following rights associated with Azure Hybrid Benefit for SQL Server:

|License footprint|What does Azure Hybrid Benefit for SQL Server get you?|
|---|---|
|SQL Server Enterprise Edition core customers with SA|<li>Can pay base rate on General Purpose or Business Critical SKU</li><li>One core on-premises = Four vCores in General Purpose SKU</li><li>One core on-premises = One vCore in Business Critical SKU</li>|
|SQL Server Standard Edition core customers with SA|<li>Can pay base rate on General Purpose or Business Critical SKU</li><li>One core on-premises = One vCore in General Purpose SKU</li><li>Four cores on-premises = One vCore in Business Critical SKU</li>|

## Next steps

- For help with choosing an Azure SQL deployment option, see [Service comparison](azure-sql-iaas-vs-paas-what-is-overview.md).
- For a comparison of SQL Database and SQL Managed Instance features, see [Features of SQL Database and SQL Managed Instance](database/features-comparison.md).
