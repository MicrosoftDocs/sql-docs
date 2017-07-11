---
title: "CONTEXT_INFO  (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "CONTEXT_INFO_TSQL"
  - "CONTEXT_INFO"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "CONTEXT_INFO function"
  - "Multiple Active Result Sets"
  - "context information [SQL Server]"
  - "MARS [SQL Server]"
  - "session context information [SQL Server]"
ms.assetid: 571320f5-7228-4b0e-9d01-ab732d2d1eab
caps.latest.revision: 19
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# CONTEXT_INFO  (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx_md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Returns the **context_info** value that was set for the current session or batch by using the [SET CONTEXT_INFO](../../t-sql/statements/set-context-info-transact-sql.md) statement.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
CONTEXT_INFO()  
```  
  
## Return Value  
 The value of **context_info**.  
  
 If **context_info** was not set:  
  
-   In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] returns NULL.  
  
-   In [!INCLUDE[ssSDS](../../includes/sssds-md.md)] returns a unique session-specific GUID.  
  
## Remarks  
 Multiple active result sets (MARS) enables applications to run multiple batches, or requests, at the same time on the same connection. When one of the batches on a MARS connection runs SET CONTEXT_INFO, the new context value is returned by the CONTEXT_INFO function when it is run in the same batch as the SET statement. The new value is not returned by the CONTEXT_INFO function run in one or more of the other batches on the connection, unless they started after the batch that ran the SET statement completed.  
  
## Permissions  
 Requires no special permissions. The context information is also stored in the **sys.dm_exec_requests**, **sys.dm_exec_sessions**, and **sys.sysprocesses** system views, but querying the views directly requires SELECT and VIEW SERVER STATE permissions.  
  
## Examples  
 The following simple example sets the **context_info** value to `0x1256698456`, and then uses the `CONTEXT_INFO` function to retrieve the value.  
  
```  
SET CONTEXT_INFO 0x1256698456;  
GO  
SELECT CONTEXT_INFO();  
GO  
```  
  
## See Also  
 [SET CONTEXT_INFO &#40;Transact-SQL&#41;](../../t-sql/statements/set-context-info-transact-sql.md)  
  
  