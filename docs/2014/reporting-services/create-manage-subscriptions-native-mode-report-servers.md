---
title: "Create and Manage Subscriptions for Native Mode Report Servers | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "subscriptions [Reporting Services], managing"
ms.assetid: 7f46cbdb-5102-4941-bca2-5e0ff9012c6b
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Create and Manage Subscriptions for Native Mode Report Servers
  This section contains topics about subscription processing, oversight, and control. Subscription management varies for standard subscriptions and data-driven subscriptions. Standard subscriptions are typically user-owned and managed. In contrast, data-driven subscriptions are typically created and maintained by a report server administrator.  
  
 Subscription and delivery features are enabled by default (e-mail delivery requires configuration before it can be used). The default delivery extensions include report server e-mail and file share delivery. Unless you create or install custom delivery extensions, these are the only distribution methods available to subscriptions on a native mode report server.  
  
## Permissions for Subscribing to Reports on a Native Mode Report Server  
 Depending on how you use roles, you can provide subscription functionality to selected groups of users by enabling or disabling subscription tasks for different roles. Subscription features are available to users through two tasks:  
  
-   The "Manage individual subscriptions" task allows users to create, modify, and delete subscriptions for a specific report. In the predefined roles, this task is part of Browser and Report Builder roles. Role assignments that include this task allow a user to manage only those subscriptions that he or she creates.  
  
-   The "Manage all subscriptions" task allows users to access and modify all subscriptions. This task is required to create data-driven subscriptions. In predefined roles, only the Content Manager role includes this task.  
  
## Disabling Subscriptions  
 To prevent users from creating subscriptions, clear the "Manage individual subscriptions" task from the role. When you remove this task, the Subscription pages are not available. In Report Manager, the My Subscriptions page appears to be empty (it cannot be deleted), even if it previously contained subscriptions. Removing subscription-related tasks prevents users from creating and modifying subscriptions, but does not delete existing subscriptions. Existing subscriptions will continue to execute until you delete them. For more information about deleting subscriptions, see [Create, Modify, and Delete Standard Subscriptions &#40;Reporting Services in Native Mode&#41;](subscriptions/create-and-manage-subscriptions-for-native-mode-report-servers.md).  
  
 To disable subscription processing on a report server, you can set the `ScheduleEventsAndReportDeliveryEnabled` property to `False` in the **Surface Area Configuration for Reporting Services** facet of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Policy-Based Management. Doing so will prevent all scheduled operations from running. You cannot turn off just subscription processing on the report server.  
  
 For instructions on how to cancel subscription that is processing on the report server, see [Manage a Running Process](subscriptions/manage-a-running-process.md).  
  
## Disabling Delivery Extensions  
 All delivery extensions installed on a report server are available to any user who has permission to create a subscription to a given report. The following delivery extensions are available and configured automatically:  
  
-   Windows File Share  
  
-   SharePoint Library (available only from a SharePoint site that is integrated with a  SharePoint integrated mode report server)  
  
 E-mail delivery must be configured before it can be used. If you do not configure it, it is not available. For more information, see [Configure a Report Server for E-Mail Delivery &#40;SSRS Configuration Manager&#41;](../../2014/sql-server/install/configure-a-report-server-for-e-mail-delivery-ssrs-configuration-manager.md).  
  
 If you want to turn off specific extensions, you can remove extension entries in the RSReportServer.config file. For more information, see [RSReportServer Configuration File](report-server/rsreportserver-config-configuration-file.md) and [Configure a Report Server for E-Mail Delivery &#40;SSRS Configuration Manager&#41;](../../2014/sql-server/install/configure-a-report-server-for-e-mail-delivery-ssrs-configuration-manager.md).  
  
 After you remove a delivery extension, it is no longer available in Report Manager or a SharePoint site. Removing a delivery extension can result in inactive subscriptions. Be sure to delete the subscriptions or configure them to use a different delivery extension before removing an extension.  
  
## In this section  
 [Use My Subscriptions](subscriptions/use-my-subscriptions-native-mode-report-server.md)  
 Explains how to use the My Subscriptions page to manage the subscriptions you own.  
  
 [Pause Report and Subscription Processing](subscriptions/disable-or-pause-report-and-subscription-processing.md)  
 Describes the various ways to pause report processing, such as using role assignments or disabling report server resources.  
  
 [Control Report Distribution](../../2014/reporting-services/control-report-distribution.md)  
 Describes configuration settings and delivery options you can use to control the distribution of reports.  
  
 [Monitor Reporting Services Subscriptions](subscriptions/monitor-reporting-services-subscriptions.md)  
 Describes how you can determine whether a subscription succeeded or failed, as well as the effects of report changes on existing subscriptions.  
  
## See Also  
 [Create, Modify, and Delete Standard Subscriptions &#40;Reporting Services in Native Mode&#41;](subscriptions/create-and-manage-subscriptions-for-native-mode-report-servers.md)  
  
  
