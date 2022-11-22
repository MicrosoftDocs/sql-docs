---
description: "MSSQLSERVER_9002"
title: "MSSQLSERVER_9002 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "9002 (Database Engine error)"
ms.assetid: 2e50841f-2b99-45f4-aec5-aa4add70cbeb
author: MashaMSFT
ms.author: mathoma
---
# MSSQLSERVER_9002
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|SQL Server|  
|Event ID|9002|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|LOG_IS_FULL|  
|Message Text|The transaction log for database '%.*ls' is full. To find out why space in the log cannot be reused, see the log_reuse_wait_desc column in sys.databases.|  
  
## Explanation  

The database log is out of space. These are reasons why the log can run out of space

- Log not being truncated
- Disk volume is full
- Log size is set to a fixed maximum value or autogrow is disabled)
- Replication or availability group synchronization that is unable to complete

The **log_reuse_wait_desc** column in **[sys.databases &#40;Transact-SQL&#41;](~/relational-databases/system-catalog-views/sys-databases-transact-sql.md)** describes why space in the log cannot be reused. 
  
## User Action  

A very common solution to this problem is to ensure transaction log backups are performed for your database which will ensure the log is truncated. If no recent transaction log history is indicated for the database with a full transaction log, the solution to the problem is straightforward: resume regular transaction log backups of the database. 

For detailed information on resolving this error, see [Troubleshoot a Full Transaction Log &#40;SQL Server Error 9002&#41;](~/relational-databases/logs/troubleshoot-a-full-transaction-log-sql-server-error-9002.md).
  
