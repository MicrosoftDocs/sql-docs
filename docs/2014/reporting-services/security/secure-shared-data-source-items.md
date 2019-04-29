---
title: "Secure Shared Data Source Items | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "shared data sources [Reporting Services]"
  - "data sources [Reporting Services], shared"
  - "security [Reporting Services], data sources"
ms.assetid: 7299e498-0a1a-4821-a22a-5199bb773ce0
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Secure Shared Data Source Items
  You can set security on a shared data source item to enable or disable access to it.  
  
 A user who has minimal access to a shared data source (for example, the access granted through the **Browser** role) can view the list of reports that use the item, provided the user is also authorized to view the reports themselves.  
  
 A user who has additional access (such as that granted through the **Content Manager** role) can view and set properties for the shared data source.  
  
 To set security, you create a role assignment that specifies which user or group account has access to the shared data source. Users who have access to a shared data source item can change its name, description, connection string, or credential information.  
  
## Tasks and Access to Items  
 When creating role assignments, use a role that has these tasks to assign appropriate permissions to users and groups.  
  
|Select this task|To give users permission to|  
|----------------------|---------------------------------|  
|View data sources|View the shared data source item in the folder hierarchy. Without this task, the item is not visible to users and they may not be aware that the data source is available.|  
|Manage data sources|View properties that specify the name, description, and connection information. This task is also used to display a shared data source item in the folder hierarchy. If you choose this task, you can omit the "View data sources" task.|  
|Set security on items|Create and modify role assignments that control access to the shared data source. This task must be used with either "View data source" or "Manage data sources" tasks. If it is not, it has no effect because the user cannot select the item.|  
  
## See Also  
 [Manage Report Data Sources](../report-data/manage-report-data-sources.md)   
 [Secure Folders](secure-folders.md)   
 [Secure Reports and Resources](secure-reports-and-resources.md)   
 [Granting Permissions on a Native Mode Report Server](granting-permissions-on-a-native-mode-report-server.md)   
 [Store Credentials in a Reporting Services Data Source](../report-data/store-credentials-in-a-reporting-services-data-source.md)  
  
  
