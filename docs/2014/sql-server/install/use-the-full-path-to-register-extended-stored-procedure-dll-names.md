---
title: "Use the full path to register extended stored procedure DLL names | Microsoft Docs"
ms.custom: ""
ms.date: "04/27/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "registering DLL names"
  - "extended stored procedures [SQL Server], registering"
  - "DLL names [SQL Server]"
  - "full path DLL name registration [SQL Server]"
ms.assetid: f648d57c-af32-4c71-9882-47b6766f3c2b
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Use the full path to register extended stored procedure DLL names
  Extended stored procedures that were previously registered without the full path for the DLL name may not work in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
## Component  
 [!INCLUDE[ssDE](../../includes/ssde-md.md)]  
  
## Description  
 Extended stored procedures that were previously registered without the full path for the DLL name may not work after you upgrade. This is because the old BINN directory is not added to the new path during the upgrade process. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] may not be able to locate the extended stored procedures.  
  
## Corrective Action  
 Before you upgrade, follow these steps for each extended stored procedure that was not registered with a full path name:  
  
1.  Run sp_dropextendedproc to remove the extended stored procedure.  
  
2.  Run sp_addextendedproc to register the extended stored procedure with the full path name.  
  
## See Also  
 [Database Engine Upgrade Issues](../../../2014/sql-server/install/database-engine-upgrade-issues.md)   
 [SQL Server 2014 Upgrade Advisor &#91;new&#93;](/sql/2014/sql-server/install/sql-server-2014-upgrade-advisor)  
  
  
