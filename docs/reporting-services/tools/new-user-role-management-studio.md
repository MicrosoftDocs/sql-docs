---
title: "New User Role (Management Studio) | Microsoft Docs"
ms.date: 03/01/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: tools


ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.reportserver.newrole.f1"
ms.assetid: 9f76a235-0b58-479c-8e5b-50588091b71c
author: markingmyname
ms.author: maghan
---
# New User Role (Management Studio)
  Use this page to create an item-level role definition. An item-level role definition is a named collection of tasks that enumerate the tasks a user can perform in relation to folders, reports, models, resources, and shared data sources. An example of an item-level role definition is the predefined Browser role that identifies the kinds of actions a report end user might require for navigating folders and viewing reports.  
  
 Role definitions are intended to be few in number. Most organizations only require a few role definitions. However, if the predefined role definitions are insufficient, you can vary them or create new ones.  
  
> [!NOTE]  
>  Role definitions are used only on a report server that runs in native mode. If the report server is configured for SharePoint integration, this page is not available.  
  
## Options  
 **Name**  
 Type the name of the role definition. A role definition name must be unique within the report server namespace. A name must contain at least one alphanumeric character. It can also include spaces and some symbols. Do not use the following characters when specifying a name:  
  
 ; ? : \@ & = + , $ / * < >  
  
 " /  
  
 **Description**  
 Type a description that explains how to use the role and enumerates what the role supports.  
  
 **Task**  
 Select the tasks that can be performed through this role. You cannot create new tasks or modify the existing tasks that are supported by [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]. Only item-level tasks can be used in an item-level role definition.  
  
 **Task Description**  
 Shows a description of the task that enumerates the operations or permissions that the task supports.  
  
## See Also  
 [Report Server in Management Studio F1 Help](../../reporting-services/tools/report-server-in-management-studio-f1-help.md)   
 [Role Definitions](../../reporting-services/security/role-definitions.md)  
  
  
