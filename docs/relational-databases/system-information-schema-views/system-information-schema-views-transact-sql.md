---
title: "System Information Schema Views (Transact-SQL)"
description: System information schema views are one method to provide SQL Server Database Engine metadata.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/16/2025
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "information schema views"
  - "schemas [SQL Server], information schema views"
  - "metadata [SQL Server], views"
  - "views [SQL Server], information schema"
  - "system views [SQL Server], information schema"
dev_langs:
  - "TSQL"
---
# System information schema views (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

An information schema view is one of several methods [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] provides for obtaining metadata. Information schema views provide an internal, system table-independent view of the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] metadata. Information schema views enable applications to work correctly, although significant changes were made to the underlying system tables. The information schema views included in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] comply with the ISO standard definition for the `INFORMATION_SCHEMA`.

> [!IMPORTANT]  
> Some changes were made to the information schema views that break backward compatibility. These changes are described in the articles for the specific views.

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] supports a three-part naming convention when you refer to the current server. The ISO standard also supports a three-part naming convention. However, the names used in both naming conventions are different. The information schema views are defined in a special schema named `INFORMATION_SCHEMA`. This schema is contained in each database. Each information schema view contains metadata for all data objects stored in that particular database. The following table shows the relationships between the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] names and the SQL standard names.

| SQL Server name | Maps to this equivalent SQL standard name |
| --- | --- |
| Database | Catalog |
| Schema | Schema |
| Object | Object |
| User-defined data type | Domain |

This name-mapping convention applies to the following [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] ISO-compatible views.

- [CHECK_CONSTRAINTS](check-constraints-transact-sql.md)
- [COLUMN_DOMAIN_USAGE](column-domain-usage-transact-sql.md)
- [COLUMN_PRIVILEGES](column-privileges-transact-sql.md)
- [COLUMNS](columns-transact-sql.md)
- [CONSTRAINT_COLUMN_USAGE](constraint-column-usage-transact-sql.md)
- [CONSTRAINT_TABLE_USAGE](constraint-table-usage-transact-sql.md)
- [DOMAIN_CONSTRAINTS](domain-constraints-transact-sql.md)
- [DOMAINS](domains-transact-sql.md)
- [KEY_COLUMN_USAGE](key-column-usage-transact-sql.md)
- [PARAMETERS](parameters-transact-sql.md)
- [REFERENTIAL_CONSTRAINTS](referential-constraints-transact-sql.md)
- [ROUTINE_COLUMNS](routine-columns-transact-sql.md)
- [ROUTINES](routines-transact-sql.md)
- [SCHEMATA](schemata-transact-sql.md)
- [TABLE_CONSTRAINTS](table-constraints-transact-sql.md)
- [TABLE_PRIVILEGES](table-privileges-transact-sql.md)
- [TABLES](tables-transact-sql.md)
- [VIEW_COLUMN_USAGE](view-column-usage-transact-sql.md)
- [VIEW_TABLE_USAGE](view-table-usage-transact-sql.md)
- [VIEWS](views-transact-sql.md)

Also, some views contain references to different classes of data such as character data or binary data.

When you reference the information schema views, you must use a qualified name that includes the `INFORMATION_SCHEMA` schema name. For example:

```sql
USE AdventureWorks2022;
GO

SELECT TABLE_CATALOG,
       TABLE_SCHEMA,
       TABLE_NAME,
       COLUMN_NAME,
       COLUMN_DEFAULT
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = N'Product';
```

## Permissions

The visibility of the metadata in information schema views is limited to securables that a user either owns or on which the user is granted some permission. For more information, see [Metadata Visibility Configuration](../security/metadata-visibility-configuration.md).

Information schema views are defined server-wide and therefore can't be denied within the context of a user database. To `REVOKE` or `DENY` access (`SELECT`), the `master` database must be used. By default the public role has `SELECT`-permission to all information schema views but the content is limited with metadata visibility rules.

You can't deny access to information schema views in [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)].

## Related content

- [Replication Views (Transact-SQL)](../system-views/replication-views-transact-sql.md)
- [Data types (Transact-SQL)](../../t-sql/data-types/data-types-transact-sql.md)
- [System stored procedures (Transact-SQL)](../system-stored-procedures/system-stored-procedures-transact-sql.md)
