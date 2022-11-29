---
description: "Refresh from Database (DB2ToSQL)"
title: "Refresh from Database (DB2ToSQL) | Microsoft Docs"
ms.service: sql
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.subservice: ssma
ms.topic: conceptual
ms.assetid: 613a8368-b372-443f-8252-fb6dc31a003d
author: cpichuka 
ms.author: cpichuka 
---
# Refresh from Database (DB2ToSQL)
The **Refresh from Database** dialog box lets you select which objects to refresh from the DB2 database. Rows in the dialog box are color coded based on the state of the metadata:  
  
-   If the object metadata has changed locally and in the DB2 database, the row is blue.  
  
-   If the object metadata has changed in the DB2 database but not in SSMA, the row is yellow.  
  
-   If the object metadata has changed locally, but not in the DB2 database, the row is green.  
  
-   If the object is new in the DB2 database, the row is pink.  
  
You can specify default object refresh settings in the **Project Settings** dialog box. For more information, see [Project Settings&#40;Synchronization&#41; &#40;DB2ToSQL&#41;](../../ssma/db2/project-settings-synchronization-db2tosql.md).  
  
To access the **Refresh from Database** dialog box, right-click an object in DB2 Metadata Explorer and click **Refresh from Database**.  
  
## Options  
**Collapse (-)**  
Collapse all object groups to hide individual objects.  
  
**Expand (+)**  
Expand all object groups to show individual objects.  
  
**Hide/Show Equal Objects**  
Hides objects from the list if the object metadata is the same in the DB2 database and in SSMA.  
  
**Refresh from Database (arrow button)**  
Use the arrow button to specify that the metadata for the selected objects should be updated in SSMA.  
  
**Do Not Refresh from Database (X button)**  
Use the X button to specify that the metadata for the selected objects should not be updated in SSMA.  
  
**Legend**  
Displays a **Legend** dialog box. The legend contains the mapping between row colors and metadata states.  
  
To keep the **Legend** dialog box on top of the **Refresh from Database** dialog box, select the **Show on top** check box.  
  
