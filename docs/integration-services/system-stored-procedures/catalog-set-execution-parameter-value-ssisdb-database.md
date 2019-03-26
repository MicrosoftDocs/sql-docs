---
title: "catalog.set_execution_parameter_value (SSISDB Database) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: "language-reference"
ms.assetid: 055d86c9-befd-4e63-acb1-6dfe833549d2
author: janinezhang
ms.author: janinez
manager: craigg
---
# catalog.set_execution_parameter_value (SSISDB Database)
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  Sets the value of a parameter for an instance of execution in the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog.  
  
 A parameter value cannot be changed after an instance of execution has started.  
  
## Syntax  
  
```sql  
catalog.set_execution_parameter_value [ @execution_id = execution_id  
    , [ @object_type = ] object_type  
    , [ @parameter_name = ] parameter_name  
    , [ @parameter_value = ] parameter_value  
```  
  
## Arguments  
 [ @execution_id = ] *execution_id*  
 The unique identifier for the instance of execution. The *execution_id* is **bigint**.  
  
 [ @object_type = ] *object_type*  
 The type of parameter.  
  
 For the following parameters, set *object_type* to 50  
  
-   LOGGING_LEVEL  
  
-   CUSTOMIZED_LOGGING_LEVEL  
  
-   DUMP_ON_ERROR  
  
-   DUMP_ON_EVENT  
  
-   DUMP_EVENT_CODE  
  
-   CALLER_INFO  
  
-   SYNCHRONIZED  
  
 Use the value `20` to indicate a project parameter or the value `30` to indicate a package parameter.  
  
 The *object_type* is **smallint**.  
  
 [ @parameter_name = ] *parameter_name*  
 The name of the parameter. The *parameter_name* is **nvarchar(128)**.  
  
 [ @parameter_value = ] *parameter_value*  
 The value of the parameter. The *parameter_value* is **sql_variant**.  
  
## Remarks  
 To find out the parameter values that were used for a given execution, query the catalog.execution_parameter_values view.  
  
 To specify the scope of information that is logged during a package execution, set *parameter_name* to LOGGING_LEVEL and set *parameter_value* to one of the following values.  
  
 Set the *object_type* parameter to 50.  
  
|Value|Description|  
|-----------|-----------------|  
|0|None<br /><br /> Logging is turned off. Only the package execution status is logged.|  
|1|Basic<br /><br /> All events are logged, except custom and diagnostic events. This is the default value.|  
|2|Performance<br /><br /> Only performance statistics, and OnError and OnWarning events, are logged.|  
|3|Verbose<br /><br /> All events are logged, including custom and diagnostic events. <br />Custom events include those events that are logged by Integration Services tasks. For more information, see [Custom Messages for Logging](../../integration-services/performance/integration-services-ssis-logging.md#custom_messages)|  
|4|Runtime lineage<br /><br /> Collects the data required to track lineage in the data flow.|  
|100|Custom logging level<br /><br /> Specify the settings in the CUSTOMIZED_LOGGING_LEVEL parameter. For more info about the values that you can specify, see [catalog.create_customized_logging_level](../../integration-services/system-stored-procedures/catalog-create-customized-logging-level.md).<br /><br /> For more info about customized logging levels, see [Enable Logging for Package Execution on the SSIS Server](../../integration-services/performance/integration-services-ssis-logging.md#server_logging).|  
  
 To specify that the Integration Services server generates dump files when any error occurs during a package execution, set the following parameter values for an execution instance that hasn't run.  
  
|Parameter|Value|  
|---------------|-----------|  
|*execution_id*|The unique identifier for the instance of execution|  
|*object_type*|50|  
|*parameter_name*|'DUMP_ON_ERROR|  
|*parameter_value*|1|  
  
 To specify that the Integration Services server generates dump files when events occur during a package execution, set the following parameter values for an execution instance that hasn't run.  
  
|Parameter|Value|  
|---------------|-----------|  
|*execution_id*|The unique identifier for the instance of execution|  
|*object_type*|50|  
|*parameter_name*|'DUMP_ON_EVENT|  
|*parameter_value*|1|  
  
 To specify the events during package execution that cause the Integration Services server to generate dump files, set the following parameter values for an execution instance that hasn't run. Separate multiple event codes using a semi-colon.  
  
|Parameter|Value|  
|---------------|-----------|  
|*execution_id*|The unique identifier for the instance of execution|  
|*object_type*|50|  
|*parameter_name*|DUMP_EVENT_CODE|  
|*parameter_value*|One or more event codes|  
  
## Example  
 The following example specifies that the Integration Services server generates dump files when any error occurs during a package execution.  
  
```sql
exec catalog.create_execution  'TR2','Recurring ETL', 'Dim_DCVendor.dtsx',NULL, 0,@execution_id out  
exec catalog.set_execution_parameter_value  @execution_id, 50, 'DUMP_ON_ERROR',1  
```  
  
## Example  
 The following example specifies that the Integration Services server generates dump files when events occur during a package execution, and specifies the event that causes the server to generate the files.  
  
```sql
exec catalog.create_execution  'TR2','Recurring ETL', 'Dim_DCVendor.dtsx',NULL, 0,@execution_id out  
exec catalog.set_execution_parameter_value  @execution_id, 50, 'DUMP_ON_EVENT',1  
  
declare @event_code nvarchar(50)  
set @event_code = '0xC020801C'  
exec catalog.set_execution_parameter_value  @execution_id, 50, 'DUMP_EVENT_CODE', @event_code  
```  
  
## Return Code Value  
 0 (success)  
  
## Result Sets  
 None  
  
## Permissions  
 This stored procedure requires one of the following permissions:  
  
-   READ and MODIFY permissions on the instance of execution  
  
-   Membership to the **ssis_admin** database role  
  
-   Membership to the **sysadmin** server role  
  
## Errors and Warnings  
 The following list describes some conditions that may raise an error or warning:  
  
-   The user does not have the appropriate permissions  
  
-   The execution identifier is not valid  
  
-   The parameter name is not valid  
  
-   The data type of the parameter value does not match the data type of the parameter  
  
## See Also  
 [catalog.execution_parameter_values &#40;SSISDB Database&#41;](../../integration-services/system-views/catalog-execution-parameter-values-ssisdb-database.md)   
 [Generating Dump Files for Package Execution](../../integration-services/troubleshooting/generating-dump-files-for-package-execution.md)  
  
  
