---
title: "sp_helpsubscriberinfo (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_helpsubscriberinfo"
  - "sp_helpsubscriberinfo_TSQL"
helpviewer_keywords: 
  - "sp_helpsubscriberinfo"
ms.assetid: fbabe1ec-57cf-425c-bae7-af7f5d3198fd
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_helpsubscriberinfo (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Displays information about a Subscriber. This stored procedure is executed at the Publisher on any database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_helpsubscriberinfo [ [ @subscriber =] 'subscriber']  
    [ , [ @publisher = ] 'publisher' ]  
```  
  
## Arguments  
 [ **@subscriber =** ] **'**_subscriber_**'**  
 Is the name of the Subscriber. *subscriber* is **sysname**, with a default of **%**, which returns all information.  
  
 [ **@publisher =** ] **'**_publisher_**'**  
 Is the name of the Publisher. *publisher* is **sysname**, and defaults to the name of the current server.  
  
> [!NOTE]  
>  *publisher* should not be specified, except when it is an Oracle Publisher.  
  
## Result Sets  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**publisher**|**sysname**|Name of the Publisher.|  
|**subscriber**|**sysname**|Name of the Subscriber.|  
|**type**|**tinyint**|Type of Subscriber:<br /><br /> **0** = [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database **1** = ODBC data source|  
|**login**|**sysname**|Login ID for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication.|  
|**password**|**sysname**|Password for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication.|  
|**commit_batch_size**|**int**|Not supported.|  
|**status_batch_size**|**int**|Not supported.|  
|**flush_frequency**|**int**|Not supported.|  
|**frequency_type**|**int**|Frequency with which the Distribution Agent is run:<br /><br /> **1** = One time<br /><br /> **2** = On demand<br /><br /> **4** = Daily<br /><br /> **8** = Weekly<br /><br /> **16** = Monthly<br /><br /> **32** = Monthly relative<br /><br /> **64** = Autostart<br /><br /> **128** = Recurring|  
|**frequency_interval**|**int**|Value applied to the frequency set by *frequency_type*.|  
|**frequency_relative_interval**|**int**|Date of the Distribution Agent used when *frequency_type* is set to **32** (monthly relative):<br /><br /> **1** = First<br /><br /> **2** = Second<br /><br /> **4** = Third<br /><br /> **8** = Fourth<br /><br /> **16** = Last|  
|**frequency_recurrence_factor**|**int**|Recurrence factor used by *frequency_type*.|  
|**frequency_subday**|**int**|How often to reschedule during the defined period:<br /><br /> **1** = Once<br /><br /> **2** = Second<br /><br /> **4** = Minute<br /><br /> **8** = Hour|  
|**frequency_subday_interval**|**int**|Interval for *frequency_subday*.|  
|**active_start_time_of_day**|**int**|Time of day when the Distribution Agent is first scheduled, formatted as HHMMSS.|  
|**active_end_time_of_day**|**int**|Time of day when the Distribution Agent stops being scheduled, formatted as HHMMSS.|  
|**active_start_date**|**int**|Date when the Distribution Agent is first scheduled, formatted as YYYYMMDD.|  
|**active_end_date**|**int**|Date when the Distribution Agent stops being scheduled, formatted as YYYYMMDD.|  
|**retryattempt**|**int**|Not supported.|  
|**retrydelay**|**int**|Not supported.|  
|**description**|**nvarchar(255)**|Text description of the Subscriber.|  
|**security_mode**|**int**|Implemented security mode:<br /><br /> **0** = [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication<br /><br /> **1** = [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows Authentication|  
|**frequency_type2**|**int**|Frequency with which the Merge Agent is run:<br /><br /> **1** = One time<br /><br /> **2** = On demand<br /><br /> **4** = Daily<br /><br /> **8** = Weekly<br /><br /> **16** = Monthly<br /><br /> **32** = Monthly relative<br /><br /> **64** = Autostart<br /><br /> **128** = Recurring|  
|**frequency_interval2**|**int**|Value applied to the frequency set by *frequency_type*.|  
|**frequency_relative_interval2**|**int**|Date of the Merge Agent used when *frequency_type* is set to 32(monthly relative):<br /><br /> **1** = First<br /><br /> **2** = Second<br /><br /> **4** = Third<br /><br /> **8** = Fourth<br /><br /> **16** = Last|  
|**frequency_recurrence_factor2**|**int**|Recurrence factor used by *frequency_type**.*|  
|**frequency_subday2**|**int**|How often to reschedule during the defined period:<br /><br /> **1** = Once<br /><br /> **2** = Second<br /><br /> **4** = Minute<br /><br /> **8** = Hour|  
|**frequency_subday_interval2**|**int**|Interval for *frequency_subday*.|  
|**active_start_time_of_day2**|**int**|Time of day when the Merge Agent is first scheduled, formatted as HHMMSS.|  
|**active_end_time_of_day2**|**int**|Time of day when the Merge Agent stops being scheduled, formatted as HHMMSS.|  
|**active_start_date2**|**int**|Date when the Merge Agent is first scheduled, formatted as YYYYMMDD.|  
|**active_end_date2**|**int**|Date when the Merge Agent stops being scheduled, formatted as YYYYMMDD.|  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_helpsubscriberinfo** is used in snapshot replication, transactional replication, and merge replication.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role, the **db_owner** fixed database role, or the publication access list for the publication can execute **sp_helpsubscriberinfo**.  
  
## See Also  
 [sp_adddistpublisher &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-adddistpublisher-transact-sql.md)   
 [sp_addpullsubscription &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addpullsubscription-transact-sql.md)   
 [sp_changesubscriber &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-changesubscriber-transact-sql.md)   
 [sp_dropsubscriber &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dropsubscriber-transact-sql.md)   
 [sp_helpdistributor &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helpdistributor-transact-sql.md)   
 [sp_helpserver &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helpserver-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
