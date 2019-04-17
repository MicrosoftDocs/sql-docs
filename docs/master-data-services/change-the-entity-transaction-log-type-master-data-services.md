---
title: "Change the Entity Transaction Log Type (Master Data Services) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "mds"
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
ms.assetid: 75250b32-3384-43c2-9b5c-1607cc3aa7b3
author: leolimsft
ms.author: lle
manager: craigg
---
# Change the Entity Transaction Log Type (Master Data Services)

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

  You can change the transaction log type of an entity, to attribute, member, or none.  
  
|Transaction Log Type|Description|  
|--------------------------|-----------------|  
|Attribute|Entity change logs are saved at the attribute level.<br /><br /> The transaction log is saved, as it is for [!INCLUDE[ssSQL14](../includes/sssql14-md.md)] [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)].|  
|Member|Entity change logs as saved at the row level.<br /><br /> Any attribute change triggers a new row revision.<br /><br /> When using row transaction log type, the entity is stored as a slowly changing dimension Type 4. Type 2 subscription view and Type 4 (History) subscription view are supported. For more information, see [Subscription View Formats &#40;Master Data Services&#41;](../master-data-services/subscription-view-formats-master-data-services.md)<br /><br /> Provides better performance.|  
|None|No change logs are saved.<br /><br /> Provides the best performance.|  
  
## Prerequisites  
 To perform this procedure:  
  
-   You must have permission to access the System Administration functional area.For more information, see [Functional Area Permissions &#40;Master Data Services&#41;](../master-data-services/functional-area-permissions-master-data-services.md).  
  
-   You must be a model administrator. For more information, see [Administrators &#40;Master Data Services&#41;](../master-data-services/administrators-master-data-services.md).  
  
-   The entity must exist. For more information, see [Create an Entity &#40;Master Data Services&#41;](../master-data-services/create-an-entity-master-data-services.md).  
  
 **To change the transaction log type**  
  
1.  In Master Data Manager, click **System Administration**.  
  
2.  On the **Manage Model** page, select the row for the model  of the entity that you want to edit and then click **Entities**.  
  
3.  On the **Manage Entity** page, select the row for  the entity that you want to update and then click **Edit**.  
  
4.  Choose the transaction log type in the drop-down list.  
  
5.  Click **Save**.  
  
  
