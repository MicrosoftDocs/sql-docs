---
title: "sp_enumcustomresolvers (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_enumcustomresolvers"
  - "sp_enumcustomresolvers_TSQL"
helpviewer_keywords: 
  - "sp_enumcustomresolvers"
ms.assetid: 81bd0d3a-48dc-42b1-b662-c630f61fc630
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_enumcustomresolvers (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns a list of all available business logic handlers and custom resolvers registered at the Distributor. This stored procedure is executed at the Publisher on any database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_enumcustomresolvers [ [ @distributor =] 'distributor']  
```  
  
## Arguments  
`[ @distributor = ] 'distributor'`
 Is the name of the Distributor where the custom resolver is located. *distributor* is **sysname**, with a default of NULL. *This parameter is deprecated and will be removed in a future release.*  
  
## Result Sets  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**article_resolver**|**nvarchar(255)**|Friendly name for the business logic handler or conflict resolver.|  
|**resolver_clsid**|**nvarchar(50)**|Class ID (CLSID) of the COM-based resolver. For a business logic handler, this column returns a zero CLSID value.|  
|**is_dotnet_assembly**|**bit**|Indicates whether the registration is for a business logic handler.<br /><br /> **0** = COM-based conflict resolver<br /><br /> **1** = business logic handler|  
|**dotnet_assembly_name**|**nvarchar(255)**|The name of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] .NET Framework assembly that implements the business logic handler.|  
|**dotnet_class_name**|**nvarchar(255)**|Is the name of the class that overrides <xref:Microsoft.SqlServer.Replication.BusinessLogicSupport.BusinessLogicModule> to implement the business logic handler.|  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_enumcustomresolvers** is used in merge replication.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role and the **db_owner** fixed database role can execute **sp_enumcustomresolvers**.  
  
## See Also  
 [Implement a Business Logic Handler for a Merge Article](../../relational-databases/replication/implement-a-business-logic-handler-for-a-merge-article.md)   
 [Implement a Custom Conflict Resolver for a Merge Article](../../relational-databases/replication/implement-a-custom-conflict-resolver-for-a-merge-article.md)   
 [sp_lookupcustomresolver &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-lookupcustomresolver-transact-sql.md)   
 [sp_unregistercustomresolver &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-unregistercustomresolver-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
