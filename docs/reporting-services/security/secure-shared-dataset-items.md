---
title: "Secure Shared Dataset Items | Microsoft Docs"
ms.date: 03/01/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: security


ms.topic: conceptual
ms.assetid: 08e6d8b5-d88c-4ed2-9c05-55c757e00014
author: markingmyname
ms.author: maghan
---
# Secure Shared Dataset Items
  On a report server, shared dataset items can be used by multiple reports. You can secure shared datasets to control the degree of access that users have. By default, only users who are members of the **Administrators** built-in group can view shared datasets, modify properties, enable caching, create cache refresh plans, and delete the items. All other users must have role assignments created for them that allow access to a shared dataset.  
  
 To set security, create a role assignment that specifies which user or group account has access to the shared dataset.  
  
## Role-based Access to Shared Datasets  
 To grant access to shared datasets, you can allow users to inherit existing role assignments from a parent folder or create a new role assignment on the item itself.  
  
 Default role assignments that enable you to add, delete, edit item properties, and view related reports and shared data sources for shared datasets are Content Manager, My Reports, and Publisher. To edit a shared dataset definition, use the default role assignments Report Builder or Content Manager.  
  
 Because role assignments are typically inherited from a parent node, a folder that has the **View reports** task enabled passes that permission to shared datasets and reports in the folder.  
  
 To provide more control over shared datasets and their query results, configure item level security on the shared dataset item or save shared datasets to a folder and configure item level security on the folder.  
  
## Shared Dataset Parameters  
 Shared dataset parameters cannot be used to restrict data for specific users. The purpose of shared dataset parameters is to provide a way to specify which data to include when the shared dataset is processed. On the report server, you cannot secure the values for a shared dataset parameter.  
  
 In the shared dataset definition, you can mark parameters as read-only and specify default values. Parameters that are marked read-only cannot be overridden on the server. For example, in a cache refresh plan for a shared dataset, you cannot specify values for read-only parameters. If the shared dataset parameters include expressions that refer to the User global collection, or have any other user dependencies, you cannot create a cache refresh plan for the shared dataset.  
  
## Tasks, Access to Items, and Default Roles  
 Shared datasets follow the same security model as reports. To provide a user with permission for specific actions, choose a role that includes the correspond task that includes those permissons. The following table lists tasks and the actions they include.  
  
|Select this task|To give users permission to|Default roles that include the task|  
|----------------------|---------------------------------|-----------------------------------------|  
|View reports|View the shared dataset item in the folder hierarchy. Without this task, the item is not visible to users and they might not realize that the dataset is available.|Browser<br /><br /> Content Manager<br /><br /> Report Builder<br /><br /> My Reports|  
|Manage reports|View properties that specify the name, description, and connection information. This task is also used to display a shared dataset item in the folder hierarchy. If you choose this task, you can omit the "View reports" task.|Content Manager<br /><br /> Publisher<br /><br /> My Reports|  
|Consume reports|View the shared dataset definition.|Content Manager<br /><br /> Report Builder|  
|Set security on items|Create and modify role assignments that control access to the shared dataset. This task must be used with either "View reports" or "Manage reports" tasks. If it is not, it has no effect because the user cannot select the item.|Content Manager|  
  
 For more information, see [Item-Level Tasks](../../reporting-services/security/tasks-and-permissions-item-level-tasks.md) and [Predefined Roles](../../reporting-services/security/role-definitions-predefined-roles.md).  
  
## See Also  
 [Manage Shared Datasets](../../reporting-services/report-data/manage-shared-datasets.md)   
 [Secure Folders](../../reporting-services/security/secure-folders.md)   
 [Secure Reports and Resources](../../reporting-services/security/secure-reports-and-resources.md)   
 [Granting Permissions on a Native Mode Report Server](../../reporting-services/security/granting-permissions-on-a-native-mode-report-server.md)   
 [Granting Permissions on a Native Mode Report Server](../../reporting-services/security/granting-permissions-on-a-native-mode-report-server.md)  
  
  
