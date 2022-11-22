---
description: "catalog.effective_object_permissions (SSISDB Database)"
title: "catalog.effective_object_permissions (SSISDB Database) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: "language-reference"
helpviewer_keywords: 
  - "catalog.effective_object_permissions views [Integration Services]"
  - "effective_object_permissions view [Integration Services]"
ms.assetid: e70c4ce9-79f5-44df-ac75-6c29b6e38776
author: chugugrace
ms.author: chugu
---
# catalog.effective_object_permissions (SSISDB Database)

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]

  Displays the effective permissions for the current principal for all objects in the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|object_type|**smallint**|The type of securable object. Securable objects types include folder (`1`), project (`2`), environment (`3`), and operation (`4`).|  
|object_id|**bigint**|The unique identifier (ID) or primary key of the object.|  
|permission_type|**smallint**|The type of permission.|  
  
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
  
 Only objects on which the caller has permissions are evaluated. The permissions are computed based on the following:  
  
-   Explicit permissions  
  
-   Inherited permissions  
  
-   Membership of the principal in roles  
  
-   Membership of the principal in groups  
  
## Permissions  
 Users can see effective permissions only for themselves and for roles of which they are members.  
  
  
