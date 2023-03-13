---
title: "sp_unregistercustomresolver (Transact-SQL)"
description: "sp_unregistercustomresolver (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_unregistercustomresolver_TSQL"
  - "sp_unregistercustomresolver"
helpviewer_keywords:
  - "sp_unregistercustomresolver"
dev_langs:
  - "TSQL"
---
# sp_unregistercustomresolver (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Unregisters a previously registered business logic module. Business logic can be in the form of either a COM component or a [!INCLUDE[msCoName](../../includes/msconame-md.md)] .NET Framework assembly. This stored procedure is executed on the Distributor where the custom business logic was registered.  
  
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
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
  
  
