---
description: "Purge Version Members (Master Data Services)"
title: Purge Version Members
ms.custom: ""
ms.date: "03/01/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: master-data-services
ms.topic: conceptual
ms.assetid: adecce2d-46bb-49ff-8be9-0b31b8dd3cb6
author: CordeliaGrey
ms.author: jiwang6
---
# Purge Version Members (Master Data Services)

[!INCLUDE [SQL Server - Windows only ASDBMI  ](../includes/applies-to-version/sql-windows-only-asdbmi.md)]

  In [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)], deleting a member only deactivates, or soft-deletes, it. The  data still resides in the database. This topic describes how to purge (permanently delete) all soft-deleted members in a model version.  
  
## Prerequisites  
 To perform this procedure.  
  
-   You must have permission to access the Version Management functional area.  
  
-   You must be a model administrator. For more information, see [Administrators &#40;Master Data Services&#41;](../master-data-services/administrators-master-data-services.md).  
  
## To purge soft-deleted members  
  
1.  In [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)], click **Version Management**.  
  
2.  On the **Manage Versions** page, select the model that has the version that you want to purge. The list of model versions is displayed.  
  
3.  Select the row for the version that you want to purge.  
  
4.  Click **Purge Members**.  
  
5.  Click "OK" on the confirmation prompt.  
  
## Additional Methods to Purge Members  
 Purging version members permanently delete soft-deleted members in all entities pertaining to the selected version. A more finely grained alternative is to use entity base staging to permanently delete only specific members of an entity. Also, entity administrators with Explorer functional permission may purge an entity version in the entity explorer page.  
  
 For more information, see [Leaf Member Staging Table &#40;Master Data Services&#41;](../master-data-services/leaf-member-staging-table-master-data-services.md)  
  
  
