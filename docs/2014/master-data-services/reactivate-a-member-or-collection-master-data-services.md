---
title: "Reactivate a Member or Collection (Master Data Services) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
helpviewer_keywords: 
  - "collections [Master Data Services], reactivating"
  - "consolidated members [Master Data Services], reactivating"
  - "reactivating members [Master Data Services]"
  - "members [Master Data Services], reactivating"
  - "reactivating collections [Master Data Services]"
  - "leaf members [Master Data Services], reactivating"
ms.assetid: bb4884c0-3658-4763-92d1-636804278b1c
author: leolimsft
ms.author: lle
manager: craigg
---
# Reactivate a Member or Collection (Master Data Services)
  In [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)], you can reactivate a member that was either:  
  
-   Deactivated by the staging process.  
  
-   Deleted in the MDS [!INCLUDE[ssMDSXLS](../includes/ssmdsxls-md.md)].  
  
-   Deleted in the [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)] web application.  
  
 When you reactivate a member, its attributes and its membership in hierarchies and collections are restored.  
  
 You can also reactivate collections. When you do, the collection's attributes and the members that belong to the collection are restored.  
  
 When either a collection or member is reactivated, all previous transactions are restored.  
  
## Prerequisites  
 To perform this procedure:  
  
-   In [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)], you must have permission to the **Version Management** functional area.  
  
-   You must be a model administrator. For more information, see [Administrators &#40;Master Data Services&#41;](administrators-master-data-services.md).  
  
### To reactivate a member or collection  
  
1.  On the [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)] home page, click **Version Management**.  
  
2.  On the menu bar, click **Transactions**.  
  
3.  On the **Transactions** page, from the **Model** list, select a model.  
  
4.  From the **Version** list, select a version.  
  
5.  In the **Transactions** pane, click the row for the member or collection you want to reactivate. This row should have **Active** displayed in the **Prior Value** column and **De-Activated** in the **New Value** column.  
  
6.  Click **Reverse Transaction**.  
  
7.  On the confirmation dialog box, click **OK**. A new transaction is added, showing **Active** in the **New Value** column.  
  
## See Also  
 [Deactivate or Delete Members by Using the Staging Process &#40;Master Data Services&#41;](add-update-and-delete-data-master-data-services.md)   
 [Delete a Member or Collection &#40;Master Data Services&#41;](../../2014/master-data-services/delete-a-member-or-collection-master-data-services.md)   
 [Members &#40;Master Data Services&#41;](../../2014/master-data-services/members-master-data-services.md)   
 [Collections &#40;Master Data Services&#41;](../../2014/master-data-services/collections-master-data-services.md)  
  
  
