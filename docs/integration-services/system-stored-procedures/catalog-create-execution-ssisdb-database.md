---
title: "catalog.create_execution (SSISDB Database) | Microsoft Docs"
ms.custom: ""
ms.date: "12/16/2016"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: "language-reference"
ms.assetid: 45d0c2f6-1f38-445f-ac06-e2a01f6ac600
author: janinezhang
ms.author: janinez
manager: craigg
---
# catalog.create_execution (SSISDB Database)

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  Creates an instance of execution in the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog.  
  
 This stored procedure uses the default server logging level.  
  
## Syntax  
  
```sql  
catalog.create_execution [@folder_name = folder_name  
     , [@project_name =] project_name  
     , [@package_name =] package_name  
  [  , [@reference_id =] reference_id ]  
  [  , [@use32bitruntime =] use32bitruntime ] 
  [  , [@runinscaleout =] runinscaleout ]
  [  , [@useanyworker =] useanyworker ] 
     , [@execution_id =] execution_id OUTPUT  
```  
  
## Arguments  
 [@folder_name =] *folder_name*  
 The name of the folder that contains the package that is to be executed. The *folder_name* is **nvarchar(128)**.  
  
 [@project_name =] *project_name*  
 The name of the project that contains the package that is to be executed. The *project_name* is **nvarchar(128)**.  
  
 [@package_name =] *package_name*  
 The name of the package that is to be executed. The *package_name* is **nvarchar(260)**.  
  
 [@reference_id =] *reference_id*  
 A unique identifier for an environment reference. This parameter is optional. The *reference_id* is **bigint**.  
  
 [@use32bitruntime =] *use32bitruntime*  
 Indicates if the 32-bit runtime should be used to run the package on a 64-bit operating system. Use the value of 1 to execute the package with the 32-bit runtime when running on a 64-bit operating system. Use the value of 0 to execute the package with the 64-bit runtime when running on a 64-bit operating system. This parameter is optional. The *Use32bitruntime* is **bit**.  
 
 [@runinscaleout =] *runinscaleout*  
 Indicate whether the execution is in Scale Out. Use the value of 1 to execute the package in Scale Out. Use the value of 0 to execute the package without Scale Out. This parameter is optional. If not specified, its value is set to DEFAULT_EXECUTION_MODE in [SSISDB].[catalog].[catalog_properties]. The *runinscaleout* is **bit**. 
 
[@useanyworker =] *useanyworker*  
Indicate whether any Scale Out Worker is allowed to do the execution.

-   Use the value of 1 to execute the package with any Scale Out Worker. When you set `@useanyworker` to true, any worker whose maximum task count (as specified in the worker configuration file) is not yet reached is available to run the package. For info about the worker configuration file, see [Integration Services (SSIS) Scale Out Worker](../scale-out/integration-services-ssis-scale-out-worker.md).

-   Use the value of 0 to indicate that not all Scale Out Workers are allowed to execute the package. When you set `@useanyworker` to false, you have to specify the workers that are allowed to run the package by using Scale Out Manager or by calling the stored procedure `[catalog].[add_execution_worker]`. If you specify a worker that's already running another package, the worker finishes running the current package before it requests another execution.

This parameter is optional. If not specified, its value is set to 1. The *useanyworker* is **bit**. 
  
 [@execution_id =] *execution_id*  
 Returns the unique identifier for an instance of execution. The *execution_id* is **bigint**.  

  
## Remarks  
 An execution is used to specify the parameter values that are a package uses during a single instance of package execution.  
  
 If an environment reference is specified with the *reference_id* parameter, the stored procedure populates the project and package parameters with literal values or referenced values from the corresponding environment variables. If environment reference is specified, default parameter values are used during package execution. To determine exactly which values are used for a particular instance of execution, use the *execution_id* output parameter value from this stored procedure and query the [execution_parameter_values](../../integration-services/system-views/catalog-execution-parameter-values-ssisdb-database.md) view.  
  
 Only packages that are marked as entry point packages can be specified in an execution. If a package that is not an entry point is specified, the execution fails.  
  
## Example  
 The following example calls catalog.create_execution to create an instance of execution for the Child1.dtsx package, which is not in Scale Out. Integration Services Project1 contains the package. The example calls catalog.set_execution_parameter_value to set values for the Parameter1, Parameter2, and LOGGING_LEVEL parameters. The example calls catalog.start_execution to start an instance of execution.  
  
```sql  
Declare @execution_id bigint  
EXEC [SSISDB].[catalog].[create_execution] @package_name=N'Child1.dtsx', @execution_id=@execution_id OUTPUT, @folder_name=N'TestDeply4', @project_name=N'Integration Services Project1', @use32bitruntime=False, @reference_id=Null  
Select @execution_id  
DECLARE @var0 sql_variant = N'Child1.dtsx'  
EXEC [SSISDB].[catalog].[set_execution_parameter_value] @execution_id, @object_type=20, @parameter_name=N'Parameter1', @parameter_value=@var0  
DECLARE @var1 sql_variant = N'Child2.dtsx'  
EXEC [SSISDB].[catalog].[set_execution_parameter_value] @execution_id, @object_type=20, @parameter_name=N'Parameter2', @parameter_value=@var1  
DECLARE @var2 smallint = 1  
EXEC [SSISDB].[catalog].[set_execution_parameter_value] @execution_id, @object_type=50, @parameter_name=N'LOGGING_LEVEL', @parameter_value=@var2  
EXEC [SSISDB].[catalog].[start_execution] @execution_id  
GO  
```  
  
## Return Code Value  
 0 (success)  
  
## Result Sets  
 None  
  
## Permissions  
 This stored procedure requires one of the following permissions:  
  
-   READ and EXECUTE permissions on the project and, if applicable, READ permissions on the referenced environment  
  
-   Membership to the **ssis_admin** database role  
  
-   Membership to the **sysadmin** server role  

 If @runinscaleout is 1, the stored procedure requires one of the following permissions:
 
-   Membership to the **ssis_admin** database role

-   Membership to the **ssis_cluster_executor** database role

-   Membership to the **sysadmin** server role
  
## Errors and Warnings  
 The following list describes some conditions that can raise an error or warning:  
  
-   The package does not exist.  
  
-   The user does not have the appropriate permissions.  
  
-   The environment reference, *reference_id*, is not valid.  
  
-   The package that is specified is not an entry point package.  
  
-   The data type of the referenced environment variable is different from the data type of the project or package parameter.  
  
-   The project or package contains parameters that require values, but no values have been assigned.  
  
-   The referenced environment variables cannot be found in the environment that the environment reference, *reference_id*, specifies.  
  
## See Also  
 [catalog.start_execution &#40;SSISDB Database&#41;](../../integration-services/system-stored-procedures/catalog-start-execution-ssisdb-database.md)   
 [catalog.set_execution_parameter_value &#40;SSISDB Database&#41;](../../integration-services/system-stored-procedures/catalog-set-execution-parameter-value-ssisdb-database.md)  
 [catalog.add_execution_worker &#40;SSISDB Database&#41;](../../integration-services/system-stored-procedures/catalog-add-execution-worker-ssisdb-database.md)  
  
