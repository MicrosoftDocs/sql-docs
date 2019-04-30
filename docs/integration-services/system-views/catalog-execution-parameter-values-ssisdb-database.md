---
title: "catalog.execution_parameter_values (SSISDB Database) | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: "language-reference"
ms.assetid: ec93e67b-04ce-4aae-ab96-3ad20e9793ad
author: janinezhang
ms.author: janinez
manager: craigg
---
# catalog.execution_parameter_values (SSISDB Database)

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  Displays the actual parameter values that are used by [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] packages during an instance of execution.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|execution_parameter_id|**bigint**|Unique identifier (ID) of the execution parameter.|  
|execution_id|**bigint**|Unique ID for the instance of execution.|  
|object_type|**smallint**|When the value is `20`, the parameter is a project parameter. When the value is `30`, the parameter is a package parameter. When the value is `50`, the parameter is one of the following:<br /><br /> **LOGGING_LEVEL**<br /><br /> **DUMP_ON_ERROR**<br /><br /> **DUMP_ON_EVENT**<br /><br /> **DUMP_EVENT_CODE**<br /><br /> **CALLER_INFO**<br /><br /> **SYNCHRONIZED**|  
|parameter_data_type|**nvarchar(128)**|The data type of the parameter.|  
|parameter_name|**sysname**|The name of the parameter.|  
|parameter_value|**sql_variant**|The value of the parameter. When sensitive is `0`, the plaintext value is shown. When sensitive is `1`, the **NULL** value is displayed.|  
|sensitive|**bit**|When the value is `1`, the parameter value is sensitive. When the value is `0`, the parameter value is not sensitive.|  
|required|**bit**|When the value is `1`, the parameter value is required in order to start the execution. When the value is `0`, the parameter value is not required to start the execution.|  
|value_set|**bit**|When the value is `1`, the parameter value has been assigned. When the value is `0`, the parameter value has not been assigned.|  
|runtime_override|**bit**|When the value is `1`, the parameter value was changed from the original value before the execution was started. When the value is `0`, the parameter value is the original value that was set.|  
  
## Remarks  
 This view displays a row for each execution parameter in the catalog. An execution parameter value is the value that is assigned to a project parameter or package parameter during a single instance of execution.  
  
## Permissions  
 This view requires one of the following permissions:  
  
-   READ permission on the instance of execution  
  
-   Membership to the **ssis_admin** database role  
  
-   Membership to the **sysadmin** server role  
  
> [!NOTE]  
>  When you have permission to perform an operation on the server, you also have permission to view information about the operation. Row-level security is enforced; only rows that you have permission to view are displayed.  
  
  
