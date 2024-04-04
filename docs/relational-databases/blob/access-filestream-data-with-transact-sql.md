---
title: "Access FILESTREAM data with Transact-SQL"
description: Learn how to use the Transact-SQL INSERT, UPDATE, and DELETE statements to access and manage FILESTREAM data.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 12/13/2022
ms.service: sql
ms.subservice: filestream
ms.topic: conceptual
helpviewer_keywords:
  - "FILESTREAM [SQL Server], Transact-SQL"
---
# Access FILESTREAM data with Transact-SQL

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article describes how to use the [!INCLUDE[tsql](../../includes/tsql-md.md)] INSERT, UPDATE, and DELETE statements to manage FILESTREAM data.

> [!NOTE]  
> The examples in this article require the FILESTREAM-enabled database and table that are created in [Create a FILESTREAM-Enabled Database](../../relational-databases/blob/create-a-filestream-enabled-database.md) and [Create a Table for Storing FILESTREAM Data](../../relational-databases/blob/create-a-table-for-storing-filestream-data.md).

## <a id="ins"></a> Insert a row that contains FILESTREAM data

To add a row to a table that supports FILESTREAM data, use the [!INCLUDE[tsql](../../includes/tsql-md.md)] INSERT statement. When you insert data into a FILESTREAM column, you can insert NULL or a **varbinary(max)** value.

### Insert NULL

The following example shows how to insert `NULL`. When the FILESTREAM value is `NULL`, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] doesn't create a file in the file system.

:::code language="sql" source="codesnippet/tsql/access-filestream-data-w_1_1.sql":::

### Insert a zero-length record

The following example shows how to use `INSERT` to create a zero-length record. This is useful for when you want to obtain a file handle, but will be manipulating the file by using Win32 APIs.

:::code language="sql" source="codesnippet/tsql/access-filestream-data-w_1_2.sql":::

### Create a data file

The following example shows how to use `INSERT` to create a file that contains data. The [!INCLUDE[ssDE](../../includes/ssde-md.md)] converts the string `Seismic Data` to a `varbinary(max)` value. FILESTREAM creates the Windows file if it doesn't already exist. The data is then added to the data file.

:::code language="sql" source="codesnippet/tsql/access-filestream-data-w_1_3.sql":::

When you select all data from the `Archive.dbo.Records` table, the results are similar to the results that are shown in the following table. However, the `Id` column will contain different GUIDs.

| ID | SerialNumber | Chart |
| --- | --- | --- |
| `C871B90F-D25E-47B3-A560-7CC0CA405DAC` | `1` | `NULL` |
| `F8F5C314-0559-4927-8FA9-1535EE0BDF50` | `2` | `0x` |
| `7F680840-B7A4-45D4-8CD5-527C44D35B3F` | `3` | `0x536569736D69632044617461` |

## <a id="upd"></a> Update FILESTREAM data

You can use [!INCLUDE[tsql](../../includes/tsql-md.md)] to update the data in the file system file, but you might not want to when streaming large amounts of data to a file.

The following example replaces any text in the file record with the text `Xray 1`.

:::code language="sql" source="codesnippet/tsql/access-filestream-data-w_1_4.sql":::

## <a id="del"></a> Delete FILESTREAM data

When you delete a row that contains a FILESTREAM field, you also delete its underlying file system files. The only way to delete a row, and therefore the file, is to use the [!INCLUDE[tsql](../../includes/tsql-md.md)] DELETE statement.

The following example shows how to delete a row and its associated file system files.

:::code language="sql" source="codesnippet/tsql/access-filestream-data-w_1_5.sql":::

When you select all data from the `Archive.dbo.Records` table, the row is gone, and you can no longer use the associated file.

> [!NOTE]  
> The underlying files are removed by the FILESTREAM garbage collector.

## Check whether a table or database contains FILESTREAM data

To find out whether a database or table contains FILESTREAM data, you must query the system views.

The following extended example shows the steps to create a new database, create tables which have FILESTREAM data, and query system views to see whether the tables, and the database itself, contain FILESTREAM data.

:::code language="sql" source="codesnippet/tsql/access-filestream-data-w_1_6.sql":::

## See also

- [Enable and Configure FILESTREAM](../../relational-databases/blob/enable-and-configure-filestream.md)
- [Avoid Conflicts with Database Operations in FILESTREAM Applications](../../relational-databases/blob/avoid-conflicts-with-database-operations-in-filestream-applications.md)
