---
title: Deploy SQL Server in a container via Notebook
description: This tutorial shows how you can deploy SQL Server in a container.
author: dzsquared
ms.author: drskwier
ms.reviewer: markingmyname
ms.date: 7/22/2022
ms.service: azure-data-studio
ms.topic: tutorial
ms.custom: intro-deployment
---

# Deploy SQL Server in a container using Azure Data Studio

You can deploy SQL Server in a container using Azure Data Studio through the deployment wizard and notebooks.

## Pre-requisites

 - [Azure Data Studio](download-azure-data-studio.md) is installed
 - Docker Engine 1.8+ on any supported Linux distribution or Docker for Mac/Windows. For more information, see [Install Docker](https://docs.docker.com/engine/installation/)

## Use the deployment wizard

Follow these steps to use the deployment wizard, which will guide you through the required settings in a simple UI experience.

First, find and select Azure SQL Database in the deployment wizard.

 1. In Azure Data Studio, select the Connections viewlet on the left-side navigation.
 2. Select the **...** button at the top of the Connections panel and choose **New Deployment...**
 3. In the deployment wizard, select the **SQL Server container image** tile and select **Select** to move to the next screen.
 4. In the Version dropdown, select which image of SQL Server you would like to deploy. The wizard will check whether you have a running instance of Docker at this time. Select **Select** to move to the next screen.
 5. Enter the container name, a password for the *sa* account of suitable complexity (at least eight characters long and contain characters from three of the following four sets: uppercase letters, lowercase letters, base-10 digits, and symbols), and the port in the host environment which the container will map to.
 6. Review the parameters you have entered and then select **Open Notebook**.

Once the Notebook opens, you can review the content and the code and make changes if you like. However, be aware that any changes you make to the Notebook could potentially cause validation errors.

The last step is to select **Run all** to run all cells in the Notebook. Once this completes you should have a fully created and running SQL Database that you can connect to and query from ADS.

## Next steps

After you've created your database, server, or pool, you can [connect and query](quickstart-sql-database.md) your database in Azure Data Studio.

For more information on deploying SQL Server in containers, see [Quickstart: Run SQL Server Linux container images with Docker](../linux/quickstart-install-connect-docker.md).