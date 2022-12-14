---
title: "Execution Related Dynamic Management Views and Functions (Transact-SQL)"
description: Execution Related Dynamic Management Views and Functions (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: 05/28/2021
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
helpviewer_keywords:
  - "dynamic management objects [SQL Server], execution"
  - "execution-related dynamic management objects [SQL Server]"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-linux-2017||>=sql-server-2016||>=aps-pdw-2016||=azure-sqldw-latest"
---
# Execution Related Dynamic Management Views and Functions (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

::: moniker range=">= sql-server-linux-2017 || >= sql-server-2016"

  This section contains the following dynamic management objects:  
  
:::row:::
    :::column:::
        [sys.dm_exec_background_job_queue](../../relational-databases/system-dynamic-management-views/sys-dm-exec-background-job-queue-transact-sql.md)

        [sys.dm_exec_cached_plan_dependent_objects](../../relational-databases/system-dynamic-management-views/sys-dm-exec-cached-plan-dependent-objects-transact-sql.md)

        [sys.dm_exec_compute_node_errors](../../relational-databases/system-dynamic-management-views/sys-dm-exec-compute-node-errors-transact-sql.md)

        [sys.dm_exec_compute_nodes](../../relational-databases/system-dynamic-management-views/sys-dm-exec-compute-nodes-transact-sql.md)

        [sys.dm_exec_cursors](../../relational-databases/system-dynamic-management-views/sys-dm-exec-cursors-transact-sql.md)

        [sys.dm_exec_describe_first_result_set_for_object](../../relational-databases/system-dynamic-management-views/sys-dm-exec-describe-first-result-set-for-object-transact-sql.md)

        [sys.dm_exec_distributed_requests](../../relational-databases/system-dynamic-management-views/sys-dm-exec-distributed-requests-transact-sql.md)

        [sys.dm_exec_dms_services](../../relational-databases/system-dynamic-management-views/sys-dm-exec-dms-services-transact-sql.md)

        [sys.dm_exec_external_operations](../../relational-databases/system-dynamic-management-views/sys-dm-exec-external-operations-transact-sql.md)

        [sys.dm_exec_function_stats](../../relational-databases/system-dynamic-management-views/sys-dm-exec-function-stats-transact-sql.md)

        [sys.dm_exec_plan_attributes](../../relational-databases/system-dynamic-management-views/sys-dm-exec-plan-attributes-transact-sql.md)

        [sys.dm_exec_query_memory_grants](../../relational-databases/system-dynamic-management-views/sys-dm-exec-query-memory-grants-transact-sql.md)

        [sys.dm_exec_query_optimizer_memory_gateways](../../relational-databases/system-dynamic-management-views/sys-dm-exec-query-optimizer-memory-gateways.md)

        [sys.dm_exec_query_parallel_workers](../../relational-databases/system-dynamic-management-views/sys-dm-exec-query-parallel-workers-transact-sql.md)

        [sys.dm_exec_query_resource_semaphores](../../relational-databases/system-dynamic-management-views/sys-dm-exec-query-resource-semaphores-transact-sql.md)

        [sys.dm_exec_query_stats](../../relational-databases/system-dynamic-management-views/sys-dm-exec-query-stats-transact-sql.md)

        [sys.dm_exec_requests](../../relational-databases/system-dynamic-management-views/sys-dm-exec-requests-transact-sql.md)

        [sys.dm_exec_sessions](../../relational-databases/system-dynamic-management-views/sys-dm-exec-sessions-transact-sql.md)

        [sys.dm_exec_text_query_plan](../../relational-databases/system-dynamic-management-views/sys-dm-exec-text-query-plan-transact-sql.md)

        [sys.dm_exec_valid_use_hints](../../relational-databases/system-dynamic-management-views/sys-dm-exec-valid-use-hints-transact-sql.md)

        [sys.dm_external_script_execution_stats](../../relational-databases/system-dynamic-management-views/sys-dm-external-script-execution-stats.md)
    :::column-end:::
    :::column:::
        [sys.dm_exec_background_job_queue_stats](../../relational-databases/system-dynamic-management-views/sys-dm-exec-background-job-queue-stats-transact-sql.md)

        [sys.dm_exec_cached_plans](../../relational-databases/system-dynamic-management-views/sys-dm-exec-cached-plans-transact-sql.md)

        [sys.dm_exec_compute_node_status](../../relational-databases/system-dynamic-management-views/sys-dm-exec-compute-node-status-transact-sql.md)

        [sys.dm_exec_connections](../../relational-databases/system-dynamic-management-views/sys-dm-exec-connections-transact-sql.md)

        [sys.dm_exec_describe_first_result_set](../../relational-databases/system-dynamic-management-views/sys-dm-exec-describe-first-result-set-transact-sql.md)

        [sys.dm_exec_distributed_request_steps](../../relational-databases/system-dynamic-management-views/sys-dm-exec-distributed-request-steps-transact-sql.md)

        [sys.dm_exec_distributed_sql_requests](../../relational-databases/system-dynamic-management-views/sys-dm-exec-distributed-sql-requests-transact-sql.md)

        [sys.dm_exec_dms_workers](../../relational-databases/system-dynamic-management-views/sys-dm-exec-dms-workers-transact-sql.md)

        [sys.dm_exec_external_work](../../relational-databases/system-dynamic-management-views/sys-dm-exec-external-work-transact-sql.md)

        [sys.dm_exec_input_buffer](../../relational-databases/system-dynamic-management-views/sys-dm-exec-input-buffer-transact-sql.md)

        [sys.dm_exec_procedure_stats](../../relational-databases/system-dynamic-management-views/sys-dm-exec-procedure-stats-transact-sql.md)

        [sys.dm_exec_query_optimizer_info](../../relational-databases/system-dynamic-management-views/sys-dm-exec-query-optimizer-info-transact-sql.md)

        [sys.dm_exec_query_plan](../../relational-databases/system-dynamic-management-views/sys-dm-exec-query-plan-transact-sql.md)

        [sys.dm_exec_query_profiles](../../relational-databases/system-dynamic-management-views/sys-dm-exec-query-profiles-transact-sql.md)

        [sys.dm_exec_query_statistics_xml](../../relational-databases/system-dynamic-management-views/sys-dm-exec-query-statistics-xml-transact-sql.md)

        [sys.dm_exec_query_plan_stats](../../relational-databases/system-dynamic-management-views/sys-dm-exec-query-plan-stats-transact-sql.md)

        [sys.dm_exec_session_wait_stats](../../relational-databases/system-dynamic-management-views/sys-dm-exec-session-wait-stats-transact-sql.md)

        [sys.dm_exec_sql_text](../../relational-databases/system-dynamic-management-views/sys-dm-exec-sql-text-transact-sql.md)

        [sys.dm_exec_trigger_stats](../../relational-databases/system-dynamic-management-views/sys-dm-exec-trigger-stats-transact-sql.md)

        [sys.dm_exec_xml_handles](../../relational-databases/system-dynamic-management-views/sys-dm-exec-xml-handles-transact-sql.md)

        [sys.dm_external_script_requests](../../relational-databases/system-dynamic-management-views/sys-dm-external-script-requests.md)
    :::column-end:::
:::row-end:::

> [!NOTE]  
>  The **sys.dm_exec_query_transformation_stats** dynamic management view is identified for informational purposes only. Not supported. Future compatibility is not guaranteed.  

::: moniker-end
::: moniker range=">= aps-pdw-2016 || = azure-sqldw-latest"
  
This section contains Azure Synapse Analytics or Parallel Data Warehouse dynamic management views (DMVs):

:::row:::
    :::column:::
      [sys.dm_pdw_dms_cores](../../relational-databases/system-dynamic-management-views/sys-dm-pdw-dms-cores-transact-sql.md)   

      [sys.dm_pdw_dms_external_work](../../relational-databases/system-dynamic-management-views/sys-dm-pdw-dms-external-work-transact-sql.md)   

      [sys.dm_pdw_dms_workers](../../relational-databases/system-dynamic-management-views/sys-dm-pdw-dms-workers-transact-sql.md)   

      [sys.dm_pdw_errors](../../relational-databases/system-dynamic-management-views/sys-dm-pdw-errors-transact-sql.md)

      [sys.dm_pdw_exec_connections](../../relational-databases/system-dynamic-management-views/sys-dm-pdw-exec-connections-transact-sql.md)

      [sys.dm_pdw_exec_requests](../../relational-databases/system-dynamic-management-views/sys-dm-pdw-exec-requests-transact-sql.md)  

      [sys.dm_pdw_exec_sessions](../../relational-databases/system-dynamic-management-views/sys-dm-pdw-exec-sessions-transact-sql.md)

      [sys.dm_pdw_nodes_exec_query_plan](../../relational-databases/system-dynamic-management-views/sys-dm-pdw-nodes-exec-query-plan-transact-sql.md)

      [sys.dm_pdw_nodes_exec_query_profiles](../../relational-databases/system-dynamic-management-views/sys-dm-pdw-nodes-exec-query-profiles-transact-sql.md)

      [sys.dm_pdw_nodes_exec_query_statistics_xml](../../relational-databases/system-dynamic-management-views/sys-dm-pdw-nodes-exec-query-statistics-xml-transact-sql.md)

      [sys.dm_pdw_nodes_exec_sql_text](../../relational-databases/system-dynamic-management-views/sys-dm-pdw-nodes-exec-sql-text-transact-sql.md)

      [sys.dm_pdw_nodes_exec_text_query_plan](../../relational-databases/system-dynamic-management-views/sys-dm-pdw-nodes-exec-text-query-plan-transact-sql.md)

      [sys.dm_pdw_hadoop_operations](../../relational-databases/system-dynamic-management-views/sys-dm-pdw-hadoop-operations-transact-sql.md)

      [sys.dm_pdw_query_stats_xe](../../relational-databases/system-dynamic-management-views/sys-dm-pdw-query-stats-xe-transact-sql.md)

      [sys.dm_pdw_query_stats_xe_file](../../relational-databases/system-dynamic-management-views/sys-dm-pdw-query-stats-xe-file-transact-sql.md)

      [sys.dm_pdw_request_steps](../../relational-databases/system-dynamic-management-views/sys-dm-pdw-request-steps-transact-sql.md)

      [sys.dm_pdw_sql_requests](../../relational-databases/system-dynamic-management-views/sys-dm-pdw-sql-requests-transact-sql.md)


    :::column-end:::
:::row-end:::

::: moniker-end

