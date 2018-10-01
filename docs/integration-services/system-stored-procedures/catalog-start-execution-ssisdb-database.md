---
title: "catalog.start_execution (SSISDB Database) | Microsoft Docs"
ms.custom: ""
ms.date: "12/16/2016"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: "language-reference"
ms.assetid: f8663ff3-aa98-4dd8-b850-b21efada0b87
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# catalog.start_execution (SSISDB Database)
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  Starts an instance of execution in the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog.  
  
## Syntax  
  
```sql  
catalog.start_execution [@execution_id =] execution_id [, [@retry_count =] retry_count]  
```  
  
## Arguments  
 [@execution_id =] *execution_id*  
 The unique identifier for the instance of execution. The *execution_id* is **bigint**.
 
 [@retry_count =] *retry_count*  
 The retry count if the execution fails. It takes effect only if the execution is in Scale Out. This parameter is optional. If not specified, its value is set to 0. The *retry_count* is **int**.
  
## Remarks  
 An execution is used to specify the parameter values that is used by a package during a single instance of package execution. After an instance of execution has been created, before it has been started, the corresponding project might be redeployed. In this case, the instance of execution references a project that is outdated. This invalid reference causes the stored procedure to fail.  
  
> [!NOTE]  
>  Executions can only be started once. To start an instance of execution, it must be in the created state (a value of `1` in the **status** column of the [catalog.operations](../../integration-services/system-views/catalog-operations-ssisdb-database.md) view).  
  
## Example  
 The following example calls catalog.create_execution to create an instance of execution for the Child1.dtsx package. Integration Services Project1 contains the package. The example calls catalog.set_execution_parameter_value to set values for the Parameter1, Parameter2, and LOGGING_LEVEL parameters. The example calls catalog.start_execution to start an instance of execution.  
  
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
  
-   READ and MODIFY permissions on the instance of execution, READ and EXECUTE permissions on the project, and if applicable, READ permissions on the referenced environment  
  
-   Membership to the **ssis_admin** database role  
  
-   Membership to the **sysadmin** server role  
  
## Errors and Warnings  
 The following list describes some conditions that may raise an error or warning:  
  
-   The user does not have the appropriate permissions  
  
-   The execution identifier is not valid  
  
-   The execution has already been started, or it has already been completed; executions can be started only once  
  
-   The environment reference associated with the project is not valid  
  
-   Required parameter values have not been set  
  
-   The project version associated with the instance of execution is outdated; only the most current version of a project can be executed  
  
  
