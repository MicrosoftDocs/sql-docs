---
title: "sp_attachsubscription (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_attachsubscription"
  - "sp_attachsubscription_TSQL"
helpviewer_keywords: 
  - "sp_attachsubscription"
ms.assetid: b9bbda36-a46a-4327-a01e-9cd632e4791b
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# sp_attachsubscription (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Attaches an existing subscription database to any Subscriber. This stored procedure is executed at the new Subscriber on the master database.  
  
> [!IMPORTANT]  
>  This feature is deprecated and will be removed in a future release. This feature should not be used in new development work. For merge publications that are partitioned using parameterized filters, we recommend using the new features of partitioned snapshots, which simplify the initialization of a large number of subscriptions. For more information, see [Snapshots for Merge Publications with Parameterized Filters](../../relational-databases/replication/create-a-snapshot-for-a-merge-publication-with-parameterized-filters.md). For publications that are not partitioned, you can initialize a subscription with a backup. For more information, see [Initialize a Transactional Subscription Without a Snapshot](../../relational-databases/replication/initialize-a-transactional-subscription-without-a-snapshot.md).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_attachsubscription [ @dbname = ] 'dbname'  
        , [ @filename = ] 'filename'  
    [ , [ @subscriber_security_mode = ] 'subscriber_security_mode' ]  
    [ , [ @subscriber_login = ] 'subscriber_login' ]  
    [ , [ @subscriber_password = ] 'subscriber_password' ]  
    [ , [ @distributor_security_mode = ] distributor_security_mode ]   
    [ , [ @distributor_login = ] 'distributor_login' ]   
    [ , [ @distributor_password = ] 'distributor_password' ]   
    [ , [ @publisher_security_mode = ] publisher_security_mode ]   
    [ , [ @publisher_login = ] 'publisher_login' ]   
    [ , [ @publisher_password = ] 'publisher_password' ]   
    [ , [ @job_login = ] 'job_login' ]   
    [ , [ @job_password = ] 'job_password' ]   
    [ , [ @db_master_key_password = ] 'db_master_key_password' ]  
```  
  
## Arguments  
`[ @dbname = ] 'dbname'`
 Is the string that specifies the destination subscription database by name. *dbname* is **sysname**, with no default.  
  
`[ @filename = ] 'filename'`
 Is the name and physical location of the primary MDF (**master** data file). *filename* is **nvarchar(260)**, with no default.  
  
`[ @subscriber_security_mode = ] 'subscriber_security_mode'`
 Is the security mode of the Subscriber to use when connecting to a Subscriber when synchronizing. *subscriber_security_mode* is **int**, with a default of NULL.  
  
> [!NOTE]  
>  Windows Authentication must be used. If *subscriber_security_mode* is not **1** (Windows Authentication), an error is returned.  
  
`[ @subscriber_login = ] 'subscriber_login'`
 Is the Subscriber login name to use when connecting to a Subscriber when synchronizing. *subscriber_login* is **sysname**, with a default of NULL.  
  
> [!NOTE]  
>  This parameter has been deprecated and is maintained only backward-compatibility of scripts. If *subscriber_security_mode* is not **1** and *subscriber_login* is specified, an error is returned.  
  
`[ @subscriber_password = ] 'subscriber_password'`
 Is the Subscriber password. *subscriber_password* is **sysname**, with a default of NULL.  
  
> [!NOTE]  
>  This parameter has been deprecated and is maintained only backward-compatibility of scripts. If *subscriber_security_mode* is not **1** and *subscriber_password* is specified, an error is returned.  
  
`[ @distributor_security_mode = ] distributor_security_mode`
 Is the security mode to use when connecting to a Distributor when synchronizing. *distributor_security_mode* is **int**, with a default of **0**. **0** specifies [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication. **1** specifies Windows Authentication. [!INCLUDE[ssNoteWinAuthentication](../../includes/ssnotewinauthentication-md.md)]  
  
`[ @distributor_login = ] 'distributor_login'`
 Is the Distributor login to use when connecting to a Distributor when synchronizing. *distributor_login* is required if *distributor_security_mode* is set to **0**. *distributor_login* is **sysname**, with a default of NULL.  
  
`[ @distributor_password = ] 'distributor_password'`
 Is the Distributor password. *distributor_password* is required if *distributor_security_mode* is set to **0**. *distributor_password* is **sysname**, with a default of NULL. The value of *distributor_password* must be less than 120 Unicode characters.  
  
> [!IMPORTANT]  
>  Do not use a blank password. Use a strong password. When possible, prompt users to enter security credentials at runtime. If you must store credentials in a script file, you must secure the file to prevent unauthorized access.  
  
`[ @publisher_security_mode = ] publisher_security_mode`
 Is the security mode to use when connecting to a Publisher when synchronizing. *publisher_security_mode* is **int**, with a default of **1**. If **0**, specifies [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication. If **1**, specifies Windows Authentication. [!INCLUDE[ssNoteWinAuthentication](../../includes/ssnotewinauthentication-md.md)]  
  
`[ @publisher_login = ] 'publisher_login'`
 Is the login to use when connecting to a Publisher when synchronizing. *publisher_login* is **sysname**, with a default of NULL.  
  
`[ @publisher_password = ] 'publisher_password'`
 Is the password used when connecting to the Publisher. *publisher_password* is **sysname**, with a default of NULL. The value of *publisher_password* must be less than 120 Unicode characters.  
  
> [!IMPORTANT]  
>  Do not use a blank password. Use a strong password. When possible, prompt users to enter security credentials at runtime. If you must store credentials in a script file, you must secure the file to prevent unauthorized access.  
  
`[ @job_login = ] 'job_login'`
 Is the login for the Windows account under which the agent runs. *job_login* is **nvarchar(257)**, with no default. This Windows account is always used for agent connections to the Distributor.  
  
`[ @job_password = ] 'job_password'`
 Is the password for the Windows account under which the agent runs. *job_password* is **sysname**, with no default. The value of *job_password* must be less than 120 Unicode characters.  
  
> [!IMPORTANT]  
>  When possible, prompt users to enter security credentials at runtime. If you must store credentials in a script file, you must secure the file to prevent unauthorized access.  
  
`[ @db_master_key_password = ] 'db_master_key_password'`
 Is the password of a user-defined Database Master Key. *db_master_key_password* is **nvarchar(524)**, with a default value of NULL. If *db_master_key_password* is not specified, an existing Database Master Key will be dropped and re-created.  
  
> [!IMPORTANT]  
>  When possible, prompt users to enter security credentials at runtime. If you must store credentials in a script file, you must secure the file to prevent unauthorized access.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_attachsubscription** is used in snapshot replication, transactional replication, and merge replication.  
  
 A subscription cannot be attached to the publication if the publication retention period has expired. If a subscription with an elapsed retention period is specified, an error occurs either when the subscription is attached or when it is first synchronized. Publications with a publication retention period of **0** (never expire) are ignored.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role can execute **sp_attachsubscription**.  
  
## See Also  
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
