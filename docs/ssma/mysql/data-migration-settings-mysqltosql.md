---
description: "Data Migration Settings (MySQLToSQL)"
title: "Data Migration Settings (MySQLToSQL) | Microsoft Docs"
ms.service: sql
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.subservice: ssma
ms.topic: conceptual
ms.assetid: 9c396df4-5676-4f32-9c57-70d4f15f9b7a
author: cpichuka 
ms.author: cpichuka
f1_keywords: 
    - "ssma.mysql.detaildatamigrationsettingcontrol.f1" 
---
# Data Migration Settings (MySQLToSQL)
  
## Data Migration Settings  
**Data Migration Settings** allows the user to write custom queries for data migration.  
  
-   This tab is available when **Extended data migration options** is set to **Show** and is hidden when the setting is set to **Hide** in Project Settings. For more information about Project Migration Settings, see [Project Settings (Migration)](./project-settings-migration-mysqltosql.md) .  
  
-   Parsing of Custom SQL statements will be implemented in **Data migration settings** tab of Table Node.  
  
-   Following are the two check boxes available in the **Data Migration Settings** viz.:  
  
    1.  Truncate SQL Server table  
  
    2.  Use custom select  
  
1.  **Truncate SQL Server table:**  
     This option allows the user to have a clear view of the migrated data at the target database.  
  
    -   By default, this textbox is checked.  
  
    -   If this textbox is unchecked, then the data that is migrated will be added on to the existing data at the target database.  
  
2.  **Use custom select:**  
     This option allows the user to modify the **select** statement present (**select** statement allows the users to select the data to be displayed at the target database).  
  
    1.  By default, this textbox is unchecked.  
  
    2.  If this textbox is checked, then it allows the users to modify the **select** statement present.  
  
There are two buttons present viz.:  
  
-   **Apply:** Click **Apply** to apply the settings that have been changed.  
  
-   **Cancel:** Click **Cancel** to restore the settings present before the changes were being made.  
  
## See Also  
[Migrating MySQL Data to SQL Server/SQL Azure](./migrating-mysql-data-into-sql-server-azure-sql-db-mysqltosql.md)  
