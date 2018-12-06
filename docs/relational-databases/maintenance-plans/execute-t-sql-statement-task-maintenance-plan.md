---
title: "Execute T-SQL Statement Task (Maintenance Plan) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.maint.tsql.f1"
helpviewer_keywords: 
  - "Execute T-SQL Statement Task dialog box"
ms.assetid: fef3e259-0c47-4f6e-ade0-aee95e3d6c1a
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Execute T-SQL Statement Task (Maintenance Plan)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  Use the **Execute T-SQL Statement Task** dialog to customize your maintenance plan by adding [!INCLUDE[tsql](../../includes/tsql-md.md)] statements of your choice to this maintenance plan.  
  
## Options  
 **Connection**  
 Select the server connection to use when performing this task.  
  
 **New**  
 Create a new server connection to use when performing this task. The **New Connection** dialog box is described below.  
  
 **Execution time out**  
 Time (seconds) to wait for task completion before timing out (terminating task).  
  
 **T-SQL statement**  
 [!INCLUDE[tsql](../../includes/tsql-md.md)] statements to execute.  
  
 **View T-SQL**  
 View the [!INCLUDE[tsql](../../includes/tsql-md.md)] statements performed against the server for this task, based on the selected options.  
  
> [!NOTE]  
>  When the number of objects affected is large, this display can take a considerable amount of time.  
  
## New Connection Dialog Box  
 **Connection name**  
 Enter a name for the new connection.  
  
 **Select or enter a server name**  
 Select a server to connect to when performing this task.  
  
 **Refresh**  
 Refresh the list of available servers.  
  
 **Enter information to log on to the server**  
 Specify how to authenticate against the server.  
  
 **Use Windows integrated security**  
 Connect to an instance of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] with [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows Authentication.  
  
 **Use a specific user name and password**  
 Connect to an instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication. This option is not available.  
  
 **User name**  
 Provide a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login to use when authenticating. This option is not available.  
  
 **Password**  
 Provide a password to use when authenticating. This option is not available.  
  
  
