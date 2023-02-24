---
description: "MSSQL_ENG004929"
title: "MSSQL_ENG004929 | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: reference
helpviewer_keywords: 
  - "MSSQL_ENG004929 error"
ms.assetid: 1d9b1d88-1fbf-4089-b392-687d3b0220ca
author: "MashaMSFT"
ms.author: "mathoma"
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016"
---
# MSSQL_ENG004929
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]
    
## Message Details  
  
|Attribute|Value|  
|-|-|  
|Product Name|SQL Server|  
|Event ID|4929|  
|Event Source|MSSQLSERVER|  
|Component|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|  
|Symbolic Name||  
|Message Text|Cannot alter the %S_MSG '%.*ls' because it is being published for replication.|  
  
## Explanation  
 This error typically occurs if you attempt to drop the primary key constraint on a table that is published for transactional replication. Transactional replication requires a primary key for each published table; therefore the constraint cannot be dropped.  
  
## User Action  
 To drop the constraint, first drop the article associated with the table. For more information, see [Add Articles to and Drop Articles from Existing Publications](../../relational-databases/replication/publish/add-articles-to-and-drop-articles-from-existing-publications.md). If this error occurs in a database that is not replicated, execute [sp_removedbreplication &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-removedbreplication-transact-sql.md) to ensure objects in the database are not marked as replicated.  
  
## See Also  
 [Errors and Events Reference &#40;Replication&#41;](../../relational-databases/replication/errors-and-events-reference-replication.md)  
  
  
