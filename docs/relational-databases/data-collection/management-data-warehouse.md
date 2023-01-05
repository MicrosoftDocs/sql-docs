---
title: "Management Data Warehouse | Microsoft Docs"
description: The management data warehouse in SQL Server is a relational database that contains the data that is collected from a target server.
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "data collector [SQL Server], management data warehouse"
  - "data warehouse"
  - "management data warehouse"
ms.assetid: 9874a8b2-7ccd-494a-944c-ad33b30b5499
author: MashaMSFT
ms.author: mathoma
---
# Management Data Warehouse
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  The management data warehouse is a relational database that contains the data that is collected from a server that is a data collection target. This data is used to generate the reports for the System Data collection sets, and can also be used to create custom reports.  
  
 The data collector infrastructure defines the jobs and maintenance plans that are needed to implement the retention policies defined by the database administrator.  
  
> [!IMPORTANT]  
>  For this release of the data collector, the management data warehouse is created using the Simple recovery model, to minimize logging. You should implement the appropriate recovery model for your organization.  
  
## Deploying and Using the Data Warehouse  
 You can install the management data warehouse on the same instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that runs the data collector. However, if server resources or performance is an issue on the server being monitored, you can install the management data warehouse on a different computer.  
  
 The required schemas and their objects for the predefined system collection sets are created when you create the management data warehouse. The schemas that are created are core and snapshots.A third schema, custom_snapshots, is created when user-defined collection sets are created that include collection items that use the Generic T-SQL Query collector type.  
  
###### Core schema  
 The core schema describes the tables, stored procedures, and views that are used to organize and to identify collected data. These tables are shared among all the data tables created for individual collector types. This schema is locked and can only be modified by the owner of the management data warehouse database. The names of the tables in this schema are prefixed by "core".  
  
 The following table describes the database tables in the core schema. These database tables enable the data collector to track where the data came from, who inserted it, and when it was uploaded to the data warehouse.  
  
|Table name|Description|  
|----------------|-----------------|  
|core.performance_counter_report_group_items|Stores information about how the management data warehouse reports should group and aggregate performance counters.|  
|core.snapshots_internal|Identifies each new snapshot. A new row is inserted into this table whenever an upload package starts uploading a new batch of data.|  
|core.snapshot_timetable_internal|Stores information about the snapshot times. The snapshot time is stored in a separate table because many snapshots can happen at nearly the same time.|  
|core.source.info_internal|This table stores information about the data source. This table is updated whenever a new collection set starts uploading data to the data warehouse.|  
|core.supported_collector_types_internal|Contains the IDs of registered collector types that can upload data to the management data warehouse. This table is only updated when the schema of the warehouse is updated to support a new collector type. When the management data warehouse is created, this table is populated with the IDs of the collector types provided by the data collector.|  
|core.wait_categories|Contains the categories used to group wait types according to wait_type characteristic.|  
|core.wait_types|Contains the wait types recognized by the data collector.|  
|core.purge_info_internal|Indicates that a request has been made to stop the removal of data from the management data warehouse.|  
  
 The preceding tables are used with collector type tables to store information. For example, the Generic SQL Trace collector type uses the following tables to store trace data:  
  
-   core.source_info_internal  
  
-   core.snapshots_internal  
  
-   snapshots.trace_info  
  
-   snapshots.trace_data  
  
###### Snapshots schema  
 The snapshots schema describes the objects needed to store and maintain the data collected by the collector types that are provided. The tables in this schema are fixed and do not need to be changed during the lifetime of the collector type. If changes are needed, the schema can only be changed by members of the mdw_admin role. These tables are created to store the data collected by the System Data collection sets.  
  
 The following tables illustrate a portion of the management data warehouse schema that is required for the Server Activity and Query Statistics collection sets.  
  
-   System-level resource tables  
  
    -   **snapshots.os_wait_stats**  
  
    -   **snapshots.os_latch_stats**  
  
    -   **snapshots.os_schedulers**  
  
    -   **snapshots.os_memory_clerks**  
  
    -   **snapshots.os_memory_nodes**  
  
    -   snapshots.sql_process_and_system_memory  
  
-   System activity  
  
    -   snapshots.active_sessions_and_requests  
  
-   Query statistics  
  
    -   snapshots.query_stats  
  
-   I/O statistics  
  
    -   **snapshots.io_virtual_file_stats**  
  
-   Query text and plan  
  
    -   snapshots.notable_query_text  
  
    -   snapshots.notable_query_plan  
  
-   Normalized query statistics  
  
    -   snapshots.distinct_queries  
  
    -   snapshots.distinct_query_to_handle  
  
 **Custom_snapshots schema**  
  
 The custom_snapshots schema describes new tables and views that are created when standard or third-party collector types are used to create user-defined collection sets. Any collector type that requires a new data table for a collection item can create that table in this schema. New tables can be added in this schema by members of the mdw_writer role. Any other changes to the schema can only be made by members of the mdw_admin role.  
  
 You can get detailed data type and content information for the database table columns by reading the documentation for the appropriate data collector stored procedure for each of the tables.  
  
### Best Practices  
 When working with the management data warehouse, we recommend following these best practices:  
  
-   Do not modify the metadata of management data warehouse tables unless you are adding a new collector type.  
  
-   Do not directly modify the data in the management data warehouse. Changing the data that you have collected invalidates the legitimacy of the collected data.  
  
-   Instead of directly using the tables, use the documented stored procedures and functions that are provided with the data collector to access instance and application data. The table names and definitions can change, do change when you update the application, and might change in future releases.  
  
## Change History  
  
|Updated content|  
|---------------------|  
|Added the core.performance_counter_report_group_items table to the "Core schema" section.|  
|Updated the list of tables in the "Snapshots schema" section. Added snapshots.os_memory_clerks,snapshots.sql_process_and_system_memory, and snapshots.io_virtual_file_stats. Removedsnapshots.os_process_memory and snapshots.distinct_query_stats.|  
  
## See Also  
 [Management Data Warehouse Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/management-data-warehouse-stored-procedures-transact-sql.md)   
 [Data Collector Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/data-collector-stored-procedures-transact-sql.md)   
 [Data Collection](../../relational-databases/data-collection/data-collection.md)   
 [View a Collection Set Report &#40;SQL Server Management Studio&#41;](../../relational-databases/data-collection/view-a-collection-set-report-sql-server-management-studio.md)  
  
  
