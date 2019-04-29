---
title: "rsAccessedDenied - Reporting Services Error | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "rsAccessDenied error"
ms.assetid: 2f76b1bf-96a2-4755-b76b-84e933220efc
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# rsAccessedDenied - Reporting Services Error
  The [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] error **rsAccessedDenied** occurs when a user does not have permission to perform an action. For, example, they do not have a role assignment that allows them to open a report or they did not open their browser with the required permissions.  
  
||  
|-|  
|**[!INCLUDE[applies](../../includes/applies-md.md)]**  [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Native mode &#124; SharePoint mode|  
  
-   If the error occurred while accessing the report server directly through a URL, the exception is mapped to an HTTP 401 error.  
  
-   If the error occurred while using Report Manager or another tool, the error appears in an error page.  
  
-   If the error occurred during a scheduled operation, subscription, or delivery, the error will appear in the report server log file only.  
  
## Details  
  
|||  
|-|-|  
|**Product Name**|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]|  
|**Event ID**|rsAccessedDenied|  
|**Event Source**|Microsoft.ReportingServices.Diagnostics.Utilities.ErrorStrings|  
|**Component**|[!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]|  
|**Message Text**|The permissions granted to user 'mydomain\myAccount' are insufficient for performing this operation. (rsAccessDenied) (ReportingServicesLibrary)|  
  
## User Action  
 Permission to access report server content and operations are granted through role assignments. On a new installation, only local administrators have access to a report server. To grant access to other users, a local administrator must create a role assignment that specifies a domain user or group account, one or more roles that define the tasks the user can perform, and a scope (usually the Home folder or root node of the report server folder hierarchy). You can use Report Manager to create the role assignments. For more information, search for "Role Assignments" in SQL Server Books Online.  
  
 This error is also caused by local administration of the report server. For more information, see [Configure a Native Mode Report Server for Local Administration &#40;SSRS&#41;](../report-server/configure-a-native-mode-report-server-for-local-administration-ssrs.md).  
  
## See Also  
 [Role Assignments](../security/role-assignments.md)   
 [Granting Permissions on a Native Mode Report Server](../security/granting-permissions-on-a-native-mode-report-server.md)   
 [Roles and Permissions &#40;Reporting Services&#41;](../security/roles-and-permissions-reporting-services.md)  
  
  
