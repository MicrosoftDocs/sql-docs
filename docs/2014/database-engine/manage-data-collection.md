---
title: "Manage Data Collection | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "dbe-cross-instance"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "data collection [SQL Server]"
  - "data collector [SQL Server], Transact-SQL"
  - "data collector [SQL Server], SQL Server Management Studio"
ms.assetid: bc137daa-9f37-4c01-9766-8b7350c75af8
caps.latest.revision: 25
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# Manage Data Collection
  You can use [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)] stored procedures and functions to manage different aspects of data collection, such as enabling or disabling data collection, changing a collection set configuration, or viewing data in the management data warehouse.  
  
## Manage Data Collection by Using SQL Server Management Studio  
 You can perform the following data collector-related tasks by using Object Explorer in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]:  
  
-   [Configure the Management Data Warehouse &#40;SQL Server Management Studio&#41;](../../2014/database-engine/configure-the-management-data-warehouse-sql-server-management-studio.md)  
  
-   [Configure Properties of a Data Collector](../../2014/database-engine/configure-properties-of-a-data-collector.md)  
  
-   [Enable or Disable Data Collection](../../2014/database-engine/enable-or-disable-data-collection.md)  
  
-   [Start or Stop a Collection Set](../../2014/database-engine/start-or-stop-a-collection-set.md)  
  
-   [Use SQL Server Profiler to Create a SQL Trace Collection Set &#40;SQL Server Management Studio&#41;](../../2014/database-engine/use-sql-server-profiler-to-create-a-sql-trace-collection-set.md)  
  
-   [View Collection Set Logs &#40;SQL Server Management Studio&#41;](../../2014/database-engine/view-collection-set-logs-sql-server-management-studio.md)  
  
-   [View or Change Collection Set Schedules &#40;SQL Server Management Studio&#41;](../../2014/database-engine/view-or-change-collection-set-schedules-sql-server-management-studio.md)  
  
-   [View a Collection Set Report &#40;SQL Server Management Studio&#41;](../../2014/database-engine/view-a-collection-set-report-sql-server-management-studio.md)  
  
## Manage Data Collection by Using Transact-SQL  
 The data collector provides an extensive collection of stored procedures that you can use to perform any data-collector related task. For example, by using [!INCLUDE[tsql](../../includes/tsql-md.md)], you can perform the following tasks:  
  
-   [Configure Data Collection Parameters &#40;Transact-SQL&#41;](../../2014/database-engine/configure-data-collection-parameters-transact-sql.md)  
  
-   [Enable or Disable Data Collection](../../2014/database-engine/enable-or-disable-data-collection.md)  
  
-   [Start or Stop a Collection Set](../../2014/database-engine/start-or-stop-a-collection-set.md)  
  
-   [Create a Custom Collection Set That Uses the Generic T-SQL Query Collector Type &#40;Transact-SQL&#41;](../../2014/database-engine/create-custom-collection-set-generic-t-sql-query-collector-type.md)  
  
-   [Add a Collection Item to a Collection Set &#40;Transact-SQL&#41;](../../2014/database-engine/add-a-collection-item-to-a-collection-set-transact-sql.md)  
  
 In addition, there are functions and views that you can use to get configuration data for the msdb and management data warehouse databases, execution log data, and data that is stored in the management data warehouse.  
  
 You can use the stored procedures, functions, and views that are provided to create your own end-to-end data collection scenarios.  
  
> [!IMPORTANT]  
>  Unlike regular stored procedures, the data collector stored procedures use strictly typed parameters and do not support automatic data type conversion. If these parameters are not called with the correct input parameter data types, as specified in the argument description, the stored procedure returns an error.  
  
 You can use [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] to create and execute the provided code samples. For more information, see [Object Explorer](../../2014/database-engine/object-explorer.md). As an alternative you can create the query in any editor and save it in a text file that has a .sql file name extension. You can execute the query from the Windows command prompt using the `sqlcmd` utility. For more information, see [Use the sqlcmd Utility](../../2014/database-engine/use-the-sqlcmd-utility.md).  
  
### Stored Procedures and Views  
 **Working with the data collector**  
  
 The following table describes the stored procedures that you can use to work with the data collector.  
  
|Procedure name|Description|  
|--------------------|-----------------|  
|[sp_syscollector_enable_collector](../Topic/sp_syscollector_enable_collector%20\(Transact-SQL\).md)|Enable the data collector.|  
|[sp_syscollector_disable_collector](../Topic/sp_syscollector_disable_collector%20\(Transact-SQL\).md)|Disable the data collector.|  
  
 **Working with collection sets**  
  
 The following table describes the stored procedures that you can use to work with collection sets.  
  
|Procedure name|Description|  
|--------------------|-----------------|  
|[sp_syscollector_run_collection_set &#40;Transact-SQL&#41;](../Topic/sp_syscollector_run_collection_set%20\(Transact-SQL\).md)|Run a collection set on demand.|  
|[sp_syscollector_start_collection_set &#40;Transact-SQL&#41;](../Topic/sp_syscollector_start_collection_set%20\(Transact-SQL\).md)|Start a collection set.|  
|[sp_syscollector_stop_collection_set &#40;Transact-SQL&#41;](../Topic/sp_syscollector_stop_collection_set%20\(Transact-SQL\).md)|Stop a collection set.|  
|[sp_syscollector_create_collection_set &#40;Transact-SQL&#41;](../Topic/sp_syscollector_create_collection_set%20\(Transact-SQL\).md)|Create a collection set.|  
|[sp_syscollector_delete_collection_set &#40;Transact-SQL&#41;](../Topic/sp_syscollector_delete_collection_set%20\(Transact-SQL\).md)|Delete a collection set.|  
|[sp_syscollector_update_collection_set &#40;Transact-SQL&#41;](../Topic/sp_syscollector_update_collection_set%20\(Transact-SQL\).md)|Change a collection set configuration.|  
|[sp_syscollector_upload_collection_set &#40;Transact-SQL&#41;](../Topic/sp_syscollector_upload_collection_set%20\(Transact-SQL\).md)|Upload collection set data to the management data warehouse. This is effectively an on-demand upload.|  
  
 **Working with collection items**  
  
 The following table describes the stored procedures that you can use to work with collection items.  
  
|Procedure name|Description|  
|--------------------|-----------------|  
|[sp_syscollector_create_collection_item &#40;Transact-SQL&#41;](../Topic/sp_syscollector_create_collection_item%20\(Transact-SQL\).md)|Create a collection item.|  
|[sp_syscollector_delete_collection_item &#40;Transact-SQL&#41;](../Topic/sp_syscollector_delete_collection_item%20\(Transact-SQL\).md)|Delete a collection item.|  
|[sp_syscollector_update_collection_item &#40;Transact-SQL&#41;](../Topic/sp_syscollector_update_collection_item%20\(Transact-SQL\).md)|Update a collection item.|  
  
 **Working with collector types**  
  
 The following table describes the stored procedures that you can use to work with collector types.  
  
|Procedure name|Description|  
|--------------------|-----------------|  
|[sp_syscollector_create_collector_type &#40;Transact-SQL&#41;](../Topic/sp_syscollector_create_collector_type%20\(Transact-SQL\).md)|Create a collector type.|  
|[sp_syscollector_update_collector_type &#40;Transact-SQL&#41;](../Topic/sp_syscollector_update_collector_type%20\(Transact-SQL\).md)|Update a collector type.|  
|[sp_syscollector_delete_collector_type &#40;Transact-SQL&#41;](../Topic/sp_syscollector_delete_collector_type%20\(Transact-SQL\).md)|Delete a collector type.|  
  
 **Getting configuration information**  
  
 The following table describes the views that you can use for getting configuration information and execution log data.  
  
|View name|Description|  
|---------------|-----------------|  
|[syscollector_config_store &#40;Transact-SQL&#41;](../Topic/syscollector_config_store%20\(Transact-SQL\).md)|Get data collector configuration.|  
|[syscollector_collection_items &#40;Transact-SQL&#41;](../Topic/syscollector_collection_items%20\(Transact-SQL\).md)|Get collection item information.|  
|[syscollector_collection_sets &#40;Transact-SQL&#41;](../Topic/syscollector_collection_sets%20\(Transact-SQL\).md)|Get collection set information.|  
|[syscollector_collector_types &#40;Transact-SQL&#41;](../Topic/syscollector_collector_types%20\(Transact-SQL\).md)|Get collector type information.|  
|[syscollector_execution_log &#40;Transact-SQL&#41;](../Topic/syscollector_execution_log%20\(Transact-SQL\).md)|Get information about collection set and package execution.|  
|[syscollector_execution_stats &#40;Transact-SQL&#41;](../Topic/syscollector_execution_stats%20\(Transact-SQL\).md)|Get information about task execution.|  
|[syscollector_execution_log_full &#40;Transact-SQL&#41;](../Topic/syscollector_execution_log_full%20\(Transact-SQL\).md)|Get information when the execution log is full.|  
  
 **Configuring access to the management data warehouse**  
  
 The following table describes the stored procedures that you can use to configure access to the management data warehouse.  
  
|Procedure name|Description|  
|--------------------|-----------------|  
|[sp_syscollector_set_warehouse_database_name &#40;Transact-SQL&#41;](../Topic/sp_syscollector_set_warehouse_database_name%20\(Transact-SQL\).md)|Specify the database name defined in the connection string for the management data warehouse.|  
|[sp_syscollector_set_warehouse_instance_name &#40;Transact-SQL&#41;](../Topic/sp_syscollector_set_warehouse_instance_name%20\(Transact-SQL\).md)|Specify the instance defined in the connection string for the management data warehouse.|  
  
 **Configuring the management data warehouse**  
  
 The following table describes the stored procedures that you can use to work with the management data warehouse configuration.  
  
|Procedure name|Description|  
|--------------------|-----------------|  
|[core.sp_create_snapshot &#40;Transact-SQL&#41;](../Topic/core.sp_create_snapshot%20\(Transact-SQL\).md)|Create a collection snapshot in the management data warehouse.|  
|[core.sp_update_data_source &#40;Transact-SQL&#41;](../Topic/core.sp_update_data_source%20\(Transact-SQL\).md)|Update the data source for data collection.|  
|[core.sp_add_collector_type &#40;Transact-SQL&#41;](../Topic/core.sp_add_collector_type%20\(Transact-SQL\).md)|Add a collector type to the management data warehouse.|  
|[core.sp_remove_collector_type &#40;Transact-SQL&#41;](../Topic/core.sp_remove_collector_type%20\(Transact-SQL\).md)|Remove a collector type from the management data warehouse.|  
|[core.sp_purge_data &#40;Transact-SQL&#41;](../Topic/core.sp_purge_data%20\(Transact-SQL\).md)|Delete data from the management data warehouse.|  
  
 **Working with upload packages**  
  
 The following table describes the stored procedures that you can use to work with upload packages.  
  
|Procedure name|Description|  
|--------------------|-----------------|  
|[sp_syscollector_set_cache_window &#40;Transact-SQL&#41;](../Topic/sp_syscollector_set_cache_window%20\(Transact-SQL\).md)|Configure the number of data upload retries.|  
|[sp_syscollector_set_cache_directory &#40;Transact-SQL&#41;](../Topic/sp_syscollector_set_cache_directory%20\(Transact-SQL\).md)|Specify temporary storage for data between upload retries.|  
  
 **Working with the data collection execution log**  
  
 The following table describes the stored procedures that you can use to work with the data collection execution log.  
  
|Procedure name|Description|  
|--------------------|-----------------|  
|[sp_syscollector_delete_execution_log_tree &#40;Transact-SQL&#41;](../Topic/sp_syscollector_delete_execution_log_tree%20\(Transact-SQL\).md)|Delete collection set entries from the execution log.|  
  
### Functions  
 The following table describes the functions that you can use to obtain execution and trace information.  
  
|Function name|Description|  
|-------------------|-----------------|  
|[fn_syscollector_get_execution_details &#40;Transact-SQL&#41;](../Topic/fn_syscollector_get_execution_details%20\(Transact-SQL\).md)|Get [!INCLUDE[ssIS](../../includes/ssis-md.md)] execution log data for a specific package.|  
|[fn_syscollector_get_execution_stats &#40;Transact-SQL&#41;](../Topic/fn_syscollector_get_execution_stats%20\(Transact-SQL\).md)|Get execution statistics for a collection set or package. This information includes errors that are logged.|  
|[snapshots.fn_trace_getdata &#40;Transact-SQL&#41;](../Topic/snapshots.fn_trace_getdata%20\(Transact-SQL\).md)|Get the events that are logged when the Generic SQL Trace collector type is used to collect data.|  
  
## See Also  
 [Execute a Stored Procedure](../../2014/database-engine/execute-a-stored-procedure.md)   
 [Use SQL Server Management Studio](../../2014/database-engine/use-sql-server-management-studio.md)   
 [Data Collection](../../2014/database-engine/data-collection.md)  
  
  