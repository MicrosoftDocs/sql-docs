---
title: "Enable and disable My Reports"
description: Learn how to enable and disable the My Reports feature in Reporting Services. My Reports provides storage in the report server database for users.
author: maggiesMSFT
ms.author: maggies
ms.date: 03/01/2017
ms.service: reporting-services
ms.subservice: report-server
ms.topic: conceptual
ms.custom: updatefrequency5
helpviewer_keywords:
  - "deactivated My Reports folder"
  - "folders [Reporting Services], My Reports"
  - "activated My Reports folder"
  - "My Reports folder [Reporting Services]"
  - "disabling My Reports folder"
---
# Enable and disable My Reports
  The My Reports feature allocates personal storage in the report server database so that users can save reports that they own in a private folder. As a report server administrator, you can enable or disable this feature. Or, you can change how the feature works by modifying the security settings that control what users can do with this workspace.  
  
 The My Reports feature is disabled by default. You can either enable or disable the feature for all users, but you can't enable it for a subset of users. Most users and organizations find this feature valuable. Study the advantages and disadvantages presented later in this article to determine whether it's a good fit for your organization.  
  
## How to enable and disable My Reports  
 To enable My Reports by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], connect to the report server instance and open the **Server Properties** page. Then on the **General** tab, select the **Enable a My Reports folder for each user** option.  
  
 The role definition used for My Reports determines what actions are supported in the My Reports workspace. For example, if the My Reports role excludes **Create linked reports**, users can't create linked reports in the **My Reports** folders. For more information, see [Secure My Reports](../../reporting-services/security/secure-my-reports.md).  
  
 To deactivate My Reports, clear **Enable a My Reports folder for each user**. Deactivating My Reports removes for users all visible indications of the My Reports folder. The folders that provide actual storage, like the subfolders in **Users Folders**, must be deleted manually once the feature is disabled.  
  
### When My Reports is activated  
 Once the feature is activated, users see a My Reports folder located under the root folder, **Home**. In addition to a **My Reports** folder, report server administrators also see a **Users Folders** folder that contains the subfolder for each user.  
  
 While the feature is activated, **Users Folders** and its subfolders can't be deleted. Furthermore, the name "My Reports" becomes a reserved name for folders created under the root node (Home).  
  
 If you activate My Reports after the feature is deactivated, the report server creates a new **Users Folders** folder if one doesn't already exist. If **Users Folders** exists, the report server adds new subfolders as users sign in to their **My Reports** folders.  
  
### When My Reports is deactivated  
 Once the feature is deactivated, the name "My Reports" is no longer reserved.  Users can create a personal folder named **My Reports** under the **Home** folder. In addition, redirection from **My Reports** to user-specific **My Reports** subfolders is no longer performed. Lastly, any report links that include a user-specific **My Reports** folder in the URL address no longer works.  
  
## Choose to use My Reports  
 Deciding whether to use My Reports depends on whether you want to dedicate server resources to support a user workspace. My Reports is a powerful feature that allows users to have control over information resources that help them do their jobs. It also provides a way for users to work with reports that aren't intended for general use. One of the most compelling reasons to use My Reports is that it provides secure, manageable support for the segment of users who need to author and review reports. Without this feature, you might find yourself creating folders and security policies for various users on a case-by-case basis. As users and user needs change, this approach results in ever-increasing numbers of folders and policies that are difficult to maintain over time.  
  
 If you do activate My Reports, the report server creates a **My Reports** folder for every user with a domain account who selects the **My Reports** link, even if the user doesn't want or need a **My Reports** folder. There's no systematic way to determine which folders are being used. You must review the folders manually to see whether they contain anything.  
  
## Related content  
 [Secure My Reports](../../reporting-services/security/secure-my-reports.md)   
 [Report server content management &#40;SSRS native mode&#41;](../../reporting-services/report-server/report-server-content-management-ssrs-native-mode.md)  
  
  
