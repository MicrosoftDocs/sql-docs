---
title: "sp_vupgrade_replication (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_vupgrade_replication_TSQL"
  - "sp_vupgrade_replication"
helpviewer_keywords: 
  - "sp_vupgrade_replication"
ms.assetid: d2c0ed66-07d1-4adc-82e5-a654376879bc
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_vupgrade_replication (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Activated by setup when upgrading a replication server. Upgrades schema and system data as needed to support replication at the current product level. Creates new replication system objects in system and user databases. This stored procedure is executed at the machine where the replication upgrade is to occur.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_vupgrade_replication [ [@login=] 'login' ]  
    [ , [ @password= ] 'password' ]  
    [ , [ @ver_old= ] 'old_version' ]  
    [ , [ @force_remove= ] 'force_removal' ]  
    [ , [ @security_mode= ] security_mode ]  
```  
  
## Arguments  
 [ **@login=**] **'**_login_**'**  
 Is the system administrator login to use when creating new system objects in the Distribution database. *login* is **sysname**, with a default of NULL. This parameter is not required if *security_mode* is set to **1**, which is Windows Authentication.  
  
> [!NOTE]  
>  This parameter is ignored when you are upgrading to [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] and later versions.  
  
 [ **@password=**] **'**_password_**'**  
 Is the system administrator password to use when creating new system objects in the Distribution database. *password* is **sysname**, with a default of **''** (empty string). This parameter is not required if *security_mode* is set to **1**, which is Windows Authentication.  
  
> [!NOTE]  
>  This parameter is ignored when you are upgrading to SQL [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] and later versions.  
  
 [ **@ver_old=**] **'**_old_version_**'**  
 [!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]  
  
 This stored procedure is deprecated and will be removed in a future release of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 [ **@force_remove=**] **'**_force_removal_**'**  
 [!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]  
  
 [ **@security_mode=**] **'**_security_mode_**'**  
 Is the login security mode to use when creating new system objects in the Distribution database. *security_mode* is **bit** with a default value of **0**. If **0**, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication will be used. If **1**, Windows Authentication will be used.  
  
> [!NOTE]  
>  This parameter is ignored when you are upgrading to [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] and later versions.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_vupgrade_replication** is used when upgrading all types of replication.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role can execute **sp_vupgrade_replication**.  
  
## See Also  
 [Replication Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/replication-stored-procedures-transact-sql.md)   
 [Validate Replicated Data](../../relational-databases/replication/validate-data-at-the-subscriber.md)  
  
  
