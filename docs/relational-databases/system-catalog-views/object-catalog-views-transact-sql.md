---
title: "Object Catalog Views (Transact-SQL)"
description: Object Catalog Views (Transact-SQL)
author: dimitri-furman
ms.author: dfurman
ms.date: 01/19/2026
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
helpviewer_keywords:
  - "object catalog views [SQL Server]"
  - "catalog views [SQL Server], objects"
dev_langs:
  - "TSQL"
---
# Object catalog views (Transact-SQL)

[!INCLUDE [sqlserver](../../includes/applies-to-version/sqlserver.md)]

Object catalog views expose metadata about database objects, their definitions, dependencies, and physical structures. The views in the following sections are grouped by the type of metadata they describe, so you can more easily navigate related concepts.

- [Object definitions and primary object types](#object-definitions-and-primary-object-types)
- [Columns, parameters, and data structure](#columns-parameters-and-data-structure)
- [Constraints and relationships](#constraints-and-relationships)
- [Indexing, statistics, and storage layout](#indexing-statistics-and-storage-layout)
- [Module definitions and dependencies](#module-definitions-and-dependencies)
- [Triggers, events, and messaging](#triggers-events-and-messaging)
- [Specialized and system-managed metadata](#specialized-and-system-managed-metadata)

## Object definitions and primary object types

These views identify the logical objects defined in a database and their basic characteristics.

| System catalog view | Description |
| --- | --- |
| [sys.objects](sys-objects-transact-sql.md) | Central catalog of schema-scoped objects. Use it as the starting point for discovering object type, schema, and status. |
| [sys.tables](sys-tables-transact-sql.md) | Metadata specific to user tables, including table-level properties. |
| [sys.views](sys-views-transact-sql.md) | Metadata for views, including whether they're schema-bound or indexed. |
| [sys.procedures](sys-procedures-transact-sql.md) | Metadata for stored procedures. |
| [sys.numbered_procedures](sys-numbered-procedures-transact-sql.md) | Metadata for numbered stored procedures created with the same base name. |
| [sys.numbered_procedure_parameters](sys-numbered-procedure-parameters-transact-sql.md) | Parameter metadata specific to numbered stored procedures. |
| [sys.table_types](sys-table-types-transact-sql.md) | Metadata for user-defined table types used in parameters and variables. |
| [sys.synonyms](sys-synonyms-transact-sql.md) | Maps synonyms to the objects they reference. |
| [sys.sequences](sys-sequences-transact-sql.md) | Metadata for sequence objects used to generate numeric values. |

## Columns, parameters, and data structure

These views describe how data is structured within tables, views, and programmable objects.

| System catalog view | Description |
| --- | --- |
| [sys.columns](sys-columns-transact-sql.md) | Defines column names, data types, nullability, and other column-level attributes. |
| [sys.computed_columns](sys-computed-columns-transact-sql.md) | Describes computed columns and their defining expressions. |
| [sys.identity_columns](sys-identity-columns-transact-sql.md) | Identifies columns that generate values automatically and their identity settings. |
| [sys.masked_columns](sys-masked-columns-transact-sql.md) | Indicates which columns use dynamic data masking and how masking is applied. |
| [sys.parameters](sys-parameters-transact-sql.md) | Describes input and output parameters for stored procedures and functions. |
| [sys.function_order_columns](sys-function-order-columns-transact-sql.md) | Provides metadata for columns involved in ordered set functions. |

## Constraints and relationships

These views describe rules that enforce data integrity and relationships between tables.

| System catalog view | Description |
| --- | --- |
| [sys.check_constraints](sys-check-constraints-transact-sql.md) | Defines logical conditions that restrict allowable values in columns. |
| [sys.default_constraints](sys-default-constraints-transact-sql.md) | Specifies default values applied when no explicit value is provided. |
| [sys.key_constraints](sys-key-constraints-transact-sql.md) | Identifies PRIMARY KEY and UNIQUE constraints. |
| [sys.foreign_keys](sys-foreign-keys-transact-sql.md) | Describes relationships between parent and referenced tables. |
| [sys.foreign_key_columns](sys-foreign-key-columns-transact-sql.md) | Maps the specific columns participating in foreign key relationships. |

## Indexing, statistics, and storage layout

These views describe how data is indexed, partitioned, and physically stored, and how the query optimizer gathers metadata.

| System catalog view | Description |
| --- | --- |
| [sys.index_columns](sys-index-columns-transact-sql.md) | Defines which columns participate in indexes and how they're ordered. |
| [sys.hash_indexes](sys-hash-indexes-transact-sql.md) | Metadata for hash indexes used by memory-optimized tables. |
| [sys.stats](sys-stats-transact-sql.md) | Describes statistics objects used by the query optimizer. |
| [sys.stats_columns](sys-stats-columns-transact-sql.md) | Identifies the columns that make up each statistics object. |
| [sys.partitions](sys-partitions-transact-sql.md) | Describes how tables and indexes are divided into partitions. |
| [sys.allocation_units](sys-allocation-units-transact-sql.md) | Exposes storage allocation details used to persist table and index data. |

## Module definitions and dependencies

These views expose executable object definitions and the dependencies between database objects.

| System catalog view | Description |
| --- | --- |
| [sys.sql_modules](sys-sql-modules-transact-sql.md) | Stores the Transact-SQL source text for views, procedures, functions, and triggers. |
| [sys.assembly_modules](sys-assembly-modules-transact-sql.md) | Metadata for CLR-based database objects. |
| [sys.sql_expression_dependencies](sys-sql-expression-dependencies-transact-sql.md) | Tracks dependencies inferred from SQL expressions, used for impact analysis. |
| [sys.sql_dependencies](sys-sql-dependencies-transact-sql.md) | Legacy dependency information retained for backward compatibility. |

## Triggers, events, and messaging

These views describe event-driven behavior and asynchronous processing infrastructure.

| System catalog view | Description |
| --- | --- |
| [sys.triggers](sys-triggers-transact-sql.md) | Metadata for Data Manipulation Language (DML) and Data Definition Language (DDL) triggers. |
| [sys.trigger_events](sys-trigger-events-transact-sql.md) | Identifies which events cause triggers to fire. |
| [sys.trigger_event_types](sys-trigger-event-types-transact-sql.md) | Lists the supported trigger event types. |
| [sys.event_notifications](sys-event-notifications-transact-sql.md) | Describes event notifications configured for database or server events. |
| [sys.events](sys-events-transact-sql.md) | Lists event types that can be used with event notifications. |
| [sys.service_queues](sys-service-queues-transact-sql.md) | Metadata for Service Broker queues used for message processing. |

## Specialized and system-managed metadata

These views expose metadata for features that are engine-managed or feature-specific rather than general-purpose schema elements.

| System catalog view | Description |
| --- | --- |
| [sys.periods](sys-periods-transact-sql.md) | Defines system-time periods for temporal tables. |
| [sys.memory_optimized_tables_internal_attributes](sys-memory-optimized-tables-internal-attributes-transact-sql.md) | Internal metadata for memory-optimized tables. |
| [sys.extended_procedures](sys-extended-procedures-transact-sql.md) | Metadata for legacy extended stored procedures. |

## Related content

- [System catalog views (Transact-SQL)](catalog-views-transact-sql.md)
- [Transact-SQL reference (Database Engine)](../../t-sql/language-reference.md)
