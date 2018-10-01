---
title: "Merge Conflicts (MDS Add-in for Excel) | Microsoft Docs"
ms.custom: microsoft-excel-add-in
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "mds"
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
ms.assetid: cf95978f-a2c5-4325-8606-dbd4e88741b8
author: leolimsft
ms.author: lle
manager: craigg
---
# Merge Conflicts (MDS Add-in for Excel)

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

  In the [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)] Add-in for Excel, if data has been changed on the server by another user, the publish will fail with a conflict error. To resolve this error, you can perform merge conflicts and republish the changes.  
  
## Prerequisites  
 To perform this procedure:  
  
-   You must have permission to access the **Explorer** functional area.  
  
-   You must have a minimum of Update permission to the leaf model object for the entity you are updating.  
  
-   The active worksheet must have MDS-managed data.  
  
-   The active worksheet must have a conflict error after you tried to publish your changes.  
  
### To merge conflicts  
  
1.  In the worksheet, select the row or cell that has the conflict error.  
  
2.  In the **Publish and Validate** menu group, select **Merge Conflicts** to open the **Merge Conflicts** dialog.  
  
3.  In the **Merge Conflicts** dialog, you can either:  
  
    -   Choose **Latest** and click **Apply** to undo the pending changes and reload the latest version from the server.  
  
    -   Choose **Original** and click **Apply** to apply the original version in the worksheet.  
  
    -   Choose **Yours** and click **Apply** to keep the existing local changes.  
  
4.  After you click **Apply**, you can make additional changes and publish again. Or you can click **Cancel** to cancel the update and reload the latest version from the server.  
  
## See Also  
 [Overview: Importing Data from Excel &#40;MDS Add-in for Excel&#41;](../../master-data-services/microsoft-excel-add-in/overview-importing-data-from-excel-mds-add-in-for-excel.md)  
  
  
