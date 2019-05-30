---
title: "sp_helpmergedeleteconflictrows (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_helpmergedeleteconflictrows"
  - "sp_helpmergedeleteconflictrows_TSQL"
helpviewer_keywords: 
  - "sp_helpmergedeleteconflictrows"
ms.assetid: 222be651-5690-4341-9dfb-f9ec1d80c970
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_helpmergedeleteconflictrows (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns information on data rows that lost delete conflicts. This stored procedure is executed at the Publisher on the publication database or at the Subscriber on the subscription database when decentralized conflict logging is used.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_helpmergedeleteconflictrows [ [ @publication = ] 'publication']  
    [ , [ @source_object = ] 'source_object']  
    [ , [ @publisher = ] 'publisher'  
    [ , [ @publisher_db = ] 'publsher_db'  
```  
  
## Arguments  
`[ @publication = ] 'publication'`
 Is the name of the publication. *publication* is **sysname**, with a default of **%**. If the publication is specified, all conflicts qualified by the publication are returned.  
  
`[ @source_object = ] 'source_object'`
 Is the name of the source object. *source_object* is **nvarchar(386)**, with a default of NULL.  
  
`[ @publisher = ] 'publisher'`
 Is the name of the Publisher.*publisher* is **sysname**, with a default of NULL.  
  
`[ @publisher_db = ] 'publisher_db'`
 Is the name of the publisher database.*publisher_db* is **sysname**, with a default of NULL.  
  
## Result Sets  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**source_object**|**nvarchar(386)**|Source object for the delete conflict.|  
|**rowguid**|**uniqueidentifier**|Row identifier for the delete conflict.|  
|**conflict_type**|**int**|Code indicating type of conflict:<br /><br /> **1** = UpdateConflict: Conflict is detected at the row level.<br /><br /> **2** = ColumnUpdateConflict: Conflict detected at the column level.<br /><br /> **3** = UpdateDeleteWinsConflict: Delete wins the conflict.<br /><br /> **4** = UpdateWinsDeleteConflict: The deleted rowguid that loses the conflict is recorded in this table.<br /><br /> **5** = UploadInsertFailed: Insert from Subscriber could not be applied at the Publisher.<br /><br /> **6** = DownloadInsertFailed: Insert from Publisher could not be applied at the Subscriber.<br /><br /> **7** = UploadDeleteFailed: Delete at Subscriber could not be uploaded to the Publisher.<br /><br /> **8** = DownloadDeleteFailed: Delete at Publisher could not be downloaded to the Subscriber.<br /><br /> **9** = UploadUpdateFailed: Update at Subscriber could not be applied at the Publisher.<br /><br /> **10** = DownloadUpdateFailed: Update at Publisher could not be applied to the Subscriber.|  
|**reason_code**|**Int**|Error code that can be context-sensitive.|  
|**reason_text**|**varchar(720)**|Error description that can be context-sensitive.|  
|**origin_datasource**|**varchar(255)**|Origin of the conflict.|  
|**pubid**|**uniqueidentifier**|Publication identifier.|  
|**MSrepl_create_time**|**datetime**|Time the conflict information was added.|  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_helpmergedeleteconflictrows** is used in merge replication.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role and the **db_owner** fixed database role can execute **sp_helpmergedeleteconflictrows**.  
  
## See Also  
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
