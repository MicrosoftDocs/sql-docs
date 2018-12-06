---
title: "Leaf Permissions (Master Data Services) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
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
  Leaf permissions apply to the attribute values for all leaf members of an entity.  
  
 For entities without explicit hierarchies enabled, assigning permission to **Leaf** is the same as assigning permission to the entity.  
  
 **Notes:**  
  
-   Leaf permissions apply to the **Explorer** functional area of the user interface only.  
  
-   Permissions assigned to **Name** and **Code** attributes are not enforced.  
  
|Permission|Description|  
|----------------|-----------------|  
|**Read-only**|Leaf members are displayed but the user cannot add, remove, or change them.<br /><br /> If consolidated members exist, the names and codes are displayed but the user cannot add, remove, or change them.|  
|**Update**|Leaf members are displayed and the user can add, remove, and change them.<br /><br /> If consolidated members exist, the names and codes are displayed but the user cannot add, remove, or change them.|  
|**Deny**|Leaf members for the entity are not displayed.|  
  
## Attribute Permissions  
 Attribute permissions apply to the attribute's values for the specific entity. Users with attribute permissions only cannot add or remove members.  
  
|Permission|Description|  
|----------------|-----------------|  
|**Read-only**|The attribute is displayed but the user cannot change attribute values.|  
|**Update**|The attribute is displayed and the user can change attribute values.|  
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
 [Assign Model Object Permissions &#40;Master Data Services&#41;](assign-model-object-permissions-master-data-services.md)   
 [Consolidated Permissions &#40;Master Data Services&#41;](../../2014/master-data-services/consolidated-permissions-master-data-services.md)   
 [Model Object Permissions &#40;Master Data Services&#41;](../../2014/master-data-services/model-object-permissions-master-data-services.md)   
 [Members &#40;Master Data Services&#41;](../../2014/master-data-services/members-master-data-services.md)   
 [Attributes &#40;Master Data Services&#41;](../../2014/master-data-services/attributes-master-data-services.md)  
  
  
