---
title: Request a quota increase
titleSuffix: Azure SQL Database & Azure SQL Managed Instance
description: This page describes how to create a support request to increase the quotas for Azure SQL Database and Azure SQL Managed Instance.
author: sachinpMSFT
ms.author: sachinp
ms.reviewer: wiassaf, mathoma, randolphwest
ms.date: 08/26/2024
ms.service: azure-sql
ms.subservice: deployment-configuration
ms.topic: how-to
monikerRange: "= azuresql || = azuresql-db || = azuresql-mi"
---

# Request quota increases for Azure SQL Database and SQL Managed Instance

[!INCLUDE [appliesto-sqldb-sqlmi](../includes/appliesto-sqldb-sqlmi.md)]

This article explains how to request a quota increase for Azure SQL Database and Azure SQL Managed Instance, as well as request subscription access and zone redundancy for Azure SQL Database. 

## <a id="newquota"></a> Create quota increase request

To request an increase to your quota, follow these steps: 

1. Go to the **New support request** page in the Azure portal by following the steps to [Open a support request](/azure/azure-portal/supportability/how-to-create-azure-support-request). 
1. On the **Problem description** tab of the **New support request** page, choose *Service and subscription limits (quotas)* for the **Issue type**, and your subscription from the drop-down. For **Quota type**, type in `sql` and choose the appropriate product:

   - **SQL Database** for single database and elastic pool quotas.
   - **SQL Database Managed Instance** for managed instances.

   :::image type="content" source="./media/quota-increase-request/select-quota-issue-type.png" alt-text="Screenshot of the Azure portal, Select an issue type.":::

1. Select **Next: Solutions >>** to go to the **Additional details** tab.

1. On the **Additional details** tab, under **Problem details**, select **Enter details** to enter additional information.

   :::image type="content" source="./media/quota-increase-request/provide-details-link.png" alt-text="Screenshot of the Azure portal, Enter details link.":::

Selecting **Enter details** displays the **Quota details** window that allows you to add additional information. The following sections describe the different options for **SQL Database** and **SQL Managed Instance** quota types.

## <a id="sqldbquota"></a> SQL Database quota request types

The following sections describe the quota increase options for the **SQL Database** quota types:

- vCores per subscription
- Region access
- Zone Redundant Access (Availability Zones)

Regardless of what [purchasing model](purchasing-models.md) you use for your Azure SQL Database, quota requests are made by using vCores. 

If your SQL database uses the DTU purchasing model, then use the following calculation to determine how many vCores correlate to the DTU quota increase you're requesting: 

`1 vCore ~ 100-125 DTU`. 

For example, a subscription that uses 1000 DTUs consumes about 10 vCores. 


### vCores per subscription 

Use *vCores per subscription* to request increases to your compute quotas. For databases that use the DTU-purchasing model, use the `1 vCore ~ 100-125 DTU` formula to determine your new limits in vCores. For example, if your subscription is currently limited to 100 DTUs, and you want to increase your DTU capacity to 1000 DTUs, then request a quota increase of 10 vCores. 

To increase your compute quota, follow these steps: 

1. For **SQL database quota type**, choose *vCores per subscription*. 
1. Provide a location. 
1. Provide the new quota, in vCores. Then select **Save and continue** to save your changes and navigate back to the **New support request** page. 
 
   :::image type="content" source="./media/quota-increase-request/quota-details-dtus.png" alt-text="Screenshot of the Azure portal, DTU quota details.":::


For more information, see [Resource limits for single databases using the DTU purchasing model](resource-limits-dtu-single-databases.md) and [Resources limits for elastic pools using the DTU purchasing model](resource-limits-dtu-elastic-pools.md).


### <a id="region"></a> Enable subscription access to a region

Use *Region access* to request creating a resource in a selected region since some subscriptions have limits to where they can create resources. You might see an error such as: 

`Your subscription does not have access to create a server in the selected region.`

Consumption is calculated in vCores, regardless of your [purchasing model](purchasing-models.md), so if you're using the DTU-based purchasing model, use the following formula to convert your expected DTU consumption to vCores: 

`1 vCore ~ 100-125 DTU`. 

To request region access, follow these steps: 

1. Select the **Region access** quota type on the **Quota details** window.
1. Use the **Location** dropdown to select the Azure region where you want access. The quota is per subscription in each region.
1. Enter the **Expected Consumption** in vCores. Then select **Save and continue** to save your changes and navigate back to the **New support request** page. 


> [!NOTE]
> Not all service tiers are available in all regions. Use the [Azure SQL Database pricing](https://azure.microsoft.com/pricing/details/sql-database/single/) page to determine region and service tier availability. 

### Zone Redundant Access

Use *Zone Redundant Access (Availability Zones)* to request [zone redundant storage](high-availability-sla.md) support in a specific region, since not every region in every subscription supports availability zones. You might see an error such as: 

`Provisioning of zone redundant database/pool is not supported for your current request`. 

To request zone redundant access, follow these steps: 

1. Select the **Zone Redundant Access (Availability Zones)** quota type on the **Quota details** window. 
1. Use the **Location** dropdown to select the Azure region where you want to use availability zones. 
1. Enter the **Expected Consumption** in vCores. Then select **Save and continue** to save your changes and navigate back to the **New support request** page. 

> [!NOTE]
> Not all regions support availability zones. Review [Availability zones](/azure/reliability/cross-region-replication-azure#azure-paired-regions) for more information. 

## SQL Managed Instance quota request types 

With Azure SQL Managed Instance, use the **Quota details** window to request increase limits for:

- The number of available subnets in a region 
- Compute capacity in a region, calculated by vCores


Specify your new limits on the **Quota details** window, and then use **Save and continue** to apply your new limits and navigate back to the **New support request** page. 


## Submit your request

The final step is to fill in the remaining details of your quota request. Then select **Next: Review + create>>**, and after reviewing the request details, select **Create** to submit the request.

## Next step

> [!div class="nextstepaction"]
> [Azure subscription and service limits, quotas, and constraints](/azure/azure-resource-manager/management/azure-subscription-service-limits)
