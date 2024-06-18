---
title: Roles and permissions in Reporting Services
description: Learn how you can use the roles and permissions tools in Reporting Services to manage your report servers.
author: maggiesMSFT
ms.author: maggies
ms.date: 06/13/2024
ms.service: reporting-services
ms.subservice: security
ms.topic: conceptual
ms.custom: updatefrequency5
helpviewer_keywords:
  - "access controls [Reporting Services]"
  - "permissions [Reporting Services], about permissions"
  - "security [Reporting Services], identity and access control"
  - "authentication [Reporting Services]"
  - "permissions [Reporting Services]"
  - "security [Reporting Services], role-based"
  - "identity [Reporting Services]"

#customer intent: As a Reporting Services administrator, I want to learn how roles and permissions work in Reporting Services so that I can better secure the data in my report servers.
---
# Roles and permissions in Reporting Services

Reporting Services provides an authentication subsystem and role-based authorization model. Authentication and authorization models vary depending on whether the report server runs in native mode or SharePoint mode. If the report server is part of a SharePoint deployment, SharePoint permissions determine who has access to the report server.  
  
## Identity and access control for native mode  

Default authentication is based on Windows Authentication and integrated security. You can change the authentication settings to allow the report server to respond to different authentication requests. Or, you can replace the default security features with a custom authentication extension that you provide. For more information, see [Grant permissions on a native mode report server](/sql/reporting-services/security/granting-permissions-on-a-native-mode-report-server).
  
Authorization is based on roles that you assign to a principal. Each role consists of a set of related tasks, which are in turn composed of related operations. For example, the **Content Manager** role has the **Manage reports** task. This task grants access to the following report server operations: view reports, add reports, update reports, delete reports, schedule reports, and update report properties.

### Roles and System Roles

Report servers have two types of roles that you can define: **Roles** and **System Roles**.

**Roles** come with the following tasks that you can assign:

|Task                             |Description                                                                                                                      |
|-------------------------------------------------------------------------------------------------------------------------------------------------------------------|
|Set security for individual items|View and modify security settings for reports, folders, resources, and shared data sources.                                      |
|Create linked reports            |Create linked reports and publish them to a report server folder.                                                                |
|View reports                     |View reports and linked reports in the folder hierarchy; view report history snapshots and report properties.                    |
|Manage reports                   |Create and delete reports; and modify report properties.                                                                         |
|View resources                   |View resources in the folder hierarchy; and view resource properties.                                                            |
|Manage resources                 |Create, modify and delete resources, and modify resource properties.                                                             |
|View Folders                     |View folder items in the folder hierarchy; and view folder properties.                                                           |
|Manage folders                   |Create, view, and delete folders; and view and modify folder properties.                                                         |
|Manage report history            |Create, view, and delete report history snapshots; and modify report history properties.                                         |
|Manage individual subscriptions  |Each user can create, view, modify, and delete subscriptions that they own.                                                 |
|Manage all subscriptions         |View, modify, and delete any subscription regardless of who owns the subscription.                                               |
|View data sources                |View shared data source items in the folder hierarchy; and view data source properties.                                          |
|Manage data sources              |Create and delete shared data source items; and modify data source properties.                                                   |
|View models                      |View models in the folder hierarchy, use models as data sources for a report, and run queries against the model to retrieve data.|
|Manage models                    |Create, view, and delete models; and view and modify model properties.                                                           |
|Consume reports                  |Reads report definitions.                                                                                                        |
|Comment on reports               |Create, view, edit, and delete comments on reports.                                                                              |
|Manage comments                  |Delete other users' comments on reports.                                                                                         |

**System Roles** come with the following tasks that you can assign:

|Task                           |Descriptions                                                                                         |
|-------------------------------|-----------------------------------------------------------------------------------------------------|
|Manage roles                   |Create, view, modify and delete role definitions.                                                    |
|Manage report server security  |View and modify system-wide role assignments.                                                        |
|View report server properties  |View properties that apply to the report server.                                                     |
|Manage report server properties|View and modify properties that apply to the report server and to items managed by the report server.|
|View shared schedules          |View a predefined schedule that made available for general use.                             |
|Manage shared schedules        |Create, view, modify and delete shared schedules used to run reports or refresh a report.            |
|Generate events                |Provides an application with the ability to generate events within the report server namespace.      |
|Manage jobs                    |View and cancel running jobs.                                                                        |
|Execute Report Definitions     |Start execution from report definition without publishing it to Report Server.                       |

## Identity and access control for SharePoint mode  

In SharePoint integrated mode, authentication and authorization are handled on the SharePoint site, before requests reach the report server. Depending on how you configure authentication, requests from a SharePoint site include a security token or a trusted user name. Permissions that you set for SharePoint users and groups authorize access to report server items that are placed in SharePoint libraries. For more information, see [Grant permissions on report server items on a SharePoint site](/sql/reporting-services/security/granting-permissions-on-report-server-items-on-a-sharepoint-site).
  
## Related content

- [Authentication with the report server](../../reporting-services/security/authentication-with-the-report-server.md)
- [Grant permissions on a native mode report server](../../reporting-services/security/granting-permissions-on-a-native-mode-report-server.md)  
  