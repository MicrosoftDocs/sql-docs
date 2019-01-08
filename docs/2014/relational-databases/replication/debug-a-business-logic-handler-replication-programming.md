---
title: "Debug a Business Logic Handler | Microsoft Docs"
ms.custom: ""
ms.date: "03/08/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
dev_langs: 
  - "VB"
  - "CSharp"
helpviewer_keywords: 
  - "merge replication business logic handlers [SQL Server replication]"
  - "business logic handlers [SQL Server replication]"
  - "BusinessLogicModule class"
ms.assetid: edd0d17a-0e9c-4c28-8395-a7d47e8ce3d6
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Debug a Business Logic Handler (Replication Programming)
  Use a business logic handler to invoke custom business logic when a merge subscription is synchronized. For more information, see [Execute Business Logic During Merge Synchronization](merge/execute-business-logic-during-merge-synchronization.md).  
  
 The Merge Replication Reconciler (replrec.dll) calls the managed code assembly containing the business logic. In most cases, replrec.dll and the custom business logic is executed on the computer where the Merge Agent runs (at the Subscriber for a pull subscription or at the Distributor for a push subscription). In the case of Web synchronization, or in the case of a [!INCLUDE[ssEW](../../includes/ssew-md.md)] Subscriber, the reconciler and the custom business logic is executed on the Web server.  
  
### To debug a business logic handler on a local computer  
  
1.  Configure publishing and distribution, create a publication, and create a subscription to the publication. For more information, see [Configure Publishing and Distribution](configure-publishing-and-distribution.md) and [Create a Publication](publish/create-a-publication.md).  
  
2.  Create and register a business logic handler. For more information, see [Implement a Business Logic Handler for a Merge Article](implement-a-business-logic-handler-for-a-merge-article.md).  
  
3.  Create a Replication Management Objects (RMO) project in [!INCLUDE[msCoName](../../includes/msconame-md.md)] Visual Studio that programmatically starts the Merge Agent synchronously. For more information, see [Synchronize a Pull Subscription](synchronize-a-pull-subscription.md).  
  
4.  Set a breakpoint in the business logic handler code, either in the method being debugged or in the class constructor. For more information about the methods that can be implemented in a business logic handler, see the <xref:Microsoft.SqlServer.Replication.BusinessLogicSupport.BusinessLogicModule> methods topic.  
  
5.  Build the business logic handler in debug mode and deploy the assembly and debugging symbol file (.pdb) in the location registered in step 1.  
  
    > [!NOTE]  
    >  To simplify debugging, create a single Visual Studio .NET solution that contains both the business logic handler project and the project that synchronizes the subscription. In this case, set the synchronization project as the startup project, and configure the build environment to deploy the business logic assembly to the location registered in step 1 during debugging.  
  
6.  Execute insert, update, or delete commands against the subscription or publication database. The command and execution location depends on the method being debugged.  
  
7.  Start the project from step 3 in debug mode to synchronize the subscription.  
  
8.  Assuming that no other breakpoints are set and the proper commands are replicated, the execution stops when it reaches the breakpoint in the business logic handler.  
  
### To debug a business logic handler on a Web server using Web synchronization, or for a SQL Server Compact Subscriber  
  
1.  Configure publishing and distribution, create a publication, and create a pull subscription to the publication. The publication must support Web synchronization or [!INCLUDE[ssEW](../../includes/ssew-md.md)] Subscribers.  
  
2.  Create and register a business logic handler. For more information, see [Implement a Business Logic Handler for a Merge Article](implement-a-business-logic-handler-for-a-merge-article.md).  
  
3.  Set a breakpoint in the business logic handler code, either in the method being debugged or in the class constructor. For more information about the methods that can be implemented in a business logic handler, see the <xref:Microsoft.SqlServer.Replication.BusinessLogicSupport.BusinessLogicModule> methods topic.  
  
4.  Build the business logic handler in debug mode and deploy the assembly and debugging symbol file (.pdb) at the Web server in the location registered in step 1.  
  
    > [!NOTE]  
    >  If the business logic handler fails to build because the assembly is in use, type the command `iisreset` on the Web server at the command prompt to reset the Web server.  
  
5.  Synchronize the subscription with Web synchronization enabled. During synchronization, the Web server loads the registered assembly.  
  
6.  Using the Visual Studio .NET debugger, attach to the one of the following processes on the Web server:  
  
    -   w3wp.exe - Windows Server 2003.  
  
    -   inetinfo.exe - Windows 2000 and Windows XP.  
  
7.  In the **Output** window, check the debug output to verify that the symbols for the registered assembly loaded properly. If the symbols were not loaded, ensure that the correct .pdb file was copied in step 4, and repeat step 5.  
  
8.  Execute insert, update, or delete commands against the subscription or publication database. The command and execution location depends on the method being debugged.  
  
9. Using the Visual Studio debugger, attach to the w3wp.exe process.  
  
10. Synchronize the subscription again, using Web synchronization.  
  
11. Assuming that no other breakpoints are set and the proper commands are replicated, the execution stops when it reaches the breakpoint in the business logic handler.  
  
## See Also  
 [Implement a Business Logic Handler for a Merge Article](implement-a-business-logic-handler-for-a-merge-article.md)  
  
  
