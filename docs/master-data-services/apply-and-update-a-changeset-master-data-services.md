---
description: "Apply and Update a Changeset (Master Data Services)"
title: Apply and Update a Changeset
ms.custom: ""
ms.date: "03/01/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: master-data-services
ms.topic: conceptual
ms.assetid: 3a6a3cf2-1e77-43d3-a64a-855ae51258e7
author: CordeliaGrey
ms.author: jiwang6
---
# Apply and Update a Changeset (Master Data Services)

[!INCLUDE [SQL Server - Windows only ASDBMI  ](../includes/applies-to-version/sql-windows-only-asdbmi.md)]

  A changeset is a collection of the pending changes on the master data. You can apply the changeset locally to view, add, update and delete the pending changes in the changeset.  
  
## Prerequisites  
  
-   You must have permission to access the **Explorer** functional area. For more information, see [Functional Area Permissions &#40;Master Data Services&#41;](../master-data-services/functional-area-permissions-master-data-services.md).  
  
-   You must have at least update access to the entity or one of its attributes.  
  
-   You can view only the changeset you own or the changeset submitted for approval when you are the entity administrator.  
  
-   You can modify only the changeset you own and when the changeset status is open or rejected.  
  
## To apply and update a changeset  
  
1.  On the [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)] home page, select a model and version and then click **Explorer**.  
  
2.  Click an entity on the **Entities** menu.  
  
3.  In the right pane, select **Changesets** and double-click the changeset you want to view and change.  
  
4.  Click **Apply**.  
  
     The pending changes are applied to the entity member in the grid. The pending changes are highlighted.  
  
     Creating, deleting and updating members result in the changes in the changeset.  
  
5.  To revert pending changes, in the **Changesets** pane, right-click in the  grid and then click **Revert**.  
  
## Next Steps  
 [Commit or Submit a Changeset &#40;Master Data Services&#41;](../master-data-services/commit-or-submit-a-changeset-master-data-services.md)  
  
## See Also  
 [Create a Changeset &#40;Master Data Services&#41;](../master-data-services/create-a-changeset-master-data-services.md)   
 [Approve or Reject a Changeset &#40;Master Data Services&#41;](../master-data-services/approve-or-reject-a-changeset-master-data-services.md)  
  
  
