---
title: "Item-Level Tasks | Microsoft Docs"
ms.date: 03/01/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: security


ms.topic: conceptual
helpviewer_keywords: 
  - "item-level tasks [Reporting Services]"
ms.assetid: fdeb7bc3-167a-4342-84e3-32e3faa1fa39
author: markingmyname
ms.author: maghan
---
# Tasks and Permissions - Item-Level Tasks
  An item-level task is a collection of permissions that relate to a report, folder, report model, resource, or shared data source. [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] also includes system-level tasks that apply to the report server site as a whole. For more information, see [System-Level Tasks](../../reporting-services/security/tasks-and-permissions-system-level-tasks.md). For more information about tasks and permissions in general, see [Tasks and Permissions](../../reporting-services/security/tasks-and-permissions.md).  
  
> [!NOTE]  
>  If you are working with these tasks programmatically, you must use methods that support item-level tasks. For more information, see <xref:ReportService2010.ReportingService2010.ListTasks%2A> and <xref:ReportService2010.ReportingService2010.ListRoles%2A>.  
  
## Permissions in Item-Level Tasks  
 The following table lists item-level tasks, the permissions that are included in each task, and the items to which the permissions apply. Permissions are listed for informational purposes only to provide a more exact description of the functionality available through each task.  
  
 Shared datasets use the same set of permissions as reports. Report parts use the same set of permissions as Resources.  
  
|Task|Applies to Item|Permissions|  
|----------|---------------------|-----------------|  
|Consume reports|Reports|Read Content<br /><br /> Read Report Definitions<br /><br /> Read Properties|  
|Consume reports|Shared Datasets|Read Content<br /><br /> Read Report Definitions<br /><br /> Read Properties|  
|Create linked reports|Reports|Create Link<br /><br /> Read Properties|  
|Manage all subscriptions|Reports|Read Properties<br /><br /> Read Any Subscription<br /><br /> Create Any Subscription<br /><br /> Delete Any Subscription<br /><br /> Update Any Subscription|  
|Manage data sources|Folders|Create Data Source|  
|Manage data sources|Data Sources|Update Properties<br /><br /> Delete Update Content<br /><br /> Read Properties|  
|Manage folders|Folders|Create Folder<br /><br /> Delete Update Properties<br /><br /> Read Properties|  
|Manage individual subscriptions|Reports|Read Properties<br /><br /> Create Subscription<br /><br /> Delete Subscription<br /><br /> Read Subscription<br /><br /> Update Subscription|  
|Manage models|Folders|Create Model|  
|Manage models|Models|Read Properties<br /><br /> Read Content<br /><br /> Delete Update Content<br /><br /> Read Data Sources<br /><br /> Update Data Sources<br /><br /> Read Model Item Authorization Policies<br /><br /> Update Model Item Authorization Policies<br /><br /> Delete Update Properties|  
|Manage report history|Reports|Read Properties<br /><br /> Create Report History<br /><br /> Delete Report History<br /><br /> Execute Read Policy<br /><br /> Update Policy<br /><br /> List Report History|  
|Manage reports|Folders|Create Report<br /><br /> also applies to creating shared datasets|  
|Manage reports|Reports|Read Properties<br /><br /> Delete Update Properties<br /><br /> Update Parameters<br /><br /> Read Data Sources<br /><br /> Update Data Sources<br /><br /> Read Report Definition<br /><br /> Update Report Definition<br /><br /> Execute Read Policy<br /><br /> Update Policy|  
|Manage reports|Shared Datasets|Read Properties<br /><br /> Delete Update Properties<br /><br /> Update Parameters<br /><br /> Read Data Sources<br /><br /> Update Data Sources<br /><br /> Read Report Definition<br /><br /> Update Report Definition<br /><br /> Execute Read Policy<br /><br /> Update Policy|  
|Manage resources|Folders|Create Resource|  
|Manage resources|Resources|Update Properties<br /><br /> Delete Update Content<br /><br /> Read Properties|  
|Manage resources|Report Parts|Update Properties<br /><br /> Delete Update Content<br /><br /> Read Properties|  
|Set security for individual items|Reports, Resources, Data sources, Shared Datasets, Folders|Read Security Policies Update Security Policies|  
|View data sources|Data sources|Read Content<br /><br /> Read Properties|  
|View folders|Folders|Read Properties<br /><br /> Execute And View<br /><br /> List Report History|  
|View models|Report models|Read Properties<br /><br /> Read Content<br /><br /> Read Data Sources|  
|View reports|Reports|Read Content<br /><br /> Read Properties|  
|View reports|Shared Datasets|Read Content<br /><br /> Read Properties|  
|View resources|Resources|Read Content<br /><br /> Read Properties|  
|View resources|Report Parts|Read Content<br /><br /> Read Properties|  
  
## See Also  
 [Granting Permissions on a Native Mode Report Server](../../reporting-services/security/granting-permissions-on-a-native-mode-report-server.md)  
  
  
