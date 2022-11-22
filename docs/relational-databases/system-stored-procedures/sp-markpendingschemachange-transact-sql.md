---
description: "sp_markpendingschemachange (Transact-SQL)"
title: "sp_markpendingschemachange (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: "reference"
dev_langs: 
  - "TSQL"
f1_keywords: 
  - "sp_markpendingschemachange"
  - "sp_markpendingschemachange_TSQL"
helpviewer_keywords: 
  - "sp_markpendingschemachange"
ms.assetid: 01100309-7bef-4154-85bf-f18489577e37
author: markingmyname
ms.author: maghan
---
# sp_markpendingschemachange (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Used for supportability of merge publications by enabling an administrator to skip selected pending schema changes so that they will not be replicated. This stored procedure is executed at the Publisher on the publication database.  
  
> [!CAUTION]  
>  This stored procedure can cause schema changes not to be replicated. It should only be used to resolve issues after other methods, such as reinitialization, have already been tried or are too expensive in terms of performance.  
  
## Syntax  
  
```  
  
sp_markpendingschemachange [@publication = ] 'publication'  
    [ , [ @schemaversion = ] schemaversion ]  
    [ , [ @status = ] 'status' ]  
```  
  
## Arguments  
`[ @publication = ] 'publication'`
 Is the name of the publication. *publication* is **sysname**, with no default.  
  
`[ @schemaversion = ] schemaversion`
 Identifies a pending schema change. *schemaversion* is **int**, with a default value of **0**. Use [sp_enumeratependingschemachanges &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-enumeratependingschemachanges-transact-sql.md) to list the pending schema changes for the publication.  
  
`[ @status = ] 'status'`
 Is whether a pending schema change will be skipped. *status* is **nvarchar(10)** with a default value of **active**. If the value of *status* is **skipped**, then the selected schema change will not be replicated.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_markpendingschemachange** is used with merge replication.  
  
 **sp_markpendingschemachange** is a stored procedure intended for the supportability of merge replication and should be used only when other corrective actions, such as reinitialization, have failed to correct the situation or are too expensive in terms of performance.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute **sp_markpendingschemachange**.  
  
## See Also  
 [sysmergeschemachange &#40;Transact-SQL&#41;](../../relational-databases/system-tables/sysmergeschemachange-transact-sql.md)  
  
  
