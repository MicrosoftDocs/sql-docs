---
title: "sys.dm_pdw_component_health_status (Transact-SQL)"
description: sys.dm_pdw_component_health_status holds information about the current health of appliance components.
author: rwestMSFT
ms.author: randolphwest
ms.date: 03/31/2025
ms.service: sql
ms.subservice: data-warehouse
ms.topic: reference
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016"
---
# sys.dm_pdw_component_health_status (Transact-SQL)

[!INCLUDE [pdw](../../includes/applies-to-version/pdw.md)]

Holds information about the current health of appliance components.

| Column name | Data type | Description |
| --- | --- | --- | --- |
| `pdw_node_id` | **int** | Not nullable. |
| `component_id` | **int** | The ID of the component. For more information, see [sys.pdw_health_components](../system-catalog-views/sys-pdw-health-components-transact-sql.md). Not nullable. |
| `property_id` | **int** | The ID of the property. For more information, see [sys.pdw_health_component_properties](../system-catalog-views/sys-pdw-health-component-properties-transact-sql.md). Not nullable. |
| `component_instance_id` | **nvarchar(255)** | Identifies an instance of a component. For example, an instance of a CPU might be identified by `component_instance_id = 'CPU1'`. Not nullable. |
| `property_value` | **nvarchar(255)** | The current property value. Nullable. |
| `update_time` | **datetime** | The last time the metric was updated. Not nullable. |

<sup>1</sup> `pdw_node_id`, `component_id`, `property_id`, and `component_instance_id` form the key for this view.

## Related content

- [SQL and Parallel Data Warehouse Dynamic Management Views](sql-and-parallel-data-warehouse-dynamic-management-views.md)
