---
description: "catalog.grant_permission (SSISDB Database)"
title: "catalog.grant_permission (SSISDB Database) | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: "language-reference"
helpviewer_keywords: 
  - "grant_permission stored procedure [Integration Services]"
  - "catalog.grant_permission stored procedure [Integration Services]"
ms.assetid: e72cfd52-de66-45e9-98b9-b8580ac7b956
author: chugugrace
ms.author: chugu
---
# catalog.grant_permission (SSISDB Database)

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]


[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Grants a permission on a securable object in the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog.  
  
## Syntax  
  
```sql
catalog.grant_permission [ @object_type = ] object_type  
    , [ @object_id = ] object_id  
    , [ @principal_id = ] principal_id  
    , [ @permission_type = ] permission_type  
```  
  
## Arguments  
 [ @object_type = ] *object_type*  
 The type of securable object. Securable objects types include folder (`1`), project (`2`), environment (`3`), and operation (`4`).The *object_type* is **smallint**_._  
  
 [ @object_id = ] *object_id*  
 The unique identifier (ID) of the securable object. The *object_id* is **bigint**.  
  
 [ @principal_id = ] *principal_id*  
 The ID of the principal to be granted permission. The *principal_id* is **int**.  
  
 [ @permission_type = ] *permission_type*  
 The type of permission to be granted. The *permission_type* is **smallint**.  
  
## Return Code Values  
 0 (success)  
  
 1 (object_class is invalid)  
  
 2 (object_id does not exist)  
  
 3 (principal does not exist)  
  
 4 (permission is invalid)  
  
 5 (other error)  
  
## Result Sets  
 None  
  
## Permissions  
 This stored procedure requires one of the following permissions:  
  
-   ASSIGN_PERMISSIONS permissions on the object  
  
-   Membership to the **ssis_admin** database role  
  
-   Membership to the **sysadmin** server role  

This procedure cannot be called by logins that were authenticated by SQL Server. It cannot be called by the sa login.
  
## Remarks  
 This stored procedure allows you to grant the permission types described in the following table:  
  
|permission_type Value|Permission Name|Permission Description|Applicable Object Types|  
|----------------------------|---------------------|----------------------------|-----------------------------|  
|`1`|READ|Allows the principal to read information that is considered part of the object, such as properties. It does not allow the principal to enumerate or read the contents of other objects contained within the object.|Folder, Project, Environment, Operation|  
|`2`|MODIFY|Allows the principal to modify information that is considered part of the object, such as properties. It does not allow the principal to modify other objects contained within the object.|Folder, Project, Environment, Operation|  
|`3`|EXECUTE|Allows the principal to execute all packages in the project.|Project|  
|`4`|MANAGE_PERMISSIONS|Allows the principal to assign permissions to the objects.|Folder, Project, Environment, Operation|  
|`100`|CREATE_OBJECTS|Allows the principal to create objects in the folder.|Folder|  
|`101`|READ_OBJECTS|Allows the principal to read all objects in the folder.|Folder|  
|`102`|MODIFY_OBJECTS|Allows the principal to modify all objects in the folder.|Folder|  
|`103`|EXECUTE_OBJECTS|Allows the principal to execute all packages from all projects in the folder.|Folder|  
|`104`|MANAGE_OBJECT_PERMISSIONS|Allows the principal to manage permissions on all objects in the folder.|Folder|  
  
## Errors and Warnings  
 See the Return Code Values section for relevant errors and messages.  
  
  
