---
title: "sp_helpxactsetjob (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_helpxactsetjob"
  - "sp_helpxactsetjob_TSQL"
helpviewer_keywords: 
  - "sp_helpxactsetjob"
ms.assetid: 242cea3e-e6ac-4f84-a072-b003b920eb33
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_helpxactsetjob (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Displays information on the Xactset job for an Oracle Publisher. This stored procedure is executed at the Distributor on any database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_helpxactsetjob [ @publisher = ] 'publisher'   
```  
  
## Arguments  
 [**@publisher** = ] **'***publisher***'**  
 Is the name of the non- [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Publisher to which the job belongs. *publisher* is **sysname**, with no default.  
  
## Result Sets  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**jobnumber**|**int**|Oracle job number.|  
|**lastdate**|**varchar(22)**|Last date that the job ran.|  
|**thisdate**|**varchar(22)**|Time of change|  
|**nextdate**|**varchar(22)**|Next date that the job will run.|  
|**broken**|**varchar(1)**|Flag indicating if the job is broken.|  
|**interval**|**varchar(200)**|Interval for the job.|  
|**failures**|**int**|Number of failures for the job.|  
|**xactsetjobwhat**|**varchar(200)**|Name of procedure executed by the job.|  
|**xactsetjob**|**varchar(1)**|Status of the job, which can be one of the following:<br /><br /> **1** - the job is enabled.<br /><br /> **0** - the job is disabled.|  
|**xactsetlonginterval**|**int**|Long interval for the job.|  
|**xactsetlongthreshold**|**int**|Long threshold for the job.|  
|**xactsetshortinterval**|**int**|Short interval for the job.|  
|**xactsetshortthreshold**|**int**|Short threshold for the job.|  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_helpxactsetjob** is used in snapshot replication and transactional replication for an Oracle Publishers.  
  
 **sp_helpxactsetjob** always returns the current settings for the Xactset job (HREPL_XactSetJob) at the publisher. If the Xactset job is currently in the job queue, it additionally returns attributes of the job from the USER_JOB data dictionary view created under the administrator account at the Oracle Publisher.  
  
## Permissions  
 Only a member of the **sysadmin** fixed server role can execute **sp_helpxactsetjob**.  
  
## See Also  
 [Configure the Transaction Set Job for an Oracle Publisher &#40;Replication Transact-SQL Programming&#41;](../../relational-databases/replication/administration/configure-the-transaction-set-job-for-an-oracle-publisher.md)   
 [sp_publisherproperty &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-publisherproperty-transact-sql.md)  
  
  
