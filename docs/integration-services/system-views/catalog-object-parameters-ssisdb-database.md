---
title: "catalog.object_parameters (SSISDB Database) | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: "language-reference"
ms.assetid: d7b04903-2d61-4159-9456-475942d1f732
author: janinezhang
ms.author: janinez
manager: craigg
---
# catalog.object_parameters (SSISDB Database)
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  Displays the parameters for all packages and projects in the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|parameter_id|**bigint**|The unique identifier (ID) of the parameter.|  
|project_id|**bigint**|The unique ID of the project.|  
|object_type|**smallint**|The type of parameter. The value is `20` for a project parameter and the value is `30` for a package parameter.|  
|object_name|**sysname**|The name of the corresponding project or package.|  
|parameter_name|**sysname(nvarchar(128))**|The name of the parameter.|  
|data_type|**nvarchar(128)**|The data type of the parameter.|  
|required|**bit**|When the value is `1`, the parameter value is required in order to start the execution. When the value is `0`, the parameter value is not required to start the execution.|  
|sensitive|**bit**|When the value is `1`, the parameter value is sensitive. When the value is `0`, the parameter value is not sensitive.|  
|description|**nvarchar(1024)**|An optional description of the package.|  
|design_default_value|**sql_variant**|The default value for the parameter that was assigned during the design of the project or package.|  
|default_value|**sql_variant**|The default value that is currently used on the server.|  
|value_type|**char(1)**|Indicates the type of parameter value. This field displays `V` when parameter_value is a literal value and `R` when the value is assigned by referencing an environment variable.|  
|value_set|**bit**|When the value is `1`, the parameter value has been assigned. When the value is `0`, the parameter value has not been assigned.|  
|referenced_variable_name|**nvarchar(128)**|The name of the environment variable that is assigned to the value of the parameter. The default value is **NULL**.|  
|validation_status|**char(1)**|Identified for informational purposes only. Not used in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].|  
|last_validation_time|**datetimeoffset(7)**|Identified for informational purposes only. Not used in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].|  
  
## Permissions  
 To see rows in this view, you must have one of the following permissions:  
  
-   READ permission on the project  
  
-   Membership to the **ssis_admin** database role  
  
-   Membership in the **sysadmin** server role.  
  
 Row-level security is enforced; only rows that you have permission to view are displayed.  
  
  
