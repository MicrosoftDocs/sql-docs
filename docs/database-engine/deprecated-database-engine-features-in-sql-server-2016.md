---
title: "Deprecated Database Engine Features"
titleSuffix: "SQL Server 2016"
description: Find out about deprecated database engine features that are still available in SQL Server 2016 (13.x), but shouldn't be used in new applications.
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
---
# Deprecated Database Engine features in SQL Server 2016

[!INCLUDE [SQL Server 2016](../includes/applies-to-version/sqlserver2016.md)]

This article describes the deprecated [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] features that are still available in [!INCLUDE[sssql15-md](../includes/sssql16-md.md)]. Deprecated features should not be used in new applications.

When a feature is marked deprecated, it means:

- The feature is in maintenance mode only. No new changes will be done, including those related to addressing inter-operability with new features.
- We strive not to remove a deprecated feature from future releases to make upgrades easier. However, under rare situations, we may choose to permanently discontinue (remove) the feature from [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] if it limits future innovations.
- For new development work, do not use deprecated features. For existing applications, plan to modify applications that currently use these features as soon as possible.


For [!INCLUDE[sssql17-md](../includes/sssql17-md.md)], see [Deprecated Database Engine Features in SQL Server 2017](../database-engine/deprecated-database-engine-features-in-sql-server-2017.md).

You can monitor the use of deprecated features by using the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Deprecated Features Object performance counter and trace events. For more information, see [Use SQL Server Objects](../relational-databases/performance-monitor/use-sql-server-objects.md).

The value of these counters is also available by executing the following statement:

```sql
SELECT * FROM sys.dm_os_performance_counters
WHERE object_name = 'SQLServer:Deprecated Features';
```

## Features deprecated in the next version of SQL Server

The following [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] features will not be supported in a future version of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. Do not use these features in new development work, and modify applications that currently use these features as soon as possible. The **Feature name** value appears in trace events as the ObjectName and in performance counters and `sys.dm_os_performance_counters` as the instance name. The **Feature ID** value appears in trace events as the ObjectId.

|Category|Deprecated feature|Replacement|Feature name|Feature ID|
|--------------|------------------------|-----------------|------------------|----------------|
|Backup and Restore|RESTORE { DATABASE &#124; LOG } WITH [MEDIA]PASSWORD continues to be deprecated. BACKUP { DATABASE &#124; LOG } WITH PASSWORD and BACKUP { DATABASE &#124; LOG } WITH MEDIAPASSWORD are discontinued.|None|BACKUP DATABASE or LOG WITH PASSWORD<br /><br /> BACKUP DATABASE or LOG WITH MEDIAPASSWORD|104<br /><br /> 103|
|Compatibility levels|Upgrade from version 100 ([!INCLUDE[ssKatmai](../includes/sskatmai-md.md)] and [!INCLUDE[ssKilimanjaro](../includes/sskilimanjaro-md.md)]).|When a [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] version goes out of [support](/lifecycle/products/?products=sql-server), the associated database compatibility level will be marked deprecated. However, we will continue to support applications certified on any supported Database Compatibility Level as long as possible, to make the upgrades easier. For more information about compatibility levels, see [ALTER DATABASE Compatibility Level &#40;Transact-SQL&#41;](../t-sql/statements/alter-database-transact-sql-compatibility-level.md).|Database compatibility level 100|108|
|Database objects|Ability to return result sets from triggers|None|Returning results from trigger|12|
|Encryption|Encryption using RC4 or RC4_128 is deprecated and is scheduled to be removed in the next version. Decrypting RC4 and RC4_128 is not deprecated.|Use another encryption algorithm such as AES.|Deprecated encryption algorithm|253|
|Hash algorithms|Using the MD2, MD4, MD5, SHA, and SHA1 is deprecated.|Use SHA2_256 or SHA2_512 instead. Older algorithms will continue working, but they will raise a deprecation event.|Deprecated hash algorithm|None|
|Remote servers|sp_addremotelogin<br /><br /> sp_addserver<br /><br /> sp_dropremotelogin<br /><br /> sp_helpremotelogin<br /><br /> sp_remoteoption|Replace remote servers by using linked servers. sp_addserver can only be used with the local option.|sp_addremotelogin<br /><br /> sp_addserver<br /><br /> sp_dropremotelogin<br /><br /> sp_helpremotelogin<br /><br /> sp_remoteoption|70<br /><br /> 69<br /><br /> 71<br /><br /> 72<br /><br /> 73|
|Remote servers|\@\@remserver|Replace remote servers by using linked servers.|None|None|
|Remote servers|SET REMOTE_PROC_TRANSACTIONS|Replace remote servers by using linked servers.|SET REMOTE_PROC_TRANSACTIONS|110|
|Table hints|HOLDLOCK table hint without parenthesis.|Use HOLDLOCK with parenthesis.|HOLDLOCK table hint without parenthesis|167|

## Features deprecated in a future version of SQL Server

The following [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] features are supported in the next version of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], but will be deprecated in a later version. The specific version of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] has not been determined.

|Category|Deprecated feature|Replacement|Feature name|Feature ID|
|--------------|------------------------|-----------------|------------------|----------------|
|Compatibility levels|sp_dbcmptlevel|ALTER DATABASE ... SET COMPATIBILITY_LEVEL. For more information, see [ALTER DATABASE Compatibility Level &#40;Transact-SQL&#41;](../t-sql/statements/alter-database-transact-sql-compatibility-level.md).|sp_dbcmptlevel|80|
|Compatibility levels|Database compatibility level 110 and 120.|Plan to upgrade the database and application for a future release. However, we will continue to support applications certified on any supported database compatibility level as long as possible, to make the upgrades easier. For more information about compatibility levels, see [ALTER DATABASE Compatibility Level &#40;Transact-SQL&#41;](../t-sql/statements/alter-database-transact-sql-compatibility-level.md).|Database compatibility level 110<br /><br /> Database compatibility level 120||
|XML|Inline XDR Schema Generation|The XMLDATA directive to the FOR XML option is deprecated. Use XSD generation in the case of RAW and AUTO modes. There is no replacement for the XMLDATA directive in EXPLICT mode.|XMLDATA|181|
|XML|sys.sp_db_selective_xml_index|ALTER INDEX … DISABLE<BR><BR>For more information, see [ALTER INDEX](../t-sql/statements/alter-index-transact-sql.md).|[sys.sp_db_selective_xml_index](../relational-databases/system-stored-procedures/sp-db-selective-xml-index-transact-sql.md)|
|Backup and restore|BACKUP { DATABASE &#124; LOG } TO TAPE<br /><br /> BACKUP { DATABASE &#124; LOG } TO *device_that_is_a_tape*|BACKUP { DATABASE &#124; LOG } TO DISK<br /><br /> BACKUP { DATABASE &#124; LOG } TO *device_that_is_a_disk*|BACKUP DATABASE or LOG TO TAPE|235|
|Backup and restore|sp_addumpdevice'**tape**'|sp_addumpdevice'**disk**'|ADDING TAPE DEVICE|236|
|Backup and restore|sp_helpdevice|sys.backup_devices|sp_helpdevice|100|
|Collations|Korean_Wansung_Unicode<br /><br /> Lithuanian_Classic<br /><br /> SQL_AltDiction_CP1253_CS_AS|None. These collations exist in [!INCLUDE[ssVersion2005](../includes/ssversion2005-md.md)], but are not visible through fn_helpcollations.|Korean_Wansung_Unicode<br /><br /> Lithuanian_Classic<br /><br /> SQL_AltDiction_CP1253_CS_AS|191<br /><br /> 192<br /><br /> 194|
|Collations|Hindi<br /><br /> Macedonian|These collations exist in [!INCLUDE[ssVersion2005](../includes/ssversion2005-md.md)] and higher, but are not visible through fn_helpcollations. Use Macedonian_FYROM_90 and Indic_General_90 instead.|Hindi<br /><br /> Macedonian|190<br /><br /> 193|
|Collations|Azeri_Latin_90<br /><br /> Azeri_Cyrilllic_90|Azeri_Latin_100<br /><br /> Azeri_Cyrilllic_100|Azeri_Latin_90<br /><br /> Azeri_Cyrilllic_90|232<br /><br /> 233|
|Configuration|SET ANSI_NULLS OFF and ANSI_NULLS OFF database option<br /><br /> SET ANSI_PADDING OFF and ANSI_PADDING OFF database option<br /><br /> SET CONCAT_NULL_YIELDS_NULL OFF and CONCAT_NULL_YIELDS_NULL OFF database option<br /><br /> SET OFFSETS|None.<br /><br /> ANSI_NULLS, ANSI_PADDING and CONCAT_NULLS_YIELDS_NULL will always be set to ON. SET OFFSETS will be unavailable.|SET ANSI_NULLS OFF<br /><br /> SET ANSI_PADDING OFF<br /><br /> SET CONCAT_NULL_YIELDS_NULL OFF<br /><br /> SET OFFSETS<br /><br /> ALTER DATABASE SET ANSI_NULLS OFF<br /><br /> ALTER DATABASE SET ANSI_PADDING OFF<br /><br /> ALTER DATABASE SET CONCAT_NULL_YIELDS_NULL OFF|111<br /><br /> 113<br /><br /> 112<br /><br /> 36<br /><br /> 111<br /><br /> 113<br /><br /> 112|
|Data types|sp_addtype<br /><br /> sp_droptype|CREATE TYPE<br /><br /> DROP TYPE|sp_addtype<br /><br /> sp_droptype|62<br /><br /> 63|
|Data types|**timestamp** syntax for **rowversion** data type|**rowversion** data type syntax|TIMESTAMP|158|
|Data types|Ability to insert null values into **timestamp** columns.|Use a DEFAULT instead.|INSERT NULL into TIMESTAMP columns|179|
|Data types|'text in row' table option|Use **varchar(max)**, **nvarchar(max)**, and **varbinary(max)** data types. For more information, see [sp_tableoption &#40;Transact-SQL&#41;](../relational-databases/system-stored-procedures/sp-tableoption-transact-sql.md).|Text in row table option|9|
|Data types|Data types:<br /><br /> **text**<br /><br /> **ntext**<br /><br /> **image**|Use **varchar(max)**, **nvarchar(max)**, and **varbinary(max)** data types.|Data types: **text**, **ntext** or **image**|4|
|Database management|sp_attach_db<br /><br /> sp_attach_single_file_db|CREATE DATABASE statement with the FOR ATTACH option. To rebuild multiple log files, when one or more have a new location, use the FOR ATTACH_REBUILD_LOG option.|sp_attach_db<br /><br /> sp_attach_single_file_db|81<br /><br /> 82|
|Database objects|CREATE DEFAULT<br /><br /> DROP DEFAULT<br /><br /> sp_bindefault<br /><br /> sp_unbindefault|DEFAULT keyword in CREATE TABLE and ALTER TABLE|CREATE_DROP_DEFAULT<br /><br /> sp_bindefault<br /><br /> sp_unbindefault|162<br /><br /> 64<br /><br /> 65|
|Database objects|CREATE RULE<br /><br /> DROP RULE<br /><br /> sp_bindrule<br /><br /> sp_unbindrule|CHECK keyword in CREATE TABLE and ALTER TABLE|CREATE_DROP_RULE<br /><br /> sp_bindrule<br /><br /> sp_unbindrule|161<br /><br /> 66<br /><br /> 67|
|Database objects|sp_change_users_login|Use ALTER USER.|sp_change_users_login|231|
|Database objects|sp_depends|sys.dm_sql_referencing_entities and sys.dm_sql_referenced_entities|sp_depends|19|
|Database objects|sp_renamedb|MODIFY NAME in ALTER DATABASE|sp_renamedb|79|
|Database objects|sp_getbindtoken|Use MARS or distributed transactions.|sp_getbindtoken|98|
|Database options|sp_bindsession|Use MARS or distributed transactions.|sp_bindsession|97|
|Database options|sp_resetstatus|ALTER DATABASE SET { ONLINE &#124; EMERGENCY }|sp_resetstatus|83|
|Database options|TORN_PAGE_DETECTION option of ALTER DATABASE|PAGE_VERIFY TORN_PAGE_DETECTION option of ALTER DATABASE|ALTER DATABASE WITH TORN_PAGE_DETECTION|102|
|DBCC|DBCC DBREINDEX|REBUILD option of ALTER INDEX.|DBCC DBREINDEX|11|
|DBCC|DBCC INDEXDEFRAG|REORGANIZE option of ALTER INDEX|DBCC INDEXDEFRAG|18|
|DBCC|DBCC SHOWCONTIG|sys.dm_db_index_physical_stats|DBCC SHOWCONTIG|10|
|DBCC|DBCC PINTABLE<br /><br /> DBCC UNPINTABLE|Has no effect.|DBCC [UN]PINTABLE|189|
|Extended properties|Level0type = 'type' and Level0type = 'USER' to add extended properties to level-1 or level-2 type objects.|Use Level0type = 'USER' only to add an extended property directly to a user or role.<br /><br /> Use Level0type = 'SCHEMA' to add an extended property to level-1 types such as TABLE or VIEW, or level-2 types such as COLUMN or TRIGGER. For more information, see [sp_addextendedproperty &#40;Transact-SQL&#41;](../relational-databases/system-stored-procedures/sp-addextendedproperty-transact-sql.md).|EXTPROP_LEVEL0TYPE<br /><br /> EXTPROP_LEVEL0USER|13<br /><br /> 14|
|Extended stored procedure programming|srv_alloc<br /><br /> srv_convert<br /><br /> srv_describe<br /><br /> srv_getbindtoken<br /><br /> srv_got_attention<br /><br /> srv_message_handler<br /><br /> srv_paramdata<br /><br /> srv_paraminfo<br /><br /> srv_paramlen<br /><br /> srv_parammaxlen<br /><br /> srv_paramname<br /><br /> srv_paramnumber<br /><br /> srv_paramset<br /><br /> srv_paramsetoutput<br /><br /> srv_paramstatus<br /><br /> srv_paramtype<br /><br /> srv_pfield<br /><br /> srv_pfieldex<br /><br /> srv_rpcdb<br /><br /> srv_rpcname<br /><br /> srv_rpcnumber<br /><br /> srv_rpcoptions<br /><br /> srv_rpcowner<br /><br /> srv_rpcparams<br /><br /> srv_senddone<br /><br /> srv_sendmsg<br /><br /> srv_sendrow<br /><br /> srv_setcoldata<br /><br /> srv_setcollen<br /><br /> srv_setutype<br /><br /> srv_willconvert<br /><br /> srv_wsendmsg|Use CLR Integration instead.|XP_API|20|
|Extended stored procedure programming|sp_addextendedproc<br /><br /> sp_dropextendedproc<br /><br /> sp_helpextendedproc|Use CLR Integration instead.|sp_addextendedproc<br /><br /> sp_dropextendedproc<br /><br /> sp_helpextendedproc|94<br /><br /> 95<br /><br /> 96|
|Extended stored procedures|xp_grantlogin<br /><br /> xp_revokelogin<br /><br /> xp_loginConfig|Use CREATE LOGIN<br /><br /> Use DROP LOGIN IsIntegratedSecurityOnly argument of SERVERPROPERTY|xp_grantlogin<br /><br /> xp_revokelogin<br /><br /> xp_loginconfig|44<br /><br /> 45<br /><br /> 59|
|Functions|fn_get_sql|sys.dm_exec_sql_text|fn_get_sql|151|
|High availability|database mirroring|[!INCLUDE[ssHADR](../includes/sshadr-md.md)]<br /><br /> If your edition of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] does not support [!INCLUDE[ssHADR](../includes/sshadr-md.md)], use log shipping.|DATABASE_MIRRORING|267|
|Index options|sp_indexoption|ALTER INDEX|sp_indexoption|78|
|Index options|CREATE TABLE, ALTER TABLE, or CREATE INDEX syntax without parentheses around the options.|Rewrite the statement to use the current syntax.|INDEX_OPTION|33|
|Instance options|sp_configure option 'allow updates'|System tables are no longer updatable. Setting has no effect.|sp_configure 'allow updates'|173|
|Instance options|sp_configure options:<br /><br /> 'locks'<br /><br /> 'open objects'<br /><br /> 'set working set size'|Now automatically configured. Setting has no effect.|sp_configure 'locks'<br /><br /> sp_configure 'open objects'<br /><br /> sp_configure 'set working set size'|174<br /><br /> 175<br /><br /> 176|
|Instance options|sp_configure option 'priority boost'|System tables are no longer updatable. Setting has no effect. Use the Windows start /high ... program.exe option instead.|sp_configure 'priority boost'|199|
|Instance options|sp_configure option 'remote proc trans'|System tables are no longer updatable. Setting has no effect.|sp_configure 'remote proc trans'|37|
|Linked servers|Specifying the SQLOLEDB provider for linked servers.|[Microsoft OLE DB Driver (MSOLEDBSQL) for SQL Server](../connect/oledb/oledb-driver-for-sql-server.md)|SQLOLEDB for linked servers|19|
|Locking|sp_lock|sys.dm_tran_locks|sp_lock|99|
|Metadata|FILE_ID<br /><br /> INDEXKEY_PROPERTY|FILE_IDEX<br /><br /> sys.index_columns|FILE_ID<br /><br /> INDEXKEY_PROPERTY|15<br /><br /> 17|
|Native XML Web Services|The CREATE ENDPOINT or ALTER ENDPOINT statement with the FOR SOAP option.<br /><br /> sys.endpoint_webmethods<br /><br /> sys.soap_endpoints|Use Windows Communications Foundation (WCF) or ASP.NET instead.|CREATE/ALTER ENDPOINT<br /><br /> sys.endpoint_webmethods<br /><br /> EXT_soap_endpoints<br /><br /> sys.soap_endpoints|21<br /><br /> 22<br /><br /> 23|
|Removable databases|sp_certify_removable<br /><br /> sp_create_removable|sp_detach_db|sp_certify_removable<br /><br /> sp_create_removable|74<br /><br /> 75|
|Removable databases|sp_dbremove|DROP DATABASE|sp_dbremove|76|
|Security|The ALTER LOGIN WITH SET CREDENTIAL syntax|Replaced by the new ALTER LOGIN ADD and DROP CREDENTIAL syntax|ALTER LOGIN WITH SET CREDENTIAL|230|
|Security|sp_addapprole<br /><br /> sp_dropapprole|CREATE APPLICATION ROLE<br /><br /> DROP APPLICATION ROLE|sp_addapprole<br /><br /> sp_dropapprole|53<br /><br /> 54|
|Security|sp_addlogin<br /><br /> sp_droplogin|CREATE LOGIN<br /><br /> DROP LOGIN|sp_addlogin<br /><br /> sp_droplogin|39<br /><br /> 40|
|Security|sp_adduser<br /><br /> sp_dropuser|CREATE USER<br /><br /> DROP USER|sp_adduser<br /><br /> sp_dropuser|49<br /><br /> 50|
|Security|sp_grantdbaccess<br /><br /> sp_revokedbaccess|CREATE USER<br /><br /> DROP USER|sp_grantdbaccess<br /><br /> sp_revokedbaccess|51<br /><br /> 52|
|Security|sp_addrole<br /><br /> sp_droprole|CREATE ROLE<br /><br /> DROP ROLE|sp_addrole<br /><br /> sp_droprole|56<br /><br /> 57|
|Security|sp_approlepassword<br /><br /> sp_password|ALTER APPLICATION ROLE<br /><br /> ALTER LOGIN|sp_approlepassword<br /><br /> sp_password|55<br /><br /> 46|
|Security|sp_changeobjectowner|ALTER SCHEMA or ALTER AUTHORIZATION|sp_changeobjectowner|58|
|Security|sp_control_dbmasterkey_password|A master key must exist and password must be correct.|sp_control_dbmasterkey_password|274|
|Security|sp_defaultdb<br /><br /> sp_defaultlanguage|ALTER LOGIN|sp_defaultdb<br /><br /> sp_defaultlanguage|47<br /><br /> 48|
|Security|sp_denylogin<br /><br /> sp_grantlogin<br /><br /> sp_revokelogin|ALTER LOGIN DISABLE<br /><br /> CREATE LOGIN<br /><br /> DROP LOGIN|sp_denylogin<br /><br /> sp_grantlogin<br /><br /> sp_revokelogin|42<br /><br /> 41<br /><br /> 43|
|Security|USER_ID|DATABASE_PRINCIPAL_ID|USER_ID|16|
|Security|sp_srvrolepermission<br /><br /> sp_dbfixedrolepermission|These stored procedures return information that was correct in [!INCLUDE[ssVersion2000](../includes/ssversion2000-md.md)]. The output does not reflect changes to the permissions hierarchy implemented in [!INCLUDE[ssKatmai](../includes/sskatmai-md.md)]. For more information, see [Permissions of Fixed Server Roles](https://msdn.microsoft.com/library/ms175892\(SQL.100\).aspx).|sp_srvrolepermission<br /><br /> sp_dbfixedrolepermission|61<br /><br /> 60|
|Security|GRANT ALL<br /><br /> DENY ALL<br /><br /> REVOKE ALL|GRANT, DENY, and REVOKE specific permissions.|ALL Permission|35|
|Security|PERMISSIONS intrinsic function|Query sys.fn_my_permissions instead.|PERMISSIONS|170|
|Security|SETUSER|EXECUTE AS|SETUSER|165|
|Security|RC4 and DESX encryption algorithms|Use another algorithm such as AES.|DESX algorithm|238|
|SET options|SET FMTONLY|[sys.dm_exec_describe_first_result_set &#40;Transact-SQL&#41;](../relational-databases/system-dynamic-management-views/sys-dm-exec-describe-first-result-set-transact-sql.md), [sys.dm_exec_describe_first_result_set_for_object &#40;Transact-SQL&#41;](../relational-databases/system-dynamic-management-views/sys-dm-exec-describe-first-result-set-for-object-transact-sql.md), [sp_describe_first_result_set &#40;Transact-SQL&#41;](../relational-databases/system-stored-procedures/sp-describe-first-result-set-transact-sql.md), and [sp_describe_undeclared_parameters &#40;Transact-SQL&#41;](../relational-databases/system-stored-procedures/sp-describe-undeclared-parameters-transact-sql.md).|SET FMTONLY|250|
|Server Configuration Options|c2 audit option<br /><br /> default trace enabled option|[common criteria compliance enabled Server Configuration Option](../database-engine/configure-windows/common-criteria-compliance-enabled-server-configuration-option.md)<br /><br /> [Extended Events](../relational-databases/extended-events/extended-events.md)|sp_configure 'c2 audit mode'<br /><br /> sp_configure 'default trace enabled'|252<br /><br /> 253|
|SMO classes|**Microsoft.SQLServer. Management.Smo.Information** class<br /><br /> **Microsoft.SQLServer. Management.Smo.Settings** class<br /><br /> **Microsoft.SQLServer.Management. Smo.DatabaseOptions** class<br /><br /> **Microsoft.SqlServer.Management.Smo. DatabaseDdlTrigger.NotForReplication** property|**Microsoft.SqlServer.  Management.Smo.Server** class<br /><br /> **Microsoft.SqlServer.  Management.Smo.Server** class<br /><br /> **Microsoft.SqlServer. Management.Smo.Database** class<br /><br /> None|None|None|
|SQL Server Agent|**net send** notification<br /><br /> Pager notification|E-mail notification<br /><br /> E-mail notification |None|None|
|[!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)]|Solution Explorer integration in [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)]||None|None|
|System Stored Procedures|sp_db_increased_partitions|None. Support for increased partitions is now available by default.|sp_db_increased_partitions|253|
|System tables|sysaltfiles<br /><br /> syscacheobjects<br /><br /> syscolumns<br /><br /> syscomments<br /><br /> sysconfigures<br /><br /> sysconstraints<br /><br /> syscurconfigs<br /><br /> sysdatabases<br /><br /> sysdepends<br /><br /> sysdevices<br /><br /> sysfilegroups<br /><br /> sysfiles<br /><br /> sysforeignkeys<br /><br /> sysfulltextcatalogs<br /><br /> sysindexes<br /><br /> sysindexkeys<br /><br /> syslockinfo<br /><br /> syslogins<br /><br /> sysmembers<br /><br /> sysmessages<br /><br /> sysobjects<br /><br /> sysoledbusers<br /><br /> sysopentapes<br /><br /> sysperfinfo<br /><br /> syspermissions<br /><br /> sysprocesses<br /><br /> sysprotects<br /><br /> sysreferences<br /><br /> sysremotelogins<br /><br /> sysservers<br /><br /> systypes<br /><br /> sysusers|Compatibility views. For more information, see [Compatibility Views &#40;Transact-SQL&#41;](~/relational-databases/system-compatibility-views/system-compatibility-views-transact-sql.md).<br /><br /> **Important:** The compatibility views do not expose metadata for features that were introduced in [!INCLUDE[ssVersion2005](../includes/ssversion2005-md.md)]. We recommend that you upgrade your applications to use catalog views. For more information, see [Catalog Views &#40;Transact-SQL&#41;](../relational-databases/system-catalog-views/catalog-views-transact-sql.md).|sysaltfiles<br /><br /> syscacheobjects<br /><br /> syscolumns<br /><br /> syscomments<br /><br /> sysconfigures<br /><br /> sysconstraints<br /><br /> syscurconfigs<br /><br /> sysdatabases<br /><br /> sysdepends<br /><br /> sysdevices<br /><br /> sysfilegroups<br /><br /> sysfiles<br /><br /> sysforeignkeys<br /><br /> sysfulltextcatalogs<br /><br /> sysindexes<br /><br /> sysindexkeys<br /><br /> syslockinfo<br /><br /> syslogins<br /><br /> sysmembers<br /><br /> sysmessages<br /><br /> sysobjects<br /><br /> sysoledbusers<br /><br /> sysopentapes<br /><br /> sysperfinfo<br /><br /> syspermissions<br /><br /> sysprocesses<br /><br /> sysprotects<br /><br /> sysreferences<br /><br /> sysremotelogins<br /><br /> sysservers<br /><br /> systypes<br /><br /> sysusers|141<br /><br /> None<br /><br /> 133<br /><br /> 126<br /><br /> 146<br /><br /> 131<br /><br /> 147<br /><br /> 142<br /><br /> 123<br /><br /> 144<br /><br /> 128<br /><br /> 127<br /><br /> 130<br /><br /> 122<br /><br /> 132<br /><br /> 134<br /><br /> 143<br /><br /> 140<br /><br /> 119<br /><br /> 137<br /><br /> 125<br /><br /> 139<br /><br /> 145<br /><br /> 157<br /><br /> 121<br /><br /> 153<br /><br /> 120<br /><br /> 129<br /><br /> 138<br /><br /> 136<br /><br /> 135<br /><br /> 124|
|System tables|sys.numbered_procedures<br /><br /> sys.numbered_procedure_parameters|None|numbered_procedures<br /><br /> numbered_procedure_parameters|148<br /><br /> 149|
|System functions|fn_virtualservernodes<br /><br /> fn_servershareddrives|sys.dm_os_cluster_nodes<br /><br /> sys.dm_io_cluster_shared_drives|fn_virtualservernodes<br /><br /> fn_servershareddrives|155<br /><br /> 156|
|System views|sys.sql_dependencies|sys.sql_expression_dependencies|sys.sql_dependencies|198|
|Table compression|The use of the vardecimal storage format.|Vardecimal storage format is deprecated. Data compression in this version compresses decimal values as well as other data types. We recommend that you use data compression instead of the vardecimal storage format.|Vardecimal storage format|200|
|Table compression|Use of the sp_db_vardecimal_storage_format procedure.|Vardecimal storage format is deprecated. [!INCLUDE[ssnoversion](../includes/ssnoversion-md.md)] data compression, compresses decimal values as well as other data types. We recommend that you use data compression instead of the vardecimal storage format.|sp_db_vardecimal_storage_format|201|
|Table compression|Use of the sp_estimated_rowsize_reduction_for_vardecimal procedure.|Use data compression and the sp_estimate_data_compression_savings procedure instead.|sp_estimated_rowsize_reduction_for_vardecimal|202|
|Table hints|Specifying NOLOCK or READUNCOMMITTED in the FROM clause of an UPDATE or DELETE statement.|Remove the NOLOCK or READUNCOMMITTED table hints from the FROM clause.|NOLOCK or READUNCOMMITTED in UPDATE or DELETE|1|
|Table hints|Specifying table hints without using the WITH keyword.|Use WITH.|Table hint without WITH|8|
|Table hints|INSERT_HINTS||INSERT_HINTS|34|
|Textpointers|WRITETEXT<br /><br /> UPDATETEXT<br /><br /> READTEXT|None|UPDATETEXT or WRITETEXT<br /><br /> READTEXT|115<br /><br /> 114|
|Textpointers|TEXTPTR()<br /><br /> TEXTVALID()|None|TEXTPTR<br /><br /> TEXTVALID|5<br /><br /> 6|
|[!INCLUDE[tsql](../includes/tsql-md.md)]|:: function-calling sequence|Replaced by SELECT *column_list* FROM sys.\<*function_name*>().<br /><br /> For example, replace `SELECT * FROM ::fn_virtualfilestats(2,1)`with `SELECT * FROM sys.fn_virtualfilestats(2,1)`.|'::' function calling syntax|166|
|[!INCLUDE[tsql](../includes/tsql-md.md)]|Three-part and four-part column references.|Two-part names is the standard-compliant behavior.|More than two-part column name|3|
|[!INCLUDE[tsql](../includes/tsql-md.md)]|A string enclosed in quotation marks used as a column alias for an expression in a SELECT list:<br /><br /> '*string_alias*' = *expression*|*expression* [AS] *column_alias*<br /><br /> *expression* [AS] [*column_alias*]<br /><br /> *expression* [AS] "*column_alias*"<br /><br /> *expression* [AS] '*column_alias*'<br /><br /> *column_alias* = *expression*|String literals as column aliases|184|
|[!INCLUDE[tsql](../includes/tsql-md.md)]|Numbered procedures|None. Do not use.|ProcNums|160|
|[!INCLUDE[tsql](../includes/tsql-md.md)]|*table_name.index_name* syntax in DROP INDEX|*index_name* ON *table_name* syntax in DROP INDEX.|DROP INDEX with two-part name|163|
|[!INCLUDE[tsql](../includes/tsql-md.md)]|Not ending [!INCLUDE[tsql](../includes/tsql-md.md)] statements with a semicolon.|End [!INCLUDE[tsql](../includes/tsql-md.md)] statements with a semicolon ( ; ).|None|None|
|[!INCLUDE[tsql](../includes/tsql-md.md)]|GROUP BY ALL|Use custom case-by-case solution with UNION or derived table.|GROUP BY ALL|169|
|[!INCLUDE[tsql](../includes/tsql-md.md)]|ROWGUIDCOL as a column name in DML statements.|Use $rowguid.|ROWGUIDCOL|182|
|[!INCLUDE[tsql](../includes/tsql-md.md)]|IDENTITYCOL as a column name in DML statements.|Use $identity.|IDENTITYCOL|183|
|[!INCLUDE[tsql](../includes/tsql-md.md)]|Use of #, ## as temporary table and temporary stored procedure names.|Use at least one additional character.|'#' and '##' as the name of temporary tables and stored procedures|185|
|[!INCLUDE[tsql](../includes/tsql-md.md)]|Use of \@, \@\@, or \@\@ as [!INCLUDE[tsql](../includes/tsql-md.md)] identifiers.|Do not use \@ or \@\@ or names that begin with \@\@ as identifiers.|'\@' and names that start with '\@\@' as [!INCLUDE[tsql](../includes/tsql-md.md)] identifiers|186.|
|[!INCLUDE[tsql](../includes/tsql-md.md)]|Use of DEFAULT keyword as default value.|Do not use the word DEFAULT as a default value.|DEFAULT keyword as a default value|187|
|[!INCLUDE[tsql](../includes/tsql-md.md)]|Use of a space as a separator between table hints.|Use a comma to separate table hints.|Multiple table hints without comma|168|
|[!INCLUDE[tsql](../includes/tsql-md.md)]|The select list of an aggregate indexed view must contain COUNT_BIG (\*) in 90 compatibility mode|Use COUNT_BIG (\*).|Index view select list without COUNT_BIG(\*)|2|
|[!INCLUDE[tsql](../includes/tsql-md.md)]|The indirect application of table hints to an invocation of a multi-statement table-valued function (TVF) through a view.|None.|Indirect TVF hints|7|
|[!INCLUDE[tsql](../includes/tsql-md.md)]|ALTER DATABASE syntax:<br /><br /> MODIFY FILEGROUP READONLY<br /><br /> MODIFY FILEGROUP READWRITE|MODIFY FILEGROUP READ_ONLY<br /><br /> MODIFY FILEGROUP READ_WRITE|MODIFY FILEGROUP READONLY<br /><br /> MODIFY FILEGROUP READWRITE|195<br /><br /> 196|
|Other|DB-Library<br /><br /> Embedded SQL for C|Although the [!INCLUDE[ssDE](../includes/ssde-md.md)] still supports connections from existing applications that use the DB-Library and Embedded SQL APIs, it does not include the files or documentation required to do programming work on applications that use these APIs. A future version of the [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] will drop support for connections from DB-Library or Embedded SQL applications. Do not use DB-Library or Embedded SQL to develop new applications. Remove any dependencies on either DB-Library or Embedded SQL when you are modifying existing applications. Instead of these APIs, use the SQLClient namespace or an API such as ODBC. The current version does not include the DB-Library DLL required to run these applications. To run DB-Library or Embedded SQL applications, you must have available the DB-Library DLL from [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] version 6.5, [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] 7.0, or [!INCLUDE[ssVersion2000](../includes/ssversion2000-md.md)].|None|None|
|Tools|SQL Server Profiler for Trace Capture|Use Extended Events Profiler embedded in SQL Server Management Studio.|SQL Server Profiler|None|
|Tools|SQL Server Profiler for Trace Replay|[SQL Server Distributed Replay](../tools/distributed-replay/sql-server-distributed-replay.md)|SQL Server Profiler|None|
|Trace Management Objects|Microsoft.SqlServer.Management.Trace namespace (contains the APIs for SQL Server Trace and Replay objects)|Trace Configuration: <xref:Microsoft.SqlServer.Management.XEvent><br /><br /> Trace Reading: <xref:Microsoft.SqlServer.XEvent.Linq><br /><br /> Trace Replay: None|||
|SQL Trace stored procedures, functions, and catalog views|sp_trace_create<br /><br /> sp_trace_setevent<br /><br /> sp_trace_setfilter<br /><br /> sp_trace_setstatus<br /><br /> fn_trace_geteventinfo<br /><br /> fn_trace_getfilterinfo<br /><br /> fn_trace_getinfo<br /><br /> fn_trace_gettable<br /><br /> sys.traces<br /><br /> sys.trace_events<br /><br /> sys.trace_event_bindings<br /><br /> sys.trace_categories<br /><br /> sys.trace_columns<br /><br /> sys.trace_subclass_values|[Extended Events](../relational-databases/extended-events/extended-events.md)|sp_trace_create<br /><br /> sp_trace_setevent<br /><br /> sp_trace_setfilter<br /><br /> sp_trace_setstatus<br /><br /> fn_trace_geteventinfo<br /><br /> fn_trace_getfilterinfo<br /><br /> fn_trace_getinfo<br /><br /> fn_trace_gettable<br /><br /> sys.traces<br /><br /> sys.trace_events<br /><br /> sys.trace_event_bindings<br /><br /> sys.trace_categories<br /><br /> sys.trace_columns<br /><br /> sys.trace_subclass_values|258<br /><br /> 260<br /><br /> 261<br /><br /> 259<br /><br /> 256<br /><br /> 257|
|Set options|**SET ROWCOUNT** for **INSERT**, **UPDATE**, and **DELETE** statements|TOP keyword|SET ROWCOUNT|109|

> [!NOTE]  
> The cookie **OUTPUT** parameter for **sp_setapprole** is currently documented as **varbinary(8000)** which is the correct maximum length. However the current implementation returns **varbinary(50)**. If developers have allocated **varbinary(50)** the application might require changes if the cookie return size increases in a future release. Though not a deprecation issue this is mentioned in this topic because the application adjustments are similar. For more information, see [sp_setapprole &#40;Transact-SQL&#41;](../relational-databases/system-stored-procedures/sp-setapprole-transact-sql.md).

## See also

- [Discontinued Database Engine Functionality in SQL Server 2016](./discontinued-database-engine-functionality-in-sql-server.md)
- [Deprecated Database Engine Features in SQL Server 2017](../database-engine/deprecated-database-engine-features-in-sql-server-2017.md)
