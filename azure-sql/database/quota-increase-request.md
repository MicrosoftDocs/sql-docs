---
title: Request a quota increase
titleSuffix: Azure SQL Database & Azure SQL Managed Instance
description: This page describes how to create a support request to increase the quotas for Azure SQL Database and Azure SQL Managed Instance.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: wiassaf, mathoma, randolphwest
ms.date: 05/21/2026
ms.service: azure-sql
ms.subservice: deployment-configuration
ms.topic: how-to
monikerRange: "= azuresql || = azuresql-db || = azuresql-mi"
---

# Request quota increases for Azure SQL Database and SQL Managed Instance

[!INCLUDE [appliesto-sqldb-sqlmi](../includes/appliesto-sqldb-sqlmi.md)]

This article explains how to request a quota increase for Azure SQL Database and Azure SQL Managed Instance, as well as how to request subscription access and zone redundancy for Azure SQL Database. 

<a id="newquota"></a>

## Create quota increase request

To request a quota increase, follow these steps:

1. Go to the [New support request](https://portal.azure.com/#view/Microsoft_Azure_Support/HelpAndSupportBlade/~/overview) page in the Azure portal by following the steps to [Open a support request](/azure/azure-portal/supportability/how-to-create-azure-support-request).
1. Select **Create a support request** to open the **Support + troubleshooting** pane.
1. In the **Support + troubleshooting** pane, search for `quota`. Under **Which service are you having an issue with?**, select **None of the above** and then choose **Service and subscription limits (quotas)** from the **Select a service** drop-down menu. Select **Next**.

   :::image type="content" source="media/quota-increase-request/support-troubleshooting-quota-search.png" alt-text="Screenshot from the Azure portal showing the Support + troubleshooting pane. Search for quota, select None of the above, and then choose Databases / SQL Database or Databases / SQL Managed Instance.":::

1. Select your subscription. Select **Next**.
1. For the **Problem type**, select **SQL Database** for single database and elastic pool quotas, or **SQL Database Managed Instance** for SQL managed instances. Select **Next**.
1. Select **Create a support request**.
1. Review details and then select **Next: Support method**.
1. For **Support Method**, select the desired severity, options, and contact method. Select **Next: Add additional details**.
1. For **Provide details for this request**, select **Enter details**.
1. On the **Quota details** page, select a **Region** from the drop-down menu and then provide the new desired quota limit.
1. Provide the requested **New quota** information. For more information, see [Azure quotas](https://aka.ms/quotalimits).
1. Select **Save and continue**.
1. In the **Support + troubleshooting** pane, select **Next: add contact details**.
1. In the **Confirm contact info** section, confirm your contact information.
1. Select **Create support request**.

For more information about quota requests, see the following sections.

<a id="sqldbquota"></a>

## SQL Database quota request types

The following sections describe the quota increase options for the **SQL Database** quota types:

- vCores per subscription
- Region access
- Zone Redundant Access (Availability Zones)

Regardless of what [purchasing model](purchasing-models.md) you use for your Azure SQL Database, quota requests are made by using vCores. 

If your SQL database uses the DTU purchasing model, use the following calculation to determine how many vCores correlate to the DTU quota increase you're requesting: 

`1 vCore ~ 100-125 DTU`. 

For example, a subscription that uses 1,000 DTUs consumes about 10 vCores. 

### vCores per subscription 

Use *vCores per subscription* to request increases to your compute quotas. For databases that use the DTU-purchasing model, use the `1 vCore ~ 100-125 DTU` formula to determine your new limits in vCores. For example, if your subscription is currently limited to 100 DTUs, and you want to increase your DTU capacity to 1,000 DTUs, request a quota increase of 10 vCores. 

To increase your compute quota, follow these steps: 

1. For **SQL database quota type**, choose *vCores per subscription*. 
1. Enter a location. 
1. Enter the new quota, in vCores. Then select **Save and continue** to save your changes and go back to the **New support request** page. 

For more information, see [Resource limits for single databases using the DTU purchasing model](resource-limits-dtu-single-databases.md) and [Resources limits for elastic pools using the DTU purchasing model](resource-limits-dtu-elastic-pools.md).

<a id="region"></a>

### Enable subscription access to a region

Use *Region access* to request creating a resource in a selected region since some subscriptions have limits to where they can create resources. You might see an error such as: 

`Your subscription does not have access to create a server in the selected region.`

Consumption is calculated in vCores, regardless of your [purchasing model](purchasing-models.md), so if you're using the DTU-based purchasing model, use the following formula to convert your expected DTU consumption to vCores: 

`1 vCore ~ 100-125 DTU`. 

To request region access, follow these steps: 

1. Select the **Region access** quota type on the **Quota details** window.
1. Use the **Location** dropdown to select the Azure region where you want access. The quota is per subscription in each region.
1. Enter the **Expected Consumption** in vCores. Then select **Save and continue** to save your changes and go back to the **New support request** page. 

> [!NOTE]
> Not all service tiers are available in all regions. Use the [Azure SQL Database pricing](https://azure.microsoft.com/pricing/details/sql-database/single/) page to determine region and service tier availability. 

### Zone redundant access

Use *Zone Redundant Access (Availability Zones)* to request [zone redundant storage](high-availability-sla.md) support in a specific region, since not every region in every subscription supports availability zones. You might see an error such as: 

`Provisioning of zone redundant database/pool is not supported for your current request`. 

To request zone redundant access, follow these steps: 

1. Select the **Zone Redundant Access (Availability Zones)** quota type on the **Quota details** window. 
1. Use the **Location** dropdown to select the Azure region where you want to use availability zones. 
1. Enter the **Expected Consumption** in vCores. Then select **Save and continue** to save your changes and navigate back to the **New support request** page. 

> [!NOTE]
> Not all regions support availability zones. Review [Availability zones region support](/azure/reliability/availability-zones-region-support) for more information. 

## SQL Managed Instance quota request types

With Azure SQL Managed Instance, use the **Quota details** window to request increased limits for compute capacity in a region for Standard-Series, Premium-Series, and Memory Optimized Premium-Series hardware generations, calculated by vCores.

Specify your new limits on the **Quota details** window, and then use **Save and continue** to apply your new limits and return to the **New support request** page.

Starting August 2026, quota request types for SQL Managed Instance changed:

- **Subnets**: Subnet quotas no longer apply. There's no limit on the number of subnets where you can deploy SQL Managed Instance.
- **vCores**: vCore quotas are now scoped per hardware generation (Standard-Series, Premium-Series, and Memory Optimized Premium-Series) rather than a single regional limit across all hardware.

If you previously requested a regional vCore quota increase, your existing allowance is carried forward as the same amount for each hardware generation (Standard-series, Premium-series, and Memory optimized premium-series) in that region.

## Next step

> [!div class="nextstepaction"]
> [Azure subscription and service limits, quotas, and constraints](/azure/azure-resource-manager/management/azure-subscription-service-limits)
