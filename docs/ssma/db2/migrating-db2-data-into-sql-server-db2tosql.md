---
title: "Migrate Db2 Data into SQL Server (Db2ToSQL)"
description: Learn how to migrate data from a Db2 database to SQL Server or Azure SQL Database, after you synchronize the converted objects.
author: cpichuka
ms.author: cpichuka
ms.reviewer: randolphwest
ms.date: 09/24/2024
ms.service: sql
ms.subservice: ssma
ms.topic: conceptual
ms.collection:
  - sql-migration-content
ms.custom:
  - intro-migration
f1_keywords:
  - "ssma.db2.general.f1"
---
# Migrate Db2 Data into SQL Server (Db2ToSQL)

After you successfully synchronize the converted objects with [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], you can migrate data from Db2 to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] using SQL Server Migration Assistant (SSMA) for Db2.

## Set migration options

Before migrating data to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], review the project migration options in the **Project Settings** dialog box.

- By using this dialog box, you can set options such as migration batch size, table locking, constraint checking, null value handling, and identity value handling. For more information about the Project Migration Settings, see [Project Settings (Migration)](project-settings-migration-db2tosql.md).

#### Client-side data migration

- To initiate data-migration on the client side, select the **Client Side Data Migration Engine** option in the **Project Settings** dialog box.

- In **Project Settings**, the **Client Side Data Migration Engine** option is set.

  > [!NOTE]  
  > The **Client-Side Data Migration Engine** resides inside the SSMA application and is, therefore, not dependent on the availability of the extension pack.

## Migrate data to SQL Server

Migrating data is a bulk-load operation that moves rows of data from Db2 tables into [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] tables in transactions. The number of rows loaded into [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] in each transaction is configured in the project settings.

To view migration messages, make sure that the Output pane is visible. Otherwise, navigate to **View** > **Output**.

1. Verify that:

   - The Db2 providers are installed on the computer that is running SSMA.

   - You synchronized the converted objects with the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] database.

1. In Db2 Metadata Explorer, select the objects that contain the data that you want to migrate:

   - To migrate data for all schemas, select the check box next to **Schemas**.

   - To migrate data or omit individual tables, first expand the schema, expand **Tables**, and then select or clear the check box next to the table.

1. Right-click **Schemas** in Db2 Metadata Explorer, and then select **Migrate Data**. You can also migrate data for individual objects or categories of objects: Right-click the object or its parent folder; select the **Migrate Data** option.

1. In the **Connect to Db2** dialog box, enter the connection credentials, and then select **Connect**. For more information on connecting to Db2, see [Connect to Db2 database](connecting-to-db2-database-db2tosql.md)

   For connecting to the target database [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], enter the connection credentials in the **Connect to SQL Server** dialog box, and select **Connect**. For more information on connecting to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], see [Connect to SQL Server](connecting-to-sql-server-db2tosql.md)

   Messages appear in the **Output** pane. When the migration is complete, the **Data Migration Report** appears. If any data didn't migrate, select the row that contains the errors, and then select **Details**. When you're finished with the report, select **Close**. For more information on Data Migration Report, see [Data Migration Report (Db2ToSQL)](data-migration-report-db2tosql.md).

## Related content

- [Migrate Db2 Data into SQL Server (Db2ToSQL)](migrating-db2-data-into-sql-server-db2tosql.md)
