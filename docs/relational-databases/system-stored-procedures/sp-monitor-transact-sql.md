---
title: "sp_monitor (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_monitor_TSQL"
  - "sp_monitor"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_monitor"
ms.assetid: cb628496-2f9b-40e4-b018-d0831c4cb018
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_monitor (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Displays statistics about [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_monitor  
```  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Result Sets  
  
|Column name|Description|  
|-----------------|-----------------|  
|**last_run**|Time **sp_monitor** was last run.|  
|**current_run**|Time **sp_monitor** is being run.|  
|**seconds**|Number of elapsed seconds since **sp_monitor** was run.|  
|**cpu_busy**|Number of seconds that the server computer's CPU has been doing [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] work.|  
|**io_busy**|Number of seconds that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] has spent doing input and output operations.|  
|**idle**|Number of seconds that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] has been idle.|  
|**packets_received**|Number of input packets read by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
|**packets_sent**|Number of output packets written by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
|**packet_errors**|Number of errors encountered by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] while reading and writing packets.|  
|**total_read**|Number of reads by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
|**total_write**|Number of writes by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
|**total_errors**|Number of errors encountered by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] while reading and writing.|  
|**connections**|Number of logins or attempted logins to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
  
## Remarks  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] keeps track, through a series of functions, of how much work it has done. Executing **sp_monitor** displays the current values returned by these functions and shows how much they have changed since the last time the procedure was run.  
  
 For each column, the statistic is printed in the form *number*(*number*)-*number*% or *number*(*number*). The first *number* refers to the number of seconds (for **cpu_busy**, **io_busy**, and **idle**) or the total number (for the other variables) since [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] was restarted. The *number* in parentheses refers to the number of seconds or total number since the last time **sp_monitor** was run. The percentage is the percentage of time since **sp_monitor** was last run. For example, if the report shows **cpu_busy** as 4250(215)-68%, the CPU has been busy 4250 seconds since [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] was last started up, 215 seconds since **sp_monitor** was last run, and 68 percent of the total time since **sp_monitor** was last run.  
  
## Permissions  
 Requires membership in the **sysadmin** fixed server role.  
  
## Examples  
 The following example reports information about how busy [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] has been.  
  
```  
USE master  
EXEC sp_monitor  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
||||  
|-|-|-|  
|**last_run**|**current_run**|**seconds**|  
|Mar 29 1998 11:55AM|Apr 4 1998 2:22 PM|561|  
  
||||  
|-|-|-|  
|**cpu_busy**|**io_busy**|**idle**|  
|190(0)-0%|187(0)-0%|148(556)-99%|  
  
||||  
|-|-|-|  
|**packets_received**|**packets_sent**|**packet_errors**|  
|16(1)|20(2)|0(0)|  
  
|||||  
|-|-|-|-|  
|**total_read**|**total_write**|**total_errors**|**connections**|  
|141(0)|54920(127)|0(0)|4(0)|  
  
## See Also  
 [sp_who &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-who-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
