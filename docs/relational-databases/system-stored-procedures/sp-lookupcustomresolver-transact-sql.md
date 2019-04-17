---
title: "sp_lookupcustomresolver (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_lookupcustomresolver_TSQL"
  - "sp_lookupcustomresolver"
helpviewer_keywords: 
  - "sp_lookupcustomresolver"
ms.assetid: 356a7b8a-ae53-4fb5-86ee-fcfddbf23ddd
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_lookupcustomresolver (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns the information on a business logic handler or the class identifier (CLSID) value of a COM-based custom resolver component that is registered at the Distributor. This stored procedure is executed at the Publisher on the publication database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_lookupcustomresolver [ @article_resolver = ] 'article_resolver'   
    [, [ @resolver_clsid = ] 'resolver_clsid' OUTPUT ]  
    [ , [ @is_dotnet_assembly = ] is_dotnet_assembly OUTPUT ]  
    [ , [ @dotnet_assembly_name = ] 'dotnet_assembly_name' OUTPUT ]  
    [ , [ @dotnet_class_name = ] 'dotnet_class_name' OUTPUT ]  
    [ , [ @publisher = ] 'publisher' ]  
```  
  
## Arguments  
`[ @article_resolver = ] 'article_resolver'`
 Specifies the name of the custom business logic being unregistered. *article_resolver* is **nvarchar(255)**, with no default. If the business logic being removed is a COM component, then this parameter is the friendly name of the component. If the business logic is a [!INCLUDE[msCoName](../../includes/msconame-md.md)] .NET Framework assembly, then this parameter is the name of the assembly.  
  
`[ @resolver_clsid = ] 'resolver_clsid' OUTPUT`
 Is the CLSID value of the COM object associated with the name of the custom business logic specified in the *article_resolver* parameter. *resolver_clsid* is **nvarchar(50)**, with a default of NULL.  
  
`[ @is_dotnet_assembly = ] 'is_dotnet_assembly' OUTPUT`
 Specifies the type of custom business logic that is being registered. *is_dotnet_assembly* is **bit**, with a default of 0. **1** indicates that the custom business logic being registered is a business logic handler Assembly; **0** indicates that it is a COM component.  
  
`[ @dotnet_assembly_name = ] 'dotnet_assembly_name' OUTPUT`
 Is the name of the assembly that implements the business logic handler. *dotnet_assembly_name* is **nvarchar(255)**, with a default value of NULL.  
  
`[ @dotnet_class_name = ] 'dotnet_class_name' OUTPUT`
 Is the name of the class that overrides <xref:Microsoft.SqlServer.Replication.BusinessLogicSupport.BusinessLogicModule> to implement the business logic handler. *dotnet_class_name* is **nvarchar(255)**, with a default value of NULL.  
  
`[ @publisher = ] 'publisher'`
 Is the name of the Publisher. *publisher* is **sysname**, with a default value of NULL. Use this parameter when the stored procedure is not called from the Publisher. If not specified, it is assumed that the local server is the Publisher.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_lookupcustomresolver** is used in merge replication.  
  
 **sp_lookupcustomresolver** returns a NULL value for *resolver_clsid* when the component is not registered at the Distribution and a value of "00000000-0000-0000-0000-000000000000" when the registration belongs to a .NET Framework assembly registered as a business logic handler.  
  
 **sp_lookupcustomresolver** is called by [sp_addmergearticle](../../relational-databases/system-stored-procedures/sp-addmergearticle-transact-sql.md) and [sp_changemergearticle](../../relational-databases/system-stored-procedures/sp-changemergearticle-transact-sql.md) to validate the specified *article_resolver*.  
  
## Permissions  
 Only members of the **db_owner** fixed database role on the publication database can execute **sp_lookupcustomresolver**.  
  
## See Also  
 [Advanced Merge Replication Conflict Detection and Resolution](../../relational-databases/replication/merge/advanced-merge-replication-conflict-detection-and-resolution.md)   
 [Execute Business Logic During Merge Synchronization](../../relational-databases/replication/merge/execute-business-logic-during-merge-synchronization.md)   
 [Implement a Business Logic Handler for a Merge Article](../../relational-databases/replication/implement-a-business-logic-handler-for-a-merge-article.md)   
 [Specify a Merge Article Resolver](../../relational-databases/replication/publish/specify-a-merge-article-resolver.md)   
 [sp_registercustomresolver &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-registercustomresolver-transact-sql.md)   
 [sp_unregistercustomresolver &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-unregistercustomresolver-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
