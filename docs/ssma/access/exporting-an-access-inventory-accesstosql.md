---
title: "Exporting an Access Inventory (AccessToSQL)"
description: Exporting an Access inventory (AccessToSQL)
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
  - "Access databases"
  - "Access databases, exporting metadata"
  - "exporting"
  - "exporting Access metadata"
  - "exporting, Access metadata"
  - "exporting, querying exported metadata"
  - "inventories of Access databases"
  - "querying exported metadata"
---
# Export an Access inventory (AccessToSQL)

If you have multiple Access databases and you're not sure which ones to migrate into [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], export an inventory of all Access databases in a project. You can review and query the inventory metadata to determine which databases and objects within those databases to migrate. This inventory helps you quickly find answers to questions such as the following list:

- What are the largest databases?
- Who owns most of the databases?
- Which databases contain the same tables?
- Which databases weren't modified in the last six months?
- Which databases contain private information?

Query examples that answer these questions are provided at the end of this article.

## Exported metadata

SQL Server Migration Assistant (SSMA) exports metadata about Access databases, tables, columns, indexes, foreign keys, queries, reports, forms, macros, and modules. SSMA exports metadata about each of these categories of items to a separate table. For schemas of these tables, see [Access inventory schemas](access-inventory-schemas-accesstosql.md).

## Export inventory data

To export an Access inventory, first open or create an SSMA project. Then add the Access database that you want to analyze. After you add databases to an SSMA project, you export metadata about those databases to a specified [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] database and schema. If necessary, SSMA creates tables to store the metadata. SSMA then adds the metadata about the Access databases to the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] database.

> [!NOTE]  
> An Access database can be split into multiple files: a back-end database that contains tables and front-end databases that contain queries, forms, reports, macros, modules, and shortcuts. If you want to migrate a split database to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], add the front-end database to SSMA.

The following instructions describe how to create a project, add databases to the project, connect to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], and then export inventory data.

#### Create a project

1. Open SSMA for Access.

1. On the **File** menu, select **New Project**.

   The **New Project** dialog box appears.

1. In the **Name** box, enter a name for your project.

1. In the **Location** box, enter or select a folder for the project.

1. In the **Migrate To** combo box, select the target version to which you want to migrate, and then select **OK**.

For more information about creating projects, see [Create and manage projects](creating-and-managing-projects-accesstosql.md).

#### Find and add databases

1. On the **File** menu, select **Find Databases**.

1. In the Find Databases Wizard, enter the drive, file path, or the UNC path that you want to search. Alternatively, select **Browse** to select the drive or network folder.

1. Select **Add** to add the location to the list box.

   Repeat the previous two steps to add more search locations.

1. Optionally, add search criteria to refine the list of databases that are returned.

   > [!IMPORTANT]  
   > The **All or part of the file name** text box doesn't support wildcard characters.

1. Select **Scan**.

   The **Scan** page appears. This page shows the databases that the tool found and the search progress. To stop the search, select **Stop**.

1. On the **Select Files** page, select each database that you want to add to the project.

   You can use the **Select All** and **Clear All** buttons at the top of the list to select or clear all databases. You can also hold the <kbd>Ctrl</kbd> key down to select multiple rows, or hold the <kbd>Shift</kbd> key down to select a range of rows.

1. Select **Next**.

1. On the **Verify** page, select **Finish**.

For more information about adding databases to projects, see [Add and remove Access database files](adding-and-removing-access-database-files-accesstosql.md).

#### Connect to SQL Server

1. On the **File** menu, select **Connect to SQL Server**.

1. In the connection dialog box, enter or select the name of the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

   - If you connect to the default instance on the local computer, enter `localhost` or a dot (`.`).

   - If you connect to the default instance on another computer, enter the name of the computer.

   - If you connect to a named instance, enter the computer name, a backslash, and the instance name. For example: `MyServer\MyInstance`.

1. In the **Database** box, enter the name of the target database for exported metadata.

1. If your instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is configured to accept connections on a nondefault port, enter the port number that is used for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] connections in the **Server port** box. For the default instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], the default port number is 1433. For named instances, SSMA tries to obtain the port number from the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Browser Service.

1. In the **Authentication** dropdown list, select the authentication type to use for the connection. To use the current Windows account, select **Windows Authentication**. To use a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login, select **SQL Server Authentication**, and then provide a user name and password.

For more information about connecting to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], see [Connect to SQL Server](connecting-to-sql-server-accesstosql.md).

#### Export inventory information

1. In Access Metadata Explorer, expand **Access-metabase**.

1. Select the check box next to **Databases**.

   To omit individual databases or database objects, expand the **Databases** folder, and then clear the check box next to the database or database object.

1. Right-click **Databases** and select **Export Schema**.

1. In the **Select Schema for Export** dialog box, select the target schema for the exported metadata, and then select **OK**.

Each time you export metadata, SSMA appends the data to the inventory. Existing data in the inventory isn't updated or deleted.

## Query the exported metadata

After you export metadata about Access databases, you can query the metadata. The following instructions describe how to use the Query Editor window in [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] to run queries.

#### Query metadata

1. Go to **SQL Server Management Studio** from the **Start** menu.

1. In the **Connect to Server** dialog box, verify the settings, and then select **Connect**.

1. On the Management Studio toolbar, select **New Query** to open Query Editor.

1. In the Query Editor window, enter a query. Some examples are shown in the following section.

1. Press the <kbd>F5</kbd> key to run the query.

## Query examples

Before you run any of the following queries, run a `USE database_name` query to make sure the queries run against the database that contains the exported metadata. For example, if you exported metadata to a database named `MyAccessMetadata`, you would add the following statement at the beginning of the [!INCLUDE [tsql](../../includes/tsql-md.md)] code:

```sql
USE MyAccessMetadata;
GO
```

The following examples all use the `dbo` schema. If you exported the metadata to another schema, make sure to change the schema when you run these queries.

### What tables and columns are in these databases?

The following query joins the tables that contain column, table, and database metadata, and then returns the names of all databases, tables, and columns, sorted by column name:

```sql
SELECT DatabaseName,
       TableName,
       ColumnName
FROM dbo.SSMA_Access_InventoryColumns AS C
     INNER JOIN dbo.SSMA_Access_InventoryTables AS T
         ON C.TableId = T.TableId
     INNER JOIN dbo.SSMA_Access_InventoryDatabases AS D
         ON T.DatabaseId = D.DatabaseId
ORDER BY ColumnName;
```

### What are the largest databases?

The following query returns the database name, file size, and number of tables in each Access database, sorted by file size:

```sql
SELECT DatabaseName,
       FileSize,
       TablesCount
FROM dbo.SSMA_Access_InventoryDatabases
ORDER BY FileSize DESC;
```

### Who owns most of the databases?

The following query returns the database name and owner of each Access database, sorted by owner.

```sql
SELECT DatabaseName,
       FileOwner
FROM dbo.SSMA_Access_InventoryDatabases
ORDER BY FileOwner;
```

### Which databases contain the same tables?

The following query uses a subquery to find all table names that appear more than once in the list of tables. It then uses this list of tables to get the database name. The results are returned as the database name and then the table name, and are sorted by table name.

```sql
SELECT DatabaseName,
       TableName
FROM dbo.SSMA_Access_InventoryTables AS T
     INNER JOIN dbo.SSMA_Access_InventoryDatabases AS D
         ON D.DatabaseId = T.DatabaseId
WHERE TableName IN (SELECT TableName
                    FROM dbo.SSMA_Access_InventoryTables
                    GROUP BY TableName
                    HAVING COUNT(*) > 1)
ORDER BY TableName;
```

### Which databases weren't modified in the last six months?

The following query gets the current date, gets the month value for six months ago, and then returns a list of databases with a modified date of greater than six months ago.

```sql
SELECT DatabaseName,
       DateModified
FROM dbo.SSMA_Access_InventoryDatabases
WHERE DATEDIFF(MONTH, DateModified, GETDATE()) > 6
ORDER BY DateModified;
```

### Which databases contain private information?

Your Access databases might contain sensitive or personal information. You might want to move these databases to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to take advantage of its security features. If you know that columns containing sensitive data have a specific name, or contain specific characters, use a query to find all columns that contain that information. For example, you can find all columns that include the string `salary`. The query then returns the database name, table name, and column name.

```sql
SELECT DatabaseName,
       TableName,
       ColumnName
FROM dbo.SSMA_Access_InventoryColumns AS C
     INNER JOIN dbo.SSMA_Access_InventoryTables AS T
         ON C.TableId = T.TableId
     INNER JOIN dbo.SSMA_Access_InventoryDatabases AS D
         ON T.DatabaseId = D.DatabaseId
WHERE ColumnName LIKE '%salary%';
```

If you don't know the column name, write a query to return all columns by removing the `WHERE` clause from this query.

## Related content

- [Prepare Access databases for migration](preparing-access-databases-for-migration-accesstosql.md)
