---
title: "Changesets (Master Data Services) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "mds"
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
ms.assetid: f227c49a-ed46-4e0f-8992-83093456cf94
author: leolimsft
ms.author: lle
manager: craigg
---
# Changesets (Master Data Services)

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

  [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] now supports the ability to save any pending changes to an entity as changesets. There are two usage scenarios for this feature.  
  
-   **Changes when "Approval Required" is turned on by Entity Administrator**  
  
     If an Entity administrator specifies that the changes to a given entity require approval before they are committed, any changes to the entity have to be saved into a new or an existing changeset before they can be submitted for approval.  For more information, see [Approval Required &#40;Master Data Services&#41;](../master-data-services/approval-required-master-data-services.md)  
  
     You would follow this workflow.  
  
    1.  You create a changeset. The changeset is in the Open state. See [Create a Changeset &#40;Master Data Services&#41;](../master-data-services/create-a-changeset-master-data-services.md)  
  
    2.  You apply the changeset and add some changes to the changeset. See [Apply and Update a Changeset &#40;Master Data Services&#41;](../master-data-services/apply-and-update-a-changeset-master-data-services.md)  
  
    3.  You submit the changeset to the entity administrator for approval. The changeset is in the Pending state. See [Commit or Submit a Changeset &#40;Master Data Services&#41;](../master-data-services/commit-or-submit-a-changeset-master-data-services.md)  
  
    4.  The entity administrator gets an email notification that a changeset is waiting for approval. If the entity administrator approves the changeset, the changeset is in the Approved state. See [Approve or Reject a Changeset &#40;Master Data Services&#41;](../master-data-services/approve-or-reject-a-changeset-master-data-services.md)  
  
    5.  The approved changeset will be committed automatically. If the change is committed successfully, the changeset is in the committed state.  
  
-   **Local User changes**  
  
     If you merely want to save your local changes so you can use or retrieve them later, you can use changesets to achieve that.  
  
     You would follow this workflow.  
  
    1.  You create a changeset. The changeset is in the Open state. See [Create a Changeset &#40;Master Data Services&#41;](../master-data-services/create-a-changeset-master-data-services.md)  
  
    2.  You apply the changeset and add some changes to the changeset. See [Apply and Update a Changeset &#40;Master Data Services&#41;](../master-data-services/apply-and-update-a-changeset-master-data-services.md)  
  
    3.  When ready, you commit the changeset. See [Commit or Submit a Changeset &#40;Master Data Services&#41;](../master-data-services/commit-or-submit-a-changeset-master-data-services.md)  
  
## See Also  
 [Create a Changeset &#40;Master Data Services&#41;](../master-data-services/create-a-changeset-master-data-services.md)   
 [Apply and Update a Changeset &#40;Master Data Services&#41;](../master-data-services/apply-and-update-a-changeset-master-data-services.md)   
 [Commit or Submit a Changeset &#40;Master Data Services&#41;](../master-data-services/commit-or-submit-a-changeset-master-data-services.md)   
 [Approve or Reject a Changeset &#40;Master Data Services&#41;](../master-data-services/approve-or-reject-a-changeset-master-data-services.md)   
 [Manage Changesets &#40;Master Data Services&#41;](../master-data-services/manage-changesets-master-data-services.md)  
  
  
