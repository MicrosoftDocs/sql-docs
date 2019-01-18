---
title: "sp_registercustomresolver (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_registercustomresolver"
  - "sp_registercustomresolver_TSQL"
helpviewer_keywords: 
  - "sp_registercustomresolver"
ms.assetid: 6d2b0472-0e1f-4005-833c-735d1940fe93
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_registercustomresolver (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Registers a business logic handler or a COM-based custom resolver that can be invoked during the merge replication synchronization process. This stored procedure is executed at the Distributor.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_registercustomresolver [ @article_resolver = ] 'article_resolver'   
    [ , [ @resolver_clsid = ] 'resolver_clsid' ]  
    [ , [ @is_dotnet_assembly = ] 'is_dotnet_assembly' ]  
    [ , [ @dotnet_assembly_name = ] 'dotnet_assembly_name' ]  
    [ , [ @dotnet_class_name = ] 'dotnet_class_name' ]  
```  
  
## Arguments  
 [ **@article_resolver =** ] **'***article_resolver***'**  
 Specifies the friendly name for the custom business logic being registered. *article_resolver* is **nvarchar(255)**, with no default.  
  
 [ **@resolver_clsid=** ] **'***resolver_clsid***'**  
 Specifies the CLSID value of the COM object that being registered. Custom business logic *resolver_clsid* is **nvarchar(50)**, with a default of NULL. This parameter must be set to a valid CLSID or set to NULL when registering a business logic handler assembly.  
  
 [ **@is_dotnet_assembly=** ] **'***is_dotnet_assembly***'**  
 Specifies the type of custom business logic that is being registered. *is_dotnet_assembly* is **nvarchar(50)**, with a default of FALSE. **true** indicates that the custom business logic being registered is a business logic handler Assembly; **false** indicates that it is a COM component.  
  
 [ **@dotnet_assembly_name=** ] **'***dotnet_assembly_name***'**  
 Is the name of the assembly that implements the business logic handler. *dotnet_assembly_name* is **nvarchar(255)**, with a default value of NULL. You must specify the full path to the assembly if it is not deployed in the same directory as the Merge Agent executable, in the same directory as the application that synchronously starts the Merge Agent, or in the global assembly cache (GAC).  
  
 [ **@dotnet_class_name=** ] **'***dotnet_class_name***'**  
 Is the name of the class that overrides <xref:Microsoft.SqlServer.Replication.BusinessLogicSupport.BusinessLogicModule> to implement the business logic handler. The name should be specified in the form **Namespace.Classname**. *dotnet_class_name* is **nvarchar(255)**, with a default value of NULL.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_registercustomresolver** is used in merge replication.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute **sp_registercustomresolver**.  
  
## See Also  
 [Implement a Business Logic Handler for a Merge Article](../../relational-databases/replication/implement-a-business-logic-handler-for-a-merge-article.md)   
 [Implement a Custom Conflict Resolver for a Merge Article](../../relational-databases/replication/implement-a-custom-conflict-resolver-for-a-merge-article.md)   
 [sp_lookupcustomresolver &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-lookupcustomresolver-transact-sql.md)   
 [sp_unregistercustomresolver &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-unregistercustomresolver-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
