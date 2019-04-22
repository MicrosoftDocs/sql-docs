---
title: "Roles and Permissions (Reporting Services) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "access controls [Reporting Services]"
  - "permissions [Reporting Services], about permissions"
  - "security [Reporting Services], identity and access control"
  - "authentication [Reporting Services]"
  - "permissions [Reporting Services]"
  - "security [Reporting Services], role-based"
  - "identity [Reporting Services]"
ms.assetid: eea655fe-43ed-418d-8233-b288a8f4daa4
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Roles and Permissions (Reporting Services)
  Reporting Services provides an authentication subsystem and role-based authorization model. Authentication and authorization models vary depending on whether the report server runs in native mode or SharePoint mode. If the report server is part of a SharePoint deployment, SharePoint permissions determine who has access to the report server.  
  
## Identity and Access Control for Native Mode  
 Default authentication is based on Windows Authentication and integrated security. You can change the authentication settings to allow the report server to respond to different authentication requests, or even replace the default security features with a custom authentication extension that you provide.  
  
 Authorization is based on roles that you assign to a principle. Each role consists of a set of related tasks, which are in turn composed of related operations. For example, the **Manage reports** task grants access to the following report server operations: view reports, add report, update report, delete report, schedule report, and update report properties.  
  
## Identity and Access Control for SharePoint Mode  
 In SharePoint integrated mode, authentication and authorization are handled on the SharePoint site, before requests reach the report server. Depending on how you configure authentication, requests from a SharePoint site include a security token or a trusted user name. Permissions that you set for SharePoint users and groups authorize access to report server items that are placed in SharePoint libraries.  
  
## In This Section  
 [Granting Permissions on a Native Mode Report Server](granting-permissions-on-a-native-mode-report-server.md)  
 Describes the role-based authorization model that provides access to content and operations.  
  
 [Granting Permissions on Report Server Items on a SharePoint Site](granting-permissions-on-report-server-items-on-a-sharepoint-site.md)  
 Explains how SharePoint groups, permission levels, and permissions are used to control access to a report server.  
  
## See Also  
 [Authentication with the Report Server](authentication-with-the-report-server.md)   
 [Granting Permissions on a Native Mode Report Server](granting-permissions-on-a-native-mode-report-server.md)  
  
  
