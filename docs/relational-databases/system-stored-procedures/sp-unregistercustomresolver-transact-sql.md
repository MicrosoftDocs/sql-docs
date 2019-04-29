---
title: "sp_unregistercustomresolver (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_unregistercustomresolver_TSQL"
  - "sp_unregistercustomresolver"
helpviewer_keywords: 
  - "sp_unregistercustomresolver"
ms.assetid: 08bd20c8-c6be-4be2-be9f-2b5e1d7bee43
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_unregistercustomresolver (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Unregisters a previously registered business logic module. Business logic can be in the form of either a COM component or a [!INCLUDE[msCoName](../../includes/msconame-md.md)] .NET Framework assembly. This stored procedure is executed on the Distributor where the custom business logic was registered.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_unregistercustomresolver [ @article_resolver = ] 'article_resolver'   
```  
  
## Arguments  
`[ @article_resolver = ] 'article_resolver'`
 Specifies the name of the custom business logic being unregistered. *article_resolver* is **nvarchar(255)**, with no default. If the business logic being removed is a COM component, this parameter is the friendly name of the component. If the business logic is a .NET Framework assembly, this parameter is the name of the assembly.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_unregistercustomresolver** is used in merge replication.  
  
 Use [sp_enumcustomresolvers](../../relational-databases/system-stored-procedures/sp-enumcustomresolvers-transact-sql.md) at any server in the replication topology to return the list of registered custom business logic modules or COM resolvers available to the topology.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute **sp_unregistercustomresolver**.  
  
## See Also  
 [sp_lookupcustomresolver &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-lookupcustomresolver-transact-sql.md)   
 [sp_registercustomresolver &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-registercustomresolver-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
