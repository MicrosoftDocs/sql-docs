---
title: "Securable Items | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "securable items [Reporting Services]"
  - "roles [Reporting Services], securable items"
  - "security [Reporting Services], securable items listed"
  - "role-based security [Reporting Services], securable items"
ms.assetid: 27f58d4c-5c7b-4947-af5b-0f1fa60faf5f
author: markingmyname
ms.author: maghan
manager: kfile
---
# Securable Items
  [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] uses role-based security to control access to items that are stored on a report server. When you grant a user access to a report server, you typically do so by creating a pair of role assignments:  
  
-   At the site level  
  
-   On Home, which is the root node of the report server folder hierarchy  
  
 Security is inherited within the report server folder hierarchy. Creating role assignments at the site level and on the Home folder sets permission inheritance that extends to all items and operations on a report server.  
  
 You can override permission inheritance by defining security for individual items. Items that you can secure individually include:  
  
-   Folders  
  
-   Reports  
  
-   Report models  
  
-   Resources  
  
-   Shared data sources  
  
-   Shared datasets  
  
 Other constructs, such as schedules and subscriptions, are not explicitly secured. Schedules and subscriptions operate within the security of a report.  
  
## Item Descriptions  
 The following table lists securable items and describes their characteristics.  
  
|Item|Characteristics|  
|----------|---------------------|  
|Folders|Folder security applies to the folder itself and the items it contains. The Home folder is the root node of the folder hierarchy. Security that you set for this folder establishes the initial security settings for all subordinate folders, reports, resources, and shared data sources in the folder hierarchy. For more information, see [Secure Folders](secure-folders.md).<br /><br /> My Reports is a special-purpose folder that is secured through an implied role assignment based on a dedicated role. For more information, see [Secure My Reports](secure-my-reports.md).|  
|Reports|Reports and linked reports can be secured to control the range of actions that users can perform, such as changing the properties of a given report.<br /><br /> Report history is secured through the report that contains the history. You cannot secure individual snapshots within report history.<br /><br /> For more information about report security, see [Secure Reports and Resources](secure-reports-and-resources.md).|  
|Report models|You can specify role assignment on all or part of a report model. Because report models can be quite extensive, you might want to secure the model items that map to confidential data.|  
|Resources|Resources can be secured to control access to the resource itself and its properties.<br /><br /> Only stand-alone resources can be secured as separate items. Resources that are embedded within a report cannot be secured separately from that report.<br /><br /> For more information about resource security, see [Secure Reports and Resources](secure-reports-and-resources.md).|  
|Shared data sources|Shared data sources can be secured to limit access to the item and its property pages. For more information, see [Secure Shared Data Source Items](secure-shared-data-source-items.md).|  
|Shared datasets|Shared datasets can be secured to control the range of actions that users can perform, such as viewing or changing the definition, or changing the properties of a given shared dataset.<br /><br /> For more information, see [Secure Shared Dataset Items](secure-shared-dataset-items.md).|  
  
## See Also  
 [Granting Permissions on a Native Mode Report Server](granting-permissions-on-a-native-mode-report-server.md)   
 [Create, Delete, or Modify a Role &#40;Management Studio&#41;](role-definitions-create-delete-or-modify.md)   
 [Grant User Access to a Report Server &#40;Report Manager&#41;](grant-user-access-to-a-report-server.md)   
 [Modify or Delete a Role Assignment &#40;Report Manager&#41;](role-assignments-modify-or-delete.md)  
  
  
