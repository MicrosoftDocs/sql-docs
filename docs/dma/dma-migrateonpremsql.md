---
title: "Upgrade SQL Server using the Data Migration Assistant"
description: Learn how to use Data Migration Assistant to upgrade an on-premises SQL Server to a later version of SQL Server or to SQL Server on Azure VMs
author: ajithkr-ms
ms.author: ajithkr
ms.reviewer: randolphwest
ms.date: 06/28/2024
ms.service: sql
ms.subservice: dma
ms.topic: conceptual
ms.collection:
  - sql-migration-content
helpviewer_keywords:
  - "Data Migration Assistant, on-premises SQL Server"
---

# Upgrade SQL Server using the Data Migration Assistant

[!INCLUDE [deprecation-notice](includes/deprecation-notice.md)]

The Data Migration Assistant provides seamless assessments of SQL Server on-premises and upgrades to later versions of SQL Server or migrations to SQL Server on Azure VMs or Azure SQL Database.

This article provides step-by-step instructions for upgrading SQL Server on-premises to later versions of SQL Server or to SQL Server on Azure VMs by using the Data Migration Assistant.

## Create a new migration project

1. On the left pane, select **New** (+), and then the **Migration** project type.

1. Set the source and target server type to **SQL Server** if you're upgrading an on-premises SQL Server to a later version of on-premises SQL Server.

1. Select **Create**.

   :::image type="content" source="media/NewCreate.png" alt-text="Screenshot of Create migration project." lightbox="media/NewCreate.png":::

## Specify the source and target

1. For the source, enter the SQL Server instance name in the **Server name** field in the **Source server details** section.

1. Select the **Authentication type** supported by the source SQL Server instance.

1. For the target, enter the SQL Server instance name in the **Server name** field in the **Target server details** section.

1. Select the **Authentication type** supported by the target SQL Server instance.

1. It's recommended that you encrypt the connection by selecting **Encrypt connection** in the **Connection properties** section.

1. Select **Next**.

   :::image type="content" source="media/SourceTarget.png" alt-text="Screenshot of Specify source and target page." lightbox="media/SourceTarget.png":::

## Add databases

1. Choose the specific databases that you want to migrate by only selecting those databases, in the left pane of the **Add databases** page.

   By default all the user databases on the source SQL Server instance are selected for migration

1. Use the migration settings on the right side of the page to set the migration options that are applied to the databases, by doing the following.

   > [!NOTE]  
   > You can apply the migration settings to all the databases that you're migrating, by selecting the server in the left pane. You can also configure an individual database with specific settings by selecting the database in the left pane.

    a. Specify the **Shared location accessible by source and target SQL servers for backup operation**. Make sure that the service account running the source SQL Server instance has write privileges on the shared location and the target service account has read privileges on the shared location.

    b. Specify the location to restore the data and transactional log files on the target server.

    :::image type="content" source="media/AddDatabases.png" alt-text="Screenshot of Add databases page." lightbox="media/AddDatabases.png":::

1. Enter a shared location that the source and target SQL Server instances have access to, in the **Share location options** box.

1. If you can't provide a shared location that both the source and target SQL Server instances have access to, select **Copy the database backups to a different location that the target server can read and restore from**. Then, enter a value for the **Location for backups for restore option** box.

   Make sure that the user account running Data Migration Assistant has read privileges to the backup location and write privileges to the location from which the target server restores.

   :::image type="content" source="media/CopyDatabaseDifferentLocation.png" alt-text="Screenshot of Option to copy database backups to different location." lightbox="media/CopyDatabaseDifferentLocation.png":::

1. Select **Next**.

The Data Migration Assistant performs validations on the backup folders, data, and log file locations. If any validation fails, fix the options, and then select **Next**.

## Select logins

1. Select specific logins for migration.

   > [!IMPORTANT]  
   > Make sure to select the logins that are mapped to one or more users in the databases selected for migration.

   By default, all the SQL Server and Windows logins that qualify for migration are selected for migration.

1. Select **Start Migration**.

   :::image type="content" source="media/SelectLogins.png" alt-text="Screenshot of Select logins and start migration." lightbox="media/SelectLogins.png":::

## View results

You can monitor the migration progress on the **View results** page.

:::image type="content" source="media/ViewResults.png" alt-text="Screenshot of results page." lightbox="media/ViewResults.png":::

## Export migration results

1. Select **Export report** at the bottom of the **View results** page to save the migration results to a CSV file.

1. Review the saved file for details about the login migration, and then verify the changes.

## Related content

- [Data Migration Assistant (DMA)](dma-overview.md)
- [Data Migration Assistant: Configuration settings](dma-configurationsettings.md)
- [Data Migration Assistant: Best Practices](dma-bestpractices.md)
