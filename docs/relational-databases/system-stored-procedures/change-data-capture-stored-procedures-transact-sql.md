---
title: "Change Data Capture stored procedures (Transact-SQL)"
description: "Change Data Capture stored procedures (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/22/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
helpviewer_keywords:
  - "system stored procedures [SQL Server], change data capture"
  - "change data capture [SQL Server], stored procedures"
dev_langs:
  - "TSQL"
---
# Change Data Capture stored procedures (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Change data capture makes available in a convenient relational format the historical record of Data Manipulation Language (DML) activity that occurred on enabled tables. The following stored procedures are used to configure change data capture, manage the change data capture Agent jobs, and supply current metadata to change data consumers.

:::row:::
    :::column:::
        [sys.sp_cdc_add_job](sys-sp-cdc-add-job-transact-sql.md)

        [sys.sp_cdc_change_job](sys-sp-cdc-change-job-transact-sql.md)

        [sys.sp_cdc_cleanup_change_table](sys-sp-cdc-cleanup-change-table-transact-sql.md)

        [sys.sp_cdc_disable_db](sys-sp-cdc-disable-db-transact-sql.md)

        [sys.sp_cdc_disable_table](sys-sp-cdc-disable-table-transact-sql.md)

        [sys.sp_cdc_drop_job](sys-sp-cdc-drop-job-transact-sql.md)

        [sys.sp_cdc_enable_db](sys-sp-cdc-enable-db-transact-sql.md)

        [sys.sp_cdc_enable_table](sys-sp-cdc-enable-table-transact-sql.md)
    :::column-end:::
    :::column:::
        [sys.sp_cdc_generate_wrapper_function](sys-sp-cdc-generate-wrapper-function-transact-sql.md)

        [sys.sp_cdc_get_captured_columns](sys-sp-cdc-get-captured-columns-transact-sql.md)

        [sys.sp_cdc_get_ddl_history](sys-sp-cdc-get-ddl-history-transact-sql.md)

        [sys.sp_cdc_help_change_data_capture](sys-sp-cdc-help-change-data-capture-transact-sql.md)

        [sys.sp_cdc_help_jobs](sys-sp-cdc-help-jobs-transact-sql.md)

        [sys.sp_cdc_scan](sys-sp-cdc-scan-transact-sql.md)

        [sys.sp_cdc_start_job](sys-sp-cdc-start-job-transact-sql.md)

        [sys.sp_cdc_stop_job](sys-sp-cdc-stop-job-transact-sql.md)
    :::column-end:::
:::row-end:::

## Related content

- [Change Data Capture Tables (Transact-SQL)](../system-tables/change-data-capture-tables-transact-sql.md)
