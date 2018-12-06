---
title: "sp_replmonitorhelpmergesession (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_replmonitorhelpmergesession_TSQL"
  - "sp_replmonitorhelpmergesession"
helpviewer_keywords: 
  - "sp_replmonitorhelpmergesession"
ms.assetid: a0400ba8-9609-4901-917e-925e119103a1
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_replmonitorhelpmergesession (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns information on past sessions for a given replication Merge Agent, with one row returned for each session that matches the filtering criterion. This stored procedure, which is used to monitor merge replication, is executed at the Distributor on the distribution database or at the Subscriber on the subscription database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_replmonitorhelpmergesession [ [ @agent_name = ] 'agent_name' ]  
    [ , [ @hours = ] hours ]  
    [ , [ @session_type = ] session_type ]  
    [ , [ @publisher = ] 'publisher' ]  
    [ , [ @publisher_db = ] 'publisher_db' ]  
    [ , [ @publication = ] 'publication' ]   
```  
  
## Arguments  
 [ **@agent_name** = ] **'***agent_name***'**  
 Is the name of the agent. *agent_name* is **nvarchar(100)** with no default.  
  
 [ **@hours** = ] *hours*  
 Is the range of time, in hours, for which historical agent session information is returned. *hours* is **int**, which can be one of the following ranges.  
  
|Value|Description|  
|-----------|-----------------|  
|< **0**|Returns information on past agent runs, up to a maximum of 100 runs.|  
|**0** (default)|Returns information on all past agent runs.|  
|> **0**|Returns information on agent runs that occurred in the last *hours* number of hours.|  
  
 [ **@session_type** = ] *session_type*  
 Filters the result set based on the session end result. *session_type* is **int**, and can be one of these values.  
  
|Value|Description|  
|-----------|-----------------|  
|**1** (default)|Agent sessions with a retry or succeed result.|  
|**0**|Agent sessions with a failure result.|  
  
 [ **@publisher** = ] **'***publisher***'**  
 Is the name of the Publisher. *publisher* is **sysname**, with a default of NULL. This parameter is used when executing **sp_replmonitorhelpmergesession** at the Subscriber.  
  
 [ **@publisher_db** = ] **'***publisher_db***'**  
 Is the name of the publication database. *publisher_db* is **sysname**, with a default of NULL. This parameter is used when executing **sp_replmonitorhelpmergesession** at the Subscriber.  
  
 [ **@publication=** ] **'***publication***'**  
 Is the name of the publication. *publication* is **sysname**, with a default of NULL. This parameter is used when executing **sp_replmonitorhelpmergesession** at the Subscriber.  
  
## Result Sets  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**Session_id**|**int**|ID of the agent job session.|  
|**Status**|**int**|Agent run status:<br /><br /> **1** = Start<br /><br /> **2** = Succeed<br /><br /> **3** = In progress<br /><br /> **4** = Idle<br /><br /> **5** = Retry<br /><br /> **6** = Fail|  
|**StartTime**|**datetime**|Time agent job session began.|  
|**EndTime**|**datetime**|Time agent job session was completed.|  
|**Duration**|**int**|Cumulative duration, in seconds, of this job session.|  
|**UploadedCommands**|**int**|Number of commands uploaded during the agent session.|  
|**DownloadedCommands**|**int**|Number of commands downloaded during the agent session.|  
|**ErrorMessages**|**int**|Number of error messages that were generated during the agent session.|  
|**ErrorID**|**int**|ID of the error that occurred|  
|**PercentageDone**|**decimal**|Estimated percent of the total changes that have already been delivered in an active session.|  
|**TimeRemaining**|**int**|Estimated number of seconds left in an active session.|  
|**CurrentPhase**|**int**|Is the current phase of an active session, which can be one of the following.<br /><br /> **1** = Upload<br /><br /> **2** = Download|  
|**LastMessage**|**nvarchar(500)**|Is the last message logged by Merge Agent during the session.|  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_replmonitorhelpmergesession** is used to monitor merge replication.  
  
 When executed on the Subscriber, **sp_replmonitorhelpmergesession** only returns information on the last five Merge Agent sessions.  
  
## Permissions  
 Only members of the **db_owner** or **replmonitor** fixed database role on the distribution database at the Distributor or on the subscription database at the Subscriber can execute **sp_replmonitorhelpmergesession**.  
  
## See Also  
 [Programmatically Monitor Replication](../../relational-databases/replication/monitor/programmatically-monitor-replication.md)  
  
  
