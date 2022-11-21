---
description: "sp_changearticle (Transact-SQL)"
title: "sp_changearticle (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "10/28/2015"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: "reference"
dev_langs: 
  - "TSQL"
f1_keywords: 
  - "sp_changearticle"
  - "sp_changearticle_TSQL"
helpviewer_keywords: 
  - "sp_changearticle"
ms.assetid: 24c33ca5-f03a-4417-a267-131ca5ba6bb5
author: markingmyname
ms.author: maghan
---
# sp_changearticle (Transact-SQL)
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

  Changes the properties of an article in a transactional or snapshot publication. This stored procedure is executed at the Publisher on the publication database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_changearticle [ [@publication= ] 'publication' ]  
    [ , [ @article= ] 'article' ]  
    [ , [ @property= ] 'property' ]  
    [ , [ @value= ] 'value' ]  
    [ , [ @force_invalidate_snapshot = ] force_invalidate_snapshot ]  
    [ , [ @force_reinit_subscription = ] force_reinit_subscription ]  
    [ , [ @publisher = ] 'publisher' ]  
```  
  
## Arguments  
`[ @publication = ] 'publication'`
 Is the name of the publication that contains the article. *publication* is **sysname**, with a default of NULL.  
  
`[ @article = ] 'article'`
 Is the name of the article whose property is to be changed. *article* is **sysname**, with a default of NULL.  
  
`[ @property = ] 'property'`
 Is an article property to change. *property* is **nvarchar(100)**.  
  
`[ @value = ] 'value'`
 Is the new value of the article property. *value* is **nvarchar(255)**.  
  
 This table describes the properties of articles and the values for those properties.  
  
|Property|Values|Description|  
|--------------|------------|-----------------|  
|**creation_script**||Path and name of an article schema script used to create target tables. The default is NULL.|  
|**del_cmd**||DELETE statement to execute; otherwise, it is constructed from the log.|  
|**description**||New descriptive entry for the article.|  
|**dest_object**||Provided for backward compatibility. Use **dest_table**.|  
|**dest_table**||New destination table.|  
|**destination_owner**||Name of the owner of the destination object.|  
|**filter**||New stored procedure to be used to filter the table (horizontal filtering). The default is NULL. Cannot be changed for publications in peer-to-peer replication.|  
|**fire_triggers_on_snapshot**|**true**|Replicated user triggers are executed when the initial snapshot is applied.<br /><br /> Note: For triggers to be replicated, the bitmask value of *schema_option* must include the value **0x100**.|  
||**false**|Replicated user triggers are not executed when the initial snapshot is applied.|  
|**identity_range**||Controls the size of assigned identity ranges assigned at the Subscriber. Not supported for peer-to-peer replication.|  
|**ins_cmd**||INSERT statement to execute; otherwise, it is constructed from the log.|  
|**pre_creation_cmd**||Pre-creation command that can drop, delete, or truncate the destination table before synchronization is applied.|  
||**none**|Does not use a command.|  
||**drop**|Drops the destination table.|  
||**delete**|Deletes the destination table.|  
||**truncate**|Truncates the destination table.|  
|**pub_identity_range**||Controls the size of assigned identity ranges assigned at the Subscriber. Not supported for peer-to-peer replication.|  
|**schema_option**||Specifies the bitmap of the schema generation option for the given article. *schema_option* is **binary(8)**. For more information, see the Remarks section later in this topic.|  
||**0x00**|Disables scripting by the Snapshot Agent.|  
||**0x01**|Generates the object creation (CREATE TABLE, CREATE PROCEDURE, and so on).|  
||**0x02**|Generates the stored procedures that propagate changes for the article, if defined.|  
||**0x04**|Identity columns are scripted using the IDENTITY property.|  
||**0x08**|Replicate **timestamp** columns. If not set, **timestamp** columns are replicated as **binary**.|  
||**0x10**|Generates a corresponding clustered index.|  
||**0x20**|Converts user-defined data types (UDT) to base data types at the Subscriber. This option cannot be used when there is a CHECK or DEFAULT constraint on a UDT column, if a UDT column is part of the primary key, or if a computed column references a UDT column. Not supported for Oracle Publishers.|  
||**0x40**|Generates corresponding nonclustered indexes.|  
||**0x80**|Includes declared referential integrity on the primary keys.|  
||**0x100**|Replicates user triggers on a table article, if defined.|  
||**0x200**|Replicates FOREIGN KEY constraints. If the referenced table is not part of a publication, all FOREIGN KEY constraints on a published table are not replicated.|  
||**0x400**|Replicates CHECK constraints.|  
||**0x800**|Replicates defaults.|  
||**0x1000**|Replicates column-level collation.|  
||**0x2000**|Replicates extended properties associated with the published article source object.|  
||**0x4000**|Replicates unique keys if defined on a table article.|  
||**0x8000**|Replicates primary key and unique keys on a table article as constraints using ALTER TABLE statements.<br /><br /> Note: This option has been deprecated. Use **0x80** and **0x4000** instead.|  
||**0x10000**|Replicates CHECK constraints as NOT FOR REPLICATION so that the constraints are not enforced during synchronization.|  
||**0x20000**|Replicates FOREIGN KEY constraints as NOT FOR REPLICATION so that the constraints are not enforced during synchronization.|  
||**0x40000**|Replicates filegroups associated with a partitioned table or index.|  
||**0x80000**|Replicates the partition scheme for a partitioned table.|  
||**0x100000**|Replicates the partition scheme for a partitioned index.|  
||**0x200000**|Replicates table statistics.|  
||**0x400000**|Default Bindings|  
||**0x800000**|Rule Bindings|  
||**0x1000000**|Full-text index|  
||**0x2000000**|XML schema collections bound to **xml** columns are not replicated.|  
||**0x4000000**|Replicates indexes on **xml** columns.|  
||**0x8000000**|Create any schemas not already present on the subscriber.|  
||**0x10000000**|Converts **xml** columns to **ntext** on the Subscriber.|  
||**0x20000000**|Converts large object data types (**nvarchar(max)**, **varchar(max)**, and **varbinary(max)**) that were introduced in [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] to data types that are supported on [!INCLUDE[ssVersion2000](../../includes/ssversion2000-md.md)].|  
||**0x40000000**|Replicate permissions.|  
||**0x80000000**|Attempt to drop dependencies to any objects that are not part of the publication.|  
||**0x100000000**|Use this option to replicate the FILESTREAM attribute if it is specified on **varbinary(max)** columns. Do not specify this option if you are replicating tables to [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] Subscribers. Replicating tables that have FILESTREAM columns to [!INCLUDE[ssVersion2000](../../includes/ssversion2000-md.md)] Subscribers is not supported, regardless of how this schema option is set.<br /><br /> See related option **0x800000000**.|  
||**0x200000000**|Converts date and time data types (**date**, **time**, **datetimeoffset**, and **datetime2**) that were introduced in [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] to data types that are supported on earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
||**0x400000000**|Replicates the compression option for data and indexes. For more information, see [Data Compression](../../relational-databases/data-compression/data-compression.md).|  
||**0x800000000**|Set this option to store FILESTREAM data on its own filegroup at the Subscriber. If this option is not set, FILESTREAM data is stored on the default filegroup. Replication does not create filegroups; therefore, if you set this option, you must create the filegroup before you apply the snapshot at the Subscriber. For more information about how to create objects before you apply the snapshot, see [Execute Scripts Before and After the Snapshot Is Applied](../../relational-databases/replication/snapshot-options.md#execute-scripts-before-and-after-snapshot-is-applied).<br /><br /> See related option **0x100000000**.|  
||**0x1000000000**|Converts common language runtime (CLR) user-defined types (UDTs) larger than 8000 bytes to **varbinary(max)** so that columns of type UDT can be replicated to Subscribers that are running [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)].|  
||**0x2000000000**|Converts the **hierarchyid** data type to **varbinary(max)** so that columns of type **hierarchyid** can be replicated to Subscribers that are running [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)]. For more information about how to use **hierarchyid** columns in replicated tables, see [hierarchyid &#40;Transact-SQL&#41;](../../t-sql/data-types/hierarchyid-data-type-method-reference.md).|  
||**0x4000000000**|Replicates any filtered indexes on the table. For more information about filtered indexes, see [Create Filtered Indexes](../../relational-databases/indexes/create-filtered-indexes.md).|  
||**0x8000000000**|Converts the **geography** and **geometry** data types to **varbinary(max)** so that columns of these types can be replicated to Subscribers that are running [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)].|  
||**0x10000000000**|Replicates indexes on columns of type **geography** and **geometry**.|  
||**0x20000000000**|Replicates the SPARSE attribute for columns. For more information about this attribute, see [Use Sparse Columns](../../relational-databases/tables/use-sparse-columns.md).|  
||**0x40000000000**|Enable scripting by the snapshot agent to create memory optimized table on the subscriber.|  
||**0x80000000000**|Converts clustered index to nonclustered index for memory-optimized articles.|  
|**status**||Specifies the new status of the property.|  
||**dts horizontal partitions**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
||**include column names**|Column names are included in the replicated INSERT statement.|  
||**no column names**|Column names are not included in the replicated INSERT statement.|  
||**no dts horizontal partitions**|The horizontal partition for the article is not defined by a transformable subscription.|  
||**none**|Clears all status options in the [sysarticles](../../relational-databases/system-tables/sysarticles-transact-sql.md) table and marks the article as inactive.|  
||**parameters**|Changes are propagated to the Subscriber using parameterized commands. This is the default setting for a new article.|  
||**string literals**|Changes are propagated to the Subscriber using string literal values.|  
|**sync_object**||Name of the table or view used to produce a synchronization output file. The default is NULL. Not supported for Oracle Publishers.|  
|**tablespace**||Identifies the tablespace used by the logging table for an article published from an Oracle database. For more information, see [Manage Oracle Tablespaces](../../relational-databases/replication/non-sql/manage-oracle-tablespaces.md).|  
|**threshold**||The percentage value that controls when the Distribution Agent assigns a new identity range. Not supported for peer-to-peer replication.|  
|**type**||Not supported for Oracle Publishers.|  
||**logbased**|Log-based article.|  
||**logbased manualboth**|Log-based article with manual filter and manual view. This option requires that the *sync_object* and *filter* properties also be set. Not supported for Oracle Publishers.|  
||**logbased manualfilter**|Log-based article with manual filter. This option requires that the *sync_object* and *filter* properties also be set. Not supported for Oracle Publishers.|  
||**logbased manualview**|Log-based article with manual view. This option requires that the *sync_object* property also be set. Not supported for Oracle Publishers.|  
||**indexed viewlogbased**|Log-based indexed view article. Not supported for Oracle Publishers. For this type of article, the base table does not need to be published separately.|  
||**indexed viewlogbased manualboth**|Log-based indexed view article with manual filter and manual view. This option requires that the *sync_object* and *filter* properties also be set. For this type of article, the base table does not need to be published separately. Not supported for Oracle Publishers.|  
||**indexed viewlogbased manualfilter**|Log-based indexed view article with manual filter. This option requires the *sync_object* and *filter* properties also be set. For this type of article, the base table does not need to be published separately. Not supported for Oracle Publishers.|  
||**indexed viewlogbased manualview**|Log-based indexed view article with manual view. This option requires that the *sync_object* property also be set. For this type of article, the base table does not need to be published separately. Not supported for Oracle Publishers.|  
|**upd_cmd**||UPDATE statement to execute; otherwise, it is constructed from the log.|  
|NULL|NULL|Returns a list of article properties that can be changed.|  
  
`[ @force_invalidate_snapshot = ] force_invalidate_snapshot`
 Acknowledges that the action taken by this stored procedure may invalidate an existing snapshot. *force_invalidate_snapshot* is a **bit**, with a default of **0**.  
  
 **0** specifies that changes to the article do not cause the snapshot to be invalid. If the stored procedure detects that the change does require a new snapshot, an error occurs and no changes are made.  
  
 **1** specifies that changes to the article may cause the snapshot to be invalid, and if there are existing subscriptions that would require a new snapshot, gives permission for the existing snapshot to be marked as obsolete and a new snapshot generated.  
  
 See the Remarks section for the properties that, when changed, require the generation of a new snapshot.  
  
`[ @force_reinit_subscription = ]force_reinit_subscription_`
 Acknowledges that the action taken by this stored procedure may require existing subscriptions to be reinitialized. *force_reinit_subscription* is a **bit** with a default of **0**.  
  
 **0** specifies that changes to the article do not cause the subscription to be reinitialized. If the stored procedure detects that the change would require existing subscriptions to be reinitialized, an error occurs and no changes are made.  
  
 **1** specifies that changes to the article cause existing subscriptions to be reinitialized, and gives permission for the subscription reinitialization to occur.  
  
 See the Remarks section for the properties that, when changed, require that all existing subscriptions be reinitialized.  
  
`[ @publisher = ] 'publisher'`
 Specifies a non- [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Publisher. *publisher* is **sysname**, with a default of NULL.  
  
> [!NOTE]  
>  *publisher* should not be used when changing article properties on a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Publisher.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_changearticle** is used in snapshot replication and transactional replication.  
  
 When an article belongs to a publication that supports peer-to-peer transactional replication, you can only change the **description**, **ins_cmd**, **upd_cmd**, and **del_cmd** properties.  
  
 Changing any of the following properties requires that a new snapshot be generated, and you must specify a value of **1** for the *force_invalidate_snapshot* parameter:  
  
-   **del_cmd**  
  
-   **dest_table**  
  
-   **destination_owner**  
  
-   **ins_cmd**  
  
-   **pre_creation_cmd**  
  
-   **schema_options**  
  
-   **upd_cmd**  
  
 Changing any of the following properties requires that existing subscriptions be reinitialized, and you must specify a value of **1** for the *force_reinit_subscription* parameter.  
  
-   **del_cmd**  
  
-   **dest_table**  
  
-   **destination_owner**  
  
-   **filter**  
  
-   **ins_cmd**  
  
-   **status**  
  
-   **upd_cmd**  
  
 Within an existing publication, you can use **sp_changearticle** to change an article without having to drop and re-create the entire publication.  
  
> [!NOTE]  
>  When changing the value of *schema_option*, the system does not perform a bitwise update. This means that when you set *schema_option* using **sp_changearticle**, existing bit settings may be turned off. To retain the existing settings, you should perform [| (Bitwise OR)](../../t-sql/language-elements/bitwise-or-transact-sql.md) between the value that you are setting and the current value of *schema_option*, which can be determined by executing [sp_helparticle](../../relational-databases/system-stored-procedures/sp-helparticle-transact-sql.md).  
  
## Valid Schema Options  
 The following table describes the allowable values of *schema_option* based upon the replication type (shown across the top) and the article type (shown down the first column).  
  
| Article type | Replication type - Transactional | Replication type - Snapshot |
| ------------ | -------------------------------- | --------------------------- |
|**logbased**|All options|All options but **0x02**|  
|**logbased manualfilter**|All options|All options but **0x02**|  
|**logbased manualview**|All options|All options but **0x02**|  
|**indexed view logbased**|All options|All options but **0x02**|  
|**indexed view logbased manualfilter**|All options|All options but **0x02**|  
|**indexed view logbased manualview**|All options|All options but **0x02**|  
|**indexed view logbase manualboth**|All options|All options but **0x02**|  
|**proc exec**|**0x01**, **0x20**, **0x2000**, **0x400000**, **0x800000**, **0x2000000**, **0x8000000**, **0x10000000**, **0x20000000**, **0x40000000**, and **0x80000000**|**0x01**, **0x20**, **0x2000**, **0x400000**, **0x800000**, **0x2000000**, **0x8000000**, **0x10000000**, **0x20000000**, **0x40000000**, and **0x80000000**|  
|**serializable proc exec**|**0x01**, **0x20**, **0x2000**, **0x400000**, **0x800000**, **0x2000000**, **0x8000000**, **0x10000000**, **0x20000000**, **0x40000000**, and **0x80000000**|**0x01**, **0x20**, **0x2000**, **0x400000**, **0x800000**, **0x2000000**, **0x8000000**, **0x10000000**, **0x20000000**, **0x40000000**, and **0x80000000**|  
|**proc schema only**|**0x01**, **0x20**, **0x2000**, **0x400000**, **0x800000**, **0x2000000**, **0x8000000**, **0x10000000**, **0x20000000**, **0x40000000**, and **0x80000000**|**0x01**, **0x20**, **0x2000**, **0x400000**, **0x800000**, **0x2000000**, **0x8000000**, **0x10000000**, **0x20000000**, **0x40000000**, and **0x80000000**|  
|**view schema only**|**0x01**, **0x010**, **0x020**, **0x040**, **0x0100**, **0x2000**, **0x40000**, **0x100000**, **0x200000**, **0x400000**, **0x800000**, **0x2000000**, **0x8000000**, **0x40000000**, and **0x80000000**|**0x01**, **0x010**, **0x020**, **0x040**, **0x0100**, **0x2000**, **0x40000**, **0x100000**, **0x200000**, **0x400000**, **0x800000**, **0x2000000**, **0x8000000**, **0x40000000**, and **0x80000000**|  
|**func schema only**|**0x01**, **0x20**, **0x2000**, **0x400000**, **0x800000**, **0x2000000**, **0x8000000**, **0x10000000**, **0x20000000**, **0x40000000**, and **0x80000000**|**0x01**, **0x20**, **0x2000**, **0x400000**, **0x800000**, **0x2000000**, **0x8000000**, **0x10000000**, **0x20000000**, **0x40000000**, and **0x80000000**|  
|**indexed view schema only**|**0x01**, **0x010**, **0x020**, **0x040**, **0x0100**, **0x2000**, **0x40000**, **0x100000**, **0x200000**, **0x400000**, **0x800000**, **0x2000000**, **0x8000000**, **0x40000000**, and **0x80000000**|**0x01**, **0x010**, **0x020**, **0x040**, **0x0100**, **0x2000**, **0x40000**, **0x100000**, **0x200000**, **0x400000**, **0x800000**, **0x2000000**, **0x8000000**, **0x40000000**, and **0x80000000**|  
  
> [!NOTE]
>  For queued updating publications, the *schema_option* value of **0x80** must be enabled. The supported *schema_option* values for non- [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] publications are: **0x01**, **0x02**, **0x10**, **0x40**, **0x80**, **0x1000** and **0x4000**.  
  
## Example  
 [!code-sql[HowTo#sp_changetranarticle](../../relational-databases/replication/codesnippet/tsql/sp-changearticle-transac_1.sql)]  
  
## Permissions  
 Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute **sp_changearticle**.  
  
## See Also  
 [View and Modify Article Properties](../../relational-databases/replication/publish/view-and-modify-article-properties.md)   
 [Change Publication and Article Properties](../../relational-databases/replication/publish/change-publication-and-article-properties.md)   
 [sp_addarticle &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addarticle-transact-sql.md)   
 [sp_articlecolumn &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-articlecolumn-transact-sql.md)   
 [sp_droparticle &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-droparticle-transact-sql.md)   
 [sp_helparticle &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helparticle-transact-sql.md)   
 [sp_helparticlecolumns &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helparticlecolumns-transact-sql.md)  
  
  
