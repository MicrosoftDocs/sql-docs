---
title: "Dummy merge article update (Replication SP)"
description: Use Transact-SQL replication stored procedures to perform a dummy update of a merge article used in merge replication.
ms.custom: seo-lt-2019
ms.date: "03/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: conceptual
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_mergedummyupdate"
  - "dummy updates [SQL Server replication]"
ms.assetid: 2f339210-4d85-4843-bd94-e86f7100d3ef
author: "MashaMSFT"
ms.author: "mathoma"
---
# Perform a Dummy Update for a Merge Article (Replication Transact-SQL Programming)
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
  Merge replication uses triggers as part of the replication process; when an update is made to published table, an update trigger fires. In some cases, data can be updated without the trigger firing, such as during the WRITETEXT and UPDATETEXT operations. In these cases, you need to add a dummy UPDATE statement explicitly to replicate the change. You can add a dummy UPDATE statement using replication stored procedures.  
  
### To add a dummy UPDATE statement  
  
1.  Execute the operation (for example, UPDATETEXT) on a row in a merge published table  that requires a dummy update.  
  
2.  At the server (Publisher or Subscriber) on the database where the change was made, execute [sp_mergedummyupdate &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-mergedummyupdate-transact-sql.md). Specify the table on which the change was made for `@source_object`, and the unique identifier of the changed row for `@rowguid`.  
  
3.  Synchronize the subscription to replicate the changed row.  
  
  
