---
title: "Quickstart: Connect and query Azure Cosmos DB API for MongoDB (preview)"
description: Use Azure Data Studio to connect to Azure Cosmos DB for MongoDB API, and then query a collection.
author: seesharprun
ms.author: sidandrews
ms.reviewer: esarroyo
ms.date: 05/10/2022
ms.service: azure-data-studio
ms.topic: quickstart
ms.custom: event-tier1-build-2022
---

# Quickstart: Use Azure Data Studio to connect and query Azure Cosmos DB API for MongoDB (Preview)

This quickstart shows how to use Azure Data Studio to connect to the Azure Cosmos DB API for MongoDB.

## Prerequisites

To complete this quickstart, you need Azure Data Studio and an Azure Cosmos DB API account.

- [Install Azure Data Studio](download-azure-data-studio.md). 
- [Install the Azure Cosmos DB API for MongoDB extension for Azure Data Studio](./extensions/azure-cosmos-db-mongodb-extension.md).
- [Create an Azure Cosmos DB API for MongoDB account](/azure/cosmos-db/mongodb/mongodb-introduction).

## Connect to an Azure Cosmos DB API for MongoDB account

1. Start **Azure Data Studio**.

1. The first time you start Azure Data Studio the **Connection** dialog opens. If the **Connection** dialog doesn't open, select the **New Connection** icon in the **SERVERS** page:

    :::image type="content" source="media/quickstart-azure-cosmos-db-mongodb/new-connection-icon.png" lightbox="media/quickstart-azure-cosmos-db-mongodb/new-connection-icon.png" alt-text="Screenshot of new connection icon in the Servers sidebar.":::

1. In the dialog that appears, select **Browse**, and then select the **+** icon to sign in to Azure.

    :::image type="content" source="media/quickstart-azure-cosmos-db-mongodb/add-azure-account.png" lightbox="media/quickstart-azure-cosmos-db-mongodb/add-azure-account.png" alt-text="Screenshot of the plus button to add new Azure account in the Connection dialog.":::

1. Once you're signed in to Azure, select the Azure Cosmos DB account you'd like to access or fill in the **Connection Details** from fields using the details in this table.

    | Setting | Value | Description |
    | --- | --- | --- |
    | **Connection type** | *Mongo account* | Set this value to *Mongo account* to use with Azure Cosmos DB API for MongoDB. |
    | **Mongo account** | *\[cosmos-account-name\]* | Name of Azure Cosmos DB account. |
    | **Authentication type** | *Azure Active Directory - Universal with MFA support* | Choose between *Azure Active Directory* or *Basic* authentication options. |
    | **Account** | *\[current-aad-user-name\]* | Only visible if using *Azure Active Directory* authentication. Select the Azure AD account that you wish to use for authentication. |
    | **Username** | | Only visible if using *Basic* authentication. Enter the username for authentication. |
    | **Connection String** | | Only visible if using *Basic* authentication. Enter the connection string for authentication. |
    | **Server group** | *\<Default\>* | |
    | **Name (optional)** | | Enter any unique name to use in the **SERVERS** sidebar. |

    :::image type="content" source="media/quickstart-azure-cosmos-db-mongodb/connection-credentials-form.png" lightbox="media/quickstart-azure-cosmos-db-mongodb/connection-credentials-form.png" alt-text="Screenshot of dialog with credentials required to create a connection.":::

1. Select **Connect**.

    > [!IMPORTANT]
    > If you selected *Azure Active Directory* authentication, you may be prompted to select a MongoDB connection string value to use for your connection.

Your server will open in the **SERVERS** sidebar after you've successfully connected.

## Create a database and collection

The following steps will create a database named **SourceDatabase** and a collection named **People**:

1. Open the context menu for your account, and select **Create Database**.

    :::image type="content" source="media/quickstart-azure-cosmos-db-mongodb/create-database-menu-option.png" lightbox="media/quickstart-azure-cosmos-db-mongodb/create-database-menu-option.png" alt-text="Screenshot of the create database option in the Mongo DB context menu.":::

1. In the **Database** and **Collection** popup dialogs, use the details in this table.

    | Prompt | Value |
    | --- | --- |
    | **Database name** | *SourceDatabase* |
    | **Collection name** | *People* |

1. After the operation completes, the new database should appear in the list of databases.

    > [!TIP]
    > If the database does not appear in the list, select **Refresh**.

1. Expand the **SourceDatabase** and **People** nodes in the **SERVERS** sidebar.

    :::image type="content" source="media/quickstart-azure-cosmos-db-mongodb/servers-sidebar-tree.png" lightbox="media/quickstart-azure-cosmos-db-mongodb/servers-sidebar-tree.png" alt-text="Screenshot of database and collection hierarchy under the Mongo account note in the SERVERS sidebar.":::

## Create a sample collection

The following steps will populate the **People** collection with a sample data set:

1. Select **Databases** to navigate to the list of databases in your account.

    :::image type="content" source="media/quickstart-azure-cosmos-db-mongodb/navigate-databases.png" lightbox="media/quickstart-azure-cosmos-db-mongodb/navigate-databases.png" alt-text="Screenshot of option to navigate to databases view.":::

1. Select the **SourceDatabase** item in the list of databases.

    :::image type="content" source="media/quickstart-azure-cosmos-db-mongodb/database-list-item.png" lightbox="media/quickstart-azure-cosmos-db-mongodb/database-list-item.png" alt-text="Screenshot of database list item named Source Database within databases list.":::

1. In the header menu, select **Import Sample Data**. In the confirmation dialog, select **Yes**.

    :::image type="content" source="media/quickstart-azure-cosmos-db-mongodb/navigate-import.png" lightbox="media/quickstart-azure-cosmos-db-mongodb/navigate-import.png" alt-text="Screenshot of dialog option to perform an import.":::

1. Wait for the import operation to complete.

    > [!TIP]
    > The import operation may take a few minutes to finish.

## Query the data

The following steps will use the **Mongo Shell** to execute a query and view the results of the query:

1. In the menu for your database, select **Open Mongo Shell**.

    :::image type="content" source="media/quickstart-azure-cosmos-db-mongodb/navigate-mongodb-shell.png" lightbox="media/quickstart-azure-cosmos-db-mongodb/navigate-mongodb-shell.png" alt-text="Screenshot of menu option to open Mongo Shell.":::

1. Execute the following query in the shell.

    ```bash
    db.Persons.find().pretty()
    ```

    > [!TIP]
    > The ``pretty()`` method in Mongo displays the results of a query in a format that's easy to read. To learn more, see the [official MongoDB documentation for cursor.pretty](https://www.mongodb.com/docs/manual/reference/method/cursor.pretty/).

1. Observe the output of the query within the shell. The output should contain

    :::image type="content" source="media/quickstart-azure-cosmos-db-mongodb/mongodb-query-results.png" lightbox="media/quickstart-azure-cosmos-db-mongodb/mongodb-query-results.png" alt-text="Screenshot of Mongo Shell query results.":::

## Next steps

In this quickstart, you've learned how to create an API for MongoDB account, create a database and a collection in Azure Data Studio, and query sample data. You can now use Azure Data Studio to review real data in your database.

- Learn about the [extension available for Azure Cosmos DB API for MongoDB](./extensions/azure-cosmos-db-mongodb-extension.md).
- Interested in importing MongoDB data into Azure Cosmos DB? See [Migrate MongoDB to Azure Cosmos DB's API for MongoDB](/azure/cosmos-db/mongodb/tutorial-mongotools-cosmos-db)
- Trying to do capacity planning for a migration to Azure Cosmos DB? You can use information about your existing database cluster for capacity planning.
  - If all you know is the number of vCores and servers in your existing database cluster, read about [estimating request units using vCores or vCPUs](/azure/cosmos-db/convert-vcore-to-request-unit)
  - If you know typical request rates for your current database workload, read about [estimating request units using Azure Cosmos DB capacity planner](/azure/cosmos-db/sql/estimate-ru-with-capacity-planner)
