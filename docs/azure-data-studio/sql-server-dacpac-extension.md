---
title: SQL Server dacpac extension
description: Learn how to install and launch the Data-tier Application Wizard, which makes it easy to deploy and extract dacpac files, and import and export bacpac files.
ms.prod: azure-data-studio
ms.technology: azure-data-studio
ms.topic: conceptual
author: "yualan"
ms.author: "alayu"
ms.reviewer: "maghan, sstein"
ms.custom: "seodec18"
ms.date: 11/04/2019
---

# SQL Server dacpac extension

**The Data-tier Application Wizard** provides an easy-to-use wizard experience to deploy and extract dacpac files and import and export bacpac files.

## Features

* Deploy a dacpac to a SQL Server instance
* Extract a SQL Server instance to a dacpac
* Create a database from a bacpac
* Export schema and data to a bacpac

![dacpac extension demo gif](media/sql-server-dacpac-extension/dacpac-extension-demo.gif)

## Why would I use the Data-tier Application Wizard?

The wizard makes it easier to manage dacpac and bacpac files, which simplifies the development and deployment of data-tier elements that support your application. To learn more about using Data-tier applications, [check out our documentation.](../relational-databases/data-tier-applications/data-tier-applications.md)

## Install the extension

1. Select the Extensions Icon to view the available extensions.

    ![extension manager icon](extensions/media/add-extensions/extension-manager-icon.png)

2. Search for the **SQL Server dacpac** extension and select it to view its details. Click **Install** to add the extension.

3. Once installed, **Reload** to enable the extension in Azure Data Studio (only required when installing an extension for the first time).

## Launch the Data-tier Application Wizard

To launch the wizard, right-click the Databases folder or right-click a specific database in the Object Explorer. Then, click **Data-tier Application Wizard**.

![dacpac extension launch menu](media/sql-server-dacpac-extension/dacpac-extension-launch.png)

## Next steps

To learn more about dacpacs, [check out our documentation.](../relational-databases/data-tier-applications/data-tier-applications.md)
Please report issues and feature requests [here.](https://github.com/microsoft/azuredatastudio/issues)