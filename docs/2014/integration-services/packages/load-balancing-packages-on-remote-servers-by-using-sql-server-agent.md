---
title: "Load-Balancing Packages on Remote Servers by Using SQL Server Agent | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "load-balancing [Integration Services]"
  - "parent packages [Integration Services]"
  - "SQL Server Agent [Integration Services]"
ms.assetid: 9281c5f8-8da3-4ae8-8142-53c5919a4cfe
author: janinezhang
ms.author: janinez
manager: craigg
---
# Load-Balancing Packages on Remote Servers by Using SQL Server Agent
  When many packages have to be run, it is convenient to use other servers that are available. This method of using other servers to run packages when the packages are all under the control of one parent package is called load balancing. In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)], load balancing is a manual procedure that must be architected by the owners of the packages. Load balancing is not performed automatically by the servers. Also, the packages that are run on the remote servers must be whole packages, not individual tasks in other packages.  
  
 Load balancing is useful in the following scenarios:  
  
-   Packages can run at the same time.  
  
-   Packages are large and, if run sequentially, can run longer than the time allowed for processing.  
  
 Administrators and architects can determine whether using additional servers for processing would benefit their processes.  
  
## Illustration of Load-Balancing  
 The following diagram shows a parent package on a server. The parent package contains multiple Execute SQL Job Agent tasks. Each task in the parent package calls a SQL Server Agent on a remote server. Those remote servers contain SQL Server Agent jobs that include a step that calls a package on that server.  
  
 ![Overview of SSIS load balancing architecture](../media/loadbalancingoverview.gif "Overview of SSIS load balancing architecture")  
  
 The steps required for load balancing in this architecture are not new concepts. Instead, load balancing is achieved by using existing concepts and common SSIS objects in a new way.  
  
## Execution of Packages on a Remote Instance by using SQL Server Agent  
 In the basic architecture for remote package execution, a central package resides on the instance of SQL Server that controls the other remote packages. The diagram shows this central package, named the SSIS Parent. The instance where this parent package resides controls execution of the SQL Server Agent jobs that run the child packages. The child packages are not run according to a fixed schedule controlled by the SQL Server Agent on the remote server. Instead, the child packages are started by the SQL Server Agent when called by the parent package and are run on the same instance of SQL Server on which the SQL Server Agent resides.  
  
 Before you can run a remote package by using SQL Server Agent, you must configure the parent and child packages and set up the SQL Server Agent jobs that control the child packages. The following sections provide more information about how to create, configure, run, and maintain packages that run on remote servers. There are several steps to this process:  
  
-   Creating the child packages and installing them on remote servers.  
  
-   Creating the SQL Server Agent jobs on the remote instances that will run the packages.  
  
-   Creating the parent package.  
  
-   Determine the logging scenario for the child packages.  
  
 The following table provides links to topics that guide you through the process.  
  
|Topic|Description|  
|-----------|-----------------|  
|[Implementation of Child Packages](../implementation-of-child-packages.md)|Describes the installation of packages, and creation of the SQL Server Agent jobs to run the packages.|  
|[Implementation of the Parent Package](../implementation-of-the-parent-package.md)|Describes the creation of the parent package that contains many Execute SQL Server Agent Job tasks. Each task runs one of the child packages.|  
|[Logging for Load Balanced Packages on Remote Servers](../logging-for-load-balanced-packages-on-remote-servers.md)|Describes the logging scenario for the remote packages.|  
  
## Related Tasks  
 [Schedule a Package by using SQL Server Agent](../schedule-a-package-by-using-sql-server-agent.md)  
  
  
