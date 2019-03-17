---
title: "Site Settings Page (Report Manager) | Microsoft Docs"
ms.custom: ""
ms.date: "05/24/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: 4d67a01c-eae4-49ba-a6e8-8e983c0248f5
author: markingmyname
ms.author: maghan
manager: kfile
---
# Site Settings Page (Report Manager)
  Use the Site Settings page to change the application title, set server-wide defaults for report history limits and report processing timeout values, manage system-level role assignments, and manage shared schedules. You must have Content Manager and System Administrator permissions to view this page.  
  
> [!NOTE]  
>  The following features are not available in every edition of SQL Server: report history, report execution, and shared schedules. For a list of features that are supported by the editions of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], see [Features Supported by the Editions of SQL Server 2014](../../2014/getting-started/features-supported-by-the-editions-of-sql-server-2014.md).  
  
## Navigation  
 Use the following procedure to navigate to this location in the user interface (UI).  
  
### To open the Site Settings page  
  
1.  Open Report Manager.  
  
2.  At the top of the page, click **Site Settings**. This opens the General Properties page of the site.  
  
     **Note:** If you do not see the **Site Settings** option in the menu, you do not have the required permissions, For more information see the "site settings" section of [Configure a Native Mode Report Server for Local Administration &#40;SSRS&#41;](report-server/configure-a-native-mode-report-server-for-local-administration-ssrs.md).  
  
## Options  
 **Name**  
 Specify the title to use for this instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Report Manager. By default, the title is "[!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)][!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)]".  
  
 **Select the default settings for report history**  
 Select a default value for the number of copies of report history to retain. The default value provides an initial setting that establishes report history limits. You can vary these settings at the report level. For more information, see [Snapshot Options Properties Page &#40;Report Manager&#41;](../../2014/reporting-services/snapshot-options-properties-page-report-manager.md).  
  
 If you limit report history later, when the existing report history exceeds the limit you specify, the report server reduces the existing report history to the new limit. The oldest report snapshots are deleted first. If report history is empty or below the limit, new report snapshots are added. When the limit is reached, the oldest snapshot is deleted when a new report snapshot is added.  
  
 **Report Execution Timeout**  
 Specify whether report processing times out after a certain number of seconds.  
  
 This value applies to report processing on a report server. It does not affect data processing on the database server that provides the data for your report.  
  
 The report processing timer clock begins when the report is selected and ends when the report opens. When you set this value, specify enough time to complete both data processing and report processing.  
  
 **Custom Report Builder launch URL**  
 Specify a custom URL when the report server does not use the default Report Builder URL. This setting is optional. If you do not specify a value, the default URL will be used, which launches Report Builder as a ClickOnce application. The default URL is one of the following:  
  
 **Native mode report server:** In a native mode installation, the default URL will take the form of http://\<*computername*>/reportserver/ReportBuilder/ReportBuilder_3_0_0_0.application.  
  
 SharePoint integrated mode: The default URL will take the form of http://\<*SharePoint_site*>/_vti_bin/ReportBuilder/ReportBuilder_3_0_0_0.application."  
  
 **Apply**  
 Click to save your changes to the report server.  
  
 **Security**  
 Click this link to open the System Role Assignments page, on which you can assign user and group accounts to predefined system roles.  
  
 **Schedules**  
 Click this link to open the Schedules page, on which you can predefine shared schedules that users can select for their reports and subscriptions.  
  
## See Also  
 [Report Manager  &#40;SSRS Native Mode&#41;](../../2014/reporting-services/report-manager-ssrs-native-mode.md)   
 [Granting Permissions on a Native Mode Report Server](security/granting-permissions-on-a-native-mode-report-server.md)   
 [Predefined Roles](security/role-definitions-predefined-roles.md)   
 [Report Manager F1 Help](../../2014/reporting-services/report-manager-f1-help.md)  
  
  
