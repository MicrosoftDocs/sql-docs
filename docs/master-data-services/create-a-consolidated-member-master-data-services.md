---
title: "Create a Consolidated Member (Master Data Services) | Microsoft Docs"
ms.custom: ""
ms.date: "04/01/2016"
ms.prod: sql
ms.prod_service: "mds"
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
helpviewer_keywords: 
  - "creating consolidated members [Master Data Services]"
  - "members [Master Data Services], creating consolidated members"
  - "consolidated members [Master Data Services], creating"
ms.assetid: 431ab2d2-5517-4372-9980-142b05427c08
author: leolimsft
ms.author: lle
manager: craigg
---
# Create a Consolidated Member (Master Data Services)

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

  In [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)], create a consolidated member when you want a parent node for an explicit hierarchy. If you want to add data in bulk, use the staging tables instead. For more information, see  [Import Data from Tables &#40;Master Data Services&#41;](../master-data-services/import-data-from-tables-master-data-services.md).  
  
## Prerequisites  
 To perform this procedure:  
  
-   You must have permission to access the **Explorer** functional area.  
  
-   You must have a minimum of **Update** permission to the consolidated model object for the entity you are adding a member to, as well as **Create permission** to Consolidated Type under the entity.  
  
### To create a consolidated member  
  
1.  On the [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)] home page, from the **Model** list, select a model.  
  
2.  From the **Version** list, select a version.  
  
3.  Click **Explorer**.  
  
4.  From the menu bar, point to **Hierarchies** and click the name of the hierarchy you want to add a consolidated member to.  
  
5.  Above the grid, select either the **Consolidated members** or the **All consolidated members in hierarchy** option.  
  
6.  In the left-hand pane, select either a Root node or a consolidated member under which you want to create a consolidated member.  
  
7.  Click **Add**.  
  
8.  In the pane on the right, complete the fields.  
  
9. Optional. In the **Annotations** box, type a comment about why the member was added. All users who have access to the member can view the annotation.  
  
10. Click **OK**.  
  
## See Also  
 [Create an Explicit Hierarchy &#40;Master Data Services&#41;](../master-data-services/create-an-explicit-hierarchy-master-data-services.md)   
 [Create a Leaf Member &#40;Master Data Services&#41;](../master-data-services/create-a-leaf-member-master-data-services.md)   
 [Members &#40;Master Data Services&#41;](../master-data-services/members-master-data-services.md)   
 [Explicit Hierarchies &#40;Master Data Services&#41;](../master-data-services/explicit-hierarchies-master-data-services.md)  
  
  
