---
title: "Upgrade on-premises SQL Server to SQL Server or SQL Server on Azure VMs using the Data Migration Assistant | Microsoft Docs"
description: Learn how to use Data Migration Assistant to upgrade an on-premises SQL Server to a later version of SQL Server or to SQL Server on Azure VMs
ms.custom: ""
ms.date: "10/20/2018"
ms.prod: sql
ms.prod_service: "dma"
ms.reviewer: ""
ms.technology: dma
ms.topic: conceptual
keywords: ""
helpviewer_keywords: 
  - "Data Migration Assistant, on-premises SQL Server"
ms.assetid: ""
author: pochiraju
ms.author: rajpo
manager: craigg
---

# Upgrade on-premises SQL Server to SQL Server or SQL Server on Azure VMs using the Data Migration Assistant

The Data Migration Assistant provides seamless assessments of SQL Server on-premises and upgrades to later versions of SQL Server or migrations to SQL Server on Azure VMs or Azure SQL Database.

This article provides step-by-step instructions for upgrading SQL Server on-premises to later version of SQL Server or to SQL Server on Azure VMs by using the Data Migration Assistant.   

## Create a new migration project

1. On the left pane, select **New** (+), and then the **Migration** project type.

2. Set the source and target server type to **SQL Server** if you're upgrading an on-premises SQL Server to a later version of on-premises SQL Server.

3. Select **Create**.

   ![Create migration project](../dma/media/NewCreate.png)

## Specify the source and target

1. For the source, enter the SQL Server instance name in the **Server name** field in the **Source server details** section. 

2. Select the **Authentication type** supported by the source SQL Server instance.

3. For the target, enter the SQL Server instance name in the **Server name** field in the **Target server details** section. 

4. Select the **Authentication type** supported by the target SQL Server instance.

5. It's recommended that you encrypt the connection by selecting **Encrypt connection**  in the **Connection properties** section.

6. Click **Next**.

   ![Specify source and target page](../dma/media/SourceTarget.png)

## Add databases

1. Choose the specific databases that you want to migrate by only selecting those databases, in the left pane of the **Add databases** page.

   By default all the user databases on the source SQL Server instance are selected for migration

2. Use the migration settings on the right side of the page to set the migration options that are applied to the databases, by doing the following.

   > [!NOTE]
   > You can apply the migration settings to all the databases that you're migrating, by selecting the server in the left pane. You can also configure an individual database with specific settings by selecting the database in the left pane.

    a. Specify the **Shared location accessible by source and target SQL servers for backup operation**. Make sure that the service account running the source SQL Server instance has write privileges on the shared location and the target service account has read privileges on the shared location.

    b. Specify the location to restore the data and transactional log files on the target server.

    ![Add databases page](../dma/media/AddDatabases.png)

3. Enter a shared location that the source and target SQL Server instances have access to, in the **Share location options** box.

4. If you can't provide a shared location that both the source and target SQL Servers have access to, select **Copy the database backups to a different location that the target server can read and restore from**. Then, enter a value for the **Location for backups for restore option** box. 

   Make sure that the user account running Data Migration Assistant has read privileges to the backup location and write privileges to the location from which the target server restores.

   ![Option to copy database backups to different location](../dma/media/CopyDatabaseDifferentLocation.png)

5. Select **Next**.

The Data Migration Assistant performs validations on the backup folders, data, and log file locations. If any validation fails, fix the options, and then select **Next**.

## Select logins

1. Select specific logins for migration.

   > [!IMPORTANT]
   > Make sure to select the logins that are mapped to one or more users in the databases selected for migration.   

   By default, all the SQL Server and Windows logins that qualify for migration are selected for migration.

2. Select **Start Migration**.

   ![Select logins and start migration](../dma/media/SelectLogins.png)

## View results

You can monitor the migration progress on the **View results** page.

![View results page](../dma/media/ViewResults.png)

## Export migration results

1. Click **Export report** at the bottom of the **View results** page to save the migration results to a CSV file.

2. Review the saved file for details about the login migration, and then verify the changes.

## See also

- [Data Migration Assistant (DMA)](../dma/dma-overview.md)
- [Data Migration Assistant: Configuration settings](../dma/dma-configurationsettings.md)
- [Data Migration Assistant: Best Practices](../dma/dma-bestpractices.md)
