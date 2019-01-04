---
title: "Create a Changeset (Master Data Services) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "mds"
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
ms.assetid: cfad6f1c-9125-4896-b5f5-a4b9f9593cc4
author: leolimsft
ms.author: lle
manager: craigg
---
# Create a Changeset (Master Data Services)

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

  A changeset is a collection of the pending changes on the master data. If the entity requires approval for changes, the pending changes must be saved in a changeset and then submitted for administrator approval.  
  
## Prerequisites  
  
-   You must have permission to access the Explorer functional area. For more information, see [Functional Area Permissions &#40;Master Data Services&#41;](../master-data-services/functional-area-permissions-master-data-services.md)  
  
-   You must have at least read access to the entity or one of its attributes.  
  
## To create a local changeset  
  
1.  On the [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)] home page, select  the model and version and then click **Explorer**.  
  
2.  Click an entity on the **Entities** menu.  
  
3.  In the right pane, select **Changesets** and click **Create**.  
  
4.  Enter a name for the changeset, and click **Save**.  
  
     The name of the changeset must be unique within a model.  
  
## To create a changeset for approval  
  
1.  On the [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)] home page, select  the model and version and then click **Explorer**.  
  
2.  Click an entity on the **Entities** menu.  
  
3.  Make changes to the entity and click on**OK**.  
  
4.  **Choose A changeset** dialog box is displayed.  
  
5.  Click **New**, enter a name for the changeset, and click **Save**. The changeset name must be unique within a model.  
  
6.  To use  an existing changeset, click **Existing** and choose the changeset from the list. Only changesets that are in an open or rejected state are available.  
  
## Next Steps  
 [Apply and Update a Changeset &#40;Master Data Services&#41;](../master-data-services/apply-and-update-a-changeset-master-data-services.md)  
  
## See Also  
 [Commit or Submit a Changeset &#40;Master Data Services&#41;](../master-data-services/commit-or-submit-a-changeset-master-data-services.md)   
 [Approve or Reject a Changeset &#40;Master Data Services&#41;](../master-data-services/approve-or-reject-a-changeset-master-data-services.md)  
  
  
