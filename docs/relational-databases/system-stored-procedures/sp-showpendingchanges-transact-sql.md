---
title: "sp_showpendingchanges (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_showpendingchanges"
  - "sp_showpendingchanges_TSQL"
helpviewer_keywords: 
  - "sp_showpendingchanges"
ms.assetid: 8013a792-639d-4550-b262-e65d30f9d291
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_showpendingchanges (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns a result set showing the changes that are waiting to be replicated. This stored procedure is executed at the Publisher on the publication database and at the Subscriber on the subscription database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
> [!NOTE]  
>  This procedure provides an approximation of the number of changes and the rows that are involved in those changes. For example, the procedure retrieves information from either the Publisher or Subscriber, but not both at the same time. Information that is stored at the other node might result in a smaller set of changes to synchronize than the procedure estimates.  
  
## Syntax  
  
```  
  
sp_showpendingchanges [ [ @destination_server = ] 'destination_server' ]  
    [ , [ @publication = ] 'publication' ]  
    [ , [ @article = ] 'article']  
    [ , [ @show_rows = ] show_rows]  
```  
  
## Arguments  
 [ @destination_server**=** ] **'***destination_server***'**  
 Is the name of the server where the replicated changes are applied. *destination_server* is **sysname**, with default value of NULL.  
  
 [ @publication**=** ] **'***publication***'**  
 Is the name of the publication. *publication* is **sysname**, with a default value of NULL. When *publication* is specified, results are limited only to the specified publication.  
  
 [ @article **=** ] **'***article***'**  
 Is the name of the article. *article* is **sysname**, with a default value of NULL. When *article* is specified, results are limited only to the specified article.  
  
 [ @show_rows **=** ] *show_rows*  
 Specifies whether the result set contains more specific information about pending changes, with a default value of **0**. If a value of **1** is specified, the result set contains the columns is_delete and rowguid.  
  
## Result Set  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|destination_server|**sysname**|The name of the server to which the changes are being replicated.|  
|pub_name|**sysname**|The name of the publication.|  
|destination_db_name|**sysname**|The name of the database to which the changes are being replicated.|  
|is_dest_subscriber|**bit**|Indicates of the changes are being replicated to a Subscriber. A value of **1** indicates that the changes are being replicated to a Subscriber. **0** means that changes are being replicated to a Publisher.|  
|article_name|**sysname**|The name of the article for the table where changes originated.|  
|pending_deletes|**int**|The number of deletes waiting to be replicated.|  
|pending_ins_and_upd|**int**|The number of inserts and updates waiting to be replicated.|  
|is_delete|**bit**|Indicates whether the pending change is a delete. A value of **1** indicates that the change is a delete. Requires a value of **1** for @show_rows.|  
|rowguid|**uniqueidentifier**|The GUID that identifies the row that changed. Requires a value of **1** for @show_rows.|  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 sp_showpendingchanges is used in merge replication.  
  
 sp_showpendingchanges is used when troubleshooting merge replication.  
  
 The result of sp_showpendingchanges does not include rows in generation 0.  
  
 When an article specified for *article* does not belong to the publication specified for *publication,* a count of 0 is returned for pending_deletes and pending_ins_and_upd.  
  
## Permissions  
 Only members of the sysadmin fixed server role or db_owner fixed database role can execute sp_showpendingchanges.  
  
## See Also  
 [Replication Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/replication-stored-procedures-transact-sql.md)  
  
  
