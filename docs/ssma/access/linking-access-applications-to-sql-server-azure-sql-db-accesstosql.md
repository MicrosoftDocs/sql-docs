---
title: "Link Access Applications to SQL Server and Azure SQL Database"
description: Learn how to link your Access tables to the migrated tables so that you can use your existing Access applications with SQL Server or Azure SQL Database.
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
  - "Access databases, linking to Azure SQL"
  - "Access databases, linking to SQL Server"
  - "auto-increment columns"
  - "data types, unsupported"
  - "hyperlink columns"
  - "linking tables"
  - "migrating databases, post-migration issues"
  - "post-migration issues"
  - "reference, post-migration issues"
  - "refreshing linked tables"
  - "slow performance"
  - "unlinking tables"
---
# Link Access applications to SQL Server and Azure SQL (AccessToSQL)

If you want to use your existing Access applications with [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], you can link your original Access tables to the migrated [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL tables. Linking modifies your Access database so that your queries, forms, reports, and data access pages use the data in the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL Database instead of the data in your Access database.

> [!NOTE]  
> Your Access tables remain in Access, but aren't updated together with [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL updates. After you link the tables and verify functionality, you might want to delete your Access tables.

## Link Access and SQL Server tables

When you link an Access table to a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL table, the Jet database engine stores connection information and table metadata, but the data is stored in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL. This linking allows your Access applications to operate against the Access tables even though the actual tables and data are in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL.

> [!NOTE]  
> If you use [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Authentication, your password is stored in clear text on the linked Access tables. Use Windows authentication instead.

1. In Access Metadata Explorer, select the tables that you want to link.

1. Right-click **Tables**, and then select **Link**.

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Migration Assistant (SSMA) for Access backs up the original Access table and creates a linked table.

After you link the tables, the tables in SSMA appear with a small link icon. In Access, the tables appear with a "linked" icon, which is a globe with an arrow pointing to it.

When you open a table in Access, the data is retrieved by using a keyset cursor. As a result, for large tables, the data isn't retrieved all at once. However, as you browse through the table, Access retrieves more data as necessary.

> [!IMPORTANT]  
> To link Access tables with an Azure database, you need SQL Server Native Client (SNAC) version 10.5 or later. Obtain the latest version of SNAC from [Microsoft SQL Server 2008 R2 Feature Pack](https://www.microsoft.com/download/details.aspx?id=44272). SNAC was removed in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)].

## Unlink Access tables

When you unlink an Access table from a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL table, SSMA restores the original Access table and its data.

1. In Access Metadata Explorer, select the tables that you want to unlink.

1. Right-click **Tables**, and then select **Unlink**.

## Link tables to a different server

If you link the Access tables to one SQL Server instance and later want to change the links to another instance, you must relink the tables.

1. In Access Metadata Explorer, select the tables that you want to unlink.

1. Right-click **Tables** and then select **Unlink**.

1. Select the **Reconnect to SQL Server** button.

1. Connect to the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL to which you want to link the Access tables.

1. In Access Metadata Explorer, select the tables that you want to link.

1. Right-click **Tables**, and then select **Link**.

## Update linked tables

If you alter the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL table definitions, you can unlink and then relink the tables in SSMA by using the procedures shown earlier in this article. You can also update the tables by using Access.

1. Open the Access database.

1. In the **Objects** list, select **Tables**.

1. Right-click a linked table, and then select **Linked Table Manager**.

1. Select the check box next to each linked table that you want to update, and then select **OK**.

## Possible post-migration issues

The following sections list issues that might occur in existing Access applications after you migrate databases from Access to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL and then link the tables. The sections also describe the causes and resolutions for these issues.

### Slow performance with linked tables

**Cause**: Some queries might be slow after upsizing for the following reasons:

- The application depends on functions that don't exist in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL. Jet pulls down tables locally to run a `SELECT` query.

- Jet sends queries that update or delete many rows as a parameterized query for each row.

**Resolution**: Convert the slow-running queries to pass-through queries, stored procedures, or views. Converting to pass-through queries has the following issues:

- You can't modify pass-through queries. You must modify the query result or add new records in an alternative way. For example, you can have explicit **Modify** or **Add** buttons on your form that is bound to the query.

- Pass-through queries don't support user input, but some queries require user input. You can use Visual Basic for Applications (VBA) to prompt for parameters, or get user input using a form. In both cases, the VBA code submits the query with the user input to the server.

### Autoincrement columns aren't updated until the record is updated

**Cause**: After calling `RecordSet.AddNew` in Jet, the autoincrement column is available before the record is updated. This condition isn't true in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL. The new value of the identity column is available only after saving the new record.

**Resolution**: Run the following Visual Basic for Applications (VBA) code before accessing the identity field:

```vba
Recordset.Update
Recordset.Move 0,
Recordset.LastModified
```

### New records aren't available

**Cause**: When you add a record to a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL table using VBA, if the table's unique index field has a default value and you don't assign a value to that field, the new record doesn't appear until you reopen the table in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL. If you try to get a value from the new record, you receive the following error message:

`Run-time error '3167' Record is deleted.`

**Resolution**: When you open the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL table by using VBA code, include the `dbSeeChanges` option, as in the following example:

```vba
Set rs = db.OpenRecordset("TestTable", dbOpenDynaset, dbSeeChanges)
```

### After migration, some queries don't allow the user to add a new record

**Cause**: If a query doesn't include all columns that are included in a unique index, you can't add new values by using the query.

**Resolution**: Ensure that all columns included in at least one unique index are part of the query.

### You can't modify a linked table schema by using Access

**Cause**: After migrating data and linking tables, you can't modify the schema of a table in Access.

**Resolution**: Modify the table schema by using [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)], and then update the link in Access.

### Hyperlink functionality is lost after migrating data

**Cause**: After data is migrated, hyperlinks in columns lose their functionality and become simple **nvarchar(max)** columns.

**Resolution**: None.

### Access doesn't support some SQL Server data types

**Cause**: If you update your [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL tables to include [data types](../../t-sql/data-types/data-types-transact-sql.md) that Access doesn't support, you can't open the table in Access.

**Resolution**: Define an Access query that returns only rows with supported data types.

## Related content

- [Migrate Access databases to SQL Server and Azure SQL](migrating-access-databases-to-sql-server-azure-sql-db-accesstosql.md)
