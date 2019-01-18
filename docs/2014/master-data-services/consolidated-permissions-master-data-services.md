---
title: "Consolidated Permissions (Master Data Services) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "master-data-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "attributes [Master Data Services], consolidated member attribute permissions"
  - "consolidated members [Master Data Services], attribute permissions"
  - "permissions [Master Data Services], consolidated members"
  - "members [Master Data Services], consolidated member permissions"
ms.assetid: 084055a3-5fd3-43f3-b620-ac6afab42a3d
author: douglaslms
ms.author: douglasl
manager: craigg
---
# Consolidated Permissions (Master Data Services)
  Consolidated permissions apply to the attribute values for all consolidated members of an entity.  
  
 Consolidated permissions apply only to entities that are enabled for explicit hierarchies and collections.  
  
 **Notes:**  
  
-   Leaf permissions apply to the **Explorer** functional area of the user interface only.  
  
-   Permissions assigned to **Name** and **Code** attributes are not enforced.  
  
|Permission|Description|  
|----------------|-----------------|  
|**Read-only**|Consolidated members are displayed but the user cannot add, remove, or change them.|  
|**Update**|Consolidated members are displayed and the user can add, remove, and change them.|  
|**Deny**|Consolidated members for the entity are not displayed.|  
  
## Attribute Permissions  
 Attribute permissions apply to the attribute's values for the specific entity. Users with only attribute permissions cannot add or remove members.  
  
|Permission|Description|  
|----------------|-----------------|  
|**Read-only**|The attribute is displayed but the user cannot change attribute values.|  
|**Update**|The attribute is displayed and the user can change attribute values.|  
|**Deny**|The attribute is not displayed.<br /><br /> Note: You cannot explicitly deny access to Name and Code attributes.|  
  
## See Also  
 [Assign Model Object Permissions &#40;Master Data Services&#41;](assign-model-object-permissions-master-data-services.md)   
 [Leaf Permissions &#40;Master Data Services&#41;](../../2014/master-data-services/leaf-permissions-master-data-services.md)   
 [Model Object Permissions &#40;Master Data Services&#41;](../../2014/master-data-services/model-object-permissions-master-data-services.md)   
 [Members &#40;Master Data Services&#41;](../../2014/master-data-services/members-master-data-services.md)   
 [Attributes &#40;Master Data Services&#41;](../../2014/master-data-services/attributes-master-data-services.md)  
  
  
