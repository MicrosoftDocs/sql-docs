---
title: "Integration Services (SSIS) Parameters | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: 9ed9ca8e-8b1e-48d9-907d-285516d6562b
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Integration Services (SSIS) Parameters
  [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] (SSIS) parameters allow you to assign values to properties within packages at the time of package execution. You can create *project parameters* at the project level and *package parameters* at the package level. Project parameters are used to supply any external input the project receives to one or more packages in the project. Package parameters allow you to modify package execution without having to edit and redeploy the package.  
  
 In [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)] you create, modify, or delete project parameters by using the **Project.params** window. You create, modify, and delete package parameters by using the **Parameters** tab in the [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer. You associate a new or an existing parameter with a task property by using the **Parameterize** dialog box. For more about using the **Project.params** window and the **Parameters** tab, see [Create Parameters](create-parameters.md). For more information about the **Parameterize** dialog box, see [Parameterize Dialog Box](parameterize-dialog-box.md).  
  
## Parameters and Package Deployment Model  
 In general, if you are deploying a package using the package deployment model, you should use configurations instead of parameters.  
  
 When you deploy a package that contains parameters using the package deployment model and then execute the package, the parameters are not called during execution. If the package contains package parameters and expressions within the package use the parameters, the resulting values are applied at runtime. If the package contains project parameters, the package execution may fail.  
  
## Parameters and Project Deployment Model  
 When you deploy a project to the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] server, you use views, stored procedures, and the [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] UI to manage project and package parameters. For more information, see the following topics.  
  
-   [Views &#40;Integration Services Catalog&#41;](/sql/integration-services/system-views/views-integration-services-catalog)  
  
-   [Stored Procedures &#40;Integration Services Catalog&#41;](/sql/integration-services/system-stored-procedures/stored-procedures-integration-services-catalog)  
  
-   [Configure Dialog Box](catalog/configure-dialog-box.md)  
  
-   [Execute Package Dialog Box](../../2014/integration-services/execute-package-dialog-box.md)  
  
### Parameter Values  
 You can assign up to three different types of values to a parameter. When a package execution is started, a single value is used for the parameter, and the parameter is resolved to its final literal value.  
  
 The following table lists the types of values.  
  
|Value Name|Description|Type of value|  
|----------------|-----------------|-------------------|  
|Execution Value|The value that is assigned to a specific instance of package execution. This assignment overrides all other values, but applies to only a single instance of package execution.|Literal|  
|Server Value|The value assigned to the parameter within the scope of the project, after the project is deployed to the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] server. This value overrides the design default.|Literal or Environment Variable Reference|  
|Design Value|The value assigned to the parameter when the project is created or edited in [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)]. This value persists with the project.|Literal|  
  
 You can use a single parameter to assign a value to multiple package properties. A single package property can be assigned a value only from a single parameter.  
  
###  <a name="executions"></a> Executions and Parameter Values  
 The *execution* is an object that represents a single instance of package execution. When you create an execution, you specify all of the details necessary to run a package such as execution parameter values. You can also modify the parameters values for existing executions.  
  
 When you explicitly set an execution parameter value, the value is applicable only to that particular instance of execution. The execution value is used instead of a server value or a design value. If you do not explicitly set an execution value, and a server value has been specified, the server value is used.  
  
 When a parameter is marked as required, a server value or execution value must be specified for that parameter. Otherwise, the corresponding package does not execute. Although the parameter has a default value at design time, it will never be used once the project is deployed.  
  
#### Environment Variables  
 If a parameter references an environment variable, the literal value from that variable is resolved through the specified environment reference and applied to the parameter. The final literal parameter value that is used for package execution is referred to as the execution parameter value. You specify the environment reference for an execution by using the **Execute** dialog box  
  
 If a project parameter references an environment variable and the literal value from the variable cannot be resolved at execution, the design value is used. The server value is not used.  
  
 To view the environment variables that are assigned to parameter values, query the catalog.object_parameters view. For more information, see [catalog.object_parameters &#40;SSISDB Database&#41;](/sql/integration-services/system-views/catalog-object-parameters-ssisdb-database).  
  
#### Determining Execution Parameter Values  
 The following Transact-SQL views and stored procedure can be used to display and set parameter values.  
  
 [catalog.execution_parameter_values &#40;SSISDB Database&#41;](/sql/integration-services/system-views/catalog-execution-parameter-values-ssisdb-database)(view)  
 Shows the actual parameter values that will be used by a specific execution  
  
 [catalog.get_parameter_values &#40;SSISDB Database&#41;](/sql/integration-services/system-stored-procedures/catalog-get-parameter-values-ssisdb-database) (stored procedure)  
 Resolves and shows the actual values for the specified package and environment reference  
  
 [catalog.object_parameters &#40;SSISDB Database&#41;](/sql/integration-services/system-views/catalog-object-parameters-ssisdb-database) (view)  
 Displays the parameters and properties for all packages and projects in the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] catalog, including the design default and server default values.  
  
 [catalog.set_execution_parameter_value &#40;SSISDB Database&#41;](/sql/integration-services/system-stored-procedures/catalog-set-execution-parameter-value-ssisdb-database)  
 Sets the value of a parameter for an instance of execution in the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] catalog.  
  
 You can also use the **Execute Package** dialog box in [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)] modify the parameter value. For more information, see [Execute Package Dialog Box](../../2014/integration-services/execute-package-dialog-box.md).  
  
 You can also use the dtexec `/Parameter` option to modify a parameter value. For more information, see [dtexec Utility](packages/dtexec-utility.md).  
  
### Parameter Validation  
 If parameter values cannot be resolved, the corresponding package execution will fail. To help avoid failures, you can validate projects and packages by using the **Validate** dialog box in [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)]. Validation allows you to confirm that all parameters have the necessary values or can resolve the necessary values with specific environment references. Validation also checks for other common package issues.  
  
 For more information, see [Validate Dialog Box](catalog/validate-dialog-box.md).  
  
### Parameter Example  
 This example describes a parameter named **pkgOptions** that is used to specify options for the package in which it resides.  
  
 During design time, when the parameter was created in [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)], a default value of 1 was assigned to the parameter. This default value is referred to as the design default. If the project was deployed to the SSISDB catalog and no other values were assigned to this parameter, the package property corresponding to the **pkgOptions** parameter would be assigned the value of 1 during package execution. The design default persists with the project throughout its life cycle.  
  
 While preparing a specific instance of package execution, a value of 5 is assigned to the **pkgOptions** parameter. This value is referred to as the execution value because it applies to the parameter only for that particular instance of execution. When execution starts, the package property corresponding to the **pkgOptions** parameter is assigned the value of 5.  
  
## Related Tasks  
 [Create Parameters](create-parameters.md)  
  
 [Set Parameter Values After the Project Is Deployed](../../2014/integration-services/set-parameter-values-after-the-project-is-deployed.md)  
  
## Related Content  
 Blog entry, [SSIS Quick Tip: Required Parameters](https://go.microsoft.com/fwlink/?LinkId=239781), on mattmasson.com.  
  
  
