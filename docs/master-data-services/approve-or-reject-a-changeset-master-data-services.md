---
title: "Approve or Reject a Changeset (Master Data Services) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "mds"
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
ms.assetid: 45bd01f9-ae15-4fc5-a2ba-eee565a26ef8
author: leolimsft
ms.author: lle
manager: craigg
---
# Approve or Reject a Changeset (Master Data Services)

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

  A changeset is a collection of the pending changes on the master data. If the entity changes require administrator approval and a changeset is submitted for approval, you can review and then approve or reject the changeset.  
  
## Prerequisites  
  
-   You must have permission to access the **Explorer** functional area. For more information, see [Functional Area Permissions &#40;Master Data Services&#41;](../master-data-services/functional-area-permissions-master-data-services.md).  
  
-   You must have administrator permission for the entity.  
  
-   The entity changes must require administrator approval.  
  
-   If the changeset status is pending, you can review and then approve or reject the changeset.  
  
-   Users are not allowed to approve their own changes. If you are the entity administrator, you must assign a secondary administrator to approve your own changeset.  
  
## To approve or reject a changeset  
  
1.  On the [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)] home page, select the model and version and then click **Explorer**.  
  
2.  Click an entity on the **Entities** menu.  
  
3.  In the right pane, select **Changesets** and double click the changeset you want to approve or reject.  
  
4.  Click **Apply** to apply the changeset and review the pending changes.  
  
5.  Click **Reject** to reject the changeset and send it back to the owner.  
  
6.  Click **Approve** to approve the changeset. The changeset is committed automatically.  
  
## See Also  
 [Create a Changeset &#40;Master Data Services&#41;](../master-data-services/create-a-changeset-master-data-services.md)   
 [Apply and Update a Changeset &#40;Master Data Services&#41;](../master-data-services/apply-and-update-a-changeset-master-data-services.md)   
 [Commit or Submit a Changeset &#40;Master Data Services&#41;](../master-data-services/commit-or-submit-a-changeset-master-data-services.md)  
  
  
