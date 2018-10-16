---
title: "Refresh from database (MySQLToSQL) | Microsoft Docs"
ms.prod: sql
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.technology: ssma
ms.topic: conceptual
ms.assetid: 59a6db8f-2db6-4071-9005-928a7231de92
author: "Shamikg"
ms.author: "Shamikg"
manager: craigg
---
# Refresh from database (MySQLToSQL)
The **Refresh from Database** dialog box lets you select which objects to refresh from the MySQL database. Rows in the dialog box are color coded based on the state of the metadata:  
  
-   If the object metadata has changed locally and in the MySQL database, the row is blue.  
  
-   If the object metadata has changed in the MySQL database but not in SSMA, the row is yellow.  
  
-   If the object metadata has changed locally, but not in the MySQL database, the row is green.  
  
-   If the object is new in the MySQL database, the row is pink.  
  
You can specify default object refresh settings in the **Project Settings** dialog box. For more information, see [Project Settings &#40;Synchronization&#41; &#40;MySQLToSQL&#41;](../../ssma/mysql/project-settings-synchronization-mysqltosql.md)  
  
To access the **Refresh from Database** dialog box, right-click an object in MySQL Metadata Explorer and click **Refresh from Database**.  
  
## Options  
  
|||  
|-|-|  
|**Term**|**Definition**|  
|**Collapse (-)**|Collapse all object groups to hide individual objects.|  
|**Expand (+)**|Expand all object groups to show individual objects.|  
|**Hide/Show Equal Objects**|Hides objects from the list if the object metadata is the same in the MySQL database and in SSMA.|  
|**Refresh from Database (arrow button)**|Use the arrow button to specify that the metadata for the selected objects should be updated in SSMA.|  
|**Do Not Refresh from Database (X button)**|Use the X button to specify that the metadata for the selected objects should not be updated in SSMA.|  
|**Legend**|Displays a **Legend** dialog box. The legend contains the mapping between row colors and metadata states.<br /><br />To keep the **Legend** dialog box on top of the **Refresh from Database** dialog box, select the **Show on top** check box.|  
  
