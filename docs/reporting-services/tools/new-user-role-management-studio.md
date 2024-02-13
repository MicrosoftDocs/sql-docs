---
title: "New User Role page (Management Studio)"
description: Learn how to create an item-level role definition that enumerates the tasks a user can perform in the New User Role page in SQL Server Management Studio.
author: maggiesMSFT
ms.author: maggies
ms.date: 03/01/2017
ms.service: reporting-services
ms.subservice: tools
ms.topic: conceptual
ms.custom: updatefrequency5
f1_keywords:
  - "sql13.swb.reportserver.newrole.f1"
---
# New User Role page (Management Studio)
  Use this page to create an item-level role definition. An item-level role definition is a named collection of tasks that enumerate the tasks a user can perform in relation to folders, reports, models, resources, and shared data sources. An example of an item-level role definition is the predefined Browser role that identifies the kinds of actions a report end user might require. These actions are for navigating folders and viewing reports.  
  
 Role definitions are intended to be few in number. Most organizations only require a few role definitions. However, if the predefined role definitions are insufficient, you can vary them or create new ones.  
  
> [!NOTE]  
>  Role definitions are used only on a report server that runs in native mode. If the report server is configured for SharePoint integration, this page is not available.  
  
## Options  
 **Name**  
 Enter the name of the role definition. A role definition name must be unique within the report server namespace. A name must contain at least one alphanumeric character. It can also include spaces and some symbols. Don't use the following characters when specifying a name:  
  
 `; ? : \@ & = + , $ / * < > " /`  
  
 **Description**  
 Enter a description that explains how to use the role and enumerates what the role supports.  
  
 **Task**  
 Select the tasks that can be performed through this role. You can't create new tasks or modify the existing tasks that [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] supports. Only item-level tasks can be used in an item-level role definition.  
  
 **Task Description**  
 Shows a description of the task that enumerates the operations or permissions that the task supports.  
  
## Related content  
 [Report server in Management Studio F1 help](../../reporting-services/tools/report-server-in-management-studio-f1-help.md)   
 [Role definitions](../../reporting-services/security/role-definitions.md)  
  
  
