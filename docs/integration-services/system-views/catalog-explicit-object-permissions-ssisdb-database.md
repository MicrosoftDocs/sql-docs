---
description: "catalog.explicit_object_permissions (SSISDB Database)"
title: "catalog.explicit_object_permissions (SSISDB Database) | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: "language-reference"
ms.assetid: 49b09e0f-06e8-451f-b979-a0d91000bfe3
author: chugugrace
ms.author: chugu
---
# catalog.explicit_object_permissions (SSISDB Database)

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]

 Displays only the permissions that have been explicitly assigned to the user.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|object_type|**smallint**|The type of securable object. Securable objects types include folder (`1`), project (`2`), environment (`3`), and operation (`4`).|  
|object_id|**bigint**|The unique identifier (ID) or primary key of the secured object.|  
|principal_id|**int**|The ID of the database principal.|  
|permission_type|**smallint**|The type of permission.|  
|is_deny|**bit**|Indicates whether the permission has been denied or granted. When the value is `1`, the permission has been denied. When the value is `0`, the permission has not been denied.|  
|grantor_id|**int**|The ID of the principal who granted the permission.|  
  
## Remarks  
 This view displays the permission types listed in the following table:  
  
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
  
## Permissions  
 This view does not give a complete view of permissions for the current principal. The user must also check whether the principal is a member of roles and groups that have permissions assigned.  
  
  
