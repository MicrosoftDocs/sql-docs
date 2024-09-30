---
title: "Refresh from Database (AccessToSQL)"
description: "Refresh from Database (AccessToSQL)"
author: cpichuka
ms.author: cpichuka
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ssma
ms.topic: conceptual
ms.collection:
  - sql-migration-content
---
# Refresh from Database (AccessToSQL)
The **Refresh from Database** dialog box lets you select which objects to refresh from the Access database. Rows in the dialog box are color coded based on the state of the metadata:  
  
-   If the object metadata has changed locally and in the Access database, the row is blue.  
  
-   If the object metadata has changed in the Access database but not in SSMA, the row is yellow.  
  
-   If the object metadata has changed locally, but not in the Access database, the row is green.  
  
-   If the object is new in the Access database, the row is pink.  
  
You can specify default object refresh settings in the **Project Settings** dialog box. For more information, see [Project Settings &#40;Loading Objects&#41; &#40;AccessToSQL&#41;](../../ssma/access/project-settings-loading-objects-accesstosql.md)  
  
To access the **Refresh from Database** dialog box, right-click any **database** node in Access Metadata Explorer and click **Refresh from Database**.  
  
