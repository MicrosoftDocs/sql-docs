---
title: "Versions (Master Data Services) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "mds"
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
helpviewer_keywords: 
  - "version flags [Master Data Services], about version flags"
  - "versions [Master Data Services]"
  - "version flags [Master Data Services]"
  - "versions [Master Data Services], version flags"
ms.assetid: 752ec96d-53d7-4160-8ed2-92e0324645f3
author: leolimsft
ms.author: lle
manager: craigg
---
# Versions (Master Data Services)

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

  In [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)], you can create multiple versions of the master data within a model. Versions can be locked while you validate your data and committed after the data is validated. Committed versions form an auditable record of changes. Each version you create contains all members, attribute values, hierarchy members, hierarchy relationships, and collections for the model.  
  
## When to Use Versions  
 Use versions to:  
  
-   Maintain an auditable record of your master data as it changes over time.  
  
-   Prevent users from making changes while you ensure all data validates successfully against business rules.  
  
-   Lock down a model for use by subscribing systems.  
  
-   Test different hierarchies without implementing them right away.  
  
> [!NOTE]  
>  When you change the structure of your model, such as when you create a new entity or domain-based attribute, the change applies to all versions. If you view an earlier version of the model, the entity or attribute is displayed, but no data exists.  
  
## Version Flags  
 When a version is ready for users or for a subscribing system, you can set a flag to identify the version. You can move this flag from version to version as needed. Flags help users and subscribing systems identify which version of a model to use.  
  
## Workflow for Version Management  
 Use the following workflow for version management:  
  
1.  An initial version is created automatically when you create a model and populate the [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] database with your company's master data. Based on permissions, users can make changes to this version as needed.  
  
2.  When you want to commit a version of a model, lock the version so that only model administrators can update the data. For more information, see [Administrators &#40;Master Data Services&#41;](../master-data-services/administrators-master-data-services.md). If notifications are configured, an email notification is sent to model administrators each time the version's status changes. For more information, see [Configure Email Notifications &#40;Master Data Services&#41;](../master-data-services/configure-email-notifications-master-data-services.md).  
  
3.  Apply business rules to the locked version's data and review any validation issues. If necessary, you can fill in missing information or revert the transaction that caused the issue. You can also unlock the version for users to make changes.  
  
4.  When all the data passes validation, commit the version and flag it for use by subscribing systems. Changes cannot be made to a committed version.  
  
5.  Copy the committed version and notify users that they can begin working in a new version of the model.  
  
## Sequential or Simultaneous Versions  
 You can create sequential or simultaneous versions of your model.  
  
-   **Sequential versions.** Each time you commit a version, create a new copy and give the version the next sequential number. For example, you can copy **Version 7** of your model and name the copy **Version 8**.  
  
-   **Simultaneous versions.** Create simultaneous versions of your model when you want to work on two or more versions of your data at once. This is useful when your company has reorganizations or mergers that coincide with the normal course of business and you want to determine how the new master data might fit into your existing structures.  
  
    > [!NOTE]  
    >  A setting in [!INCLUDE[ssMDScfgmgr](../includes/ssmdscfgmgr-md.md)] determines whether or not you can copy all versions or only those that are committed. To create simultaneous versions you must configure [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] to allow you to copy all versions. This setting is also available in the System Settings table. For more information, see [System Settings &#40;Master Data Services&#41;](../master-data-services/system-settings-master-data-services.md).  
  
## Related Tasks  
  
|Task Description|Topic|  
|----------------------|-----------|  
|Change the name of an existing version.|[Change a Version Name &#40;Master Data Services&#41;](../master-data-services/change-a-version-name-master-data-services.md)|  
|Lock a version so only administrators can edit its data.|[Lock a Version &#40;Master Data Services&#41;](../master-data-services/lock-a-version-master-data-services.md)|  
|Unlock a version so users can edit its data.|[Unlock a Version &#40;Master Data Services&#41;](../master-data-services/unlock-a-version-master-data-services.md)|  
|Commit a version after all data is validated.|[Commit a Version &#40;Master Data Services&#41;](../master-data-services/commit-a-version-master-data-services.md)|  
|Create a new flag to mark a version.|[Create a Version Flag &#40;Master Data Services&#41;](../master-data-services/create-a-version-flag-master-data-services.md)|  
|Change the name of an existing version flag.|[Change a Version Flag Name &#40;Master Data Services&#41;](../master-data-services/change-a-version-flag-name-master-data-services.md)|  
|Assign an existing flag to a version.|[Assign a Flag to a Version &#40;Master Data Services&#41;](../master-data-services/assign-a-flag-to-a-version-master-data-services.md)|  
|Create a new copy of an existing version|[Copy a Version &#40;Master Data Services&#41;](../master-data-services/copy-a-version-master-data-services.md)|  
|Delete an existing version.|[Delete a Version &#40;Master Data Services&#41;](../master-data-services/delete-a-version-master-data-services.md)|  
|Purge soft-deleted members from a version|[Purge Version Members &#40;Master Data Services&#41;](../master-data-services/purge-version-members-master-data-services.md)|  
  
## Related Content  
  
-   [Reverse a Transaction &#40;Master Data Services&#41;](../master-data-services/reverse-a-transaction-master-data-services.md)  
  
-   [Notifications &#40;Master Data Services&#41;](../master-data-services/notifications-master-data-services.md)  
  
-   [Business Rules &#40;Master Data Services&#41;](../master-data-services/business-rules-master-data-services.md)  
  
  
