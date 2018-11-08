---
title: "Upgrade all target servers before upgrading the master server | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "TSX [SQL Server Agent]"
  - "target servers [SQL Server Agent]"
  - "MSX [SQL Server Agent]"
  - "master servers [SQL Server Agent]"
ms.assetid: 2c231793-3878-4a5e-a425-1fa0d787ba84
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Upgrade all target servers before upgrading the master server
  Before you upgrade the master server, upgrade all computers that are running [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that are configured as target servers.  
  
## Component  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent  
  
## Description  
 To automate administration in multiserver environments, you must have at least one master server and at least one target server. A master server distributes jobs to, and receives events from, target servers. A master server also stores the central copy of job definitions for jobs that are run on target servers.  
  
 If the current server is configured as a master server, upgrade all of your target servers first before you upgrade the master server.  
  
## Corrective Action  
 If you cannot upgrade all target servers before you upgrade the master server, you must defect all target servers and reenlist them after you upgrade.  
  
 For more information, see the topics "Automating Administration across an Enterprise," "How to: Defect a Target Server from a Master Server," and "How to: Enlist a Target Server to a Master" in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.  
  
## See Also  
 [SQL Server Agent Upgrade Issues](../../../2014/sql-server/install/sql-server-agent-upgrade-issues.md)   
 [SQL Server Agent Upgrade Issues](../../../2014/sql-server/install/sql-server-agent-upgrade-issues.md)  
  
  
