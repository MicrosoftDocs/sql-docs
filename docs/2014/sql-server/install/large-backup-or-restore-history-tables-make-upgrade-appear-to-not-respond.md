---
title: "Large backup or restore history tables make upgrade appear to not respond | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "backup history tables"
  - "history tables"
ms.assetid: f88d86ec-324b-4518-b6d7-1af7e7265812
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Large backup or restore history tables make upgrade appear to not respond
  In [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)], new columns were added to some of the backup and restore history tables. Upgrading these tables requires altering them to add the new columns. If one or more of these tables contains a large number of rows, the upgrade will stall for a substantial amount of time on the ALTER TABLE statement that adds columns to that table.  
  
## Component  
 [!INCLUDE[ssDE](../../includes/ssde-md.md)]  
  
## Description  
 Upgrade can appear to hang if any of the following backup or restore history tables contains a large number of rows:  
  
-   **backupfile**  
  
-   **backupmediafamily**  
  
-   **backupmediaset**  
  
-   **backupset**  
  
-   **restorefile**  
  
-   **restorefilegroup**  
  
-   **restorehistory**  
  
 If any of these tables has 10,000 or more rows, Upgrade Advisor reports a noncompliance failure. It does not report which table exceeds the allowed number of rows. The first table that exceeds 10,000 rows causes the failure.  
  
## Corrective Action  
 Before you upgrade a database, if any of these tables exceeds 10,000 rows, we recommend that you reduce the number to less than 10,000. To reduce rows in all of these tables, you can run the **sp_delete_backuphistory** stored procedure. This procedure deletes the entries in all of the backup and restore history tables for backup sets older than a specified date. For more information, see [sp_delete_backuphistory &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-delete-backuphistory-transact-sql).  
  
> [!NOTE]  
>  You can upgrade a database whose backup and restore history tables are larger than 10,000 rows. But the upgrade may appear to stall while large tables are being altered. The larger the tables, the longer that Setup takes to complete.  
  
## See Also  
 [Database Engine Upgrade Issues](../../../2014/sql-server/install/database-engine-upgrade-issues.md)   
 [SQL Server 2014 Upgrade Advisor &#91;new&#93;](sql-server-2014-upgrade-advisor.md)  
  
  
