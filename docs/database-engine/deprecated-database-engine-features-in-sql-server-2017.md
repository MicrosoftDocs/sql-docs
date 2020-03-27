---
title: "Deprecated Database Engine Features | Microsoft Docs"
titleSuffix: "SQL Server 2019"
description: Find out about deprecated database engine features that are still available in SQL Server 2017 (14.x), but shouldn't be used in new applications.
ms.custom: "seo-lt-2019"
ms.date: "03/30/2020"
ms.prod: sql
ms.prod_service: high-availability
ms.reviewer: ""
ms.technology: release-landing
ms.topic: conceptual
helpviewer_keywords: 
  - "deprecated features [SQL Server]"
  - "Database Engine [SQL Server], backward compatibility"
  - "deprecation [SQL Server], feature list"
ms.assetid: 
author: MikeRayMSFT
ms.author: mikeray
monikerRange: ">=sql-server-2017||=sqlallproducts-allversions||>=sql-server-linux-2017"
---

# Deprecated Database Engine Features in SQL Server 2017

[!INCLUDE[tsql-appliesto-ss2017-xxxx-xxxx-xxx-md](../includes/tsql-appliesto-ss2017-xxxx-xxxx-xxx-md.md)]

This topic describes the deprecated SQL Server Database Engine features that are still available in SQL Server 2017 (14.x). Deprecated features should not be used in new applications.  

When a feature is marked deprecated, it means:

- The feature is in maintenance mode only. No new changes will be done, including those related to inter-operability with new features.

- We strive not to remove a deprecated feature from future releases to make upgrades easier. However, under rare situations, we may choose to permanently remove the feature from SQL Server if it limits future innovations.

- For new development work, we do not recommend using deprecated features.

You can monitor the use of deprecated features by using the SQL Server Deprecated Features Object performance counter and trace events. For more information, see [Use SQL Server Objects](../relational-databases/performance-monitor/use-sql-server-objects.md).

The value of these counters are also available by executing the following statement:

```sql
SELECT * FROM sys.dm_os_performance_counter
WHERE object_name = 'SQLServer:Deprecated Features';
```

> [!NOTE]
> This list is identical to the [!INCLUDE[sssql15-md](../includes/sssql15-md.md)] list. There are no new deprecated or discontinued Database Engine features announced for [!INCLUDE[sssqlv14-md](../includes/sssqlv14-md.md)].

## Features deprecated in the next version of SQL Server

The following SQL Server Database Engine features will be deprecated in the next version of SQL Server. Do not use these features in new development work, and modify applications that currently use these features as soon as possible. The **Feature name** value appears in trace events as the ObjectName and in performance counters and `sys.dm_os_performance_counters` as the instance name.

### Back up and Restore

| Deprecated feature | Replacement | Feature name |
|--------------------|-------------|--------------|
| RESTORE { DATABASE &#124; LOG } WITH [MEDIA]PASSWORD continues to be deprecated. BACKUP { DATABASE &#124; LOG } WITH PASSWORD and BACKUP { DATABASE &#124; LOG } WITH MEDIAPASSWORD are discontinued. | None. | BACKUP DATABASE or LOG WITH PASSWORD </br></br> BACKUP DATABASE or LOG WITH MEDIAPASSWORD |

### Compatibility levels

| Deprecated feature | Replacement | Feature name |
|--------------------|-------------|--------------|
Upgrade from version 100 (SQL Server 2008 and  SQL Server 2008 R2). | When a SQL Server version goes out of [support](https://aka.ms/sqllifecycle), the associated Database Compatibility Level will be marked deprecated. However, we will continue to support applications certified on any supported database compatibility level as long as possible, to make the upgrades easier. For more information about compatibility levels, see [ALTER DATABASE Compatibility Level &#40;Transact-SQL&#41;](../t-sql/statements/alter-database-transact-sql-compatibility-level.md). | Database compatibility level 100 |

### Database objects

| Deprecated feature | Replacement | Feature name |
|--------------------|-------------|--------------|
| Ability to return result sets from triggers | None | Returning results from trigger |

### Encryption

| Deprecated feature | Replacement | Feature name |
|--------------------|-------------|--------------|
| Encryption using RC4 or RC4_128 is deprecated and is scheduled to be removed in the next version. Decrypting RC4 and RC4_128 is not deprecated. | Use another encryption algorithm such as AES. | Deprecated encryption algorithm |

| Deprecated feature | Replacement | Feature name |
|--------------------|-------------|--------------|
| Hash algorithms | Using the MD2, MD4, MD5, SHA, and SHA1 is deprecated. | Use SHA2_256 or SHA2_512 instead. Older algorithms will continue working, but they will raise a deprecation event. |Deprecated hash algorithm|

### Remote servers

| Deprecated feature | Replacement | Feature name |
|--------------------|-------------|--------------|
| Remote servers | sp_addremotelogin </br></br> sp_addserver </br></br> sp_dropremotelogin </br></br> sp_helpremotelogin </br></br> sp_remoteoption|Replace remote servers by using linked servers. sp_addserver can only be used with the local option.|sp_addremotelogin </br></br> sp_addserver </br></br> sp_dropremotelogin </br></br> sp_helpremotelogin </br></br> sp_remoteoption |
| Remote servers | \@\@remserver | Replace remote servers by using linked servers. | None |
| Remote servers | SET REMOTE_PROC_TRANSACTIONS|Replace remote servers by using linked servers. | SET REMOTE_PROC_TRANSACTIONS |

### Set options

| Deprecated feature | Replacement | Feature name |
|--------------------|-------------|--------------|
| Set options | **SET ROWCOUNT** for **INSERT**, **UPDATE**, and **DELETE** statements | TOP keyword | SET ROWCOUNT |

### Table hints

| Category | Deprecated feature | Replacement | Feature name |
|----------|--------------------|-------------|--------------|
| HOLDLOCK table hint without parenthesis. | Use HOLDLOCK with parenthesis. | HOLDLOCK table hint without parenthesis |

## Features deprecated in a future version of SQL Server

The following SQL Server Database Engine features are supported in the next version of SQL Server, but will be deprecated in a later version. The specific version of SQL Server has not been determined.

### Compatibility levels

| Deprecated feature | Replacement | Feature name |
|--------------------|-------------|--------------|
| sp_dbcmptlevel|ALTER DATABASE ... SET COMPATIBILITY_LEVEL. For more information, see [ALTER DATABASE Compatibility Level &#40;Transact-SQL&#41;](../t-sql/statements/alter-database-transact-sql-compatibility-level.md). | sp_dbcmptlevel |
| Database compatibility level 110 and 120. | Plan to upgrade the database and application for a future release. However, we will continue to support applications certified on any supported database compatibility level as long as possible, to make the upgrades easier. For more information about compatibility levels, see [ALTER DATABASE Compatibility Level &#40;Transact-SQL&#41;](../t-sql/statements/alter-database-transact-sql-compatibility-level.md). | Database compatibility level 110 </br></br> Database compatibility level 120 |

### Backup and restore

| Deprecated feature | Replacement | Feature name |
|--------------------|-------------|--------------|
| BACKUP { DATABASE &#124; LOG } TO TAPE </br></br> BACKUP { DATABASE &#124; LOG } TO *device_that_is_a_tape*|BACKUP { DATABASE &#124; LOG } TO DISK </br></br> BACKUP { DATABASE &#124; LOG } TO *device_that_is_a_disk* | BACKUP DATABASE or LOG TO TAPE |
| sp_addumpdevice '**tape**' | sp_addumpdevice '**disk**' | ADDING TAPE DEVICE |
| sp_helpdevice|sys.backup_devices | sp_helpdevice |

### Collations

| Deprecated feature | Replacement | Feature name |
|--------------------|-------------|--------------|
| Korean_Wansung_Unicode </br></br> Lithuanian_Classic </br></br> SQL_AltDiction_CP1253_CS_AS | None. These collations exist in  SQL Server 2005 (9.x), but are not visible through fn_helpcollations. | Korean_Wansung_Unicode </br></br> Lithuanian_Classic </br></br> SQL_AltDiction_CP1253_CS_AS |
| Hindi </br></br> Macedonian | These collations exist in  SQL Server 2005 (9.x) and higher, but are not visible through fn_helpcollations. Use Macedonian_FYROM_90 and Indic_General_90 instead.|Hindi </br></br> Macedonian |
| Azeri_Latin_90 </br></br> Azeri_Cyrilllic_90 | Azeri_Latin_100 </br></br> Azeri_Cyrilllic_100 | Azeri_Latin_90 </br></br> Azeri_Cyrilllic_90 |

### Configuration

| Deprecated feature | Replacement | Feature name |
|--------------------|-------------|--------------|
| SET ANSI_NULLS OFF and ANSI_NULLS OFF database option </br></br> SET ANSI_PADDING OFF and ANSI_PADDING OFF database option </br></br> SET CONCAT_NULL_YIELDS_NULL OFF and CONCAT_NULL_YIELDS_NULL OFF database option</br></br> SET OFFSETS | None. </br></br> ANSI_NULLS, ANSI_PADDING and CONCAT_NULLS_YIELDS_NULL will always be set to ON. SET OFFSETS will be unavailable. | SET ANSI_NULLS OFF </br></br> SET ANSI_PADDING OFF </br></br> SET CONCAT_NULL_YIELDS_NULL OFF </br></br> SET OFFSETS </br></br> ALTER DATABASE SET ANSI_NULLS OFF </br></br> ALTER DATABASE SET ANSI_PADDING OFF </br></br> ALTER DATABASE SET CONCAT_NULL_YIELDS_NULL OFF |

### Data types

| Deprecated feature | Replacement | Feature name |
|--------------------|-------------|--------------|
| Data types | sp_addtype </br></br> sp_droptype|CREATE TYPE</br></br> DROP TYPE | sp_addtype</br></br> sp_droptype |
| Data types | **timestamp** syntax for **rowversion** data type | **rowversion** data type syntax | TIMESTAMP |
| Data types | Ability to insert null values into **timestamp** columns. | Use a DEFAULT instead. | INSERT NULL into TIMESTAMP columns |
| Data types | 'text in row' table option|Use **varchar(max)**, **nvarchar(max)**, and **varbinary(max)** data types. For more information, see [sp_tableoption &#40;Transact-SQL&#41;](../relational-databases/system-stored-procedures/sp-tableoption-transact-sql.md).|Text in row table option |
|Data types|Data types:</br></br> **text**</br></br> **ntext**</br></br> **image**|Use **varchar(max)**, **nvarchar(max)**, and **varbinary(max)** data types.|Data types: **text**, **ntext** or **image** |

### Database management

| Deprecated feature | Replacement | Feature name |
|--------------------|-------------|--------------|
| sp_attach_db </br></br> sp_attach_single_file_db|CREATE DATABASE statement with the FOR ATTACH option. To rebuild multiple log files, when one or more have a new location, use the FOR ATTACH_REBUILD_LOG option. | sp_attach_db </br></br> sp_attach_single_file_db |

### Database objects

| Deprecated feature | Replacement | Feature name |
|--------------------|-------------|--------------|
| CREATE DEFAULT</br></br> DROP DEFAULT</br></br> sp_bindefault</br></br> sp_unbindefault|DEFAULT keyword in CREATE TABLE and ALTER TABLE|CREATE_DROP_DEFAULT</br></br> sp_bindefault</br></br> sp_unbindefault |
| CREATE RULE</br></br> DROP RULE</br></br> sp_bindrule</br></br> sp_unbindrule | CHECK keyword in CREATE TABLE and ALTER TABLE | CREATE_DROP_RULE</br></br> sp_bindrule</br></br> sp_unbindrule |
| sp_change_users_login | Use ALTER USER. | sp_change_users_login |
| sp_depends | sys.dm_sql_referencing_entities and sys.dm_sql_referenced_entities | sp_depends |
| sp_renamedb | MODIFY NAME in ALTER DATABASE | sp_renamedb |
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
| DBCC PINTABLE</br></br> DBCC UNPINTABLE | Has no effect. | DBCC [UN]PINTABLE |

### Extended properties

| Level0type = 'type' and Level0type = 'USER' to add extended properties to level-1 or level-2 type objects.|Use Level0type = 'USER' only to add an extended property directly to a user or role.</br></br> Use Level0type = 'SCHEMA' to add an extended property to level-1 types such as TABLE or VIEW, or level-2 types such as COLUMN or TRIGGER. For more information, see [sp_addextendedproperty &#40;Transact-SQL&#41;](../relational-databases/system-stored-procedures/sp-addextendedproperty-transact-sql.md). | EXTPROP_LEVEL0TYPE</br></br> EXTPROP_LEVEL0USER |

### Extended stored procedures

| Deprecated feature | Replacement | Feature name |
|--------------------|-------------|--------------|
| xp_grantlogin</br></br> xp_revokelogin</br></br> xp_loginConfig|Use CREATE LOGIN</br></br> Use DROP LOGIN IsIntegratedSecurityOnly argument of SERVERPROPERTY|xp_grantlogin </br></br> | | 
xp_revokelogin</br></br> xp_loginconfig |
| srv_alloc</br></br> srv_convert</br></br> srv_describe</br></br> srv_getbindtoken</br></br> srv_got_attention</br></br> srv_message_handler</br></br> srv_paramdata</br></br> srv_paraminfo</br></br> srv_paramlen</br></br> srv_parammaxlen</br></br> srv_paramname</br></br> srv_paramnumber</br></br> srv_paramset</br></br> srv_paramsetoutput</br></br> srv_paramstatus</br></br> srv_paramtype</br></br> srv_pfield</br></br> srv_pfieldex</br></br> srv_rpcdb</br></br> srv_rpcname</br></br> srv_rpcnumber</br></br> srv_rpcoptions</br></br> srv_rpcowner</br></br> srv_rpcparams</br></br> srv_senddone</br></br> srv_sendmsg</br></br> srv_sendrow</br></br> srv_setcoldata</br></br> srv_setcollen</br></br> srv_setutype</br></br> srv_willconvert</br></br> srv_wsendmsg|Use CLR Integration instead. | XP_API |
| sp_addextendedproc</br></br> sp_dropextendedproc</br></br> sp_helpextendedproc|Use CLR Integration instead. | sp_addextendedproc</br></br> sp_dropextendedproc</br></br> sp_helpextendedproc |

### Function

| Deprecated feature | Replacement | Feature name |
|--------------------|-------------|--------------|
| fn_get_sql | sys.dm_exec_sql_text | fn_get_sql |

### High availability

| Deprecated feature | Replacement | Feature name |
|--------------------|-------------|--------------|
| database mirroring | Always On availability groups</br></br> If your edition of SQL Server does not support Always On availability groups, use log shipping. | DATABASE_MIRRORING |

### Index options

| Deprecated feature | Replacement | Feature name |
|--------------------|-------------|--------------|
| sp_indexoption | ALTER INDEX | sp_indexoption |
| CREATE TABLE, ALTER TABLE, or CREATE INDEX syntax without parentheses around the options. | Rewrite the statement to use the current syntax. | INDEX_OPTION |

### Instance options

| Deprecated feature | Replacement | Feature name |
|--------------------|-------------|--------------|
| sp_configure option 'allow updates'|System tables are no longer updatable. Setting has no effect. | sp_configure 'allow updates' |
| sp_configure options: </br></br> 'locks' </br></br> 'open objects'</br></br> 'set working set size' | Now automatically configured. Setting has no effect. | sp_configure 'locks'</br></br> sp_configure 'open objects'</br></br> sp_configure 'set working set size' |
| sp_configure option 'priority boost' | System tables are no longer updatable. Setting has no effect. Use the Windows start /high ... program.exe option instead. | sp_configure 'priority boost' |
| sp_configure option 'remote proc trans'|System tables are no longer updatable. Setting has no effect. | sp_configure 'remote proc trans' |

### Linked servers

| Deprecated feature | Replacement | Feature name |
|--------------------|-------------|--------------|
| Specifying the SQLOLEDB provider for linked servers. | SQL Server Native Client (SQLNCLI) | SQLOLEDDB for linked servers |

### Locking

| Deprecated feature | Replacement | Feature name |
|--------------------|-------------|--------------|
| sp_lock | sys.dm_tran_locks | sp_lock |

### Metadata

| Deprecated feature | Replacement | Feature name |
|--------------------|-------------|--------------|
| FILE_ID</br></br> INDEXKEY_PROPERTY | FILE_IDEX</br></br> sys.index_columns | FILE_ID</br></br> INDEXKEY_PROPERTY |

### Native XML WEb Services

| Deprecated feature | Replacement | Feature name |
|--------------------|-------------|--------------|
| Native XML Web Services | The CREATE ENDPOINT or ALTER ENDPOINT statement with the FOR SOAP option.</br></br> sys.endpoint_webmethods</br></br> sys.soap_endpoints|Use Windows Communications Foundation (WCF) or ASP.NET instead.|CREATE/ALTER ENDPOINT</br></br> sys.endpoint_webmethods</br></br> EXT_soap_endpoints</br></br> sys.soap_endpoints

| Deprecated feature | Replacement | Feature name |
|--------------------|-------------|--------------|
|Removable databases|sp_certify_removable</br></br> sp_create_removable|sp_detach_db|sp_certify_removable</br></br> sp_create_removable|74</br></br> 75|  
|Removable databases|sp_dbremove|DROP DATABASE|sp_dbremove|76|  

| Deprecated feature | Replacement | Feature name |
|--------------------|-------------|--------------|
|Security|The ALTER LOGIN WITH SET CREDENTIAL syntax|Replaced by the new ALTER LOGIN ADD and DROP CREDENTIAL syntax|ALTER LOGIN WITH SET CREDENTIAL|230|  
|Security|sp_addapprole</br></br> sp_dropapprole|CREATE APPLICATION ROLE</br></br> DROP APPLICATION ROLE|sp_addapprole</br></br> sp_dropapprole|53</br></br> 54|  
|Security|sp_addlogin</br></br> sp_droplogin|CREATE LOGIN</br></br> DROP LOGIN|sp_addlogin</br></br> sp_droplogin|39</br></br> 40|  
|Security|sp_adduser</br></br> sp_dropuser|CREATE USER</br></br> DROP USER|sp_adduser</br></br> sp_dropuser|49</br></br> 50|  
|Security|sp_grantdbaccess</br></br> sp_revokedbaccess|CREATE USER</br></br> DROP USER|sp_grantdbaccess</br></br> sp_revokedbaccess|51</br></br> 52|  
|Security|sp_addrole</br></br> sp_droprole|CREATE ROLE</br></br> DROP ROLE|sp_addrole</br></br> sp_droprole|56</br></br> 57|  
|Security|sp_approlepassword</br></br> sp_password|ALTER APPLICATION ROLE</br></br> ALTER LOGIN|sp_approlepassword</br></br> sp_password|55</br></br> 46|  
|Security|sp_changedbowner|ALTER AUTHORIZATION|sp_changedbowner|243|
|Security|sp_changeobjectowner|ALTER SCHEMA or ALTER AUTHORIZATION|sp_changeobjectowner|58|  
|Security|sp_control_dbmasterkey_password|A master key must exist and password must be correct.|sp_control_dbmasterkey_password|274|  
|Security|sp_defaultdb</br></br> sp_defaultlanguage|ALTER LOGIN|sp_defaultdb</br></br> sp_defaultlanguage|47</br></br> 48|  
|Security|sp_denylogin</br></br> sp_grantlogin</br></br> sp_revokelogin|ALTER LOGIN DISABLE</br></br> CREATE LOGIN</br></br> DROP LOGIN|sp_denylogin</br></br> sp_grantlogin</br></br> sp_revokelogin|42</br></br> 41</br></br> 43|  
|Security|USER_ID|DATABASE_PRINCIPAL_ID|USER_ID|16|  
|Security|sp_srvrolepermission</br></br> sp_dbfixedrolepermission|These stored procedures return information that was correct in [!INCLUDE[ssVersion2000](../includes/ssversion2000-md.md)]. The output does not reflect changes to the permissions hierarchy implemented in SQL Server 2008. For more information, see [Permissions of Fixed Server Roles](https://msdn.microsoft.com/library/ms175892\(SQL.100\).aspx).|sp_srvrolepermission</br></br> sp_dbfixedrolepermission|61</br></br> 60|  
|Security|GRANT ALL</br></br> DENY ALL</br></br> REVOKE ALL|GRANT, DENY, and REVOKE specific permissions.|ALL Permission|35|  
|Security|PERMISSIONS intrinsic function|Query sys.fn_my_permissions instead.|PERMISSIONS|170|  
|Security|SETUSER|EXECUTE AS|SETUSER|165|  
|Security|RC4 and DESX encryption algorithms|Use another algorithm such as AES.|DESX algorithm|238|  



### XML

| Deprecated feature | Replacement | Feature name |
|--------------------|-------------|--------------|
| Inline XDR Schema Generation | The XMLDATA directive to the FOR XML option is deprecated. Use XSD generation in the case of RAW and AUTO modes. There is no replacement for the XMLDATA directive in EXPLICT mode. | XMLDATA |

> [!NOTE]
> The cookie **OUTPUT** parameter for **sp_setapprole** is currently documented as **varbinary(8000)** which is the correct maximum length. However the current implementation returns **varbinary(50)**. If developers have allocated **varbinary(50)** the application might require changes if the cookie return size increases in a future release. Though not a deprecation issue this is mentioned in this topic because the application adjustments are similar. For more information, see [sp_setapprole &#40;Transact-SQL&#41;](../relational-databases/system-stored-procedures/sp-setapprole-transact-sql.md).  

## See Also

 [Discontinued Database Engine Functionality in SQL Server 2016](../database-engine/discontinued-database-engine-functionality-in-sql-server-2016.md)