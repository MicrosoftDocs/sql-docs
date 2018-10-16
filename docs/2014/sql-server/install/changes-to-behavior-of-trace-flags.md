---
title: "Changes to behavior of trace flags | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "trace flags [SQL Server], behavior changes"
ms.assetid: d739df96-2659-4383-8e10-194657632526
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Changes to behavior of trace flags
  Global trace flags set by a session take effect in other sessions immediately. Some trace flags from [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] do not exist in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
## Component  
 [!INCLUDE[ssDE](../../includes/ssde-md.md)]  
  
## Description  
 We recommend that you disable all trace flags before you upgrade. Trace flags that modify the database availability or recovery modes might prevent the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)][!INCLUDE[ssDE](../../includes/ssde-md.md)] from successfully upgrading your instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. You can enable the trace flags after you verify that the trace flags are required and are still valid in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]. If you must re-enable trace flags, you should perform additional tests on your instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] supports global and session level trace flags. In [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)], trace flags can be specified as either local or global by using an additional argument (-1) in the DBCC TRACEON command. If this argument is not specified, the default value is local.  
  
 Also, in [!INCLUDE[ssVersion2000](../../includes/ssversion2000-md.md)], a trace flag set in session A does not automatically take effect in an already existing session B. Instead, this trace flag takes effect only after the first time any trace flag is set in session B. This behavior is nondeterministic in [!INCLUDE[ssVersion2000](../../includes/ssversion2000-md.md)] and is deterministic in [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] and later versions, where global trace flags set in session A are set immediately in other concurrent sessions.  
  
## See Also  
 [Database Engine Upgrade Issues](../../../2014/sql-server/install/database-engine-upgrade-issues.md)   
 [SQL Server 2014 Upgrade Advisor &#91;new&#93;](/sql/2014/sql-server/install/sql-server-2014-upgrade-advisor)  
  
  
