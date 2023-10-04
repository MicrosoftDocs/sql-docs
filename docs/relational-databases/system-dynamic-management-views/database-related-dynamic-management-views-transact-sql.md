---
title: "Database related dynamic management views (Transact-SQL)"
description: Database Related Dynamic Management Views (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: 06/23/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
helpviewer_keywords:
  - "database dynamic management objects [SQL Server]"
  - "dynamic management objects [SQL Server], database"
dev_langs:
  - "TSQL"
---
# Database related dynamic management views (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This section describes the following dynamic management objects in [!INCLUDE [ssNoVersion_md](../../includes/ssnoversion-md.md)] and sometimes in SQL Database.

:::row:::
    :::column:::
        [sys.dm_db_file_space_usage](sys-dm-db-file-space-usage-transact-sql.md)

        [sys.dm_db_log_info](sys-dm-db-log-info-transact-sql.md)

        [sys.dm_db_log_stats](sys-dm-db-log-stats-transact-sql.md)

        [sys.dm_db_partition_stats](sys-dm-db-partition-stats-transact-sql.md)

        [sys.dm_db_session_space_usage](sys-dm-db-session-space-usage-transact-sql.md)

        [sys.dm_db_uncontained_entities](sys-dm-db-uncontained-entities-transact-sql.md)
    :::column-end:::
    :::column:::
        [sys.dm_db_fts_index_physical_stats](sys-dm-db-fts-index-physical-stats-transact-sql.md)

        [sys.dm_db_log_space_usage](sys-dm-db-log-space-usage-transact-sql.md)

        [sys.dm_db_page_info](sys-dm-db-page-info-transact-sql.md)

        [sys.dm_db_persisted_sku_features](sys-dm-db-persisted-sku-features-transact-sql.md)

        [sys.dm_db_task_space_usage](sys-dm-db-task-space-usage-transact-sql.md)
    :::column-end:::
:::row-end:::

DMVs unique to SQL Database or Azure Synapse Analytics.

:::row:::
    :::column:::
        [sys.dm_db_wait_stats (Azure SQL Database)](sys-dm-db-wait-stats-azure-sql-database.md)

        [sys.dm_db_resource_stats (Azure SQL Database)](sys-dm-db-resource-stats-azure-sql-database.md)

        [sys.dm_operation_status (Azure SQL Database)](sys-dm-operation-status-azure-sql-database.md)
    :::column-end:::
    :::column:::
        [sys.dm_database_copies (Azure SQL Database)](sys-dm-database-copies-azure-sql-database.md)

        [sys.dm_db_objects_impacted_on_version_change (Azure SQL Database)](sys-dm-db-objects-impacted-on-version-change-azure-sql-database.md)
    :::column-end:::
:::row-end:::

## See also

- [System dynamic management views](system-dynamic-management-views.md)
