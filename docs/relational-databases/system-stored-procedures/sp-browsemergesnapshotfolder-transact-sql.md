---
description: "sp_browsemergesnapshotfolder (Transact-SQL)"
title: "sp_browsemergesnapshotfolder (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: "reference"
dev_langs: 
  - "TSQL"
f1_keywords: 
  - "sp_browsemergesnapshotfolder"
  - "sp_browsemergesnapshotfolder_TSQL"
helpviewer_keywords: 
  - "sp_browsemergesnapshotfolder"
ms.assetid: e248642f-5fea-4ed7-be1a-36ff75abcfde
author: markingmyname
ms.author: maghan
---
# sp_browsemergesnapshotfolder (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Returns the complete path for the latest snapshot generated for a merge publication. This stored procedure is executed at the Publisher on the publication database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_browsemergesnapshotfolder [@publication= ] 'publication'  
```  
  
## Arguments  
`[ @publication = ] 'publication'`
 Is the name of the publication. *publication* is **sysname**, with no default.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Result Sets  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**snapshot_folder**|**nvarchar(2000)**|Full path to the snapshot directory.|  
  
## Remarks  
 **sp_browsemergesnapshotfolder** is used in merge replication.  
  
 If the publication is set up to generate snapshot files in both the Publisher working directory and Publisher snapshot folder, the result set contains two rows: the first row contains the publication snapshot folder, and the second row contains the publisher working directory.  
  
 **sp_browsemergesnapshotfolder** is useful for determining the directory where the merge snapshot files are generated. This folder/path and its contents can then be copied to removable media, and the snapshot used to synchronize a subscription from an alternate snapshot location.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute **sp_browsemergesnapshotfolder**.  
  
## See Also  
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
