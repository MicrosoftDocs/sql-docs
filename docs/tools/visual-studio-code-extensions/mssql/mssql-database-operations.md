---
title: Database Operations in the MSSQL Extension for Visual Studio Code
description: Learn how to manage databases, search objects, back up and restore databases, and import flat files using the MSSQL extension for Visual Studio Code.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: tsiddique, roblescarlos
ms.date: 02/21/2026
ms.service: sql
ms.subservice: vs-code-sql-extensions
ms.topic: overview
ms.collection:
  - data-tools
ai-usage: ai-assisted
---

# Database operations (Preview)

The MSSQL extension for Visual Studio Code provides built-in tools for common database operations. You can create and manage databases, search for objects, back up and restore databases, and import data from flat files, all without leaving the editor.

| Feature | Description |
| --- | --- |
| [Database management](#database-management) | Create, rename, and drop databases directly from the Object Explorer. |
| [Database Object Search](#database-object-search) | Find tables, views, functions, and stored procedures with type-aware search. |
| [Backup database](#backup-database) | Back up databases to disk or Azure Blob Storage with full, differential, or transaction log options. |
| [Restore database](#restore-database) | Restore databases from existing backup sets, backup files, or Azure Blob Storage. |
| [Import flat file](#import-flat-file) | Import CSV and TXT files into new SQL Server tables with a guided wizard. |

> [!TIP]  
> The features on this page are currently in preview and might change based on feedback. Join the community at [GitHub Discussions](https://aka.ms/vscode-mssql-discussions) to share ideas or report issues.

## Database management

The MSSQL extension provides dialogs for creating, renaming, and dropping databases directly from the **Object Explorer**.

### Create a database

1. In the **Connections** view, right-click a SQL Server instance node.

1. Select **Create Database (Preview)**.

1. In the **Create Database** dialog, enter the following information:

   - **Database Name**: Specify the name for the new database.
   - **Owner**: Choose the database owner (defaults to `<default>`).

1. (Optional) Expand **Advanced Options** to configure collation, recovery model, compatibility level, and containment type.

1. Select **Create** to create the database, or select **Script** to generate the equivalent T-SQL script.

   :::image type="content" source="media/mssql-database-operations/create-database.png" alt-text="Screenshot of the Create Database dialog with database name and advanced options.":::

After you create the database, it appears in the server's **Databases** list.

### Rename a database

1. In the **Connections** view, right-click a database node.

1. Select **Rename Database (Preview)**.

1. In the inline input box, enter the new name and press **Enter** to confirm, or press **Escape** to cancel.

   :::image type="content" source="media/mssql-database-operations/rename-database.png" alt-text="Screenshot of the Rename Database inline input prompt.":::

### Drop a database

1. In the **Connections** view, right-click a database node.

1. Select **Drop Database (Preview)**.

1. In the **Drop Database** dialog, review the database details (name, owner, and status).

1. (Optional) Select additional options:

   - **Drop active connections**: Terminates all active connections to the database before dropping.
   - **Delete backup and restore history**: Removes the backup and restore history for the database.

1. Select **Drop** to permanently delete the database, or select **Script** to generate the equivalent T-SQL script.

   :::image type="content" source="media/mssql-database-operations/drop-database.png" alt-text="Screenshot of the Drop Database dialog showing database details and drop options.":::

> [!IMPORTANT]  
> Dropping a database is irreversible. Make sure you have a backup before proceeding.

## Database Object Search

The Database Object Search feature lets you quickly find tables, views, functions, and stored procedures across your databases. You can search by name, filter by object type or schema, and run common actions directly from the results list.

### Open Database Object Search

1. In the **Connections** view, right-click a server or database node.

1. Select **Search Database Objects**.

   :::image type="content" source="media/mssql-database-operations/open-search-db-objects.png" alt-text="Screenshot of the Search Database Objects option in the Object Explorer context menu.":::

### Search and filter

In the Database Object Search view, type an object name (partial matches work) or use type prefixes to narrow your search:

- `t:` for tables
- `v:` for views
- `f:` for functions
- `sp:` for stored procedures

For example, `t:Customer` or `sp:GetOrders`.

You can also switch databases from the dropdown list, filter by type or schema, and refresh results.

:::image type="content" source="media/mssql-database-operations/database-object-search-view.png" alt-text="Screenshot of the database object search view with search results and filter options." lightbox="media/mssql-database-operations/database-object-search-view.png":::

### Actions

Each result row includes an **Actions** menu (**...**) with common operations such as scripting options, **Edit Data**, **Modify Data**, and **Copy Object Name**.

:::image type="content" source="media/mssql-database-operations/database-object-search-actions.png" alt-text="Screenshot of the actions menu for a database object search result." lightbox="media/mssql-database-operations/database-object-search-actions.png":::

## Backup database

The MSSQL extension provides a guided dialog for backing up SQL Server databases. You can save backups to disk or to Azure Blob Storage.

### Start a backup

1. In the **Connections** view, expand a SQL Server instance and then expand **Databases**.

1. Right-click the database you want to back up.

1. Select **Backup Database (Preview)**.

   :::image type="content" source="media/mssql-database-operations/backup-entry.png" alt-text="Screenshot of the Backup Database option in the Object Explorer context menu.":::

### Backup options

In the **Backup** dialog, configure the following settings:

- **Backup Name**: Auto-generated by default using the database name and timestamp. You can edit this value.
- **Backup Type**: Choose **Full**, **Differential**, or **Transaction Log**.
- **Copy-only Backup**: Creates a backup that doesn't affect the normal backup chain. This type is useful for ad hoc backups.

### Save to disk

Select **Save to Disk** to save the backup file to a location accessible by the SQL Server instance. Typically, you use this option for local or container-based SQL Server environments.

:::image type="content" source="media/mssql-database-operations/backup-dialog.png" alt-text="Screenshot of the Backup dialog with Save to Disk selected.":::

### Save to URL (Azure Blob Storage)

Select **Save to URL** to save the backup to Azure Blob Storage. When you select this option, provide the following information:

- **Azure Account**: Select an existing signed-in account or select **Add account** to sign in.
- **Tenant**: The tenant associated with the account.
- **Subscription**: The Azure subscription containing the storage account.
- **Storage Account**: The Azure Storage Account where the backup is stored.
- **Blob Container**: The container where the `.bak` file is uploaded.

:::image type="content" source="media/mssql-database-operations/backup-url.png" alt-text="Screenshot of the Backup dialog with Save to URL selected showing Azure configuration fields.":::

Select **Backup** to execute the operation, **Script** to generate the equivalent T-SQL script, or **Cancel** to close the dialog.

## Restore database

The MSSQL extension provides a guided dialog for restoring SQL Server databases from multiple sources.

### Start a restore

1. In the **Connections** view, expand a SQL Server instance and then expand **Databases**.

1. Right-click a database.

1. Select **Restore Database (Preview)**.

   :::image type="content" source="media/mssql-database-operations/restore-entry.png" alt-text="Screenshot of the Restore Database option in the Object Explorer context menu.":::

### Restore from database

Select **Database** as the backup location to restore from an existing backup set on the same SQL Server instance.

1. Select the **Source Database** that contains the backup history.
1. Choose the **Target Database** to restore into.
1. Review the available backup sets and select which ones to restore.

:::image type="content" source="media/mssql-database-operations/restore-dialog.png" alt-text="Screenshot of the Restore dialog with Database option selected.":::

### Restore from backup file

Select **Backup File** to restore from a `.bak` file accessible to the SQL Server instance.

1. Select an existing file or use **Browse files** to locate a backup file.
1. Specify the **Target Database** name.
1. Review and select backup sets to restore.

:::image type="content" source="media/mssql-database-operations/restore-backup-file.png" alt-text="Screenshot of the Restore dialog with Backup File option selected.":::

### Restore from URL (Azure Blob Storage)

Select **URL** to restore from a backup stored in Azure Blob Storage.

1. Sign in with your **Azure Account** or select **Add account**.
1. Select the **Tenant**, **Subscription**, **Storage Account**, and **Blob Container**.
1. Select the **Blob** containing the backup file.
1. Specify the **Target Database** name.

:::image type="content" source="media/mssql-database-operations/restore-url.png" alt-text="Screenshot of the Restore dialog with URL option selected showing Azure configuration fields.":::

Select **Restore** to execute the operation, **Script** to generate the equivalent T-SQL script, or **Cancel** to close the dialog.

## Import flat file

The Import Flat File feature provides a guided wizard that creates a new SQL Server table and populates it with data from a structured text file.

### Supported file types

Currently, the following text-based flat files are supported:

- `.csv`: Comma-separated values
- `.txt`: Delimited or fixed-width text files

> [!NOTE]  
> File formats such as Excel (`.xlsx`), JSON, XML, or Parquet aren't currently supported.

### Start the import

1. In the **Connections** view, expand your SQL Server connection and then expand **Databases**.

1. Right-click the database where you want to create the table.

1. Select **Import flat file (Preview)**.

   :::image type="content" source="media/mssql-database-operations/flat-file-import-entry.png" alt-text="Screenshot of the Import flat file option in the Object Explorer context menu.":::

### Step 1: Specify input file

In the first step, define where the data is imported and how the new table is created.

- **Database**: Select the target database.
- **Location of the file to be imported**: Enter the local file path or use **Browse** to select a file.
- **New Table Name**: Specify the name of the table to create.
- **Table Schema**: Choose the schema (for example, `dbo`).

Select **Next** to continue.

:::image type="content" source="media/mssql-database-operations/flat-file-dialog.png" alt-text="Screenshot of the Import File wizard Step 1 showing input file configuration.":::

### Step 2: Preview data

The wizard analyzes the input file and generates a preview of the data. It automatically infers column names and data types. Use this step to validate column alignment, delimiters, and data formatting.

Select **Next** if the preview looks correct. If not, go back and verify the input file.

:::image type="content" source="media/mssql-database-operations/flat-file-preview.png" alt-text="Screenshot of the Import File wizard Step 2 showing a data preview with inferred columns." lightbox="media/mssql-database-operations/flat-file-preview.png":::

### Step 3: Modify columns

Fine-tune the table schema before importing. For each column, you can:

- Edit the **Column Name**.
- Change the **Data Type** (for example, **nvarchar**, **float**, **tinyint**).
- Mark a column as a **Primary Key**.
- Configure **Allow Nulls**.

Select **Import Data** to create the table and import the data.

:::image type="content" source="media/mssql-database-operations/flat-file-modify.png" alt-text="Screenshot of the Import File wizard Step 3 showing column modification options.":::

## Feedback and support

[!INCLUDE [feedback](../includes/feedback.md)]

## Related content

- [Quickstart: Connect to and query a database with the MSSQL extension for Visual Studio Code](connect-database-visual-studio-code.md)
- [GitHub Copilot for MSSQL extension for Visual Studio Code](../github-copilot/overview.md)
- [Schema Designer](mssql-schema-designer.md)
- [Schema Compare](mssql-schema-compare.md)
- [Visual Studio Code documentation](https://code.visualstudio.com/docs)
- [MSSQL extension for Visual Studio Code repository on GitHub](https://github.com/Microsoft/vscode-mssql)
