---
title: "Lock a Version (Master Data Services) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "mds"
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
helpviewer_keywords: 
  - "versions [Master Data Services], locking"
  - "locking versions [Master Data Services]"
ms.assetid: 7bb62a84-12d8-4b29-9b6e-6aa25410618e
author: leolimsft
ms.author: lle
manager: craigg
---
# Lock a Version (Master Data Services)

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

  In [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)], lock a version of a model to prevent changes to the model's members and their attributes.  
  
> [!NOTE]  
>  When a version is locked, super users and model administrators can continue to add, edit, and remove members. Other users with permission to the model can view members only.  
  
## Prerequisites  
 To perform this procedure:  
  
-   You must be a model administrator. For more information, see [Administrators &#40;Master Data Services&#41;](../master-data-services/administrators-master-data-services.md).  
  
-   The version's status must be **Open**.  
  
-   You must have permission to access the Version Management functional area. For more information, see [Functional Area Permissions &#40;Master Data Services&#41;](../master-data-services/functional-area-permissions-master-data-services.md).  
  
### To lock a version  
  
1.  In [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)], click **Version Management**.  
  
2.  On the **Manage Versions** page, select the row for the version that you want to lock.  
  
3.  Click **Lock**.  
  
4.  In the confirmation dialog box, click **OK**.  
  
## Next Steps  
  
-   [Validate a Version against Business Rules &#40;Master Data Services&#41;](../master-data-services/validate-a-version-against-business-rules-master-data-services.md)  
  
-   [Commit a Version &#40;Master Data Services&#41;](../master-data-services/commit-a-version-master-data-services.md)  
  
## See Also  
 [Versions &#40;Master Data Services&#41;](../master-data-services/versions-master-data-services.md)   
 [Unlock a Version &#40;Master Data Services&#41;](../master-data-services/unlock-a-version-master-data-services.md)  
  
  
