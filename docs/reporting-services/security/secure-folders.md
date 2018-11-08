---
title: "Secure Folders | Microsoft Docs"
ms.date: 03/01/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: security


ms.topic: conceptual
helpviewer_keywords: 
  - "high-security folders [Reporting Services]"
  - "low-security folders"
  - "folders [Reporting Services], security"
  - "security [Reporting Services], folders"
ms.assetid: 0fd91f77-0143-476b-9af0-87293be78e44
author: markingmyname
ms.author: maghan
---
# Secure Folders
  Folder security is the foundation for securing all content in a report server. Because security is inherited throughout the folder structure, you can designate large or small sections of the folder hierarchy to allow for certain kinds of access.  
  
 High-security folders can be used to store confidential reports or as staging areas; for example, you can have a folder that you use to test reports before moving them to a final location. To control access to this area, you can define one role assignment that allows only report authors to add and delete items, and a second role assignment that allows testers to run reports but not to add or remove items. Because the role assignments are defined explicitly for testers and report authors, no other users (except for local system administrators) can access the folder.  
  
 Low-security folders can be used to store reports that you want to be easily accessible.  
  
 Folder security forms the basis of item-level security, starting with the root node of the report server folder hierarchy, the Home folder. Because security is inherited, it is advisable to set a fairly restrictive security policy on the Home folder. Using the **Browser** role in Home folder role assignments does exactly that by providing view-only access.  
  
## Tasks and Folder Access  
 When creating role assignments for folders, consider the tasks listed in the following table.  
  
|Select this task|To give permission to|  
|----------------------|---------------------------|  
|View folders|View the folder hierarchy and read-only properties that indicate when the folder was created and modified.<br /><br /> Users cannot view items in the folder unless they are assigned to roles that also include the following tasks: "View reports," "View models", "View resources," and "View data sources."|  
|Manage folders|View folder properties, change the name or description, or move the folder to another location. This task allows users to create folders.|  
|Manage reports|Add reports from the file system to a folder and publish reports from Report Designer to the report server.|  
|Manage data sources|Add new shared data source items to a folder and change existing shared data sources.|  
|Set security on items|Create and modify role assignments that control access to the folder. This task must be used with either "View folders" or "Manage folders." If it is not, it will have no effect because the user will not be able to select the item.|  
  
## See Also  
 [Secure Reports and Resources](../../reporting-services/security/secure-reports-and-resources.md)   
 [Secure Shared Data Source Items](../../reporting-services/security/secure-shared-data-source-items.md)   
 [Granting Permissions on a Native Mode Report Server](../../reporting-services/security/granting-permissions-on-a-native-mode-report-server.md)  
  
  
