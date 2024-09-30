---
title: "New system role (Management Studio)"
description: Learn about the New System Role page in Management Studio where you create a system-level role definition that specifies a set of tasks that apply to a report server as a whole.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: tools
ms.topic: conceptual
ms.custom:
  - updatefrequency5
f1_keywords:
  - "sql13.swb.reportserver.newsystemrole.f1"
---
# New system role (Management Studio)
  Use this page to create a system-level role definition. A system role definition specifies a set of system-level tasks that apply to a report server as whole.  
  
> [!NOTE]  
>  Role definitions are used only on a report server that runs in native mode. If the report server is configured for SharePoint integration, this page is not available.  
  
## Options  
 **Name**  
 Enter the name of the role definition. A role definition name must be unique within the report server namespace. A name must contain at least one alphanumeric character. It can also include spaces and some symbols. Don't use the following characters when specifying a name:  
  
 `; ? : \@ & = + , $ / * < > " /`  
  
 **Description**  
 Provide a description that explains how to use the role and enumerates what the role supports.  
  
 **Task**  
 Select the system-level tasks that can be performed through this role. You can't create new tasks or modify the existing tasks that [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] supports. You can't choose item-level tasks for a system role definition.  
  
 **Task Description**  
 Shows a description of the task that enumerates the operations or permissions that the task supports.  
  
## Related content

- [Report server in Management Studio F1 Help](../../reporting-services/tools/report-server-in-management-studio-f1-help.md)
- [Role definitions](../../reporting-services/security/role-definitions.md)
