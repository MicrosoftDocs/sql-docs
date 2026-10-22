---
title: "Incompatible Access Features (AccessToSQL)"
description: Learn about possible migration issues with Access database features that aren't compatible with SQL Server, and how to address them.
author: nilabjaball
ms.author: niball
ms.reviewer: vanto
ms.date: 12/30/2025
ms.service: sql
ms.subservice: ssma
ms.topic: concept-article
ms.collection:
  - sql-migration-content
helpviewer_keywords:
  - "Access databases"
  - "Access databases, features incompatible with Azure SQL"
  - "Access databases, features incompatible with SQL Server"
  - "dates"
  - "default expressions"
  - "foreign keys"
  - "hyperlink columns"
  - "incompatible Access features"
  - "indexes"
  - "indexes, length of"
  - "keywords"
  - "primary keys"
  - "reference, incompatible features"
  - "replication columns"
  - "special characters"
  - "unique indexes"
  - "validation rules"
---
# Incompatible Access features (AccessToSQL)

Not all Access database features work with [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. For example, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] and Access use different sets of reserved keywords. These differences can cause problems when you migrate using SQL Server Migration Assistant (SSMA). The following section describes possible migration problems and how you can fix them.

## Database settings or features that might affect migration

Review the following Access settings or features that can affect migration to [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] or Azure SQL.

- [Tables don't have unique indexes](#tables-dont-have-unique-indexes)
- [Tables have replication columns](#tables-have-replication-columns)
- [Tables with unique indexes contain multiple NULL values](#tables-with-unique-indexes-contain-multiple-null-values)
- [Tables contain date values that are out of SQL Server range](#tables-contain-date-values-that-are-out-of-sql-server-range)
- [Index lengths exceed 900 bytes](#index-lengths-exceed-900-bytes)
- [Object names are SQL Server keywords, or contain special characters](#object-names-are-sql-server-keywords-or-contain-special-characters)
- [Field sizes differ in primary key or foreign key relationships](#field-sizes-differ-in-primary-key-or-foreign-key-relationships)
- [Referenced tables don't have a primary key or a unique index](#referenced-tables-dont-have-a-primary-key-or-a-unique-index)
- [Tables have hyperlink columns](#tables-have-hyperlink-columns)
- [Functions can't be converted to SQL Server or Azure SQL](#functions-cant-be-converted-to-sql-server-or-azure-sql)

### Tables don't have unique indexes

If you migrate a table without a unique index to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], you can't modify the table after migration. This limitation can cause application compatibility problems.

When you convert Access database objects, the Output window lists any Access tables that don't have unique indexes.

You can configure Access to add a primary key on the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] table during conversion. For more information, see [Project Settings (Conversion)](project-settings-conversion-accesstosql.md).

### Tables have replication columns

If you migrate an Access table that includes replication system columns to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], Jet replication functionality stops working after migration.

After migration, consider using [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] replication to maintain synchronized copies of your databases.

### Tables with unique indexes contain multiple NULL values

Before version 8.13, you can't transfer Access tables that have unique indexes with multiple null values to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. In [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], unique indexes disallow multiple nulls. Migration fails for these tables.

SSMA flags this issue in assessment reports. To create an assessment report, see [Assess Access database objects for conversion](assessing-access-database-objects-for-conversion-accesstosql.md).

If this problem exists, make sure that the primary key doesn't have duplicate null values. Or, remove the primary key or unique indexes that contain multiple null values.

### Tables contain date values that are out of SQL Server range

The [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] **datetime** type accepts dates in the range of January 1, 1753, to December 31, 9999, only. Access accepts dates in the range of January 1, 100, to December 31, 9999.

SSMA flags this issue in assessment reports. To create an assessment report, see [Assess Access database objects for conversion](assessing-access-database-objects-for-conversion-accesstosql.md).

You can configure how SSMA resolves dates that are out of the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] range. For more information, see [Project Settings (Migration)](project-settings-migration-accesstosql.md).

### Index lengths exceed 900 bytes

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] indexes have a 900-byte limit for the total size of index key columns. If your Access tables use larger indexes, SSMA displays a warning.

If you continue with data migration, the migration might fail.

### Object names are SQL Server keywords or contain special characters

Access and [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] have different sets of reserved keywords and special characters. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] accepts objects that are named by using [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] keywords or that contain special characters if you use bracketed or quoted identifiers, such as `select` or `[select].p`. For more information, see [Database identifiers](../../relational-databases/databases/database-identifiers.md).

> [!NOTE]  
> To use quotation marks to delimit identifiers, `SET QUOTED_IDENTIFIER` must be `ON`.

For example, `CREATE TABLE [schema](c1 [FOR])` is a valid statement, even though `schema` and `FOR` are reserved keywords. Also, `CREATE TABLE [xxx*yyy](c1 x&y)` is a valid statement, even though the table and column name contain the special characters `*` and `&`.

All queries that reference those objects must also use the names with brackets or quotation marks. For example, the query `SELECT * FROM schema` fails. The correct query is: `SELECT * FROM [schema]`.

When you convert Access database objects, the Output pane lists any Access tables that use keywords or special characters. You can modify the tables in Access, and then remove and add the database again. Or, you can modify queries that reference those objects so that the queries use brackets or quotation marks to delimit identifiers. If you don't modify your queries, your Access applications might return errors or have other problems.

### Field sizes differ in primary key or foreign key relationships

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] doesn't support the Jet functionality of linking columns that have different data types or sizes with foreign key constraints.

When you convert Access database objects, the Output window lists any primary key or foreign key constraints that aren't converted to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. You can alter data types and sizes on Access columns so that they match, then remove and add the Access database back again. Or, you can migrate data although these constraints aren't created in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

### Referenced tables don't have a primary key or a unique index

Access accepts relationships between tables where the referenced table doesn't have a primary key or a unique index. However, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] doesn't support this type of relationship.

When you convert Access database objects, the Output window lists any tables that have relationships but no primary key or unique index. You can alter the tables to add primary keys or unique indexes, then remove and add the Access database back again. Or, you can migrate data although the relationship between the tables is broken.

### Tables have hyperlink columns

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] doesn't support **hyperlink** columns. Instead, the columns are treated like Access memo columns. By default, these columns are converted to **nvarchar(max)** columns in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. You can customize the mapping. For more information, see [Map source and target data types](mapping-source-and-target-data-types-accesstosql.md).

### Functions can't be converted to SQL Server or Azure SQL

Access default expressions or validation rules might include Access system functions or user-defined functions that don't map to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL. If you use functions that don't map to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL, you can't load the default expressions or validation rules into [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL.

## Related content

- [Prepare Access databases for migration](preparing-access-databases-for-migration-accesstosql.md)
- [Migrate Access databases to SQL Server and Azure SQL](migrating-access-databases-to-sql-server-azure-sql-db-accesstosql.md)
