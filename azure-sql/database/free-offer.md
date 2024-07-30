---
title: Try for free (preview)
description: Guidance on how to deploy the Azure SQL Database free offer for new accounts.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: amapatil, mathoma
ms.date: 02/27/2024
ms.service: azure-sql-database
ms.subservice: service-overview
ms.topic: how-to
ms.custom: 
monikerRange: "=azuresql || =azuresql-db"
---
# Try Azure SQL Database for free (preview)

> [!div class="op_single_selector"]
> * [Azure SQL Database](free-offer.md?view=azuresql-db&preserve-view=true)
> * [Azure SQL Managed Instance](../managed-instance/free-offer.md?view=azuresql-mi&preserve-view=true)

Try Azure SQL Database free of charge and get 100,000 vCore seconds of compute every month. This free offer provides a General Purpose database for the lifetime of your subscription.

The free SQL Database offer is designed for new Azure customers looking to get started with Azure SQL Database, and existing customers that might need a development database for a proof of concept.

To learn more about the offer, see this short video:

> [!VIDEO https://learn-video.azurefd.net/vod/player?id=dd55c855-df9f-4627-96c1-de0da0425cea]

This offer is available for one database per Azure subscription. To get started, look for the **Apply offer** banner on the [provisioning page for Azure SQL Database](https://portal.azure.com/#create/Microsoft.SQLDatabase).

:::image type="content" source="media/free-offer/azure-sql-database-free-banner.png" alt-text="Screenshot from the Azure portal of the Free Offer banner.":::

You know the offer has been applied when the **Cost summary** card on the right side of the page shows **Estimated Cost/Month** $0.

:::image type="content" source="media/free-offer/cost-summary-card.png" alt-text="Screenshot from the Azure portal of the Free Offer Cost summary card. Included in the details are 'First 32 G B of storage free' and 'First 100,000 vCore seconds free'.":::

The free Azure SQL Database offer is currently in preview.

## Monthly free limits

The Azure SQL Database you get with the free offer is the same fully managed platform as a service (PaaS) database with database management functions such as upgrading, patching, backups, and monitoring handled without user involvement. The offer is available for one database per Azure subscription.

The monthly free limits include 100,000 vCore seconds of [serverless database](serverless-tier-overview.md) compute and a maximum size of 32 GB of data.

You have two options you can set in the **Behavior when free limit reached** setting:

- Once the monthly limits on vCore activity or storage are met, the database can be auto-paused until the beginning of the next calendar month.
  - This is the **Auto-pause the database until next month** option.
- Keep the database online, with vCore usage and storage amount over the free limits charged to your subscription's billing method, at the standard General Purpose tier serverless rates.
  - This is the **Continue using database for additional charges** option.

Regardless, the free amount renews at the start of the next calendar month.

For more information, see the [Azure SQL Database free offer FAQ](free-offer-faq.yml).

## Prerequisites

To try Azure SQL Database for free, you need:

- An Azure account.
  - If you're evaluating Azure, consider the [Azure free account, with many services available free for 12 months](https://azure.microsoft.com/free/).
  - While the [previous offer](free-sql-db-free-account-how-to-deploy.md) required an Azure Free subscription, this new offer is available regardless of your Azure subscription type.
- An Azure subscription that doesn't already have a SQL Database with the free offer applied.
  - There's a limit of one free amount database per subscription.

## Create a database

Use the Azure portal to create the new free Azure SQL Database.

To create your database, follow these steps:

1. Go to the Azure portal [provisioning page for Azure SQL Database](https://portal.azure.com/#create/Microsoft.SQLDatabase).
1. On the **Basics** tab, look for the banner that says "Want to try Azure SQL Database for free?", select the **Apply offer** button.
1. Under **Project details**, select your Subscription name.
1. For **Resource group**, select **Create new**, enter `myFreeDBResourceGroup`, and select **OK**.
1. For **Database name**, enter `myFreeDB`.
1. For **Server**, select **Create new**, and fill out the **New server** form with the following values:
   - **Server name**: Enter `myfreesqldbserver`, and add some characters for uniqueness. The name of the Azure SQL logical server must be lowercase.
   - **Authentication method**: Select **Use both SQL and Microsoft Entra authentication**.
   - **Server admin login**: Enter a username for the SQL authentication server admin.
   - - **Password**: Enter a password for the SQL authenticated server admin that meets complexity requirements, and enter it again in the **Confirm password** field.
   - **Location**: Select a location from the dropdown list.
1. Select **OK**. Leave other options as default.
1. Under **Compute + storage**, leave the existing default database as configured "Standard-series (Gen5), 2 vCores, 32-GB storage". You can adjust this setting later if needed.
1. For the **Behavior when free limit reached** setting, you have two choices to determine what happens when the free monthly offer limits are exhausted.
    :::image type="content" source="media/free-offer/behavior-when-free-offer-limit-reached.png" alt-text="Screenshot from the Azure portal showing the free Azure SQL Database offer options.":::
    - If you choose **Auto-pause the database until next month** option, you'll not be charged for that month once the free limits are reached, however the database will become inaccessible for the remainder of the calendar month. Later, you can enable the **Continue using database for additional charges** setting in the **Compute + Storage** page of the SQL database.
    - To maintain access to the database when limits are reached, which results in charges for any amount above the free offer vCore and storage size limits, select the **Continue using database for additional charges** option. You only pay for any usage over the free offer limits.
    - You continue to get the free amount renewed at the beginning of each month.
    > [!IMPORTANT]  
    > Once you have chosen **Continue using database for additional charges**, it's not possible to go back to the free amount with auto-pause.
1. Select **Next : Networking**. On the **Networking** tab, for **Firewall rules**, set **Allow Azure services and resources to access this server** set to **Yes**. Set **Add current client IP address** to **Yes**. Leave other options as default.
1. Select **Next : Security**. Leave these options as defaults.
1. Select **Next: Additional settings**. On the **Additional settings** tab, in the **Data source** section, for **Use existing data**, you have options to use an existing database:
    - Choose **Sample** to use the sample `AdventureWorksLT` database.
    - If you intend to populate with your own data, leave this set to **None**.
1. Select **Review + create**. If you're starting with the free database offer, you should see a card with no charges on it.
1. Review and select **Create**.

## Query the database

Once your database is created, you can use the **Query editor (preview)** in the Azure portal to connect to the database and query data. For more information, see [Query the database](/azure/azure-sql/database/single-database-create-quickstart?tabs=azure-portal#query-the-database).

## Monitor and track service usage

You won't be charged for the Azure SQL Database unless you exceed the free database amount and have selected to continue using for additional charges. To remain within the limit, use the Azure portal to track and monitor your free services usage.

1. On the database **Overview** tab, you see a **Free monthly vCore amount** entry:

    :::image type="content" source="media/free-offer/free-monthly-vcore-amount-remaining.png" alt-text="Screenshot from the Azure portal of free monthly vCore seconds amount remaining.":::

1. Select the **seconds remaining** amount. The **Metrics** chart launches where you can look at **Free amount remaining** or **Free amount consumed** metrics.

### Tips on managing vCore seconds

1. Disconnect querying tools such as [Azure Data Studio](/azure-data-studio/download-azure-data-studio) and [SQL Server Management Studio](/sql/ssms/download-sql-server-management-studio-ssms), including the object explorer, when you're done using them. Leaving connections open can continue to consume credits by preventing auto-pause.
1. On the **Metrics** tab, [create an alert rule](/azure/azure-monitor/alerts/tutorial-metric-alert) at no cost. Use the **Free amount remaining** metric to send an alert when the amount is less than 10,000 vCore seconds (10% of the monthly limit), so you know when you're running out for the month.

## Offer limitations

Compared to a normal General Purpose database, this free offer has the following limitations.

- When the **Auto-pause the database until next month** option is enabled:
  - Maximum of 4 vCores and the maximum database size of 32 GB.
  - Long-term backup retention isn't available, and point-in-time restore (PITR) retention is limited to seven days.
  - Backup storage is local redundant storage only.
  - Backup storage is free.
- When the **Continue using database for additional charges** option is enabled:
  - Backup storage up to 32 GB is free.
  - You can't revert to the **Auto-pause the database until next month** option.
- The ability to restore or convert an existing database to the free offer database, or data sync with other databases, isn't available.
- Elastic Jobs and DNS Alias aren't available for this free offer.
- The free offer Azure SQL Database can't be a part of an [elastic pool](elastic-pool-overview.md) or [failover group](failover-group-sql-db.md).
- Currently, you can only use the Azure portal to create the free offer database.
- Currently, the Microsoft Azure for Students Starter offer is incompatible with this Azure SQL Database free offer. Instead, consider the [Azure for College Students offer](https://azure.microsoft.com/pricing/offers/ms-azr-0170p/) or the [Azure Free offer](https://azure.microsoft.com/pricing/offers/ms-azr-0044p/). If desired, the **Continue using database for additional charges** option can deduct from the starting credits.
- For more information, review the [Azure SQL Database free offer FAQ](free-offer-faq.yml).

## Clean up resources

When you're finished using these resources, or if you want to start over again with a new free database (limit one per subscription), you can delete the resource group you created, which also deletes the Azure SQL Database logical server and single database within it.

To delete `myFreeDBResourceGroup` and all its resources using the Azure portal:

1. In the Azure portal, search for and select **Resource groups**, and then select `myFreeDBResourceGroup` from the list.
1. On the **Resource group** page, select **Delete resource group**.
1. Under **Type the resource group name**, enter `myFreeDBResourceGroup`, and then select **Delete**.
1. After you delete your free offer database, it takes up to one hour for the free offer banner to reappear.

## Related content

- [Azure SQL Database free offer FAQ](free-offer-faq.yml)
- [Connect and query](connect-query-content-reference-guide.md) your database using different tools and languages
- [Connect and query using SQL Server Management Studio](connect-query-ssms.md)
- [Connect and query using Azure Data Studio](/azure-data-studio/quickstart-sql-database)
