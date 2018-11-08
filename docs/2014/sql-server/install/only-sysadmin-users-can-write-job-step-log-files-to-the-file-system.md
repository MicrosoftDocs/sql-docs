---
title: "Only sysadmin users can write job step log files to the file system | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "job step log files [SQL Server Agent]"
  - "log files [SQL Server Agent]"
  - "writing job step log files"
ms.assetid: d26a7cef-1a60-4c95-b9df-f8b4fec59f9b
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Only sysadmin users can write job step log files to the file system
  [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] optionally writes a log for each job step.  
  
## Component  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent  
  
## Description  
 In [!INCLUDE[ssVersion2000](../../includes/ssversion2000-md.md)], [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent can write logs to the file system for jobs that are owned by members of the **sysadmin** fixed server role. If the job owner is not a member of the **sysadmin** role and if the proxy account is enabled, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent can write logs to the file system by using the credentials of the proxy account.  
  
 After you upgrade, jobs that are owned by users who are not members of the **sysadmin** fixed server role can no longer write logs to the file system. Instead, these users can select the option to write their logs to a table in the **msdb** database. Members of the **sysadmin** role can still write log files to the file system.  
  
## Corrective Action  
 After you upgrade, jobs that are owned by users who are not members of the **sysadmin** role will continue to run, but logs will not be created. To log job steps to a table, users who are not members of the **sysadmin** role must manually update their jobs.  
  
 For more information, see the topics "Creating Jobs," "Creating Job Steps," and "Handling Multiple Job Steps" in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.  
  
## See Also  
 [SQL Server Agent Upgrade Issues](../../../2014/sql-server/install/sql-server-agent-upgrade-issues.md)  
  
  
