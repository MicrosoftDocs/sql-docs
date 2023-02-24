---
description: "Project Settings(Synchronization) (OracleToSQL)"
title: "Project Settings(Synchronization) (OracleToSQL) | Microsoft Docs"
ms.service: sql
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.subservice: ssma
ms.topic: conceptual
ms.assetid: e223fb7d-05ec-4fa5-8973-d845c33a23dd
author: cpichuka 
ms.author: cpichuka 
---
# Project Settings(Synchronization) (OracleToSQL)
The Synchronization page of the **Project Settings** dialog box contains settings that customize how SSMA loads and refreshes database objects, such as tables and stored procedures, into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
The default actions options specify default settings for refreshing objects from the Oracle database and for synchronizing objects with the SQL Server database. For more information, see [Refresh from Database - Oracle](../../ssma/oracle/refresh-from-database-oracletosql.md).  
  
You can access two different Synchronization pages that contain the same settings:  
  
-   To specify settings for all future SSMA projects, on the **Tools** menu, click **Default Project Settings**, and then click **Synchronization** at the bottom of the left pane.  
  
-   To specify settings for the current project, on the **Tools** menu, click **Project Settings**, and then click **Synchronization** at the bottom of the left pane.  
  
## Miscellaneous Options  
**Attempts**  
Specifies the number of attempts SSMA should make when it loads objects into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Objects that are not loaded into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in the current attempt will be tried again until SSMA reaches the maximum number of attempts in the current synchronization process. Default value set is **2**  
  
## Synchronization for Oracle Options  
**Action on local and remote object change**  
Specifies the default setting in the Synchronization dialog box when the object definition changes in SSMA and on the database server. Default value set is **Refresh from database**.  
  
-   If you select **Refresh from Database**, SSMA will load database definitions into the metadata when the condition is met.  
  
-   If you select **Skip**, SSMA will not perform any refresh actions.  
  
**Action on local object change**  
Specifies the default setting in the Synchronization dialog box when the object changes in SSMA. Default value set is **Skip**.  
  
-   If you select **Refresh from Database**, SSMA will load database definitions into the metadata when the condition is met.  
  
-   If you select **Skip**, SSMA will not perform any refresh actions.  
  
**Action on remote object change**  
Specifies the default setting in the Synchronization dialog box when the objects change on the database server. Default value set is **Refresh from database**.  
  
-   If you select **Refresh from Database**, SSMA will load database definitions into the metadata when the condition is met.  
  
-   If you select **Skip**, SSMA will not perform any refresh actions.  
  
**Action when local object metadata is missing**  
Specifies the default setting in the Synchronization dialog box when local metadata is missing. Default value set is **Refresh from database**.  
  
-   If you select **Refresh from Database**, SSMA will load database definitions into the metadata when the condition is met.  
  
-   If you select **Skip**, SSMA will not perform any refresh actions.  
  
## Synchronization for SQL Server Options  
**Action on local and remote object change**  
Specifies the default setting in the Synchronization dialog box when the object definition changes in SSMA and on the database server. Default value set is **Write to database**.  
  
-   If you select **Refresh from Database**, SSMA will load database definitions into the metadata when the condition is met.  
  
-   If you select **Write to Database**, SSMA will update objects in the database according to SSMA metadata contents when the condition is met.  
  
-   If you select **Skip**, SSMA will not perform any refresh actions.  
  
**Action on local object change**  
Specifies the default setting in the Synchronization dialog box when the object changes in SSMA. Default value set is **Write to database**.  
  
-   If you select **Refresh from Database**, SSMA will load database definitions into the metadata when the condition is met.  
  
-   If you select **Write to Database**, SSMA will update the object in the database according to SSMA metadata contents when the condition is met.  
  
-   If you select **Skip**, SSMA will not perform any refresh actions.  
  
**Action on remote object change**  
Specifies the default setting in the Synchronization dialog box when the objects change on the database server.  Default value set is **Refresh from database**.  
  
-   If you select **Refresh from Database**, SSMA will load database definitions into the metadata when the condition is met.  
  
-   If you select **Write to Database**, SSMA will update the object in the database according to SSMA metadata contents when the condition is met.  
  
-   If you select **Skip**, SSMA will not perform any refresh actions.  
  
**Action when local object metadata is missing**  
Specifies the default setting in the Synchronization dialog box when local metadata is missing. Default value set is **Refresh from database**.  
  
-   If you select **Refresh from Database**, SSMA selects the **Refresh from Database** option when the condition is met.  
  
-   If you select **Write to Database**, SSMA will delete the object from the database when the condition is met.  
  
-   If you select **Skip**, SSMA will not perform any refresh actions.  
  
