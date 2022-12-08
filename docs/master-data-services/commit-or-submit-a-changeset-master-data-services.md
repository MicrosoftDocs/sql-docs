---
description: "Commit or Submit a Changeset (Master Data Services)"
title: Commit or Submit a Changeset
ms.custom: ""
ms.date: "03/01/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: master-data-services
ms.topic: conceptual
ms.assetid: d323bbac-c8d4-4d2f-a7d2-a597e8b53e2d
author: CordeliaGrey
ms.author: jiwang6
---
# Commit or Submit a Changeset (Master Data Services)

[!INCLUDE [SQL Server - Windows only ASDBMI  ](../includes/applies-to-version/sql-windows-only-asdbmi.md)]

  A changeset is a collection of the pending changes on the master data. If entity changes don't require administrator approval, you can commit the changeset. If the entity changes require administrator approval, you can submit the changeset for approval.  
  
## Prerequisites  
  
-   You must have permission to access the **Explorer** functional area. For more information, see [Functional Area Permissions &#40;Master Data Services&#41;](../master-data-services/functional-area-permissions-master-data-services.md)  
  
-   If the entity changes don't require administrator approval, you can commit the changeset only if you own the changeset and the changeset status is open.  
  
-   If the entity changes require administrator approval, you can submit the changeset for approval only if you own the changeset and the changeset status is open or rejected.  
  
## To commit a local changeset  
 The commit option is only available for local changesets on entities where the Entity Administrator has not enabled the need for approval.  
  
1.  On the [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)] home page, select the model and version and then click **Explorer**.  
  
2.  Click an entity on the **Entities** menu.  
  
3.  In the right pane, select **Changesets** and double click the changeset you want to commit.  
  
4.  Click **Commit**.  
  
## To submit a changeset  
 The submit option is only available on changesets on entities where the Entity Administrator has enabled the need for approval.  
  
1.  On the [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)] home page, select the model and version and then click **Explorer**.  
  
2.  Click an entity on the **Entities** menu.  
  
3.  In the right pane, select **Changesets** and double click the changeset you want to submit.  
  
4.  Click **Submit**.  
  
## Next Steps  
 [Approve or Reject a Changeset &#40;Master Data Services&#41;](../master-data-services/approve-or-reject-a-changeset-master-data-services.md)  
  
## See Also  
 [Create a Changeset &#40;Master Data Services&#41;](../master-data-services/create-a-changeset-master-data-services.md)   
 [Apply and Update a Changeset &#40;Master Data Services&#41;](../master-data-services/apply-and-update-a-changeset-master-data-services.md)   
 [Approve or Reject a Changeset &#40;Master Data Services&#41;](../master-data-services/approve-or-reject-a-changeset-master-data-services.md)  
  
  
