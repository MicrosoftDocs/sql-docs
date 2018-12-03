---
title: "Leaf Permissions (Master Data Services) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.prod: sql
ms.prod_service: "mds"
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
helpviewer_keywords: 
  - "attribute groups [Master Data Services], permissions"
  - "members [Master Data Services], leaf member permissions"
  - "permissions [Master Data Services], leaf members"
  - "leaf members [Master Data Services], attribute permissions"
  - "attributes [Master Data Services], leaf member attribute permissions"
ms.assetid: bde16e8c-bcd4-4041-8130-55c5450e5f72
author: leolimsft
ms.author: lle
manager: craigg
---
# Leaf Permissions (Master Data Services)

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

  Leaf permissions apply to the attribute values for all leaf members of an entity.  
  
 For entities without explicit hierarchies enabled, assigning permission to **Leaf** is the same as assigning permission to the entity.  
  
 **Notes:**  
  
-   Leaf permissions apply to the **Explorer** functional area of the user interface only.  
  
-   Permissions assigned to **Name** and **Code** attributes are not enforced.  
  
|Permission|Description|  
|----------------|-----------------|  
|**Read**|User can read leaf members, attributes.|  
|**Create**|User can create leaf members, and assign attribute values during create.|  
|**Update**|User can update leaf members and attributes.|  
|**Delete**|User can delete leaf members.|  
|**Deny**|Deny all access to the leaf members.|  
  
 The Read, Create, Update, and Delete permissions can be combined. When Create, Update and Delete are assigned, the read permission is assigned automatically.  
  
## Attribute Permissions  
 Attribute permissions apply to the attribute's values for the specific entity. Users with attribute permissions only cannot add or remove members.  
  
|Permission|Description|  
|----------------|-----------------|  
|**Read**|User can read attributes.|  
|**Create**|User can assign values when they create members.|  
|**Update**|User can update attributes.|  
|**Delete**|No effect.|  
|**Deny**|The attribute is not displayed.<br /><br /> Note: You cannot explicitly deny access to Name and Code attributes.|  
  
### Example  
 For the Product entity, assign **Update** permission to Subcategory attribute. Deny permission to all other attributes.  
  
|Name|Code|Subcategory (Update)|  
|----------|----------|----------------------------|  
|Mountain-100|BK-M101|{5} Mountain Bikes|  
|Mountain-100|BK-M201|{5} Mountain Bikes|  
  
 In **Explorer**, you can update any attribute value in the Subcategory column. If you do not have permission to an attribute, the attribute is not displayed.  
  
> [!NOTE]  
>  In this example, Subcategory is a domain-based attribute, based on the SubcategoryList entity. You can select a different subcategory for Mountain-100 but you cannot add members to or delete members from the SubcategoryList entity.  
  
## See Also  
 [Assign Model Object Permissions &#40;Master Data Services&#41;](../master-data-services/assign-model-object-permissions-master-data-services.md)   
    
 [Model Object Permissions &#40;Master Data Services&#41;](../master-data-services/model-object-permissions-master-data-services.md)   
 [Members &#40;Master Data Services&#41;](../master-data-services/members-master-data-services.md)   
 [Attributes &#40;Master Data Services&#41;](../master-data-services/attributes-master-data-services.md)  
  
  
