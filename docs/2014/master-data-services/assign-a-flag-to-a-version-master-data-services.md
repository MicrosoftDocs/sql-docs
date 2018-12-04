---
title: "Assign a Flag to a Version (Master Data Services) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
helpviewer_keywords: 
  - "version flags [Master Data Services], assigning flags"
  - "versions [Master Data Services], assigning flags"
ms.assetid: 6629ec7e-32e7-4a1e-8b31-eb43c5923766
author: leolimsft
ms.author: lle
manager: craigg
---
# Assign a Flag to a Version (Master Data Services)
  In [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)], assign a flag to a version to indicate the version that users or subscribing systems should use.  
  
> [!NOTE]  
>  Version flags can be assigned to only one version at a time. If you assign a flag that is already assigned to another version, the flag is moved to the version you selected.  
  
## Prerequisites  
 To perform this procedure:  
  
-   You must have permission to access the **Version Management** functional area.  
  
-   You must be a model administrator. For more information, see [Administrators &#40;Master Data Services&#41;](administrators-master-data-services.md).  
  
-   You must have created a version flag to assign. For more information, see [Create a Version Flag &#40;Master Data Services&#41;](../../2014/master-data-services/create-a-version-flag-master-data-services.md).  
  
### To assign a flag to a version  
  
1.  In [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)], click **Version Management**.  
  
2.  On the **Manage Versions** page, in the row for the version to which you want to assign a flag, double-click the cell in the **Flag** column.  
  
3.  From the list, select the flag you want to assign.  
  
    > [!NOTE]  
    >  If the flag you want is not available, the flag might be available for **Committed** versions only. To confirm, go to the **Manage Version Flags** page and view the **Committed Versions Only** field for the flag.  
  
4.  Press ENTER to save the change.  
  
## See Also  
 [Create a Version Flag &#40;Master Data Services&#41;](../../2014/master-data-services/create-a-version-flag-master-data-services.md)   
 [Versions &#40;Master Data Services&#41;](../../2014/master-data-services/versions-master-data-services.md)  
  
  
