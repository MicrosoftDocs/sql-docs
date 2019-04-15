---
title: "Rename logins matching fixed server role names | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "user-defined login names [SQL Server]"
  - "fixed server roles [SQL Server]"
  - "renamed logins [SQL Server]"
  - "logins [SQL Server], names"
ms.assetid: 10a1d77c-3153-474f-a6a0-969556794467
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Rename logins matching fixed server role names
  Upgrade Advisor detected one or more user-defined login names that match the names of fixed server roles. Fixed server role names are reserved. Rename the login before you upgrade.  
  
## Component  
 [!INCLUDE[ssDE](../../includes/ssde-md.md)]  
  
## Description  
 The following fixed server role names are reserved and cannot be used as user-defined login names.  
  
-   **sysadmin**  
  
-   **serveradmin**  
  
-   **setupadmin**  
  
-   **securityadmin**  
  
-   **processadmin**  
  
-   **dbcreator**  
  
-   **diskadmin**  
  
-   **bulkadmin**  
  
## Corrective Action  
 Before upgrading, perform the following steps:  
  
1.  Record the security identifiers (SIDs) of the logins by executing the following statement.  
  
    ```  
    SELECT name, sid   
    FROM master.dbo.syslogins   
    WHERE name IN('sysadmin', 'serveradmin','setupadmin', 'securityadmin','processadmin', 'dbcreator','diskadmin','bulkadmin')  
    ```  
  
2.  Drop the logins.  
  
3.  Use the **sp_addlogin** system procedure to create new logins. Specify the SID returned in step 1 in the **@sid** parameter for each corresponding login.  
  
## See Also  
 [Database Engine Upgrade Issues](../../../2014/sql-server/install/database-engine-upgrade-issues.md)   
 [SQL Server 2014 Upgrade Advisor &#91;new&#93;](sql-server-2014-upgrade-advisor.md)  
  
  
