---
title: "Database Engine Stored Procedures (Transact-SQL)"
description: Reference guide for Database Engine system stored procedures that perform general maintenance, configuration, and management tasks for SQL Server instances.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 02/24/2026
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
ai-usage: ai-assisted
helpviewer_keywords:
  - "Database Engine [SQL Server], stored procedures"
  - "system stored procedures [SQL Server], Database Engine"
  - "stored procedures [SQL Server], Database Engine"
dev_langs:
  - "TSQL"
---
# Database Engine stored procedures (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] supports the following system stored procedures that are used for general maintenance of an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

## Database recovery and file management

These procedures help recover suspect databases and manage database files.

| Stored procedure | Description |
| --- | --- |
| [sp_add_data_file_recover_suspect_db](sp-add-data-file-recover-suspect-db-transact-sql.md) | Adds a data file to a filegroup when database recovery fails due to insufficient space. |
| [sp_add_log_file_recover_suspect_db](sp-add-log-file-recover-suspect-db-transact-sql.md) | Adds a log file to a database when recovery fails due to insufficient log space. |
| [sp_attach_db](sp-attach-db-transact-sql.md) | Attaches a database to a server (deprecated; use CREATE DATABASE FOR ATTACH instead). |
| [sp_attach_single_file_db](sp-attach-single-file-db-transact-sql.md) | Attaches a database that has only one data file to the server (deprecated). |
| [sp_certify_removable](sp-certify-removable-transact-sql.md) | Verifies that a database is correctly configured for distribution on removable media (deprecated). |
| [sp_create_removable](sp-create-removable-transact-sql.md) | Creates a removable media database (deprecated; use sp_detach_db instead). |
| [sp_detach_db](sp-detach-db-transact-sql.md) | Detaches a database from a server instance and optionally runs UPDATE STATISTICS before detaching. |
| [sp_resetstatus](sp-resetstatus-transact-sql.md) | Resets the status of a suspect database (deprecated; use ALTER DATABASE SET ONLINE or EMERGENCY). |

## Server configuration

These procedures configure server-level settings and options.

| Stored procedure | Description |
| --- | --- |
| [sp_configure](sp-configure-transact-sql.md) | Displays or changes global configuration settings for the current server. |
| [sp_procoption](sp-procoption-transact-sql.md) | Sets or clears a stored procedure for automatic execution when SQL Server is started. |
| [sp_serveroption](sp-serveroption-transact-sql.md) | Sets server options for remote servers and linked servers. |
| [sp_setnetname](sp-setnetname-transact-sql.md) | Sets the network names in sys.servers to their actual network computer names for remote instances. |

## Backup devices and history

These procedures manage backup devices and backup history.

| Stored procedure | Description |
| --- | --- |
| [sp_addumpdevice](sp-addumpdevice-transact-sql.md) | Adds a backup device to the SQL Server instance. |
| [sp_dropdevice](sp-dropdevice-transact-sql.md) | Drops a backup device from an instance of SQL Server. |
| [sp_helpdevice](sp-helpdevice-transact-sql.md) | Reports information about backup devices (deprecated; use sys.backup_devices instead). |
| [sp_delete_backuphistory](sp-delete-backuphistory-transact-sql.md) | Deletes backup history entries older than a specified date from backup and restore history tables. |

## Extended properties and messages

These procedures manage extended properties on database objects and user-defined error messages.

| Stored procedure | Description |
| --- | --- |
| [sp_addextendedproperty](sp-addextendedproperty-transact-sql.md) | Adds an extended property to a database object such as a table, view, column, or other schema objects. |
| [sp_dropextendedproperty](sp-dropextendedproperty-transact-sql.md) | Drops an existing extended property from a database object. |
| [sp_updateextendedproperty](sp-updateextendedproperty-transact-sql.md) | Updates the value of an existing extended property. |
| [sp_addmessage](sp-addmessage-transact-sql.md) | Creates a user-defined error message that can be retrieved using the RAISERROR statement. |
| [sp_altermessage](sp-altermessage-transact-sql.md) | Alters the state of a user-defined error message in the sys.messages catalog view. |
| [sp_dropmessage](sp-dropmessage-transact-sql.md) | Drops a specified user-defined error message from the sys.messages catalog view. |

## Extended stored procedures (deprecated)

These procedures manage extended stored procedures, which are deprecated in favor of CLR integration.

| Stored procedure | Description |
| --- | --- |
| [sp_addextendedproc](sp-addextendedproc-transact-sql.md) | Registers the name of an extended stored procedure to the system (deprecated; use CLR integration). |
| [sp_dropextendedproc](sp-dropextendedproc-transact-sql.md) | Drops an extended stored procedure (deprecated; use CLR integration instead). |
| [sp_helpextendedproc](sp-helpextendedproc-transact-sql.md) | Reports the currently defined extended stored procedures and the DLL they belong to. |

## Data types and defaults (deprecated)

These procedures manage alias data types, defaults, and rules, which are deprecated in favor of modern alternatives.

| Stored procedure | Description |
| --- | --- |
| [sp_addtype](sp-addtype-transact-sql.md) | Creates an alias data type (deprecated; use CREATE TYPE instead). |
| [sp_droptype](sp-droptype-transact-sql.md) | Deletes an alias data type from systypes (deprecated; use DROP TYPE instead). |
| [sp_bindefault](sp-bindefault-transact-sql.md) | Binds a default to a column or to an alias data type (deprecated; use DEFAULT constraint). |
| [sp_unbindefault](sp-unbindefault-transact-sql.md) | Unbinds a default from a column or an alias data type in the current database. |
| [sp_bindrule](sp-bindrule-transact-sql.md) | Binds a rule to a column or to an alias data type (deprecated; use CHECK constraint). |
| [sp_unbindrule](sp-unbindrule-transact-sql.md) | Unbinds a rule from a column or an alias data type in the current database. |

## Statistics management

These procedures manage statistics for query optimization.

| Stored procedure | Description |
| --- | --- |
| [sp_autostats](sp-autostats-transact-sql.md) | Displays or changes the automatic UPDATE STATISTICS setting for an index, statistics object, table, or indexed view. |
| [sp_createstats](sp-createstats-transact-sql.md) | Creates single-column statistics for all eligible columns for all user tables in the current database. |
| [sp_helpstats](sp-helpstats-transact-sql.md) | Returns statistics information about columns and indexes on the specified table. |
| [sp_updatestats](sp-updatestats-transact-sql.md) | Runs UPDATE STATISTICS against all user-defined and internal tables in the current database. |

## Plan guides

These procedures manage plan guides for optimizing query execution plans.

| Stored procedure | Description |
| --- | --- |
| [sp_control_plan_guide](sp-control-plan-guide-transact-sql.md) | Enables, disables, or drops a plan guide. |
| [sp_create_plan_guide](sp-create-plan-guide-transact-sql.md) | Creates a plan guide for associating query hints or actual query plans with queries in a database. |
| [sp_create_plan_guide_from_handle](sp-create-plan-guide-from-handle-transact-sql.md) | Creates one or more plan guides from a query plan in the plan cache. |
| [sp_get_query_template](sp-get-query-template-transact-sql.md) | Returns the parameterized form of a query useful for creating a TEMPLATE plan guide. |

## Database mirroring monitoring

These procedures monitor database mirroring sessions.

| Stored procedure | Description |
| --- | --- |
| [sp_dbmmonitoraddmonitoring](sp-dbmmonitoraddmonitoring-transact-sql.md) | Creates a job that periodically updates the status information for every mirrored database on the server instance. |
| [sp_dbmmonitorchangealert](sp-dbmmonitorchangealert-transact-sql.md) | Adds or changes the warning threshold for a specified database mirroring performance metric. |
| [sp_dbmmonitorchangemonitoring](sp-dbmmonitorchangemonitoring-transact-sql.md) | Changes the value of a database mirroring monitoring parameter. |
| [sp_dbmmonitordropalert](sp-dbmmonitordropalert-transact-sql.md) | Drops the warning for a specified performance metric on a mirrored database. |
| [sp_dbmmonitordropmonitoring](sp-dbmmonitordropmonitoring-transact-sql.md) | Stops and deletes the database mirroring monitor job for all databases on the server instance. |
| [sp_dbmmonitorhelpalert](sp-dbmmonitorhelpalert-transact-sql.md) | Returns information about warning thresholds on one or all database mirroring performance metrics. |
| [sp_dbmmonitorhelpmonitoring](sp-dbmmonitorhelpmonitoring-transact-sql.md) | Returns the current update period for the database mirroring monitor job. |
| [sp_dbmmonitorresults](sp-dbmmonitorresults-transact-sql.md) | Returns status rows for a monitored mirrored database from the status table. |

## Query execution

These procedures execute and manage Transact-SQL statements.

| Stored procedure | Description |
| --- | --- |
| [sp_execute](sp-execute-transact-sql.md) | Executes a prepared Transact-SQL statement using a specified handle and optional parameter values. |
| [sp_executesql](sp-executesql-transact-sql.md) | Executes a Transact-SQL statement or batch that can be reused many times with different parameters. |
| [sp_prepare](sp-prepare-transact-sql.md) | Prepares a parameterized Transact-SQL statement and returns a statement handle for execution. |
| [sp_prepexec](sp-prepexec-transact-sql.md) | Prepares and executes a parameterized Transact-SQL statement, combining the prepare and first execute actions. |
| [sp_prepexecrpc](sp-prepexecrpc-transact-sql.md) | Prepares and executes a parameterized stored procedure call that has been specified using an RPC identifier. |
| [sp_unprepare](sp-unprepare-transact-sql.md) | Discards the execution plan created by the sp_prepare stored procedure. |
| [sp_describe_first_result_set](sp-describe-first-result-set-transact-sql.md) | Returns the metadata for the first possible result set of the Transact-SQL batch. |
| [sp_describe_undeclared_parameters](sp-describe-undeclared-parameters-transact-sql.md) | Returns a result set containing metadata about undeclared parameters in a Transact-SQL batch. |

## Application locks

These procedures manage application-level locks for custom synchronization schemes.

| Stored procedure | Description |
| --- | --- |
| [sp_getapplock](sp-getapplock-transact-sql.md) | Places a lock on an application resource for use with custom synchronization schemes. |
| [sp_releaseapplock](sp-releaseapplock-transact-sql.md) | Releases a lock on an application resource previously obtained by sp_getapplock. |

## Session and connection management

These procedures manage sessions and bound connections.

| Stored procedure | Description |
| --- | --- |
| [sp_bindsession](sp-bindsession-transact-sql.md) | Binds or unbinds a connection to other sessions in the same instance (deprecated; use MARS or distributed transactions). |
| [sp_getbindtoken](sp-getbindtoken-transact-sql.md) | Returns a unique identifier for the transaction to bind sessions (deprecated). |
| [sp_set_session_context](sp-set-session-context-transact-sql.md) | Sets a key-value pair in the session context. |

## Database information

These procedures return information about databases and database objects.

| Stored procedure | Description |
| --- | --- |
| [sp_help](sp-help-transact-sql.md) | Reports information about a database object, a user-defined data type, or a data type. |
| [sp_helpconstraint](sp-helpconstraint-transact-sql.md) | Returns a list of all constraint types, their names, and the columns on which they're defined. |
| [sp_helpdb](sp-helpdb-transact-sql.md) | Reports information about a specified database or all databases. |
| [sp_helpfile](sp-helpfile-transact-sql.md) | Returns the physical names and attributes of files associated with the current database. |
| [sp_helpfilegroup](sp-helpfilegroup-transact-sql.md) | Returns the names and attributes of filegroups associated with the current database. |
| [sp_helpindex](sp-helpindex-transact-sql.md) | Reports information about the indexes on a table or view. |
| [sp_helplanguage](sp-helplanguage-transact-sql.md) | Reports information about a particular alternative language or about all languages in SQL Server. |
| [sp_helpserver](sp-helpserver-transact-sql.md) | Reports information about a particular remote or replication server, or about all servers of both types. |
| [sp_helpsort](sp-helpsort-transact-sql.md) | Displays the sort order and character set for the instance of SQL Server. |
| [sp_helptext](sp-helptext-transact-sql.md) | Displays the definition of a user-defined rule, default, unencrypted stored procedure, function, trigger, or view. |
| [sp_helptrigger](sp-helptrigger-transact-sql.md) | Returns the type or types of DML triggers defined on the specified table for the current database. |
| [sp_depends](sp-depends-transact-sql.md) | Displays information about database object dependencies (deprecated; use sys.dm_sql_referencing_entities). |
| [sp_datatype_info](sp-datatype-info-transact-sql.md) | Returns information about the data types supported by the current environment. |

## Database maintenance

These procedures perform various database maintenance tasks.

| Stored procedure | Description |
| --- | --- |
| [sp_clean_db_file_free_space](sp-clean-db-file-free-space-transact-sql.md) | Removes residual information left on database pages in a specific database file due to data modification routines. |
| [sp_clean_db_free_space](sp-clean-db-free-space-transact-sql.md) | Removes residual information left on database pages in all files due to data modification routines. |
| [sp_cycle_errorlog](sp-cycle-errorlog-transact-sql.md) | Closes the current error log file and cycles the error log extension numbers like a server restart. |
| [sp_readerrorlog](sp-readerrorlog-transact-sql.md) | Reads the SQL Server error log or SQL Server Agent log file and filters on keywords. |
| [sp_recompile](sp-recompile-transact-sql.md) | Marks a stored procedure, trigger, or user-defined function to be recompiled the next time it runs. |
| [sp_refreshview](sp-refreshview-transact-sql.md) | Updates the metadata for the specified non-schema-bound view. |
| [sp_spaceused](sp-spaceused-transact-sql.md) | Displays the number of rows, disk space reserved, and disk space used by a table, indexed view, or Service Broker queue. |

## Object management

These procedures manage database objects such as tables, indexes, and triggers.

| Stored procedure | Description |
| --- | --- |
| [sp_rename](sp-rename-transact-sql.md) | Renames a user-created object in the current database such as a table, index, column, or alias data type. |
| [sp_renamedb](sp-renamedb-transact-sql.md) | Changes the name of a database (deprecated; use ALTER DATABASE MODIFY NAME instead). |
| [sp_indexoption](sp-indexoption-transact-sql.md) | Sets locking option values for user-defined indexes (deprecated; use ALTER INDEX instead). |
| [sp_settriggerorder](sp-settriggerorder-transact-sql.md) | Specifies the AFTER triggers that are fired first or last. |
| [sp_tableoption](sp-tableoption-transact-sql.md) | Sets option values for user-defined tables such as text in row option for tables with text, ntext, or image columns. |
| [sp_sequence_get_range](sp-sequence-get-range-transact-sql.md) | Returns a range of sequence values from a sequence object. |
| [sp_validname](sp-validname-transact-sql.md) | Checks for valid SQL Server identifier names. |

## Monitoring and diagnostics

These procedures provide monitoring and diagnostic information.

| Stored procedure | Description |
| --- | --- |
| [sp_lock](sp-lock-transact-sql.md) | Reports information about locks (deprecated; use sys.dm_tran_locks instead). |
| [sp_monitor](sp-monitor-transact-sql.md) | Displays statistics including CPU usage, I/O usage, and the amount of time idle since sp_monitor was last executed. |
| [sp_who](sp-who-transact-sql.md) | Provides information about current users, sessions, and processes in an instance of SQL Server. |
| [sp_invalidate_textptr](sp-invalidate-textptr-transact-sql.md) | Invalidates the specified in-row text pointer in the session or all in-row text pointers in the session. |

## Endpoint management

These procedures manage endpoints and their certificates.

| Stored procedure | Description |
| --- | --- |
| [sp_get_endpoint_certificate](sp-get-endpoint-certificate-transact-sql.md) | Gets information about the certificate currently being used by an endpoint. |

## Compatibility (deprecated)

These procedures are deprecated and provided for backward compatibility.

| Stored procedure | Description |
| --- | --- |
| [sp_db_increased_partitions](sp-db-increased-partitions.md) | Enables or disables support for up to 15,000 partitions (deprecated; available by default). |
| [sp_dbcmptlevel](sp-dbcmptlevel-transact-sql.md) | Sets database behaviors compatible with a specified version (deprecated; use ALTER DATABASE SET COMPATIBILITY_LEVEL). |

## In-Memory OLTP

These procedures manage memory-optimized tables and In-Memory OLTP features.

| Stored procedure | Description |
| --- | --- |
| [sys.sp_merge_xtp_checkpoint_files](sys-sp-xtp-merge-checkpoint-files-transact-sql.md) | Merges all data and delta files in the transaction range specified (deprecated; now automatic). |
| [sys.sp_xtp_control_proc_exec_stats](sys-sp-xtp-control-proc-exec-stats-transact-sql.md) | Enables or disables statistics collection at the procedure level for natively compiled stored procedures. |
| [sys.sp_flush_log](sys-sp-flush-log-transact-sql.md) | Flushes the transaction log of the current database to disk, hardening all prior durable memory-optimized transactions. |
| [sys.sp_xtp_bind_db_resource_pool](sys-sp-xtp-bind-db-resource-pool-transact-sql.md) | Binds a database with memory-optimized tables to a specified Resource Governor resource pool. |
| [sys.sp_xtp_control_query_exec_stats](sys-sp-xtp-control-query-exec-stats-transact-sql.md) | Enables or disables per-query statistics collection for all natively compiled stored procedures. |
| [sys.sp_xtp_checkpoint_force_garbage_collection](sys-sp-xtp-checkpoint-force-garbage-collection-transact-sql.md) | Forces garbage collection of In-Memory OLTP checkpoint files that are no longer needed. |
| [sys.sp_xtp_force_gc](sys-sp-xtp-force-gc-transact-sql.md) | Forces garbage collection of memory-optimized tables and their associated row versions. |
| [sys.sp_xtp_unbind_db_resource_pool](sys-sp-xtp-unbind-db-resource-pool-transact-sql.md) | Removes the binding between a database and the Resource Governor resource pool for In-Memory OLTP. |

## Related content

- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
