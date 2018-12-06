---
title: "sp_adddistributor (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_adddistributor"
  - "sp_adddistributor_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_adddistributor"
ms.assetid: 35415502-68d0-40f6-993c-180e50004f1e
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# sp_adddistributor (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Creates an entry in the [sys.sysservers](../../relational-databases/system-compatibility-views/sys-sysservers-transact-sql.md) table (if there is not one), marks the server entry as a Distributor, and stores property information. This stored procedure is executed at the Distributor on the master database to register and mark the server as a distributor. In the case of a remote distributor, it is also executed at the Publisher from the master database to register the remote distributor.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_adddistributor [ @distributor= ] 'distributor'   
    [ , [ @heartbeat_interval= ] heartbeat_interval ]   
    [ , [ @password= ] 'password' ]   
    [ , [ @from_scripting= ] from_scripting ]  
```  
  
## Arguments  
 [ **@distributor=**] **'***distributor***'**  
 Is the distribution server name. *distributor* is **sysname**, with no default. This parameter is only used if setting up a remote Distributor. It adds entries for the Distributor properties in the **msdb..MSdistributor** table.  
  
 [ **@heartbeat_interval=**] *heartbeat_interval*  
 Is the maximum number of minutes that an agent can go without logging a progress message. *heartbeat_interval* is **int**, with a default of 10 minutes. A SQL Server Agent job is created that runs on this interval to check the status of the replication agents that are running.  
  
 [ **@password=**]  **'***password***'**]  
 Is the password of the **distributor_admin** login. *password* is **sysname**, with a default of NULL. If NULL or an empty string, password is reset to a random value. The password must be configured when the first remote distributor is added. **distributor_admin** login and *password* are stored for linked server entry used for a *distributor* RPC connection, including local connections. If *distributor* is local, the password for **distributor_admin** is set to a new value. For Publishers with a remote Distributor, the same value for *password* must be specified when executing **sp_adddistributor** at both the Publisher and Distributor. [sp_changedistributor_password](../../relational-databases/system-stored-procedures/sp-changedistributor-password-transact-sql.md) can be used to change the Distributor password.  
  
> [!IMPORTANT]  
>  When possible, prompt users to enter security credentials at runtime. If you must store credentials in a script file, you must secure the file to prevent unauthorized access.  
  
 [ **@from_scripting=** ] *from_scripting*  
 [!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Remarks  
 **sp_adddistributor** is used in snapshot replication, transactional replication, and merge replication.  
  
## Example  
 [!code-sql[HowTo#AddDistPub](../../relational-databases/replication/codesnippet/tsql/sp-adddistributor-transa_1.sql)]  
  
## Permissions  
 Only members of the **sysadmin** fixed server role can execute **sp_adddistributor**.  
  
## See Also  
 [Configure Publishing and Distribution](../../relational-databases/replication/configure-publishing-and-distribution.md)   
 [sp_changedistributor_property &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-changedistributor-property-transact-sql.md)   
 [sp_dropdistributor &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dropdistributor-transact-sql.md)   
 [sp_helpdistributor &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helpdistributor-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)   
 [Configure Distribution](../../relational-databases/replication/configure-distribution.md)  
  
  
