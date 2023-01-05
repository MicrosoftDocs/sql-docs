---
description: "Member Revision History (Master Data Services)"
title: Member Revision History
ms.custom: ""
ms.date: "03/01/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: master-data-services
ms.topic: conceptual
ms.assetid: 113069c5-12e6-48ec-b443-b42e14f77308
author: CordeliaGrey
ms.author: jiwang6
---
# Member Revision History (Master Data Services)

[!INCLUDE [SQL Server - Windows only ASDBMI  ](../includes/applies-to-version/sql-windows-only-asdbmi.md)]

  A member revision history is recorded each time a member is changed, if the entity transaction log type is member.  
  
 For information on transaction log types, see [Change the Entity Transaction Log Type &#40;Master Data Services&#41;](../master-data-services/change-the-entity-transaction-log-type-master-data-services.md).  
  
 Member revision histories are recorded when the following changes occur.  
  
-   Members are created, deleted, reactivated or purged.  
  
-   Attribute values are changed.  
  
-   Members are moved in a hierarchy or collection  
  
## View and Manage Revision History by Entity  
 In the Explorer functional area, you can view the revisions for all members in the entity. If you have update permissions, you can roll back the member to a previous revision.  
  
 **To view and manage revision history**  
  
1.  In [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)], select the model and version and then click **Explorer**.  
  
2.  Select the entity from the **Entities** menu.  
  
3.  Click **View History** to view all the historical data of the entity.  
  
4.  Click **Filter** to filter the data.  
  
5.  Click the column header to sort the data.  
  
6.  If you have update permissions, click **Revert Member** to roll back to the selected version.  
  
## View and Manage Revision History by Member  
 In the Explorer functional area, you can view the revisions for a member if you have read permissions on the member. If you have update permissions, you can roll back the member to a previous revision or add annotations to the revision.  
  
1.  In [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)], select the model and version and then click **Explorer**.  
  
2.  Select the entity from the **Entities** menu.  
  
3.  Select the member.  
  
4.  Click **View History** in the right pane.  
  
## Log Retention Setting  
 You can configure how long historical data is retained by setting the **Log retention in Days** property in system settings for the [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)] database, and by setting **Log Retention Days** when you create or edit a model.  
  
## Related Task  
  
|Task Description|Topic|  
|----------------------|-----------|  
|Rollback the member revision history|[Rollback Member Revision History &#40;Master Data Services&#41;](../master-data-services/rollback-member-revision-history-master-data-services.md)|  
  
## See Also  
 [Create a Model &#40;Master Data Services&#41;](../master-data-services/create-a-model-master-data-services.md)   
 [System Settings &#40;Master Data Services&#41;](../master-data-services/system-settings-master-data-services.md)  
  
  
