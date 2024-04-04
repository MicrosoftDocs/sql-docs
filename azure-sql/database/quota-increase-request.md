---
title: Request a quota increase
titleSuffix: Azure SQL Database & Azure SQL Managed Instance
description: This page describes how to create a support request to increase the quotas for Azure SQL Database and Azure SQL Managed Instance.
author: sachinpMSFT
ms.author: sachinp
ms.reviewer: wiassaf, mathoma, randolphwest
ms.date: 10/23/2023
ms.service: sql-db-mi
ms.subservice: deployment-configuration
ms.topic: how-to
monikerRange: "= azuresql || = azuresql-db || = azuresql-mi"
---

# Request quota increases for Azure SQL Database and SQL Managed Instance

[!INCLUDE [appliesto-sqldb-sqlmi](../includes/appliesto-sqldb-sqlmi.md)]

This article explains how to request a quota increase for Azure SQL Database and Azure SQL Managed Instance. It also explains how to enable subscription access to a region and how to request enabling specific hardware in a region.

## <a id="newquota"></a> Create a new support request

Use the following steps to create a new support request from the Azure portal for SQL Database.

1. On  the [Azure portal](https://portal.azure.com) menu, select **Help + support**.

   :::image type="content" source="media/quota-increase-request/help-plus-support.png" alt-text="Screenshot of the Azure portal left side navigation menu that highlights the Help + support menu option." lightbox="media/quota-increase-request/help-plus-support.png":::

1. In **Help + support**, select **New support request**.

    :::image type="content" source="./media/quota-increase-request/new-support-request.png" alt-text="Screenshot of the Azure portal, Create a new support request.":::

1. For **Issue type**, select **Service and subscription limits (quotas)**.

   :::image type="content" source="./media/quota-increase-request/select-quota-issue-type.png" alt-text="Screenshot of the Azure portal, Select an issue type.":::

1. For **Subscription**, select the subscription whose quota you want to increase.

   :::image type="content" source="./media/quota-increase-request/select-subscription-support-request.png" alt-text="Screenshot of the Azure portal, select a subscription for an increased quota.":::

1. For **Quota type**, select one of the following quota types:

   - **SQL Database** for single database and elastic pool quotas.
   - **SQL Database Managed Instance** for managed instances.

   Then select **Next: Solutions >>**.

   :::image type="content" source="./media/quota-increase-request/select-quota-type.png" alt-text="Screenshot of the Azure portal, select a quota type.":::

1. In the **Details** window, select **Enter details** to enter additional information.

   :::image type="content" source="./media/quota-increase-request/provide-details-link.png" alt-text="Screenshot of the Azure portal, Enter details link.":::

Selecting **Enter details** displays the **Quota details** window that allows you to add additional information. The following sections describe the different options for **SQL Database** and **SQL Managed Instance** quota types.

## <a id="sqldbquota"></a> SQL Database quota types

The following sections describe the quota increase options for the **SQL Database** quota types:

- Database transaction units (DTUs) per server
- Servers per subscription
- Region access for subscriptions or specific hardware

### Database transaction units (DTUs) per server

Use the following steps to request an increase in the DTUs per server.

1. Select the **Database transaction units (DTUs) per server** quota type.

1. In the **Resource** list, select the resource to target.

1. In the **New quota** field, enter the new DTU limit that you're requesting. Then select **Save and continue** to save your changes. 

   :::image type="content" source="./media/quota-increase-request/quota-details-dtus.png" alt-text="Screenshot of the Azure portal, DTU quota details.":::

For more information, see [Resource limits for single databases using the DTU purchasing model](resource-limits-dtu-single-databases.md) and [Resources limits for elastic pools using the DTU purchasing model](resource-limits-dtu-elastic-pools.md).

### Servers per subscription

Use the following steps to request an increase in the number of servers per subscription.

1. Select the **Servers per subscription** quota type.

1. In the **Location** list, select the Azure region to use. The quota is per subscription in each region.

1. In the **New quota** field, enter your request for the maximum number of servers in that region. Then select **Save and continue** to save your changes. 

   :::image type="content" source="./media/quota-increase-request/quota-details-servers.png" alt-text="Screenshot of the Azure portal, Servers quota details.":::

For more information, see [SQL Database resource limits and resource governance](resource-limits-logical-server.md).

### <a id="region"></a> Enable subscription access to a region

Some offer types aren't available in every region. You might see an error such as the following:

`Your subscription does not have access to create a server in the selected region. For the latest information about region availability for your subscription, go to aka.ms/sqlcapacity. Please try another region or create a support ticket to request access.`

If your subscription needs access in a particular region, select the **Region access** option. In your request, specify the offering and SKU details that you want to enable for the region. To explore the offering and SKU options, see [Azure SQL Database pricing](https://azure.microsoft.com/pricing/details/sql-database/single/).

1. Select the **Region access** quota type.

1. In the **Select a location** list, select the Azure region to use. The quota is per subscription in each region.

1. Enter the **Expected Consumption** details. Then select **Save and continue** to save your changes. 

   :::image type="content" source="./media/quota-increase-request/quota-request.png" alt-text="Screenshot of the Azure portal, Request region access.":::

### Request enabling specific hardware in a region

If the hardware you want to use isn't available in your region, you might request it using the following steps. For more information on hardware regional availability, see [Hardware configurations for SQL Database](./service-tiers-sql-database-vcore.md#hardware-configuration) or [Hardware configurations for SQL Managed Instance](../managed-instance/service-tiers-managed-instance-vcore.md#hardware-configurations).

1. Select the **Other quota request** quota type.

1. In the **Description** field, state your request, including the name of the hardware and the name of the region you need it in.

   :::image type="content" source="./media/quota-increase-request/hardware-in-new-region.png" alt-text="Screenshot of the Azure portal, Request hardware in a new region.":::

## Submit your request

The final step is to fill in the remaining details of your SQL Database quota request. Then select **Next: Review + create>>**, and after reviewing the request details, select **Create** to submit the request.

## Next step

> [!div class="nextstepaction"]
> [Azure subscription and service limits, quotas, and constraints](/azure/azure-resource-manager/management/azure-subscription-service-limits)
