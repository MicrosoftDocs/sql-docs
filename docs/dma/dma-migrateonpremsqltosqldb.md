---
title: "Migrate SQL Server to Azure SQL Database using the Data Migration Assistant"
description: Learn how to use Data Migration Assistant to migrate an on-premises SQL Server to Azure SQL Database
author: ajithkr-ms
ms.author: ajithkr
ms.reviewer: randolphwest
ms.date: 06/28/2024
ms.service: sql
ms.subservice: dma
ms.topic: conceptual
ms.collection:
  - sql-migration-content
ms.custom:
  - intro-migration
helpviewer_keywords:
  - "Data Migration Assistant, on-premises SQL Server"
---

# Migrate on-premises SQL Server or SQL Server on Azure VMs to Azure SQL Database using the Data Migration Assistant

[!INCLUDE [deprecation-notice](includes/deprecation-notice.md)]

The Data Migration Assistant provides seamless assessments of SQL Server on-premises and upgrades to later versions of SQL Server or migrations to SQL Server on Azure VMs or Azure SQL Database.

This article provides step-by-step instructions for migrating SQL Server on-premises to Azure SQL Database by using the Data Migration Assistant.

## Create a new migration project

1. On the left pane, select **New** (+), and then select the **Migration** project type.

1. Set the source type to **SQL Server** and the target server type to **Azure SQL Database**.

1. Select **Create**.

   :::image type="content" source="media/NewCreate1.png" alt-text="Screenshot of Create migration project." lightbox="media/NewCreate1.png":::

## Specify the source server and database

1. For the source, under **Connect to source server**, in the **Server name** text box, enter the name of the source SQL Server instance.

1. Select the **Authentication type** supported by the source SQL Server instance.

   > [!NOTE]  
   > It's recommended that you encrypt the connection by selecting the **Encrypt connection** check box under **Connection poperties**.

    :::image type="content" source="media/select-source-server.png" alt-text="Screenshot of Select source server." lightbox="media/select-source-server.png":::

1. Select **Connect**.

1. Select a single source database to migrate to Azure SQL Database.

   > [!NOTE]  
   > If you would like to assess the database and view and apply recommended fixes before migration, select the **Assess database before migration?** check box.

    :::image type="content" source="media/select-source-database.png" alt-text="Screenshot of Select source database." lightbox="media/select-source-database.png":::

1. Select **Next**.

## Specify the target server and database

1. For the target, under **Connect to target server**, in the **Server name** text box, enter the name of the Azure SQL Database instance.

1. Select the **Authentication type** supported by the target Azure SQL Database instance.

   > [!NOTE]  
   > It's recommended that you encrypt the connection by selecting the **Encrypt connection** check box under **Connection poperties**.

     :::image type="content" source="media/select-target-server.png" alt-text="Screenshot of Select target server." lightbox="media/select-target-server.png":::

1. Select **Connect**.

1. Select a single target database to which to migrate.

   > [!NOTE]  
   > If you intend to migrate Windows users, in the **Target external user domain name** text box, make sure that the target external user domain name is specified correctly.

    :::image type="content" source="media/select-target-database.png" alt-text="Screenshot of Select target database." lightbox="media/select-target-database.png":::

1. Select **Next**.

## Select schema objects

1. Select the schema objects from the source database that you want to migrate to Azure SQL Database.

    :::image type="content" source="media/select-schema-objects.png" alt-text="Screenshot of Select schema objects." lightbox="media/select-schema-objects.png":::

    > [!NOTE]  
    > Some of the objects that can't be converted as-is are presented with automatic fix opportunities. Selecting these objects on the left pane displays the suggested fixes on the right pane. Review the fixes and choose to either apply or ignore all changes, object by object. Applying or ignoring all changes for one object doesn't affect changes to other database objects. Statements that can't be converted or automatically fixed are reproduced to the target database and commented.

    :::image type="content" source="media/suggested-fix.png" alt-text="Screenshot of Suggested fix." lightbox="media/suggested-fix.png":::

1. Select **General SQL script**.

1. Review the generated script.

    :::image type="content" source="media/generated-script.png" alt-text="Screenshot of Generated script." lightbox="media/generated-script.png":::

## Deploy schema

1. Select **Deploy schema**.

1. Review the results of the schema deployment.

    :::image type="content" source="media/schema-deployment-results.png" alt-text="Screenshot of Schema deployment results." lightbox="media/schema-deployment-results.png":::

1. Select **Migrate data** to initiate the data migration process.

1. Select the tables with the data you want to migrate.

    :::image type="content" source="media/select-tables-to-migrate.png" alt-text="Screenshot of Select tables to migrate." lightbox="media/select-tables-to-migrate.png":::

1. Select **Start data migration**.

The final screen shows the overall status.

   :::image type="content" source="media/migration-status.png" alt-text="Screenshot of Migration status." lightbox="media/migration-status.png":::

## Related content

- [Data Migration Assistant (DMA)](dma-overview.md)
- [Data Migration Assistant: Configuration settings](dma-configurationsettings.md)
- [Data Migration Assistant: Best Practices](dma-bestpractices.md)
