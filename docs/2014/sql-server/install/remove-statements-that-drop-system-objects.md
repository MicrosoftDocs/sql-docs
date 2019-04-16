---
title: "Remove statements that drop system objects | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "drop system objects [SQL Server]"
ms.assetid: cdfc3c50-c801-4039-a4bf-b35f876f1c61
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Remove statements that drop system objects
  Upgrade Advisor detected statements that drop system objects. System objects, including extended stored procedures, are deployed in the read-only **resource** database (mssqlsystemresource) and cannot be dropped. Modify your applications to either revoke or deny EXECUTE permission on system objects.  
  
## Component  
 [!INCLUDE[ssDE](../../includes/ssde-md.md)]  
  
## Description  
 Statements such as DROP TABLE, DROP PROCEDURE, and **sp_dropextendedproc** cannot be used to remove system objects, because these objects are deployed in the read-only **resource** database.  
  
## Corrective Action  
 Remove all statements that try to drop system objects from your applications. Modify your applications to either revoke or deny EXECUTE permission on system objects. Alternatively, you can use the Surface Area Configuration (SAC) tool to disable some of these objects. For example, the **xp_cmdshell** extended stored procedure can be disabled or enabled using the SAC tool.  
  
## See Also  
 [Database Engine Upgrade Issues](../../../2014/sql-server/install/database-engine-upgrade-issues.md)   
 [SQL Server 2014 Upgrade Advisor &#91;new&#93;](sql-server-2014-upgrade-advisor.md)  
  
  
