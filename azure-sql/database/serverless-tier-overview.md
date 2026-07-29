---
title: Serverless compute tier
description: This article describes the new serverless compute tier and compares it with the existing provisioned compute tier for Azure SQL Database.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: kendalv, moslake, mathoma, dfurman
ms.date: 07/28/2026
ms.service: azure-sql-database
ms.subservice: service-overview
ms.topic: concept-article
ms.custom:
  - "test sqldbrb=1"
  - "devx-track-azurecli"
  - "devx-track-azurepowershell"
monikerRange: "=azuresql||=azuresql-db"
---
# Serverless compute tier for Azure SQL Database
[!INCLUDE [appliesto-sqldb](../includes/appliesto-sqldb.md)]

Serverless is a [compute tier](service-tiers-sql-database-vcore.md#compute) for single databases in Azure SQL Database that automatically scales compute based on workload demand and bills for the amount of compute used per second. The serverless compute tier also automatically pauses databases during inactive periods when only storage is billed and automatically resumes databases when activity returns. The serverless compute tier is available in the [General Purpose](service-tiers-sql-database-vcore.md#general-purpose) service tier and the [Hyperscale](service-tier-hyperscale.md) service tier.

Currently, auto-pause and auto-resume are only supported in the General Purpose service tier.

## Overview

A compute autoscaling range and an auto-pause delay are important parameters for the serverless compute tier. The configuration of these parameters shapes the database performance experience and compute cost.

:::image type="content" source="media/serverless-tier-overview/serverless-billing.png" alt-text="Diagram indicating when serverless billing would stop incurring compute charges due to inactivity.":::

### Performance configuration

- The **minimum vCores** and **maximum vCores** are configurable parameters that define the range of compute capacity available for the database. Memory and IO limits are proportional to the vCore range specified.  
- The **auto-pause delay** is a configurable parameter that defines the period of time the database must be inactive before it's automatically paused. The database is automatically resumed when the next login or other activity occurs. Alternatively, you can disable automatic pausing. For more information, see [Auto-pause and auto-resume in the serverless compute tier for Azure SQL Database](serverless-tier-auto-pause-resume.md).

### Default settings

The following table shows the available values for these parameters. 

   |Parameter|Value choices|Default value|
   |---|---|---|---|
   |Minimum vCores|Depends on maximum vCores configured - see [resource limits](resource-limits-vcore-single-databases.md#general-purpose---serverless-compute---gen5).|0.5 vCores|
   |Auto-pause delay|Minimum: 15 minutes<br>Maximum: 10,080 minutes (seven days)<br>Increments: 1 minute<br>Disable auto-pause: -1|60 minutes|

### Cost

The cost for a serverless database is the sum of the compute cost and storage cost. The storage cost is determined in the same way as in the provisioned compute tier.

- When compute usage is between the minimum and maximum limits you configure, the compute cost is based on vCore and memory used.
- When compute usage is below the minimum limits you configure, the compute cost is based on the minimum vCores and minimum memory configured.
- When the database is paused, the compute cost is zero and only storage costs are incurred.

For more cost details, see [Serverless compute tier billing and cost scenarios](serverless-tier-billing.md).

## Scenarios

Serverless is price-performance optimized for single databases with intermittent, unpredictable usage patterns that can afford some delay in compute warm-up after idle usage periods. In contrast, the [provisioned compute tier](service-tiers-sql-database-vcore.md#compute) is price-performance optimized for single databases or multiple databases in [elastic pools](elastic-pool-overview.md) with higher average usage that can't afford any delay in compute warm-up.

### Scenarios well suited for serverless compute

- Single databases with intermittent, unpredictable usage patterns interspersed with periods of inactivity, and lower average compute utilization over time.
- Single databases in the provisioned compute tier that are frequently rescaled and customers who prefer to delegate compute rescaling to the service.
- New single databases without usage history where compute sizing is difficult or not possible to estimate before deployment in an Azure SQL Database.

### Scenarios well suited for provisioned compute

- Single databases with more regular, predictable usage patterns and higher average compute utilization over time.
- Databases that can't tolerate performance trade-offs resulting from more frequent memory trimming or delays in resuming from a paused state.
- Multiple databases with intermittent, unpredictable usage patterns that can be consolidated into elastic pools for better price-performance optimization.

### Compare compute tiers

The following table summarizes distinctions between the serverless compute tier and the provisioned compute tier:

| | **Serverless compute** | **Provisioned compute** |
|:---|:---|:---|
|**Database usage pattern**| Intermittent, unpredictable usage with lower average compute utilization over time. | More regular usage patterns with higher average compute utilization over time, or multiple databases using elastic pools.|
| **Performance management effort** |Lower|Higher|
|**Compute scaling**|Automatic|Manual|
|**Compute responsiveness**|Lower after inactive periods|Immediate|
|**Billing granularity**|Per second|Per hour|

## Purchasing model and service tier

The following table describes serverless support based on purchasing model, service tiers, and hardware:

| **Category** | **Supported** | **Not supported**|
|:---|:---|:---|
| **Purchasing model** | [vCore](service-tiers-vcore.md) | [DTU](service-tiers-dtu.md) |
| **Service tier** | [General Purpose](service-tiers-sql-database-vcore.md#general-purpose) <br/> [Hyperscale](service-tier-hyperscale.md) | Business Critical| 
| **Hardware** | Standard-series (Gen5) | All other hardware |  

<a id="autoscaling"></a>
<a id="auto-scaling"></a>

## Auto-scale serverless databases

For more information about auto-scaling and the responsiveness of the serverless compute tier, see [Serverless compute tier auto-scaling for Azure SQL Database](serverless-tier-auto-scaling.md).

<a id="auto-pausing-and-auto-resuming"></a>
<a id="auto-pause-and-auto-resume"></a>

## Auto-pause and auto-resume

For detailed information about auto-pause and auto-resume behavior, triggers, troubleshooting, and connectivity, see [Auto-pause and auto-resume in the serverless compute tier](serverless-tier-auto-pause-resume.md).

<a id="create-serverless-db"></a>

## Create a new serverless database

For step-by-step instructions on creating, moving, and configuring serverless databases using the Azure portal, PowerShell, Azure CLI, and T-SQL, see [Create and configure a serverless database](serverless-tier-create-configure.md).

## Serverless compute tier best practices

Consider the following best practices when using serverless databases.

<a id="connectivity"></a>

### Implement application retry logic 

All cloud-connected applications should use [connection retry logic recommendations](/azure/architecture/patterns/retry). Transient connectivity errors must be handled with retry logic in all cloud applications. Retry logic for serverless databases is especially important because temporary connectivity errors due to auto-resume are predictable.  

For more information, see [Troubleshooting auto-resume connectivity](serverless-tier-auto-pause-resume.md#troubleshooting-auto-resume-connectivity).

### Review interoperability that prevents auto-pause

Some features and settings can [prevent a serverless database from auto-pausing](serverless-tier-auto-pause-resume.md#features-that-prevent-auto-pause). You can disable auto-pause on the database if these features are in use.

For more information, see [Troubleshooting auto-pause](serverless-tier-auto-pause-resume.md#troubleshooting-auto-pause).

<a id="monitoring"></a>

## Monitor

For more information on monitoring pause and resume events of your serverless database, see [Monitor the serverless compute tier for Azure SQL Database](serverless-tier-monitor.md).

## Resource limits

For resource limits, see [serverless compute tier](resource-limits-vcore-single-databases.md#general-purpose---serverless-compute---gen5).

## Billing

For detailed billing information, including compute cost calculations, minimum compute bill, and scenario examples for both General Purpose and Hyperscale tiers, see [Serverless compute tier billing](serverless-tier-billing.md).

## Available regions

For regional availability, see [Serverless availability by region for Azure SQL Database](region-availability.md#serverless-region-availability).

## Next step

> [!div class="nextstepaction"]
> [Create a serverless database](serverless-tier-create-configure.md)

## Related content

- [Serverless compute tier FAQ](serverless-tier-faq.yml)
- [Serverless compute tier billing](serverless-tier-billing.md)
- [Auto-pause and auto-resume in serverless](serverless-tier-auto-pause-resume.md)
- [Modifiable configuration reference for Azure SQL Database](modifiable-configuration-reference.md)
