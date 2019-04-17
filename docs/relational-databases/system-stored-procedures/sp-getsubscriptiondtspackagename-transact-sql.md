---
title: "sp_getsubscriptiondtspackagename (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_getsubscriptiondtspackagename"
  - "sp_getsubscriptiondtspackagename_TSQL"
helpviewer_keywords: 
  - "sp_getsubscriptiondtspackagename"
ms.assetid: 606c40aa-2593-43af-9762-0f260bbb51f2
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_getsubscriptiondtspackagename (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns the name of the Data Transformation Services (DTS) package used to transform data before they are sent to a Subscriber. This stored procedure is executed at the Publisher on any database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_getsubscriptiondtspackagename [ @publication = ] 'publication'   
    [ , [ @subscriber = ] 'subscriber' ]  
```  
  
## Arguments  
`[ @publication = ] 'publication'`
 Is the name of the publication. **'***publication***'** is **sysname**, with no default.  
  
`[ @subscriber = ] 'subscriber'`
 Is the name of the Subscriber. *subscriber* is sysname, with a default of NULL.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Result Sets  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**new_package_name**|**sysname**|The name of the DTS package.|  
  
## Remarks  
 **sp_getsubscriptiondtspackagename** is used in snapshot replication and transactional replication.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role or the **db_owner** fixed database role can execute **sp_getsubscriptiondtspackagename**.  
  
  
