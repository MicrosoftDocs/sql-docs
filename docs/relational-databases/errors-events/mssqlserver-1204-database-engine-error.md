---
title: "MSSQLSERVER_1204 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: supportability
ms.topic: "language-reference"
helpviewer_keywords: 
  - "1204 (Database Engine error)"
ms.assetid: de6ece78-79de-484d-9224-ca0f7645815f
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_1204
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|1204|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|LK_OUTOF|  
|Message Text|The instance of the SQL Server Database Engine cannot obtain a LOCK resource at this time. Rerun your statement when there are fewer active users. Ask the database administrator to check the lock and memory configuration for this instance, or to check for long-running transactions.|  
  
## Explanation  
[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] cannot obtain a lock resource. This can be caused by either of the following reasons:  
  
-   [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] cannot allocate more memory from the operating system, either because other processes are using it, or because the server is operating with the **max server memory** option configured.  
  
-   The lock manager will not use more than 60 percent of the memory available to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
## User Action  
If you suspect that SQL Server cannot allocate sufficient memory, try the following:  
  
-   If applications besides SQL Server are consuming resources, try stopping these applications or consider running them on a separate server. This will remove release memory from other processes for SQL Server.  
  
-   If you have configured max server memory, increase max server memory setting.  
  
If you suspect that the lock manager has used the maximum amount of available memory identify the transaction that is holding the most locks and terminate it. The following script will identify the transaction with the most locks:  
  
```  
SELECT request_session_id, COUNT (*) num_locks  
FROM sys.dm_tran_locks  
GROUP BY request_session_id   
ORDER BY count (*) DESC  
```  
  
Take the highest session id, and terminate it using the KILL command.  
  
