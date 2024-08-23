---
title: "Data collector stored procedures (Transact-SQL)"
description: "Data collector stored procedures (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/21/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
helpviewer_keywords:
  - "stored procedures [SQL Server], data collector"
  - "system stored procedures [SQL Server], data collector"
  - "data collector [SQL Server], stored procedures"
dev_langs:
  - "TSQL"
---
# Data collector stored procedures (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

SQL Server supports the following system stored procedures that are used to work with the data collector and the following components: collection sets, collection items, and collection types.

> [!IMPORTANT]  
> Unlike regular stored procedures, the parameters for data collector stored procedures are strictly typed and don't support automatic data type conversion. If these parameters aren't called with the correct input parameter data types, as specified in the argument description, the stored procedure returns an error.

:::row:::
    :::column:::
        [sp_syscollector_create_collection_item](sp-syscollector-create-collection-item-transact-sql.md)

        [sp_syscollector_create_collection_set](sp-syscollector-create-collection-set-transact-sql.md)

        [sp_syscollector_create_collector_type](sp-syscollector-create-collector-type-transact-sql.md)

        [sp_syscollector_delete_collection_item](sp-syscollector-delete-collection-item-transact-sql.md)

        [sp_syscollector_delete_collection_set](sp-syscollector-delete-collection-set-transact-sql.md)

        [sp_syscollector_delete_collector_type](sp-syscollector-delete-collector-type-transact-sql.md)

        [sp_syscollector_delete_execution_log_tree](sp-syscollector-delete-execution-log-tree-transact-sql.md)

        [sp_syscollector_disable_collector](sp-syscollector-disable-collector-transact-sql.md)

        [sp_syscollector_enable_collector](sp-syscollector-enable-collector-transact-sql.md)

        [sp_syscollector_set_cache_directory](sp-syscollector-set-cache-directory-transact-sql.md)
    :::column-end:::
    :::column:::
        [sp_syscollector_set_cache_window](sp-syscollector-set-cache-window-transact-sql.md)

        [sp_syscollector_set_warehouse_database_name](sp-syscollector-set-warehouse-database-name-transact-sql.md)

        [sp_syscollector_set_warehouse_instance_name](sp-syscollector-set-warehouse-instance-name-transact-sql.md)

        [sp_syscollector_start_collection_set](sp-syscollector-start-collection-set-transact-sql.md)

        [sp_syscollector_stop_collection_set](sp-syscollector-stop-collection-set-transact-sql.md)

        [sp_syscollector_run_collection_set](sp-syscollector-run-collection-set-transact-sql.md)

        [sp_syscollector_update_collection_item](sp-syscollector-update-collection-item-transact-sql.md)

        [sp_syscollector_update_collection_set](sp-syscollector-update-collection-set-transact-sql.md)

        [sp_syscollector_update_collector_type](sp-syscollector-update-collector-type-transact-sql.md)

        [sp_syscollector_upload_collection_set](sp-syscollector-upload-collection-set-transact-sql.md)
    :::column-end:::
:::row-end:::

The following stored procedures are for internal use only:

- `sp_syscollector_get_warehouse_connection_string`
- `sp_syscollector_set_warehouse_connection_password`
- `sp_syscollector_set_warehouse_connection_user`
- `sp_syscollector_event_oncollectionbegin`
- `sp_syscollector_event_oncollectionend`
- `sp_syscollector_event_onpackagebegin`
- `sp_syscollector_event_onpackageend`
- `sp_syscollector_event_onpackageupdate`
- `sp_syscollector_event_onerror`
- `sp_syscollector_event_onstatsupdate`

## Related content

- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
