---
title: "Set permissions for report server items on a SharePoint site"
description: "Set permissions for report server items on a SharePoint site"
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: security
ms.topic: conceptual
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "permissions [Reporting Services], SharePoint integrated mode"
  - "SharePoint integration [Reporting Services], permissions"
---
# Set permissions for report server items on a SharePoint site
  If default security settings don't provide the level of access that you require, you can create new permission levels to provide access to specific report server items or operations. Custom security settings can be useful if you want to restrict access to a particular report.  
  
 You must be a site owner to create permission levels and groups. Permission levels are used globally throughout a site. If you create a new permission level, it's available to other site owners.  
  
 Most permissions are inherited from a parent site. If you assign permissions on a specific library or item, you break permission inheritance and incur more overhead in how to manage permissions for that branch of your site hierarchy.  
  
 You can set permissions on report definition (.rdl), model (.smdl), and shared data source (.rsds) files. You can't combine inherited and managed permissions on the same item. If you choose to manage permissions directly, inherited permissions have no effect on the current item. If you want to resume permission inheritance later, you can choose **Inherit Permissions** on the **Actions** menu.  
  
 To set permissions on entities and perspectives in a model, you must have Full Control level of permission for the model. Full Control includes "Manage Permissions," which is a site level permission. This permission is granted to site owners and other SharePoint groups who have Full Control level of permission. If you want to offer specific users the ability to set model item security, you must break permission inheritance and grant elevated permissions (such as Full Control) to the user or group on the model file. When you grant Full Control on an item such as a file in the library, the permissions are scoped to that item. Those permissions don't extend to the parent or to other items within the same library. After the user has Manage Permissions permission on the model, they can use set model item security via the SharePoint site or Model Designer.  
  
### Set permissions on an individual report, model, or data source  
  
1.  If the library isn't already open, select its name on the Quick Launch. If the name of your library doesn't appear, select **View All Site Content**, and then choose the name of your library.  
  
2.  Point to the report, report model, or shared data source file.  
  
3.  Select the down arrow, and on the menu, choose **Manage Permissions**.  
  
4.  On the **Actions** menu, select **Edit Permissions**, and then choose **OK** to confirm the action.  
  
5.  To give permissions to a user or group that doesn't yet have permissions to use the file, select **New**, and then choose **Add Users**.  
  
6.  To remove or modify permissions for an existing user or group, select the check box next to the user or group, select **Actions**, and then choose **Remove User Permissions** or **Edit User Permissions**.  
  
### Set permissions that enable model item security  
  
1.  Sign in to the SharePoint site by using an account that has Manage Permissions permission on the site.  
  
2.  Open the library that contains the model.  
  
3.  Point to the model.  
  
4.  Select the down arrow next to the model, and choose **Manage Permissions**.  
  
5.  Select **Actions**.  
  
6.  Select **Edit Permissions**. Choose **OK**.  
  
7.  Select **New**.  
  
8.  Select **Add Users**.  
  
9. In Users/Groups, enter the user account.  
  
10. Select **Give users permission directly**.  
  
11. Select **Full Control**.  
  
12. Select **OK**. Once a user has the ability to manage permissions for a specific model, they can open the model to edit permissions within the model.  
  
## Related content

- [Use built-in security in Windows SharePoint Services for report server items](../../reporting-services/security/use-built-in-security-in-windows-sharepoint-services-for-report-server-items.md)
- [Set permissions for report server operations in a SharePoint web application](../../reporting-services/security/set-permissions-for-report-server-operations-in-a-sharepoint-web-application.md)
- [Compare Roles and Tasks in Reporting Services to SharePoint Groups and Permissions](../../reporting-services/security/reporting-services-roles-tasks-vs-sharepoint-groups-permissions.md)
- [SharePoint site and list permission reference for report server items](../../reporting-services/security/sharepoint-site-and-list-permission-reference-for-report-server-items.md)
- [Grant permissions on report server items on a SharePoint site](../../reporting-services/security/granting-permissions-on-report-server-items-on-a-sharepoint-site.md)
