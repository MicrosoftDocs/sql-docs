---
title: "Attribute Groups (Master Data Services) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
helpviewer_keywords: 
  - "attribute groups [Master Data Services]"
  - "attribute groups [Master Data Services], about attribute groups"
ms.assetid: 648b3d0b-e15a-45f9-8292-3a54a072e62c
author: leolimsft
ms.author: lle
manager: craigg
---
# Attribute Groups (Master Data Services)
  In [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)], attribute groups help organize attributes in an entity. When an entity has lots of attributes, attribute groups improve the way an entity is displayed in the [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)] web application.  
  
## How Attribute Groups Change the Display  
 Attribute groups are displayed as tabs above the grid in the **Explorer** functional area of [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)].  
  
 When an entity has a large number of attributes and you view that entity in a grid in **Explorer**, you must scroll to the right to view all of the attributes. To prevent this scrolling, you can create attribute groups.  
  
-   Attribute groups always include the Name and Code attributes.  
  
-   Each attribute for an entity can belong to one or more attribute groups.  
  
-   All attributes are automatically included on the **All Attributes** tab in **Explorer**.  
  
-   There is no way to hide the **All Attributes** tab.  
  
 Attribute groups are administered in the **System Administration** functional area of [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)].  
  
## Show or Hide Attribute Groups  
 When you create an attribute group, it is automatically hidden from all users except the one who created it. For more information about making the group visible, see [Make an Attribute Group Visible to Users &#40;Master Data Services&#41;](make-an-attribute-group-visible-to-users-master-data-services.md).  
  
 If you want to hide a specific attribute within a group, you can assign **Deny** permission to the attribute. For more information, see [Leaf Permissions &#40;Master Data Services&#41;](../../2014/master-data-services/leaf-permissions-master-data-services.md) or [Consolidated Permissions &#40;Master Data Services&#41;](../../2014/master-data-services/consolidated-permissions-master-data-services.md).  
  
## Related Tasks  
  
|Task Description|Topic|  
|----------------------|-----------|  
|Create a new attribute group and add attributes to it.|[Create an Attribute Group &#40;Master Data Services&#41;](../../2014/master-data-services/create-an-attribute-group-master-data-services.md)|  
|Make an attribute group visible to users.|[Make an Attribute Group Visible to Users &#40;Master Data Services&#41;](make-an-attribute-group-visible-to-users-master-data-services.md)|  
|Change the name of an existing attribute group.|[Change an Attribute Group Name &#40;Master Data Services&#41;](../../2014/master-data-services/change-an-attribute-group-name-master-data-services.md)|  
|Delete an existing attribute group.|[Delete an Attribute Group &#40;Master Data Services&#41;](../../2014/master-data-services/delete-an-attribute-group-master-data-services.md)|  
  
## Related Content  
  
-   [Attributes &#40;Master Data Services&#41;](../../2014/master-data-services/attributes-master-data-services.md)  
  
  
