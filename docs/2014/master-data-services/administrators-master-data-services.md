---
title: "Administrators (Master Data Services) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
helpviewer_keywords: 
  - "administrators [Master Data Services], about administrators"
  - "administrators [Master Data Services]"
  - "models [Master Data Services], administrators"
ms.assetid: d330aa4e-6ade-4b09-b376-1b15d6c78f7d
author: leolimsft
ms.author: lle
manager: craigg
---
# Administrators (Master Data Services)
  In [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)], there are two types of administrators: model administrators, and the [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] system administrator.  
  
## Model Administrators  
 In [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)], a model administrator is a user who has **Update** permission assigned to the top-level model object on the **Model Objects** tab and no other assigned permissions.  
  
-   If the user has access to the **Explorer** functional area, the user can add, delete, and update all master data in this area.  
  
-   If the user has access to other functional areas, the user can perform all administrative tasks available in the functional area.  
  
 Each model can have multiple administrators. Each user can be a model administrator for one, several, or all models in the [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] deployment.  
  
 A user can be configured as a model administrator either in [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)] or programmatically. For more information, see [Create a Model Administrator &#40;Master Data Services&#41;](create-a-model-administrator-master-data-services.md).  
  
## Master Data Services System Administrator  
 There is only one [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] system administrator. The system administrator is the user specified for the **Administrator Account** when you create the [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] database.  
  
 The [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] system administrator:  
  
-   Automatically has access to all functional areas.  
  
-   Can add, delete, and update all master data for all models in the **Explorer** functional area.  
  
 You can change the user who is assigned as the system administrator. For more information, see [Change the System Administrator Account &#40;Master Data Services&#41;](../../2014/master-data-services/change-the-system-administrator-account-master-data-services.md).  
  
## Comparing Administrator Types  
  
|Administrator Type|Description|  
|------------------------|-----------------|  
|[!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] system administrator|Permissions assigned in [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)] have no effect on the administrator's access.<br /><br /> Automatically has **Update** permission to all models.<br /><br /> Automatically has access to all functional areas.<br /><br /> In mdm.tblUser, the value in the **ID** column is **1**.|  
|Model administrator|Permissions assigned in [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)] determine whether or not the user is a model administrator.<br /><br /> Can be a model administrator based on permissions assigned explicitly or permissions inherited from a group.<br /><br /> Is an administrator only for models that have **Update** permission assigned to top-level model object, and no other permissions.<br /><br /> Has access only to functional areas that access is granted to.<br /><br /> In mdm.tblUser, the value in the **ID** column is not **1**.|  
  
## See Also  
 [Create a Model Administrator &#40;Master Data Services&#41;](create-a-model-administrator-master-data-services.md)   
 [Change the System Administrator Account &#40;Master Data Services&#41;](../../2014/master-data-services/change-the-system-administrator-account-master-data-services.md)   
 [Create a Master Data Services Database](install-windows/create-a-master-data-services-database.md)   
 [Notifications &#40;Master Data Services&#41;](../../2014/master-data-services/notifications-master-data-services.md)  
  
  
