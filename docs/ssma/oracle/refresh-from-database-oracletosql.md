---
description: "Refresh from Database (OracleToSQL)"
title: "Refresh from Database (OracleToSQL) | Microsoft Docs"
ms.service: sql
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.subservice: ssma
ms.topic: conceptual
ms.assetid: 84492f44-c368-4c75-954d-7307a2d2bbc0
author: cpichuka 
ms.author: cpichuka 
---
# Refresh from Database (OracleToSQL)
The **Refresh from Database** dialog box lets you select which objects to refresh from the Oracle database. Rows in the dialog box are color coded based on the state of the metadata:  
  
-   If the object metadata has changed locally and in the Oracle database, the row is blue.  
  
-   If the object metadata has changed in the Oracle database but not in SSMA, the row is yellow.  
  
-   If the object metadata has changed locally, but not in the Oracle database, the row is green.  
  
-   If the object is new in the Oracle database, the row is pink.  
  
You can specify default object refresh settings in the **Project Settings** dialog box. For more information, see [Project Settings&#40;Synchronization&#41; &#40;OracleToSQL&#41;](../../ssma/oracle/project-settings-synchronization-oracletosql.md).  
  
To access the **Refresh from Database** dialog box, right-click an object in Oracle Metadata Explorer and click **Refresh from Database**.  
  
## Options  
**Collapse (-)**  
Collapse all object groups to hide individual objects.  
  
**Expand (+)**  
Expand all object groups to show individual objects.  
  
**Hide/Show Equal Objects**  
Hides objects from the list if the object metadata is the same in the Oracle database and in SSMA.  
  
**Refresh from Database (arrow button)**  
Use the arrow button to specify that the metadata for the selected objects should be updated in SSMA.  
  
**Do Not Refresh from Database (X button)**  
Use the X button to specify that the metadata for the selected objects should not be updated in SSMA.  
  
**Legend**  
Displays a **Legend** dialog box. The legend contains the mapping between row colors and metadata states.  
  
To keep the **Legend** dialog box on top of the **Refresh from Database** dialog box, select the **Show on top** check box.  
  
