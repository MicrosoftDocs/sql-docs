---
title: "Move Members within a Hierarchy (Master Data Services) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "master-data-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "hierarchies [Master Data Services], moving members"
  - "explicit hierarchies, moving members"
  - "derived hierarchies, moving members"
  - "members [Master Data Services], moving"
ms.assetid: 049c9a15-89c1-478c-8438-028fffc9e187
author: douglaslms
ms.author: douglasl
manager: craigg
---
# Move Members within a Hierarchy (Master Data Services)
  In [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)], move members within a hierarchy to change their location or parent assignment.  
  
## Prerequisites  
 To perform this procedure:  
  
-   You must have permission to access the **Explorer** functional area.  
  
-   For explicit hierarchies, you must have a minimum of **Update** permission to the entity.  
  
-   For derived hierarchies, you must have a minimum of **Update** to the model and to any domain-based attributes used in the hierarchy.  
  
### To move members within a hierarchy  
  
1.  On the [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)] home page, from the **Model** list, select a model.  
  
2.  From the **Version** list, select a version.  
  
3.  Click **Explorer**.  
  
4.  From the menu bar, point to **Hierarchies** and click *hierarchy_name*.  
  
5.  In the **Hierarchy** pane, where the hierarchy is displayed in a tree structure, click the check box for each member you want to move.  
  
6.  At the top of the **Hierarchy** pane, click **Cut**.  
  
7.  In the **Hierarchy** pane, click the check box for the member you want to move the members to.  
  
8.  Click **Paste**.  
  
    > [!NOTE]  
    >  In derived hierarchies, you can move members to the same level only. Also, you cannot change the sort order of members.  
  
## See Also  
 [Move Explicit Hierarchy Members by Using the Staging Process &#40;Master Data Services&#41;](add-update-and-delete-data-master-data-services.md)   
 [Derived Hierarchies &#40;Master Data Services&#41;](../../2014/master-data-services/derived-hierarchies-master-data-services.md)   
 [Explicit Hierarchies &#40;Master Data Services&#41;](../../2014/master-data-services/explicit-hierarchies-master-data-services.md)  
  
  
