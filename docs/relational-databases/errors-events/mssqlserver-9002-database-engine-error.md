---
title: "MSSQLSERVER_9002 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: supportability
ms.topic: "language-reference"
helpviewer_keywords: 
  - "9002 (Database Engine error)"
ms.assetid: 2e50841f-2b99-45f4-aec5-aa4add70cbeb
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_9002
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|9002|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|LOG_IS_FULL|  
|Message Text|The transaction log for database '%.*ls' is full. To find out why space in the log cannot be reused, see the log_reuse_wait_desc column in sys.databases.|  
  
## Explanation  
The database log is out of space. The **log_reuse_wait_desc** column in **sys.databases** describes why space in the log cannot be reused.  
  
## User Action  
Use **sys.databases** to determine why the log is full and then correct the condition. For more information, see "Troubleshooting a Full Transaction Log (Error 9002)" in SQL Server Books Online.  
  
## See Also  
[Troubleshoot a Full Transaction Log &#40;SQL Server Error 9002&#41;](~/relational-databases/logs/troubleshoot-a-full-transaction-log-sql-server-error-9002.md)  
[sys.databases &#40;Transact-SQL&#41;](~/relational-databases/system-catalog-views/sys-databases-transact-sql.md)  
  
