---
title: "New Subscription or Edit Subscription Page (Report Manager) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: e02d6529-ce67-4305-b7f0-433997e99c21
author: markingmyname
ms.author: maghan
manager: kfile
---
# New Subscription or Edit Subscription Page (Report Manager)
  Use the New Subscription or Edit Subscription page to create a new subscription or modify an existing subscription to a report. The options on this page vary depending on your role assignment. Users with advanced permissions can work with additional options.  
  
 Subscriptions are supported for reports that can run unattended. At a minimum, the report must use stored or no credentials. If the report uses parameters, a default value must be specified. Subscriptions may become inactive if you change report execution settings or remove the default values used by parameter properties. For more information, see [Create and Manage Subscriptions for Native Mode Report Servers](../../2014/reporting-services/create-manage-subscriptions-native-mode-report-servers.md).  
  
> [!NOTE]  
>  This feature is not available in every edition of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. For a list of features that are supported by the editions of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], see [Features Supported by the Editions of SQL Server 2014](../../2014/getting-started/features-supported-by-the-editions-of-sql-server-2014.md).  
  
## Navigation  
 Use the following procedure to navigate to this location in the user interface (UI).  
  
### To open the New Subscription or Edit Subscription page  
  
1.  Open Report Manager, and locate the report for which you want to add a subscription.  
  
2.  Hover over the report, and click the drop-down arrow.  
  
3.  In the drop-down menu, do one of the following:  
  
    -   Click **Manage**. This opens the General properties page for the report. Then select the **Subscriptions** tab. In the toolbar, click **New Subscription**, or select an existing subscription and click **Edit**.  
  
    -   Click **Subscribe**. This opens the **New Subscription** page for the report.  
  
## Options  
 **Delivered by**  
 Select the delivery extension to use to distribute the report. Depending on the delivery extension you select, the following settings appear:  
  
-   E-mail subscriptions provide fields that are familiar to e-mail users (for example, **To**, **Subject**, and **Priority** fields). Specify **Include Report** to embed or attach the report, or **Include Link** to include a URL to the report. Specify **Render Format** to choose a presentation format for the attached or embedded report.  
  
-   File share subscriptions provide fields that allow you to specify a target location. You can deliver any report to a file share. However, reports that support interactive features (including matrix reports that support drill-down to supporting rows and columns) are rendered as static files. You cannot view drill-down rows and columns in a static file. The file share name must be specified in Uniform Naming Convention (UNC) format (for example, \\\mycomputer\public\myreportfiles). Do not include a trailing backslash in the path name. The report file will be delivered in a file format that is based on the render format (for example, if you choose **Excel**, the report is delivered as an .xls file).  
  
 The availability of a delivery extension depends on whether it is installed and configured on the report server. Report Server E-mail is the default delivery extension, but it must be configured before you can use it. File Share delivery does not require configuration, but you must define a shared folder before you can use it.  
  
## Subscription Processing Options  
 Use these settings to define the conditions that cause a subscription to process. Some of the options are only available for reports that use parameters or that run as report execution snapshots.  
  
 **When the report content is refreshed**  
 Select this option to subscribe to a report snapshot that is refreshed on a scheduled basis. This option is visible only when you are subscribing to a report that runs as a report execution snapshot. The content for a report execution snapshot is typically refreshed on a schedule. For reports that run in this mode, you can define a subscription to occur when the snapshot is refreshed.  
  
 **When the scheduled report run is complete**  
 Create a schedule that determines when the subscription is processed.  
  
 **On a shared schedule**  
 Select a predefined schedule to process the subscription.  
  
 **Enter parameter values**  
 Use this option when you are subscribing to a report that has parameters. This option is available only for parameterized reports. When subscribing to a parameterized report, you can specify the parameter values that are used to create the version of the report that is delivered through the subscription. For example, you can specify a region code to select sales data for a particular region. If you do not specify a value, the default value is used.  
  
## See Also  
 [Configure a Report Server for E-Mail Delivery &#40;SSRS Configuration Manager&#41;](../../2014/sql-server/install/configure-a-report-server-for-e-mail-delivery-ssrs-configuration-manager.md)   
 [Report Manager  &#40;SSRS Native Mode&#41;](../../2014/reporting-services/report-manager-ssrs-native-mode.md)   
 [Create, Modify, and Delete Schedules](subscriptions/create-modify-and-delete-schedules.md)   
 [Report Manager F1 Help](../../2014/reporting-services/report-manager-f1-help.md)  
  
  
