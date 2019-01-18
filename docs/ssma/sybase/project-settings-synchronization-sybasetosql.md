---
title: "Project Settings (Synchronization) (SybaseToSQL) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: ssma
ms.topic: conceptual
ms.assetid: 2cd6bc01-b8e5-4312-83a4-eac66dc1d460
author: "Shamikg"
ms.author: "Shamikg"
manager: craigg
---
# Project Settings (Synchronization) (SybaseToSQL)
The Synchronization page of the **Project Settings** dialog box contains settings that customize how SSMA loads database objects, such as tables and stored procedures, into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure.  
  
You can access two different Synchronization pages that contain the same settings:  
  
-   If you want to specify settings for all future SSMA projects, on the **Tools** menu, select **Default Project Settings**, select migration project type for which settings are required to be viewed or changed from **Migration Target Version** drop down and then select **Synchronization** at the bottom of the left pane.  
  
-   To specify settings for the current project, on the **Tools** menu, select **Project Settings**, and then select **Synchronization** at the bottom of the left pane.  
  
## Options  
**Attempts**  
Specifies the number of attempts SSMA should make when it loads objects into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Objects that are not loaded into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in the current attempt will be tried again until SSMA reaches the maximum number of attempts in the current synchronization process.  
  
## Synchronization for SQL Server  
**Refresh local object on local and remote object change**  
Specifies whether SSMA should replace the local object metadata with remote object metadata if both the local and remote objects change.  
If you select **Refresh from Database**, SSMA will load database definitions into the metadata when the condition is met.  
If you select **Write to Database**, SSMA will update objects in the database according to SSMA metadata contents when the condition is met.  
If you select **Skip**, SSMA will not perform any refresh actions.   
Default option set is **Write to Database.**  
  
**Refresh local object on local object change**  
Specifies whether SSMA should replace the local object metadata with remote object metadata if the remote object changes.  
If you select **Refresh from Database**, SSMA will load database definitions into the metadata when the condition is met.  
If you select **Write to Database**, SSMA will update the object in the database according to SSMA metadata contents when the condition is met.  
If you select **Skip**, SSMA will not perform any refresh actions.   
Default option set is **Write to Database**.  
  
**Refresh local object on Remote object change**  
Specifies whether SSMA should replace the local object metadata with remote object metadata if the remote object changes.  
If you select **Refresh from Database**, SSMA will load database definitions into the metadata when the condition is met.  
If you select **Write to Database**, SSMA will update the object in the database according to SSMA metadata contents when the condition is met.  
If you select **Skip**, SSMA will not perform any refresh actions.   
Default option set is **Refresh from Database**.  
  
**Refresh when local object metadata is missing**  
Specifies whether SSMA should create local metadata if an object exists on the target database, but not locally.  
If you select **Refresh from Database**, SSMA selects the Refresh from Database option when the condition is met.  
If you select **Write to Database**, SSMA will delete the object from the database when the condition is met.  
If you select **Skip**, SSMA will not perform any refresh actions.   
Default option set is **Refresh from Database**.  
  
