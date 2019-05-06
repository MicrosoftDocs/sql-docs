---
title: "Enable and Disable My Reports | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "deactivated My Reports folder"
  - "folders [Reporting Services], My Reports"
  - "activated My Reports folder"
  - "My Reports folder [Reporting Services]"
  - "disabling My Reports folder"
ms.assetid: 16c76e82-9fd4-417c-9ed3-a7d5bcd1dba2
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Enable and Disable My Reports
  The My Reports feature allocates personal storage in the report server database so that users can save reports that they own in a private folder. As a report server administrator, you can enable or disable this feature or change how the feature works by modifying the security settings that control what users can do with this workspace.  
  
 The My Reports feature is disabled by default. You can either enable or disable the feature for all users, but you cannot enable it for a subset of users. Most users and organizations find this feature valuable; study the advantages and disadvantages presented later in this topic to determine whether it is a good fit for your organization.  
  
## How to Enable and Disable My Reports  
 To enable My Reports by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], connect to the report server instance and open the **Server Properties** page. Then on the **General** tab, select the **Enable a My Reports folder for each user** option.  
  
 The role definition used for My Reports determines what actions are supported in the My Reports workspace. For example, if the My Reports role excludes "Create linked reports," users cannot create linked reports in the My Reports folders. For more information, see [Secure My Reports](../security/secure-my-reports.md).  
  
 To deactivate My Reports, clear **Enable a My Reports folder for each user**. Deactivating My Reports removes for users all visible indications of the My Reports folder. The folders that provide actual storage (that is, the subfolders in Users Folders) must be deleted manually once the feature is disabled.  
  
### When My Reports Is Activated  
 Once the feature is activated, users see a My Reports folder located under the root folder, Home. In addition to a My Reports folder, report server administrators also see a Users Folders folder that contains the subfolder for each user.  
  
 While the feature is activated, Users Folders and its subfolders cannot be deleted. Furthermore, the name "My Reports" becomes a reserved name for folders created under the root node (Home).  
  
 If you activate My Reports after it has been deactivated, the report server creates a new Users Folders folder if one does not already exist. If Users Folders exists, the report server adds new subfolders as users log on to their My Reports folders.  
  
### When My Reports Is Deactivated  
 Once the feature is deactivated, the name "My Reports" is no longer reserved; users can create a personal folder named My Reports under the Home folder. In addition, redirection from My Reports to user-specific My Reports subfolders is no longer performed. Lastly, any report links that include a user-specific My Reports folder in the URL address will no longer work.  
  
## Choosing to Use My Reports  
 Deciding whether to use My Reports depends on whether you want to dedicate server resources to support a user workspace. My Reports is a powerful feature that allows users to have control over information resources that help them do their jobs. It also provides a way for users to work with reports that are not intended for general use. One of the most compelling reasons to use My Reports is that it provides secure, manageable support for the segment of users who need to author and review reports. Without this feature, you may find yourself creating folders and security policies for various users on an ad hoc basis. As users and user needs change, this approach results in ever-increasing numbers of folders and policies that are difficult to maintain over time.  
  
 Note that if you do activate My Reports, the report server creates a My Reports folder for every user with a domain account who clicks the My Reports link, even if the user does not want or need a My Reports folder. There is no systematic way to determine which folders are being used. You must review the folders manually to see whether they contain anything.  
  
## See Also  
 [Secure My Reports](../security/secure-my-reports.md)   
 [Report Server Content Management &#40;SSRS Native Mode&#41;](report-server-content-management-ssrs-native-mode.md)  
  
  
