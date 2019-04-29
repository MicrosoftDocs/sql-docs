---
title: "Set Permissions for Report Server Items on a SharePoint Site (Reporting Services in SharePoint Integrated Mode) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "permissions [Reporting Services], SharePoint integrated mode"
  - "SharePoint integration [Reporting Services], permissions"
ms.assetid: 2467c657-a3bf-4ec3-a88c-8877df19823d
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Set Permissions for Report Server Items on a SharePoint Site (Reporting Services in SharePoint Integrated Mode)
  If default security settings do not provide the level of access that you require, you can create new permission levels to provide access to specific report server items or operations. Custom security settings can be useful if you want to restrict access to a particular report.  
  
 You must be a site owner to create permission levels and groups. Permission levels are used globally throughout a site. If you create a new permission level, it will be available to other site owners.  
  
 Most permissions are inherited from a parent site. If you assign permissions on a specific library or item, you break permission inheritance and incur additional overhead in how manage permissions for that branch of your site hierarchy.  
  
 You can set permissions on report definition (.rdl), model (.smdl), and shared data source (.rsds) files. You cannot combine inherited and managed permissions on the same item. If you choose to manage permissions directly, inherited permissions will have no effect on the current item. If you want to resume permission inheritance later, you can select **Inherit Permissions** on the **Actions** menu.  
  
 To set permissions on entities and perspectives in a model, you must have Full Control level of permission for the model. Full Control includes "Manage Permissions", which is a site level permission that is granted to site owners and other SharePoint groups who have Full Control level of permission. If you want to offer specific users the ability to set model item security, you must break permission inheritance and grant elevated permissions (such as Full Control) to the user or group on the model file. When you grant Full Control on an item such as a file in the library, the permissions are scoped to that item and do not extend to the parent or to other items within the same library. After the user has Manage Permissions permission on the model, he or she can use set model item security via the SharePoint site or Model Designer.  
  
### To set permissions on an individual report, model, or data source  
  
1.  If the library is not already open, click its name on the Quick Launch. If the name of your library does not appear, click **View All Site Content**, and then click the name of your library.  
  
2.  Point to the report, report model, or shared data source file.  
  
3.  Click the down arrow, and on the menu, click **Manage Permissions**.  
  
4.  On the **Actions** menu, click **Edit Permissions**, and then click **OK** to confirm the action.  
  
5.  To give permissions to a user or group that does not yet have permissions to use the file, click **New**, and then click **Add Users**.  
  
6.  To remove or modify permissions for an existing user or group, click the check box next to the user or group, click **Actions**, and then click **Remove User Permissions** or **Edit User Permissions**.  
  
### To set permissions that enable model item security  
  
1.  Log in to the SharePoint site using an account that has Manage Permissions permission on the site.  
  
2.  Open the library that contains the model.  
  
3.  Point to the model.  
  
4.  Click the down arrow next to the model, and click **Manage Permissions**.  
  
5.  Click **Actions**.  
  
6.  Click **Edit Permissions**. Click **OK**.  
  
7.  Click **New**.  
  
8.  Click **Add Users**.  
  
9. In Users/Groups, enter the user account.  
  
10. Select **Give users permission directly**.  
  
11. Click **Full Control**.  
  
12. Click **OK**. Once a user has the ability to manage permissions for a specific model, he or she can open the model to edit permissions within the model.  
  
## See Also  
 [Use Built-in Security in Windows SharePoint Services for Report Server Items](use-built-in-security-in-windows-sharepoint-services-for-report-server-items.md)   
 [Set Permissions for Report Server Operations in a SharePoint Web Application](set-permissions-for-report-server-operations-in-a-sharepoint-web-application.md)   
 [Compare Roles and Tasks in Reporting Services to SharePoint Groups and Permissions](../reporting-services-roles-tasks-vs-sharepoint-groups-permissions.md)   
 [SharePoint Site and List Permission Reference for Report Server Items](sharepoint-site-and-list-permission-reference-for-report-server-items.md)   
 [Granting Permissions on Report Server Items on a SharePoint Site](granting-permissions-on-report-server-items-on-a-sharepoint-site.md)  
  
  
