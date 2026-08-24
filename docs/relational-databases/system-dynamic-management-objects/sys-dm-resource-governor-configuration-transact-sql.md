---
title: "sys.dm_resource_governor_configuration (Transact-SQL)"
description: sys.dm_resource_governor_configuration (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: dfurman
ms.date: 02/11/2025
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "dm_resource_governor_configuration_TSQL"
  - "dm_resource_governor_configuration"
  - "sys.dm_resource_governor_configuration"
  - "sys.dm_resource_governor_configuration_TSQL"
helpviewer_keywords:
  - "sys.dm_resource_governor_configuration dynamic management view"
dev_langs:
  - "TSQL"
---

# sys.dm_resource_governor_configuration (Transact-SQL)

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

Returns a row that contains the currently effective resource governor configuration.

| Column name | Data type | Description |
|:--|:--|:--|
| `classifier_function_id` | **int** | The object ID of the currently used classifier function in [sys.objects](../system-catalog-views/sys-objects-transact-sql.md). Returns 0 if no classifier function is being used. Not nullable.<br /><br /> **Note:** This function is used to classify new requests and uses rules to route these requests to the appropriate workload group. For more information, see [Resource governor](../resource-governor/resource-governor.md). |
| `is_reconfiguration_pending` | **bit** | Indicates whether or not changes to resource governor configuration were made but have not been applied to the currently effective configuration using `ALTER RESOURCE GOVERNOR RECONFIGURE`. The changes that require resource governor reconfiguration include:<br /><br />- Create, modify, or delete a resource pool.<br />- Create, modify, or delete a workload group.<br />- Specify or remove a classifier function.<br />- Set or remove a limit on the maximum number of outstanding I/O operations per volume.<br /><br /> The value returned is one of:<br /><br /> 0 - A reconfiguration is not required.<br /><br /> 1 - A reconfiguration or server restart is required in order for pending configuration changes to be applied.<br /><br /> **Note:** The value returned is always 0 when resource governor is disabled.<br /><br /> Not nullable. |
| `max_outstanding_io_per_volume` | **int** | **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] and later.<br /><br /> The maximum number of outstanding I/O operations per volume. |

## Remarks

This dynamic management view shows the currently effective configuration. To see the stored configuration metadata, use [sys.resource_governor_configuration](../system-catalog-views/sys-resource-governor-configuration-transact-sql.md).

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

## Permissions

Requires the `VIEW SERVER STATE` permission.
  
### Permissions for SQL Server 2022 and later

Requires the `VIEW SERVER PERFORMANCE STATE` permission on the server.

## Related content

- [System dynamic management views and functions](system-dynamic-management-objects.md)
- [sys.resource_governor_configuration (Transact-SQL)](../system-catalog-views/sys-resource-governor-configuration-transact-sql.md)
- [Resource governor](../resource-governor/resource-governor.md)
