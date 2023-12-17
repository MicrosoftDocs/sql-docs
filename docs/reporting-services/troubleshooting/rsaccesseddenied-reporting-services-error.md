---
title: "rsAccessedDenied - Reporting Services error"
description: "In this error reference, learn about 'rsAccessedDenied': The permissions granted to user 'mydomain\\myAccount' are insufficient for performing this operation."
author: maggiesMSFT
ms.author: maggies
ms.date: 05/22/2019
ms.service: reporting-services
ms.subservice: troubleshooting
ms.topic: conceptual
ms.custom: updatefrequency5
helpviewer_keywords:
  - "rsAccessDenied error"
---
# rsAccessedDenied - Reporting Services error
  The [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] error **rsAccessedDenied** occurs when a user doesn't have permission to perform an action. For example, they don't have a role assignment that allows them to open a report, or they didn't open their browser with the required permissions.  
  
||  
|-|  
|**[!INCLUDE[applies](../../includes/applies-md.md)]**  [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Native mode &#124; SharePoint mode|  
  
- If the error occurred while accessing the report server directly through a URL, the exception is mapped to an HTTP 401 error.  
  
- If the error occurs when you use the web portal, the exception is typically mapped to an HTTP 401 error, or other defined HTML error page.  
  
- If the error occurred during a scheduled operation, subscription, or delivery, the error appears in the report server log file only.  
  
## Details  
  
|Detail|Value|  
|-|-|  
|**Product Name**|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]|  
|**Event ID**|rsAccessedDenied|  
|**Event Source**|Microsoft.ReportingServices.Diagnostics.Utilities.ErrorStrings|  
|**Component**|[!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]|  
|**Message Text**|The permissions granted to user 'mydomain\myAccount' are insufficient for performing this operation. (rsAccessDenied) (ReportingServicesLibrary)|  
  
## User action  
 Permission to access report server content and operations are granted through role assignments. On a new installation, only local administrators have access to a report server. To grant access to other users, a local administrator must create a role assignment that specifies a domain user or group account. The administrator must also create one or more roles that define the tasks the user can perform and a scope. The scope is usually the Home folder or root node of the report server folder hierarchy. You can use the web portal to create role assignments. For more information, see [Role Assignments](../../reporting-services/security/role-assignments.md).  
  
 Local administration of the report server causes this error. For more information, see [Configure a native mode report server for local administration &#40;SSRS&#41;](../../reporting-services/report-server/configure-a-native-mode-report-server-for-local-administration-ssrs.md).  
  
## Related content 
 [Role assignments](../../reporting-services/security/role-assignments.md)  
 [Grant permissions on a native mode report server](../../reporting-services/security/granting-permissions-on-a-native-mode-report-server.md)  
 [Roles and permissions &#40;Reporting Services&#41;](../../reporting-services/security/roles-and-permissions-reporting-services.md)  
  
