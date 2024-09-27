---
title: "Secure shared data source items"
description: "Secure shared data source items"
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: security
ms.topic: conceptual
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "shared data sources [Reporting Services]"
  - "data sources [Reporting Services], shared"
  - "security [Reporting Services], data sources"
---
# Secure shared data source items
  You can set security on a shared data source item to enable or disable access to it.  
  
 A user who has minimal access to a shared data source (for example, the access granted through the **Browser** role) can view the list of reports that use the item, provided the user is also authorized to view the reports themselves.  
  
 A user who has more access (such as access granted through the **Content Manager** role) can view and set properties for the shared data source.  
  
 To set security, you create a role assignment that specifies which user or group account has access to the shared data source. Users who have access to a shared data source item can change its name, description, connection string, or credential information.  
  
## Tasks and access to items  
 When creating role assignments, use a role that has these tasks to assign appropriate permissions to users and groups.  
  
|Select this task|To give users permission to|  
|----------------------|---------------------------------|  
|View data sources|View the shared data source item in the folder hierarchy. Without this task, the item isn't visible to users and they might not know that the data source is available.|  
|Manage data sources|View properties that specify the name, description, and connection information. This task is also used to display a shared data source item in the folder hierarchy. If you choose this task, you can omit the "View data sources" task.|  
|Set security on items|Create and modify role assignments that control access to the shared data source. This task must be used with either "View data source" or "Manage data sources" tasks. If it isn't, it has no effect because the user can't select the item.|  
  
## Related content

- [Manage report data sources](../../reporting-services/report-data/manage-report-data-sources.md)
- [Secure folders](../../reporting-services/security/secure-folders.md)
- [Secure reports and resources](../../reporting-services/security/secure-reports-and-resources.md)
- [Grant permissions on a native mode report server](../../reporting-services/security/granting-permissions-on-a-native-mode-report-server.md)
- [Store credentials in a Reporting Services data source](../../reporting-services/report-data/store-credentials-in-a-reporting-services-data-source.md)
