---
title: Explore Azure SQL resources with the Azure View
description: Learn how to explore and manage Azure SQL Server, Azure SQL Database, and Azure SQL Managed Instance through the Azure View.
author: erinstellato-ms
ms.author: erinstellato
ms.reviewer: maghan, randolphwest
ms.date: 03/22/2023
ms.service: azure-data-studio
ms.topic: how-to
ms.custom: updatefrequency5
---
# Explore and manage Azure SQL resources from the Azure View

In this document, learn how you can explore and manage Azure SQL Server, Azure SQL database, Dedicated SQL Pools and additional resources through the Azure View in [!INCLUDE [Azure Data Studio](../includes/name-sos-short.md)].

## Connect to Azure

The Azure View appears within the Connection View. Select the activity bar to open the Azure View. If you don't see the Azure activity bar, right-click the left menu bar, and select **Azure**.

### Add an Azure account

To view the SQL resources associated with an Azure account, you must first add the account to [!INCLUDE [Azure Data Studio](../includes/name-sos-short.md)].

1. Open **Linked accounts** dialog through the Accounts icon on the left bottom, or through **Azure: Sign In** icon in the Azure View.

   :::image type="content" source="media/azure-view/azure-sign-in.png" alt-text="Screenshot of Sign into Azure screen.":::

1. In the **Linked accounts** dialog, select **Add an account**.

   :::image type="content" source="media/azure-view/add-an-azure-account.png" alt-text="Screenshot of Add an Azure account screen.":::

1. When your browser opens for authentication, enter your credentials to authenticate.

   :::image type="content" source="media/azure-view/authenticate-azure-account.png" alt-text="Screenshot of Azure authentication window.":::

1. In [!INCLUDE [Azure Data Studio](../includes/name-sos-short.md)] you should now find the signed in Azure account in **Linked Accounts** dialog.

   :::image type="content" source="media/azure-view/linked-accounts.png" alt-text="Screenshot of linked Azure accounts.":::

### Add more Azure accounts

Multiple Azure accounts are supported in [!INCLUDE [Azure Data Studio](../includes/name-sos-short.md)]. To add more Azure accounts, select the button on the right top of **Linked Accounts** dialog and follow the same steps in the Add an Azure account section to add more Azure accounts.

:::image type="content" source="media/azure-view/add-another-azure-account.png" alt-text="Screenshot of Add another Azure account screen.":::

### Remove an Azure account

To remove an existing signed in Azure account:

1. Open **Linked Accounts** dialog through the account management icon on the left bottom.
1. Select the **X** button at the right of the Azure account to remove it.

:::image type="content" source="media/azure-view/remove-azure-account-2.png" alt-text="Screenshot of Remove Azure account screen.":::

## Filter subscription

Once logged in to an Azure account, all subscriptions associated with that Azure account display in the Azure View. You can filter subscriptions for each Azure account.

1. Select the **Select Subscription** button at right of the Azure account.

   :::image type="content" source="media/azure-view/filter-subscriptions.png" alt-text="Screenshot of Filter subscriptions screen.":::

1. Select the check boxes for the account subscriptions you want to browse and then select **OK**.

   :::image type="content" source="media/azure-view/select-subscription.png" alt-text="Screenshot of Select subscriptions screen.":::

## Explore Azure SQL resources

To navigate an Azure SQL resource from the Azure View, expand the Azure accounts and resource type group.

Azure View supports Azure SQL Server, Azure SQL Database, Azure SQL Managed Instance, Log Analytics, Cosmos DB for Mongo, Azure Database for MySQL, Azure Database for PostgreSQL, Azure Data Explorer Cluster, Dedicated SQL Pools, and Azure Synapse Analytics.

## Connect to Azure SQL resources

Azure View provides quick access to help you connect to SQL Server instances and databases for query and management.

1. Explore the SQL resource you would like to connect with from the tree view.
1. Right-click the resource and select **Connect**, you can also find the connect button at the right of the resource.

   :::image type="content" source="media/azure-view/connect-azure-resource.png" alt-text="Screenshot of Connect to an Azure resource screen.":::

1. In the opened **Connection** dialog, select the appropriate **Authentication type**, enter your authentication information, select whether you want to add the connection to a **Server group** (the default is **\<Do not save\>**) and select **Connect**.

   :::image type="content" source="media/azure-view/azure-connection-configuration.png" alt-text="Screenshot of Configure Azure connection screen.":::

1. The **Manage Server** window will automatically open for the connected SQL server/database after connection succeeds.  If the connection was added to the **Default** or existing **Server group**, it appears in the **Connection View**.

## Next steps

- [Use [!INCLUDE[Azure Data Studio](../includes/name-sos-short.md)] to connect and query Azure SQL database](quickstart-sql-database.md)
- [Use [!INCLUDE[Azure Data Studio](../includes/name-sos-short.md)] to connect and query data in Azure Synapse Analytics](quickstart-sql-dw.md)
