---
title: "sys.resource_governor_configuration (Transact-SQL)"
description: sys.resource_governor_configuration (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: dfurman
ms.date: 02/10/2025
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.resource_governor_configuration_TSQL"
  - "sys.resource_governor_configuration"
  - "resource_governor_configuration_TSQL"
  - "resource_governor_configuration"
helpviewer_keywords:
  - "sys.resource_governor_configuration catalog view"
dev_langs:
  - "TSQL"
---

# sys.resource_governor_configuration (Transact-SQL)

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

Returns the stored resource governor configuration.

| Column name | Data type | Description |
|:--|:--|:--|
| `classifier_function_id` | **int** | The object ID of the classifier function in [sys.objects](../system-catalog-views/sys-objects-transact-sql.md). Not nullable.<br /><br /> **Note** This function is used to classify new sessions and uses rules to route the workload to the appropriate workload group. For more information, see [Resource governor](../../relational-databases/resource-governor/resource-governor.md). |
| `is_enabled` | **bit** | Indicates the current state of resource governor:<br /><br /> 0 = is not enabled.<br /><br /> 1 = is enabled.<br /><br /> Not nullable. |
| `max_outstanding_io_per_volume` | **int** | **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] and later.<br /><br /> The maximum number of outstanding I/O requests per volume. |

## Remarks

The catalog view displays resource governor configuration as stored in metadata. To see the currently effective configuration, use [sys.dm_resource_governor_configuration](../system-dynamic-management-views/sys-dm-resource-governor-configuration-transact-sql.md).

## Permissions

Requires the `VIEW ANY DEFINITION` permission to view contents.

## Examples

The following example shows how to get and compare the stored metadata values and the currently effective values of resource governor configuration.

```sql
USE master;

-- Get the stored metadata
SELECT OBJECT_SCHEMA_NAME(classifier_function_id) AS classifier_function_schema_name,
       OBJECT_NAME(classifier_function_id) AS classifier_function_name
FROM sys.resource_governor_configuration;

-- Get the currently effective configuration
SELECT OBJECT_SCHEMA_NAME(classifier_function_id) AS classifier_function_schema_name,
       OBJECT_NAME(classifier_function_id) AS classifier_function_name
FROM sys.dm_resource_governor_configuration;
```

## Related content

- [Resource governor catalog views (Transact-SQL)](resource-governor-catalog-views-transact-sql.md)
- [System catalog views (Transact-SQL)](catalog-views-transact-sql.md)
- [sys.dm_resource_governor_configuration (Transact-SQL)](../system-dynamic-management-objects/sys-dm-resource-governor-configuration-transact-sql.md)
- [Resource governor](../resource-governor/resource-governor.md)
