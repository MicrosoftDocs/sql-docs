---
title: "sp_fulltext_service (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_fulltext_service"
  - "sp_fulltext_service_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "full-text search [SQL Server], properties"
  - "sp_fulltext_service"
  - "Full-Text Search Upgrade Option"
ms.assetid: 17a91433-f9b6-4a40-88c4-8c704ec2de9f
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# sp_fulltext_service (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Changes the server properties of full-text search for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_fulltext_service [ [@action=] 'action'   
     [ , [ @value= ] value ] ]  
```  
  
## Arguments  
`[ @action = ] 'action'`
 Is the property to be changed or reset. *action* is **nvarchar(100),** with no default. For a list of a*c*tion properties, their descriptions, and the values that can be set, see the table under the *value* argument. This argument returns the following properties: data type, current running value, minimum or maximum value, and deprecation status, if applicable.  
  
`[ @value = ] value`
 Is the value of the specified property. *value* is **sql_variant**, with a default value of NULL. If @value is null, **sp_fulltext_service** returns the current setting. This table lists action properties, their descriptions, and the values that can be set.  
  
> [!NOTE]  
>  The following actions will be removed in a future release of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]: **clean_up**, **connect_timeout**, **data_timeout**, and **resource_usage**. Avoid using these actions in new development work, and plan to modify applications that currently use any of them.  
  
|Action|Data type|Description|  
|------------|---------------|-----------------|  
|**clean_up**|**int**|Supported for backward compatibility only. The value is always 0.|  
|**connect_timeout**|**int**|Supported for backward compatibility only. The value is always 0.|  
|**data_timeout**|**int**|Supported for backward compatibility only. The value is always 0.|  
|**load_os_resources**|**int**|Indicates whether operating system word breakers, stemmers, and filters are registered and used with this instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. One of:<br /><br /> 0 = Use only filters and word breakers specific to this instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].<br /><br /> 1 = Load operating system filters and word breakers.<br /><br /> By default, this property is disabled to prevent inadvertent behavior changes by updates made to the operating system. Enabling use of operating system resources provides access to resources for languages and document types registered with [!INCLUDE[msCoName](../../includes/msconame-md.md)] Indexing Service that do not have an instance-specific resource installed. If you enable the loading of operating system resources, ensure that the operating system resources are trusted signed binaries; otherwise, they cannot be loaded when **verify_signature** (see below) is set to 1.|  
|**master_merge_dop**|**int**|Specifies the number of threads to be used by the master merge process. This value should not exceed the number of available CPUs or CPU cores.<br /><br /> When this argument is not specified, the service uses the lesser of 4, or the number of available CPUs or CPU cores.|  
|**pause_indexing**|**int**|Specifies whether full-text indexing should be paused, if it is currently running, or resumed, if it is currently paused.<br /><br /> 0 = Resumes full-text indexing activities for the server instance.<br /><br /> 1 = Pauses full-text indexing activities for the server instance.|  
|**resource_usage**|**int**|Has no function in [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] and later versions, and is ignored.|  
|**update_languages**|NULL|Updates the list of languages and filters that are registered with full-text search. The languages are specified when configuring indexing and in full-text queries. Filters are used by the filter daemon host to extract textual information from corresponding file formats such as .docx stored in data types, such as **varbinary**, **varbinary(max)**, **image**, or **xml**, for full-text indexing.<br /><br /> For more information, see [View or Change Registered Filters and Word Breakers](../../relational-databases/search/view-or-change-registered-filters-and-word-breakers.md).|  
|**upgrade_option**|**int**|Controls how full-text indexes are migrated when upgrading a database from [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] to a later version. This property applies to upgrading by attaching a database, restoring a database backup, restoring a file backup, or copying the database by using the Copy Database Wizard.<br /><br /> One of:<br /><br /> 0 = Full-text catalogs are rebuilt using the new and enhanced word breakers. Rebuilding indexes can take awhile, and a significant amount of CPU and memory might be required after the upgrade.<br /><br /> 1 = Full-text catalogs are reset. [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] full-text catalog files are removed, but the metadata for full-text catalogs and full-text indexes is retained. After being upgraded, all full-text indexes are disabled for change tracking and crawls are not started automatically. The catalog will remain empty until you manually issue a full population, after the upgrade completes.<br /><br /> 2 = Full-text catalogs are imported. Typically, import is significantly faster than rebuild. For example, when using only one CPU, import runs about 10 times faster than rebuild. However, an imported full-text catalog does not use the new and enhanced word breakers, so you might want to rebuild your full-text catalogs eventually.<br /><br /> Note: Rebuild can run in multi-threaded mode, and if more than 10 CPUs are available, rebuild might run faster than import if you allow rebuild to use all of the CPUs.<br /><br /> If a full-text catalog is not available, the associated full-text indexes are rebuilt. This option is available for only [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] databases.<br /><br /> For information about choosing a full-text upgrade option, see full-[Upgrade Full-Text Search](../../relational-databases/search/upgrade-full-text-search.md).<br /><br /> Note: To set this property in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], use the **Full-Text Upgrade Option** property. For more information, see [Manage and Monitor Full-Text Search for a Server Instance](../../relational-databases/search/manage-and-monitor-full-text-search-for-a-server-instance.md).|  
|**verify_signature**|**int**|Indicates whether only signed binaries are loaded by the Full-Text Engine. By default, only trusted, signed binaries are loaded.<br /><br /> 1 = Verify that only trusted, signed binaries are loaded (default).<br /><br /> 0 = Do not verify whether binaries are signed.|  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Result Sets  
 None  
  
## Permissions  
 Only members of the **serveradmin** fixed server role or the system administrator can execute **sp_fulltext_service**.  
  
## Examples  
  
### A. Updating the list of registered languages  
 The following example updates the list of languages registered with full-text search.  
  
```  
EXEC sp_fulltext_service 'update_languages';  
GO  
```  
  
### B. Changing the full-text upgrade option to reset full-text catalogs  
 The following example changes the full-text upgrade option to reset full-text catalogs. This removes them completely. This example specifies the optional `@action` and `@value` keywords.  
  
```  
EXEC sp_fulltext_service @action='upgrade_option', @value=1;  
GO  
```  
  
## See Also  
 [Full-Text Search](../../relational-databases/search/full-text-search.md)   
 [FULLTEXTSERVICEPROPERTY &#40;Transact-SQL&#41;](../../t-sql/functions/fulltextserviceproperty-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
