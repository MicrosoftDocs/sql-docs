---
title: "sys.databases (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 11affb07-a9b1-4cf9-9c63-607b3b613b3f
caps.latest.revision: 20
author: BarbKess
---
# sys.databases (SQL Server PDW)
The sys.databases view is aligned with the corresponding view exposed by SQL Server. SQL Server PDW exposes logical databases rather than the actual physical databases on the various Compute node instances. Because some features are not supported, some columns have fixed return values.  
  
|Column Name|Data Type|Description|Range|  
|---------------|-------------|---------------|---------|  
|name|**sysname**|Name of database, unique within an instance of SQL Server.||  
|database_id|**int**|ID of the database, unique within an instance of SQL Server.||  
|source_database_id|**int**|NULL = Not a database snapshot.|Always NULL.|  
|owner_sid|**varbinary(85)**|SID (SecurityIdentifier) of the external owner of the database, as registered to the server.||  
|create_date|**datetime**|Date the database was created or renamed. For tempdb, this value changes every time the server restarts.||  
|compatibility_level|**tinyint**|Integer corresponding to the version of SQL Server for which behavior is compatible.|Always returns 100.|  
|collation_name|**sysname**|Collation for the database. Acts as the default collation in the database.|Always Latin1_General_100_CI_AS_KS_WS.|  
|user_access|**tinyint**|User access setting:<br /><br />0 = MULTI_USER specified|Always 0.|  
|user_access_desc|**nvarchar(60)**|Description of user-access setting: MULTI_USER|Always ‘MULTI_USER’.|  
|is_read_only|**bit**|0 = Database is READ_WRITE.|Always 0.|  
|is_auto_close_on|**bit**|0 = AUTO_CLOSE is OFF.|Always 0.|  
|is_auto_shrink_on|**bit**|0 = AUTO_SHRINK is OFF.|Always 0.|  
|state|**tinyint**|Database state:<br /><br />0 = ONLINE|Always 0.|  
|state_desc|**nvarchar(60)**|Description of the database state: ONLINE|‘ONLINE’|  
|is_in_standby|**bit**|Database is read-only for restore log.|Always 0.|  
|is_cleanly_shutdown|**bit**|0 = Database did not shut down cleanly; recovery is required on startup.|Always 0.|  
|is_supplemental_logging_enabled|**bit**|0 = SUPPLEMENTAL_LOGGING is OFF.|Always 0.|  
|snapshot_isolation_state|**tinyint**|State of snapshot-isolation transactions being allowed,0 = Snapshot isolation state is OFF (default). Snapshot isolation is disallowed.|Always 0.|  
|snapshot_isolation_state_desc|**nvarchar(60)**|Description of state of snapshot-isolation transactions being allowed|‘OFF’|  
|is_read_committed_snapshot_on|**bit**|0 = option is OFF.|Always 0.|  
|recovery_model|**tinyint**|Recovery model selected:<br /><br />1 = FULL<br /><br />3 = SIMPLE||  
|recovery_model_desc|**nvarchar(60)**|Description of recovery model selected.|‘FULL’ or ‘SIMPLE’|  
|page_verify_option|**tinyint**|Setting of PAGE_VERIFY option:<br /><br />2 = CHECKSUM|Always 2.|  
|page_verify_option_desc|**nvarchar(60)**|Description of PAGE_VERIFY option setting|‘CHECKSUM’|  
|is_auto_create_stats_on|**bit**|1 = AUTO_CREATE_STATISTICS is ON.<br /><br />This is the setting on the Compute nodes, and does not indicate the setting for the Control node.|Always 1.|  
|is_auto_update_stats_on|**bit**|1 = AUTO_UPDATE_STATISTICS is ON.<br /><br />This is the setting on the Compute nodes, and does not indicate the setting for the Control node.|Always 1.|  
|is_auto_update_stats_async_on|**bit**|0 = AUTO_UPDATE_STATISTICS_ASYNC is OFF.<br /><br />This is the setting on the Compute nodes, and does not indicate the setting for the Control node.|Always 0.|  
|is_ansi_null_default_on|**bit**|0 = ANSI_NULL_DEFAULT is OFF.|Always 0.|  
|is_ansi_nulls_on|**bit**|0 = ANSI_NULLS is OFF.|Always 0.|  
|is_ansi_padding_on|**bit**|0 = ANSI_PADDING is OFF.|Always 0.|  
|is_ansi_warnings_on|**bit**|0 = ANSI_WARNINGS is OFF.|Always 0.|  
|is_arithabort_on|**bit**|0 = ARITHABORT is OFF.|Always 0.|  
|is_concat_null_yields_null_on|**bit**|0 = CONCAT_NULL_YIELDS_NULL is OFF.|Always 0.|  
|is_numeric_roundabort_on|**bit**|0 = NUMERIC_ROUNDABORT is OFF.|Always 0.|  
|is_quoted_identifier_on|**bit**|0 = QUOTED_IDENTIFIER is OFF.|Always 0.|  
|is_recursive_triggers_on|**bit**|0 = RECURSIVE_TRIGGERS is OFF.|Always 0.|  
|is_cursor_close_on_commit_on|**bit**|0 = CURSOR_CLOSE_ON_COMMIT is OFF.|Always 0.|  
|is_local_cursor_default|**bit**|0 = CURSOR_DEFAULT is global.|Always 0.|  
|is_fulltext_enabled|**bit**|0 = Full-text is disabled for the database.|Always 0.|  
|is_trustworthy_on|**bit**|0 = Database has not been marked trustworthy.|Always 0.|  
|is_db_chaining_on|**bit**|0 = Cross-database ownership chaining is OFF.|Always 0.|  
|is_parameterization_forced|**bit**|0 = Parameterization is SIMPLE.|Always 0.|  
|is_master_key_encrypted_by_server|**bit**|Indicates if the database has an encrypted master key. An encrypted master key is one requirement for TDE.|0 = The database does not have an encrypted master key.<br /><br />1 = The database has an encrypted master key.|  
|is_published|**bit**|0 = Is not a publication database.|Always 0.|  
|is_subscribed|**bit**|0 = Is not a subscription database.|Always 0.|  
|is_merge_published|**bit**|0 = Is not a publication database in a merge replication topology.|Always 0.|  
|is_distributor|**bit**|0 = Is not the distribution database for a replication topology.|Always 0.|  
|is_sync_with_backup|**bit**|0 = Is not marked for replication synchronization with backup.|Always 0.|  
|service_broker_guid|**uniqueidentifier**|Identifier of the service broker for this database. Used as the broker_instance of the target in the routing table.|NULL|  
|is_broker_enabled|**bit**|0 = Broker disabled.|Always 0.|  
|log_reuse_wait|**tinyint**|Reuse of transaction log space is currently waiting on one of the following: 0 = Nothing|Always 0.|  
|log_reuse_wait_desc|**nvarchar(60)**|Description of reuse of transaction log space.|Always return ‘NOTHING’.|  
|is_date_correlation_on|**bit**|0 = OFF|Always 0.|  
|is_cdc_enabled|**bit**|1 = Database is enabled for change data capture.|Always 0.|  
|is_encrypted|**bit**|Indicates whether the database is encrypted by using TDE.|0 = Not Encrypted<br /><br />1 = Encrypted|  
|is_honor_broker_priority_on|**bit**|Indicates whether the database honors conversation priorities<br /><br />0 = OFF|Always 0.|  
|**replica_id**|**uniqueidentifier**|Unique identifier of the local AlwaysOn Availability Groups availability replica of the availability group, if any, in which the database is participating.<br /><br />NULL = database is not part of an availability replica of in availability group.|Always NULL.|  
|**group_database_id**|**uniqueidentifier**|Unique identifier of the database within an AlwaysOn availability group, if any, in which the database is participating. **group_database_id** is the same for this database on the primary replica and on every secondary replica on which the database has been joined to the availability group.<br /><br />NULL = database is not part of an availability replica in any availability group.|Always NULL.|  
|**default_language_lcid**|**smallint**|Indicates the local id (lcid) of the default language of a contained database.<br /><br />**Note** Functions as the Configure the default language Server Configuration Option of **sp_configure**. This value is **null** for a non-contained database.|Always NULL.|  
|**default_language_name**|**nvarchar(128)**|Indicates the default language of a contained database.<br /><br />This value is **null** for a non-contained database.|Always NULL.|  
|**default_fulltext_language_lcid**|**int**|Indicates the local id (lcid) of the default fulltext language of the contained database.<br /><br />**Note** Functions as the default Configure the default full-text language Server Configuration Option of **sp_configure**. This value is **null** for a non-contained database.|Always NULL.|  
|**default_fulltext_language_name**|**nvarchar(128)**|Indicates the default fulltext language of the contained database.<br /><br />This value is **null** for a non-contained database.|Always NULL.|  
|**is_nested_triggers_on**|**bit**|Indicates whether or not nested triggers are allowed in the contained database.<br /><br />0 = nested triggers are not allowed<br /><br />1 = nested triggers are allowed<br /><br />**Note** Functions as the Configure the nested triggers Server Configuration Option of **sp_configure**. This value is **null** for a non-contained database. See sys.configurations for further information.|Always NULL.|  
|**is_transform_noise_words_on**|**bit**|Indicates whether or noise words should be transformed in the contained database.<br /><br />0 = noise words should not be transformed.<br /><br />1 = noise words should be transformed.<br /><br />**Note** Functions as the transform noise words of **sp_configure**. This value is **null** for a non-contained database. See sys.configurations for further information.|Always NULL.|  
|**two_digit_year_cutoff**|**smallint**|Indicates a value of a number between 1753 and 9999 to represent the cutoff year for interpreting two-digit years as four-digit years.<br /><br />**Note** Functions as the Configure the two digit year cutoff Server Configuration Option of **sp_configure**. This value is **null** for a non-contained database. See sys.configurations for further information.|Always NULL.|  
|**containment**|**tinyint not null**|Indicates the containment status of the database.|Always 0.|  
|**containment_desc**|**nvarchar(60) not null**|Indicates the containment status of the database.<br /><br />NONE = legacy database (zero containment)<br /><br />PARTIAL = partially contained database|Always NONE.|  
|**target_recovery_time_in_seconds**|**int**|The estimated time to recover the database, in seconds. Nullable.|Always 0.|  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[System Views &#40;SQL Server PDW&#41;](../sqlpdw/system-views-sql-server-pdw.md)  
  
