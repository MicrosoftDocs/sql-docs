---
title: "Specify a Breakpoint Filter | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.technology: scripting
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "Transact-SQL debugger, breakpoint filter"
ms.assetid: 7bf1dddd-7b0b-4c47-8a7b-28a5569b4fa5
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Specify a Breakpoint Filter
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
  A breakpoint filter limits the breakpoint to acting only on specified computers, operating system processes, and threads. Breakpoint filters are typically used when debugging parallel applications.  
  
##  <a name="BKMK_ActionConsiderations"></a> Filter Considerations  
 Breakpoint filters are not typically used with the [!INCLUDE[tsql](../../includes/tsql-md.md)] debugger because [!INCLUDE[tsql](../../includes/tsql-md.md)] scripts and stored procedures are not parallel applications.  
  
#### To Specify a Breakpoint Filter  
  
1.  In the editor window, right-click the breakpoint glyph, and then click **Filter** on the shortcut menu.  
  
     -or-  
  
     In the **Breakpoints** window, right-click the breakpoint glyph, and then click **Filter** on the shortcut menu.  
  
2.  In the **Breakpoint Filters** dialog box, use the **Filter** box to specify computers by name, or operating system processes and threads by either name or ID number:  
  
    -   **MachineName** is the computer running the instance of the Database Engine.  
  
    -   **ProcessID**, and **ProcessName** are the operating system process running the instance of the Database Engine.  
  
    -   **ThreadID** and **ThreadName** are the operating system thread running the [!INCLUDE[tsql](../../includes/tsql-md.md)] batch, procedure, or function in the instance of the Database Engine.  
  
3.  Click **OK** to implement the changes, or **Cancel** to exit without applying the changes.  
  
## See Also  
 [Specify a Breakpoint Condition](../../relational-databases/scripting/specify-a-breakpoint-condition.md)   
 [Specify a Hit Count](../../relational-databases/scripting/specify-a-hit-count.md)   
 [Specify a Breakpoint Action](../../relational-databases/scripting/specify-a-breakpoint-action.md)  
