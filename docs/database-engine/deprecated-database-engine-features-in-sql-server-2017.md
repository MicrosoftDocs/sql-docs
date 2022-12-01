---
title: "Deprecated database engine features in SQL Server 2017"
titleSuffix: "SQL Server 2019"
description: Find out about deprecated database engine features that are still available in SQL Server 2017 (14.x), but shouldn't be used in new applications.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 07/25/2022
ms.service: sql
ms.subservice: release-landing
ms.topic: conceptual
ms.custom: seo-lt-2019
helpviewer_keywords:
  - "deprecated features [SQL Server]"
  - "Database Engine [SQL Server], backward compatibility"
  - "deprecation [SQL Server], feature list"
monikerRange: ">=sql-server-2017||>=sql-server-linux-2017"
---
# Deprecated Database Engine features in SQL Server 2017

[!INCLUDE[SQL Server 2017](../includes/applies-to-version/sqlserver2017.md)]

  This article describes the deprecated [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] features that are still available in [!INCLUDE[sssql17-md](../includes/sssql17-md.md)]. Deprecated features shouldn't be used in new applications.

When a feature is marked deprecated, it means:

- The feature is in maintenance mode only. No new changes will be done, including those related to addressing inter-operability with new features.
- We strive not to remove a deprecated feature from future releases to make upgrades easier. However, under rare situations, we may choose to permanently discontinue (remove) the feature from [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] if it limits future innovations.
- For new development work, don't use deprecated features. For existing applications, plan to modify applications that currently use these features as soon as possible.

You can monitor the use of deprecated features by using the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Deprecated Features Object performance counter and trace events. For more information, see [Use SQL Server Objects](../relational-databases/performance-monitor/use-sql-server-objects.md).

The values of these counters are also available by executing the following statement:

```sql
SELECT * FROM sys.dm_os_performance_counters
WHERE object_name = 'SQLServer:Deprecated Features';
```

> [!NOTE]  
> This list is identical to the [!INCLUDE[sssql15-md](../includes/sssql16-md.md)] list. There are no new deprecated or discontinued Database Engine features announced for [!INCLUDE[sssql17-md](../includes/sssql17-md.md)].

## Features deprecated in the next version of SQL Server

The following [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] features won't be supported in a future version of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. Don't use these features in new development work, and modify applications that currently use these features as soon as possible. The **Feature name** value appears in trace events as the ObjectName and in performance counters and `sys.dm_os_performance_counters` as the instance name. The **Feature ID** value appears in trace events as the ObjectId.

### Back up and Restore

| Deprecated feature | Replacement | Feature name | Feature ID |
|--------------------|-------------|--------------|------------|
| RESTORE { DATABASE &#124; LOG } WITH [MEDIA]PASSWORD continues to be deprecated.<br /><br />BACKUP { DATABASE &#124; LOG } WITH PASSWORD and BACKUP { DATABASE &#124; LOG } WITH MEDIAPASSWORD are discontinued. | None. | BACKUP DATABASE or LOG WITH PASSWORD<br /><br />BACKUP DATABASE or LOG WITH MEDIAPASSWORD | 104<br /><br /> 103 |

### Compatibility levels

| Deprecated feature | Replacement | Feature name | Feature ID |
|--------------------|-------------|--------------|------------|
Upgrade from version 100 ([!INCLUDE[ssKatmai](../includes/ssKatmai-md.md)] and [!INCLUDE[ssKilimanjaro](../includes/ssKilimanjaro-md.md)]). | When a [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] version goes out of [support](/lifecycle/products/?products=sql-server), the associated Database Compatibility Levels are marked deprecated. However, we continue to support applications certified on any supported database compatibility level as long as possible, to make the upgrades easier. For more information about compatibility levels, see [ALTER DATABASE Compatibility Level &#40;Transact-SQL&#41;](../t-sql/statements/alter-database-transact-sql-compatibility-level.md). | Database compatibility level 100 | 108 |

### Database objects

| Deprecated feature | Replacement | Feature name | Feature ID |
|--------------------|-------------|--------------|------------|
| Ability to return result sets from triggers | None | Returning results from trigger | 12 |

### Encryption

| Deprecated feature | Replacement | Feature name | Feature ID |
|--------------------|-------------|--------------|------------|
| Encryption using RC4 or RC4_128 is deprecated and is scheduled to be removed in the next version. Decrypting RC4 and RC4_128 aren't deprecated. | Use another encryption algorithm such as AES. | Deprecated encryption algorithm | 253 |
| Using the MD2, MD4, MD5, SHA, and SHA1 is deprecated. | Use SHA2_256 or SHA2_512 instead. Older algorithms continue working, but they raise a deprecation event. |Deprecated hash algorithm | None |

### Remote servers

| Deprecated feature | Replacement | Feature name | Feature ID |
|--------------------|-------------|--------------|------------|
| sp_addremotelogin<br /><br />sp_addserver <br /><br /> sp_dropremotelogin <br /><br /> sp_helpremotelogin <br /><br /> sp_remoteoption|Replace remote servers by using linked servers. sp_addserver can only be used with the local option. | sp_addremotelogin<br /><br />sp_addserver <br /><br /> sp_dropremotelogin <br /><br /> sp_helpremotelogin <br /><br > sp_remoteoption | 70 <br /><br /> 69 <br /><br /> 71 <br /><br /> 72 <br /><br /> 73 |
| \@\@remserver | Replace remote servers by using linked servers. | None | None |
| SET REMOTE_PROC_TRANSACTIONS|Replace remote servers by using linked servers. | SET REMOTE_PROC_TRANSACTIONS | 110 |

### Transact-SQL

| Deprecated feature | Replacement | Feature name | Feature ID |
|--------------------|-------------|--------------|------------|
| **SET ROWCOUNT** for **INSERT**, **UPDATE**, and **DELETE** statements | TOP keyword | SET ROWCOUNT | 109 |
| HOLDLOCK table hint without parenthesis. | Use HOLDLOCK with parenthesis. | HOLDLOCK table hint without parenthesis | 167 |

## Features deprecated in a future version of SQL Server

The following SQL Server Database Engine features are supported in the next version of SQL Server. The specific version of SQL Server hasn't been determined.

### Back up and restore

| Deprecated feature | Replacement | Feature name |
|--------------------|-------------|--------------|
| BACKUP { DATABASE &#124; LOG } TO TAPE <br /><br /> BACKUP { DATABASE &#124; LOG } TO *device_that_is_a_tape*|BACKUP { DATABASE &#124; LOG } TO DISK <br /><br /> BACKUP { DATABASE &#124; LOG } TO *device_that_is_a_disk* | BACKUP DATABASE or LOG TO TAPE |
| sp_addumpdevice '**tape**' | sp_addumpdevice '**disk**' | ADDING TAPE DEVICE |
| sp_helpdevice | sys.backup_devices | sp_helpdevice |

### Compatibility levels

| Deprecated feature | Replacement | Feature name |
|--------------------|-------------|--------------|
| sp_dbcmptlevel|ALTER DATABASE ... SET COMPATIBILITY_LEVEL. For more information, see [ALTER DATABASE Compatibility Level &#40;Transact-SQL&#41;](../t-sql/statements/alter-database-transact-sql-compatibility-level.md). | sp_dbcmptlevel |
| Database compatibility level 110 and 120. | Plan to upgrade the database and application for a future release. However, we continue to support applications certified on any supported database compatibility level as long as possible, to make the upgrades easier. For more information about compatibility levels, see [ALTER DATABASE Compatibility Level &#40;Transact-SQL&#41;](../t-sql/statements/alter-database-transact-sql-compatibility-level.md). | Database compatibility level 110 <br /><br /> Database compatibility level 120 |

### Collations

| Deprecated feature | Replacement | Feature name |
|--------------------|-------------|--------------|
| Korean_Wansung_Unicode <br /><br /> Lithuanian_Classic <br /><br /> SQL_AltDiction_CP1253_CS_AS | None. These collations exist in  SQL Server 2005 (9.x), but aren't visible through fn_helpcollations. | Korean_Wansung_Unicode <br /><br /> Lithuanian_Classic <br /><br /> SQL_AltDiction_CP1253_CS_AS |
| Hindi <br /><br /> Macedonian | These collations exist in  SQL Server 2005 (9.x) and higher, but aren't visible through fn_helpcollations. Use Macedonian_FYROM_90 and Indic_General_90 instead.|Hindi <br /><br /> Macedonian |
| Azeri_Latin_90 <br /><br /> Azeri_Cyrilllic_90 | Azeri_Latin_100 <br /><br /> Azeri_Cyrilllic_100 | Azeri_Latin_90 <br /><br /> Azeri_Cyrilllic_90 |

### Data types

| Deprecated feature | Replacement | Feature name |
|--------------------|-------------|--------------|
| sp_addtype <br /><br /> sp_droptype|CREATE TYPE<br /><br /> DROP TYPE | sp_addtype<br /><br /> sp_droptype |
| **timestamp** syntax for **rowversion** data type | **rowversion** data type syntax | TIMESTAMP |
| Ability to insert null values into **timestamp** columns. | Use a DEFAULT instead. | INSERT NULL into TIMESTAMP columns |
| 'text in row' table option|Use **varchar(max)**, **nvarchar(max)**, and **varbinary(max)** data types. For more information, see [sp_tableoption &#40;Transact-SQL&#41;](../relational-databases/system-stored-procedures/sp-tableoption-transact-sql.md).|Text in row table option |
| Data types:<br /><br /> **text**<br /><br /> **ntext**<br /><br /> **image**|Use **varchar(max)**, **nvarchar(max)**, and **varbinary(max)** data types.|Data types: **text**, **ntext**, or **image** |

### Database management

| Deprecated feature | Replacement | Feature name |
|--------------------|-------------|--------------|
| sp_attach_db <br /><br /> sp_attach_single_file_db|CREATE DATABASE statement with the FOR ATTACH option. To rebuild multiple log files, when one or more have a new location, use the FOR ATTACH_REBUILD_LOG option. | sp_attach_db <br /><br /> sp_attach_single_file_db |
| sp_certify_removable<br /><br /> sp_create_removable|sp_detach_db|sp_certify_removable<br /><br /> sp_create_removable |
| sp_dbremove | DROP DATABASE | sp_dbremove |
| sp_renamedb | MODIFY NAME in ALTER DATABASE | sp_renamedb |

### Database objects

| Deprecated feature | Replacement | Feature name |
|--------------------|-------------|--------------|
| CREATE DEFAULT<br /><br /> DROP DEFAULT<br /><br /> sp_bindefault<br /><br /> sp_unbindefault|DEFAULT keyword in CREATE TABLE and ALTER TABLE|CREATE_DROP_DEFAULT<br /><br /> sp_bindefault<br /><br /> sp_unbindefault |
| CREATE RULE<br /><br /> DROP RULE<br /><br /> sp_bindrule<br /><br /> sp_unbindrule | CHECK keyword in CREATE TABLE and ALTER TABLE | CREATE_DROP_RULE<br /><br /> sp_bindrule<br /><br /> sp_unbindrule |
| sp_change_users_login | Use ALTER USER. | sp_change_users_login |
| sp_depends | sys.dm_sql_referencing_entities and sys.dm_sql_referenced_entities | sp_depends |
| sp_getbindtoken | Use MARS or distributed transactions. | sp_getbindtoken |

### Database options

| Deprecated feature | Replacement | Feature name |
|--------------------|-------------|--------------|
| sp_bindsession | Use MARS or distributed transactions. | sp_bindsession |
| sp_resetstatus | ALTER DATABASE SET { ONLINE &#124; EMERGENCY } | sp_resetstatus
| TORN_PAGE_DETECTION option of ALTER DATABASE|PAGE_VERIFY TORN_PAGE_DETECTION option of ALTER DATABASE | ALTER DATABASE WITH TORN_PAGE_DETECTION |

### DBCC

| Deprecated feature | Replacement | Feature name |
|--------------------|-------------|--------------|
| DBCC DBREINDEX|REBUILD option of ALTER INDEX. | DBCC DBREINDEX |
| DBCC INDEXDEFRAG|REORGANIZE option of ALTER INDEX | DBCC INDEXDEFRAG |
| DBCC SHOWCONTIG|sys.dm_db_index_physical_stats | DBCC SHOWCONTIG |
| DBCC PINTABLE<br /><br /> DBCC UNPINTABLE | Has no effect. | DBCC [UN]PINTABLE |

### Extended properties

| Deprecated feature | Replacement | Feature name |
|--------------------|-------------|--------------|
| Level0type = 'type' and Level0type = 'USER' to add extended properties to level-1 or level-2 type objects. | Use Level0type = 'USER' only to add an extended property directly to a user or role.<br /><br /> Use Level0type = 'SCHEMA' to add an extended property to level-1 types such as TABLE or VIEW, or level-2 types such as COLUMN or TRIGGER. For more information, see [sp_addextendedproperty &#40;Transact-SQL&#41;](../relational-databases/system-stored-procedures/sp-addextendedproperty-transact-sql.md). | EXTPROP_LEVEL0TYPE<br /><br /> EXTPROP_LEVEL0USER |

### Extended stored procedures

| Deprecated feature | Replacement | Feature name |
|--------------------|-------------|--------------|
| xp_grantlogin<br /><br /> xp_revokelogin<br /><br /> xp_loginConfig|Use CREATE LOGIN<br /><br /> Use DROP LOGIN IsIntegratedSecurityOnly argument of SERVERPROPERTY|xp_grantlogin<br /><br /> xp_revokelogin<br /><br /> xp_loginconfig |

### Extended stored procedures programming

| Deprecated feature | Replacement | Feature name |
|--------------------|-------------|--------------|
| srv_alloc<br /><br /> srv_convert<br /><br /> srv_describe<br /><br /> srv_getbindtoken<br /><br /> srv_got_attention<br /><br /> srv_message_handler<br /><br /> srv_paramdata<br /><br /> srv_paraminfo<br /><br /> srv_paramlen<br /><br /> srv_parammaxlen<br /><br /> srv_paramname<br /><br /> srv_paramnumber<br /><br /> srv_paramset<br /><br /> srv_paramsetoutput<br /><br /> srv_paramstatus<br /><br /> srv_paramtype<br /><br /> srv_pfield<br /><br /> srv_pfieldex<br /><br /> srv_rpcdb<br /><br /> srv_rpcname<br /><br /> srv_rpcnumber<br /><br /> srv_rpcoptions<br /><br /> srv_rpcowner<br /><br /> srv_rpcparams<br /><br /> srv_senddone<br /><br /> srv_sendmsg<br /><br /> srv_sendrow<br /><br /> srv_setcoldata<br /><br /> srv_setcollen<br /><br /> srv_setutype<br /><br /> srv_willconvert<br /><br /> srv_wsendmsg | Use CLR Integration instead. | XP_API |
| sp_addextendedproc<br /><br /> sp_dropextendedproc<br /><br /> sp_helpextendedproc | Use CLR Integration instead. | sp_addextendedproc<br /><br /> sp_dropextendedproc<br /><br /> sp_helpextendedproc |
| xp_grantlogin<br /><br /> xp_revokelogin<br /><br /> xp_loginConfig|Use CREATE LOGIN<br /><br /> Use DROP LOGIN IsIntegratedSecurityOnly argument of SERVERPROPERTY | xp_grantlogin<br /><br /> xp_revokelogin<br /><br /> xp_loginconfig |

### High availability

| Deprecated feature | Replacement | Feature name |
|--------------------|-------------|--------------|
| database mirroring | Always On availability groups<br /><br /> If your edition of SQL Server doesn't support Always On availability groups, use log shipping. | DATABASE_MIRRORING |

### Index options

| Deprecated feature | Replacement | Feature name |
|--------------------|-------------|--------------|
| sp_indexoption | ALTER INDEX | sp_indexoption |
| CREATE TABLE, ALTER TABLE, or CREATE INDEX syntax without parentheses around the options. | Rewrite the statement to use the current syntax. | INDEX_OPTION |

### Instance options

| Deprecated feature | Replacement | Feature name |
|--------------------|-------------|--------------|
| sp_configure option 'allow updates'|System tables are no longer updatable. Setting has no effect. | sp_configure 'allow updates' |
| sp_configure options: <br /><br /> 'locks' <br /><br /> 'open objects'<br /><br /> 'set working set size' | Now automatically configured. Setting has no effect. | sp_configure 'locks'<br /><br /> sp_configure 'open objects'<br /><br /> sp_configure 'set working set size' |
| sp_configure option 'priority boost' | System tables are no longer updatable. Setting has no effect. Use the Windows start /high ... program.exe option instead. | sp_configure 'priority boost' |
| sp_configure option 'remote proc trans' | System tables are no longer updatable. Setting has no effect. | sp_configure 'remote proc trans' |

### Linked servers

| Deprecated feature | Replacement | Feature name |
|--------------------|-------------|--------------|
| Specifying the SQLOLEDB provider for linked servers. | [Microsoft OLE DB Driver (MSOLEDBSQL) for SQL Server](../connect/oledb/oledb-driver-for-sql-server.md)  | SQLOLEDB for linked servers |

### Metadata

| Deprecated feature | Replacement | Feature name |
|--------------------|-------------|--------------|
| FILE_ID<br /><br /> INDEXKEY_PROPERTY | FILE_IDEX<br /><br /> sys.index_columns | FILE_ID<br /><br /> INDEXKEY_PROPERTY |

### Native XML Web Services

| Deprecated feature | Replacement | Feature name |
|--------------------|-------------|--------------|
| The CREATE ENDPOINT or ALTER ENDPOINT statement with the FOR SOAP option.<br /><br /> sys.endpoint_webmethods<br /><br /> sys.soap_endpoints|Use Windows Communications Foundation (WCF) or ASP.NET instead. | CREATE/ALTER ENDPOINT<br /><br /> sys.endpoint_webmethods<br /><br /> EXT_soap_endpoints<br /><br /> sys.soap_endpoints |

### Other

| Deprecated feature | Replacement | Feature name |
|--------------------|-------------|--------------|
| DB-Library<br /><br />Embedded SQL for C|Although the Database Engine still supports connections from existing applications that use the DB-Library and Embedded SQL APIs, it doesn't include the files or documentation required to do programming work on applications that use these APIs. A future version of the SQL Server Database Engine drops support for connections from DB-Library or Embedded SQL applications. Don't use DB-Library or Embedded SQL to develop new applications. Remove any dependencies on either DB-Library or Embedded SQL when you are modifying existing applications. Instead of these APIs, use the SQLClient namespace or an API such as ODBC. SQL Server 2019 (15.x) doesn't include the DB-Library DLL required to run these applications. To run DB-Library or Embedded SQL applications, you must have available the DB-Library DLL from SQL Server version 6.5, SQL Server 7.0, or SQL Server 2000 (8.x). | None |

### Security

| Deprecated feature | Replacement | Feature name |
|--------------------|-------------|--------------|
| The ALTER LOGIN WITH SET CREDENTIAL syntax | Replaced by the new ALTER LOGIN ADD and DROP CREDENTIAL syntax | ALTER LOGIN WITH SET CREDENTIAL |
| sp_addapprole<br /><br /> sp_dropapprole | CREATE APPLICATION ROLE<br /><br /> DROP APPLICATION ROLE | sp_addapprole<br /><br /> sp_dropapprole |
| sp_addlogin<br /><br /> sp_droplogin | CREATE LOGIN<br /><br /> DROP LOGIN|sp_addlogin<br /><br /> sp_droplogin |
| sp_adduser<br /><br /> sp_dropuser | CREATE USER<br /><br /> DROP USER|sp_adduser<br /><br /> sp_dropuser |
| sp_grantdbaccess<br /><br /> sp_revokedbaccess|CREATE USER<br /><br /> DROP USER|sp_grantdbaccess<br /><br /> sp_revokedbaccess |
| sp_addrole<br /><br /> sp_droprole | CREATE ROLE<br /><br /> DROP ROLE|sp_addrole<br /><br /> sp_droprole |
| sp_approlepassword<br /><br /> sp_password | ALTER APPLICATION ROLE<br /><br /> ALTER LOGIN|sp_approlepassword<br /><br /> sp_password |
| sp_changedbowner|ALTER AUTHORIZATION | sp_changedbowner |
| sp_changeobjectowner|ALTER SCHEMA or ALTER AUTHORIZATION | sp_changeobjectowner |
| sp_control_dbmasterkey_password | A master key must exist and password must be correct.|sp_control_dbmasterkey_password |
| sp_defaultdb<br /><br /> sp_defaultlanguage | ALTER LOGIN|sp_defaultdb<br /><br /> sp_defaultlanguage |
| sp_denylogin<br /><br /> sp_grantlogin<br /><br /> sp_revokelogin|ALTER LOGIN DISABLE<br /><br /> CREATE LOGIN<br /><br /> DROP LOGIN|sp_denylogin<br /><br /> sp_grantlogin<br /><br /> sp_revokelogin |
| USER_ID|DATABASE_PRINCIPAL_ID | USER_ID |
| sp_srvrolepermission<br /><br /> sp_dbfixedrolepermission|These stored procedures return information that was correct in [!INCLUDE[ssVersion2000](../includes/ssversion2000-md.md)]. The output doesn't reflect changes to the permissions hierarchy implemented in SQL Server 2008. For more information, see [Permissions of Fixed Server Roles](https://msdn.microsoft.com/library/ms175892\(SQL.100\).aspx). | sp_srvrolepermission<br /><br /> sp_dbfixedrolepermission |
| GRANT ALL<br /><br /> DENY ALL<br /><br /> REVOKE ALL|GRANT, DENY, and REVOKE-specific permissions.|ALL Permission |
| PERMISSIONS intrinsic function | Query sys.fn_my_permissions instead. | PERMISSIONS |
| SETUSER | EXECUTE AS | SETUSER |
| RC4 and DESX encryption algorithms|Use another algorithm such as AES. | DESX algorithm |

### Server Configuration Options

| Deprecated feature | Replacement | Feature name |
|--------------------|-------------|--------------|
| c2 audit option default trace enabled option<br /><br /> default trace enabled option | [common criteria compliance enabled Server Configuration Option](../database-engine/configure-windows/common-criteria-compliance-enabled-server-configuration-option.md)<br /><br /> [Extended Events](../relational-databases/extended-events/extended-events.md) | sp_configure 'c2 audit mode'<br /><br />sp_configure 'default trace enabled' |

### SMO classes

| Deprecated feature | Replacement | Feature name |
|--------------------|-------------|--------------|
| *Microsoft.SQLServer. Management.Smo.Information* class<br /><br />*Microsoft.SQLServer. Management.Smo.Settings* class<br /><br />*Microsoft.SQLServer.Management. Smo.DatabaseOptions* class<br /><br />*Microsoft.SqlServer.Management.Smo. DatabaseDdlTrigger.NotForReplication* property | *Microsoft.SqlServer.  Management.Smo.Server* class<br /><br />**Microsoft.SqlServer.  Management.Smo.Server* class<br /><br />*Microsoft.SqlServer. Management.Smo.Database* class<br /><br />None | None |

### SQL Server Agent

| Deprecated feature | Replacement | Feature name |
|--------------------|-------------|--------------|
| **net send** notification<br /><br />Pager notification | E-mail notification<br /><br />E-mail notification | None |

### SQL Server Management Studio

| Deprecated feature | Replacement | Feature name |
|--------------------|-------------|--------------|
| Solution Explorer integration in SQL Server Management Studio | | None |

### System stored procedures and functions

| Deprecated feature | Replacement | Feature name |
|--------------------|-------------|--------------|
| sp_db_increased_partitions | None. Support for increased partitions is available by default in SQL Server 2019 (15.x). | sp_db_increased_partitions |
| fn_virtualservernodes<br /><br />fn_servershareddrives | sys.dm_os_cluster_nodes<br /><br />sys.dm_io_cluster_shared_drives | fn_virtualservernodes<br /><br /> fn_servershareddrives |
| fn_get_sql | sys.dm_exec_sql_text | fn_get_sql |
| sp_lock | sys.dm_tran_locks | sp_lock |

### System tables

| Deprecated feature | Replacement | Feature name |
|--------------------|-------------|--------------|
| sysaltfiles<br /><br />syscacheobjects<br /><br />syscolumns<br /><br />syscomments<br /><br />sysconfigures<br /><br />sysconstraints<br /><br />syscurconfigs<br /><br />sysdatabases<br /><br />sysdepends<br /><br />sysdevices<br /><br />sysfilegroups<br /><br />sysfiles<br /><br />sysforeignkeys<br /><br />sysfulltextcatalogs<br /><br />sysindexes<br /><br />sysindexkeys<br /><br />syslockinfo<br /><br />syslogins<br /><br />sysmembers<br /><br />sysmessages<br /><br />sysobjects<br /><br />sysoledbusers<br /><br />sysopentapes<br /><br />sysperfinfo<br /><br />syspermissions<br /><br />sysprocesses<br /><br />sysprotects<br /><br />sysreferences<br /><br />sysremotelogins<br /><br />sysservers<br /><br />systypes<br /><br />sysusers|Compatibility views. For more information, see [Compatibility Views &#40;Transact-SQL&#41;](~/relational-databases/system-compatibility-views/system-compatibility-views-transact-sql.md).<br /><br />**Important:** The compatibility views don't expose metadata for features that were introduced in  SQL Server 2005 (9.x). We recommend that you upgrade your applications to use catalog views. For more information, see [Catalog Views &#40;Transact-SQL&#41;](../relational-databases/system-catalog-views/catalog-views-transact-sql.md). | sysaltfiles<br /><br />syscacheobjects<br /><br />syscolumns<br /><br />syscomments<br /><br />sysconfigures<br /><br />sysconstraints<br /><br />syscurconfigs<br /><br />sysdatabases<br /><br />sysdepends<br /><br />sysdevices<br /><br />sysfilegroups<br /><br />sysfiles<br /><br />sysforeignkeys<br /><br />sysfulltextcatalogs<br /><br />sysindexes<br /><br />sysindexkeys<br /><br />syslockinfo<br /><br />syslogins<br /><br />sysmembers<br /><br />sysmessages<br /><br />sysobjects<br /><br />sysoledbusers<br /><br />sysopentapes<br /><br />sysperfinfo<br /><br />syspermissions<br /><br />sysprocesses<br /><br />sysprotects<br /><br />sysreferences<br /><br />sysremotelogins<br /><br />sysservers<br /><br />systypes<br /><br />sysusers |
| sys.numbered_procedures<br /><br />sys.numbered_procedure_parameters | None | numbered_procedures<br /><br />numbered_procedure_parameters |

### SQL Trace stored procedures, functions, and catalog views

| Deprecated feature | Replacement | Feature name |
|--------------------|-------------|--------------|
| sp_trace_create<br /><br />sp_trace_setevent<br /><br />sp_trace_setfilter<br /><br />sp_trace_setstatus<br /><br />fn_trace_geteventinfo<br /><br />fn_trace_getfilterinfo<br /><br />fn_trace_getinfo<br /><br />fn_trace_gettable<br /><br />sys.traces<br /><br />sys.trace_events<br /><br />sys.trace_event_bindings<br /><br />sys.trace_categories<br /><br />sys.trace_columns<br /><br />sys.trace_subclass_values|[Extended Events](../relational-databases/extended-events/extended-events.md) | sp_trace_create<br /><br />sp_trace_setevent<br /><br />sp_trace_setfilter<br /><br />sp_trace_setstatus<br /><br />fn_trace_geteventinfo<br /><br />fn_trace_getfilterinfo<br /><br />fn_trace_getinfo<br /><br />fn_trace_gettable<br /><br />sys.traces<br /><br />sys.trace_events<br /><br />sys.trace_event_bindings<br /><br />sys.trace_categories<br /><br />sys.trace_columns<br /><br />sys.trace_subclass_values |

### System views

| Deprecated feature | Replacement | Feature name |
|--------------------|-------------|--------------|
| sys.sql_dependencies|sys.sql_expression_dependencies|sys.sql_dependencies |

### Table compression

| Deprecated feature | Replacement | Feature name |
|--------------------|-------------|--------------|
| The use of the vardecimal storage format. | Vardecimal storage format is deprecated. SQL Server 2019 (15.x) data compression, compresses decimal values, and other data types. We recommend that you use data compression instead of the vardecimal storage format. | Vardecimal storage format |
| Use of the sp_db_vardecimal_storage_format procedure.|Vardecimal storage format is deprecated. SQL Server 2019 (15.x) data compression, compresses decimal values as well as other data types. We recommend that you use data compression instead of the vardecimal storage format. | sp_db_vardecimal_storage_format |
| Use of the sp_estimated_rowsize_reduction_for_vardecimal procedure.|Use data compression and the sp_estimate_data_compression_savings procedure instead. |sp_estimated_rowsize_reduction_for_vardecimal |

### Text pointers

| Deprecated feature | Replacement | Feature name |
|--------------------|-------------|--------------|
| WRITETEXT<br /><br />UPDATETEXT<br /><br />READTEXT|None|UPDATETEXT or WRITETEXT<br /><br />READTEXT |
| TEXTPTR()<br /><br />TEXTVALID() | None | TEXTPTR<br /><br />TEXTVALID |

### Transact-SQL

| Deprecated feature | Replacement | Feature name |
|--------------------|-------------|--------------|
| `::` function-calling sequence | Replaced by SELECT *column_list* FROM sys.\<*function_name*>().<br /><br />For example, replace `SELECT * FROM ::fn_virtualfilestats(2,1)`with `SELECT * FROM sys.fn_virtualfilestats(2,1)`. | '::' function calling syntax |
| Three-part and four-part column references. | Two-part names are the standard-compliant behavior.|More than two-part column name |
| A string enclosed in quotation marks used as a column alias for an expression in a SELECT list:<br /><br />'*string_alias*' = *expression* | *expression* [AS] *column_alias*<br /><br />*expression* [AS] [*column_alias*]<br /><br />*expression* [AS] "*column_alias*"<br /><br />*expression* [AS] '*column_alias*'<br /><br />*column_alias* = *expression* | String literals as column aliases |
| Numbered procedures | None. Don't use. | ProcNums |
| *table_name.index_name* syntax in DROP INDEX|*index_name* ON *table_name* syntax in DROP INDEX.|DROP INDEX with two-part name |
| Not ending  Transact-SQL  statements with a semicolon.|End  Transact-SQL  statements with a semicolon ( ; ). | None |
| GROUP BY ALL|Use custom case-by-case solution with UNION or derived table. | GROUP BY ALL |
| ROWGUIDCOL as a column name in DML statements.|Use $rowguid.|ROWGUIDCOL |
| IDENTITYCOL as a column name in DML statements.|Use $identity.|IDENTITYCOL |
| Use of #, ## as temporary table and temporary stored procedure names. | Use at least one additional character.|'#' and '##' as the name of temporary tables and stored procedures
| Use of \@, \@\@, or \@\@ as  Transact-SQL  identifiers. | Don't use \@ or \@\@ or names that begin with \@\@ as identifiers. | '\@' and names that start with '\@\@' as  Transact-SQL identifiers |
| Use of DEFAULT keyword as default value.|Don't use the word DEFAULT as a default value. | DEFAULT keyword as a default value |
| Use of a space as a separator between table hints.|Use a comma to separate table hints. | Multiple table hints without comma |
| The select list of an aggregate indexed view must contain COUNT_BIG (\*) in 90 compatibility mode | Use COUNT_BIG (\*). | Index view selects list without COUNT_BIG(\*) |
| The indirect application of table hints to an invocation of a multi-statement table-valued function (TVF) through a view.|None.|Indirect TVF hints |
| ALTER DATABASE syntax:<br /><br />MODIFY FILEGROUP READONLY<br /><br />MODIFY FILEGROUP READWRITE | MODIFY FILEGROUP READ_ONLY<br /><br />MODIFY FILEGROUP READ_WRITE | MODIFY FILEGROUP READONLY<br /><br />MODIFY FILEGROUP READWRITE |
| SET ANSI_NULLS OFF and ANSI_NULLS OFF database option<br /><br />SET ANSI_PADDING OFF and ANSI_PADDING OFF database option<br /><br />SET CONCAT_NULL_YIELDS_NULL OFF and CONCAT_NULL_YIELDS_NULL OFF database option<br /><br />SET OFFSETS | None. <br /><br /> ANSI_NULLS, ANSI_PADDING and CONCAT_NULLS_YIELDS_NULL are always set to ON. SET OFFSETS are unavailable. | SET ANSI_NULLS OFF <br /><br /> SET ANSI_PADDING OFF<br /><br />SET CONCAT_NULL_YIELDS_NULL OFF<br /><br />SET OFFSETS<br /><br />ALTER DATABASE SET ANSI_NULLS OFF<br /><br />ALTER DATABASE SET ANSI_PADDING OFF <br /><br /> ALTER DATABASE SET CONCAT_NULL_YIELDS_NULL OFF |
| SET FMTONLY | [sys.dm_exec_describe_first_result_set &#40;Transact-SQL&#41;](../relational-databases/system-dynamic-management-views/sys-dm-exec-describe-first-result-set-transact-sql.md), [sys.dm_exec_describe_first_result_set_for_object &#40;Transact-SQL&#41;](../relational-databases/system-dynamic-management-views/sys-dm-exec-describe-first-result-set-for-object-transact-sql.md), [sp_describe_first_result_set &#40;Transact-SQL&#41;](../relational-databases/system-stored-procedures/sp-describe-first-result-set-transact-sql.md), and [sp_describe_undeclared_parameters &#40;Transact-SQL&#41;](../relational-databases/system-stored-procedures/sp-describe-undeclared-parameters-transact-sql.md). | SET FMTONLY |
| Specifying NOLOCK or READUNCOMMITTED in the FROM clause of an UPDATE or DELETE statement. | Remove the NOLOCK or READUNCOMMITTED table hints from the FROM clause. | NOLOCK or READUNCOMMITTED in UPDATE or DELETE |
| Specifying table hints without using the WITH keyword. | Use WITH. | Table hint without WITH |
| INSERT_HINTS | | INSERT_HINTS |

### Tools

| Deprecated feature | Replacement | Feature name |
|--------------------|-------------|--------------|
| SQL Server Profiler for Trace Capture | Use Extended Events Profiler embedded in SQL Server Management Studio.|SQL Server Profiler |
| SQL Server Profiler for Trace Replay | [SQL Server Distributed Replay](../tools/distributed-replay/sql-server-distributed-replay.md) |

### Trace Management Objects

| Deprecated feature | Replacement | Feature name |
|--------------------|-------------|--------------|
| Microsoft.SqlServer.Management.Trace namespace (contains the APIs for SQL Server Trace and Replay objects)|Trace Configuration: <xref:Microsoft.SqlServer.Management.XEvent><br /><br />Trace Reading: <xref:Microsoft.SqlServer.XEvent.Linq><br /><br />Trace Replay: None | |

### XML

| Deprecated feature | Replacement | Feature name |
|--------------------|-------------|--------------|
| Inline XDR Schema Generation | The XMLDATA directive to the FOR XML option is deprecated. Use XSD generation in the case of RAW and AUTO modes. There is no replacement for the XMLDATA directive in EXPLICT mode. | XMLDATA |

> [!NOTE]  
> The cookie **OUTPUT** parameter for **sp_setapprole** is currently documented as **varbinary(8000)** which is the correct maximum length. However the current implementation returns **varbinary(50)**. If developers have allocated **varbinary(50)** the application might require changes if the cookie return size increases in a future release. Though not a deprecation issue this is mentioned in this topic because the application adjustments are similar. For more information, see [sp_setapprole &#40;Transact-SQL&#41;](../relational-databases/system-stored-procedures/sp-setapprole-transact-sql.md).

## See also

- [Discontinued Database Engine Functionality in SQL Server 2016](./discontinued-database-engine-functionality-in-sql-server.md)
