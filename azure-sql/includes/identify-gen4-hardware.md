---
title: Inform customers how to find Gen4 resources
description: Inform customers how to find Gen4 resources.
author: MashaMSFT
ms.author: mathoma
ms.date: 04/03/2023
ms.topic: include
---
You can use [Azure Resource Graph Explorer](/azure/governance/resource-graph/overview) to identify all Azure SQL Database resources that currently use Gen4 hardware, or you can check the hardware used by resources for a specific [logical server](../database/logical-servers.md) in the Azure portal. 

You must have at least `read` permissions to the Azure object or object group to see results in Azure Resource Graph Explorer. 

To use **Resource Graph Explorer** to identify Azure SQL resources that are still using Gen4 hardware, follow these steps: 

1. Go to the [Azure portal](https://portal.azure.com). 
1. Search for `Resource graph` in the search box, and choose the **Resource Graph Explorer** service from the search results. 
1. In the query window, type the following query and then select **Run query**: 

   ```
   resources
   | where type contains ('microsoft.sql/servers')
   | where sku['family'] == "Gen4"
   ```

1. The **Results** pane displays all the currently deployed resources in Azure that are using Gen4 hardware.

   :::image type="content" source="media/identify-gen4-hardware/identify-gen4-resource-graph-explorer.png" alt-text="Screenshot of Azure Resources Graph Explorer in the Azure portal showing query results to identify gen4 hardware.":::



To check the hardware used by resources for a specific logical server in Azure, follow these steps: 

1. Go to the [Azure portal](https://portal.azure.com). 
1. Search for `SQL servers` in the search box and choose **SQL servers** from the search results to open the **SQL servers** page and view all servers for the chosen subscription(s). 
1. Select the server of interest to open the **Overview** page for the server. 
1. Scroll down to available resources and check the **Pricing tier** column for resources that are using gen4 hardware. 

:::image type="content" source="media/identify-gen4-hardware/identify-gen4-hardware.png" alt-text="Screenshot of the Overview page for a logical server in Azure, the overview page selected, and gen4 highlighted. ":::

To migrate resources to standard-series hardware, review [Change hardware](../database/service-tiers-sql-database-vcore.md#selecting-hardware-configuration). 
