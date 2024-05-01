---
title: "Manage data collection"
description: Manage different aspects of data collection, such as enabling or disabling data collection, changing a collection set configuration, or viewing data in the management data warehouse.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 12/28/2023
ms.service: sql
ms.subservice: supportability
ms.topic: conceptual
helpviewer_keywords:
  - "data collection [SQL Server]"
  - "data collector [SQL Server], Transact-SQL"
  - "data collector [SQL Server], SQL Server Management Studio"
keywords: Data Collection
---
# Manage data collection

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Use [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE [tsql](../../includes/tsql-md.md)] stored procedures and functions to manage different aspects of data collection, such as enabling or disabling data collection, changing a collection set configuration, or viewing data in the management data warehouse.

## Manage data collection using SSMS

Perform the following data collector-related tasks by using Object Explorer in [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)]:

- [Configure the management data warehouse (SQL Server Management Studio)](configure-the-management-data-warehouse-sql-server-management-studio.md)
- [Configure properties of a data collector](configure-properties-of-a-data-collector.md)
- [Enable or disable data collection](enable-or-disable-data-collection.md)
- [Start or stop a collection set](start-or-stop-a-collection-set.md)
- [Use SQL Server Profiler to create a SQL Trace collection set](use-sql-server-profiler-to-create-a-sql-trace-collection-set.md)
- [View collection set logs (SQL Server Management Studio)](view-collection-set-logs-sql-server-management-studio.md)
- [View or change collection set schedules (SQL Server Management Studio)](view-or-change-collection-set-schedules-sql-server-management-studio.md)
- [View a collection set report (SQL Server Management Studio)](view-a-collection-set-report-sql-server-management-studio.md)

## Manage data collection using Transact-SQL

The data collector provides an extensive collection of stored procedures that you can use to perform any data-collector related task. For example, by using [!INCLUDE [tsql](../../includes/tsql-md.md)], you can perform the following tasks:

- [Configure Data Collection Parameters (Transact-SQL)](configure-data-collection-parameters-transact-sql.md)
- [Enable or disable data collection](enable-or-disable-data-collection.md)
- [Start or stop a collection set](start-or-stop-a-collection-set.md)
- [Create custom collection set - Generic T-SQL Query collector type](create-custom-collection-set-generic-t-sql-query-collector-type.md)
- [Add a Collection Item to a Collection Set (Transact-SQL)](add-a-collection-item-to-a-collection-set-transact-sql.md)

In addition, there are functions and views that you can use to get configuration data for the `msdb` and management data warehouse databases, execution log data, and data that is stored in the management data warehouse.

You can use the stored procedures, functions, and views that are provided to create your own end-to-end data collection scenarios.

> [!IMPORTANT]  
> Unlike regular stored procedures, the data collector stored procedures use strictly typed parameters and don't support automatic data type conversion. If these parameters aren't called with the correct input parameter data types, as specified in the argument description, the stored procedure returns an error.

Use [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] to create and execute the provided code samples. For more information, see [Object Explorer](../../ssms/object/object-explorer.md). As an alternative, you can create the query in any editor and save it in a text file that has a `.sql` file name extension. You can execute the query from the Windows command prompt using the **sqlcmd** utility. For more information, see [sqlcmd - use the utility](../../tools/sqlcmd/sqlcmd-use-utility.md).

### Stored procedures and views

The following section describes stored procedures and views you use to work with data collection in the [!INCLUDE [ssde-md](../../includes/ssde-md.md)].

#### Work with the data collector

The following table describes the stored procedures that you can use to work with the data collector.

| Procedure name | Description |
| --- | --- |
| [sp_syscollector_enable_collector](../system-stored-procedures/sp-syscollector-enable-collector-transact-sql.md) | Enable the data collector. |
| [sp_syscollector_disable_collector](../system-stored-procedures/sp-syscollector-disable-collector-transact-sql.md) | Disable the data collector. |

#### Work with collection sets

The following table describes the stored procedures that you can use to work with collection sets.

| Procedure name | Description |
| --- | --- |
| [sp_syscollector_run_collection_set](../system-stored-procedures/sp-syscollector-run-collection-set-transact-sql.md) | Run a collection set on demand. |
| [sp_syscollector_start_collection_set](../system-stored-procedures/sp-syscollector-start-collection-set-transact-sql.md) | Start a collection set. |
| [sp_syscollector_stop_collection_set](../system-stored-procedures/sp-syscollector-stop-collection-set-transact-sql.md) | Stop a collection set. |
| [sp_syscollector_create_collection_set](../system-stored-procedures/sp-syscollector-create-collection-set-transact-sql.md) | Create a collection set. |
| [sp_syscollector_delete_collection_set](../system-stored-procedures/sp-syscollector-delete-collection-set-transact-sql.md) | Delete a collection set. |
| [sp_syscollector_update_collection_set](../system-stored-procedures/sp-syscollector-update-collection-set-transact-sql.md) | Change a collection set configuration. |
| [sp_syscollector_upload_collection_set](../system-stored-procedures/sp-syscollector-upload-collection-set-transact-sql.md) | Upload collection set data to the management data warehouse. This is effectively an on-demand upload. |

#### Work with collection items

The following table describes the stored procedures that you can use to work with collection items.

| Procedure name | Description |
| --- | --- |
| [sp_syscollector_create_collection_item](../system-stored-procedures/sp-syscollector-create-collection-item-transact-sql.md) | Create a collection item. |
| [sp_syscollector_delete_collection_item](../system-stored-procedures/sp-syscollector-delete-collection-item-transact-sql.md) | Delete a collection item. |
| [sp_syscollector_update_collection_item](../system-stored-procedures/sp-syscollector-update-collection-item-transact-sql.md) | Update a collection item. |

#### Work with collector types

The following table describes the stored procedures that you can use to work with collector types.

| Procedure name | Description |
| --- | --- |
| [sp_syscollector_create_collector_type](../system-stored-procedures/sp-syscollector-create-collector-type-transact-sql.md) | Create a collector type. |
| [sp_syscollector_update_collector_type](../system-stored-procedures/sp-syscollector-update-collector-type-transact-sql.md) | Update a collector type. |
| [sp_syscollector_delete_collector_type](../system-stored-procedures/sp-syscollector-delete-collector-type-transact-sql.md) | Delete a collector type. |

#### Get configuration information

The following table describes the views that you can use for getting configuration information and execution log data.

| View name | Description |
| --- | --- |
| [syscollector_config_store](../system-catalog-views/syscollector-config-store-transact-sql.md) | Get data collector configuration. |
| [syscollector_collection_items](../system-catalog-views/syscollector-collection-items-transact-sql.md) | Get collection item information. |
| [syscollector_collection_sets](../system-catalog-views/syscollector-collection-sets-transact-sql.md) | Get collection set information. |
| [syscollector_collector_types](../system-catalog-views/syscollector-collector-types-transact-sql.md) | Get collector type information. |
| [syscollector_execution_log](../system-catalog-views/syscollector-execution-log-transact-sql.md) | Get information about collection set and package execution. |
| [syscollector_execution_stats](../system-catalog-views/syscollector-execution-stats-transact-sql.md) | Get information about task execution. |
| [syscollector_execution_log_full](../system-catalog-views/syscollector-execution-log-full-transact-sql.md) | Get information when the execution log is full. |

#### Configure access to the management data warehouse

The following table describes the stored procedures that you can use to configure access to the management data warehouse.

| Procedure name | Description |
| --- | --- |
| [sp_syscollector_set_warehouse_database_name](../system-stored-procedures/sp-syscollector-set-warehouse-database-name-transact-sql.md) | Specify the database name defined in the connection string for the management data warehouse. |
| [sp_syscollector_set_warehouse_instance_name](../system-stored-procedures/sp-syscollector-set-warehouse-instance-name-transact-sql.md) | Specify the instance defined in the connection string for the management data warehouse. |

#### Configure the management data warehouse

The following table describes the stored procedures that you can use to work with the management data warehouse configuration.

| Procedure name | Description |
| --- | --- |
| [core.sp_create_snapshot](../system-stored-procedures/core-sp-create-snapshot-transact-sql.md) | Create a collection snapshot in the management data warehouse. |
| [core.sp_update_data_source](../system-stored-procedures/core-sp-update-data-source-transact-sql.md) | Update the data source for data collection. |
| [core.sp_add_collector_type](../system-stored-procedures/core-sp-add-collector-type-transact-sql.md) | Add a collector type to the management data warehouse. |
| [core.sp_remove_collector_type](../system-stored-procedures/core-sp-remove-collector-type-transact-sql.md) | Remove a collector type from the management data warehouse. |
| [core.sp_purge_data](../system-stored-procedures/core-sp-purge-data-transact-sql.md) | Delete data from the management data warehouse. |

#### Work with upload packages

The following table describes the stored procedures that you can use to work with upload packages.

| Procedure name | Description |
| --- | --- |
| [sp_syscollector_set_cache_window](../system-stored-procedures/sp-syscollector-set-cache-window-transact-sql.md) | Configure the number of data upload retries. |
| [sp_syscollector_set_cache_directory](../system-stored-procedures/sp-syscollector-set-cache-directory-transact-sql.md) | Specify temporary storage for data between upload retries. |

#### Work with the data collection execution log

The following table describes the stored procedures that you can use to work with the data collection execution log.

| Procedure name | Description |
| --- | --- |
| [sp_syscollector_delete_execution_log_tree](../system-stored-procedures/sp-syscollector-delete-execution-log-tree-transact-sql.md) | Delete collection set entries from the execution log. |

### Functions

The following table describes the functions that you can use to obtain execution and trace information.

| Function name | Description |
| --- | --- |
| [fn_syscollector_get_execution_details](../system-functions/fn-syscollector-get-execution-details-transact-sql.md) | Get [!INCLUDE [ssIS](../../includes/ssis-md.md)] execution log data for a specific package. |
| [fn_syscollector_get_execution_stats](../system-functions/fn-syscollector-get-execution-stats-transact-sql.md) | Get execution statistics for a collection set or package. This information includes errors that are logged. |
| [snapshots.fn_trace_getdata](../system-functions/snapshots-fn-trace-getdata-transact-sql.md) | Get the events that are logged when the Generic SQL Trace collector type is used to collect data. |

## Related content

- [Execute a stored procedure](../stored-procedures/execute-a-stored-procedure.md)
- [What is SQL Server Management Studio (SSMS)?](../../ssms/sql-server-management-studio-ssms.md)
- [Data collection](data-collection.md)
