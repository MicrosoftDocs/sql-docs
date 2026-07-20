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
monikerRange: ">=sql-server-linux-2017 || >=sql-server-2017 || >=aps-pdw-2016 || =azure-sqldw-latest"
---
# Execution Related Dynamic Management Views and Functions (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

::: moniker range=">=sql-server-linux-2017 || >=sql-server-2017"

  This section contains the following dynamic management objects:  
  
:::row:::
    :::column:::
        [sys.dm_exec_background_job_queue](sys-dm-exec-background-job-queue-transact-sql.md)

        [sys.dm_exec_cached_plan_dependent_objects](sys-dm-exec-cached-plan-dependent-objects-transact-sql.md)

        [sys.dm_exec_compute_node_errors](sys-dm-exec-compute-node-errors-transact-sql.md)

        [sys.dm_exec_compute_nodes](sys-dm-exec-compute-nodes-transact-sql.md)

        [sys.dm_exec_cursors](sys-dm-exec-cursors-transact-sql.md)

        [sys.dm_exec_describe_first_result_set_for_object](sys-dm-exec-describe-first-result-set-for-object-transact-sql.md)

        [sys.dm_exec_distributed_requests](sys-dm-exec-distributed-requests-transact-sql.md)

        [sys.dm_exec_dms_services](sys-dm-exec-dms-services-transact-sql.md)

        [sys.dm_exec_external_operations](sys-dm-exec-external-operations-transact-sql.md)

        [sys.dm_exec_function_stats](sys-dm-exec-function-stats-transact-sql.md)

        [sys.dm_exec_plan_attributes](sys-dm-exec-plan-attributes-transact-sql.md)

        [sys.dm_exec_query_memory_grants](sys-dm-exec-query-memory-grants-transact-sql.md)

        [sys.dm_exec_query_optimizer_memory_gateways](sys-dm-exec-query-optimizer-memory-gateways.md)

        [sys.dm_exec_query_parallel_workers](sys-dm-exec-query-parallel-workers-transact-sql.md)

        [sys.dm_exec_query_resource_semaphores](sys-dm-exec-query-resource-semaphores-transact-sql.md)

        [sys.dm_exec_query_stats](sys-dm-exec-query-stats-transact-sql.md)

        [sys.dm_exec_requests](sys-dm-exec-requests-transact-sql.md)

        [sys.dm_exec_sessions](sys-dm-exec-sessions-transact-sql.md)

        [sys.dm_exec_text_query_plan](sys-dm-exec-text-query-plan-transact-sql.md)

        [sys.dm_exec_valid_use_hints](sys-dm-exec-valid-use-hints-transact-sql.md)

        [sys.dm_external_script_execution_stats](sys-dm-external-script-execution-stats.md)
    :::column-end:::
    :::column:::
        [sys.dm_exec_background_job_queue_stats](sys-dm-exec-background-job-queue-stats-transact-sql.md)

        [sys.dm_exec_cached_plans](sys-dm-exec-cached-plans-transact-sql.md)

        [sys.dm_exec_compute_node_status](sys-dm-exec-compute-node-status-transact-sql.md)

        [sys.dm_exec_connections](sys-dm-exec-connections-transact-sql.md)

        [sys.dm_exec_describe_first_result_set](sys-dm-exec-describe-first-result-set-transact-sql.md)

        [sys.dm_exec_distributed_request_steps](sys-dm-exec-distributed-request-steps-transact-sql.md)

        [sys.dm_exec_distributed_sql_requests](sys-dm-exec-distributed-sql-requests-transact-sql.md)

        [sys.dm_exec_dms_workers](sys-dm-exec-dms-workers-transact-sql.md)

        [sys.dm_exec_external_work](sys-dm-exec-external-work-transact-sql.md)

        [sys.dm_exec_input_buffer](sys-dm-exec-input-buffer-transact-sql.md)

        [sys.dm_exec_procedure_stats](sys-dm-exec-procedure-stats-transact-sql.md)

        [sys.dm_exec_query_optimizer_info](sys-dm-exec-query-optimizer-info-transact-sql.md)

        [sys.dm_exec_query_plan](sys-dm-exec-query-plan-transact-sql.md)

        [sys.dm_exec_query_profiles](sys-dm-exec-query-profiles-transact-sql.md)

        [sys.dm_exec_query_statistics_xml](sys-dm-exec-query-statistics-xml-transact-sql.md)

        [sys.dm_exec_query_plan_stats](sys-dm-exec-query-plan-stats-transact-sql.md)

        [sys.dm_exec_session_wait_stats](sys-dm-exec-session-wait-stats-transact-sql.md)

        [sys.dm_exec_sql_text](sys-dm-exec-sql-text-transact-sql.md)

        [sys.dm_exec_trigger_stats](sys-dm-exec-trigger-stats-transact-sql.md)

        [sys.dm_exec_xml_handles](sys-dm-exec-xml-handles-transact-sql.md)

        [sys.dm_external_script_requests](sys-dm-external-script-requests.md)
    :::column-end:::
:::row-end:::

> [!NOTE]  
>  The **sys.dm_exec_query_transformation_stats** dynamic management view is identified for informational purposes only. Not supported. Future compatibility is not guaranteed.  

::: moniker-end
::: moniker range=">= aps-pdw-2016 || = azure-sqldw-latest"
  
This section contains Azure Synapse Analytics or Parallel Data Warehouse dynamic management views (DMVs):

:::row:::
    :::column:::
      [sys.dm_pdw_dms_cores](sys-dm-pdw-dms-cores-transact-sql.md)   

      [sys.dm_pdw_dms_external_work](sys-dm-pdw-dms-external-work-transact-sql.md)   

      [sys.dm_pdw_dms_workers](sys-dm-pdw-dms-workers-transact-sql.md)   

      [sys.dm_pdw_errors](sys-dm-pdw-errors-transact-sql.md)

      [sys.dm_pdw_exec_connections](sys-dm-pdw-exec-connections-transact-sql.md)

      [sys.dm_pdw_exec_requests](sys-dm-pdw-exec-requests-transact-sql.md)  

      [sys.dm_pdw_exec_sessions](sys-dm-pdw-exec-sessions-transact-sql.md)

      [sys.dm_pdw_nodes_exec_query_plan](sys-dm-pdw-nodes-exec-query-plan-transact-sql.md)

      [sys.dm_pdw_nodes_exec_query_profiles](sys-dm-pdw-nodes-exec-query-profiles-transact-sql.md)

      [sys.dm_pdw_nodes_exec_query_statistics_xml](sys-dm-pdw-nodes-exec-query-statistics-xml-transact-sql.md)

      [sys.dm_pdw_nodes_exec_sql_text](sys-dm-pdw-nodes-exec-sql-text-transact-sql.md)

      [sys.dm_pdw_nodes_exec_text_query_plan](sys-dm-pdw-nodes-exec-text-query-plan-transact-sql.md)

      [sys.dm_pdw_hadoop_operations](sys-dm-pdw-hadoop-operations-transact-sql.md)

      [sys.dm_pdw_query_stats_xe](sys-dm-pdw-query-stats-xe-transact-sql.md)

      [sys.dm_pdw_query_stats_xe_file](sys-dm-pdw-query-stats-xe-file-transact-sql.md)

      [sys.dm_pdw_request_steps](sys-dm-pdw-request-steps-transact-sql.md)

      [sys.dm_pdw_sql_requests](sys-dm-pdw-sql-requests-transact-sql.md)


    :::column-end:::
:::row-end:::

::: moniker-end
