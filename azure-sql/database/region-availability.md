---
title: Feature Availability by Region
description: Learn about feature availability by region for Azure SQL Database.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: rsetlem, peskount, rokhot, shrtiwar
ms.date: 02/20/2026
ms.service: azure-sql-database
ms.topic: concept-article
ms.custom:
  - references_regions
---
# Feature availability by region - Azure SQL Database

[!INCLUDE [appliesto-sqldb](../includes/appliesto-sqldb.md)]

> [!div class="op_single_selector"]
> * [Azure SQL Database](region-availability.md?view=azuresql-db&preserve-view=true)
> * [Azure SQL Managed Instance](../managed-instance/region-availability.md?view=azuresql-mi&preserve-view=true)

This article is a centralized list of the availability of various Azure SQL Database features in [Azure regions](https://azure.microsoft.com/explore/global-infrastructure/geographies/). 

- To view regional availability of Azure SQL Database, see [Azure global infrastructure products by region](https://azure.microsoft.com/explore/global-infrastructure/products-by-region/table). 
- To learn how to design globally available services, see [Designing globally available services using Azure SQL Database](designing-cloud-solutions-for-disaster-recovery.md?view=azuresql-db&preserve-view=true).
- To move Azure SQL Database to another region, see [Move resources to new region - Azure SQL Database](move-resources-across-regions.md?view=azuresql-db&preserve-view=true).

> [!TIP]
> For a visualization of Azure regions, see [Azure global infrastructure](https://datacenters.microsoft.com/globe/explore).

<!-- Alphabetization guidance for region names: sort by the region, then the directional. East Asia comes before Australia East. This results in regions kept together, for example, all the US regions. -->

## vCore purchasing model hardware availability

Azure SQL Database hardware is available in all regions, except where indicated. For more information, see [vCore purchasing model - Azure SQL Database](service-tiers-sql-database-vcore.md).

<a id="gen4gen5-1"></a>

<a id="gen5"></a>

### Standard-series (Gen5) availability

Standard-series (Gen5) hardware is available in [all public regions worldwide where Azure SQL Database is available](https://azure.microsoft.com/explore/global-infrastructure/products-by-region/table).

<a id="hyperscale-premium-series-availability"></a>

### Hyperscale premium-series availability

[Premium-series hardware](service-tiers-sql-database-vcore.md#hyperscale-premium-series) is available for single databases and elastic pools. 

[Premium-series memory optimized hardware](service-tiers-sql-database-vcore.md#hyperscale-premium-series) is not currently available in the following regions:

- Brazil South
- Sweden Central
- UK West

**Hyperscale premium-series** and **premium-series memory optimized hardware** is available for single databases and elastic pools in the following regions:

#### [Americas](#tab/americas)

| Azure region | Premium-series available | Premium-series memory optimized available | [Availability zone support](high-availability-sla-local-zone-redundancy.md#high-availability-through-zone-redundancy) | 
|:--|:--|:--|
| Brazil South | [!INCLUDE [yes](../includes/yes.md)]  |  | [!INCLUDE [yes](../includes/yes.md)]  |
| Canada Central | [!INCLUDE [yes](../includes/yes.md)]  | [!INCLUDE [yes](../includes/yes.md)]  | [!INCLUDE [yes](../includes/yes.md)]  |
| Canada East | [!INCLUDE [yes](../includes/yes.md)]  | [!INCLUDE [yes](../includes/yes.md)]  |  |
| US Central | [!INCLUDE [yes](../includes/yes.md)]  | [!INCLUDE [yes](../includes/yes.md)]  | [!INCLUDE [yes](../includes/yes.md)]  |
| US East | [!INCLUDE [yes](../includes/yes.md)]  | [!INCLUDE [yes](../includes/yes.md)]  | [!INCLUDE [yes](../includes/yes.md)]  |
| US East 2 | [!INCLUDE [yes](../includes/yes.md)]  | [!INCLUDE [yes](../includes/yes.md)]  | [!INCLUDE [yes](../includes/yes.md)]  |
| US North Central | [!INCLUDE [yes](../includes/yes.md)]  | [!INCLUDE [yes](../includes/yes.md)]  |  |
| US South Central | [!INCLUDE [yes](../includes/yes.md)]  | [!INCLUDE [yes](../includes/yes.md)]  |  |
| US West Central | [!INCLUDE [yes](../includes/yes.md)]  | [!INCLUDE [yes](../includes/yes.md)]  |  |
| US West 1 | [!INCLUDE [yes](../includes/yes.md)]  | [!INCLUDE [yes](../includes/yes.md)]  |  |
| US West 2 | [!INCLUDE [yes](../includes/yes.md)]  | [!INCLUDE [yes](../includes/yes.md)]  | [!INCLUDE [yes](../includes/yes.md)]  |
| US West 3 | [!INCLUDE [yes](../includes/yes.md)]  | [!INCLUDE [yes](../includes/yes.md)]  | [!INCLUDE [yes](../includes/yes.md)]  |

#### [Asia Pacific](#tab/asia)

| Azure region | Premium-series available | Premium-series memory optimized available | [Availability zone support](high-availability-sla-local-zone-redundancy.md#high-availability-through-zone-redundancy) | 
|:--|:--|:--|
| East Asia | [!INCLUDE [yes](../includes/yes.md)]  | [!INCLUDE [yes](../includes/yes.md)]  |  |
| Southeast Asia | [!INCLUDE [yes](../includes/yes.md)]  | [!INCLUDE [yes](../includes/yes.md)]  | [!INCLUDE [yes](../includes/yes.md)]  |
| Australia East | [!INCLUDE [yes](../includes/yes.md)]  | [!INCLUDE [yes](../includes/yes.md)]  | [!INCLUDE [yes](../includes/yes.md)]  |
| Australia Southeast | [!INCLUDE [yes](../includes/yes.md)]  | [!INCLUDE [yes](../includes/yes.md)]  |  |
| Central India | [!INCLUDE [yes](../includes/yes.md)]  | [!INCLUDE [yes](../includes/yes.md)]  |  |
| South India | [!INCLUDE [yes](../includes/yes.md)]  | [!INCLUDE [yes](../includes/yes.md)]  |  |
| Japan East | [!INCLUDE [yes](../includes/yes.md)]  | [!INCLUDE [yes](../includes/yes.md)]  | [!INCLUDE [yes](../includes/yes.md)]  |
| Japan West | [!INCLUDE [yes](../includes/yes.md)]  | [!INCLUDE [yes](../includes/yes.md)]  |  |

#### [Europe, the Middle East, and Africa](#tab/emea)

| Azure region | Premium-series available | Premium-series memory optimized available | [Availability zone support](high-availability-sla-local-zone-redundancy.md#high-availability-through-zone-redundancy) | 
|:--|:--|:--|
| Europe North | [!INCLUDE [yes](../includes/yes.md)]  | [!INCLUDE [yes](../includes/yes.md)]  | [!INCLUDE [yes](../includes/yes.md)]  |
| Europe West | [!INCLUDE [yes](../includes/yes.md)]  | [!INCLUDE [yes](../includes/yes.md)]  | [!INCLUDE [yes](../includes/yes.md)]  |
| France Central | [!INCLUDE [yes](../includes/yes.md)]  | [!INCLUDE [yes](../includes/yes.md)]  |  |
| Germany West Central | [!INCLUDE [yes](../includes/yes.md)]  | [!INCLUDE [yes](../includes/yes.md)]  | [!INCLUDE [yes](../includes/yes.md)]  |
| Sweden Central | [!INCLUDE [yes](../includes/yes.md)]  |  | [!INCLUDE [yes](../includes/yes.md)]  |
| Switzerland North | [!INCLUDE [yes](../includes/yes.md)]  | [!INCLUDE [yes](../includes/yes.md)]  | |
| UK South | [!INCLUDE [yes](../includes/yes.md)]  | [!INCLUDE [yes](../includes/yes.md)]  | [!INCLUDE [yes](../includes/yes.md)]  |

---

### Fsv2-series availability

Fsv2-series is available in the following regions:

#### [Americas](#tab/americas)

- Brazil South
- Canada Central
- US East
- US West 2

#### [Asia Pacific](#tab/asia)

- East Asia
- Southeast Asia
- Australia Central
- Australia Central 2
- Australia East
- Australia Southeast
- Central India
- Korea Central
- Korea South

#### [Europe, the Middle East, and Africa](#tab/emea)

- Europe North
- Europe West 
- France Central
- South Africa North
- UK South
- UK West

---

### DC-series availability

If you need DC-series in a currently unsupported region, [submit a support request](https://portal.azure.com/#blade/Microsoft_Azure_Support/HelpAndSupportBlade/newsupportrequest). 

DC-series is available in the following regions:

#### [Americas](#tab/americas)

- Canada Central
- US East
- US West

#### [Asia Pacific](#tab/asia)

- Southeast Asia

#### [Europe, the Middle East, and Africa](#tab/emea)

- Europe North
- Europe West
- UK South

---

## Maintenance window availability

Choosing a [maintenance window in Azure SQL Database](maintenance-window.md) other than the default is currently available in certain regions, depending on the option to enable zone-redundancy.

Availability depends on whether your database is configured to be zone redundant. Zone-redundant availability ensures your data is distributed across multiple Azure availability zones in the primary region. Data spans two or three zones, selected by Azure SQL for optimal resilience. The zones are in separate physical locations with independent power, cooling, and networking. For up to date information about the regions that support zone-redundant databases, see [Services support by region](/azure/reliability/availability-zones-region-support).

### Maintenance window availability for databases that are not zone redundant

The following table is for databases that are not [zone-redundant](high-availability-sla-local-zone-redundancy.md#zone-redundant-availability). For databases in an [Azure Availability Zone](high-availability-sla-local-zone-redundancy.md#zone-redundant-availability), see [the table for zone-redundant databases.](#ZR-maintenance-window-availability) 

#### [Americas](#tab/americas)

| Azure Region | Hyperscale premium-series and premium-series memory optimized | Hyperscale standard-series | All other Azure SQL Database purchasing models and tiers |
|:---|:---|:---|:---|
| Brazil South | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] |
| Brazil Southeast | | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] |
| Canada Central | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] |
| Canada East | | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] |
| Central US | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] |
| East US 1 | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] |
| East US 2 | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] |
| North Central US | | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] |
| South Central US | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] |
| West Central US | | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] |
| West US | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] |
| West US 2 | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] |
| West US 3 | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] |
| US Gov Texas | | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] |
| US Gov Virginia | | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] |

#### [Asia Pacific](#tab/asia)

| Azure Region | Hyperscale premium-series and premium-series memory optimized | Hyperscale standard-series | All other Azure SQL Database purchasing models and tiers |
|:---|:---|:---|:---|
| East Asia | | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] |
| Southeast Asia | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] |
| Australia East | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] |
| Australia Southeast | | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] |
| China East 2 | | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] |
| China North 2 | | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] |
| Central India | | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] |
| South India | | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] |
| Japan East | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] |
| Japan West | | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] |

#### [Europe, the Middle East, and Africa](#tab/emea)

| Azure Region | Hyperscale premium-series and premium-series memory optimized | Hyperscale standard-series | All other Azure SQL Database purchasing models and tiers |
|:---|:---|:---|:---|
| North Europe | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] |
| West Europe | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] |
| France Central | | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] |
| France South | | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] |
| Germany West Central | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] |
| South Africa North | | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] |
| Sweden Central | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] |
| Switzerland North | | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] |
| UAE North | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] |
| UK South | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] |
| UK West | | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] |

---

<a id="ZR-maintenance-window-availability"></a>

### Maintenance window availability for zone-redundant databases

The following table is for [zone-redundant](high-availability-sla-local-zone-redundancy.md#zone-redundant-availability) databases.

#### [Americas](#tab/americas)

| Azure Region | Hyperscale premium-series and premium-series memory optimized | Hyperscale standard-series | All other Azure SQL Database purchasing models and tiers in an [Azure Availability Zone](high-availability-sla-local-zone-redundancy.md#zone-redundant-availability) |
|:---|:---|:---|:---|
| Brazil South | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] |
| Canada Central | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] |
| Central US | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] |
| East US 1 | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] |
| East US 2 | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] |
| South Central US | | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] |
| West US 2 | | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] |
| West US 3 | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] |

#### [Asia Pacific](#tab/asia)

| Azure Region | Hyperscale premium-series and premium-series memory optimized | Hyperscale standard-series | All other Azure SQL Database purchasing models and tiers in an [Azure Availability Zone](high-availability-sla-local-zone-redundancy.md#zone-redundant-availability) |
|:---|:---|:---|:---|
| Southeast Asia | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] |
| Australia East | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] |
| Central India | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] |
| Japan East | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] |

#### [Europe, the Middle East, and Africa](#tab/emea)

| Azure Region | Hyperscale premium-series and premium-series memory optimized | Hyperscale standard-series | All other Azure SQL Database purchasing models and tiers in an [Azure Availability Zone](high-availability-sla-local-zone-redundancy.md#zone-redundant-availability) |
|:---|:---|:---|:---|
| North Europe | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] |
| West Europe | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] |
| France Central | | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] |
| Germany West Central | | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] |
| Sweden Central | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] |
| UAE North | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] |
| UK South | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] | [!INCLUDE [yes](../includes/yes.md)] |

---

## Serverless region availability

Serverless is a [compute tier](service-tiers-sql-database-vcore.md#compute) for single databases in Azure SQL Database that automatically scales compute based on workload demand and bills for the amount of compute used per second. For more information, see [Serverless compute tier for Azure SQL Database](serverless-tier-overview.md).

Serverless for General Purpose and Hyperscale tiers is available worldwide, except the following regions: 

- China East
- China North
- Germany Central
- Germany Northeast

Currently, all regions with serverless support 40 vCores and provide [availability zones](high-availability-sla-local-zone-redundancy.md?view=azuresql-db&preserve-view=true). Some regions also support up to a maximum of 80 vCores for General Purpose and Hyperscale tiers and [availability zone support](high-availability-sla-local-zone-redundancy.md?view=azuresql-db&preserve-view=true) according to the following table:

#### [Americas](#tab/americas)

| Azure Region | Supports 80 vCore maximum | Availability zone support for 80 vCores|
|:---|:---|:---|:---|
| Brazil South | [!INCLUDE [yes](../includes/yes.md)]  | [!INCLUDE [yes](../includes/yes.md)]  |
| Brazil Southeast | [!INCLUDE [yes](../includes/yes.md)]  |  |
| Canada Central | [!INCLUDE [yes](../includes/yes.md)]  | [!INCLUDE [yes](../includes/yes.md)]  |
| Canada East | [!INCLUDE [yes](../includes/yes.md)]  |  |
| Mexico Central | [!INCLUDE [yes](../includes/yes.md)]  |  |
| Central US | [!INCLUDE [yes](../includes/yes.md)]  | [!INCLUDE [yes](../includes/yes.md)]  |
| East US | [!INCLUDE [yes](../includes/yes.md)]  | [!INCLUDE [yes](../includes/yes.md)]  |
| East US 2 | [!INCLUDE [yes](../includes/yes.md)]  | [!INCLUDE [yes](../includes/yes.md)]  |
| North Central US | [!INCLUDE [yes](../includes/yes.md)]  |  |
| South Central US | [!INCLUDE [yes](../includes/yes.md)]  | [!INCLUDE [yes](../includes/yes.md)]  |
| West Central US | [!INCLUDE [yes](../includes/yes.md)]  |  |
| West US | [!INCLUDE [yes](../includes/yes.md)]  |  |
| West US 2 | [!INCLUDE [yes](../includes/yes.md)]  | [!INCLUDE [yes](../includes/yes.md)]  |
| West US 3 | [!INCLUDE [yes](../includes/yes.md)]  | [!INCLUDE [yes](../includes/yes.md)]  |

#### [Asia Pacific](#tab/asia)

| Azure Region | Supports 80 vCore maximum | Availability zone support for 80 vCores |
|:---|:---|:---|:---|
| East Asia | [!INCLUDE [yes](../includes/yes.md)]  | [!INCLUDE [yes](../includes/yes.md)]  |
| Southeast Asia | [!INCLUDE [yes](../includes/yes.md)]  | [!INCLUDE [yes](../includes/yes.md)]  |
| Australia Central 1 | [!INCLUDE [yes](../includes/yes.md)]  |  |
| Australia Central 2 | [!INCLUDE [yes](../includes/yes.md)]  |  |
| Australia East | [!INCLUDE [yes](../includes/yes.md)]  | [!INCLUDE [yes](../includes/yes.md)]  |
| Australia Southeast | [!INCLUDE [yes](../includes/yes.md)]  |  |
| China East 2 | [!INCLUDE [yes](../includes/yes.md)]  |  |
| China East 3 | [!INCLUDE [yes](../includes/yes.md)]  |  |
| China North 2 | [!INCLUDE [yes](../includes/yes.md)]  |  |
| China North 3 | [!INCLUDE [yes](../includes/yes.md)]  |  |
| Central India | [!INCLUDE [yes](../includes/yes.md)]  | [!INCLUDE [yes](../includes/yes.md)]  |
| Jio India Central | [!INCLUDE [yes](../includes/yes.md)]  |  |
| Jio India West | [!INCLUDE [yes](../includes/yes.md)]  |  |
| South India | [!INCLUDE [yes](../includes/yes.md)]  |  |
| Japan East | [!INCLUDE [yes](../includes/yes.md)]  | [!INCLUDE [yes](../includes/yes.md)]  |
| Japan West | [!INCLUDE [yes](../includes/yes.md)]  |  |
| Korea Central | [!INCLUDE [yes](../includes/yes.md)]  | [!INCLUDE [yes](../includes/yes.md)]  |
| Korea South | [!INCLUDE [yes](../includes/yes.md)]  |  |
| Malaysia South | [!INCLUDE [yes](../includes/yes.md)]  |  |
| Taiwan North | [!INCLUDE [yes](../includes/yes.md)]  |  |
| Taiwan Northwest | [!INCLUDE [yes](../includes/yes.md)]  |  |

#### [Europe, the Middle East, and Africa](#tab/emea)

| Azure Region | Supports 80 vCore maximum | Availability zone support for 80 vCores|
|:---|:---|:---|:---|
| North Europe | [!INCLUDE [yes](../includes/yes.md)]  | [!INCLUDE [yes](../includes/yes.md)]  |
| West Europe | [!INCLUDE [yes](../includes/yes.md)]  | [!INCLUDE [yes](../includes/yes.md)]  |
| France Central | [!INCLUDE [yes](../includes/yes.md)]  | [!INCLUDE [yes](../includes/yes.md)]  |
| France South | [!INCLUDE [yes](../includes/yes.md)]  |  |
| Germany North | [!INCLUDE [yes](../includes/yes.md)]  |  |
| Germany West Central | [!INCLUDE [yes](../includes/yes.md)]  | [!INCLUDE [yes](../includes/yes.md)]  |
| Israel Central | [!INCLUDE [yes](../includes/yes.md)]  |  |
| Italy North | [!INCLUDE [yes](../includes/yes.md)]  |  |
| Norway East | [!INCLUDE [yes](../includes/yes.md)]  |  |
| Norway West | [!INCLUDE [yes](../includes/yes.md)]  |  |
| Poland Central | [!INCLUDE [yes](../includes/yes.md)]  |  |
| Qatar Central | [!INCLUDE [yes](../includes/yes.md)]  |  |
| South Africa North | [!INCLUDE [yes](../includes/yes.md)]  | [!INCLUDE [yes](../includes/yes.md)]  |
| South Africa West | [!INCLUDE [yes](../includes/yes.md)]  |  |
| Spain Central | [!INCLUDE [yes](../includes/yes.md)]  |  |
| Sweden Central | [!INCLUDE [yes](../includes/yes.md)]  | [!INCLUDE [yes](../includes/yes.md)]  |
| Sweden South | [!INCLUDE [yes](../includes/yes.md)]  |  |
| Switzerland North | [!INCLUDE [yes](../includes/yes.md)]  |  |
| Switzerland West | [!INCLUDE [yes](../includes/yes.md)]  |  |
| UAE Central | [!INCLUDE [yes](../includes/yes.md)]  |  |
| UAE North | [!INCLUDE [yes](../includes/yes.md)]  | [!INCLUDE [yes](../includes/yes.md)]  |
| UK South | [!INCLUDE [yes](../includes/yes.md)]  | [!INCLUDE [yes](../includes/yes.md)]  |
| UK West | [!INCLUDE [yes](../includes/yes.md)]  |  |

---

## Database watcher availability

[Database watcher](../database-watcher-overview.md) is a managed monitoring solution for Azure SQL Database and Azure SQL Managed Instance. It is available in the following regions:

[!INCLUDE [database-watcher](../includes/regional-support/database-watcher.md)]

## Related content

- [What's new in Azure SQL Database?](doc-changes-updates-release-notes-whats-new.md?view=azuresql-db&preserve-view=true)
- [Azure products by region](https://azure.microsoft.com/explore/global-infrastructure/products-by-region/)
- [Designing globally available services using Azure SQL Database](designing-cloud-solutions-for-disaster-recovery.md?view=azuresql-db&preserve-view=true)
- [Move resources to new region - Azure SQL Database](move-resources-across-regions.md?view=azuresql-db&preserve-view=true)
- [Disaster recovery guidance - Azure SQL Database](disaster-recovery-guidance.md)
