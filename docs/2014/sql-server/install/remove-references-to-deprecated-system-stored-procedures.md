---
title: "Remove references to deprecated system stored procedures | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "undocumented system stored procedures [SQL Server]"
  - "system stored procedures [SQL Server]"
ms.assetid: 487d24fc-41d5-495e-843c-0ac974ec472f
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Remove references to deprecated system stored procedures
  Upgrade Advisor detected statements that reference undocumented system stored procedures and extended stored procedures that are no longer available in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Statements that reference these objects will fail. Do not use undocumented system objects or APIs as the functionality might change or be removed without notification in a future release.  
  
## Component  
 [!INCLUDE[ssDE](../../includes/ssde-md.md)]  
  
## Description  
  
### Documented System Stored Procedures  
 The following documented system stored procedures have been removed:  
  
-   sp_addalias  
  
-   sp_addgroup  
  
-   sp_changegroup  
  
-   sp_dropgroup  
  
-   sp_helpgroup  
  
### Undocumented System Stored Procedures  
 The following undocumented system stored procedures and extended stored procedures have been removed:  
  
-   sp_articlesynctranprocs  
  
-   sp_diskdefault  
  
-   sp_EventLog  
  
-   sp_GetMBCSCharLen  
  
-   sp_helplog  
  
-   sp_helpsql  
  
-   sp_IsMBCSLeadByte  
  
-   sp_lock2  
  
-   sp_MSget_current_activity  
  
-   sp_MSset_current_activity  
  
-   sp_MSobjessearch  
  
-   xp_enum_ActiveScriptEngines  
  
-   xp_eventlog  
  
-   xp_GetAdminGroupName  
  
-   xp_GetFileDetails  
  
-   xp_GetLocalSystemAccountName  
  
-   xp_IsNTAdmin  
  
-   xp_MSLocalSystem  
  
-   xp_MSnt2000  
  
-   xp_MSplatform  
  
-   xp_SetSecurity  
  
-   xp_varbintohexstr  
  
## Corrective Action  
  
### Documented System Stored Procedures  
 Modify your applications according to the following table.  
  
|Instead of|Do this|  
|----------------|-------------|  
|sp_addalias|Replace aliases with a combination of user accounts and database roles. For more information, see "CREATE USER (Transact-SQL)" and "CREATE ROLE (Transact-SQL)" in SQL Server Books Online. Remove aliases in upgraded databases by using sp_dropalias.|  
|sp_addgroupsp_changegroup<br /><br /> sp_dropgroup<br /><br /> sp_helpgroup|Use roles. For more information, see "Server-Level Roles" and "Database-Level Roles" in SQL Server Books Online.|  
  
### Undocmented System Stored Procedures  
 You can create CLR stored procedures with equivalent functionality to replace the undocumented system stored procedures. For more information, see the topic "CLR Stored Procedures" in SQL Server Books Online.  
  
## See Also  
 [Database Engine Upgrade Issues](../../../2014/sql-server/install/database-engine-upgrade-issues.md)   
 [SQL Server 2014 Upgrade Advisor &#91;new&#93;](sql-server-2014-upgrade-advisor.md)  
  
  
