---
title: Feature Availability by Region
description: Learn about feature availability by region for Azure SQL Managed Instance.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: 
ms.date: 05/05/2025
ms.service: azure-sql-managed-instance
ms.topic: concept-article
ms.custom:
  - references_regions
---
# Feature availability by region - Azure SQL Managed Instance

[!INCLUDE [appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

> [!div class="op_single_selector"]
> * [Azure SQL Database](../database/region-availability.md?view=azuresql-db&preserve-view=true)
> * [Azure SQL Managed Instance](region-availability.md?view=azuresql-mi&preserve-view=true)

This article is a centralized list of the availability of various Azure SQL Managed Instance features in [Azure regions](https://azure.microsoft.com/explore/global-infrastructure/geographies/). 

- To view regional availability of Azure SQL Managed Instance, see [Azure global infrastructure products by region](https://azure.microsoft.com/global-infrastructure/services/?products=sql-database&regions=all). 
- To move Azure SQL Managed Instance to another region, see [Move resources to new region](move-resources-across-regions.md?view=azuresql-mi&preserve-view=true).

> [!TIP]
> For a visualization of Azure regions, see [Azure global infrastructure](https://datacenters.microsoft.com/globe/explore).

<!-- Alphabetization guidance for region names: sort by the region, then the directional. East Asia comes before Australia East. This results in regions kept together, for example, all the US regions. -->

## vCore purchasing model hardware availability

Azure SQL Managed Instance hardware is available in all regions, except where indicated. For more information, see [vCore purchasing model](service-tiers-managed-instance-vcore.md).

### Standard-series (Gen5) availability

Standard-series (Gen5) hardware is available in [all public regions worldwide where Azure SQL Managed Instance is available](https://azure.microsoft.com/global-infrastructure/services/?products=sql-database&regions=all).

<a id="hyperscale-premium-series-availability"></a>

### Regional supports for memory optimized premium-series hardware and for premium-series hardware with 16-TB storage

Supports for the memory-optimized premium-series hardware and the premium-series hardware with 16-TB storage are currently available in these specific regions only:

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

#### [Asia Pacific](#tab/asia)

- East Asia<sup>1</sup>
- Southeast Asia
- Australia East
- Australia Southeast
- China North 3
- India Central
- Japan East
- Japan West

<sup>1</sup> The creation of new instances and modification of existing instances may be temporarily disabled due to limited hardware capacity in this region. To proceed with these actions, please select a different hardware generation. 

#### [Europe, the Middle East, and Africa](#tab/emea)

- North Europe
- West Europe
- France Central
- Germany West Central
- Italy North
- Poland Central
- Qatar Central 
- Sweden Central
- Switzerland North
- UK South

---

## Free offer

The [free offer](free-offer.md) is available in the following regions:

#### [Americas](#tab/americas)

- East US
- East US 2
- South Central US
- West US 2
- West US 3

#### [Asia Pacific](#tab/asia)

- Southeast Asia
- Australia East

#### [Europe, the Middle East, and Africa](#tab/emea)

- North Europe
- West Europe
- Sweden Central
- UK South

---

## Service endpoint policy

[Service endpoint policy](service-endpoint-policies-configure.md) is available in *ALL Azure regions where SQL Managed Instance is available*, with the **following exceptions**: 

- China East 2
- China North 2
- US Gov Arizona
- US Gov Texas
- US Gov Virginia

## Maintenance window availability

Choosing a [maintenance window](maintenance-window.md) for Azure SQL Managed Instance other than the default is available in all regions.

## Zone redundancy 

[Zone redundancy](high-availability-sla-local-zone-redundancy.md#zone-redundant-availability) is available in the following regions: 

#### [Americas](#tab/americas)

- Brazil South
- Canada Central
- Mexico Central
- Spain Central
- East US
- East US 2
- Central US
- West US 2 
- West US 3

#### [Asia Pacific](#tab/asia)

- East Asia
- Southeast Asia
- Australia East
- Central India
- Korea Central
- Japan East
- Japan West
- New Zealand North

#### [Europe, the Middle East, and Africa](#tab/emea)

- North Europe
- West Europe
- France Central
- Germany West Central
- Italy North
- Israel Central
- Norway East
- Poland Central
- Qatar Central
- South Africa North
- Switzerland North
- Sweden Central
- UAE North
- UK South

---

## Database watcher

[Database watcher](../database-watcher-overview.md) is a managed monitoring solution for Azure SQL Managed Instance and Azure SQL Database. It is available in the following regions:

[!INCLUDE [database-watcher](../includes/regional-support/database-watcher.md)]

## Related content

- [What's new in Azure SQL Managed Instance?](doc-changes-updates-release-notes-whats-new.md?view=azuresql-db&preserve-view=true)
- [Azure products by region](https://azure.microsoft.com/explore/global-infrastructure/products-by-region/)
- [Move resources to new region](move-resources-across-regions.md?view=azuresql-db&preserve-view=true)
- [Disaster recovery guidance](disaster-recovery-guidance.md)
