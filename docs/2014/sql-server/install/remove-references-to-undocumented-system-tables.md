---
title: "Remove references to undocumented system tables | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "undocumented system tables [SQL Server]"
  - "system tables [SQL Server]"
ms.assetid: 010b1236-2219-4bf4-a6db-e3fc3abfa37a
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Remove references to undocumented system tables
  Many system tables that were undocumented in prior releases have changed or no longer exist; therefore, using these tables may cause errors after upgrading. Because Upgrade Advisor looks for references to system table names, it will report references to any user tables that have the same names as system tables.  
  
## Component  
 [!INCLUDE[ssDE](../../includes/ssde-md.md)]  
  
## Description  
 The following undocumented system tables are removed:  
  
-   **spt_datatype_info**  
  
-   **spt_datatype_info_ext**  
  
-   **spt_provider_types**  
  
-   **spt_server_info**  
  
-   **spt_values**  
  
-   **sysfulltextnotify**  
  
-   **syslocks**  
  
-   **sysproperties**  
  
-   **sysprotects_aux**  
  
-   **sysprotects_view**  
  
-   **SYSREMOTE_CATALOGS**  
  
-   **SYSREMOTE_COLUMN_PRIVILEGES**  
  
-   **SYSREMOTE_COLUMNS**  
  
-   **SYSREMOTE_FOREIGN_KEYS**  
  
-   **SYSREMOTE_INDEXES**  
  
-   **SYSREMOTE_PRIMARY_KEYS**  
  
-   **SYSREMOTE_PROVIDER_TYPES**  
  
-   **SYSREMOTE_SCHEMATA**  
  
-   **SYSREMOTE_STATISTICS**  
  
-   **SYSREMOTE_TABLE_PRIVILEGES**  
  
-   **SYSREMOTE_TABLES**  
  
-   **SYSREMOTE_VIEWS**  
  
-   **syssegments**  
  
-   **sysxlogins**  
  
## Corrective Action  
 Modify your applications according to the following table.  
  
|Instead of|Use|  
|----------------|---------|  
|**sysfulltextnotify**|**TableFulltextPendingChanges** property of the OBJECTPROPERTYEX function.|  
|**syslocks**|**sys.dm_tran_locks** dynamic management view, or sp_lock, or the **sys.syslockinfo** compatibility view.|  
|**sysproperties**|**sys.extended_properties** catalog view or the **fn_listextendedproperty** function|  
|**sysxlogins**|**sys.server_principals** catalog view or **syslogins** compatibility view.|  
|all **spt_** tables|No replacement available|  
  
## See Also  
 [Database Engine Upgrade Issues](../../../2014/sql-server/install/database-engine-upgrade-issues.md)   
 [SQL Server 2014 Upgrade Advisor &#91;new&#93;](/sql/2014/sql-server/install/sql-server-2014-upgrade-advisor)  
  
  
