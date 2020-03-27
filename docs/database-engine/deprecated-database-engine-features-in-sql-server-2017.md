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
| RESTORE { DATABASE &#124; LOG } WITH [MEDIA]PASSWORD continues to be deprecated. BACKUP { DATABASE &#124; LOG } WITH PASSWORD and BACKUP { DATABASE &#124; LOG } WITH MEDIAPASSWORD are discontinued. | None. | BACKUP DATABASE or LOG WITH PASSWORD <br /><br /> BACKUP DATABASE or LOG WITH MEDIAPASSWORD |

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
| Remote servers | sp_addremotelogin <br /><br /> sp_addserver <br /><br /> sp_dropremotelogin <br /><br /> sp_helpremotelogin <br /><br /> sp_remoteoption|Replace remote servers by using linked servers. sp_addserver can only be used with the local option.|sp_addremotelogin <br /><br /> sp_addserver <br /><br /> sp_dropremotelogin <br /><br /> sp_helpremotelogin <br /><br /> sp_remoteoption |
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
| Database compatibility level 110 and 120. | Plan to upgrade the database and application for a future release. However, we will continue to support applications certified on any supported database compatibility level as long as possible, to make the upgrades easier. For more information about compatibility levels, see [ALTER DATABASE Compatibility Level &#40;Transact-SQL&#41;](../t-sql/statements/alter-database-transact-sql-compatibility-level.md). | Database compatibility level 110 <br /><br /> Database compatibility level 120 |

### XML

| Deprecated feature | Replacement | Feature name |
|--------------------|-------------|--------------|
| Inline XDR Schema Generation | The XMLDATA directive to the FOR XML option is deprecated. Use XSD generation in the case of RAW and AUTO modes. There is no replacement for the XMLDATA directive in EXPLICT mode. | XMLDATA |

### Backup and restore

| Deprecated feature | Replacement | Feature name |
|--------------------|-------------|--------------|
| BACKUP { DATABASE &#124; LOG } TO TAPE <br /><br /> BACKUP { DATABASE &#124; LOG } TO *device_that_is_a_tape*|BACKUP { DATABASE &#124; LOG } TO DISK <br /><br /> BACKUP { DATABASE &#124; LOG } TO *device_that_is_a_disk* | BACKUP DATABASE or LOG TO TAPE |
| sp_addumpdevice '**tape**' | sp_addumpdevice '**disk**' | ADDING TAPE DEVICE |
| sp_helpdevice|sys.backup_devices | sp_helpdevice |

### Collations

| Deprecated feature | Replacement | Feature name |
|--------------------|-------------|--------------|
| Korean_Wansung_Unicode <br /><br /> Lithuanian_Classic <br /><br /> SQL_AltDiction_CP1253_CS_AS | None. These collations exist in  SQL Server 2005 (9.x), but are not visible through fn_helpcollations. | Korean_Wansung_Unicode <br /><br /> Lithuanian_Classic <br /><br /> SQL_AltDiction_CP1253_CS_AS |
| Hindi <br /><br /> Macedonian | These collations exist in  SQL Server 2005 (9.x) and higher, but are not visible through fn_helpcollations. Use Macedonian_FYROM_90 and Indic_General_90 instead.|Hindi <br /><br /> Macedonian |
| Azeri_Latin_90 <br /><br /> Azeri_Cyrilllic_90 | Azeri_Latin_100 <br /><br /> Azeri_Cyrilllic_100 | Azeri_Latin_90 <br /><br /> Azeri_Cyrilllic_90 |

### Configuration

| Deprecated feature | Replacement | Feature name |
|--------------------|-------------|--------------|
| SET ANSI_NULLS OFF and ANSI_NULLS OFF database option <br /><br /> SET ANSI_PADDING OFF and ANSI_PADDING OFF database option <br /><br /> SET CONCAT_NULL_YIELDS_NULL OFF and CONCAT_NULL_YIELDS_NULL OFF database option<br /><br /> SET OFFSETS | None. <br /><br /> ANSI_NULLS, ANSI_PADDING and CONCAT_NULLS_YIELDS_NULL will always be set to ON. SET OFFSETS will be unavailable. | SET ANSI_NULLS OFF <br /><br /> SET ANSI_PADDING OFF <br /><br /> SET CONCAT_NULL_YIELDS_NULL OFF <br /><br /> SET OFFSETS <br /><br /> ALTER DATABASE SET ANSI_NULLS OFF <br /><br /> ALTER DATABASE SET ANSI_PADDING OFF <br /><br /> ALTER DATABASE SET CONCAT_NULL_YIELDS_NULL OFF |

> [!NOTE]
> The cookie **OUTPUT** parameter for **sp_setapprole** is currently documented as **varbinary(8000)** which is the correct maximum length. However the current implementation returns **varbinary(50)**. If developers have allocated **varbinary(50)** the application might require changes if the cookie return size increases in a future release. Though not a deprecation issue this is mentioned in this topic because the application adjustments are similar. For more information, see [sp_setapprole &#40;Transact-SQL&#41;](../relational-databases/system-stored-procedures/sp-setapprole-transact-sql.md).  

## See Also

 [Discontinued Database Engine Functionality in SQL Server 2016](../database-engine/discontinued-database-engine-functionality-in-sql-server-2016.md)