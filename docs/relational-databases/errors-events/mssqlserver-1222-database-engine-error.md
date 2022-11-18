---
description: "MSSQLSERVER_1222"
title: "MSSQLSERVER_1222 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "1222 (Database Engine error)"
ms.assetid: c5b1c2f4-f591-4cc1-aa17-204636a27f29
author: MashaMSFT
ms.author: mathoma
---
# MSSQLSERVER_1222
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|SQL Server|  
|Event ID|1222|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|LK_TIMEOUT|  
|Message Text|Lock request time out period exceeded.|  
  
## Explanation  
Another transaction held a lock on a required resource longer than this query could wait for it.  
  
## User Action  
Perform the following tasks to alleviate the problem:  
  
1.  Locate the transaction that is holding the lock on the required resource, if possible. Use **sys.dm_os_waiting_tasks** and **sys.dm_tran_locks** dynamic management views.  
  
2.  If the transaction is still holding the lock, terminate that transaction if appropriate.  
  
3.  Execute the query again.  

If this error occurs frequently change the lock time-out period or modify the offending transactions so that they hold the lock for less time.  
  
