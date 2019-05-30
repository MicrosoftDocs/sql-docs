---
title: "Running and Managing Packages Programmatically | Microsoft Docs"
ms.custom: ""
ms.date: "03/17/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: "reference"
ms.assetid: 1a08c75e-ce8c-45ee-81bd-32248bbdb2b2
author: janinezhang
ms.author: janinez
manager: craigg
---
# Running and Managing Packages Programmatically

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


  If you need manage and run [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] packages outside the development environment, you can manipulate packages programmatically. In this approach, you have a range of options:  
  
-   Load and run an existing package without modification.  
  
-   Load an existing package, reconfigure it (for example, for a different data source), and run it.  
  
-   Create a new package, add and configure components object by object and property by property, save it, and run it.  
  
 You can load and run an existing package from a client application by writing only a few lines of code.  
  
 This section describes and demonstrates how to run an existing package programmatically and how to access the output of the data flow from other applications. As an advanced programming option, you can programmatically create an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package line by line as described in the topic, [Building Packages Programmatically](../../integration-services/building-packages-programmatically/building-packages-programmatically.md).  
  
 This section also discusses other administrative tasks that you can perform programmatically to manage stored packages, running packages, and package roles.  
  
## Running Packages on the Integration Services Server  
 When you deploy packages to the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] server, you can run the packages programmatically by using the <xref:Microsoft.SqlServer.Management.IntegrationServices> namespace. The Microsoft.SqlServer.Management.IntegrationServices assembly is compiled with .NET Framework 3.5. If you are building a .NET Framework 4.0 application, you might need to add the assembly reference directly to your project file.  
  
 You can also use the namespace to deploy and manage [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] projects on the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] server. For an overview of the namespace and code snippets, see the blog entry, [A Glimpse of the SSIS Catalog Managed Object Model](https://go.microsoft.com/fwlink/?LinkId=253122), on blogs.msdn.com.  
  
## In This Section  
 [Understanding the Differences between Local and Remote Execution](../../integration-services/run-manage-packages-programmatically/understanding-the-differences-between-local-and-remote-execution.md)  
 Discusses critical differences between executing a package locally or on the server.  
  
 [Loading and Running a Local Package Programmatically](../../integration-services/run-manage-packages-programmatically/loading-and-running-a-local-package-programmatically.md)  
 Describes how to execute an existing package from a client application on the local computer.  
  
 [Loading and Running a Remote Package Programmatically](../../integration-services/run-manage-packages-programmatically/loading-and-running-a-remote-package-programmatically.md)  
 Describes how to execute an existing package from a client application and to ensure that the package runs on the server.  
  
 [Loading the Output of a Local Package](../../integration-services/run-manage-packages-programmatically/loading-the-output-of-a-local-package.md)  
 Describes how to execute a package on the local computer and how to load the data flow output into a client application by using the DataReader destination and the DtsClient namespace.  
  
 [Enumerating Available Packages Programmatically](../../integration-services/run-manage-packages-programmatically/enumerating-available-packages-programmatically.md)  
 Describes how to discover available packages that are managed by the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service.  
  
 [Managing Packages and Folders Programmatically](../../integration-services/run-manage-packages-programmatically/managing-packages-and-folders-programmatically.md)  
 Describes how to create, rename, and delete both packages and folders.  
  
 [Managing Running Packages Programmatically](../../integration-services/run-manage-packages-programmatically/managing-running-packages-programmatically.md)  
 Describes how to list packages that are currently running, examine their properties, and stop a running package.  
  
 [Managing Package Roles Programmatically &#40;SSIS Service&#41;](../../integration-services/run-manage-packages-programmatically/managing-package-roles-programmatically-ssis-service.md)  
 Describes how to get or set information about the roles assigned to a package or a folder.  
  
## Reference  
 [Integration Services Error and Message Reference](../../integration-services/integration-services-error-and-message-reference.md)  
 Lists the predefined [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] error codes with their symbolic names and descriptions.  
  
## Related Sections  
 [Extending Packages with Scripting](../../integration-services/extending-packages-scripting/extending-packages-with-scripting.md)  
 Discusses how to extend the control flow by using the Script task, and how to extend the data flow by using the Script component.  
  
 [Extending Packages with Custom Objects](../../integration-services/extending-packages-custom-objects/extending-packages-with-custom-objects.md)  
 Discusses how to create program custom tasks, data flow components, and other package objects for use in multiple packages.  
  
 [Building Packages Programmatically](../../integration-services/building-packages-programmatically/building-packages-programmatically.md)  
 Discusses how to create, configure, and save [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] packages programmatically.  
  
## See Also  
 [SQL Server Integration Services](../../integration-services/sql-server-integration-services.md)  
  
  
