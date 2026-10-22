---
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 06/22/2026
ms.topic: include
---

After you connect your [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] instances with Azure Arc, you can subscribe to receive ESUs.

To subscribe to ESUs in the Azure portal, follow these steps:

1. Go to the [Azure Arc | Machines](https://portal.azure.com/#servicemenu/Microsoft_Azure_ArcCenterUX/AzureArcCenterHub/servers) page in the Azure portal.

1. Select your server from the list to open the **Overview** pane for that server:

   :::image type="content" source="media/esu-enable-sql-server-instances/select-arc-machine.png" alt-text="Screenshot of a list of servers, with one server highlighted.":::

1. Under **Operations**, select **SQL Server Configuration** to open the **SQL Server Configuration** pane, where you can subscribe to ESUs:

   :::image type="content" source="media/esu-enable-sql-server-instances/subscribe-to-esu.png" alt-text="Screenshot of the SQL Server Configuration pane, with the Subscribe to Extended Security Updates option highlighted.":::

1. Select **Save** to subscribe to ESUs for that instance.

Once subscribed, ESUs are automatically installed on instances that have automatic updates enabled. If you have instances with automatic updates disabled, you can manually download ESUs from the **Extended Security Updates** pane for your [SQL Server instance](https://portal.azure.com/#servicemenu/SqlAzureExtension/AzureSqlHub/SqlServerInstance) in the Azure portal.
