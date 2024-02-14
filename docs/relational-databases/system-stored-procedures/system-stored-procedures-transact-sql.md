---
title: "System stored procedures (Transact-SQL)"
description: "System stored procedures (Transact-SQL)"
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 05/26/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sql13.TSQLSysNoExpandPortal.f1"
  - "sql13.TSQLSysNoExpandPortal.f1_TSQL"
helpviewer_keywords:
  - "stored procedures [SQL Server]"
  - "APIs [SQL Server]"
  - "stored procedures [SQL Server], categories"
  - "system stored procedures [SQL Server], categories"
  - "system stored procedures [SQL Server]"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric"
---
# System stored procedures (Transact-SQL)

[!INCLUDE [sqlserver2016-asdb-asdbmi-asa-fabricse-fabricdw](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-asa-fabricse-fabricdw.md)]

In [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)], many administrative and informational activities can be performed by using system stored procedures. The system stored procedures are grouped into the categories shown in the following table.

## In this section

| Category | Description |
| --- | --- |
| [Active Geo-Replication stored procedures]() | Used to manage Active Geo-Replication configurations in Azure SQL Database |
| [Catalog stored procedures](catalog-stored-procedures-transact-sql.md) | Used to implement ODBC data dictionary functions and isolate ODBC applications from changes to underlying system tables. |
| [Change Data Capture stored procedures](change-data-capture-stored-procedures-transact-sql.md) | Used to enable, disable, or report on change data capture objects. |
| [Cursor stored procedures](cursor-stored-procedures-transact-sql.md) | Used to implements cursor variable functionality. |
| [Data Collector stored procedures](data-collector-stored-procedures-transact-sql.md) | Used to work with the data collector and the following components: collection sets, collection items, and collection types. |
| [Database Engine stored procedures](database-engine-stored-procedures-transact-sql.md) | Used for general maintenance of the [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)]. |
| [Database Mail stored procedures](database-mail-stored-procedures-transact-sql.md) | Used to perform e-mail operations from within an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. |
| [Database Maintenance Plan stored procedures](database-maintenance-plan-stored-procedures-transact-sql.md) | Used to set up core maintenance tasks that are required to manage database performance. |
| [Distributed Queries stored procedures](distributed-queries-stored-procedures-transact-sql.md) | Used to implement and manage distributed queries. |
| [FILESTREAM and FileTable stored procedures](./filestream-and-filetable-sp-filestream-force-garbage-collection.md) | Used to configure and manage the FILESTREAM and FileTable features. |
| [Firewall Rules stored procedures (Azure SQL Database)](firewall-rules-stored-procedures-azure-sql-database.md) | Used to configure the Azure SQL Database firewall. |
| [Full-Text Search stored procedures](full-text-search-and-semantic-search-stored-procedures-transact-sql.md) | Used to implement and query full-text indexes. |
| [General extended stored procedures](general-extended-stored-procedures-transact-sql.md) | Used to provide an interface from an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to external programs for various maintenance activities. |
| [Log Shipping stored procedures](log-shipping-stored-procedures-transact-sql.md) | Used to configure, modify, and monitor log shipping configurations. |
| [Management Data Warehouse stored procedures](management-data-warehouse-stored-procedures-transact-sql.md) | Used to configure the management data warehouse. |
| [MSDTC stored procedure](msdtc-stored-procedures-transact-sql.md)| Use for resetting the Microsoft Distributed Transaction Coordinator (MSDTC) log or looking at MSDTC statistics.
| [OLE Automation stored procedures](ole-automation-stored-procedures-transact-sql.md) | Used to enable standard Automation objects for use within a standard [!INCLUDE [tsql](../../includes/tsql-md.md)] batch. |
| [Policy-Based Management stored procedures](policy-based-management-stored-procedures-transact-sql.md) | Used for Policy-Based Management. |
| [PolyBase stored procedures](./polybase-stored-procedures-sp-polybase-join-group.md) | Add or remove a computer from a PolyBase scale-out group. |
| [Query Store stored procedures](query-store-stored-procedures-transact-sql.md) | Used to tune performance. |
| [Replication stored procedures](replication-stored-procedures-transact-sql.md) | Used to manage replication. |
| [Security stored procedures](security-stored-procedures-transact-sql.md) | Used to manage security. |
| [Snapshot Backup stored procedures](./snapshot-backup-sp-delete-backup.md) | Used to delete the FILE_SNAPSHOT backup along with all of its snapshots or to delete an individual backup file snapshot. |
| [Spatial Index stored procedures](./spatial-index-stored-procedures-arguments-and-properties.md) | Used to analyze and improve the indexing performance of spatial indexes. |
| [SQL Server Agent stored procedures](sql-server-agent-stored-procedures-transact-sql.md) | Used by [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)] to monitor performance and activity. |
| [SQL Server Profiler stored procedures](sql-server-profiler-stored-procedures-transact-sql.md) | Used by [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent to manage scheduled and event-driven activities. |
| [Stretch Database stored procedures](stretch-database-extended-stored-procedures-transact-sql.md) | Used to manage stretch databases. |
| [Temporal Tables stored procedures](./spatial-index-stored-procedures-arguments-and-properties.md) | Use for temporal tables |
| [XML stored procedures](xml-stored-procedures-transact-sql.md) | Used for XML text management. |

> [!NOTE]  
> Unless specifically documented otherwise, all system stored procedures return a value of `0` to indicate success. To indicate failure, a nonzero value is returned.

> [!IMPORTANT]  
> [!INCLUDE [stretch-database-deprecation](../../includes/stretch-database-deprecation.md)]

## API system stored procedures

Users that run [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)] against ADO, OLE DB, and ODBC applications may notice these applications using system stored procedures that aren't covered in the [!INCLUDE [tsql](../../includes/tsql-md.md)] Reference. These stored procedures are used by the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB Provider and the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver to implement the functionality of a database API. These stored procedures are just the mechanism the provider or driver uses to communicate user requests to an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. They are intended only for the internal use of the provider or the driver. Calling them explicitly from a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]-based application isn't supported.

The `sp_createorphan` and `sp_droporphans` stored procedures are used for ODBC **ntext**, **text**, and **image** processing.

The `sp_reset_connection` stored procedure is used by [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to support remote stored procedure calls in a transaction. This stored procedure also causes Audit Login and Audit Logout events to fire when a connection is reused from a connection pool.

The system stored procedures in the following tables are used only within an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or through client APIs and aren't intended for general customer use. They are subject to change and compatibility isn't guaranteed.

The following stored procedures are documented:

:::row:::
    :::column:::
        [sp_catalogs](sp-catalogs-transact-sql.md)
    :::column-end:::
    :::column:::
        [sp_column_privileges](sp-column-privileges-transact-sql.md)
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sp_column_privileges_ex](sp-column-privileges-ex-transact-sql.md)
    :::column-end:::
    :::column:::
        [sp_columns](sp-columns-transact-sql.md)
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sp_columns_ex](sp-columns-ex-transact-sql.md)
    :::column-end:::
    :::column:::
        [sp_databases](sp-databases-transact-sql.md)
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sp_cursor](sp-cursor-transact-sql.md)
    :::column-end:::
    :::column:::
        [sp_cursorclose](sp-cursorclose-transact-sql.md)
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sp_cursorexecute](sp-cursorexecute-transact-sql.md)
    :::column-end:::
    :::column:::
        [sp_cursorfetch](sp-cursorfetch-transact-sql.md)
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sp_cursoroption](sp-cursoroption-transact-sql.md)
    :::column-end:::
    :::column:::
        [sp_cursoropen](sp-cursoropen-transact-sql.md)
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sp_cursorprepare](sp-cursorprepare-transact-sql.md)
    :::column-end:::
    :::column:::
        [sp_cursorprepexec](sp-cursorprepexec-transact-sql.md)
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sp_cursorunprepare](sp-cursorunprepare-transact-sql.md)
    :::column-end:::
    :::column:::
        [sp_execute](sp-execute-transact-sql.md)
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sp_datatype_info](sp-datatype-info-transact-sql.md)
    :::column-end:::
    :::column:::
        [sp_fkeys](sp-fkeys-transact-sql.md)
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sp_foreignkeys](sp-foreignkeys-transact-sql.md)
    :::column-end:::
    :::column:::
        [sp_indexes](sp-indexes-transact-sql.md)
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sp_pkeys](sp-pkeys-transact-sql.md)
    :::column-end:::
    :::column:::
        [sp_primarykeys](sp-primarykeys-transact-sql.md)
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sp_prepare (Transact SQL)](sp-prepare-transact-sql.md)
    :::column-end:::
    :::column:::
        [sp_prepexec](sp-prepexec-transact-sql.md)
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sp_prepexecrpc](sp-prepexecrpc-transact-sql.md)
    :::column-end:::
    :::column:::
        [sp_unprepare](sp-unprepare-transact-sql.md)
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sp_server_info](sp-server-info-transact-sql.md)
    :::column-end:::
    :::column:::
        [sp_special_columns](sp-special-columns-transact-sql.md)
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sp_sproc_columns](sp-sproc-columns-transact-sql.md)
    :::column-end:::
    :::column:::
        [sp_statistics](sp-statistics-transact-sql.md)
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sp_table_privileges](sp-table-privileges-transact-sql.md)
    :::column-end:::
    :::column:::
        [sp_table_privileges_ex](sp-table-privileges-ex-transact-sql.md)
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sp_tables](sp-tables-transact-sql.md)
    :::column-end:::
    :::column:::
        [sp_tables_ex](sp-tables-ex-transact-sql.md)
    :::column-end:::
:::row-end:::

&nbsp;

The following stored procedures aren't documented:

:::row:::
    :::column:::
        `sp_assemblies_rowset`
    :::column-end:::
    :::column:::
        `sp_assemblies_rowset_rmt`
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        `sp_assemblies_rowset2`
    :::column-end:::
    :::column:::
        `sp_assembly_dependencies_rowset`
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        `sp_assembly_dependencies_rowset_rmt`
    :::column-end:::
    :::column:::
        `sp_assembly_dependencies_rowset2`
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        `sp_bcp_dbcmptlevel`
    :::column-end:::
    :::column:::
        `sp_catalogs_rowset`
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        `sp_catalogs_rowset;2`
    :::column-end:::
    :::column:::
        `sp_catalogs_rowset;5`
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        `sp_catalogs_rowset_rmt`
    :::column-end:::
    :::column:::
        `sp_catalogs_rowset2`
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        `sp_check_constbytable_rowset`
    :::column-end:::
    :::column:::
        `sp_check_constbytable_rowset;2`
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        `sp_check_constbytable_rowset2`
    :::column-end:::
    :::column:::
        `sp_check_constraints_rowset`
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        `sp_check_constraints_rowset;2`
    :::column-end:::
    :::column:::
        `sp_check_constraints_rowset2`
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        `sp_column_privileges_rowset`
    :::column-end:::
    :::column:::
        `sp_column_privileges_rowset;2`
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        `sp_column_privileges_rowset;5`
    :::column-end:::
    :::column:::
        `sp_column_privileges_rowset_rmt`
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        `sp_column_privileges_rowset2`
    :::column-end:::
    :::column:::
        `sp_columns_90`
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        `sp_columns_90_rowset`
    :::column-end:::
    :::column:::
        `sp_columns_90_rowset_rmt`
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        `sp_columns_90_rowset2`
    :::column-end:::
    :::column:::
        `sp_columns_ex_90`
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        `sp_columns_rowset`
    :::column-end:::
    :::column:::
        `sp_columns_rowset;2`
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        `sp_columns_rowset;5`
    :::column-end:::
    :::column:::
        `sp_columns_rowset_rmt`
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        `sp_columns_rowset2`
    :::column-end:::
    :::column:::
        `sp_constr_col_usage_rowset`
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        `sp_datatype_info_90`
    :::column-end:::
    :::column:::
        `sp_ddopen;1`
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        `sp_ddopen;10`
    :::column-end:::
    :::column:::
        `sp_ddopen;11`
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        `sp_ddopen;12`
    :::column-end:::
    :::column:::
        `sp_ddopen;13`
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        `sp_ddopen;2`
    :::column-end:::
    :::column:::
        `sp_ddopen;3`
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        `sp_ddopen;4`
    :::column-end:::
    :::column:::
        `sp_ddopen;5`
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        `sp_ddopen;6`
    :::column-end:::
    :::column:::
        `sp_ddopen;7`
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        `sp_ddopen;8`
    :::column-end:::
    :::column:::
        `sp_ddopen;9`
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        `sp_foreign_keys_rowset`
    :::column-end:::
    :::column:::
        `sp_foreign_keys_rowset;2`
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        `sp_foreign_keys_rowset;3`
    :::column-end:::
    :::column:::
        `sp_foreign_keys_rowset;5`
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        `sp_foreign_keys_rowset_rmt`
    :::column-end:::
    :::column:::
        `sp_foreign_keys_rowset2`
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        `sp_foreign_keys_rowset3`
    :::column-end:::
    :::column:::
        `sp_indexes_90_rowset`
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        `sp_indexes_90_rowset_rmt`
    :::column-end:::
    :::column:::
        `sp_indexes_90_rowset2`
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        `sp_indexes_rowset`
    :::column-end:::
    :::column:::
        `sp_indexes_rowset;2`
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        `sp_indexes_rowset;5`
    :::column-end:::
    :::column:::
        `sp_indexes_rowset_rmt`
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        `sp_indexes_rowset2`
    :::column-end:::
    :::column:::
        `sp_linkedservers_rowset`
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        `sp_linkedservers_rowset;2`
    :::column-end:::
    :::column:::
        `sp_linkedservers_rowset2`
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        `sp_oledb_database`
    :::column-end:::
    :::column:::
        `sp_oledb_defdb`
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        `sp_oledb_deflang`
    :::column-end:::
    :::column:::
        `sp_oledb_language`
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        `sp_oledb_ro_usrname`
    :::column-end:::
    :::column:::
        `sp_primary_keys_rowset`
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        `sp_primary_keys_rowset;2`
    :::column-end:::
    :::column:::
        `sp_primary_keys_rowset;3`
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        `sp_primary_keys_rowset;5`
    :::column-end:::
    :::column:::
        `sp_primary_keys_rowset_rmt`
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        `sp_primary_keys_rowset2`
    :::column-end:::
    :::column:::
        `sp_procedure_params_90_rowset`
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        `sp_procedure_params_90_rowset2`
    :::column-end:::
    :::column:::
        `sp_procedure_params_rowset`
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        `sp_procedure_params_rowset;2`
    :::column-end:::
    :::column:::
        `sp_procedure_params_rowset2`
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        `sp_procedures_rowset`
    :::column-end:::
    :::column:::
        `sp_procedures_rowset;2`
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        `sp_procedures_rowset2`
    :::column-end:::
    :::column:::
        `sp_provider_types_90_rowset`
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        `sp_provider_types_rowset`
    :::column-end:::
    :::column:::
        `sp_schemata_rowset`
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        `sp_schemata_rowset;3`
    :::column-end:::
    :::column:::
        `sp_special_columns_90`
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        `sp_sproc_columns_90`
    :::column-end:::
    :::column:::
        `sp_statistics_rowset`
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        `sp_statistics_rowset;2`
    :::column-end:::
    :::column:::
        `sp_statistics_rowset2`
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        `sp_stored_procedures`
    :::column-end:::
    :::column:::
        `sp_table_constraints_rowset`
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        `sp_table_constraints_rowset;2`
    :::column-end:::
    :::column:::
        `sp_table_constraints_rowset2`
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        `sp_table_privileges_rowset`
    :::column-end:::
    :::column:::
        `sp_table_privileges_rowset;2`
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        `sp_table_privileges_rowset;5`
    :::column-end:::
    :::column:::
        `sp_table_privileges_rowset_rmt`
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        `sp_table_privileges_rowset2`
    :::column-end:::
    :::column:::
        `sp_table_statistics_rowset`
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        `sp_table_statistics_rowset;2`
    :::column-end:::
    :::column:::
        `sp_table_statistics2_rowset`
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        `sp_tablecollations`
    :::column-end:::
    :::column:::
        `sp_tablecollations_90`
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        `sp_tables_info_90_rowset`
    :::column-end:::
    :::column:::
        `sp_tables_info_90_rowset_64`
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        `sp_tables_info_90_rowset2`
    :::column-end:::
    :::column:::
        `sp_tables_info_90_rowset2_64`
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        `sp_tables_info_rowset`
    :::column-end:::
    :::column:::
        `sp_tables_info_rowset;2`
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        `sp_tables_info_rowset_64`
    :::column-end:::
    :::column:::
        `sp_tables_info_rowset_64;2`
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        `sp_tables_info_rowset2`
    :::column-end:::
    :::column:::
        `sp_tables_info_rowset2_64`
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        `sp_tables_rowset;2`
    :::column-end:::
    :::column:::
        `sp_tables_rowset;5`
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        `sp_tables_rowset_rmt`
    :::column-end:::
    :::column:::
        `sp_tables_rowset2`
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        `sp_usertypes_rowset`
    :::column-end:::
    :::column:::
        `sp_usertypes_rowset_rmt`
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        `sp_usertypes_rowset2`
    :::column-end:::
    :::column:::
        `sp_views_rowset`
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        `sp_views_rowset2`
    :::column-end:::
    :::column:::
        `sp_xml_schema_rowset`
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        `sp_xml_schema_rowset2`
    :::column-end:::
    :::column:::
    :::column-end:::
:::row-end:::

## Related content

- [CREATE PROCEDURE (Transact-SQL)](../../t-sql/statements/create-procedure-transact-sql.md)
- [Stored Procedures (Database Engine)](../stored-procedures/stored-procedures-database-engine.md)
- [Running stored procedures (OLE DB)](../native-client/ole-db/stored-procedures-running.md)
- [Running stored procedures](../native-client-odbc-stored-procedures/running-stored-procedures.md)
- [Database Engine stored procedures (Transact-SQL)](database-engine-stored-procedures-transact-sql.md)
