---
title: "catalog.execution_property_override_values | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: "language-reference"
ms.assetid: 83cbdd6f-ddde-47bf-abde-36bd24272621
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# catalog.execution_property_override_values
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  Displays the property override values that were set during execution of the package.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|property_id|**bigint**|Unique ID for the property override value.|  
|execution_id|**bigint**|Unique ID for the instance of execution.|  
|property_path|**nvarchar(4000)**|The path to the property in the package.|  
|property_value|**nvarchar(max)**|The override value of the property.|  
|sensitive|**bit**|When the value is 1, the property is sensitive and is encrypted when it is stored. When the value is 0, the property is not sensitive and the value is stored in plaintext.|  
  
## Remarks  
 This view displays a row for each execution in which property values were overridden using the **Property overrides** section in the **Advanced** tab of the **Execute Package** dialog. The path to the property is derived from the **Package Path** property of the package task.  
  
## Permissions  
 This view requires one of the following permissions:  
  
-   READ permission on the instance of execution  
  
-   Membership to the **ssis_admin** database role  
  
-   Membership to the **sysadmin** server role  
  
> [!NOTE]  
>  When you have permission to perform an operation on the server, you also have permission to view information about the operation. Row-level security is enforced; only rows that you have permission to view are displayed.  
  
  
