---
title: "catalog.revoke_permission (SSISDB Database) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: "language-reference"
helpviewer_keywords: 
  - "revoke_permission stored procedure [Integration Services]"
  - "catalog.revoke_permission stored procedure [Integration Services]"
ms.assetid: 850b9c26-5c7c-47b9-a61c-5cf9bb5948cf
author: janinezhang
ms.author: janinez
manager: craigg
---
# catalog.revoke_permission (SSISDB Database)
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  Revokes a permission on a securable object in the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog.  
  
## Syntax  
  
```sql
catalog.revoke_permission [ @object_type = ] object_type  
    , [ @object_id = ] object_id  
    , [ @principal_id = ] principal_id  
    , [ @permission_type = ] permission_type  
```  
  
## Arguments  
 [ @object_type = ] *object_type*  
 The type of securable object. Securable objects types include folder (`1`), project (`2`), environment (`3`), and operation (`4`).The *object_type* is **smallint**_._  
  
 [ @object_id = ] *object_id*  
 The unique identitifier (ID) of the securable object. The *object_id* is **bigint**.  
  
 [ @principal_id = ] *principal_id*  
 The ID of the principal to be revoked permission. The *principal_id* is **int**.  
  
 [ @permission_type = ] *permission_type*  
 The type of permission. The *permission_type* is **smallint**.  
  
## Return Code Values  
 0 (success)  
  
 1 (object_class is not valid)  
  
 2 (object_id does not exist)  
  
 3 (principal does not exist)  
  
 4 (permission is not valid)  
  
 5 (other error)  
  
## Result Sets  
 None  
  
## Remarks  
 None  
  
## Permissions  
 This stored procedure requires one of the following permissions:  
  
-   ASSIGN_PERMISSIONS permissions on the object  
  
-   Membership to the **ssis_admin** database role  
  
-   Membership to the **sysadmin** server role  
  
## Remarks  
 If permission_type is specified, the stored procedure removes the permission that is explicitly assigned to the principal for the object. Even if there are no such instances, the procedure returns a success code value (`0`). If permission_type is omitted, the stored procedure removes all permissions of the principal to the object.  
  
> [!NOTE]  
>  The principal may still have the specified permission on the object if the principal is a member of a role that has the specified permission.  
  
 This stored procedure allows you to revoke the permission types described in the following table:  
  
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
  
  
