---
description: "Temporal Table Security"
title: "Temporal Table Security | Microsoft Docs"
ms.custom: ""
ms.date: "10/16/2019"
ms.service: sql
ms.reviewer: ""
ms.subservice: table-view-index
ms.topic: conceptual
ms.assetid: 60e5d6f6-a26d-4bba-aada-42e382bbcd38
author: markingmyname
ms.author: maghan
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Temporal Table Security


[!INCLUDE [sqlserver2016-asdb-asdbmi](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi.md)]


To understand security as it applies to temporal tables, it is important to understand the security principals that apply to temporal tables. After you understand these security principles, you are ready to dive into the security around the **CREATE TABLE**, **ALTER TABLE**, and **SELECT** statements.

## Security Principles

 The following table describes the security principles that apply to temporal tables:

|Principle|Description|
|---------------|-----------------|
|Enabling/disabling system-versioning requires highest privileges on affected objects|Enabling and disabling SYSTEM_VERSIONING requires CONTROL permission on both the current and the history table|
|History data cannot be modified directly|When SYSTEM_VERSIONING is ON users cannot alter history data regardless of their actual permissions on current or the history table. This includes both data and schema modifications.|
|Querying history data requires **SELECT** permission on the history table|Merely because a user has **SELECT** permission on the current table does not mean that they have **SELECT** permission on the history table.|
|Audit surfaces operations affecting history table in specific ways:|Auditing settings from the current table are not automatically applied to the history table. Auditing needs to be enabled explicitly for history table.<br /><br /> Once enabled, auditing on history table regularly captures all direct attempts to access the data (regardless if they were successful or not).<br /><br /> **SELECT** with temporal query extension shows that history table was affected with that operation.<br /><br /> **CREATE/ALTER** temporal table expose information that permission check happens on history table as well. Audit file will contain additional record for history table.<br /><br /> DML operations on current table surface that history table was affected but additional_info provides necessary context (DML was result of system_versioning).|

## Performing Schema Operations

When SYSTEM_VERSIONING is set to ON, schema modification operations are limited.

### Disallowed ALTER schema operations

|Operation|Current Table|History Table|
|---------------|-------------------|-------------------|
|**DROP TABLE**|Disallowed|Disallowed|
|**ALTER TABLE...SWITCH PARTITION**|SWITCH IN only (see [Partitioning with Temporal Tables](../../relational-databases/tables/partitioning-with-temporal-tables.md))|SWITCH OUT only (see [Partitioning with Temporal Tables](../../relational-databases/tables/partitioning-with-temporal-tables.md))|
|**ALTER TABLE...DROP PERIOD**|Disallowed|-|
|**ALTER TABLE...ADD PERIOD**|-|Disallowed|

## Allowed ALTER TABLE operations

|Operation|Current|History|
|---------------|-------------|-------------|
|**ALTER TABLE...REBUILD**|Allowed (independently)|Allowed (independently)|
|**CREATE INDEX**|Allowed (independently)|Allowed (independently)|
|**CREATE STATISTICS**|Allowed (independently)|Allowed (independently)|

## Security of the CREATE Temporal TABLE Statement

| Feature | Create New History Table | Reuse Existing History Table |
| ------- | ------------------------ | ---------------------------- |
|Permission Required|**CREATE TABLE** permission in the database<br /><br /> **ALTER** permission on the schemas into which the current and history tables are being created|**CREATE TABLE** permission in the database<br /><br /> **ALTER** permission on the schema in which the current table will be created.<br /><br /> **CONTROL** permission on the history table specified as part of the **CREATE TABLE** statement creating the temporal table|
|Audit|Audit shows that users attempted to create two objects. Operation may fail due to lack of permissions to create a table in the database or due to lack of permissions to alter schemas for either table.|Audit shows that temporal table was created. Operation may fail due to lack of permission to create a table in the database, due to lack of permissions to alter the schema for the temporal table, or to lack of permissions on the history table.|

## Security of the ALTER Temporal TABLE SET (SYSTEM_VERSIONING ON/OFF) Statement

| Feature | Create New History Table | Reuse Existing History Table |
| ------- | ------------------------ | ---------------------------- |
|Permission Required|**CONTROL** permission in the database<br /><br /> **CREATE TABLE** permission in the database<br /><br /> **ALTER** permission on the schemas into which the history table is being created|**CONTROL** permission on the original table which is altered<br /><br /> **CONTROL** permission on the history table specified as part of the **ALTER TABLE** statement|
|Audit|Audit shows that the temporal table was altered and the history table was created at the same time. Operation may fail due to lack of permissions to create a table in the database, due to lack of permissions to alter schema for history table, or due to lack of permission to modify temporal table.|Audit shows that temporal table was altered, but operation required access to history table. Operation may fail due to lack of permissions on the history table or lack of permissions on the current table.|

## Security of SELECT Statement

**SELECT** permission is unchanged for **SELECT** statements that do not affect the history table. For **SELECT** statements that affect the history table, **SELECT** permission is required on both the current table and the history table.

## See Also

- [Temporal Tables](../../relational-databases/tables/temporal-tables.md) 
- [Getting Started with System-Versioned Temporal Tables](../../relational-databases/tables/getting-started-with-system-versioned-temporal-tables.md)
- [Temporal Table System Consistency Checks](../../relational-databases/tables/temporal-table-system-consistency-checks.md)
- [Partitioning with Temporal Tables](../../relational-databases/tables/partitioning-with-temporal-tables.md)
- [Temporal Table Considerations and Limitations](../../relational-databases/tables/temporal-table-considerations-and-limitations.md)
- [Manage Retention of Historical Data in System-Versioned Temporal Tables](../../relational-databases/tables/manage-retention-of-historical-data-in-system-versioned-temporal-tables.md)
- [System-Versioned Temporal Tables with Memory-Optimized Tables](../../relational-databases/tables/system-versioned-temporal-tables-with-memory-optimized-tables.md)
- [Temporal Table Metadata Views and Functions](../../relational-databases/tables/temporal-table-metadata-views-and-functions.md)
