---
title: "Create a Derived Hierarchy (Master Data Services) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "mds"
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
helpviewer_keywords: 
  - "derived hierarchies, creating"
  - "creating derived hierarchies [Master Data Services]"
ms.assetid: fec653c4-11cc-46a2-8dd8-b605341ebb40
author: leolimsft
ms.author: lle
manager: craigg
---
# Create a Derived Hierarchy (Master Data Services)

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

  In [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)], create a derived hierarchy when you want a level-based hierarchy that ensures that members exist at the correct level. Derived hierarchies are based on the domain-based attribute relationships that exist in a model.  
  
> [!NOTE]  
>  If a domain-based attribute value doesn't exist for a member, the member is not included in the derived hierarchy. See [Require Attribute Values &#40;Master Data Services&#41;](../master-data-services/require-attribute-values-master-data-services.md) to require a domain-based attribute value for all members.  
  
## Prerequisites  
 To perform this procedure:  
  
-   You must have permission to access the **System Administration** functional area.  
  
-   You must be a model administrator. For more information, see [Administrators &#40;Master Data Services&#41;](../master-data-services/administrators-master-data-services.md).  
  
### To create a derived hierarchy  
  
1.  In [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)], click **System Administration**.  
  
2.  From the menu bar, point to **Manage** and click **Derived Hierarchies**.  
  
3.  On the **Derived Hierarchy Maintenance** page, from the **Model** list, select a model.  
  
4.  Click **Add**.  
  
5.  On the **Add Derived Hierarchy** page, in the **Derived hierarchy name** box, type a name for the hierarchy.  
  
    > [!TIP]  
    >  Use a name that describes the levels in the hierarchy, for example **Product to Subcategory to Category**.  
  
6.  Click **Save derived hierarchy**.  
  
7.  On the **Edit Derived Hierarchy** page, in the **Available Entities and Hierarchies** pane, click an entity or hierarchy and drag it to the **Drop Parent Here** in **Current Levels** pane.  
  
8.  Continue dragging entities or hierarchies until your hierarchy is complete.  
  
9. Click **Back**.  
  
## See Also  
 [Derived Hierarchies &#40;Master Data Services&#41;](../master-data-services/derived-hierarchies-master-data-services.md)   
 [Derived Hierarchies with Explicit Caps &#40;Master Data Services&#41;](../master-data-services/derived-hierarchies-with-explicit-caps-master-data-services.md)   
 [Domain-Based Attributes &#40;Master Data Services&#41;](../master-data-services/domain-based-attributes-master-data-services.md)  
  
  
