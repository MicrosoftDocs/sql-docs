---
title: Azure Arc extension (preview)
description: Learn how to install and use the Azure Arc extension to try out Azure Arc data services.
ms.custom: "seodec18"
ms.date: "09/22/2020"
ms.reviewer: "alayu, maghan, sstein"
ms.prod: azure-data-studio
ms.technology: azure-data-studio
ms.topic: conceptual
author: "yualan"
ms.author: "alayu"
---

# Azure Arc extension for Azure Data Studio (Preview)

The [Azure Arc extension (preview)](https://aka.ms/azurearcdata-docs) is an extension for creating and managing Azure Arc data services resources.

**Key actions include:**
- Create a resource
    - Data Controller
    - SQL Managed Instance for Azure Arc
    - PostgreSQL for Azure Arc
- Manage a resource
    - View Data Controller dashboard
    - View SQL Managed Instance for Azure Arc dashboard
    - View PostgreSQL for Azure Arc dashboard
- Run Azure Arc Jupyter Book

## Install the extension
- Install the **Azure Data CLI** extension from the gallery.
- Install the **Azure Arc** extension from the gallery.
- Restart Azure Data Studio

## Sign in with Azure account
1. Select Accounts on bottom left
1. Select Add Account
1. This action will launch a browser. Sign in to your Azure Account.

## Create a resource
This extension supports deployment of Azure Arc data controllers, Postgres for Azure Arc, and SQL Managed Instance for Azure Arc. Deployments can be done through the built-in Deployment wizard.

1. Select Connections viewlet on left Activity Bar
1. Select the three dots and select **New Deployment**
1. Follow prompts to create a new Azure Arc resource.

## Manage a resource
After deploying an Azure Arc data controller from azdata, Azure Portal, or Azure Data Studio, you can connect to it from Azure Data Studio.

1. Open Connections viewlet on the left activity bar.
1. Expand **Azure Arc controllers**
1. Select **Connect controller**
1. Fill out parameters and connect.

Once connected, you can view the resources deployed on the data controller. You can then right-click and select **Manage** to access the resource dashboard.  

These dashboards will provide additional information about the resource, including option to open in Azure Portal.

## Next steps
To learn more about Azure Arc data services, [check our documentation.](https://aka.ms/azurearcdata-docs)
