---
title: "Connect to Azure SQL (GUI) (AccessToSQL)"
description: "Connect To Azure SQL (AccessToSQL)"
author: nilabjaball
ms.author: niball
ms.reviewer: vanto
ms.date: 12/30/2025
ms.service: sql
ms.subservice: ssma
ms.topic: how-to
ms.collection:
  - sql-migration-content
helpviewer_keywords:
  - "Connect to Azure SQL dialog box"
---
# Connect to Azure SQL (GUI) (AccessToSQL)

Use the **Connect to Azure SQL** dialog box to connect to the database in Azure SQL Database that you want to migrate.

To access this dialog box, on the **File** menu, select **Connect to Azure SQL**. If you previously connected, use **Reconnect to Azure SQL**.

## Options

#### Server Name

Select or enter the server name for connecting to Azure SQL.

#### Database

Select, enter, or **Browse** the database name.

> [!IMPORTANT]  
> SSMA for Access doesn't support connection to the `master` database in Azure SQL.

#### User name

Enter the user name that SSMA uses to connect to Azure SQL Database.

#### Password

Enter the password for the user name.

#### Encrypt

SSMA recommends an encrypted connection to Azure SQL.

## Create database

To create a new database, follow these steps:

1. Select the **Browse** button in the **Connect to Azure SQL** dialog box.

1. If there are no databases, two menu items appear:

   - **(no databases found)**, which is always disabled and grayed out.

   - **Create new database**, which is always enabled. Select this option to create a new database. When you select this menu item, the **Create Database** dialog box appears with fields for the database name and size.

1. Enter the following parameters when creating the database:

   - **Database Name**: Enter the database name.

   - **Database Size**: Select the database size that you need to create in your Azure SQL account.
