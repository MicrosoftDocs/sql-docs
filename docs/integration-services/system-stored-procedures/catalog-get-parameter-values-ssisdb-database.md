---
title: "catalog.get_parameter_values (SSISDB Database) | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: "language-reference"
ms.assetid: 5b1aeaf7-c938-4aef-bafc-e4d7a82eb578
author: janinezhang
ms.author: janinez
manager: craigg
---
# catalog.get_parameter_values (SSISDB Database)
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  Resolves and retrieves the default parameter values from a project and corresponding packages in the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog.  
  
## Syntax  
  
```sql  
catalog.get_parameter_values [ @folder_name = ] folder_name  
     , [ @project_name = ] project_name  
     , [ @package_name = ] package_name  
  [  , [ @reference_id = ] reference_id  ]  
  
```  
  
## Arguments  
 [ @folder_name = ] *folder_name*  
 The name of the folder that contains the project. The *folder_name* is **nvarchar(128)**.  
  
 [ @project_name = ] *project_name*  
 The name of the project where the parameters resides. The *project_name* is **nvarchar(128)**.  
  
 [ @package_name = ] *package_name*  
 The name of the package. Specify the package name to retrieve all project parameters and the parameters from a specific package. Use NULL to retrieve all project parameters and the parameters from all packages. The *package_name* is **nvarchar(260)**.  
  
 [ @reference_id = ] *reference_id*  
 The unique identifier of an environment reference. This parameter is optional. The *reference_id* is **bigint**.  
  
## Return Code Value  
 0 (success)  
  
## Result Sets  
 Returns a table that has the following format:  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|object_type|**smallint**|The type of parameter. The value is `20` for a project parameter and the value is `30` for a package parameter.|  
|parameter_data_type|**nvarchar(128)**|The data type of the parameter.|  
|parameter_name|**sysname**|The name of the parameter.|  
|parameter_value|**sql_variant**|The value of the parameter.|  
|sensitive|**bit**|When the value is `1`, the parameter value is sensitive. When the value is `0`, the parameter value is not sensitive.|  
|required|**bit**|When the value is `1`, the parameter value is required in order to start the execution. When the value is `0`, the parameter value is not required to start the execution.|  
|value_set|**bit**|When the value is `1`, the parameter value has been assigned. When the value is `0`, the parameter value has not been assigned.|  
  
> [!NOTE]  
>  Literal values are displayed in plaintext. **NULL** is displayed in place of sensitive values.  
  
## Permissions  
 This stored procedure requires one of the following permissions:  
  
-   READ permissions on the project and, if applicable, READ permission on the referenced environment  
  
-   Membership to the **ssis_admin** database role  
  
-   Membership to the **sysadmin** server role  
  
## Errors and Warnings  
 The following list describes some conditions that may raise an error or warning:  
  
-   The package cannot be found in the specified folder or project  
  
-   The user does not have the appropriate permissions  
  
-   The specified environment reference does not exist  
  
  
