---
title: "sys.dm_pdw_component_health_alerts (Transact-SQL)"
description: sys.dm_pdw_component_health_alerts stores previously issued alerts on appliance components.
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
# sys.dm_pdw_component_health_alerts (Transact-SQL)

[!INCLUDE [pdw](../../includes/applies-to-version/pdw.md)]

Stores previously issued alerts on appliance components.

| Column Name | Data Type | Description |
| --- | --- | --- |
| `pdw_node_id` | **int** | Unique identifier of a [!INCLUDE [ssPDW](../../includes/sspdw-md.md)] node. Not nullable. |
| `component_id` | **int** | The ID of the component. For more information, see [sys.pdw_health_components](../system-catalog-views/sys-pdw-health-components-transact-sql.md). Not nullable. |
| `component_instance_id` | **nvarchar(255)** | `pdw_node_id`, `component_id`, `component_instance_id`, `alert_id`, and `alert_instance_id` form the key for this view. Not nullable. |
| `alert_id` | **int** | The ID for the alert type. For more information, see [sys.pdw_health_alerts](../system-catalog-views/sys-pdw-health-alerts-transact-sql.md). Not nullable. |
| `alert_instance_id` | **nvarchar(36)** | Identifies an instance of a given alert. Not nullable. |
| `previous_value` <sup>2</sup>  | **nvarchar(255)** | Used when the alert is of type `StatusChange`. This is the previous component status. Value is `NULL` for alerts of type `Threshold`. Nullable. |
| `current_value` <sup>2</sup>  | **nvarchar(255)** | Used when the alert is of type `StatusChange`. This is the current component status. Value is `NULL` for alerts of type `Threshold`. Nullable. |
| `create_time` | **datetime** | Time and date when the alert was generated. Not nullable. |

<sup>1</sup> `pdw_node_id`, `component_id`, `component_instance_id`, `alert_id`, and `alert_instance_id` form the key for this view.

<sup>2</sup> See [sys.pdw_health_alerts](../system-catalog-views/sys-pdw-health-alerts-transact-sql.md) for a list of alert types.

## Related content

- [SQL and Parallel Data Warehouse Dynamic Management Views](sql-and-parallel-data-warehouse-dynamic-management-views.md)
