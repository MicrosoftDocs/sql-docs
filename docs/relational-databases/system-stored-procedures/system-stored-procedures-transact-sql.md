---
title: "System Stored Procedures (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "02/21/2016"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sql13.TSQLSysNoExpandPortal.f1"
  - "sql13.TSQLSysNoExpandPortal.f1_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "stored procedures [SQL Server]"
  - "APIs [SQL Server]"
  - "stored procedures [SQL Server], categories"
  - "system stored procedures [SQL Server], categories"
  - "system stored procedures [SQL Server]"
ms.assetid: a5c4d5b8-5a24-4a2d-99b4-d003b546ee3a
author: VanMSFT
ms.author: vanto
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# System Stored Procedures (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2016-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2016-asdb-xxxx-xxx-md.md)]

  In [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], many administrative and informational activities can be performed by using system stored procedures. The system stored procedures are grouped into the categories shown in the following table.  
  
## In This Section  
  
|Category|Description|  
|--------------|-----------------|  
|[Active Geo-Replication Stored Procedures](https://msdn.microsoft.com/library/81658ee4-4422-4d73-bf7a-86a07422cb0d)|Used to manage to manage Active Geo-Replication configurations in Azure SQL Database|  
|[Catalog Stored Procedures](../../relational-databases/system-stored-procedures/catalog-stored-procedures-transact-sql.md)|Used to implement ODBC data dictionary functions and isolate ODBC applications from changes to underlying system tables.|  
|[Change Data Capture Stored Procedures](../../relational-databases/system-stored-procedures/change-data-capture-stored-procedures-transact-sql.md)|Used to enable, disable, or report on change data capture objects.|  
|[Cursor Stored Procedures](../../relational-databases/system-stored-procedures/cursor-stored-procedures-transact-sql.md)|Used to implements cursor variable functionality.|  
|[Data Collector Stored Procedures](../../relational-databases/system-stored-procedures/data-collector-stored-procedures-transact-sql.md)|Used to work with the data collector and the following components: collection sets, collection items, and collection types.|  
|[Database Engine Stored Procedures](../../relational-databases/system-stored-procedures/database-engine-stored-procedures-transact-sql.md)|Used for general maintenance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)].|  
|[Database Mail Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/database-mail-stored-procedures-transact-sql.md)|Used to perform e-mail operations from within an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
|[Database Maintenance Plan Stored Procedures](../../relational-databases/system-stored-procedures/database-maintenance-plan-stored-procedures-transact-sql.md)|Used to set up core maintenance tasks that are required to manage database performance.|  
|[Distributed Queries Stored Procedures](../../relational-databases/system-stored-procedures/distributed-queries-stored-procedures-transact-sql.md)|Used to implement and manage distributed queries.|  
|[Filestream and FileTable Stored Procedures &#40;Transact-SQL&#41;](https://msdn.microsoft.com/library/54beca08-c012-4ebd-aa68-d8a10d221b64)|Used to configure and manage the FILESTREAM and FileTable features.|  
|[Firewall Rules Stored Procedures &#40;Azure SQL Database&#41;](../../relational-databases/system-stored-procedures/firewall-rules-stored-procedures-azure-sql-database.md)|Used to configure the Azure SQL Database firewall.|  
|[Full-Text Search Stored Procedures](../../relational-databases/system-stored-procedures/full-text-search-and-semantic-search-stored-procedures-transact-sql.md)|Used to implement and query full-text indexes.|  
|[General Extended Stored Procedures](../../relational-databases/system-stored-procedures/general-extended-stored-procedures-transact-sql.md)|Used to provide an interface from an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to external programs for various maintenance activities.|  
|[Log Shipping Stored Procedures](../../relational-databases/system-stored-procedures/log-shipping-stored-procedures-transact-sql.md)|Used to configure, modify, and monitor log shipping configurations.|  
|[Management Data Warehouse Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/management-data-warehouse-stored-procedures-transact-sql.md)|Used to configure the management data warehouse.|  
|[OLE Automation Stored Procedures](../../relational-databases/system-stored-procedures/ole-automation-stored-procedures-transact-sql.md)|Used to enable standard Automation objects for use within a standard [!INCLUDE[tsql](../../includes/tsql-md.md)] batch.|  
|[Policy-Based Management Stored Procedures](../../relational-databases/system-stored-procedures/policy-based-management-stored-procedures-transact-sql.md)|Used for Policy-Based Management.|  
|[PolyBase stored procedures](https://msdn.microsoft.com/library/a522b303-bd1b-410b-92d1-29c950a15ede)|Add or remove a computer from a PolyBase scale-out group.|  
|[Query Store Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/query-store-stored-procedures-transact-sql.md)|Used to tune performance.|  
|[Replication Stored Procedures](../../relational-databases/system-stored-procedures/replication-stored-procedures-transact-sql.md)|Used to manage replication.|  
|[Security Stored Procedures](../../relational-databases/system-stored-procedures/security-stored-procedures-transact-sql.md)|Used to manage security.|  
|[Snapshot Backup Stored Procedures](https://msdn.microsoft.com/library/c278db87-5770-4037-a1e6-b9853a943339)|Used to delete the FILE_SNAPSHOT backup along with all of its snapshots or to delete an individual backup file snapshot.|  
|[Spatial Index Stored Procedures](https://msdn.microsoft.com/library/1be0f34e-3d5a-4a1f-9299-bd482362ec7a)|Used to analyze and improve the indexing performance of spatial indexes.|  
|[SQL Server Agent Stored Procedures](../../relational-databases/system-stored-procedures/sql-server-agent-stored-procedures-transact-sql.md)|Used by [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] to monitor performance and activity.|  
|[SQL Server Profiler Stored Procedures](../../relational-databases/system-stored-procedures/sql-server-profiler-stored-procedures-transact-sql.md)|Used by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent to manage scheduled and event-driven activities.|  
|[Stretch Database Stored Procedures](../../relational-databases/system-stored-procedures/stretch-database-extended-stored-procedures-transact-sql.md)|Used to manage stretch databases.|  
|[Temporal Tables Stored Procedures](https://msdn.microsoft.com/library/f28ca74e-7876-4592-b794-e78e3690fff6)|Use for temporal tables|  
|[XML Stored Procedures](../../relational-databases/system-stored-procedures/xml-stored-procedures-transact-sql.md)|Used for XML text management.|  
  
> [!NOTE]  
>  Unless specifically documented otherwise, all system stored procedures return a value of 0 to indicate success. To indicate failure, a nonzero value is returned.  
  
## API System Stored Procedures  
 Users that run [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] against ADO, OLE DB, and ODBC applications may notice these applications using system stored procedures that are not covered in the [!INCLUDE[tsql](../../includes/tsql-md.md)] Reference. These stored procedures are used by the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB Provider and the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver to implement the functionality of a database API. These stored procedures are just the mechanism the provider or driver uses to communicate user requests to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. They are intended only for the internal use of the provider or the driver. Calling them explicitly from a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]-based application is not supported.  
  
 The sp_createorphan and sp_droporphans stored procedures are used for ODBC **ntext**, **text**, and **image** processing.  
  
 The sp_reset_connection stored procedure is used by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to support remote stored procedure calls in a transaction. This stored procedure also causes Audit Login and Audit Logout events to fire when a connection is reused from a connection pool.  
  
 The system stored procedures in the following tables are used only within an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or through client APIs and are not intended for general customer use. They are subject to change and compatibility is not guaranteed.  
  
 The following stored procedures are documented in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online:  
  
|||  
|-|-|  
|sp_catalogs|sp_column_privileges|  
|sp_column_privileges_ex|sp_columns|  
|sp_columns_ex|sp_databases|  
|sp_cursor|sp_cursorclose|  
|sp_cursorexecute|sp_cursorfetch|  
|sp_cursoroption|sp_cursoropen|  
|sp_cursorprepare|sp_cursorprepexec|  
|sp_cursorunprepare|sp_execute|  
|sp_datatype_info|sp_fkeys|  
|sp_foreignkeys|sp_indexes|  
|sp_pkeys|sp_primarykeys|  
|sp_prepare|sp_prepexec|  
|sp_prepexecrpc|sp_unprepare|  
|sp_server_info|sp_special_columns|  
|sp_sproc_columns|sp_statistics|  
|sp_table_privileges|sp_table_privileges_ex|  
|sp_tables|sp_tables_ex|  
  
 The following stored procedures are not documented:  
  
|||  
|-|-|  
|sp_assemblies_rowset|sp_assemblies_rowset_rmt|  
|sp_assemblies_rowset2|sp_assembly_dependencies_rowset|  
|sp_assembly_dependencies_rowset_rmt|sp_assembly_dependencies_rowset2|  
|sp_bcp_dbcmptlevel|sp_catalogs_rowset|  
|sp_catalogs_rowset;2|sp_catalogs_rowset;5|  
|sp_catalogs_rowset_rmt|sp_catalogs_rowset2|  
|sp_check_constbytable_rowset|sp_check_constbytable_rowset;2|  
|sp_check_constbytable_rowset2|sp_check_constraints_rowset|  
|sp_check_constraints_rowset;2|sp_check_constraints_rowset2|  
|sp_column_privileges_rowset|sp_column_privileges_rowset;2|  
|sp_column_privileges_rowset;5|sp_column_privileges_rowset_rmt|  
|sp_column_privileges_rowset2|sp_columns_90|  
|sp_columns_90_rowset|sp_columns_90_rowset_rmt|  
|sp_columns_90_rowset2|sp_columns_ex_90|  
|sp_columns_rowset|sp_columns_rowset;2|  
|sp_columns_rowset;5|sp_columns_rowset_rmt|  
|sp_columns_rowset2|sp_constr_col_usage_rowset|  
|sp_datatype_info_90|sp_ddopen;1|  
|sp_ddopen;10|sp_ddopen;11|  
|sp_ddopen;12|sp_ddopen;13|  
|sp_ddopen;2|sp_ddopen;3|  
|sp_ddopen;4|sp_ddopen;5|  
|sp_ddopen;6|sp_ddopen;7|  
|sp_ddopen;8|sp_ddopen;9|  
|sp_foreign_keys_rowset|sp_foreign_keys_rowset;2|  
|sp_foreign_keys_rowset;3|sp_foreign_keys_rowset;5|  
|sp_foreign_keys_rowset_rmt|sp_foreign_keys_rowset2|  
|sp_foreign_keys_rowset3|sp_indexes_90_rowset|  
|sp_indexes_90_rowset_rmt|sp_indexes_90_rowset2|  
|sp_indexes_rowset|sp_indexes_rowset;2|  
|sp_indexes_rowset;5|sp_indexes_rowset_rmt|  
|sp_indexes_rowset2|sp_linkedservers_rowset|  
|sp_linkedservers_rowset;2|sp_linkedservers_rowset2|  
|sp_oledb_database|sp_oledb_defdb|  
|sp_oledb_deflang|sp_oledb_language|  
|sp_oledb_ro_usrname|sp_primary_keys_rowset|  
|sp_primary_keys_rowset;2|sp_primary_keys_rowset;3|  
|sp_primary_keys_rowset;5|sp_primary_keys_rowset_rmt|  
|sp_primary_keys_rowset2|sp_procedure_params_90_rowset|  
|sp_procedure_params_90_rowset2|sp_procedure_params_rowset|  
|sp_procedure_params_rowset;2|sp_procedure_params_rowset2|  
|sp_procedures_rowset|sp_procedures_rowset;2|  
|sp_procedures_rowset2|sp_provider_types_90_rowset|  
|sp_provider_types_rowset|sp_schemata_rowset|  
|sp_schemata_rowset;3|sp_special_columns_90|  
|sp_sproc_columns_90|sp_statistics_rowset|  
|sp_statistics_rowset;2|sp_statistics_rowset2|  
|sp_stored_procedures|sp_table_constraints_rowset|  
|sp_table_constraints_rowset;2|sp_table_constraints_rowset2|  
|sp_table_privileges_rowset|sp_table_privileges_rowset;2|  
|sp_table_privileges_rowset;5|sp_table_privileges_rowset_rmt|  
|sp_table_privileges_rowset2|sp_table_statistics_rowset|  
|sp_table_statistics_rowset;2|sp_table_statistics2_rowset|  
|sp_tablecollations|sp_tablecollations_90|  
|sp_tables_info_90_rowset|sp_tables_info_90_rowset_64|  
|sp_tables_info_90_rowset2|sp_tables_info_90_rowset2_64|  
|sp_tables_info_rowset|sp_tables_info_rowset;2|  
|sp_tables_info_rowset_64|sp_tables_info_rowset_64;2|  
|sp_tables_info_rowset2|sp_tables_info_rowset2_64|  
|sp_tables_rowset;2|sp_tables_rowset;5|  
|sp_tables_rowset_rmt|sp_tables_rowset2|  
|sp_usertypes_rowset|sp_usertypes_rowset_rmt|  
|sp_usertypes_rowset2|sp_views_rowset|  
|sp_views_rowset2|sp_xml_schema_rowset|  
|sp_xml_schema_rowset2||  
  
## See Also  
 [CREATE PROCEDURE &#40;Transact-SQL&#41;](../../t-sql/statements/create-procedure-transact-sql.md)   
 [Stored Procedures &#40;Database Engine&#41;](../../relational-databases/stored-procedures/stored-procedures-database-engine.md)   
 [Running Stored Procedures &#40;OLE DB&#41;](../../relational-databases/native-client/ole-db/stored-procedures-running.md)   
 [Running Stored Procedures](../../relational-databases/native-client-odbc-stored-procedures/running-stored-procedures.md)   
 [Database Engine Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/database-engine-stored-procedures-transact-sql.md)   
 [Running Stored Procedures](../../relational-databases/native-client-odbc-stored-procedures/running-stored-procedures.md)  
  
  
