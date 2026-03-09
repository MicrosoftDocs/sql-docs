---
title: "System Stored Procedures (Transact-SQL)"
description: Reference guide for system stored procedures organized by functional category, including replication, security, Database Engine, and SQL Server Agent procedures.
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 02/24/2026
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
ms.custom:
  - ignite-2025
ai-usage: ai-assisted
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
monikerRange: "=azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---
# System stored procedures (Transact-SQL)

[!INCLUDE [sqlserver2016-asdb-asdbmi-asa-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-asa-fabricse-fabricdw-fabricsqldb.md)]

In [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)], many administrative and informational activities can be performed by using system stored procedures. The system stored procedures are grouped into the categories shown in the following table.

## Stored procedure categories

| Category | Description |
| --- | --- |
| [Active Geo-Replication stored procedures (Azure SQL Database)](/azure/azure-sql/database/active-geo-replication-overview) | Manage Active Geo-Replication and Auto-Failover Group configurations in Azure SQL Database. |
| [Catalog stored procedures](catalog-stored-procedures-transact-sql.md) | Implement ODBC data dictionary functions and isolate ODBC applications from changes to underlying system tables. |
| [Change Data Capture stored procedures](change-data-capture-stored-procedures-transact-sql.md) | Enable, disable, or report on change data capture objects. |
| [Cursor stored procedures](cursor-stored-procedures-transact-sql.md) | Implement cursor variable functionality. |
| [Data collector stored procedures](data-collector-stored-procedures-transact-sql.md) | Work with the data collector and its components: collection sets, collection items, and collection types. |
| [Database Engine stored procedures](database-engine-stored-procedures-transact-sql.md) | Perform general maintenance of the [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)]. |
| [Database Mail stored procedures](database-mail-stored-procedures-transact-sql.md) | Perform e-mail operations from within an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. |
| [Database Maintenance Plan stored procedures](database-maintenance-plan-stored-procedures-transact-sql.md) | Set up core maintenance tasks that are required to manage database performance. |
| [Distributed Queries stored procedures](distributed-queries-stored-procedures-transact-sql.md) | Implement and manage distributed queries. |
| [FILESTREAM and FileTable stored procedures](filestream-and-filetable-sp-filestream-force-garbage-collection.md) | Configure and manage the FILESTREAM and FileTable features. |
| [Firewall Rules stored procedures (Azure SQL Database)](firewall-rules-stored-procedures-azure-sql-database.md) | Configure the Azure SQL Database firewall. |
| [Full-Text Search and Semantic Search stored procedures](full-text-search-and-semantic-search-stored-procedures-transact-sql.md) | Implement and query full-text indexes. |
| [General extended stored procedures](general-extended-stored-procedures-transact-sql.md) | Provide an interface from an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to external programs for various maintenance activities. |
| [Log Shipping stored procedures](log-shipping-stored-procedures-transact-sql.md) | Configure, modify, and monitor log shipping configurations. |
| [Management Data Warehouse stored procedures](management-data-warehouse-stored-procedures-transact-sql.md) | Configure the management data warehouse. |
| [MSDTC stored procedures](msdtc-stored-procedures-transact-sql.md) | Reset the Microsoft Distributed Transaction Coordinator (MSDTC) log or look at MSDTC statistics. |
| [OLE Automation stored procedures](ole-automation-stored-procedures-transact-sql.md) | Enable standard Automation objects for use within a standard [!INCLUDE [tsql](../../includes/tsql-md.md)] batch. |
| [Policy-Based Management stored procedures](policy-based-management-stored-procedures-transact-sql.md) | Manage Policy-Based Management configurations. |
| [PolyBase stored procedures](polybase-stored-procedures-sp-polybase-join-group.md) | Add or remove a computer from a PolyBase scale-out group. |
| [Query Store stored procedures](query-store-stored-procedures-transact-sql.md) | Tune performance using Query Store data. |
| [Replication stored procedures](replication-stored-procedures-transact-sql.md) | Manage replication configurations and operations. |
| [Security stored procedures](security-stored-procedures-transact-sql.md) | Manage security settings and permissions. |
| [Snapshot Backup stored procedures](snapshot-backup-sp-delete-backup.md) | Delete the FILE_SNAPSHOT backup along with all of its snapshots or delete an individual backup file snapshot. |
| [Spatial Index stored procedures](spatial-index-stored-procedures-arguments-and-properties.md) | Analyze and improve the indexing performance of spatial indexes. |
| [SQL Server Agent stored procedures](sql-server-agent-stored-procedures-transact-sql.md) | Manage scheduled and event-driven activities for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent. |
| [SQL Server Profiler stored procedures](sql-server-profiler-stored-procedures-transact-sql.md) | Monitor performance and activity using [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)]. |
| [XML stored procedures](xml-stored-procedures-transact-sql.md) | Manage XML text processing. |

> [!NOTE]
> Unless specifically documented otherwise, all system stored procedures return a value of `0` to indicate success. To indicate failure, a nonzero value is returned.

## API system stored procedures

Users that run [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)] against ADO, OLE DB, and ODBC applications might notice these applications using system stored procedures that aren't covered in the [!INCLUDE [tsql](../../includes/tsql-md.md)] Reference. These stored procedures are used by the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB Provider and the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver to implement the functionality of a database API. These stored procedures are the mechanism the provider or driver uses to communicate user requests to an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. They are intended only for the internal use of the provider or the driver. Calling them explicitly from a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]-based application isn't supported.

The `sp_createorphan` and `sp_droporphans` stored procedures are used for ODBC **ntext**, **text**, and **image** processing.

The `sp_reset_connection` stored procedure is used by [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to support remote stored procedure calls in a transaction. This stored procedure also causes Audit Login and Audit Logout events to fire when a connection is reused from a connection pool.

The system stored procedures in the following tables are used only within an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or through client APIs and aren't intended for general customer use. They are subject to change and compatibility isn't guaranteed.

### Documented API stored procedures

| Stored procedure | Stored procedure |
| --- | --- |
| [sp_catalogs](sp-catalogs-transact-sql.md) | [sp_column_privileges](sp-column-privileges-transact-sql.md) |
| [sp_column_privileges_ex](sp-column-privileges-ex-transact-sql.md) | [sp_columns](sp-columns-transact-sql.md) |
| [sp_columns_ex](sp-columns-ex-transact-sql.md) | [sp_databases](sp-databases-transact-sql.md) |
| [sp_cursor](sp-cursor-transact-sql.md) | [sp_cursorclose](sp-cursorclose-transact-sql.md) |
| [sp_cursorexecute](sp-cursorexecute-transact-sql.md) | [sp_cursorfetch](sp-cursorfetch-transact-sql.md) |
| [sp_cursoroption](sp-cursoroption-transact-sql.md) | [sp_cursoropen](sp-cursoropen-transact-sql.md) |
| [sp_cursorprepare](sp-cursorprepare-transact-sql.md) | [sp_cursorprepexec](sp-cursorprepexec-transact-sql.md) |
| [sp_cursorunprepare](sp-cursorunprepare-transact-sql.md) | [sp_execute](sp-execute-transact-sql.md) |
| [sp_datatype_info](sp-datatype-info-transact-sql.md) | [sp_fkeys](sp-fkeys-transact-sql.md) |
| [sp_foreignkeys](sp-foreignkeys-transact-sql.md) | [sp_indexes](sp-indexes-transact-sql.md) |
| [sp_pkeys](sp-pkeys-transact-sql.md) | [sp_primarykeys](sp-primarykeys-transact-sql.md) |
| [sp_prepare (Transact SQL)](sp-prepare-transact-sql.md) | [sp_prepexec](sp-prepexec-transact-sql.md) |
| [sp_prepexecrpc](sp-prepexecrpc-transact-sql.md) | [sp_unprepare](sp-unprepare-transact-sql.md) |
| [sp_server_info](sp-server-info-transact-sql.md) | [sp_special_columns](sp-special-columns-transact-sql.md) |
| [sp_sproc_columns](sp-sproc-columns-transact-sql.md) | [sp_statistics](sp-statistics-transact-sql.md) |
| [sp_table_privileges](sp-table-privileges-transact-sql.md) | [sp_table_privileges_ex](sp-table-privileges-ex-transact-sql.md) |
| [sp_tables](sp-tables-transact-sql.md) | [sp_tables_ex](sp-tables-ex-transact-sql.md) |

### Undocumented API stored procedures

The following stored procedures aren't documented and are for internal use only:

| Stored procedure | Stored procedure |
| --- | --- |
| `sp_assemblies_rowset` | `sp_assemblies_rowset_rmt` |
| `sp_assemblies_rowset2` | `sp_assembly_dependencies_rowset` |
| `sp_assembly_dependencies_rowset_rmt` | `sp_assembly_dependencies_rowset2` |
| `sp_bcp_dbcmptlevel` | `sp_catalogs_rowset` |
| `sp_catalogs_rowset;2` | `sp_catalogs_rowset;5` |
| `sp_catalogs_rowset_rmt` | `sp_catalogs_rowset2` |
| `sp_check_constbytable_rowset` | `sp_check_constbytable_rowset;2` |
| `sp_check_constbytable_rowset2` | `sp_check_constraints_rowset` |
| `sp_check_constraints_rowset;2` | `sp_check_constraints_rowset2` |
| `sp_column_privileges_rowset` | `sp_column_privileges_rowset;2` |
| `sp_column_privileges_rowset;5` | `sp_column_privileges_rowset_rmt` |
| `sp_column_privileges_rowset2` | `sp_columns_90` |
| `sp_columns_90_rowset` | `sp_columns_90_rowset_rmt` |
| `sp_columns_90_rowset2` | `sp_columns_ex_90` |
| `sp_columns_rowset` | `sp_columns_rowset;2` |
| `sp_columns_rowset;5` | `sp_columns_rowset_rmt` |
| `sp_columns_rowset2` | `sp_constr_col_usage_rowset` |
| `sp_datatype_info_90` | `sp_ddopen;1` |
| `sp_ddopen;10` | `sp_ddopen;11` |
| `sp_ddopen;12` | `sp_ddopen;13` |
| `sp_ddopen;2` | `sp_ddopen;3` |
| `sp_ddopen;4` | `sp_ddopen;5` |
| `sp_ddopen;6` | `sp_ddopen;7` |
| `sp_ddopen;8` | `sp_ddopen;9` |
| `sp_foreign_keys_rowset` | `sp_foreign_keys_rowset;2` |
| `sp_foreign_keys_rowset;3` | `sp_foreign_keys_rowset;5` |
| `sp_foreign_keys_rowset_rmt` | `sp_foreign_keys_rowset2` |
| `sp_foreign_keys_rowset3` | `sp_indexes_90_rowset` |
| `sp_indexes_90_rowset_rmt` | `sp_indexes_90_rowset2` |
| `sp_indexes_rowset` | `sp_indexes_rowset;2` |
| `sp_indexes_rowset;5` | `sp_indexes_rowset_rmt` |
| `sp_indexes_rowset2` | `sp_linkedservers_rowset` |
| `sp_linkedservers_rowset;2` | `sp_linkedservers_rowset2` |
| `sp_oledb_database` | `sp_oledb_defdb` |
| `sp_oledb_deflang` | `sp_oledb_language` |
| `sp_oledb_ro_usrname` | `sp_primary_keys_rowset` |
| `sp_primary_keys_rowset;2` | `sp_primary_keys_rowset;3` |
| `sp_primary_keys_rowset;5` | `sp_primary_keys_rowset_rmt` |
| `sp_primary_keys_rowset2` | `sp_procedure_params_90_rowset` |
| `sp_procedure_params_90_rowset2` | `sp_procedure_params_rowset` |
| `sp_procedure_params_rowset;2` | `sp_procedure_params_rowset2` |
| `sp_procedures_rowset` | `sp_procedures_rowset;2` |
| `sp_procedures_rowset2` | `sp_provider_types_90_rowset` |
| `sp_provider_types_rowset` | `sp_schemata_rowset` |
| `sp_schemata_rowset;3` | `sp_special_columns_90` |
| `sp_sproc_columns_90` | `sp_statistics_rowset` |
| `sp_statistics_rowset;2` | `sp_statistics_rowset2` |
| `sp_stored_procedures` | `sp_table_constraints_rowset` |
| `sp_table_constraints_rowset;2` | `sp_table_constraints_rowset2` |
| `sp_table_privileges_rowset` | `sp_table_privileges_rowset;2` |
| `sp_table_privileges_rowset;5` | `sp_table_privileges_rowset_rmt` |
| `sp_table_privileges_rowset2` | `sp_table_statistics_rowset` |
| `sp_table_statistics_rowset;2` | `sp_table_statistics2_rowset` |
| `sp_tablecollations` | `sp_tablecollations_90` |
| `sp_tables_info_90_rowset` | `sp_tables_info_90_rowset_64` |
| `sp_tables_info_90_rowset2` | `sp_tables_info_90_rowset2_64` |
| `sp_tables_info_rowset` | `sp_tables_info_rowset;2` |
| `sp_tables_info_rowset_64` | `sp_tables_info_rowset_64;2` |
| `sp_tables_info_rowset2` | `sp_tables_info_rowset2_64` |
| `sp_tables_rowset;2` | `sp_tables_rowset;5` |
| `sp_tables_rowset_rmt` | `sp_tables_rowset2` |
| `sp_usertypes_rowset` | `sp_usertypes_rowset_rmt` |
| `sp_usertypes_rowset2` | `sp_views_rowset` |
| `sp_views_rowset2` | `sp_xml_schema_rowset` |
| `sp_xml_schema_rowset2` | |

## Related content

- [CREATE PROCEDURE (Transact-SQL)](../../t-sql/statements/create-procedure-transact-sql.md)
- [Stored Procedures (Database Engine)](../stored-procedures/stored-procedures-database-engine.md)
- [Running stored procedures (OLE DB)](../native-client/ole-db/stored-procedures-running.md)
- [Running stored procedures](../native-client-odbc-stored-procedures/running-stored-procedures.md)
- [Database Engine stored procedures (Transact-SQL)](database-engine-stored-procedures-transact-sql.md)

