---
description: "Collection Permissions (Master Data Services)"
title: Collection Permissions
ms.custom: ""
ms.date: "03/01/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: master-data-services
ms.topic: conceptual
helpviewer_keywords: 
  - "collections [Master Data Services], permissions"
  - "permissions [Master Data Services], collections"
ms.assetid: 703e1bf5-4b4b-4830-8a5b-f979b09f677d
author: CordeliaGrey
ms.author: jiwang6
---
# Collection Permissions (Master Data Services)

[!INCLUDE [SQL Server - Windows only ASDBMI  ](../includes/applies-to-version/sql-windows-only-asdbmi.md)]

  Collection permissions apply to all collections for an entity. You cannot give permission to a specific collection; permissions apply to all collections.  
  
> [!NOTE]  
>  These permissions apply to the **Explorer** functional area of the user interface only.  
  
|Permission|Description|  
|----------------|-----------------|  
|**Read**|User can read collection members and the member attributes.|  
|**Create**|User can create collection members and assign attribute values.|  
|**Update**|User can update collection members, attributes and relationships.|  
|**Delete**|User can delete collection members.|  
|**Deny**|Deny all access to the collection members.|  
  
 The Read, Create, Update, and Delete permissions can be combined. When Create, Update and Delete are assigned, the read permission is assigned automatically.  
  
## See Also  
 [Assign Model Object Permissions &#40;Master Data Services&#41;](../master-data-services/assign-model-object-permissions-master-data-services.md)   
 [Collections &#40;Master Data Services&#41;](../master-data-services/collections-master-data-services.md)   
 [Model Object Permissions &#40;Master Data Services&#41;](../master-data-services/model-object-permissions-master-data-services.md)  
  
  
