---
title: "sp_addpublication_snapshot (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/15/2018"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_addpublication_snapshot_TSQL"
  - "sp_addpublication_snapshot"
helpviewer_keywords: 
  - "sp_addpublication_snapshot"
ms.assetid: 192b6214-df6e-44a3-bdd4-9d933a981619
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_addpublication_snapshot (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdbmi-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdbmi-xxxx-xxx-md.md)]

  Creates the Snapshot Agent for the specified publication. This stored procedure is executed at the Publisher on the publication database.  
  
> [!IMPORTANT]  
>  When configuring a Publisher with a remote Distributor, the values supplied for all parameters, including *job_login* and *job_password*, are sent to the Distributor as plain text. You should encrypt the connection between the Publisher and its remote Distributor before executing this stored procedure. For more information, see [Enable Encrypted Connections to the Database Engine &#40;SQL Server Configuration Manager&#41;](../../database-engine/configure-windows/enable-encrypted-connections-to-the-database-engine.md).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_addpublication_snapshot [ @publication= ] 'publication'  
    [ , [ @frequency_type= ] frequency_type ]  
    [ , [ @frequency_interval= ] frequency_interval ]  
    [ , [ @frequency_subday= ] frequency_subday ]  
    [ , [ @frequency_subday_interval= ] frequency_subday_interval ]  
    [ , [ @frequency_relative_interval= ] frequency_relative_interval ]  
    [ , [ @frequency_recurrence_factor= ] frequency_recurrence_factor ]  
    [ , [ @active_start_date= ] active_start_date ]  
    [ , [ @active_end_date= ] active_end_date ]  
    [ , [ @active_start_time_of_day= ] active_start_time_of_day ]  
    [ , [ @active_end_time_of_day= ] active_end_time_of_day ]  
    [ , [ @snapshot_job_name = ] 'snapshot_agent_name' ]  
    [ , [ @publisher_security_mode = ] publisher_security_mode ]  
    [ , [ @publisher_login = ] 'publisher_login' ]  
    [ , [ @publisher_password = ] 'publisher_password' ]   
    [ , [ @job_login = ] 'job_login' ]  
    [ , [ @job_password = ] 'job_password' ]  
    [ , [ @publisher = ] 'publisher' ]  
```  
  
## Arguments  
 [ **@publication=**] **'**_publication_**'**  
 Is the name of the publication. *publication* is **sysname**, with no default.  
  
 [ **@frequency_type=**] *frequency_type*  
 Is the frequency with which the Snapshot Agent is executed. *frequency_type* is **int**, and can be one of the following values.  
  
|Value|Description|  
|-----------|-----------------|  
|**1**|Once.|  
|**4** (default)|Daily.|  
|**8**|Weekly.|  
|**16**|Monthly.|  
|**32**|Monthly, relative to the frequency interval.|  
|**64**|When [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent starts.|  
|**128**|Run when the computer is idle|  
  
 [ **@frequency_interval=**] *frequency_interval*  
 Is the value to apply to the frequency set by *frequency_type*. *frequency_interval* is **int**, and can be one of the following values.  
  
|Value of frequency_type|Effect on frequency_interval|  
|------------------------------|-----------------------------------|  
|**1**|*frequency_interval* is unused.|  
|**4** (default)|Every *frequency_interval* days, with a default of daily.|  
|**8**|*frequency_interval* is one or more of the following (combined with a [&#124; (Bitwise OR)](../../t-sql/language-elements/bitwise-or-transact-sql.md) logical operator):<br /><br /> **1** = Sunday &#124;<br /><br /> **2** = Monday &#124;<br /><br /> **4** = Tuesday &#124;<br /><br /> **8** = Wednesday &#124;<br /><br /> **16** = Thursday &#124;<br /><br /> **32** = Friday &#124;<br /><br /> **64** = Saturday|  
|**16**|On the *frequency_interval* day of the month.|  
|**32**|*frequency_interval* is one of the following:<br /><br /> **1** = Sunday &#124;<br /><br /> **2** = Monday &#124;<br /><br /> **3** = Tuesday &#124;<br /><br /> **4** = Wednesday &#124;<br /><br /> **5** = Thursday &#124;<br /><br /> **6** = Friday &#124;<br /><br /> **7** = Saturday &#124;<br /><br /> **8** = Day &#124;<br /><br /> **9** = Weekday &#124;<br /><br /> **10** = Weekend day|  
|**64**|*frequency_interval* is unused.|  
|**128**|*frequency_interval* is unused.|  
  
 [ **@frequency_subday=**] *frequency_subday*  
 Is the unit for *freq_subday_interval*. *frequency_subday* is **int**, and can be one of these values.  
  
|Value|Description|  
|-----------|-----------------|  
|**1**|Once|  
|**2**|Second|  
|**4** (default)|Minute|  
|**8**|Hour|  
  
 [ **@frequency_subday_interval=**] *frequency_subday_interval*  
 Is the interval for *frequency_subday*. *frequency_subday_interval* is **int**, with a default of 5, which means every 5 minutes.  
  
 [ **@frequency_relative_interval=**] *frequency_relative_interval*  
 Is the date the Snapshot Agent runs. *frequency_relative_interval* is **int**, with a default of 1.  
  
 [ **@frequency_recurrence_factor=**] *frequency_recurrence_factor*  
 Is the recurrence factor used by *frequency_type*. *frequency_recurrence_factor* is **int**, with a default of 0.  
  
 [ **@active_start_date=**] *active_start_date*  
 Is the date when the Snapshot Agent is first scheduled, formatted as YYYYMMDD. *active_start_date* is **int**, with a default of 0.  
  
 [ **@active_end_date=**] *active_end_date*  
 Is the date when the Snapshot Agent stops being scheduled, formatted as YYYYMMDD. *active_end_date* is **int**, with a default of 99991231, which means December 31, 9999.  
  
 [ **@active_start_time_of_day=**] *active_start_time_of_day*  
 Is the time of day when the Snapshot Agent is first scheduled, formatted as HHMMSS. *active_start_time_of_day* is **int**, with a default of 0.  
  
 [ **@active_end_time_of_day=**] *active_end_time_of_day*  
 Is the time of day when the Snapshot Agent stops being scheduled, formatted as HHMMSS. *active_end_time_of_day* is **int**, with a default of 235959, which means 11:59:59 P.M. as measured on a 24-hour clock.  
  
 [ **@snapshot_job_name =** ] **'**_snapshot_agent_name_**'**  
 Is the name of an existing Snapshot Agent job name if an existing job is being used. *snapshot_agent_name* is **nvarchar(100)** with a default value of NULL. This parameter is for internal use and should not be specified when creating a new publication. If *snapshot_agent_name* is specified, then *job_login* and *job_password* must be NULL.  
  
 [ **@publisher_security_mode**= ] *publisher_security_mode*  
 Is the security mode used by the agent when connecting to the Publisher. *publisher_security_mode* is **smallint**, with a default of 1. **0** specifies [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication, and **1** specifies Windows Authentication. A value of **0** must be specified for non- [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Publishers. [!INCLUDE[ssNoteWinAuthentication](../../includes/ssnotewinauthentication-md.md)]  
  
 [ **@publisher_login**= ] **'**_publisher_login_**'**  
 Is the login used when connecting to the Publisher. *publisher_login* is **sysname**, with a default of NULL. *publisher_login* must be specified when *publisher_security_mode* is **0**. If *publisher_login* is NULL and *publisher_security_mode* is **1**, then the account specified in *job_login* will be used when connecting to the Publisher.  
  
 [ **@publisher_password**= ] **'**_publisher_password_**'**  
 Is the password used when connecting to the Publisher. *publisher_password* is **sysname**, with a default of NULL.  
  
> [!IMPORTANT]  
>  Do not store authentication information in script files. To help improve security, we recommend that you provide login names and passwords at run time.  
  
 [ **@job_login**= ] **'**_job_login_**'**  
 Is the login for the account under which the agent runs. On Azure SQL Database Managed Instance, use a SQL Server account. *job_login* is **nvarchar(257)**, with a default of NULL. This account is always used for agent connections to the Distributor. You must supply this parameter when creating a new Snapshot Agent job.  
  
> [!NOTE]
>  For non- [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Publishers, this must be the same login specified in [sp_adddistpublisher &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-adddistpublisher-transact-sql.md).  
  
 [ **@job_password**= ] **'**_job_password_**'**  
 Is the password for the Windows account under which the agent runs. *job_password* is **sysname**, with no default. You must supply this parameter when creating a new Snapshot Agent job.  
  
> [!IMPORTANT]  
>  Do not store authentication information in script files. To help improve security, we recommend that you provide login names and passwords at run time.  
  
 [ **@publisher**= ] **'**_publisher_**'**  
 Specifies a non- [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Publisher. *publisher* is **sysname**, with a default of NULL.  
  
> [!NOTE]  
>  *publisher* should not be used when creating a Snapshot Agent at a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Publisher.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_addpublication_snapshot** is used in snapshot replication, transactional replication, and merge replication.  
  
## Example  
 [!code-sql[HowTo#sp_AddTranPub](../../relational-databases/replication/codesnippet/tsql/sp-addpublication-snapsh_1.sql)]  
  
## Permissions  
 Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute **sp_addpublication_snapshot**.  
  
## See Also  
 [Create a Publication](../../relational-databases/replication/publish/create-a-publication.md)   
 [Create and Apply the Snapshot](../../relational-databases/replication/create-and-apply-the-initial-snapshot.md)   
 [sp_addpublication &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addpublication-transact-sql.md)   
 [sp_changepublication_snapshot &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-changepublication-snapshot-transact-sql.md)   
 [sp_startpublication_snapshot &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-startpublication-snapshot-transact-sql.md)   
 [Replication Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/replication-stored-procedures-transact-sql.md)  
  
  
