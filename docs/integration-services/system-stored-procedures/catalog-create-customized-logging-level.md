---
description: "catalog.create_customized_logging_level"
title: "catalog.create_customized_logging_level | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: "language-reference"
ms.assetid: 20b3ba0a-126f-49bf-b70f-61b2a0fcb750
author: chugugrace
ms.author: chugu
---
# catalog.create_customized_logging_level 

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]

[!INCLUDE [sqlserver2016](../../includes/applies-to-version/sqlserver2016.md)]

  Creates a new customized logging level. For more info about customized logging levels, see [Integration Services &#40;SSIS&#41; Logging](../../integration-services/performance/integration-services-ssis-logging.md).  
  
## Syntax  
  
```sql  
catalog.create_customized_logging_level [ @level_name = ] level_name  
    , [ @level_description = ] level_description  
    , [ @profile_value = ] profile_value  
    , [ @events_value = ] events_value  
    , [ @level_id = ] level_id OUT   
```  
  
## Arguments  
 [ @level_name = ] *level_name*  
 The name for the new existing customized logging level.  
  
 The *level_name* is **nvarchar(128)**.  
  
 [ @level_description = ] *level_description*  
 The description for the new existing customized logging level.  
  
 The *level_description* is **nvarchar(max)**.  
  
 [ @profile_value = ] *profile_value*  
 The statistics that you want the new customized logging level to log.  
  
 Valid values for statistics include the following. These values correspond to the values on the **Statistics** tab of the **Customized Logging Level Management** dialog box.  
  
-   Execution = 0  
  
-   Volume = 1  
  
-   Performance = 2    
  
 The *profile_value* is a **bigint**.  
  
 [ @events_value = ] *events_value*  
 The events that you want the new customized logging level to log.  
  
 Valid values for events include the following. These values correspond to the values on the **Events** tab of the **Customized Logging Level Management** dialog box.  
  
|Events without event context|Events with event context|  
|----------------------------------|-------------------------------|  
|OnVariableValueChanged = 0<br /><br /> OnExecutionStatusChanged = 1<br /><br /> OnPreExecute = 2<br /><br /> OnPostExecute = 3<br /><br /> OnPreValidate = 4<br /><br /> OnPostValidate = 5<br /><br /> OnWarning = 6<br /><br /> OnInformation = 7<br /><br /> OnError = 8<br /><br /> OnTaskFailed = 9<br /><br /> OnProgress = 10<br /><br /> OnQueryCancel = 11<br /><br /> OnBreakpointHit = 12<br /><br /> OnCustomEvent = 13<br /><br /> Diagnostic = 14<br /><br /> DiagnosticEx = 15<br /><br /> NonDiagnostic = 16|OnVariableValueChanged_IncludeContext = 32<br /><br /> OnExecutionStatusChanged_IncludeContext = 33<br /><br /> OnPreExecute_IncludeContext = 34<br /><br /> OnPostExecute_IncludeContext = 35<br /><br /> OnPreValidate_IncludeContext = 36<br /><br /> OnPostValidate_IncludeContext = 37<br /><br /> OnWarning_IncludeContext = 38<br /><br /> OnInformation_IncludeContext = 39<br /><br /> OnError_IncludeContext = 40<br /><br /> OnTaskFailed_IncludeContext = 41<br /><br /> OnProgress_IncludeContext = 42<br /><br /> OnQueryCancel_IncludeContext= 43<br /><br /> OnBreakpointHit_IncludeContext = 44<br /><br /> OnCustomEvent_IncludeContext = 45<br /><br /> Diagnostic_IncludeContext = 46<br /><br /> DiagnosticEx_IncludeContext = 47<br /><br /> NonDiagnostic_IncludeContext = 48|  
  
 The *events_value* is a **bigint**.  
  
 [ @level_id = ] *level_id* OUT  
 The id of the new customized logging level.  
  
 The *level_id* is a **bigint**.  
  
## Remarks  
 To combine multiple values in Transact-SQL for the *profile_value* or *events_value* argument, follow this example. To capture the OnError (8) and DiagnosticEx (15) events, the formula to calculate *events_value* is `2^8 + 2^15 = 33024`.  
  
## Return Codes  
 0 (success)  
  
 When the stored procedure fails, it throws an error.  
  
## Result Set  
 None  
  
## Permissions  
 This stored procedure requires one of the following permissions:  
  
-   Membership in the **ssis_admin** database role  
  
-   Membership in the **sysadmin** server role  
  
## Errors and Warnings  
 The following list describes conditions that cause the stored procedure to fail.  
  
-   The user does not have the required permissions.  
  
  
