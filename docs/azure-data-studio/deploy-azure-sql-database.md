---
title: Deploy Azure SQL Database via Notebook
description: This tutorial shows how you can create an Azure SQL Database.
author: ninarn
ms.author: ninarn
ms.reviewer: alayu, markingmyname
ms.topic: tutorial
ms.prod: azure-data-studio
ms.technology: azure-data-studio
ms.custom: ""
ms.date: 09/22/2020
---

# Create an Azure SQL Database (Azure Data Studio)

You can create an Azure SQL Database using Azure Data Studio through the deployment wizard and notebooks.

## Pre-requisites

 - [Azure Data Studio](download-azure-data-studio.md) is installed
 - An active Azure account and subscription. If you do not have one, [create a free account](https://azure.microsoft.com/free/).

## Use the deployment wizard

Follow these steps to use the deployment wizard which will guide you through the required settings in a simple UI experience.

First, find and select Azure SQL Database in the deployment wizard.

 1. In Azure Data Studio, select the Connections viewlet on the left-side navigation.
 2. Select the **...** button at the top of the Connections panel and choose **New Deployment...**
 3. In the deployment wizard, select the **Azure SQL Database** tile and check the terms acceptance checkbox
 4. In the **Resource type** dropdown select what you would like to create - either a database, database server, or elastic pool. If you do not have any SQL databases in Azure, we recommend starting by creating a database.
 5. Select **Create in Azure portal**

Next, enter all preferred settings for creating your database, server, or pool. You can find some guidance on how to configure these settings in the [Azure SQL documentation](https://docs.microsoft.com/azure/azure-sql/database/single-database-create-quickstart?tabs=azure-portal).

## Next steps

After you have created your database, server, or pool, you can [connect and query](quickstart-sql-database.md) your database in Azure Data Studio.
