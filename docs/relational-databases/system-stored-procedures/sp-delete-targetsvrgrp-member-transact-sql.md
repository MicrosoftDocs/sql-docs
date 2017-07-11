---
title: "sp_delete_targetsvrgrp_member (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "sp_delete_targetsvrgrp_member_TSQL"
  - "sp_delete_targetsvrgrp_member"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_delete_targetsvrgrp_member"
ms.assetid: 178a38d9-9b19-4648-95d7-e1397110d14c
caps.latest.revision: 18
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# sp_delete_targetsvrgrp_member (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Removes a target server from a target server group.  
  
||  
|-|  
|**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ([!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [current version](http://go.microsoft.com/fwlink/p/?LinkId=299658)).|  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_delete_targetsvrgrp_member [ @group_name = ] 'group_name' , [ server_name = ] 'server_name'   
```  
  
## Arguments  
 [ **@group_name=** ] **'***group_name***'**  
 The name of the group. *group_name* is **sysname**, with no default.  
  
 [ **@server_name=** ] **'***server_name***'**  
 The name of the server to remove from the specified group. *server_name* is **nvarchar(30)**, with no default.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Result Sets  
 None  
  
## Permissions  
 To run this stored procedure, users must be granted the **sysadmin** fixed server role.  
  
## Examples  
 The following example removes the server `LONDON1` from the Servers Maintaining Customer Information group.  
  
```  
USE msdb ;  
GO  
  
EXEC sp_delete_targetsvrgrp_member   
    @group_name = N'Servers Maintaining Customer Information',  
    @server_name = N'LONDON1';  
GO  
```  
  
## See Also  
 [sp_add_targetsvrgrp_member &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-add-targetsvrgrp-member-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  