---
title: "Quickstart: Connect and query Azure Cosmos DB API for MongoDB (preview)"
description: Use Azure Data Studio to connect to Azure Cosmos DB for MongoDB API, and then query a collection.
ms.prod: azure-data-studio
ms.technology: azure-data-studio
ms.topic: quickstart
author: seesharprun
ms.author: sidandrews
ms.reviewer: esarroyo
ms.date: 05/10/2022
---

# Quickstart: Use Azure Data Studio to connect and query Azure Cosmos DB API for MongoDB (Preview)

This quickstart shows how to use Azure Data Studio to connect to the Azure Cosmos DB API for MongoDB.

## Prerequisites

To complete this quickstart, you need Azure Data Studio and an Azure Cosmos DB API account.

- [Install Azure Data Studio](./download-azure-data-studio.md).
- [Install the Azure Cosmos DB API for MongoDB extension for Azure Data Studio](./extensions/cosmos-mongo-extension.md).
- [Create an Azure Cosmos DB API for MongoDB account](/azure/cosmos-db/mongodb/mongodb-introduction).

## Connect to an Azure Cosmos DB API for MongoDB account

1. Start **Azure Data Studio**.

1. The first time you start Azure Data Studio the **Connection** dialog opens. If the **Connection** dialog doesn't open, select the **New Connection** icon in the **SERVERS** page:

    :::image type="content" source="media/quickstart-cosmos-mongo/new-connection-icon.png" lightbox="" alt-text="New connection icon.":::

1. In the form that pops up, select **Browse**, and then select the **+** icon to sign in to Azure.

    :::image type="content" source="" lightbox="" alt-text="":::

1. Once you're signed in to Azure, select the Azure Cosmos DB account you'd like to access.

    :::image type="content" source="" lightbox="" alt-text="":::

1. Select **Connect**.

Your server will open in the **SERVERS** sidebar after you've successfully connected.

## Create a database

The following steps will create a database named **SourceDatabase**:

1. Open the context menu for your account, and select **Create Database**.

    :::image type="content" source="" lightbox="" alt-text="":::

1. Fill in the dialog fields using the details in this table, and then select ****.

    :::image type="content" source="" lightbox="" alt-text="":::

1. After the operation completes, the new database should appear in the list of databases.

    > [!TIP]
    > If the database does not appear in the list, select **Refresh**.

## Create a sample collection

The following steps will create a collection named **Persons** pre-populated with a sample data set:

1. Select **Databases** to navigate to the list of databases in your account. Select the **SourceDatabase** option.

    :::image type="content" source="" lightbox="" alt-text="":::

1. In the header menu, select **Import Sample Data**.

    :::image type="content" source="" lightbox="" alt-text="":::

1. Wait for the import operation to complete.

    > [!TIP]
    > The import operation may take a few minutes to finish.

## Query the data

The following steps will use the **Mongo Shell** to execute a query and view the results of the query:

1. Open the context menu for your account, and select **Open Mongo Shell**.

    :::image type="content" source="" lightbox="" alt-text="":::

1. Execute the following query in the shell.

    ```bash
    db.Persons.find().pretty()
    ```

1. Observe the output of the query within the shell.

    :::image type="content" source="" lightbox="" alt-text="":::

## Next steps

In this quickstart, you've learned how to create an API for MongoDB account, create a database and a collection in Azure Data Studio, and query sample data. You can now use Azure Data Studio to review real data in your database.

- Interested in importing MongoDB data into Azure Cosmos DB? See [Migrate MongoDB to Azure Cosmos DB's API for MongoDB](/azure/cosmos-db/mongodb/tutorial-mongotools-cosmos-db.md)
- Trying to do capacity planning for a migration to Azure Cosmos DB? You can use information about your existing database cluster for capacity planning.
  - If all you know is the number of vCores and servers in your existing database cluster, read about [estimating request units using vCores or vCPUs](/azure/cosmos-db/convert-vcore-to-request-unit.md)
  - If you know typical request rates for your current database workload, read about [estimating request units using Azure Cosmos DB capacity planner](/azure/cosmos-db/sql/estimate-ru-with-capacity-planner.md)
