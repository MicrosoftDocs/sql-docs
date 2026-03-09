---
title: Try Azure SQL Database for Free
description: Guidance on how to deploy the Azure SQL Database offer for up to 10 free databases.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: ivujic, reneamoso, amapatil, mathoma
ms.date: 01/02/2026
ms.service: azure-sql-database
ms.subservice: service-overview
ms.topic: how-to
monikerRange: "=azuresql || =azuresql-db"
---
# Deploy Azure SQL Database for free

> [!div class="op_single_selector"]
> * [Azure SQL Database](free-offer.md?view=azuresql-db&preserve-view=true)
> * [Azure SQL Managed Instance](../managed-instance/free-offer.md?view=azuresql-mi&preserve-view=true)

Try Azure SQL Database at no cost with our free tier offer. For each database, you get 100,000 vCore seconds, 32 GB of data, and 32 GB of backup storage free per month for the lifetime of your subscription. Each Azure subscription allows you to create up to 10 General Purpose databases. 

To get started and quickly create a new free Azure SQL Database, select the **Try for free** link on the [Azure SQL hub at aka.ms/azuresqlhub](https://aka.ms/azuresqlhub).

   :::image type="content" source="media/free-offer/show-options-create-sql-database.png" alt-text="Screenshot from the Azure portal showing the Azure SQL hub and the Try for free link in the Azure SQL Database pane." lightbox="media/free-offer/show-options-create-sql-database.png":::

## Prerequisites

To create a free Azure SQL Database, you need:

- An Azure account.
  - If you're evaluating Azure, consider the [Azure free account, with many services available free for 12 months](https://azure.microsoft.com/pricing/purchase-options/azure-account?cid=msft_learn).
  - While the [previous offer](free-sql-db-free-account-how-to-deploy.md) required an Azure Free subscription, this new offer is available regardless of your Azure subscription type.
- An Azure subscription. There's a limit of 10 free offer databases per subscription.

## Create a database

Use the Azure portal to create the new free Azure SQL Database. To create a free offer database, follow these steps:

1. Select the **Try for free** link on the [Azure SQL hub at aka.ms/azuresqlhub](https://aka.ms/azuresqlhub).
   - You can also find the **Apply offer** banner on the [provisioning page for Azure SQL Database](https://portal.azure.com/#create/Microsoft.SQLDatabase), and proceed with the steps to [Create a single database in the serverless compute tier](single-database-create-quickstart.md?view=azuresql-db&preserve-view=true&tabs=azure-portal#create-a-single-database).
    
1. On the Create SQL Database page, you should see the Free offer banner.

   :::image type="content" source="media/free-offer/create-sql-database-free-offer-advanced-configuration.png" alt-text="Screenshot from the Azure portal of the Free offer applied banner.":::

1. On the **Basics** tab:
    1. Under **Project details**:
        - Select your **Subscription**. Your current subscription is already selected.
        - An existing **Resource Group** is already selected. You can change this or create a new resource group.
    1. Under **Database details**: 
        - Use the automatically-created unique **Database name**, or provide a new name.
        - An existing logical **Server** is already selected. You can change this or create a new logical server.
    
   That's all that's needed! The new database will be created with defaults, including the option to pause the database when the free limits are reached. This and other options can be changed in the future.

   You know the offer has been applied when the **Cost summary** card on the right side of the page shows **Estimated Cost/Month** zero cost.

   :::image type="content" source="media/free-offer/cost-summary-card.png" alt-text="Screenshot from the Azure portal of the Free Offer Cost summary card.":::

1. Select **Review + create**.
1. Review and select **Create**.

## Query the database

Once your database is created, you can use the **Query editor (preview)** in the Azure portal to connect to the database and query data. For more information, see [Query the database](/azure/azure-sql/database/single-database-create-quickstart?tabs=azure-portal#query-the-database).

You can [connect to and query your database](connect-query-content-reference-guide.md) using different tools and languages:

- [Connect and query using SQL Server Management Studio](connect-query-ssms.md)
- The [mssql extension](https://aka.ms/mssql-marketplace) for [Visual Studio Code](https://code.visualstudio.com/docs)
- [sqlcmd](/sql/tools/sqlcmd/sqlcmd-utility)
- [Azure portal query editor for Azure SQL Database](query-editor.md)
- Review many more [connect and query quickstarts and libraries](connect-query-content-reference-guide.md).

## Monitor and track service usage

You won't be charged for the Azure SQL Database, unless you exceed the free database amount and have selected to continue using for additional charges. To remain within the limit, use the Azure portal to track and monitor your free services usage.

Enjoy Azure SQL Database free of charge with our offer that includes up to 10 General Purpose databases per subscription. Each database comes with a monthly allowance of 100,000 vCore seconds of compute, 32 GB of data storage, and 32 GB of backup storage. 

You will not incur any charges unless you exceed these allowances and you opt to pay for usage beyond the free limits. To monitor vCore consumption, the Azure portal provides metrics to track: 

1. On the database **Overview** tab, you see a **Free monthly vCore amount** entry:

    :::image type="content" source="media/free-offer/free-monthly-vcore-amount-remaining.png" alt-text="Screenshot from the Azure portal of free monthly vCore seconds amount remaining.":::

1. Select the **seconds remaining** amount. The **Metrics** chart launches where you can look at **Free amount remaining** or **Free amount consumed** metrics.

### Tips on managing vCore seconds

- Disconnect querying tools such as [SQL Server Management Studio](/ssms/sql-server-management-studio-ssms), including the object explorer, when you're done using them. Leaving connections open can continue to consume credits by preventing auto-pause.
- On the **Metrics** tab, [create an alert rule](/azure/azure-monitor/alerts/tutorial-metric-alert) at no cost. Use the **Free amount remaining** metric to send an alert when the amount is less than 10,000 vCore seconds (10% of the monthly limit), so you know when you're running out for the month.

## Monthly free limits

An Azure SQL Database you get with the free offer is the same fully managed platform as a service (PaaS) database with database management functions such as upgrading, patching, backups, and monitoring handled without user involvement. 

- The monthly free limits include 100,000 vCore seconds of [serverless database](serverless-tier-overview.md) compute and a maximum size of 32 GB of data per database.
- The offer is available for up to 10 databases per Azure subscription.

You have two options you can set in the **Behavior when free limit reached** setting:

- Once the monthly limits on vCore activity or storage are met, each database can be auto-paused until the beginning of the next calendar month.
  - This is the **Auto-pause the database until next month** option.
- Keep the database online, with vCore usage and storage amount over the free limits charged to your subscription's billing method, at the standard General Purpose tier serverless rates.
  - This is the **Continue using database for additional charges** option.

Regardless, the free amount renews for each free offer database at the start of the next calendar month.

For more information, see the [Azure SQL Database free offer FAQ](free-offer-faq.yml).

### Offer limitations

You can use the Azure portal, PowerShell, or Azure CLI to create a free offer database. Compared to a normal General Purpose database, a free offer database has the following limitations.

- When the **Auto-pause the database until next month** option is enabled:
  - There is a maximum of 4 vCores and maximum database size of 32 GB.
  - Long-term backup retention isn't available, and point-in-time restore (PITR) retention is limited to seven days.
  - Backup storage is local redundant storage only.
  - Backup storage is free.
- When the **Continue using database for additional charges** option is enabled:
  - Backup storage up to 32 GB is free.
  - You can't revert to the **Auto-pause the database until next month** option.
- The ability to restore or convert an existing database to the free offer database, or data sync with other databases, isn't available.
- Elastic Jobs and DNS Alias aren't available for this free offer.
- The free offer Azure SQL Database can't be a part of an [elastic pool](elastic-pool-overview.md) or [failover group](failover-group-sql-db.md).
- Currently, the Microsoft Azure for Students Starter offer is incompatible with this Azure SQL Database free offer. Instead, consider the [Azure for College Students offer](https://azure.microsoft.com/pricing/offers/ms-azr-0170p/?cid=msft_learn) or the [Azure Free offer](https://azure.microsoft.com/pricing/purchase-options/azure-account?cid=msft_learn). If desired, the **Continue using database for additional charges** option can deduct from the starting credits.
- When setting an **Advanced configuration**, once a region is selected for a free database under a subscription, the same region applies to all free databases in that subscription, and cannot be changed.
- When setting an **Advanced configuration**, if the free offer banner doesn't appear in the Azure portal as expected, choose the desired logical **Server** in the dropdown list again.

## Related content

- [Azure SQL Database free offer FAQ](free-offer-faq.yml)
