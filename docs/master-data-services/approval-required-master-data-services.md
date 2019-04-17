---
title: "Approval Required (Master Data Services) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "mds"
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
ms.assetid: b475a53d-269d-49f3-bb42-965c555f80be
author: leolimsft
ms.author: lle
manager: craigg
---
# Approval Required (Master Data Services)

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

  In [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)], the administrator can set an entity to Approval Required. All the changes on this entity would require one of the entity administrators to review and approve the changes.  
  
> [!NOTE]  
>  Changes made on leaf members requires approval. The changes made on deprecated explicit hierarchies and collections  bypass the approval.  
>   
>  Changes made by the staging table process bypass the approval.  
>   
>  Changes made by a business rule bypass the approval.  
  
## Prerequisites  
 To perform this procedure:  
  
-   You must have permission to access the System Administration functional area  
  
-   You must be a model administrator. For more information, see [Administrators &#40;Master Data Services&#41;](../master-data-services/administrators-master-data-services.md)  
  
-   An entity must exist. For more information, see [Create an Entity &#40;Master Data Services&#41;](../master-data-services/create-an-entity-master-data-services.md)  
  
## To enable Approval Required for an entity  
  
1.  In [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)], click **System Administration**.  
  
2.  On the **Manage Model** page, select a model from the grid, and then click **Entities**.  
  
3.  On the **Manage Entity** page, from the grid, select the row for the entity that you want to enable  **Approval Required** for.  
  
4.  Click **Edit**, select **Approval Required**, and then click **Save**.  
  
## See Also  
 [Changesets &#40;Master Data Services&#41;](../master-data-services/changesets-master-data-services.md)  
  
  
