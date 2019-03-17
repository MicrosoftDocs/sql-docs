---
title: "Create a Version Flag (Master Data Services) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
helpviewer_keywords: 
  - "creating version flags [Master Data Services]"
  - "version flags [Master Data Services], creating"
  - "versions [Master Data Services], creating flags"
ms.assetid: 3067e1e3-05b7-4f11-9206-c612ef4e7e42
author: leolimsft
ms.author: lle
manager: craigg
---
# Create a Version Flag (Master Data Services)
  In [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)], create a version flag to assign to a version. The flag can indicate the version that users or subscribing systems should use.  
  
## Prerequisites  
 To perform this procedure:  
  
-   You must have permission to access the **Version Management** functional area.  
  
-   You must be a model administrator. For more information, see [Administrators &#40;Master Data Services&#41;](administrators-master-data-services.md).  
  
### To create a version flag  
  
1.  In [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)], click **Version Management**.  
  
2.  On the **Manage Versions** page, from the menu bar, point to **Manage** and then click **Flags**.  
  
3.  On the **Manage Version Flags** page, from the **Model** field, select the model for which you want to create a flag.  
  
4.  Click **Add**.  
  
5.  In the **Name** box, type a name.  
  
6.  In the **Description** box, type a description.  
  
7.  In the **Committed Versions Only** field, select **True** to indicate that the flag can be assigned to versions with a status of **Committed** only. Select **False** to indicate that the flag can be assigned to versions with any status.  
  
8.  Click **Save**.  
  
## Next Steps  
  
-   [Assign a Flag to a Version &#40;Master Data Services&#41;](../../2014/master-data-services/assign-a-flag-to-a-version-master-data-services.md)  
  
## See Also  
 [Versions &#40;Master Data Services&#41;](../../2014/master-data-services/versions-master-data-services.md)   
 [Change a Version Flag Name &#40;Master Data Services&#41;](../../2014/master-data-services/change-a-version-flag-name-master-data-services.md)  
  
  
