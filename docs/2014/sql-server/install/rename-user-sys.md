---
title: "Rename user sys | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "sys user names [SQL Server]"
ms.assetid: d622d646-83e4-4b6f-9a21-77b301af04b5
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Rename user sys
  Upgrade Advisor detected the user name **sys** in a database. This name is reserved. Rename the user before you upgrade. If the user is not renamed, the database will be in a suspect state after the upgrade process and will be unavailable until the database is brought online.  
  
## Component  
 [!INCLUDE[ssDE](../../includes/ssde-md.md)]  
  
## Description  
 User **sys** is reserved.  
  
## Corrective Action  
  
### Before Upgrade Procedure  
 Before you upgrade, in each database that contains user **sys**, do the following:  
  
1.  Create a new user.  
  
2.  Use the following statements to display all permissions that are granted by user **sys** and granted to user **sys**.  
  
    ```  
    -- Return permissions granted by user sys.  
    SELECT * FROM sysprotects WHERE grantor = USER_ID('sys')  
    -- Return permissions granted to user sys.  
    SELECT * FROM sysprotects WHERE uid = USER_ID('sys')  
    ```  
  
3.  To transfer ownership of all objects owned by **sys** to the new user, use **sp_changeobjectowner**.  
  
4.  Drop user **sys**.  
  
5.  To restore the original permissions captured in step 2, use the AS *new_user* clause of the GRANT statement.  
  
6.  Modify scripts to reference the new user. For example, scripts that contain statements such as `SELECT * FROM sys.my`_`table` must be changed to `SELECT * FROM new_user.my_table`.  
  
### After Upgrade Procedure  
 If the user **sys** was not renamed prior to upgrading, do the following:  
  
1.  Execute the statement `ALTER DATABASE db_name SET ONLINE`. The database will be in SINGLE_USER mode.  
  
2.  Follow all steps in the Before Upgrade Procedure section.  
  
3.  Execute the statement `ALTER DATABASE db_name SET MULTI_USER`.  
  
## See Also  
 [Database Engine Upgrade Issues](../../../2014/sql-server/install/database-engine-upgrade-issues.md)   
 [SQL Server 2014 Upgrade Advisor &#91;new&#93;](/sql/2014/sql-server/install/sql-server-2014-upgrade-advisor)  
  
  
