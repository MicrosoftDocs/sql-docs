---
title: Temporal table security
description: Learn how to secure system-versioned temporal tables.
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/29/2024
ms.service: sql
ms.subservice: table-view-index
ms.topic: conceptual
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# Temporal table security

[!INCLUDE [sqlserver2016-asdb-asdbmi](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi.md)]

To understand security as it applies to temporal tables, you must understand the security principles that apply to temporal tables. After you understand these security principles, you're ready to dive into the security around the `CREATE TABLE`, `ALTER TABLE`, and `SELECT` statements.

## Security principles

The following table describes the security principles that apply to temporal tables:

| Principle | Description |
| --- | --- |
| Enable/disable system-versioning requires highest privileges on affected objects | Enabling and disabling `SYSTEM_VERSIONING` requires `CONTROL` permission on both the current and the history table. |
| History data can't be modified directly | When `SYSTEM_VERSIONING` is `ON`, users can't alter history data, regardless of their actual permissions on the current or history table. This limitation includes both data and schema modifications. <sup>1</sup> |
| Querying history data requires `SELECT` permission on the history table | A user with `SELECT` permission on the current table doesn't automatically have `SELECT` permission on the history table. |
| Audit reveals operations affecting history table in specific ways | Auditing settings from the current table aren't automatically applied to the history table. Auditing needs to be enabled explicitly for history table. Once enabled, auditing on the history table regularly captures all direct attempts to access the data (regardless if they were successful or not).<br /><br />`SELECT` with temporal query extension shows that the history table was affected with that operation.<br /><br />`CREATE/ALTER` temporal table exposes information that the permission check happens on the history table as well. The audit file contains additional records for the history table.<br /><br />DML operations on the current table reveal that history table was affected, but `additional_information` provides necessary context (DML was result of `SYSTEM_VERSIONING`). |

<sup>1</sup> If you have ALTER permission on the current table and the history table, and you drop a column in the current table, the associated column in the history table is also dropped, even if `SYSTEM_VERSIONING` is `ON`.

## Perform schema operations

When `SYSTEM_VERSIONING` is set to `ON`, schema modification operations are limited.

### Disallowed ALTER schema operations

| Operation | Current table | History table |
| --- | --- | --- |
| `DROP TABLE` | Disallowed | Disallowed |
| `ALTER TABLE...SWITCH PARTITION` | `SWITCH IN` only (see [Partition with temporal tables](partitioning-with-temporal-tables.md)) | `SWITCH OUT` only (see [Partition with temporal tables](partitioning-with-temporal-tables.md)) |
| `ALTER TABLE...DROP PERIOD` | Disallowed | N/A |
| `ALTER TABLE...ADD PERIOD` | N/A | Disallowed |

## Allowed ALTER TABLE operations

| Operation | Current | History |
| --- | --- | --- |
| `ALTER TABLE...REBUILD` | Allowed (independently) | Allowed (independently) |
| `CREATE INDEX` | Allowed (independently) | Allowed (independently) |
| `CREATE STATISTICS` | Allowed (independently) | Allowed (independently) |

## Security of the CREATE temporal table statement

| Feature | Create new history table | Reuse existing history table |
| --- | --- | --- |
| Permission required | `CREATE TABLE` permission in the database<br /><br />`ALTER` permission on the schemas into which the current and history tables are being created | `CREATE TABLE` permission in the database<br /><br />`ALTER` permission on the schema in which the current table will be created.<br /><br />`CONTROL` permission on the history table specified as part of the `CREATE TABLE` statement creating the temporal table. |
| Audit | Audit shows that users attempted to create two objects. Operation might fail due to lack of permissions to create a table in the database, or due to lack of permissions to alter schemas for either table. | Audit shows that the temporal table was created. The operation might fail due to lack of permission to create a table in the database, lack of permissions to alter the schema for the temporal table, or lack of permissions on the history table. |

## Security of the ALTER temporal table SET (SYSTEM_VERSIONING ON/OFF) statement

| Feature | Create new history table | Reuse existing history table |
| --- | --- | --- |
| Permission required | `CONTROL` permission in the database.<br /><br />`CREATE TABLE` permission in the database.<br /><br />`ALTER` permission on the schemas into which the history table is being created. | `CONTROL` permission on the original table that is altered.<br /><br />`CONTROL` permission on the history table specified as part of the `ALTER TABLE` statement. |
| Audit | Audit shows that the temporal table was altered and the history table was created at the same time. This operation might fail due to lack of permissions to create a table in the database, lack of permissions to alter schema for history table, or lack of permission to modify the temporal table. | Audit shows that the temporal table was altered, but the operation required access to the history table. The operation might fail due to lack of permissions on the history table, or lack of permissions on the current table. |

## Security of the SELECT statement

`SELECT` permission is unchanged for `SELECT` statements that don't affect the history table. For `SELECT` statements that affect the history table, `SELECT` permission is required on both the current table and the history table.

## Related content

- [Temporal tables](temporal-tables.md)
- [Get started with system-versioned temporal tables](getting-started-with-system-versioned-temporal-tables.md)
- [Temporal table system consistency checks](temporal-table-system-consistency-checks.md)
- [Partition with temporal tables](partitioning-with-temporal-tables.md)
- [Temporal table considerations and limitations](temporal-table-considerations-and-limitations.md)
- [Manage retention of historical data in system-versioned temporal tables](manage-retention-of-historical-data-in-system-versioned-temporal-tables.md)
- [System-versioned temporal tables with memory-optimized tables](system-versioned-temporal-tables-with-memory-optimized-tables.md)
- [Temporal table metadata views and functions](temporal-table-metadata-views-and-functions.md)
