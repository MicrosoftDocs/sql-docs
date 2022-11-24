---
title: "rsAccessedDenied - Reporting Services Error | Microsoft Docs"
description: "In this error reference, learn about 'rsAccessedDenied': The permissions granted to user 'mydomain\\myAccount' are insufficient for performing this operation."
ms.date: 05/22/2019
ms.service: reporting-services
ms.subservice: troubleshooting


ms.topic: conceptual
helpviewer_keywords: 
  - "rsAccessDenied error"
ms.assetid: 2f76b1bf-96a2-4755-b76b-84e933220efc
author: maggiesMSFT
ms.author: maggies
---
# rsAccessedDenied - Reporting Services error
  The [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] error **rsAccessedDenied** occurs when a user does not have permission to perform an action. For example, they don't have a role assignment that allows them to open a report, or they didn't open their browser with the required permissions.  
  
||  
|-|  
|**[!INCLUDE[applies](../../includes/applies-md.md)]**  [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Native mode &#124; SharePoint mode|  
  
- If the error occurred while accessing the report server directly through a URL, the exception is mapped to an HTTP 401 error.  
  
- If the error occurred while using the web portal, the exception is typically mapped to an HTTP 401 error, or other defined HTML error page.  
  
- If the error occurred during a scheduled operation, subscription, or delivery, the error will appear in the report server log file only.  
  
## Details  
  
|Detail|Value|  
|-|-|  
|**Product Name**|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]|  
|**Event ID**|rsAccessedDenied|  
|**Event Source**|Microsoft.ReportingServices.Diagnostics.Utilities.ErrorStrings|  
|**Component**|[!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]|  
|**Message Text**|The permissions granted to user 'mydomain\myAccount' are insufficient for performing this operation. (rsAccessDenied) (ReportingServicesLibrary)|  
  
## User action  
 Permission to access report server content and operations are granted through role assignments. On a new installation, only local administrators have access to a report server. To grant access to other users, a local administrator must create a role assignment that specifies a domain user or group account, one or more roles that define the tasks the user can perform, and a scope (usually the Home folder or root node of the report server folder hierarchy). You can use the web portal to create role assignments. For more information, see [Role Assignments](../../reporting-services/security/role-assignments.md)
.  
  
 This error is also caused by local administration of the report server. For more information, see [Configure a Native Mode Report Server for Local Administration &#40;SSRS&#41;](../../reporting-services/report-server/configure-a-native-mode-report-server-for-local-administration-ssrs.md).  
  
## See also  
 [Role Assignments](../../reporting-services/security/role-assignments.md)  
 [Granting Permissions on a Native Mode Report Server](../../reporting-services/security/granting-permissions-on-a-native-mode-report-server.md)  
 [Roles and Permissions &#40;Reporting Services&#41;](../../reporting-services/security/roles-and-permissions-reporting-services.md)  
  