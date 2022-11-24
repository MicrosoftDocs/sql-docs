---
description: "sp_changedistributor_password (Transact-SQL)"
title: "sp_changedistributor_password (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: "reference"
dev_langs: 
  - "TSQL"
f1_keywords: 
  - "sp_changedistributor_password"
  - "sp_changedistributor_password_TSQL"
helpviewer_keywords: 
  - "sp_changedistributor_password"
ms.assetid: 4a496e60-414a-4026-ba7a-3e89391d39b7
author: markingmyname
ms.author: maghan
---
# sp_changedistributor_password (Transact-SQL)
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

  Changes the password for a Distributor. This stored procedure is executed at the Distributor on any database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_changedistributor_password [ @password= ] 'password'   
```  
  
## Arguments  
`[ @password = ] 'password'`
 Is the new password. *password* is **sysname**, with no default. If the Distributor is local, the password of the **distributor_admin** system login is changed.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_changedistributor_password** is used in all types of replication.  
  
## Example  
 [!code-sql[HowTo#sp_changedistributor_password](../../relational-databases/replication/codesnippet/tsql/sp-changedistributor-pas_1.sql)]  
  
## Permissions  
 Only members of the **sysadmin** fixed server role can execute **sp_changedistributor_password**.  
  
## See Also  
 [View and Modify Replication Security Settings](../../relational-databases/replication/security/view-and-modify-replication-security-settings.md)   
 [Secure the Distributor](../../relational-databases/replication/security/secure-the-distributor.md)   
 [sp_adddistributor &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-adddistributor-transact-sql.md)   
 [Replication Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/replication-stored-procedures-transact-sql.md)  
  
  
