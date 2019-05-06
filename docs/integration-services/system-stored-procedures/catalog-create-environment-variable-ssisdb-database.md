---
title: "catalog.create_environment_variable (SSISDB Database) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: "language-reference"
ms.assetid: 91ed017b-6567-4bf2-b9f1-e2b5c70a5343
author: janinezhang
ms.author: janinez
manager: craigg
---
# catalog.create_environment_variable (SSISDB Database)
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  Create an environment variable in the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog.  
  
## Syntax  
  
```sql  
catalog.create_environment_variable [@folder_name =] folder_name  
    , [@environment_name =] environment_name  
    , [@variable_name =] variable_name  
    , [@data_type =] data_type  
    , [@sensitive =] sensitive  
    , [@value =] value  
    , [@description =] description  
```  
  
## Arguments  
 [@folder_name =] *folder_name*  
 The name of the folder that contains the environment. The *folder_name* is **nvarchar(128)**.  
  
 [@environment_name =] *environment_name*  
 The name of the environment. The *environment_name* is **nvarchar(128)**.  
  
 [@variable_name =] *variable_name*  
 The name of the environment variable. The *variable_name* is **nvarchar(128)**.  
  
 [@data_type =] *data_type*  
 The data type of the variable. Supported environment variable data types include **Boolean**, **Byte**, **DateTime**, **Double**, **Int16**, **Int32**, **Int64**, **Single**, **String**, **UInt32**, and **UInt64**. Unsupported environment variable data types include **Char**, **DBNull**, **Object**, and **Sbyte**. The data type of the *data_type* parameter is **nvarchar(128)**.  
  
 [@sensitive =] *sensitive*  
 Indicates whether the variable contains a sensitive value or not. Use a value of `1` to indicate that the value of the environment variable is sensitive or a value of `0` to indicate that it is not. A sensitive value is encrypted when it is stored. A value that is not sensitive is stored in plaintext.*Sensitive* is **bit**.  
  
 [@value =] *value*  
 The value of the environment variable. The *value* is **sql_variant**.  
  
 [@description =] *description*  
 The description of the environment variable. The *value* is **nvarchar(1024)**.  
  
## Return Code Value  
 0 (success)  
  
## Result Sets  
 None  
  
## Permissions  
 This stored procedure requires one of the following permissions:  
  
-   READ and MODIFY permissions on the environment  
  
-   Membership to the **ssis_admin** database role  
  
-   Membership to the **sysadmin** server role  
  
## Errors and Warnings  
 The following list describes some conditions that may raise an error or warning:  
  
-   The folder name, environment name, or environment variable name is not valid  
  
-   The variable name already exists in the environment  
  
-   The user does not have the appropriate permissions  
  
## Remarks  
 An environment variable can be used to efficiently assign a value to a project parameter or package parameter for use in the execution of a package. Environment variables enable the organization of parameter values. Variable names must be unique within an environment.  
  
 The stored procedure validates the data type of the variable to make sure it is supported by the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog.  
  
> [!TIP]  
>  Consider using the **Int16** data type in [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] instead of the unsupported **Sbyte** data type.  
  
 The value passed to this stored procedure with the *value* parameter is converted from an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] data type to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data type according to the following table:  
  
|Integration Services Data Type|SQL Server Data Type|  
|------------------------------------|--------------------------|  
|**Boolean**|**bit**|  
|**Byte**|**binary**, **varbinary**|  
|**DateTime**|**datetime**, **datetime2**, **datetimeoffset**, **smalldatetime**|  
|**Double**|Exact numeric: **decimal**, **numeric**; Approximate numeric: **float**, **real**|  
|**Int16**|**smallint**|  
|**Int32**|**int**|  
|**Int64**|**bigint**|  
|**Single**|Exact numeric: **decimal**, **numeric**; Approximate numeric: **float**, **real**|  
|**String**|**varchar**, **nvarchar**, **char**|  
|**UInt32**|**int** (**int** is the closest available mapping to **Uint32**.)|  
|**UInt64**|**bigint** (**int** is the closest available mapping to **Uint64**.)|  
  
  
