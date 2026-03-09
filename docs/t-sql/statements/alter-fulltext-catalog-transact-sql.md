---
title: ALTER FULLTEXT CATALOG (Transact-SQL)
description: ALTER FULLTEXT CATALOG (Transact-SQL)
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 03/04/2026
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "ALTER_FULLEXT_CATALOG_TSQL"
  - "ALTER FULLEXT CATALOG"
helpviewer_keywords:
  - "modifying full-text catalogs"
  - "full-text catalogs [SQL Server], rebuilding"
  - "accent sensitivity"
  - "ALTER FULLTEXT CATALOG statement"
  - "full-text catalogs [SQL Server], modifying"
  - "full-text catalogs [SQL Server], reorganizing"
dev_langs:
  - TSQL
---
# ALTER FULLTEXT CATALOG (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Use this statement to change the properties of a full-text catalog.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
ALTER FULLTEXT CATALOG catalog_name
{ REBUILD [ WITH ACCENT_SENSITIVITY = { ON | OFF } ]
| REORGANIZE
| AS DEFAULT
}
```

## Arguments

#### *catalog_name*

Specifies the name of the catalog to modify. If a catalog with the specified name doesn't exist, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] returns an error and doesn't perform the `ALTER` operation.

#### REBUILD

The [!INCLUDE [ssdenoversion-md](../../includes/ssdenoversion-md.md)] rebuilds the entire catalog. When you rebuild a catalog, the existing catalog is deleted and a new catalog is created in its place. All the tables that have full-text indexing references are associated with the new catalog. Rebuilding resets the full-text metadata in the database system tables.

#### WITH ACCENT_SENSITIVITY = { ON | OFF }

Specifies if the catalog to be altered is accent-sensitive or accent-insensitive for full-text indexing and querying.

To determine the current accent-sensitivity property setting of a full-text catalog, use the `FULLTEXTCATALOGPROPERTY` function with the `AccentSensitivity` property value against *catalog_name*.

- If the function returns `1`, the full-text catalog is accent sensitive.
- If the function returns `0`, the catalog isn't accent sensitive.

The catalog and database default accent sensitivity are the same.

#### REORGANIZE

The [!INCLUDE [ssdenoversion-md](../../includes/ssdenoversion-md.md)] performs a *master merge*, which involves merging the smaller indexes created in the process of indexing into one large index. Merging the full-text index fragments can improve performance and free up disk and memory resources. If there are frequent changes to the full-text catalog, use this command periodically to reorganize the full-text catalog.

`REORGANIZE` also optimizes internal index and catalog structures.

Depending on the amount of indexed data, a master merge might take some time to complete. Merging a large amount of data can create a long running transaction, delaying truncation of the transaction log during a checkpoint. In this case, the transaction log might grow significantly under the full recovery model.

As a best practice, ensure that your transaction log contains sufficient space for a long-running transaction before reorganizing a large full-text index in a database that uses the full recovery model. For more information, see [Manage the size of the transaction log file](../../relational-databases/logs/manage-the-size-of-the-transaction-log-file.md).

#### AS DEFAULT

Specifies that this catalog is the default catalog. When you create full-text indexes without specifying catalogs, the default catalog is used. If there's an existing default full-text catalog, setting this catalog `AS DEFAULT` overrides the existing default.

## Permissions

To use `ALTER FULLTEXT CATALOG`, you need one of the following permissions:

- `ALTER` permission on the full-text catalog
- Membership in the **db_owner** or **db_ddladmin** fixed database roles
- Membership in the **sysadmin** fixed server role

To use `ALTER FULLTEXT CATALOG ... AS DEFAULT`, you need `ALTER` permission on the full-text catalog, and `CREATE FULLTEXT CATALOG` permission on the database.

## Remarks

When you run a `REBUILD` operation on a full-text catalog, the rebuild operation pauses if another session has an open transaction that's running `INSERT`, `UPDATE`, or `DELETE` operations on tables that belong to that catalog. The rebuild operation resumes only after that other transaction commits or rolls back. You can monitor this situation by using the [sys.dm_exec_requests](../../relational-databases/system-dynamic-management-views/sys-dm-exec-requests-transact-sql.md) and [sys.dm_exec_sessions](../../relational-databases/system-dynamic-management-views/sys-dm-exec-sessions-transact-sql.md) dynamic management views (DMVs). You might see locks between the user session and the background rebuild sessions, with a wait type of `LCK_M_IS`.

Similarly, during a `REORGANIZE` operation, you might see the `FT_MASTER_MERGE` wait type in the session where the command is running. This wait type can occur when other sessions have long-running transactions that are doing `INSERT`, `UPDATE`, or `DELETE` operations on tables in the same full-text catalog. In the `sys.dm_exec_requests` and `sys.dm_exec_sessions` DMVs, you might see one or more background sessions with a `LCK_M_IX` wait type and the `FT_MASTER_MERGE` command. The `REORGANIZE` operation doesn't complete until those locks are released.

The following query returns blocked background sessions.

```sql
SELECT r1.session_id,
       r1.blocking_session_id,
       r1.wait_type,
       r1.wait_resource,
       r1.last_wait_type,
       r1.command AS BlockedSessionCommand,
       r2.command AS BlockingSessionCommand,
       s1.login_name AS BlockedSessionLogin,
       s2.login_name AS BlockingSessionLogin,
       s1.host_name AS BlockedSessionHost,
       s2.host_name AS BlockingSessionHost,
       r1.status AS BlockedSessionStatus,
       r2.status AS BlockingSessionStatus
FROM sys.dm_exec_requests AS r1
     INNER JOIN sys.dm_exec_sessions AS s1
         ON r1.session_id = s1.session_id
     INNER JOIN sys.dm_exec_sessions AS s2
         ON r1.blocking_session_id = s2.session_id
     LEFT OUTER JOIN sys.dm_exec_requests AS r2
         ON s2.session_id = r2.session_id
WHERE r1.blocking_session_id <> 0
      AND r1.status = 'background'
ORDER BY r1.wait_time DESC;
```

## Examples

The following example changes the `AccentSensitivity` property of the default full-text catalog `ftCatalog`, which is accent sensitive.

1. Change catalog to accent insensitive.

   ```sql
   USE AdventureWorks2025;
   GO

   ALTER FULLTEXT CATALOG ftCatalog
   REBUILD WITH ACCENT_SENSITIVITY = OFF;
   ```

1. Check accent sensitivity.

   ```sql
   SELECT FULLTEXTCATALOGPROPERTY('ftCatalog', 'AccentSensitivity');
   ```

   The query returns `0`, which means the catalog isn't accent sensitive.

## Related content

- [sys.fulltext_catalogs (Transact-SQL)](../../relational-databases/system-catalog-views/sys-fulltext-catalogs-transact-sql.md)
- [CREATE FULLTEXT CATALOG (Transact-SQL)](create-fulltext-catalog-transact-sql.md)
- [DROP FULLTEXT CATALOG (Transact-SQL)](drop-fulltext-catalog-transact-sql.md)
- [Full-Text Search](../../relational-databases/search/full-text-search.md)
