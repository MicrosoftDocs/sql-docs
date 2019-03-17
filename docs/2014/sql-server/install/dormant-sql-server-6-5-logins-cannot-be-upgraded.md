---
title: "Dormant SQL Server 6.5 logins cannot be upgraded | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "passwords [SQL Server], dormant logins"
  - "dormant logins"
  - "logins [SQL Server], upgrading dormant logins"
  - "identifying dormant logins"
  - "password hashes [SQL Server]"
ms.assetid: ebe18a74-0375-4df4-b894-239f8fdabb64
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Dormant SQL Server 6.5 logins cannot be upgraded
  Upgrade Advisor detected a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login whose password cannot be directly upgraded to [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
 To enable this login, you must reset its password. You can reset the password by using ALTER LOGIN.  
  
```  
ALTER LOGIN <login name> WITH PASSWORD = '<new password>' MUST_CHANGE  
```  
  
 The new password will be validated against your system's password complexity policy, unless policy checking is disabled. We recommend that you use complex passwords and not disable policy checking. The MUST_CHANGE option forces the user to select a new password. This is not required, but it is recommended.  
  
 You can identify dormant logins by using the following query:  
  
```  
SELECT * FROM sysxlogins WHERE (xstatus & 2048) = 2048;  
GO  
```  
  
## See Also  
 [Database Engine Upgrade Issues](../../../2014/sql-server/install/database-engine-upgrade-issues.md)   
 [SQL Server 2014 Upgrade Advisor &#91;new&#93;](/sql/2014/sql-server/install/sql-server-2014-upgrade-advisor)  
  
  
