---
title: "Delete a Member or Collection (Master Data Services) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
helpviewer_keywords: 
  - "collections [Master Data Services], deleting"
  - "leaf members [Master Data Services], deleting"
  - "deleting members [Master Data Services]"
  - "members [Master Data Services], deleting"
  - "consolidated members [Master Data Services], deleting"
ms.assetid: 519130a7-4226-4d71-9124-d2ee0ce7e5bd
author: leolimsft
ms.author: lle
manager: craigg
---
# Delete a Member or Collection (Master Data Services)
  In [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)], delete a member or collection when you no longer need it. If you want to delete members in bulk, use the staging process instead. For more information, see [Deactivate or Delete Members by Using the Staging Process &#40;Master Data Services&#41;](add-update-and-delete-data-master-data-services.md).  
  
> [!NOTE]  
>  You cannot delete a member if it is used as a domain-based attribute value for another member.  
  
## Prerequisites  
 To perform this procedure:  
  
-   You must have permission to access the **Explorer** functional area.  
  
-   For members, you must have a minimum of **Update** permission to the leaf model object you are deleting a member from.  
  
-   For collections, you must have a minimum of **Update** permission to the leaf collection object you are deleting.  
  
### To delete a member or collection  
  
1.  On the [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)] home page, from the **Model** list, select a model.  
  
2.  From the **Version** list, select a version.  
  
3.  Click **Explorer**.  
  
4.  To delete:  
  
    -   A leaf member, from the menu bar, point to **Entities** and click the name of the entity that contains the member.  
  
    -   A consolidated member, from the menu bar, point to **Hierarchies** and click the name of the hierarchy that contains the member. Then click the node in the hierarchy that contains the member.  
  
    -   A collection, from the menu bar, point to **Collections** and click the name of the entity that contains the collection.  
  
5.  In the grid, click the row of the member or collection you want to delete.  
  
6.  Click **Delete Member**, **Delete**, or **Delete Collection**.  
  
7.  In the confirmation dialog box, click **OK**.  
  
## See Also  
 [Reactivate a Member or Collection &#40;Master Data Services&#41;](../../2014/master-data-services/reactivate-a-member-or-collection-master-data-services.md)   
 [Members &#40;Master Data Services&#41;](../../2014/master-data-services/members-master-data-services.md)   
 [Collections &#40;Master Data Services&#41;](../../2014/master-data-services/collections-master-data-services.md)  
  
  
