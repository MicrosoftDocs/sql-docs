---
description: "sys.databases (Transact-SQL)"
title: "sys.databases (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/08/2020"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "databases"
  - "databases_TSQL"
  - "sys.databases"
  - "sys.databases_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.databases catalog view"
ms.assetid: 46c288c1-3410-4d68-a027-3bbf33239289
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.databases (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

Contains one row per database in the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
If a database is not `ONLINE`, or `AUTO_CLOSE` is set to `ON` and the database is closed, the values of some columns may be `NULL`. If a database is `OFFLINE`, the corresponding row is not visible to low-privileged users. To see the corresponding row if the database is `OFFLINE`, a user must have at least the `ALTER ANY DATABASE` server-level permission, or the `CREATE DATABASE` permission in the `master` database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**name**|**sysname**|Name of database, unique within an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or within a [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] server.|  
|**database_id**|**int**|ID of the database, unique within an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or within a [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] server.|  
|**source_database_id**|**int**|Non-NULL = ID of the source database of this database snapshot.<br /> NULL = Not a database snapshot.|  
|**owner_sid**|**varbinary(85)**|SID (Security-Identifier) of the external owner of the database, as registered to the server. For information about who can own a database, see the **ALTER AUTHORIZATION for databases** section of [ALTER AUTHORIZATION](../../t-sql/statements/alter-authorization-transact-sql.md).|  
|**create_date**|**datetime**|Date the database was created or renamed. For **tempdb**, this value changes every time the server restarts.|  
|**compatibility_level**|**tinyint**|Integer corresponding to the version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] for which behavior is compatible:<br /><br /><table border="0"><tr><td>**Value**</td><td>**Applies to**</td></tr><tr><td>70</td><td>[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 7.0 through [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)]</td></tr><tr><td>80</td><td>[!INCLUDE[ssVersion2000](../../includes/ssversion2000-md.md)] through [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)]</td></tr><tr><td>90</td><td>[!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)]</td></tr><tr><td>100</td><td>[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)]) and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]</td></tr><tr><td>110</td><td>[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)]) and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]</td></tr><tr><td>120</td><td>[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]) and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]</td></tr><tr><td>130</td><td>[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)]) and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]</td></tr><tr><td>140</td><td>[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)]) and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]</td></tr><tr><td>150</td><td>[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[sql-server-2019](../../includes/sssqlv15-md.md)]) and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]</td></tr></table>|  
|**collation_name**|**sysname**|Collation for the database. Acts as the default collation in the database.<br /> NULL = Database is not online or AUTO_CLOSE is set to ON and the database is closed.|  
|**user_access**|**tinyint**|User-access setting:<br /> 0 = MULTI_USER specified<br /> 1 = SINGLE_USER specified<br /> 2 = RESTRICTED_USER specified|  
|**user_access_desc**|**nvarchar(60)**|Description of user-access setting.|  
|**is_read_only**|**bit**|1 = Database is READ_ONLY<br /> 0 = Database is READ_WRITE|  
|**is_auto_close_on**|**bit**|1 = AUTO_CLOSE is ON<br /> 0 = AUTO_CLOSE is OFF|  
|**is_auto_shrink_on**|**bit**|1 = AUTO_SHRINK is ON<br /> 0 = AUTO_SHRINK is OFF|  
|**state**|**tinyint**|**Value**<br /> 0 = ONLINE <br /> 1 = RESTORING <br /> 2 = RECOVERING <sup>1</sup><br /> 3 = RECOVERY_PENDING <sup>1</sup><br /> 4 = SUSPECT <br /> 5 = EMERGENCY <sup>1</sup><br /> 6 = OFFLINE <sup>1</sup><br /> 7 = COPYING <sup>2</sup> <br /> 10 = OFFLINE_SECONDARY <sup>2</sup> <br /><br /> **Note:** For Always On databases, query the `database_state` or `database_state_desc` columns of [sys.dm_hadr_database_replica_states](../../relational-databases/system-dynamic-management-views/sys-dm-hadr-database-replica-states-transact-sql.md).<br /><br /><sup>1</sup> **Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (starting with [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)]) and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]<br /><sup>2</sup> **Applies to**: [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] [!INCLUDE[ssGeoDR](../../includes/ssgeodr-md.md)]|  
|**state_desc**|**nvarchar(60)**|Description of the database state. See state.|  
|**is_in_standby**|**bit**|Database is read-only for restore log.|  
|**is_cleanly_shutdown**|**bit**|1 = Database shut down cleanly; no recovery required on startup<br /> 0 = Database did not shut down cleanly; recovery is required on startup|  
|**is_supplemental_logging_enabled**|**bit**|1 = SUPPLEMENTAL_LOGGING is ON<br /> 0 = SUPPLEMENTAL_LOGGING is OFF|  
|**snapshot_isolation_state**|**tinyint**|State of snapshot-isolation transactions being allowed, as set by the ALLOW_SNAPSHOT_ISOLATION option:<br /> 0 = Snapshot isolation state is OFF (default). Snapshot isolation is disallowed.<br /> 1 = Snapshot isolation state ON. Snapshot isolation is allowed.<br /> 2 = Snapshot isolation state is in transition to OFF state. All transactions have their modifications versioned. Cannot start new transactions using snapshot isolation. The database remains in the transition to OFF state until all transactions that were active when ALTER DATABASE was run can be completed.<br /> 3 = Snapshot isolation state is in transition to ON state. New transactions have their modifications versioned. Transactions cannot use snapshot isolation until the snapshot isolation state becomes 1 (ON). The database remains in the transition to ON state until all update transactions that were active when ALTER DATABASE was run can be completed.|  
|**snapshot_isolation_state_desc**|**nvarchar(60)**|Description of state of snapshot-isolation transactions being allowed, as set by the ALLOW_SNAPSHOT_ISOLATION option.|  
|**is_read_committed_snapshot_on**|**bit**|1 = READ_COMMITTED_SNAPSHOT option is ON. Read operations under the read-committed isolation level are based on snapshot scans and do not acquire locks.<br /> 0 = READ_COMMITTED_SNAPSHOT option is OFF (default). Read operations under the read-committed isolation level use share locks.|  
|**recovery_model**|**tinyint**|Recovery model selected:<br /> 1 = FULL<br /> 2 = BULK_LOGGED<br /> 3 = SIMPLE|  
|**recovery_model_desc**|**nvarchar(60)**|Description of recovery model selected.|  
|**page_verify_option**|**tinyint**|Setting of PAGE_VERIFY option:<br /> 0 = NONE<br /> 1 = TORN_PAGE_DETECTION<br /> 2 = CHECKSUM|  
|**page_verify_option_desc**|**nvarchar(60)**|Description of PAGE_VERIFY option setting.|  
|**is_auto_create_stats_on**|**bit**|1 = AUTO_CREATE_STATISTICS is ON<br /> 0 = AUTO_CREATE_STATISTICS is OFF|  
|**is_auto_create_stats_incremental_on**|**bit**|Indicates the default setting for the incremental option of auto stats.<br /> 0 = auto create stats are non-incremental<br /> 1 = auto create stats are incremental if possible<br /> **Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (starting with [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]).|  
|**is_auto_update_stats_on**|**bit**|1 = AUTO_UPDATE_STATISTICS is ON<br /> 0 = AUTO_UPDATE_STATISTICS is OFF|  
|**is_auto_update_stats_async_on**|**bit**|1 = AUTO_UPDATE_STATISTICS_ASYNC is ON<br /> 0 = AUTO_UPDATE_STATISTICS_ASYNC is OFF|  
|**is_ansi_null_default_on**|**bit**|1 = ANSI_NULL_DEFAULT is ON<br /> 0 = ANSI_NULL_DEFAULT is OFF|  
|**is_ansi_nulls_on**|**bit**|1 = ANSI_NULLS is ON<br /> 0 = ANSI_NULLS is OFF|  
|**is_ansi_padding_on**|**bit**|1 = ANSI_PADDING is ON<br /> 0 = ANSI_PADDING is OFF|  
|**is_ansi_warnings_on**|**bit**|1 = ANSI_WARNINGS is ON<br /> 0 = ANSI_WARNINGS is OFF|  
|**is_arithabort_on**|**bit**|1 = ARITHABORT is ON<br /> 0 = ARITHABORT is OFF|  
|**is_concat_null_yields_null_on**|**bit**|1 = CONCAT_NULL_YIELDS_NULL is ON<br /> 0 = CONCAT_NULL_YIELDS_NULL is OFF|  
|**is_numeric_roundabort_on**|**bit**|1 = NUMERIC_ROUNDABORT is ON<br /> 0 = NUMERIC_ROUNDABORT is OFF|  
|**is_quoted_identifier_on**|**bit**|1 = QUOTED_IDENTIFIER is ON<br /> 0 = QUOTED_IDENTIFIER is OFF|  
|**is_recursive_triggers_on**|**bit**|1 = RECURSIVE_TRIGGERS is ON<br /> 0 = RECURSIVE_TRIGGERS is OFF|  
|**is_cursor_close_on_commit_on**|**bit**|1 = CURSOR_CLOSE_ON_COMMIT is ON<br /> 0 = CURSOR_CLOSE_ON_COMMIT is OFF|  
|**is_local_cursor_default**|**bit**|1 = CURSOR_DEFAULT is local<br /> 0 = CURSOR_DEFAULT is global|  
|**is_fulltext_enabled**|**bit**|1 = Full-text is enabled for the database<br /> 0 = Full-text is disabled for the database|  
|**is_trustworthy_on**|**bit**|1 = Database has been marked trustworthy<br /> 0 = Database has not been marked trustworthy<br /> By default, restored or attached databases have the trustworthy not enabled.|  
|**is_db_chaining_on**|**bit**|1 = Cross-database ownership chaining is ON<br /> 0 = Cross-database ownership chaining is OFF|  
|**is_parameterization_forced**|**bit**|1 = Parameterization is FORCED<br /> 0 = Parameterization is SIMPLE|  
|**is_master_key_encrypted_by_server**|**bit**|1 = Database has an encrypted master key<br /> 0 = Database does not have an encrypted master key|  
|**is_query_store_on**|**bit**|1 = The query store is enable for this database. Check [sys.database_query_store_options](../../relational-databases/system-catalog-views/sys-database-query-store-options-transact-sql.md) to view the query store status.<br /> 0 = The query store is not enabled<br /> **Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (starting with [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)]).|  
|**is_published**|**bit**|1 = Database is a publication database in a transactional or snapshot replication topology<br /> 0 = Is not a publication database|  
|**is_subscribed**|**bit**|This column is not used. It will always return 0, regardless of the subscriber status of the database.|  
|**is_merge_published**|**bit**|1 = Database is a publication database in a merge replication topology<br /> 0 = Is not a publication database in a merge replication topology|  
|**is_distributor**|**bit**|1 = Database is the distribution database for a replication topology<br /> 0 = Is not the distribution database for a replication topology|  
|**is_sync_with_backup**|**bit**|1 = Database is marked for replication synchronization with backup<br /> 0 = Is not marked for replication synchronization with backup|  
|**service_broker_guid**|**uniqueidentifier**|Identifier of the service broker for this database. Used as the **broker_instance** of the target in the routing table.|  
|**is_broker_enabled**|**bit**|1 = The broker in this database is currently sending and receiving messages.<br /> 0 = All sent messages will stay on the transmission queue and received messages will not be put on queues in this database.<br /> By default, restored or attached databases have the broker disabled. The exception to this is database mirroring where the broker is enabled after failover.|  
|**log_reuse_wait**|**tinyint**|Reuse of transaction log space is currently waiting on one of the following as of the last checkpoint. For more detailed explanations of these values, see [The Transaction Log](../../relational-databases/logs/the-transaction-log-sql-server.md).<br /> **Value**<br /> 0 = Nothing<br /> 1 = Checkpoint (When a database uses a recovery model and has a memory-optimized data filegroup, you should expect to see the `log_reuse_wait` column indicate `checkpoint` or `xtp_checkpoint`) <sup>1</sup><br /> 2 = Log Backup <sup>1</sup><br /> 3 = Active backup or restore <sup>1</sup><br /> 4 = Active transaction <sup>1</sup><br /> 5 = Database mirroring <sup>1</sup><br /> 6 = Replication <sup>1</sup><br /> 7 = Database snapshot creation <sup>1</sup><br /> 8 = Log scan <br /> 9 = An Always On Availability Groups secondary replica is applying transaction log records of this database to a corresponding secondary database. <sup>2</sup><br /> 9 = Other (Transient) <sup>3</sup><br /> 10 = For internal use only <sup>2</sup><br /> 11 = For internal use only <sup>2</sup><br /> 12 = For internal use only <sup>2</sup><br /> 13 = Oldest page <sup>2</sup><br /> 14 = Other <sup>2</sup><br />  16 = XTP_CHECKPOINT (When a database uses a recovery model and has a memory-optimized data filegroup, you should expect to see the `log_reuse_wait` column indicate `checkpoint` or `xtp_checkpoint`) <sup>4</sup><br /><br /><sup>1</sup> **Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (starting with [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)])<br /><sup>2</sup> **Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (starting with [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)])<br /><sup>3</sup> **Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (up to, and including [!INCLUDE[ssKilimanjaro](../../includes/ssKilimanjaro-md.md)])<br /><sup>4</sup> **Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (starting with [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)])|  
|**log_reuse_wait_desc**|**nvarchar(60)**|Description of reuse of transaction log space is currently waiting on as of the last checkpoint.|  
|**is_date_correlation_on**|**bit**|1 = DATE_CORRELATION_OPTIMIZATION is ON<br /> 0 = DATE_CORRELATION_OPTIMIZATION is OFF|  
|**is_cdc_enabled**|**bit**|1 = Database is enabled for change data capture. For more information, see [sys.sp_cdc_enable_db &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sys-sp-cdc-enable-db-transact-sql.md).|  
|**is_encrypted**|**bit**|Indicates whether the database is encrypted (reflects the state last set by using the `ALTER DATABASE SET ENCRYPTION` clause). Can be one of the following values:<br /> 1 = Encrypted<br /> 0 = Not Encrypted<br /> For more information about database encryption, see [Transparent Data Encryption &#40;TDE&#41;](../../relational-databases/security/encryption/transparent-data-encryption.md).<br /> If the database is in the process of being decrypted, `is_encrypted` shows a value of 0. You can see the state of the encryption process by using the [sys.dm_database_encryption_keys](../../relational-databases/system-dynamic-management-views/sys-dm-database-encryption-keys-transact-sql.md) dynamic management view.|  
|**is_honor_broker_priority_on**|**bit**|Indicates whether the database honors conversation priorities (reflects the state last set by using the `ALTER DATABASE SET HONOR_BROKER_PRIORITY` clause). Can be one of the following values:<br /> 1 = HONOR_BROKER_PRIORITY is ON<br /> 0 = HONOR_BROKER_PRIORITY is OFF<br /> By default, restored or attached databases have the broker priority off.|  
|**replica_id**|**uniqueidentifier**|Unique identifier of the local [!INCLUDE[ssHADR](../../includes/sshadr-md.md)] availability replica of the availability group, if any, in which the database is participating.<br /> NULL = database is not part of an availability replica of in availability group.<br /> **Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ([!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later) and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]|  
|**group_database_id**|**uniqueidentifier**|Unique identifier of the database within an Always On availability group, if any, in which the database is participating. **group_database_id** is the same for this database on the primary replica and on every secondary replica on which the database has been joined to the availability group.<br /> NULL = database is not part of an availability replica in any availability group.<br /> **Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (starting with [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)]) and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]|  
|**resource_pool_id**|**int**|The id of the resource pool that is mapped to this database. This resource pool controls total memory available to memory-optimized tables in this database.<br /> **Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (starting with [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)])|  
|**default_language_lcid**|**smallint**|Indicates the local id (lcid) of the default language of a contained database.<br /> **Note:** Functions as the [Configure the default language Server Configuration Option](../../database-engine/configure-windows/configure-the-default-language-server-configuration-option.md) of `sp_configure`. This value is **null** for a non-contained database.<br /> **Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (starting with [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)]) and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]|  
|**default_language_name**|**nvarchar(128)**|Indicates the default language of a contained database.<br /> This value is **null** for a non-contained database.<br /> **Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ([!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later) and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]|  
|**default_fulltext_language_lcid**|**int**|Indicates the locale id (lcid) of the default fulltext language of the contained database.<br /> **Note:** Functions as the default [Configure the default full-text language Server Configuration Option](../../database-engine/configure-windows/configure-the-default-full-text-language-server-configuration-option.md) of `sp_configure`. This value is **null** for a non-contained database.<br /> **Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (starting with [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)]) and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]|  
|**default_fulltext_language_name**|**nvarchar(128)**|Indicates the default fulltext language of the contained database.<br /> This value is **null** for a non-contained database.<br /> **Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (starting with [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)]) and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]|  
|**is_nested_triggers_on**|**bit**|Indicates whether or not nested triggers are allowed in the contained database.<br /> 0 = nested triggers are not allowed<br /> 1 = nested triggers are allowed<br /> **Note:** Functions as the [Configure the nested triggers Server Configuration Option](../../database-engine/configure-windows/configure-the-nested-triggers-server-configuration-option.md) of `sp_configure`. This value is **null** for a non-contained database. See [sys.configurations &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-configurations-transact-sql.md) for further information.<br /> **Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (starting with [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)]) and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]|  
|**is_transform_noise_words_on**|**bit**|Indicates whether or noise words should be transformed in the contained database.<br /> 0 = noise words should not be transformed.<br /> 1 = noise words should be transformed.<br /> **Note:** Functions as the [transform noise words Server Configuration Option](../../database-engine/configure-windows/transform-noise-words-server-configuration-option.md) of `sp_configure`. This value is **null** for a non-contained database. See [sys.configurations &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-configurations-transact-sql.md) for further information.<br /> **Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (starting with [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)])|  
|**two_digit_year_cutoff**|**smallint**|Indicates a value of a number between 1753 and 9999 to represent the cutoff year for interpreting two-digit years as four-digit years.<br /> **Note:** Functions as the [Configure the two digit year cutoff Server Configuration Option](../../database-engine/configure-windows/configure-the-two-digit-year-cutoff-server-configuration-option.md) of `sp_configure`. This value is **null** for a non-contained database. See [sys.configurations &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-configurations-transact-sql.md) for further information.<br /> **Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (starting with [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)]) and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]|  
|**containment**|**tinyint not null**|Indicates the containment status of the database.<br />  0 = database containment is off. **Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (starting with [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)]) and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]<br /> 1 = database is in partial containment **Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (starting with [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)])|  
|**containment_desc**|**nvarchar(60) not null**|Indicates the containment status of the database.<br /> NONE = legacy database (zero containment)<br /> PARTIAL = partially contained database<br /> **Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ([!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later) and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]|  
|**target_recovery_time_in_seconds**|**int**|The estimated time to recover the database, in seconds. Nullable.<br /> **Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (starting with [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)]) and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]|  
|**delayed_durability**|**int**|The delayed durability setting:<br /> 0 = DISABLED<br /> 1 = ALLOWED<br /> 2 = FORCED<br /> For more information, see [Control Transaction Durability](../../relational-databases/logs/control-transaction-durability.md).<br /> **Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (starting with [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]) and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].|  
|**delayed_durability_desc**|**nvarchar(60)**|The delayed durability setting:<br /> DISABLED<br /> ALLOWED<br /> FORCED<br /> **Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (starting with [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]) and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].|  
|**is_memory_optimized_elevate_to_snapshot_on**|**bit**|Memory-optimized tables are accessed using SNAPSHOT isolation when the session setting TRANSACTION ISOLATION LEVEL is set to a lower isolation level, READ COMMITTED or READ UNCOMMITTED.<br /> 1 = Minimum isolation level is SNAPSHOT.<br /> 0 = Isolation level is not elevated.|  
|**is_federation_member**|**bit**|Indicates if the database is a member of a federation.<br /> **Applies to**: [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]|  
|**is_remote_data_archive_enabled**|**bit**|Indicates whether the database is stretched.<br /> 0 = The database is not Stretch-enabled.<br /> 1 = The database is Stretch-enabled.<br /> **Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (starting with [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)])<br /> For more information, see [Stretch Database](../../sql-server/stretch-database/stretch-database.md).|  
|**is_mixed_page_allocation_on**|**bit**|Indicates whether tables and indexes in the database can allocate initial pages from mixed extents.<br /> 0 = Tables and indexes in the database always allocate  initial pages from uniform extents.<br /> 1 =  Tables and indexes in the database can allocate initial pages from mixed extents.<br /> For more information, see the `SET MIXED_PAGE_ALLOCATION` option of [ALTER DATABASE SET Options &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql-set-options.md).<br /> **Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (starting with [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)])|  
|**is_temporal_retention_enabled**|**bit**|Indicates whether temporal retention policy cleanup task is enabled.<br /><br />1 = temporal retention is enabled<br />0 = temporal retention is disabled<br />**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (starting with [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)]) and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]|
|**catalog_collation_type**|**int**|The catalog collation setting:<br />0 = DATABASE_DEFAULT<br />2 = SQL_Latin_1_General_CP1_CI_AS<br /> **Applies to**: [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]|
|**catalog_collation_type_desc**|**nvarchar(60)**|The catalog collation setting:<br />DATABASE_DEFAULT<br />SQL_Latin_1_General_CP1_CI_AS<br /> **Applies to**: [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]|
|**physical_database_name**|**nvarchar(128)**|For [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the physical name of the database. For [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], a common id for the databases on a server. <br />**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (starting with [!INCLUDE[sql-server-2019](../../includes/sssqlv15-md.md)]) and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]|
|**is_result_set_caching_on**|**bit**|Indicates whether result set caching is enabled.<br />1 = result set caching is enabled<br />0 = result set caching is disabled<br />**Applies to**: [!INCLUDE[ssSDW](../../includes/sssdw-md.md)] Gen2. While this feature is being rolled out to all regions, please check the version deployed to your instance and the latest [Azure Synapse release notes](/azure/synapse-analytics/sql-data-warehouse/release-notes-10-0-10106-0) and [Gen2 upgrade schedule](/azure/synapse-analytics/sql-data-warehouse/gen2-migration-schedule) for feature availability.|
|**is_accelerated_database_recovery_on**|**bit**|Indicates whether Accelerated Database Recovery (ADR) is enabled.<br />1 = ADR is enabled<br />0 = ADR is disabled<br />**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (starting with [!INCLUDE[sql-server-2019](../../includes/sssqlv15-md.md)]) and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]|
|**is_tempdb_spill_to_remote_store**|**bit**|Indicates whether tempdb spill to remote store is enabled.<br />1 = enabled<br />0 = disabled<br />**Applies to**: [!INCLUDE[ssSDW](../../includes/sssdw-md.md)] Gen2. While this feature is being rolled out to all regions, please check the version deployed to your instance and the latest [Azure Synapse release notes](/azure/synapse-analytics/sql-data-warehouse/release-notes-10-0-10106-0) and [Gen2 upgrade schedule](/azure/synapse-analytics/sql-data-warehouse/gen2-migration-schedule) for feature availability.|
|**is_stale_page_detection_on**|**bit**|Indicates whether stale page detection is enabled.<br />1 = stale page detection is enabled<br />0 = stale page detection is disabled<br />**Applies to**: [!INCLUDE[ssSDW](../../includes/sssdw-md.md)] Gen2. While this feature is being rolled out to all regions, please check the version deployed to your instance and the latest [Azure Synapse release notes](/azure/synapse-analytics/sql-data-warehouse/release-notes-10-0-10106-0) and [Gen2 upgrade schedule](/azure/synapse-analytics/sql-data-warehouse/gen2-migration-schedule) for feature availability.|
|**is_memory_optimized_enabled**|**bit**|Indicates whether certain In-Memory features, such as [Hybrid Buffer Pool](../../database-engine/configure-windows/hybrid-buffer-pool.md), are enabled for the database. Does not reflect the availability or configuration state of [In-Memory OLTP](../../relational-databases/in-memory-oltp/in-memory-oltp-in-memory-optimization.md). <br />1 = memory-optimized features are enabled<br />0 = memory-optimized features are disabled<br />**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (starting with [!INCLUDE[sql-server-2019](../../includes/sssqlv15-md.md)]) and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]|
  
## Permissions

 If the caller of `sys.databases` is not the owner of the database and the database is not `master` or `tempdb`, the minimum permissions required to see the corresponding row are `ALTER ANY DATABASE` or the `VIEW ANY DATABASE` server-level permission, or `CREATE DATABASE` permission in the `master` database. The database to which the caller is connected can always be viewed in `sys.databases`.  
  
> [!IMPORTANT]  
> By default, the public role has the `VIEW ANY DATABASE` permission, allowing all logins to see database information. To block a login from the ability to detect a database, `REVOKE` the `VIEW ANY DATABASE` permission from `public`, or `DENY` the `VIEW ANY DATABASE` permission for individual logins.  
  
## Azure SQL Database Remarks

In [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] this view is available in the `master` database and in user databases. In the `master` database, this view returns the information on the `master` database and all user databases on the server. In a user database, this view returns information only on the current database and the master database.  
  
 Use the `sys.databases` view in the `master` database of the [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] server where the new database is being created. After the database copy starts, you can query the `sys.databases` and the `sys.dm_database_copies` views from the `master` database of the destination server to retrieve more information about the copying progress.  
  
## Examples  
  
### A. Query the sys.databases view

The following example returns a few of the columns available in the `sys.databases` view.  
  
```sql  
SELECT name, user_access_desc, is_read_only, state_desc, recovery_model_desc  
FROM sys.databases;  
```  
  
### B. Check the copying status in [!INCLUDE[ssSDS](../../includes/sssds-md.md)]

The following example queries the `sys.databases` and `sys.dm_database_copies` views to return information about a database copy operation.  
  
**Applies to**: [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]  
  
```sql
-- Execute from the master database.  
SELECT a.name, a.state_desc, b.start_date, b.modify_date, b.percent_complete  
FROM sys.databases AS a  
INNER JOIN sys.dm_database_copies AS b ON a.database_id = b.database_id  
WHERE a.state = 7;  
```

### C. Check the temporal retention policy status in [!INCLUDE[ssSDS](../../includes/sssds-md.md)]

The following example queries the `sys.databases` to return information whether temporal retention cleanup task is enabled. Be aware that after restore operation temporal retention is disabled by default. Use `ALTER DATABASE` to enable it explicitly.
  
**Applies to**: [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]  
  
```sql  
-- Execute from the master database.  
SELECT a.name, a.is_temporal_history_retention_enabled 
FROM sys.databases AS a;
```  
  
## Next steps

- [ALTER DATABASE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql.md)
- [sys.database_mirroring_witnesses &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/database-mirroring-witness-catalog-views-sys-database-mirroring-witnesses.md)
- [sys.database_recovery_status &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-recovery-status-transact-sql.md)
- [Databases and Files Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/databases-and-files-catalog-views-transact-sql.md)
- [sys.dm_database_copies &#40;Azure SQL Database&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-database-copies-azure-sql-database.md)  
