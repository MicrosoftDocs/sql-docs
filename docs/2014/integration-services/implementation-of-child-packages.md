---
title: "Implementation of Child Packages | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "child packages"
ms.assetid: ab0c09d7-ce2e-487d-a1ed-a4b5adb6cc01
author: janinezhang
ms.author: janinez
manager: craigg
---
# Implementation of Child Packages
  When you implement load balancing using [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)], child packages are installed on other servers to take advantage of the available CPU or server time. To create and run the child packages requires the following steps:  
  
-   Designing the child packages.  
  
-   Moving the packages to the remote server.  
  
-   Creating a SQL Server Agent Job on the remote server that contains a step that runs the child package.  
  
-   Testing and debugging the SQL Server Agent Job and child packages.  
  
 When you design the child packages, the packages have no limitations in their design, and you can put in any functionality you desire. However, if the package accesses data, you must ensure that the server that runs the package has access to the data.  
  
 To identify the parent package that executes child packages, in [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)] right click the package in Solution Explorer and then click **Entry-point Package**.  
  
 After the child packages have been designed, the next step is to deploy them on the remote servers.  
  
## Moving the Child Package to the Remote Instance  
 There are multiple ways to move packages to other servers. The two suggested methods are:  
  
-   Exporting packages by using [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)].  
  
-   Deploying packages by building a deployment utility for the project that contains the packages you want to deploy, and then running the Package Installation Wizard to install the packages to the file system or to an instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. For more information, see [Package Deployment &#40;SSIS&#41;](packages/legacy-package-deployment-ssis.md).  
  
 You must repeat the deployment to each remote server you want to use.  
  
## Creating the SQL Server Agent Jobs  
 After the child packages have been deployed to the various servers, create a SQL Server Agent job on each server that contains a child package. The SQL Server Agent job contains a step that runs the child package when the job agent is called. The SQL Server Agent jobs are not scheduled jobs; they run the child packages only when they are called by the parent package. Notification of success or failure of the job back to the parent package reflects the success or failure of the SQL Server Agent job and whether it was called successfully, not the success or failure of the child package or whether it was executed.  
  
## Debugging the SQL Server Agent Jobs and Child Packages  
 You can test the SQL Server Agent jobs and their child packages by using one of the following methods:  
  
-   Running each child package in SSIS Designer, by clicking **Debug** / **Start Without Debugging**.  
  
-   Running the individual SQL Server Agent job on the remote computer by using [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)], to make sure that the package runs.  
  
 For information about how to troubleshoot packages that you run from [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Agent jobs, see [An SSIS package does not run when you call the SSIS package from a SQL Server Agent job step](https://support.microsoft.com/kb/918760) in the [!INCLUDE[msCoName](../includes/msconame-md.md)] Support Knowledge Base.  
  
 The SQL Server Agent checks subsystem access for a proxy and gives access to the proxy every time the job step runs.  
  
 You can create a proxy in [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)].  
  
## Related Tasks  
  
-   [Run a Package on the SSIS Server Using SQL Server Management Studio](run-a-package-on-the-ssis-server-using-sql-server-management-studio.md)  
  
## Related Content  
  
-   Blog entry, [SSIS: Accessing variables in a parent package](https://go.microsoft.com/fwlink/?LinkId=257729), on consultingblogs.emc.com.  
  
-   Article, [Execute Package Task](../integration-services/control-flow/execute-package-task.md).  
  
  
