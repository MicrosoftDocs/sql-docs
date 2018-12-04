---
title: "Commit a Version (Master Data Services) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "mds"
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
helpviewer_keywords: 
  - "committing versions [Master Data Services]"
  - "versions [Master Data Services], committing"
ms.assetid: 6b967a39-b333-4b84-9e5f-4fb07e156826
author: leolimsft
ms.author: lle
manager: craigg
---
# Commit a Version (Master Data Services)

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

  In [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)], commit a version of a model to prevent changes to the model's members and their attributes. Committed versions cannot be unlocked.  
  
## Prerequisites  
 To perform this procedure:  
  
-   You must have permission to access the **Version Management** functional area.  
  
-   You must be a model administrator. For more information, see [Administrators &#40;Master Data Services&#41;](../master-data-services/administrators-master-data-services.md).  
  
-   The version's status must be **Locked**. For more information, see [Lock a Version &#40;Master Data Services&#41;](../master-data-services/lock-a-version-master-data-services.md).  
  
-   All members must have validated successfully.  
  
-   You must have permission to access the Version Management functional area. For more information, see [Functional Area Permissions &#40;Master Data Services&#41;](../master-data-services/functional-area-permissions-master-data-services.md).  
  
### To commit a version  
  
1.  In [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)], click **Version Management**.  
  
2.  On the **Manage Versions** page, on the menu bar, click **Validate Version**.  
  
3.  On the **Validate Version** page, select the model and version you want to commit.  
  
4.  Click **Commit**.  
  
5.  In the confirmation dialog box, click **OK**.  
  
## Next Steps  
  
-   [Create a Version Flag &#40;Master Data Services&#41;](../master-data-services/create-a-version-flag-master-data-services.md)  
  
-   [Assign a Flag to a Version &#40;Master Data Services&#41;](../master-data-services/assign-a-flag-to-a-version-master-data-services.md)  
  
-   [Copy a Version &#40;Master Data Services&#41;](../master-data-services/copy-a-version-master-data-services.md)  
  
## See Also  
 [Versions &#40;Master Data Services&#41;](../master-data-services/versions-master-data-services.md)  
  
  
