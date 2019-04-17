---
title: "Threads Window | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.technology: scripting
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "Threads Window [Transact-SQL]"
ms.assetid: e153f619-0049-4162-9076-c24a454f3278
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Transact-SQL Debugger - Threads Window
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
  The **Threads** window displays information about the [!INCLUDE[ssDE](../../includes/ssde-md.md)] thread that is used by the [!INCLUDE[ssDE](../../includes/ssde-md.md)] Query Editor session that is being debugged. You must be in debug mode to display the thread information.  
  
## Task List  
 **To access the Threads window**  
  
-   On the **Debug** menu, click **Windows**, and then click **Threads**.  
  
## Columns  
 **ID**  
 Is a unique identifying number that the [!INCLUDE[tsql](../../includes/tsql-md.md)] debugger assigns to the thread. You can find more information about the thread by selecting a row from the sys.dm_os_threads dynamic management view.  
  
 If you are not running in lightweight pooling mode, select the row in which the value in os_thread_id matches the value in the **ID** column. If you are running in lightweight pooling mode, select the row in which the value in fiber_context_address matches the value in the **ID** column.  
  
 **Name**  
 Displays information about the [!INCLUDE[ssDE](../../includes/ssde-md.md)] session in the format **ComputerName/InstanceName [SPID]**.  
  
 **ComputerName**  
 The name of the computer that is running the instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] that the Query Editor session is connected to.  
  
 **InstanceName**  
 The name of the instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] that the Query Editor session is connected to.  
  
 **[SPID]**  
 The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] session process ID that uniquely identifies this session. You can obtain more information about the session by selecting the row in the sys.sysprocesses view that has the same value in the spid column.  
  
 **Location**  
 Displays the name of the script file that is used in the Query Editor session that is being debugged.  
  
 **Priority**  
 The [!INCLUDE[tsql](../../includes/tsql-md.md)] debugger does not support this feature.  
  
 **Suspend**  
 The [!INCLUDE[tsql](../../includes/tsql-md.md)] debugger does not support this feature.  
  
## See Also  
 [Transact-SQL Debugger](../../relational-databases/scripting/transact-sql-debugger.md)   
 [Transact-SQL Debugger Information](../../relational-databases/scripting/transact-sql-debugger-information.md)   
 [sys.dm_os_threads &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-os-threads-transact-sql.md)   
 [sys.sysprocesses &#40;Transact-SQL&#41;](../../relational-databases/system-compatibility-views/sys-sysprocesses-transact-sql.md)  
