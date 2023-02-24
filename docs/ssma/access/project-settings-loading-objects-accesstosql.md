---
description: "Project Settings (Loading Objects) (AccessToSQL)"
title: "Project Settings (Loading Objects) (AccessToSQL) | Microsoft Docs"
ms.service: sql
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.subservice: ssma
ms.topic: conceptual
ms.assetid: 9ec1c1e8-a3e1-4e81-bf49-631f87daa209
author: cpichuka 
ms.author: cpichuka 
f1_keywords: 
  - "ssma.access.projectsettingsloadingobjects.f1"

---
# Project Settings (Loading Objects) (AccessToSQL)
The Loading Objects project settings let you configure how Access database objects are synchronized with SQL Server database objects.  
  
The default actions specify default settings for refreshing objects from the Access database and for synchronizing objects with the SQL Server database. For more information, see [Refresh from Database &#40;AccessToSQL&#41;](../../ssma/access/refresh-from-database-accesstosql.md)  
  
You can access two different Synchronization pages that contain the same settings:  
  
-   To specify settings for all future SSMA projects, on the **Tools** menu, click **DefaultProject Settings**, and then click **Loading Objects** at the bottom of the left pane.  
  
-   To specify settings for the current project, on the **Tools** menu, click **Project Settings**, and then click **Loading Objects** at the bottom of the left pane.  
  
## Options  
  
##### Misc  
  
##### Attempts  
Gives the information about the number of passes objects take to load into SQL Server. Loading objects into SQL Server is typically performed in multiple passes. Objects that fail to load in the first pass, such as foreign keys, might successfully load in the next pass.  
  
By default the value is 2.  
  
## Synchronization for SQL Server  
  
##### Default action on Local and Remote object change  
Specifies the default setting in the Synchronization dialog box when the object definition changes in SSMA and on the database server.  
  
-   If you select **Refresh from Database**, SSMA will load database definitions into the metadata when the condition is met.  
  
-   If you select **Write to Database**, SSMA will update objects in the database according to SSMA metadata contents when the condition is met.  
  
-   If you select **Skip**, SSMA will not perform any refresh actions.  
  
##### Default action on local object change  
Specifies the default setting in the Synchronization dialog box when the object changes in SSMA.  
  
-   If you select **Refresh from Database**, SSMA will load database definitions into the metadata when the condition is met.  
  
-   If you select **Write to Database**, SSMA will update the object in the database according to SSMA metadata contents when the condition is met.  
  
-   If you select **Skip**, SSMA will not perform any refresh actions.  
  
##### Default action on remote object change  
Specifies the default setting in the Synchronization dialog box when the objects change on the database server.  
  
-   If you select **Refresh from Database**, SSMA will load database definitions into the metadata when the condition is met.  
  
-   If you select **Write to Database**, SSMA will update the object in the database according to SSMA metadata contents when the condition is met.  
  
-   If you select **Skip**, SSMA will not perform any refresh actions.  
  
##### Default action when local object metadata is missing  
Specifies the default setting in the Synchronization dialog box when local metadata is missing.  
  
-   If you select **Refresh from Database**, SSMA selects the Refresh from Database option when the condition is met.  
  
-   If you select **Write to Database**, SSMA will delete the object from the database when the condition is met.  
  
-   If you select **Skip**, SSMA will not perform any refresh actions.  
  
