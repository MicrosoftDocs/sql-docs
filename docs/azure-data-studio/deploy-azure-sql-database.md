---
title: Deploy Azure SQL Database via Notebook
description: This tutorial shows how you can create an Azure SQL Database.
author: ninarn
ms.author: ninarn
ms.reviewer: alayu, markingmyname
ms.date: 10/16/2020
ms.service: azure-data-studio
ms.topic: tutorial
ms.custom: intro-deployment
---

# Create an Azure SQL Database using Azure Data Studio

You can create an Azure SQL Database using Azure Data Studio through the deployment wizard and notebooks.

## Pre-requisites

 - [Azure Data Studio](download-azure-data-studio.md) is installed
 - An active Azure account and subscription. If you do not have one, [create a free account](https://azure.microsoft.com/free/).

## Use the deployment wizard

Follow these steps to use the deployment wizard, which will guide you through the required settings in a simple UI experience.

First, find and select Azure SQL Database in the deployment wizard.

 1. In Azure Data Studio, select the Connections viewlet on the left-side navigation.
 2. Select the **...** button at the top of the Connections panel and choose **New Deployment...**
 3. In the deployment wizard, select the **Azure SQL Database** tile and check the terms acceptance checkbox
 4. In the Resource type dropdown, select what you would like to create - either a database, database server, or elastic pool. If you don't have any SQL databases in Azure, we recommend starting by creating a database.
 5. Select **Create in Azure portal** if you chose to create a server or pool or select **Select** if you chose to create a database.

If you chose to create a database, follow the steps below.

 1. Sign in to your Azure account if you have not already. You can refresh your connection if you have issues on this page of the wizard.
 2. Select your desired subscription and server. Then select **Next**.
 3. Enter a database name.
 4. Enter a firewall rule name and the IP range of your local client machine running Azure Data Studio. This is so you can connect to the server and database from ADS right after it has been created.
 5. Select **Next**.
 6. Review the parameters you have entered and then select **Script to Notebook**.

Once the Notebook opens, you can review the content and the code and make changes if you like. In particular you can change the compute and storage settings from the default if you have specific performance or cost preferences. However, be aware that any changes you make to the Notebook could potentially cause validation errors.

The last step is to select **Run all** to run all cells in the Notebook. Once this completes you should have a fully created and running SQL Database that you can connect to and query from ADS.

## Next steps

After you've created your database, server, or pool, you can [connect and query](quickstart-sql-database.md) your database in Azure Data Studio.
