---
title: "Deployment of Projects and Packages | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: bea8ce8d-cf63-4257-840a-fc9adceade8c
author: janinezhang
ms.author: janinez
manager: craigg
---
# Deployment of Projects and Packages
  [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] supports two deployment models, the project deployment model and the package deployment model. The project deployment model enables you to deploy your projects to the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] server.  
  
 For more information about deploying projects to the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] server, see [Deploy Projects to Integration Services Server](../deploy-projects-to-integration-services-server.md).  
  
 For more information about the package deployment model, see [Package Deployment &#40;SSIS&#41;](legacy-package-deployment-ssis.md).  
  
## Compare Project Deployment and Package Deployment  
 The type of deployment model that you choose for a project determines which development and administrative options are available for that project. The following table shows the differences and similarities between using the project deployment model and using the package deployment model.  
  
|When Using the Project Deployment Model|When Using the Package Deployment Model|  
|---------------------------------------------|---------------------------------------------|  
|A project is the unit of deployment.|A package is the unit of deployment.|  
|Parameters are used to assign values to package properties.|Configurations are used to assign values to package properties.|  
|A project, containing packages and parameters, is built to a project deployment file (.ispac extension).|Packages (.dtsx extension) and configurations (.dtsConfig extension) are saved individually to the file system.|  
|A project, containing packages and parameters, is deployed to the SSISDB catalog on an instance of SQL Server.|Packages and configurations are copied to the file system on another computer. Packages can also be saved to the MSDB database on an instance of SQL Server.|  
|CLR integration is required on the database engine.|CLR integration is not required on the database engine.|  
|Environment-specific parameter values are stored in environment variables.|Environment-specific configuration values are stored in configuration files.|  
|Projects and packages in the catalog can be validated on the server before execution. You can use SQL Server Management Studio, stored procedures, or managed code to perform the validation.|Packages are validated just before execution. You can also validate a package with dtExec or managed code.|  
|Packages are executed by starting an execution on the database engine. A project identifier, explicit parameter values (optional), and environment references (optional) are assigned to an execution before it is started.<br /><br /> You can also execute packages using `dtExec`.|Packages are executed using the `dtExec` and `DTExecUI` execution utilities. Applicable configurations are identified by command-prompt arguments (optional).|  
|During execution, events that are produced by the package are captured automatically and saved to the catalog. You can query these events with Transact-SQL views.|During execution, events that are produced by a package are not captured automatically. A log provider must be added to the package to capture events.|  
|Packages are run in a separate Windows process.|Packages are run in a separate Windows process.|  
|SQL Server Agent is used to schedule package execution.|SQL Server Agent is used to schedule package execution.|  
  
## Features of Project Deployment Model  
 The following table lists the features that are available to projects developed only for the project deployment model.  
  
|Feature|Description|  
|-------------|-----------------|  
|Parameters|A parameter specifies the data that will be used by a package. You can scope parameters to the package level or project level with package parameters and project parameters, respectively. Parameters can be used in expressions or tasks. When the project is deployed to the catalog, you can assign a literal value for each parameter or use the default value that was assigned at design time. In place of a literal value, you can also reference an environment variable. Environment variable values are resolved at the time of package execution.|  
|Environments|An environment is a container of variables that can be referenced by [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] projects. Each project can have multiple environment references, but a single instance of package execution can only reference variables from a single environment. Environments allow you to organize the values that you assign to a package. For example, you might have environments named "Dev", "test", and "Production".|  
|Environment variables|An environment variable defines a literal value that can be assigned to a parameter during package execution. To use an environment variable, create an environment reference (in the project that corresponds to the environment having the parameter), assign a parameter value to the name of the environment variable, and specify the corresponding environment reference when you configure an instance of execution.|  
|SSISDB catalog|All [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] objects are stored and managed on an instance of SQL Server in a database referred to as the SSISDB catalog. The catalog allows you to use folders to organize your projects and environments. Each instance of SQL Server can have one catalog. Each catalog can have zero or more folders. Each folder can have zero or more projects and zero or more environments. A folder in the catalog can also be used as a boundary for permissions to [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] objects.|  
|Catalog stored procedures and views|A large number of stored procedures and views can be used to manage [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] objects in the catalog. For example, you can specify values to parameters and environment variables, create and start executions, and monitor catalog operations. You can even see exactly which values will be used by a package before execution starts.|  
  
## Project Deployment  
 At the center of the project deployment model is the project deployment file (.ispac extension). The project deployment file is a self-contained unit of deployment that includes only the essential information about the packages and parameters in the project. The project deployment file does not capture all of the information contained in the Integration Services project file (.dtproj extension). For example, additional text files that you use for writing notes are not stored in the project deployment file and thus are not deployed to the catalog.  
  
## Required Tasks  
  
-   [Deploy Projects to Integration Services Server](../deploy-projects-to-integration-services-server.md)  
  
## Related Content  
 Blog entry, [Thoughts on Branching Strategies for SSIS Projects](https://go.microsoft.com/fwlink/?LinkId=245739), on mattmasson.com.  
  
## See Also  
 [dtexec Utility](dtexec-utility.md)  
  
  
