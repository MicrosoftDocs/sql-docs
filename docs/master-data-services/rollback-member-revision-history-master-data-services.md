---
description: "Rollback Member Revision History (Master Data Services)"
title: Rollback Member Revision History
ms.custom: ""
ms.date: "03/01/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: master-data-services
ms.topic: conceptual
ms.assetid: d39d3474-20e7-429f-9c8d-fcc4eb0854fc
author: CordeliaGrey
ms.author: jiwang6
---
# Rollback Member Revision History (Master Data Services)

[!INCLUDE [SQL Server - Windows only ASDBMI  ](../includes/applies-to-version/sql-windows-only-asdbmi.md)]

  A member revision history is recorded each time a member is changed. You can rollback a member revision history to a previous version.  
  
## Prerequisites  
  
-   You must have permission to update at least one of the attributes of the selected member. When you rollback a revision history, all the attribute values that can be updated will be rolled back to the previous version values.  
  
-   Revision history is available only when the transaction log type of the entity is member.  
  
 **To rollback a member revision history**  
  
1.  In Master Data Manager, click Explorer.  
  
2.  Choose the entity and the member to rollback.  
  
3.  Click **View History.**  
  
4.  Choose the revision to rollback, and then click **Rollback**.  
  
## See Also  
 [Member Revision History &#40;Master Data Services&#41;](../master-data-services/member-revision-history-master-data-services.md)   
 [Change the Entity Transaction Log Type &#40;Master Data Services&#41;](../master-data-services/change-the-entity-transaction-log-type-master-data-services.md)  
  
  
