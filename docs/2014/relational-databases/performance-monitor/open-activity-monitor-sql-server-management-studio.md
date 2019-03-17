---
title: "Open Activity Monitor (SQL Server Management Studio) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "Activity Monitor [SQL Server], setting the refresh interval"
  - "refresh interval for Activity Monitor"
  - "Activity Monitor [SQL Server], opening"
  - "opening Activity Monitor"
ms.assetid: 0a6eeb16-f02b-479d-9a60-543e40ebf46b
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Open Activity Monitor (SQL Server Management Studio)
  This topic describes how to open the Activity Monitor to obtain information about [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] processes and how these processes affect the current instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. It also describes how to set the refresh interval of the Activity Monitor.  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Security](#Security)  
  
-   **To open the Activity Monitor using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
-   **To set the refresh interval using:**  [SQL Server Management Studio](#Refresh)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
 Activity Monitor runs queries on the monitored instance to obtain information for the Activity Monitor display panes. When the refresh interval is set to less than 10 seconds, the time that is used to run these queries can affect server performance  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 To view the Activity Monitor, a user must have VIEW SERVER STATE permission. To view the Data File I/O section of Activity Monitor, you must have CREATE DATABASE, ALTER ANY DATABASE, or VIEW ANY DEFINITION permission in addition to VIEW SERVER STATE.  
  
 To KILL a process, a user must be a member of the sysadmin or processadmin fixed server roles.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To open Activity Monitor in SQL Server Management Studio  
  
1.  On the [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] standard toolbar, click **Activity Monitor**.  
  
2.  In the **Connect to Server** dialog box, select the server name and authentication mode, and then click **Connect**.  
  
 You can also open Activity Monitor at any time by pressing CTRL+ALT A.  
  
#### To open Activity Monitor in Object Explorer  
  
-   In Object Explorer, right-click the instance name, and then select **Activity Monitor**.  
  
#### To open Activity Monitor when opening SQL Server Management Studio  
  
1.  On the **Tools** menu, click **Options**.  
  
2.  In the **Options** dialog box, expand **Environment**, and then select **General**.  
  
3.  In the **At startup** box, select **Open Object Explorer and Activity Monitor**.  
  
4.  To activate the changes, close and reopen [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
###  <a name="Refresh"></a> To Set the Activity Monitor Refresh Interval  
  
-   Open the Activity Monitor.  
  
-   Right-click **Overview**, select **Refresh Interval**, and then select the interval in which Activity Monitor should obtain new instance information.  
  
  
