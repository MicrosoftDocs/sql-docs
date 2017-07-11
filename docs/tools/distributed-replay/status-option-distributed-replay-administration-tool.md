---
title: "Status Option (Distributed Replay Administration Tool) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: ea89386e-1598-4412-8b37-680d14b2a5b6
caps.latest.revision: 17
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# Status Option (Distributed Replay Administration Tool)
  The [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Distributed Replay administration tool, **DReplay.exe**, is a command-line tool that you can use to communicate with the distributed replay controller. This topic describes the **status** command-line option and corresponding syntax.  
  
 The **status** option queries the controller and displays the current status.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") For more information about the syntax conventions that are used with the administration tool syntax, see [Transact-SQL Syntax Conventions &#40;Transact-SQL&#41;](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md).  
  
## Syntax  
  
```  
  
dreplay status [-m controller] [-f status_interval]  
```  
  
#### Parameters  
 **-m** *controller*  
 Specifies the computer name of the controller. You can use "`localhost`" or "`.`" to refer to the local computer.  
  
 If the **-m** parameter is not specified, the local computer is used.  
  
 **-f** *status_interval*  
 Specifies the frequency (in seconds) at which to display the status.  
  
 If the **-f** parameter is not specified, the default interval is 30 seconds.  
  
## Examples  
 In the following example, the current status is displayed every 60 seconds. The value `localhost` indicates that the controller service is running on the same computer as the administration tool.  
  
```  
dreplay status –m localhost -f 60  
```  
  
## Permissions  
 You must run the administration tool as an interactive user, as either a local user or a domain user account. To use a local user account, the administration tool and controller must be running on the same computer.  
  
 For more information, see [Distributed Replay Security](../../tools/distributed-replay/distributed-replay-security.md).  
  
## See Also  
 [SQL Server Distributed Replay](../../tools/distributed-replay/sql-server-distributed-replay.md)   
 [Transact-SQL Debugger](../../relational-databases/scripting/transact-sql-debugger.md)  
  
  