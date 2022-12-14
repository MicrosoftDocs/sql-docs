---
title: "SQL Server, Deprecated Features object"
description: Learn about the SQLServer:Deprecated Features object, which provides a counter to monitor the features designated as deprecated. 
ms.custom: ""
ms.date: "07/13/2021"
ms.service: sql
ms.reviewer: ""
ms.subservice: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "SQLServer:Deprecated Features"
  - "performance counters [SQL Server], deprecated features"
  - "deprecation [SQL Server], performance counters"
  - "Deprecated Features object"
author: WilliamDAssafMSFT
ms.author: wiassaf
---
# SQL Server, Deprecated Features object
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  The SQLServer:Deprecated Features object in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provides a counter to monitor the features designated as deprecated. In each case the counter provides a usage count that lists the number of times the deprecated feature was encountered since [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] last started.  
  
 The value of these counters are also available by executing the following statement:  
  
```sql  
SELECT * FROM sys.dm_os_performance_counters   
WHERE object_name LIKE '%SQL%Deprecated Features%';  
```  

This following table describes the SQL Server **Deprecated Features** performance object.

|**SQL Server Deprecated Features counter**|Description|  
|-------------|-----------------|  
|**Usage**|Feature usage since last SQL Server startup.|
  
 The following table describes the SQL Server Deprecated Features counter instances.  
  
|SQL Server Deprecated Features counter instances|Description|  
|------------------------------------------------------|-----------------|  
|'#' and '##' as the name of temporary tables and stored procedures|An identifier was encountered that did not contain any characters other than #. Use at least one additional character. Occurs once per compilation.|  
|'::' function calling syntax|The :: function calling syntax was encountered for a table-valued function. Replace with `SELECT column_list FROM` *< function_name>*`()`. For example, replace `SELECT * FROM ::fn_virtualfilestats(2,1)`with `SELECT * FROM sys.fn_virtualfilestats(2,1)`. Occurs once per compilation.|  
|'\@' and names that start with '\@\@' as [!INCLUDE[tsql](../../includes/tsql-md.md)] identifiers|An identifier was encountered that began with \@ or \@\@. Do not use \@ or \@v@ or names that begin with \@\@ as identifiers. Occurs once per compilation.|  
|ADDING TAPE DEVICE|The deprecated feature `sp_addumpdevice'**tape**'` was encountered. Use `sp_addumpdevice'**disk**'` instead. Occurs once per use.|  
|ALL Permission|Total number of times the GRANT ALL, DENY ALL, or REVOKE ALL syntax was encountered. Modify the syntax to deny specific permissions. Occurs once per query.|  
|ALTER DATABASE WITH TORN_PAGE_DETECTION|Total number of times the deprecated feature TORN_PAGE_DETECTION option of ALTER DATABASE has been used since the server instance was started. Use the PAGE_VERIFY syntax instead. Occurs once per use in a DDL statement.|  
|ALTER LOGIN WITH SET CREDENTIAL|The deprecated feature syntax `ALTER LOGIN WITH SET CREDENTIAL` or `ALTER LOGIN WITH NO CREDENTIAL` was encountered. Use ADD or DROP CREDENTIAL syntax instead. Occurs once per compilation.|  
|asymmetric_keys || 
|asymmetric_keys.attested_by ||
|Azeri_Cyrillic_90 |Event occurs once per database start and once per collation use. Plan to modify applications that use this collation.|  
|Azeri_Latin_90|Event occurs once per database start and once per collation use. Plan to modify applications that use this collation.|  
|BACKUP DATABASE or LOG TO TAPE|The deprecated feature BACKUP { DATABASE &#124; LOG } TO TAPE or BACKUP { DATABASE &#124; LOG } TO *device_that_is_a_tape* was encountered.<br /><br /> Use BACKUP { DATABASE &#124; LOG } TO DISK or BACKUP { DATABASE &#124; LOG } TO *device_that_is_a_disk*, instead. Occurs once per use.|  
|BACKUP DATABASE or LOG WITH MEDIAPASSWORD|The deprecated feature BACKUP DATABASE WITH MEDIAPASSWORD or BACKUP LOG WITH MEDIAPASSWORD was encountered. Do not use WITH MEDIAPASSWORD.|  
|BACKUP DATABASE or LOG WITH PASSWORD|The deprecated feature BACKUP DATABASE WITH PASSWORD or BACKUP LOG WITH PASSWORD was encountered. Do not use WITH PASSWORD.|  
|certificates||
|certificates.attested_by||
|Create/alter SOAP endpoint|Native XML Web Services is deprecated. Use Windows Communications Foundation (WCF) or ASP.NET instead.|
|COMPUTE [BY]|The COMPUTE or COMPUTE BY syntax was encountered. Rewrite the query to use GROUP BY with ROLLUP. Occurs once per compilation.|  
|CREATE FULLTEXT CATALOG IN PATH|A CREATE FULLTEXT CATALOG statement with the IN PATH clause was encountered. This clause has no effect in this version of SQL Server. Occurs once per use.|  
|CREATE TRIGGER WITH APPEND|A CREATE TRIGGER statement with the WITH APPEND clause was encountered. Re-create the whole trigger instead. Occurs once per use in a DDL statement.|  
|CREATE_DROP_DEFAULT|The CREATE DEFAULT or DROP DEFAULT syntax was encountered. Rewrite the command by using the DEFAULT option of CREATE TABLE or ALTER TABLE. Occurs once per compilation.|  
|CREATE_DROP_RULE|The CREATE RULE syntax was encountered. Rewrite the command by using constraints. Occurs once per compilation.|  
|Data types: text ntext or image|A **text**, **ntext**, or **image** data types was encountered. Rewrite applications to use the **varchar(max)** data type and removed **text**, **ntext**, and **image** data type syntax. Occurs once per query.|  
|Database compatibility level 80, 90, 100, 110, 120, 130, 140|The total number of times a database compatibility level was changed. Plan to upgrade the database and application for a future release. Also occurs when a database at a deprecated compatibility level is started.|  
|DATABASE_MIRRORING|References to database mirroring feature were encountered. Plan to upgrade to Always On Availability Groups, or if you are running an edition of SQL Server that does not support Always On Availability Groups, plan to migrate to log shipping.|  
|database_principal_aliases|References to the deprecated `sys.database_principal_aliases` were encountered. Use roles instead of aliases. Occurs once per compilation.|  
|DATABASEPROPERTY|A statement referenced DATABASEPROPERTY. Update the statement DATABASEPROPERTY to DATABASEPROPERTYEX. Occurs once per compilation.|  
|DATABASEPROPERTYEX('IsFullTextEnabled')|A statement referenced the DATABASEPROPERTYEX IsFullTextEnabled property. The value of this property has no effect. User databases are always enabled for full-text search. Do not use this property. Occurs once per compilation.|  
|DBCC [UN]PINTABLE|The DBCC PINTABLE or DBCC UNPINTABLE statement was encountered. This statement has no effect and should be removed. Occurs once per query.|  
|DBCC DBREINDEX|The DBCC DBREINDEX statement was encountered. Rewrite the statement to use the REBUILD option of ALTER INDEX. Occurs once per query.|  
|DBCC INDEXDEFRAG|The DBCC INDEXDEFRAG statement was encountered. Rewrite the statement to use the REORGANIZE option of ALTER INDEX. Occurs once per query.|  
|DBCC SHOWCONTIG|The DBCC SHOWCONTIG statement was encountered. Query `sys.dm_db_index_physical_stats` for this information. Occurs once per query.|  
|DBCC_EXTENTINFO||
|DBCC_IND||
|DEFAULT keyword as a default value|Syntax that uses the DEFAULT keyword as a default value was encountered. Do not use. Occurs once per compilation.|  
|Deprecated Attested Option||
|Deprecated encryption algorithm|Deprecated encryption algorithm rc4 will be removed in the next version of SQL Server. Avoid using this feature in new development work, and plan to modify applications that currently use it. The RC4 algorithm is weak and is only supported for backward compatibility. New material can only be encrypted using RC4 or RC4_128 when the database is in compatibility level 90 or 100. (Not recommended.) Use a newer algorithm such as one of the AES algorithms instead. In [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and higher material encrypted using RC4 or RC4_128 can be unencrypted in any compatibility level.|  
|Deprecated hash algorithm|Use of the MD2, MD4, MD5, SHA, or SHA1 algorithms.|  
|DESX algorithm|Syntax that uses the DESX encryption algorithm was encountered. Use another algorithm for encryption. Occurs once per compilation.|  
|dm_fts_active_catalogs|The dm_fts_active_catalogs counter always remains at 0 because some columns of the `sys.dm_fts_active_catalogs` view are not deprecated. To monitor a deprecated column, use the column-specific counter; for example, `sys.dm_fts_active_catalogs.is_paused`.|  
|dm_fts_active_catalogs.is_paused|The is_paused column of the [sys.dm_fts_active_catalogs](../../relational-databases/system-dynamic-management-views/sys-dm-fts-active-catalogs-transact-sql.md) dynamic management view was encountered. Avoid using this column. Occurs every time the server instance detects a reference to the column.|  
|dm_fts_active_catalogs.previous_status|The previous_status column of the `sys.dm_fts_active_catalogs` dynamic management view was encountered. Avoid using this column. Occurs every time the server instance detects a reference to the column.|  
|dm_fts_active_catalogs.previous_status_description|The `previous_status_description` column of the `sys.dm_fts_active_catalogs` dynamic management view was encountered. Avoid using this column. Occurs every time the server instance detects a reference to the column.|  
|dm_fts_active_catalogs.row_count_in_thousands|The `row_count_in_thousands` column of the `sys.dm_fts_active_catalogs` dynamic management view was encountered. Avoid using this column. Occurs every time the server instance detects a reference to the column.|  
|dm_fts_active_catalogs.status|The `status` column of the `sys.dm_fts_active_catalogs` dynamic management view was encountered. Avoid using this column. Occurs every time the server instance detects a reference to the column.|  
|dm_fts_active_catalogs.status_description|The `status_description` column of the `sys.dm_fts_active_catalogs` dynamic management view was encountered. Avoid using this column. Occurs every time the server instance detects a reference to the column.|  
|dm_fts_active_catalogs.worker_count|The `worker_count` column of the `sys.dm_fts_active_catalogs` dynamic management view was encountered. Avoid using this column. Occurs every time the server instance detects a reference to the column.|  
|dm_fts_memory_buffers|The dm_fts_memory_buffers counter always remains at 0 because most columns of the `sys.dm_fts_memory_buffers` view are not deprecated. To monitor the deprecated column, use the column-specific counter: dm_fts_memory_buffers.row_count.|  
|dm_fts_memory_buffers.row_count|The `row_count` column of the [sys.dm_fts_memory_buffers](../../relational-databases/system-dynamic-management-views/sys-dm-fts-memory-buffers-transact-sql.md) dynamic management view was encountered. Avoid using this column. Occurs every time the server instance detects a reference to the column.|  
|DROP INDEX with two-part name|The DROP INDEX syntax contained the format *table_name.index_name* syntax in DROP INDEX. Replace with *index_name* ON *table_name* syntax in the DROP INDEX statement. Occurs once per compilation.|  
|endpoint_webmethods|The CREATE ENDPOINT or ALTER ENDPOINT statement with the FOR SOAP option. Use Windows Communications Foundation (WCF) or ASP.NET instead.|
|EXT_CREATE_ALTER_SOAP_ENDPOINT|The CREATE or ALTER ENDPOINT statement with the FOR SOAP option was encountered. Native XML Web Services is deprecated. Use Windows Communications Foundation (WCF) or ASP.NET instead.|  
|EXT_endpoint_webmethods|`sys.endpoint_webmethods` was encountered. Native XML Web Services is deprecated. Use Windows Communications Foundation (WCF) or ASP.NET instead.|  
|EXT_soap_endpoints|`sys.soap_endpoints` was encountered. Native XML Web Services is deprecated. Use Windows Communications Foundation (WCF) or ASP.NET instead.|  
|EXTPROP_LEVEL0TYPE|TYPE was encountered at a level0type. Use SCHEMA as the level0type, and TYPE as the level1type. Occurs once per query.|  
|EXTPROP_LEVEL0USER|A level0type USER when a level1type was also specified. Use USER only as a level0type for extended properties directly on a user. Occurs once per query.|  
|FASTFIRSTROW|The FASTFIRSTROW syntax was encountered. Rewrite statements to use the OPTION (FAST *n*) syntax. Occurs once per compilation.|  
|FILE_ID|The FILE_ID syntax was encountered. Rewrite statements to use FILE_IDEX. Occurs once per compilation.|  
|fn_get_sql|The `fn_get_sql` function was compiled. Use `sys.dm_exec_sql_text` instead. Occurs once per compilation.|  
|fn_servershareddrives|The `fn_servershareddrives` function was compiled. Use `sys.dm_io_cluster_shared_drives` instead. Occurs once per compilation.|  
|fn_trace_geteventinfo|SQL Trace stored procedures, functions, and catalog views are deprecated. Use [Extended Events](../extended-events/extended-events.md) instead.|
|fn_trace_getfilterinfo|SQL Trace stored procedures, functions, and catalog views are deprecated. Use [Extended Events](../extended-events/extended-events.md) instead.|
|fn_trace_getinfo|SQL Trace stored procedures, functions, and catalog views are deprecated. Use [Extended Events](../extended-events/extended-events.md) instead.|
|fn_trace_gettable|SQL Trace stored procedures, functions, and catalog views are deprecated. Use [Extended Events](../extended-events/extended-events.md) instead.|
|fn_virtualservernodes|The `fn_virtualservernodes` function was compiled. Use `sys.dm_os_cluster_nodes` instead. Occurs once per compilation.|  
|fulltext_catalogs|The fulltext_catalogs counter always remains at 0 because some columns of the `sys.fulltext_catalogs` view are not deprecated. To monitor a deprecated column, use its column-specific counter; for example, `sys.fulltext_catalogs.data_space_id.` Occurs every time the server instance detects a reference to the column.|  
|fulltext_catalogs.data_space_id|The `data_space_id` column of the [sys.fulltext_catalogs](../../relational-databases/system-catalog-views/sys-fulltext-catalogs-transact-sql.md) catalog view was encountered. Do not use this column. Occurs every time the server instance detects a reference to the column.|  
|fulltext_catalogs.file_id|The `file_id` column of the `sys.fulltext_catalogs` catalog view was encountered. Do not use this column. Occurs every time the server instance detects a reference to the column.|  
|fulltext_catalogs.path|The `path` column of the `sys.fulltext_catalogs` catalog view was encountered. Do not use this column. Occurs every time the server instance detects a reference to the column.|  
|FULLTEXTCATALOGPROPERTY('LogSize')|The LogSize property of the FULLTEXTCATALOGPROPERTY function was encountered. Avoid using this property.|  
|FULLTEXTCATALOGPROPERTY('PopulateStatus')|The PopulateStatus property of the FULLTEXTCATALOGPROPERTY function was encountered. Avoid using this property.|  
|FULLTEXTSERVICEPROPERTY('ConnectTimeout')|The ConnectTimeout property of the FULLTEXTSERVICEPROPERTY function was encountered. Avoid using this property.|  
|FULLTEXTSERVICEPROPERTY('DataTimeout')|The DataTimeout property of the FULLTEXTSERVICEPROPERTY function was encountered. Avoid using this property.|  
|FULLTEXTSERVICEPROPERTY('ResourceUsage')|The ResourceUsage property of the FULLTEXTSERVICEPROPERTY function was encountered. Avoid using this property.|  
|GROUP BY ALL|Total number of times the GROUP BY ALL syntax was encountered. Modify the syntax to group by specific tables.|  
|Hindi|Event occurs once per database start and once per collation use. Plan to modify applications that use this collation. Use Indic_General_90 instead.|  
|HOLDLOCK table hint without parentheses|Use HOLDLOCK with parenthesis. Rewrite the statement to use the current syntax.|  
|IDENTITYCOL|The INDENTITYCOL syntax was encountered. Rewrite statements to use the $identity syntax. Occurs once per compilation.|  
|IN PATH|A CREATE FULLTEXT CATALOG statement with the IN PATH clause was encountered. This clause has no effect in this version of SQL Server. Occurs once per use.|
|Index view select list without COUNT_BIG(\*)|The select list of an aggregate indexed view must contain COUNT_BIG (*) .|  
|INDEX_OPTION|Encountered CREATE TABLE, ALTER TABLE, or CREATE INDEX syntax without parentheses around the options. Rewrite the statement to use the current syntax. Occurs once per query.|  
|INDEXKEY_PROPERTY|The INDEXKEY_PROPERTY syntax was encountered. Rewrite statements to query `sys.index_columns`. Occurs once per compilation.|  
|Indirect TVF hints|The indirect application, through a view, of table hints to an invocation of a multistatement table-valued function (TVF) will be removed in a future version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
|INSERT NULL into TIMESTAMP columns|A NULL value was inserted to a TIMESTAMP column. Use a default value instead. Occurs once per compilation.|  
|INSERT_HINTS||  
|Korean_Wansung_Unicode|Event occurs once per database start and once per collation use. Plan to modify applications that use this collation.|  
|Lithuanian_Classic|Event occurs once per database start and once per collation use. Plan to modify applications that use this collation.|  
|Macedonian|Event occurs once per database start and once per collation use. Plan to modify applications that use this collation. Use Macedonian_FYROM_90 instead.|  
|MODIFY FILEGROUP READONLY|The MODIFY FILEGROUP READONLY syntax was encountered. Rewrite statements to use the READ_ONLY syntax. Occurs once per compilation.|  
|MODIFY FILEGROUP READWRITE|The MODIFY FILEGROUP READWRITE syntax was encountered. Rewrite statements to use the READ_WRITE syntax. Occurs once per compilation.|  
|More than two-part column name|A query used a 3-part or 4-part name in the column list. Change the query to use the standard-compliant 2-part names. Occurs once per compilation.|  
|Multiple table hints without comma|A space was used as the separator between table hints. Use a comma instead. Occurs once per compilation.|  
|NOLOCK or READUNCOMMITTED in UPDATE or DELETE|NOLOCK or READUNCOMMITTED was encountered in the FROM clause of an UPDATE or DELETE statement. Remove the NOLOCK or READUNCOMMITTED table hints from the FROM clause.|  
|Non-ANSI *= or =\* outer join operators|A statement that uses the *= or =\* join syntax was encountered. Rewrite the statement to use the ANSI join syntax. Occurs once per compilation.|  
|Numbered stored procedures|Numbered procedures are deprecated. Use of numbered procedures is discouraged.|  
|numbered_procedure_parameters|References to the deprecated `sys.numbered_procedure_parameters` were encountered. Do not use. Occurs once per compilation.|  
|numbered_procedures|References to the deprecated `sys.numbered_procedures` were encountered. Do not use. Occurs once per compilation.|  
|objidupdate||
|Oldstyle RAISEERROR|The deprecated RAISERROR (Format: RAISERROR integer string) syntax was encountered. Rewrite the statement using the current RAISERROR syntax. Occurs once per compilation.|  
|Old NEAR Syntax|Use the new NEAR syntax. See [NEAR](../search/search-for-words-close-to-another-word-with-near.md)|
|OLEDB for ad hoc connections|SQLOLEDB is not a supported provider. Use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client for ad hoc connections.|  
|PERMISSIONS|References to the PERMISSIONS intrinsic function were encountered. Query `sys.fn_my_permissions` instead. Occurs once per query.|  
|ProcNums|The deprecated ProcNums syntax was encountered. Rewrite statements to remove the references. Occurs once per compilation.|  
|READTEXT|The READTEXT syntax was encountered. Rewrite applications to use the **varchar(max)** data type and removed **text** data type syntax. Occurs once per query.|  
|REMSERVER|Replace remote servers by using linked servers.|
|RESTORE DATABASE or LOG WITH DBO_ONLY|The `RESTORE ... WITH DBO_ONLY` syntax was encountered. Use `RESTORE ... RESTRICTED_USER` instead.|  
|RESTORE DATABASE or LOG WITH MEDIAPASSWORD|The `RESTORE ... WITH MEDIAPASSWORD` syntax was encountered. WITH MEDIAPASSWORD provides weak security and should be removed.|  
|RESTORE DATABASE or LOG WITH PASSWORD|The `RESTORE ... WITH PASSWORD` syntax was encountered. WITH PASSWORD provides weak security and should be removed.|  
|Returning results from trigger|This event occurs once per trigger invocation. Rewrite the trigger so that it does not return result sets.|  
|ROWGUIDCOL|The ROWGUIDCOL syntax was encountered. Rewrite statements to use the $rowguid syntax. Occurs once per compilation.|  
|SET ANSI_NULLS OFF|The `SET ANSI_NULLS OFF` syntax was encountered. Remove this deprecated syntax. Occurs once per compilation.|  
|SET ANSI_PADDING OFF|The `SET ANSI_PADDING OFF` syntax was encountered. Remove this deprecated syntax. Occurs once per compilation.|  
|SET CONCAT_NULL_YIELDS_NULL OFF|The `SET CONCAT_NULL_YIELDS_NULL OFF` syntax was encountered. Remove this deprecated syntax. Occurs once per compilation.|  
|SET DISABLE_DEF_CNST_CHK|The `SET DISABLE_DEF_CNST_CHK` syntax was encountered. This has no effect. Remove this deprecated syntax. Occurs once per compilation.|  
|SET ERRLVL||
|SET FMTONLY ON|The `SET FMTONLY` syntax was encountered. Remove this deprecated syntax. Occurs once per compilation.|  
|SET OFFSETS|The `SET OFFSETS` syntax was encountered. Remove this deprecated syntax. Occurs once per compilation.|  
|SET REMOTE_PROC_TRANSACTIONS|The `SET REMOTE_PROC_TRANSACTIONS` syntax was encountered. Remove this deprecated syntax. Use linked servers and `sp_serveroption` instead.|  
|SET ROWCOUNT|The `SET ROWCOUNT` syntax was encountered in a DELETE, INSERT, or UPDATE statement. Rewrite the statement by using TOP. Occurs once per compilation.|  
|SETUSER|The `SET USER` statement was encountered. Use EXECUTE AS instead. Occurs once per query.|  
|soap_endpoints|Native XML Web Services is deprecated. Use Windows Communications Foundation (WCF) or ASP.NET instead.|
|sp_addapprole|The `sp_addapprole` procedure was encountered. Use CREATE APPLICATION ROLE instead. Occurs once per query.|  
|sp_addextendedproc|The `sp_addextendedproc` procedure was encountered. Use CLR instead. Occurs once per compilation.|  
|sp_addlogin|The `sp_addlogin` procedure was encountered. Use CREATE LOGIN instead. Occurs once per query.|  
|sp_addremotelogin|The `sp_addremotelogin` procedure was encountered. Use linked servers instead.|  
|sp_addrole|The `sp_addrole` procedure was encountered. Use CREATE ROLE instead. Occurs once per query.|  
|sp_addrolemember|The `sp_addrolemember` procedure was encountered. Use ALTER ROLE instead.|
|sp_addserver|The `sp_addserver` procedure was encountered. Use linked servers instead.|  
|sp_addsrvrolemember|The `sp_addsrvrolemember` procedure was encountered. Use ALTER SERVER ROLE instead.|
|sp_addtype|The `sp_addtype` procedure was encountered. Use CREATE TYPE instead. Occurs once per compilation.|  
|sp_adduser|The `sp_adduser` procedure was encountered. Use CREATE USER instead. Occurs once per query.|  
|sp_approlepassword|The `sp_approlepassword` procedure was encountered. Use ALTER APPLICATION ROLE instead. Occurs once per query.|  
|sp_attach_db|The `sp_attach_db` procedure was encountered. Use CREATE DATABASE FOR ATTACH instead. Occurs once per query.|  
|sp_attach_single_file_db|The `sp_single_file_db` procedure was encountered. Use CREATE DATABASE FOR ATTACH_REBUILD_LOG instead. Occurs once per query.|  
|sp_bindefault|The `sp_bindefault` procedure was encountered. Use the DEFAULT keyword of ALTER TABLE or CREATE TABLE instead. Occurs once per compilation.|  
|sp_bindrule|The `sp_bindrule` procedure was encountered. Use check constraints instead. Occurs once per compilation.|  
|sp_bindsession|The `sp_bindsession` procedure was encountered. Use Multiple Active Result Sets (MARS) or distributed transactions instead. Occurs once per compilation.|  
|sp_certify_removable|The `sp_certify_removable` procedure was encountered. Use `sp_detach_db` instead. Occurs once per query.|  
|sp_changedbowner|The `sp_changedbowner` procedure was encountered. Use ALTER AUTHORIZATION instead.|
|sp_changeobjectowner|The `sp_changeobjectowner` procedure was encountered. Use ALTER SCHEMA or ALTER AUTHORIZATION instead. Occurs once per query.|  
|sp_change_users_login|The `sp_change_users_login` procedure was encountered. Use ALTER USER instead. Occurs once per query.|  
|sp_configure 'affinity mask'|The affinity mask option of `sp_configure` was encountered. Use ALTER SERVER CONFIGURATION instead.|
|sp_configure 'affinity64 mask'|The affinity mask option of `sp_configure` was encountered. Use ALTER SERVER CONFIGURATION instead.|
|sp_configure 'allow updates'|The allow updates option of `sp_configure` was encountered. System tables are no longer updatable. Do not use. Occurs once per query.|  
|sp_configure 'c2 audit mode'|The C2 security standard has been superseded by Common Criteria Certification. See the [Common Criteria Compliance Enabled Server Configuration](../../database-engine/configure-windows/common-criteria-compliance-enabled-server-configuration-option.md).|
|sp_configure 'default trace enabled'|SQL Trace stored procedures, functions, and catalog views are deprecated. Use [Extended Events](../extended-events/extended-events.md) instead.|
|sp_configure 'disallow results from triggers'|The disallow result sets from triggers option of `sp_configure` was encountered. To disallow result sets from triggers, use `sp_configure` to set the option to 1. Occurs once per query.|  
|sp_configure 'ft crawl bandwidth (max)'|The ft crawl bandwidth (max) option of `sp_configure` was encountered. Do not use. Occurs once per query.|  
|sp_configure 'ft crawl bandwidth (min)'|The ft crawl bandwidth (min) option of `sp_configure` was encountered. Do not use. Occurs once per query.|  
|sp_configure 'ft notify bandwidth (max)'|The ft notify bandwidth (max) option of `sp_configure` was encountered. Do not use. Occurs once per query.|  
|sp_configure 'ft notify bandwidth (min)'|The ft notify bandwidth (min) option of `sp_configure` was encountered. Do not use. Occurs once per query.|  
|sp_configure 'locks'|The locks option of `sp_configure` was encountered. Locks are no longer configurable. Do not use. Occurs once per query.|  
|sp_configure 'open objects'|The open objects option of `sp_configure` was encountered. The number of open objects is no longer configurable. Do not use. Occurs once per query.|  
|sp_configure 'priority boost'|The priority boost option of `sp_configure` was encountered. Do not use. Occurs once per query. Use the Windows `start /high ... program.exe` command line option instead.|  
|sp_configure 'remote proc trans'|The remote proc trans option of `sp_configure` was encountered. Do not use. Occurs once per query.|  
|sp_configure 'set working set size'|The set working set size option of `sp_configure` was encountered. The working set size is no longer configurable. Do not use. Occurs once per query.|  
|sp_control_dbmasterkey_password|The `sp_control_dbmasterkey_password` stored procedure does not check whether a master key exists. This is permitted for backward compatibility, but displays a warning. This behavior is deprecated. In a future release the master key must exist and the password used in the stored procedure `sp_control_dbmasterkey_password` must be the same password as one of the passwords used to encrypt the database master key.|  
|sp_create_removable|The `sp_create_removable` procedure was encountered. Use `CREATE DATABASE` instead. Occurs once per query.|  
|sp_db_increased_partitions|The `sp_db_increased_partitions` procedure was encountered. Support for increased partitions is now available by default.|
|sp_db_selective_xml_index|Starting with SQL Server 2014 (12.x), the Selective XML Index functionality cannot be disabled. In SQL Server 2012 (11.x), in order to disable the Selective XML Index feature using this stored procedure, the database must be put in the SIMPLE recovery model by using the ALTER DATABASE SET command.|
|sp_db_vardecimal_storage_format|Use of **vardecimal** storage format was encountered. Use data compression instead.|  
|sp_dbcmptlevel|The `sp_dbcmptlevel` procedure was encountered. Use `ALTER DATABASE ... SET COMPATIBILITY_LEVEL` instead. Occurs once per query.|  
|sp_dbfixedrolepermission|The `sp_dbfixedrolepermission` procedure was encountered. Do not use. Occurs once per query.|  
|sp_dboption|The `sp_dboption` procedure was encountered. Use ALTER DATABASE and DATABASEPROPERTYEX instead. Occurs once per compilation.|  
|sp_dbremove|The `sp_dbremove` procedure was encountered. Use DROP DATABASE instead. Occurs once per query.|  
|sp_defaultdb|The `sp_defaultdb` procedure was encountered. Use ALTER LOGIN instead. Occurs once per compilation.|  
|sp_defaultlanguage|The `sp_defaultlanguage` procedure was encountered. Use ALTER LOGIN instead. Occurs once per compilation.|  
|sp_denylogin|The `sp_denylogin` procedure was encountered. Use ALTER LOGIN DISABLE instead. Occurs once per query.|  
|sp_depends|The `sp_depends` procedure was encountered. Use `sys.dm_sql_referencing_entities` and `sys.dm_sql_referenced_entities` instead. Occurs once per query.|  
|sp_detach_db \@keepfulltextindexfile|The \@keepfulltextindexfile argument was encountered in a `sp_detach_db` statement. Do not use this argument.|  
|sp_dropalias|The `sp_dropalias` procedure was encountered. Replace aliases with a combination of user accounts and database roles. Use `sp_dropalias` to remove aliases in upgraded databases. Occurs once per compilation.|  
|sp_dropapprole|The `sp_dropapprole` procedure was encountered. Use DROP APPLICATION ROLE instead. Occurs once per query.|  
|sp_dropextendedproc|The `sp_dropextendedproc` procedure was encountered. Use CLR instead. Occurs once per compilation.|  
|sp_droplogin|The `sp_droplogin` procedure was encountered. Use DROP LOGIN instead. Occurs once per query.|  
|sp_dropremotelogin|The `sp_dropremotelogin` procedure was encountered. Use linked servers instead.|  
|sp_droprole|The `sp_droprole` procedure was encountered. Use DROP ROLE instead. Occurs once per query.|  
|sp_droprolemember|The `sp_droprolemember` procedure was encountered. Use ALTER ROLE instead.|
|sp_dropsrvrolemember|The `sp_dropsrvrolemember` procedure was encountered. Use ALTER SERVER ROLE instead.|
|sp_droptype|The `sp_droptype` procedure was encountered. Use DROP TYPE instead.|  
|sp_dropuser|The `sp_dropuser` procedure was encountered. Use DROP USER instead. Occurs once per query.|  
|sp_estimated_rowsize_reduction_for_vardecimal|Use of **vardecimal** storage format was encountered. Use data compression and `sp_estimate_data_compression_savings` instead.|  
|sp_fulltext_catalog|The `sp_fulltext_catalog` procedure was encountered. Use CREATE/ALTER/DROP FULLTEXT CATALOG instead. Occurs once per compilation.|  
|sp_fulltext_column|The `sp_fulltext_column` procedure was encountered. Use ALTER FULLTEXT INDEX instead. Occurs once per compilation.|  
|sp_fulltext_database|The `sp_fulltext_database` procedure was encountered. Use ALTER DATABASE instead. Occurs once per compilation.|  
|sp_fulltext_service \@action=clean_up|The clean_up option of the `sp_fulltext_service` procedure was encountered. Occurs once per query.|  
|sp_fulltext_service \@action=connect_timeout|The connect_timeout option of the `sp_fulltext_service` procedure was encountered. Occurs once per query.|  
|sp_fulltext_service \@action=data_timeout|The data_timeout option of the `sp_fulltext_service` procedure was encountered. Occurs once per query.|  
|sp_fulltext_service \@action=resource_usage|The resource_usage option of the `sp_fulltext_service` procedure was encountered. This option has no function. Occurs once per query.|  
|sp_fulltext_table|The `sp_fulltext_table` procedure was encountered. Use CREATE/ALTER/DROP FULLTEXT INDEX instead. Occurs once per compilation.|  
|sp_getbindtoken|The `sp_getbindtoken` procedure was encountered. Use Multiple Active Result Sets (MARS) or distributed transactions instead. Occurs once per compilation.|  
|sp_grantdbaccess|The `sp_grantdbaccess` procedure was encountered. Use CREATE USER instead. Occurs once per query.|  
|sp_grantlogin|The `sp_grantlogin` procedure was encountered. Use CREATE LOGIN instead. Occurs once per query.|  
|sp_help_fulltext_catalog_components|The `sp_help_fulltext_catalog_components` procedure was encountered. This procedure returns empty rows. Do not use this procedure. Occurs once per compilation.|  
|sp_help_fulltext_catalogs|The `sp_help_fulltext_catalogs` procedure was encountered. Query `sys.fulltext_catalogs` instead. Occurs once per compilation.|  
|sp_help_fulltext_catalogs_cursor|The `sp_help_fulltext_catalogs_cursor` procedure was encountered. Query `sys.fulltext_catalogs` instead. Occurs once per compilation.|  
|sp_help_fulltext_columns|The `sp_help_fulltext_columns` procedure was encountered. Query `sys.fulltext_index_columns` instead. Occurs once per compilation.|  
|sp_help_fulltext_columns_cursor|The `sp_help_fulltext_columns_cursor` procedure was encountered. Query `sys.fulltext_index_columns` instead. Occurs once per compilation.|  
|sp_help_fulltext_tables|The `sp_help_fulltext_tables` procedure was encountered. Query `sys.fulltext_indexes` instead. Occurs once per compilation.|  
|sp_help_fulltext_tables_cursor|The `sp_help_fulltext_tables_cursor` procedure was encountered. Query `sys.fulltext_indexes` instead. Occurs once per compilation.|  
|sp_helpdevice|The `sp_helpdevice` procedure was encountered. Query `sys.backup_devices` instead. Occurs once per query.|  
|sp_helpextendedproc|The `sp_helpextendedproc` procedure was encountered. Use CLR instead. Occurs once per compilation.|  
|sp_helpremotelogin|The `sp_helpremotelogin` procedure was encountered. Use linked servers instead.|  
|sp_indexoption|The `sp_indexoption` procedure was encountered. Use ALTER INDEX instead. Occurs once per compilation.|  
|sp_lock|The `sp_lock` procedure was encountered. Query `sys.dm_tran_locks` instead. Occurs once per query.|  
|sp_password|The `sp_password` procedure was encountered. Use ALTER LOGIN instead. Occurs once per query.|  
|sp_remoteoption|The `sp_remoteoption` procedure was encountered. Use linked servers instead.|  
|sp_renamedb|The `sp_renamedb` procedure was encountered. Use ALTER DATABASE instead. Occurs once per query.|  
|sp_resetstatus|The `sp_resetstatus` procedure was encountered. Use ALTER DATABASE instead. Occurs once per query.|  
|sp_revokedbaccess|The `sp_revokedbaccess` procedure was encountered. Use DROP USER instead. Occurs once per query.|  
|sp_revokelogin|The `sp_revokelogin` procedure was encountered. Use DROP LOGIN instead. Occurs once per query.|  
|sp_srvrolepermission|The deprecated `sp_srvrolepermission` procedure was encountered. Do not use. Occurs once per query.|  
|sp_trace_create|SQL Trace stored procedures, functions, and catalog views are deprecated. Use [Extended Events](../extended-events/extended-events.md) instead.|
|sp_trace_getdata|SQL Trace stored procedures, functions, and catalog views are deprecated. Use [Extended Events](../extended-events/extended-events.md) instead.|
|sp_trace_setevent|SQL Trace stored procedures, functions, and catalog views are deprecated. Use [Extended Events](../extended-events/extended-events.md) instead.|
|sp_trace_setfilter|SQL Trace stored procedures, functions, and catalog views are deprecated. Use [Extended Events](../extended-events/extended-events.md) instead.|
|sp_trace_setstatus|SQL Trace stored procedures, functions, and catalog views are deprecated. Use [Extended Events](../extended-events/extended-events.md) instead.|
|sp_unbindefault|The `sp_unbindefault` procedure was encountered. Use the DEFAULT keyword in CREATE TABLE or ALTER TABLE statements instead. Occurs once per compilation.|  
|sp_unbindrule|The `sp_unbindrule` procedure was encountered. Use check constraints instead of rules. Occurs once per compilation.|  
|SQL_AltDiction_CP1253_CS_AS|Event occurs once per database start and once per collation use. Plan to modify applications that use this collation.|  
|sql_dependencies|References to `sys.sql_dependencies` were encountered. Use `sys.sql_expression_dependencies` instead. Occurs once per compilation.|  
|String literals as column aliases|Syntax that contains a string that is used as a column alias in a SELECT statement, such as `'string' = expression`, was encountered. Do not use. Occurs once per compilation.|  
|sysaltfiles|References to `sysaltfiles` were encountered. Use `sys.master_files` instead. Occurs once per compilation.|  
|syscacheobjects|References to `syscacheobjects` were encountered. Use `sys.dm_exec_cached_plans`, `sys.dm_exec_plan_attributes`, and `sys.dm_exec_sql_text` instead. Occurs once per compilation.|  
|syscolumns|References to `syscolumns` were encountered. Use `sys.columns` instead. Occurs once per compilation.|  
|syscomments|References to `syscomments` were encountered. Use `sys.sql_modules` instead. Occurs once per compilation.|  
|sysconfigures|References to the `sysconfigures` table were encountered. Reference the `sys.sysconfigures` view instead. Occurs once per compilation.|  
|sysconstraints|References to `sysconstraints` were encountered. Use `sys.check_constraints`, `sys.default_constraints`, `sys.key_constraints`, `sys.foreign_keys` instead. Occurs once per compilation.|  
|syscurconfigs|References to `syscurconfigs` were encountered. Use `sys.configurations` instead. Occurs once per compilation.|  
|sysdatabases|References to `sysdatabases` were encountered. Use `sys.databases` instead. Occurs once per compilation.|  
|sysdepends|References to `sysdepends` were encountered. Use `sys.sql_dependencies` instead. Occurs once per compilation.|  
|sysdevices|References to `sysdevices` were encountered. Use `sys.backup_devices` instead. Occurs once per compilation.|  
|sysfilegroups|References to `sysfilegroups` were encountered. Use `sys.filegroups` instead. Occurs once per compilation.|  
|sysfiles|References to `sysfiles` were encountered. Use `sys.database_files` instead. Occurs once per compilation.|  
|sysforeignkeys|References to `sysforeignkeys` were encountered. Use `sys.foreign_keys` instead. Occurs once per compilation.|  
|sysfulltextcatalogs|References to `sysfulltextcatalogs` were encountered. Use `sys.fulltext_catalogs` instead. Occurs once per compilation.|  
|sysindexes|References to `sysindexes` were encountered. Use `sys.indexes`, `sys.partitions`, `sys.allocation_units`, and `sys.dm_db_partition_stats` instead. Occurs once per compilation.|  
|sysindexkeys|References to `sysindexkeys` were encountered. Use `sys.index_columns` instead. Occurs once per compilation.|  
|syslockinfo|References to `syslockinfo` were encountered. Use `sys.dm_tran_locks` instead. Occurs once per compilation.|  
|syslogins|References to `syslogins` were encountered. Use `sys.server_principals` and `sys.sql_logins` instead. Occurs once per compilation.|  
|sysmembers|References to `sysmembers` were encountered. Use `sys.database_role_members` instead. Occurs once per compilation.|  
|sysmessages|References to `sysmessages` were encountered. Use `sys.messages` instead. Occurs once per compilation.|  
|sysobjects|References to `sysobjects` were encountered. Use `sys.objects` instead. Occurs once per compilation.|  
|sysoledbusers|References to `sysoledbusers` were encountered. Use `sys.linked_logins` instead. Occurs once per compilation.|  
|sysopentapes|References to `sysopentapes` were encountered. Use `sys.dm_io_backup_tapes` instead. Occurs once per compilation.|  
|sysperfinfo|References to `sysperfinfo` were encountered. Use `sys.dm_os_performance_counters`. instead. Occurs once per compilation.|  
|syspermissions|References to `syspermissions` were encountered. Use `sys.database_permissions` and `sys.server_permissions` instead. Occurs once per compilation.|  
|sysprocesses|References to `sysprocesses` were encountered. Use `sys.dm_exec_connections`, `sys.dm_exec_sessions`, and `sys.dm_exec_requests` instead. Occurs once per compilation.|  
|sysprotects|References to `sysprotects` were encountered. Use `sys.database_permissions` and `sys.server_permissions` instead. Occurs once per compilation.|  
|sysreferences|References to `sysreferences` were encountered. Use `sys.foreign_keys` instead. Occurs once per compilation.|  
|sysremotelogins|References to `sysremotelogins` were encountered. Use `sys.remote_logins` instead. Occurs once per compilation.|  
|sysservers|References to `sysservers` were encountered. Use `sys.servers` instead. Occurs once per compilation.|  
|systypes|References to `systypes` were encountered. Use `sys.types` instead. Occurs once per compilation.|  
|sysusers|References to `sysusers` were encountered. Use `sys.database_principals` instead. Occurs once per compilation.|  
|Table hint without WITH|A statement that used table hints but did not use the WITH keyword was encountered. Modify statements to include the word WITH. Occurs once per compilation.|  
|Text in row table option|References to the 'text in row' table option were encountered. Use `sp_tableoption 'large value types out of row'` instead. Occurs once per query.|  
|TEXTPTR|References to the TEXTPTR function were encountered. Rewrite applications to use the **varchar(max)** data type and removed **text**, **ntext**, and **image** data type syntax. Occurs once per query.|  
|TEXTVALID|References to the TEXTVALID function were encountered. Rewrite applications to use the **varchar(max)** data type and removed **text**, **ntext**, and **image** data type syntax. Occurs once per query.|  
|TIMESTAMP|Total number of times the deprecated **timestamp** data type was encountered in a DDL statement. Use the **rowversion** data type instead.|  
|UPDATETEXT or WRITETEXT|The UPDATETEXT or WRITETEXT statement was encountered. Rewrite applications to use the **varchar(max)** data type and removed **text**, **ntext**, and **image** data type syntax. Occurs once per query.|  
|USER_ID|References to the USER_ID function were encountered. Use the DATABASE_PRINCIPAL_ID function instead. Occurs once per compilation.|  
|Using OLEDB for linked servers|Specifying the SQLOLEDB provider for linked servers was encountered. Use MSOLEDBSQL instead.|  
|Vardecimal storage format|Use of **vardecimal** storage format was encountered. Use data compression instead.|  
|XMLDATA|The FOR XML syntax was encountered. Use XSD generation for RAW and AUTO modes. There is no replacement for the explicit mode. Occurs once per compilation.|  
|XP_API|An extended stored procedure statement was encountered. Do not use.|  
|xp_grantlogin|The `xp_grantlogin` procedure was encountered. Use CREATE LOGIN instead. Occurs once per compilation.|  
|xp_loginconfig|The `xp_loginconfig` procedure was encountered. Use the **IsIntegratedSecurityOnly** argument of SERVERPROPERTY instead. Occurs once per query.|  
|xp_revokelogin|The `xp_revokelogin` procedure was encountered. Use ALTER LOGIN DISABLE or DROP LOGIN instead. Occurs once per compilation.|  
  
## See also  
 - [Deprecated Database Engine Features in SQL Server 2016](../../database-engine/deprecated-database-engine-features-in-sql-server-2016.md)   
 - [Deprecated Full-Text Search Features in SQL Server 2016](../../relational-databases/search/deprecated-full-text-search-features-in-sql-server-2016.md)   
 - [Deprecation Announcement Event Class](../../relational-databases/event-classes/deprecation-announcement-event-class.md)   
 - [Deprecation Final Support Event Class](../../relational-databases/event-classes/deprecation-final-support-event-class.md)   
 - [Discontinued Database Engine Functionality in SQL Server 2016](../../database-engine/discontinued-database-engine-functionality-in-sql-server.md)   
 - [Discontinued Full-Text Search Features in SQL Server 2016](../../database-engine/discontinued-database-engine-functionality-in-sql-server.md)   
 - [Use SQL Server Objects](../../relational-databases/performance-monitor/use-sql-server-objects.md)  
  
