---
title: "sys.dm_operation_status"
description: The sys.dm_operation_status dynamic management view displays information about operations performed on databases.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: randolphwest
ms.date: 09/11/2025
ms.service: azure-sql-database
ms.topic: "reference"
ms.custom:
  - ignite-2025
f1_keywords:
  - "dm_operation_status_TSQL"
  - "dm_operation_status"
  - "sys.dm_operation_status"
  - "sys.dm_operation_status_TSQL"
helpviewer_keywords:
  - "dm_operation_status dynamic management view"
  - "sys.dm_operation_status dynamic management view"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || =azure-sqldw-latest || =azuresqldb-mi-current || =fabric-sqldb"
---
# sys.dm_operation_status

[!INCLUDE [asdb-asdbmi-asa-fabricsqldb](../../includes/applies-to-version/asdb-asdbmi-asa-fabricsqldb.md)]

The sys.dm_operation_status dynamic management view displays information about operations performed on databases.

| Column name | Data type | Description |
| --- | --- | --- |
| `session_activity_id` | **uniqueidentifier** | ID of the operation. Not null. |
| `resource_type` | **int** | Denotes the type of resource on which the operation is performed. Not null. In the current release, this view tracks operations performed on [!INCLUDE[ssSDS](../../includes/sssds-md.md)] only, and the corresponding integer value is `0`. |
| `resource_type_desc` | **nvarchar(2048)** | Description of the resource type on which the operation is performed. Currently view tracks operations performed on [!INCLUDE[ssSDS](../../includes/sssds-md.md)] only. |
| `major_resource_id` | **sql_variant** | Name of the [!INCLUDE[ssSDS](../../includes/sssds-md.md)] on which the operation is performed. Not null. |
| `minor_resource_id` | **sql_variant** | For internal use only. Not null. |
| `operation` | **nvarchar(60)** | Operation performed on a [!INCLUDE[ssSDS](../../includes/sssds-md.md)], such as `CREATE` or `ALTER`. |
| `state` | **tinyint** | The state of the operation.<br /><br />0 = Pending<br />1 = In progress<br />2 = Completed<br />3 = Failed<br />4 = Cancel in progress<br />5 = Cancelled |
| `state_desc` | **nvarchar(120)** | `PENDING` = operation is waiting for resource or quota availability.<br /><br />`IN_PROGRESS` = operation has started and is in progress.<br /><br />`COMPLETED` = operation completed successfully.<br /><br />`FAILED` = operation failed. See the `error_desc` column for details.<br /><br />`CANCEL_IN_PROGRESS` = operation is in the process of being cancelled.<br /><br />`CANCELLED` = operation stopped at the request of the user. |
| `percent_complete` | **int** | Percentage of operation that has completed. Valid values are listed below. Not null.<br /><br />`0` = Operation not started<br /><br />`50` = Operation in progress. For restore operations, this will be a value between `1` to `99`, indicating how far along the operation is in percent.<br /><br />`100` = Operation complete |
| `error_code` | **int** | Code indicating the error that occurred during a failed operation. If the value is 0, it indicates that the operation completed successfully. |
| `error_desc` | **nvarchar(2048)** | Description of the error that occurred during a failed operation. |
| `error_severity` | **int** | Severity level of the error that occurred during a failed operation. For more information about error severities, see [Database Engine Error Severities](../errors-events/database-engine-error-severities.md). |
| `error_state` | **int** | Reserved for future use. Future compatibility is not guaranteed. |
| `start_time` | **datetime** | Timestamp when the operation started. |
| `last_modify_time` | **datetime** | Timestamp when the record was last modified for a long running operation. When the operation has completed successfully, this field displays the timestamp when the operation completed. |
| `phase_code` | **int** | Only applicable when the service tier is converting to Hyperscale, else `NULL`. Phases 5 and 6 are applicable only for `MANUAL_CUTOVER` option.</br></br>`1` – LogTransitionInProgress</br>`2` – Copying</br>`3` – BuildingHyperscaleComponents</br>`4` – Catchup</br>`5` – WaitingForCutover</br>`6` – CutoverInProgress</br>|
| `phase_desc` | **nvarchar(60)** | Description of the phase that is in progress. Only applicable when the service tier is converting to Hyperscale, else `NULL`. Phases WaitingForCutover and CutoverInProgress are applicable only for `MANUAL_CUTOVER` option. For more information, see [conversion to Hyperscale](/azure/azure-sql/database/convert-to-hyperscale?view=azuresql-db&preserve-view=true). |
| `phase_info` | **nvarchar(2048)** | This column provides more information about the specific phase in progress, in JSON format. Might not be populated for all operations.</br></br>When tier [conversion to Hyperscale](/azure/azure-sql/database/convert-to-hyperscale?view=azuresql-db&preserve-view=true) is performed on primary replica, information would be shown for both primary and secondary, one at a time. </br></br>|

## Permissions

This view is only available in the `master` database to the server-level principal login.

## Remarks

To use this view, you must be connected to the `master` database. Use the `sys.dm_operation_status` view in the `master` database of the [!INCLUDE[ssSDS](../../includes/sssds-md.md)] server to track the status of the following operations performed on a [!INCLUDE[ssSDS](../../includes/sssds-md.md)]:

- Create database

- Copy database. Database Copy creates a record in this view on both the source and target servers.

- Alter database

- Change the performance level of a service tier

- Change the service tier of a database, such as changing from Basic to Standard.

- Setting up a Geo-Replication relationship

- Terminating a Geo-Replication relationship

- Restore database

- Delete database

The information in this view is retained for approximately 1 hour. You can use the [Azure Activity Log](/azure/azure-monitor/platform/activity-log) to view details of operations in the last 90 days. For retention more than 90 days, consider [sending Activity Log](/azure/azure-monitor/platform/activity-log#send-to-log-analytics-workspace) entries to a Log Analytics workspace.

## Examples

Show most recent operations associated with database `mydb`:

```sql
SELECT *
FROM sys.dm_operation_status
WHERE major_resource_id = 'mydb'
ORDER BY start_time DESC;
```

## Related content

- [Geo-Replication Dynamic Management Views and Functions (Azure SQL Database)](geo-replication-dynamic-management-views-and-functions-azure-sql-database.md)
- [sys.dm_geo_replication_link_status (Azure SQL Database)](sys-dm-geo-replication-link-status-azure-sql-database.md)
- [sys.geo_replication_links (Azure SQL Database)](sys-geo-replication-links-azure-sql-database.md)
- [ALTER DATABASE (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql.md)
