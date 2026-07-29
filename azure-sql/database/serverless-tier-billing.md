---
title: Serverless compute tier billing
description: Learn how billing works for the serverless compute tier in Azure SQL Database, including compute costs, minimum billing, and scenario examples.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: kendalv, moslake, mathoma, dfurman
ms.date: 07/28/2026
ms.service: azure-sql-database
ms.subservice: service-overview
ms.topic: concept-article
ai-usage: ai-assisted
monikerRange: "=azuresql||=azuresql-db"
---
# Serverless compute tier billing for Azure SQL Database

[!INCLUDE [appliesto-sqldb](../includes/appliesto-sqldb.md)]

This article explains how billing works for the [serverless compute tier](serverless-tier-overview.md) in Azure SQL Database.

## Serverless compute billing

The amount of compute billed for a serverless database is the maximum of CPU used and memory used each second. If the amount of CPU and memory used is less than the minimum amount you provision for each resource, you pay for the provisioned amount. To compare CPU with memory for billing purposes, memory is normalized into units of vCores by rescaling the number of GB by 3 GB per vCore.

- **Resource billed**: CPU and memory
- **Amount billed**: vCore unit price * maximum (minimum vCores, vCores used, minimum memory GB * 1/3, memory GB used * 1/3) 
- **Billing frequency**: Per second

The vCore unit price is the cost per vCore per second. For more information on specific unit prices in a given region, see [Azure SQL Database pricing page](https://azure.microsoft.com/pricing/details/sql-database/single/).

The following metric exposes the amount of compute billed in serverless for a General Purpose database, or a Hyperscale primary or named replica: 

- **Metric**: app_cpu_billed (vCore seconds)
- **Definition**: maximum (minimum vCores, vCores used, minimum memory GB * 1/3, memory GB used * 1/3)
- **Reporting frequency**: Per minute based on per second measurements aggregated over 1 minute.

The following metric exposes the amount of compute billed in serverless for Hyperscale HA replicas belonging to the primary replica or any named replica: 

- **Metric**: app_cpu_billed_HA_replicas (vCore seconds) 
- **Definition**: Sum of maximum (minimum vCores, vCores used, minimum memory GB * 1/3, memory GB used * 1/3) for any HA replicas belonging to their parent resource.
- **Parent resource and metric endpoint**: The primary replica and any named replica each separately expose this metric, which measures the compute billed for any associated HA replicas. 
- **Reporting frequency**: Per minute based on per second measurements aggregated over 1 minute. 

## Minimum compute bill

If you pause a serverless database, you don't pay for compute. If you don't pause a serverless database, the minimum compute bill depends on the number of vCores. It's calculated as the maximum of the minimum vCores and the result of the minimum memory in GB multiplied by 1/3.

Examples:

- Suppose a serverless database in the General Purpose tier isn't paused and is configured with 8 maximum vCores and 1 minimum vCore corresponding to 3.0 GB minimum memory. Then the minimum compute bill is based on maximum (1 vCore, 3.0 GB * 1 vCore / 3 GB) = 1 vCore.
- Suppose a serverless database in the General Purpose tier isn't paused and is configured with 4 maximum vCores and 0.5 minimum vCores corresponding to 2.1 GB minimum memory. Then the minimum compute bill is based on maximum (0.5 vCores, 2.1 GB * 1 vCore / 3 GB) = 0.7 vCores.
- Suppose a serverless database in the Hyperscale tier has a primary replica with one HA replica and one named replica with no HA replicas. Suppose each replica is configured with 8 maximum vCores and 1 minimum vCore corresponding to 3 GB minimum memory. Then the minimum compute bill for the primary replica, HA replica, and named replica are each based on maximum (1 vCore, 3 GB * 1 vCore / 3 GB) = 1 vCore.

Use the [Azure SQL Database pricing calculator](https://azure.microsoft.com/pricing/calculator/?service=sql-database) for serverless to determine the minimum memory you can configure based on the number of maximum and minimum vCores you set. As a rule, if the minimum vCores you configure is greater than 0.5 vCores, the minimum compute bill is independent of the minimum memory and is based only on the number of minimum vCores you set.

<a id="scenario-examples"></a>

## Scenario examples

# [General Purpose](#tab/general-purpose)

Consider a serverless database in the General Purpose tier configured with 1 minimum vCore and 4 maximum vCores. This configuration corresponds to around 3 GB minimum memory and 12 GB maximum memory. Suppose you set the auto-pause delay to 6 hours and the database workload is active during the first 2 hours of a 24-hour period and otherwise inactive.

In this case, you pay for compute and storage during the first 8 hours. Even though the database is inactive starting after the second hour, you still pay for compute in the subsequent 6 hours based on the minimum compute provisioned while the database is online. You pay only for storage during the remainder of the 24-hour period while the database is paused.

More precisely, the compute bill in this example is calculated as follows:

|Time Interval|vCores used each second|GB used each second|Compute dimension billed|vCore seconds billed over time interval|
|---|---|---|---|---|
|0:00-1:00|4|9|vCores used|4 vCores * 3,600 seconds = 14,400 vCore seconds|
|1:00-2:00|1|12|Memory used|12 GB * 1/3 * 3,600 seconds = 14,400 vCore seconds|
|2:00-8:00|0|0|Minimum memory provisioned|3 GB * 1/3 * 21,600 seconds = 21,600 vCore seconds|
|8:00-24:00|0|0|No compute billed while paused|0 vCore seconds|
|**Total vCore seconds billed over 24 hours**||||50,400 vCore seconds|

Suppose the compute unit price is $0.000145/vCore/second. Then the compute billed for this 24-hour period is the product of the compute unit price and vCore seconds billed: $0.000145/vCore/second * 50,400 vCore seconds ~ $7.31.

# [Hyperscale](#tab/hyperscale)

Consider a serverless database in the Hyperscale tier configured with 1 minimum vCore and 8 maximum vCores. Suppose that the primary replica has one HA replica and one named replica with the same resources as the primary replicas. For each replica, this configuration corresponds to 3 GB minimum memory and 24 GB maximum memory. Further suppose that write workload occurs throughout a 24-hour period, but that read-only workload occurs just during the first 8 hours of this time period.

In this example, the compute billed for the database is the sum of the compute billed for each replica. Calculate it as follows based on the usage pattern described in the following tables: 

**Primary replica**

| Time Interval    | vCores used each second    | GB used each second    | Compute dimension billed | vCore seconds billed over time interval | 
|---|---|---|---|---|
|0:00-2:00 | 8    | 15 |    vCores used    | 8 vCores * 7200 seconds = 57600 vCore seconds |
|2:00-14:00 |    1.5    | 6     | Memory used |    6 GB * 1/3 * 43200 seconds = 86400 vCore seconds |
|14:00-24:00 |    0.5    | 2     | Minimum vCores provisioned    | 1 vCore * 36000 seconds = 36000 vCore seconds | 
|**Total vCore seconds billed over 24 hours** |||| 180,000 vCore seconds |

Suppose the compute unit price for the primary replica is $0.000105/vCore/second. Then the compute billed for the primary replica over this 24-hour period is the product of the compute unit price and vCore seconds billed: $0.000105/vCore/second * 180000 vCore seconds ~ $18.90.

**HA replica**

|Time Interval    | vCores used each second    | GB used each second    | Compute dimension billed    | vCore seconds billed over time interval |
|---|---|---|---|---|
|0:00-2:00 |    8 |    9    | vCores used    | 8 vCores * 7,200 seconds = 57,600 vCore seconds |
| 2:00-8:00    | 1.5     | 3    | Memory used    | 3 GB * 1/3 * 43,200 seconds = 43,200 vCore seconds|
|8:00-24:00|    0|    2    |Minimum memory provisioned    |3 GB * 1/3 * 36,000 seconds = 36,000 vCore seconds|
|**Total vCore seconds billed over 24 hours**||||136,800 vCore seconds |

Suppose the compute unit price for an HA replica is $0.000105/vCore/second. Then the compute billed for the HA replica over this 24-hour period is $0.000105/vCore/second * 136,800 vCore seconds ~ $14.36.

**Named replica** 

Similarly for the named replica, suppose the total vCore seconds billed over 24 hours is 150,000 vCore seconds and that the compute unit price for a named replica is $0.000105/vCore/second. Then the compute billed for the named replica over this time period is $0.000105/vCore/second * 150,000 vCore seconds ~ $15.75.

**Total compute cost**

Therefore, the total compute bill for all three replicas of the database is around $18.90 + $14.36 + $15.75 = $49.01.

---

## Azure Hybrid Benefit and reservations

[Azure Hybrid Benefit (AHB)](../azure-hybrid-benefit.md) and [Azure Reservations discounts](reservations-discount-overview.md) don't apply to the serverless compute tier.

## Related content

- [Serverless compute tier for Azure SQL Database](serverless-tier-overview.md)
- [Auto-pause and auto-resume in serverless](serverless-tier-auto-pause-resume.md)
- [Azure SQL Database pricing page](https://azure.microsoft.com/pricing/details/sql-database/single/)
