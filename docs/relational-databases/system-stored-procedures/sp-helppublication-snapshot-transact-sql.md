---
title: "sp_helppublication_snapshot (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_helppublication_snapshot"
  - "sp_helppublication_snapshot_TSQL"
helpviewer_keywords: 
  - "sp_helppublication_snapshot"
ms.assetid: 97b4a7ae-40a5-4328-88f1-ff5d105bbb34
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_helppublication_snapshot (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns information on the Snapshot agent for a given publication. This stored procedure is executed at the Publisher on the publication database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_helppublication_snapshot [ @publication = ] 'publication'  
    [ , [ @publisher = ] 'publisher' ]  
```  
  
## Arguments  
`[ @publication = ] 'publication'`
 Is the name of the publication. *publication* is **sysname**, with no default.  
  
`[ @publisher = ] 'publisher'`
 Specifies a non- [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] publisher. *publisher* is **sysname**, with a default of NULL.  
  
> [!NOTE]  
>  *publisher* should not be used when adding an article to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Publisher.  
  
## Result Sets  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**id**|**int**|ID of the Snapshot Agent.|  
|**name**|**nvarchar(100)**|Name of the Snapshot Agent.|  
|**publisher_security_mode**|**smallint**|Security mode used by the agent when connecting to the Publisher, which can be one of the following:<br /><br /> **0** = [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication<br /><br /> **1** = Windows Authentication.|  
|**publisher_login**|**sysname**|Login used when connecting to the Publisher.|  
|**publisher_password**|**nvarchar(524)**|For security reasons, a value of **\*\*\*\*\*\*\*\*\*\*** is always returned.|  
|**job_id**|**uniqueidentifier**|Unique ID of the agent job.|  
|**job_login**|**nvarchar(512)**|Is the Windows account under which the Snapshot agent runs, which is returned in the format *DOMAIN*\\*username*.|  
|**job_password**|**sysname**|For security reasons, a value of **\*\*\*\*\*\*\*\*\*\*** is always returned.|  
|**schedule_name**|**sysname**|Name of the schedule used for this agent job.|  
|**frequency_type**|**int**|Is the frequency with which the agent is scheduled to run, which can be one of these values.<br /><br /> **1** = One time<br /><br /> **2** = On demand<br /><br /> **4** = Daily<br /><br /> **8** = Weekly<br /><br /> **16** = Monthly<br /><br /> **32** = Monthly relative<br /><br /> **64** = Autostart<br /><br /> **128** = Recurring|  
|**frequency_interval**|**int**|The days that the agent runs, which can be one of these values.<br /><br /> **1** = Sunday<br /><br /> **2** = Monday<br /><br /> **3** = Tuesday<br /><br /> **4** = Wednesday<br /><br /> **5** = Thursday<br /><br /> **6** = Friday<br /><br /> **7** = Saturday<br /><br /> **8** = Day<br /><br /> **9** = Weekdays<br /><br /> **10** = Weekend days|  
|**frequency_subday_type**|**int**|Is the type that defines how often the agent runs when *frequency_type* is **4** (daily), and can be one of these values.<br /><br /> **1** = At the specified time<br /><br /> **2** = Seconds<br /><br /> **4** = Minutes<br /><br /> **8** = Hours|  
|**frequency_subday_interval**|**int**|Number of intervals of *frequency_subday_type* that occur between scheduled execution of the agent.|  
|**frequency_relative_interval**|**int**|Is the week that the agent runs in a given month when *frequency_type* is **32** (monthly relative), and can be one of these values.<br /><br /> **1** = First<br /><br /> **2** = Second<br /><br /> **4** = Third<br /><br /> **8** = Fourth<br /><br /> **16** = Last|  
|**frequency_recurrence_factor**|**int**|Number of weeks or months between the scheduled execution of the agent.|  
|**active_start_date**|**int**|Date when the agent is first scheduled to run, formatted as YYYYMMDD.|  
|**active_end_date**|**int**|Date when the agent is last scheduled to run, formatted as YYYYMMDD.|  
|**active_start_time**|**int**|Time when the agent is first scheduled to run, formatted as HHMMSS.|  
|**active_end_time**|**int**|Time when the agent is last scheduled to run, formatted as HHMMSS.|  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_help_publication_snapshot** is used in all types of replication.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role at the Publisher or members of the **db_owner** fixed database role on the publication database can execute **sp_help_publication_snapshot**.  
  
## See Also  
 [View and Modify Publication Properties](../../relational-databases/replication/publish/view-and-modify-publication-properties.md)   
 [sp_addpublication_snapshot &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addpublication-snapshot-transact-sql.md)   
 [sp_changepublication_snapshot &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-changepublication-snapshot-transact-sql.md)   
 [sp_dropmergepublication &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dropmergepublication-transact-sql.md)   
 [sp_droppublication &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-droppublication-transact-sql.md)  
  
  
