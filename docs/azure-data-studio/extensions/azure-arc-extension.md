---
title: Azure Arc extension
description: Learn how to install and use the Azure Arc extension to try out Azure Arc data services.
author: erinstellato-ms
ms.author: erinstellato
ms.reviewer: maghan
ms.date: 08/02/2021
ms.service: azure-data-studio
ms.topic: conceptual
---

# Azure Arc extension for Azure Data Studio

The [Azure Arc extension](/azure/azure-arc/data/) is an extension for creating and managing Azure Arc data services resources.

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

## Prerequisite

[Download and Install Azure Data Studio](../download-azure-data-studio.md).

## Install the extensions

1. [Install Azure CLI](/cli/azure/install-azure-cli)
2. [Install Azure `arcdata` extension](/azure/azure-arc/data/install-arcdata-extension)
3. Install Azure CLI extension for Azure Data Studio from the Azure Data Studio gallery
4. Install the Azure Arc extension for Azure Data Studio from the gallery
5. Restart Azure Data Studio

## Sign in with Azure account

1. Select Accounts on bottom left
1. Select Add Account
1. This action will launch a browser. Sign in to your Azure Account.

## Create a resource

This extension supports deployment of Azure Arc data controllers, PostgreSQL for Azure Arc, and SQL Managed Instance for Azure Arc. Deployments can be done through the built-in Deployment wizard.

1. Select Connections viewlet on left Activity Bar
1. Select the three dots and select **New Deployment**
1. Follow prompts to create a new Azure Arc resource.

## Manage a resource

After deploying an Azure Arc data controller from azdata, Azure portal, or Azure Data Studio, you can connect to it from Azure Data Studio.

1. Open Connections viewlet on the left activity bar.
1. Expand **Azure Arc controllers**
1. Select **Connect controller**
1. Fill out parameters and connect.

Once connected, you can view the resources deployed on the data controller. You can then right-click and select **Manage** to access the resource dashboard.  

These dashboards will provide additional information about the resource, including option to open in Azure portal.

## Next steps

To learn more about Azure Arc data services, [check our documentation.](/azure/azure-arc/data/)
