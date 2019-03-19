---
title: "Create a Leaf Member (Master Data Services) | Microsoft Docs"
ms.custom: ""
ms.date: "03/17/2017"
ms.prod: sql
ms.prod_service: "mds"
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
helpviewer_keywords: 
  - "leaf members [Master Data Services], creating"
  - "creating leaf members [Master Data Services]"
  - "members [Master Data Services], creating leaf members"
ms.assetid: 0499d3b3-d508-4d43-a740-19cf53ade9f1
author: leolimsft
ms.author: lle
manager: craigg
---
# Create a Leaf Member (Master Data Services)

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

  In [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)], create a leaf member when you want to add master data to your system. If you want to add data in bulk, use staging tables instead. For more information, see  [Import Data from Tables &#40;Master Data Services&#41;](../master-data-services/import-data-from-tables-master-data-services.md)  
  
 You can also use [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)][!INCLUDE[ssMDSXLS](../includes/ssmdsxls-md.md)] to import data.  
  
## Prerequisites  
 To perform this procedure:  
  
-   You must have permission to access the **Explorer** functional area.  
  
-   You must have a minimum of **Create** or **Update** permission to the leaf model object for the entity you are adding a member to. The Create permission enables you to create a member and edit only the Code attribute. The Update permission enables you to update other attributes.  
  
     For more information, see [Security &#40;Master Data Services&#41;](../master-data-services/security-master-data-services.md).  
  
### To create a leaf member  
  
1.  On the [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)] home page, from the **Model** list, select a model.  
  
2.  If you are a user, select an open version from the **Version** list. If you are an administrator, select a version with open or locked status from the **Version** list.  
  
3.  Click **Explorer**.  
  
4.  From the menu bar, point to **Entities** and click the name of the entity you want to add a member to.  
  
5.  Click **Add member**.  
  
6.  In the **Details** pane, complete the fields.  
  
     If an attribute is domain-based and a filter has been applied to the attribute, the list of attribute values will be constrained by the filter parent attribute.  
  
     For more information about filter parent attributes and domain-based attributes, see [Create a Domain-Based Attribute &#40;Master Data Services&#41;](../master-data-services/create-a-domain-based-attribute-master-data-services.md)  
  
7.  Optional. In the **Annotations** box, type a comment about why the member was added. All users who have access to the member can view the annotation.  
  
8.  Click **OK**.  
  
## See Also  
 [Create a Consolidated Member &#40;Master Data Services&#41;](../master-data-services/create-a-consolidated-member-master-data-services.md)   
 [Members &#40;Master Data Services&#41;](../master-data-services/members-master-data-services.md)  
  
  
