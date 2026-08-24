---
title: "Metadata Functions (Transact-SQL)"
description: "Metadata Functions (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 01/20/2026
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
helpviewer_keywords:
  - "metadata [SQL Server], functions"
  - "functions [SQL Server], metadata"
dev_langs:
  - "TSQL"
---
# Metadata functions (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]

This article describes scalar functions that return information about the database and database objects.

All metadata functions are [nondeterministic](../../relational-databases/user-defined-functions/deterministic-and-nondeterministic-functions.md). They don't always return the same results every time they're called, even with the same set of input values.

## Server, database, session, and principal context metadata

Find out where you're running (instance and database) and who you're running as or through (client application, database principal). Use this information for environment-aware scripts and diagnostics.

| Function | Description |
| --- | --- |
| [SERVERPROPERTY](serverproperty-transact-sql.md) | Returns properties of the SQL Server instance such as edition, collation, or product level. |
| [DB_ID](db-id-transact-sql.md) | Returns the ID of a database. |
| [DB_NAME](db-name-transact-sql.md) | Returns the name of a database given the ID. |
| [DATABASEPROPERTYEX](databasepropertyex-transact-sql.md) | Returns database-level property values, such as collation and status. |
| [ORIGINAL_DB_NAME](original-db-name-transact-sql.md) | Returns the original database name before a restore sequence. |
| [APP_NAME](app-name-transact-sql.md) | Returns the application name for the current session. |
| [DATABASE_PRINCIPAL_ID](database-principal-id-transact-sql.md) | Returns the principal ID for a database security principal. |
| [VERSION](version-transact-sql-metadata-functions.md) | Returns the version string for Azure Synapse Analytics and Analytics Platform System (PDW). |

<sup>1</sup> **Applies to**: Azure Synapse Analytics and Analytics Platform System (PDW) only

## Object identification and name resolution

Resolve IDs to names across objects and schemas, and parse multipart identifiers. These functions provide core support for introspection and dynamic SQL.

| Function | Description |
| --- | --- |
| [OBJECT_ID](object-id-transact-sql.md) | Returns the ID for a schema-scoped object. |
| [OBJECT_NAME](object-name-transact-sql.md) | Returns the object name for an object ID. |
| [OBJECT_SCHEMA_NAME](object-schema-name-transact-sql.md) | Returns the schema name of an object. |
| [SCHEMA_ID](schema-id-transact-sql.md) | Returns a schema's ID. |
| [SCHEMA_NAME](schema-name-transact-sql.md) | Returns a schema's name by ID. |
| [PARSENAME](parsename-transact-sql.md) | Returns a part of a multipart object name (server, database, schema, object). |
| [@@PROCID](procid-transact-sql.md) | Returns the ID of the currently executing stored procedure. |

## Object capabilities and programmability metadata

Inspect what an object is, how it behaves, and how it's implemented. This inspection includes the definition and properties, and CLR assembly metadata.

| Function | Description |
| --- | --- |
| [OBJECT_DEFINITION](object-definition-transact-sql.md) | Returns the definition (source text) of programmable objects. |
| [OBJECTPROPERTY](objectproperty-transact-sql.md) | Returns a property for an object (for example, whether it's a view, table, and so on). |
| [OBJECTPROPERTYEX](objectpropertyex-transact-sql.md) | Returns extended object property information. |
| [ASSEMBLYPROPERTY](assemblyproperty-transact-sql.md) | Returns a specified property value of a SQL CLR assembly. |

## Data model metadata (types and columns)

Understand type identity and properties, and column identity and properties. Use this metadata for schema validation, code generation, ETL, and compatibility checks.

| Function | Description |
| --- | --- |
| [TYPE_ID](type-id-transact-sql.md) | Returns the ID of a data type. |
| [TYPE_NAME](type-name-transact-sql.md) | Returns a data type name by ID. |
| [TYPEPROPERTY](typeproperty-transact-sql.md) | Returns a property of a data type, such as precision or nullable. |
| [COL_NAME](col-name-transact-sql.md) | Returns the column name for a given column ID. |
| [COL_LENGTH](col-length-transact-sql.md) | Returns the length of a column in bytes. |
| [COLUMNPROPERTY](columnproperty-transact-sql.md) | Returns a property of a column, such as whether it's an identity or computed column. |

## Access path metadata (indexes and statistics)

Inspect indexing structures and statistics freshness. Use this metadata in tuning workflows and metadata-driven maintenance.

| Function | Description |
| --- | --- |
| [INDEX_COL](index-col-transact-sql.md) | Returns the name of an indexed column. |
| [INDEXKEY_PROPERTY](indexkey-property-transact-sql.md) | Returns a property of an index key. |
| [INDEXPROPERTY](indexproperty-transact-sql.md) | Returns a property of an index, such as clustered or disabled. |
| [STATS_DATE](stats-date-transact-sql.md) | Returns the date statistics were last updated for a table or index. |

## Physical storage and full-text metadata

Inspect the physical layout (files and filegroups) and full-text components. Use this information for storage management, troubleshooting, and configuration auditing.

| Function | Description |
| --- | --- |
| [FILE_ID](file-id-transact-sql.md) | Returns the file ID. |
| [FILE_IDEX](file-idex-transact-sql.md) | Returns the file ID based on file name. |
| [FILE_NAME](file-name-transact-sql.md) | Returns the file name for a file ID. |
| [FILEGROUP_ID](filegroup-id-transact-sql.md) | Returns a filegroup's ID. |
| [FILEGROUP_NAME](filegroup-name-transact-sql.md) | Returns the name of a filegroup. |
| [FILEGROUPPROPERTY](filegroupproperty-transact-sql.md) | Returns a property of a filegroup. |
| [FILEPROPERTY](fileproperty-transact-sql.md) | Returns a file property, such as size or status. |
| [FULLTEXTCATALOGPROPERTY](fulltextcatalogproperty-transact-sql.md) | Returns a property of a full-text catalog. |
| [FULLTEXTSERVICEPROPERTY](fulltextserviceproperty-transact-sql.md) | Returns full-text service properties, like load status. |

## Runtime coordination and generated value metadata

Coordinate work across sessions by using application locks. Retrieve generated numeric values for identity and sequence objects.

| Function | Description |
| --- | --- |
| [APPLOCK_MODE](applock-mode-transact-sql.md) | Returns the lock mode held by the current session. |
| [APPLOCK_TEST](applock-test-transact-sql.md) | Tests whether a lock can be acquired without actually acquiring it. |
| [SCOPE_IDENTITY](scope-identity-transact-sql.md) | Returns the last identity value generated in the current scope. |
| [NEXT VALUE FOR](next-value-for-transact-sql.md) | Returns the next value in a sequence object. |
