---
title: Administrators
description: "Learn about the types of administrators in Master Data Services: model administrators, entity administrators, and super user."
ms.custom: ""
ms.date: "03/01/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: master-data-services
ms.topic: conceptual
helpviewer_keywords: 
  - "administrators [Master Data Services], about administrators"
  - "administrators [Master Data Services]"
  - "models [Master Data Services], administrators"
ms.assetid: d330aa4e-6ade-4b09-b376-1b15d6c78f7d
author: CordeliaGrey
ms.author: jiwang6
---
# Administrators (Master Data Services)

[!INCLUDE [SQL Server - Windows only ASDBMI  ](../includes/applies-to-version/sql-windows-only-asdbmi.md)]

  This article describes the types of administrators in [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)]: model administrators, entity administrators, and super user.  
  
## Model Administrators  
 In [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)], a model administrator is a user who has **Admin** permission assigned to the top-level model object on the **Model Objects** tab. When a user has Admin permission on a particular model, any other permissions on the model's child objects (both model object and member permissions) are trumped by the model **Admin** permission and effectively ignored.  
  
-   If the user has access to the **Explorer** functional area, the user can add, delete, and update all master data in this area.  
  
-   If the user has access to other functional areas, the user can perform all administrative tasks available in the functional area.  
  
 Each model can have multiple administrators. Each user can be a model administrator for one, several, or all models in the [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] deployment.  
  
 A user can be configured as a model administrator either in [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)] or programmatically. For more information, see [Create a Model Administrator &#40;Master Data Services&#41;](../master-data-services/create-a-model-administrator-master-data-services.md).  
  
## Entity Administrators  
 In [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)], an entity administrator is a user who has administrator permissions assigned to the entity object on the Model Objects tab. When a user has administrator permissions for an entity, any other permissions on the entity's child objects (both model object and member permissions) are superseded by the administrator permissions and are ignored.  
  
-   If the user has access to the **Explorer** functional area, the user can add, delete, and update all master data in this area.  
  
-   If the entity changes require administrator approval, an entity administrator can review and then approve or reject change sets.  
  
 Each entity can have multiple administrators. Each user can be a entity administrator for one, several, or all entities.  
  
 A user can be configured as an entity administrator either in [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] or programmatically. For more information, see [Create an Entity Administrator &#40;Master Data Services&#41;](../master-data-services/create-an-entity-administrator-master-data-services.md).  
  
## Master Data Services Super User  
 In [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)], you can assign a user permissions to the Super User functional area. A user with permissions to the Super User functional area effectively has Admin permission on all models and has permissions for all the other functional areas. For information on the permissions for the functional areas, see [Functional Area Permissions &#40;Master Data Services&#41;](../master-data-services/functional-area-permissions-master-data-services.md).  
  
 The default super user is specified for the **Administrator Account** when you create the [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] database by using the [Create Database Wizard &#40;Master Data Services Configuration Manager&#41;](../master-data-services/create-database-wizard-master-data-services-configuration-manager.md).  
  
 The super user can do the following:  
  
-   Access all functional areas.  
  
-   Add, delete, and update all master data for all models in the **Explorer** functional area.  
  
 You can assign Super User permissions to multiple users and/or user groups.  
  
## Comparing Administrator Types  
  
|Administrator Type|Description|  
|------------------------|-----------------|  
|[!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] Super User|Permissions assigned in [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)] have no effect on the administrator's access.<br /><br /> Can be a super user based on functional area permissions assigned explicitly or permissions inherited from a group.<br /><br /> Automatically has all permissions to all models.<br /><br /> Automatically has access to all functional areas.|  
|Model administrator|Can be a model administrator based on admin permissions assigned explicitly or permissions inherited from a group.<br /><br /> Has access only to functional areas that access is granted to.<br /><br /> Automatically has all permissions to all objects and members in the specific model.|  
|Entity administrator|Can be an entity administrator based on administrator permissions assigned explicitly or permissions inherited from a group.<br /><br /> Has access only to functional areas that access is granted to.<br /><br /> Automatically has all permissions to all objects and members in the specific entity.<br /><br /> Can approve the pending change sets if the entity changes require approval.|  
  
## External Resources  
 Blog post, [Security Improvements](/archive/blogs/e7/improvements-to-autoplay), on msdn.com.  
  
## See Also  
 [Create a Model Administrator &#40;Master Data Services&#41;](../master-data-services/create-a-model-administrator-master-data-services.md)   
 [Create a Master Data Services Database](../master-data-services/install-windows/create-a-master-data-services-database.md)   
 [Notifications &#40;Master Data Services&#41;](../master-data-services/notifications-master-data-services.md)  
  
