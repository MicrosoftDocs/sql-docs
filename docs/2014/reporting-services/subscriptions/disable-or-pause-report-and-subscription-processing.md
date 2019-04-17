---
title: "Pause Report and Subscription Processing | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "pausing schedules"
  - "subscriptions [Reporting Services], pausing"
  - "report processing [Reporting Services], pausing"
  - "shared data sources [Reporting Services]"
  - "pausing subscription processing"
  - "pausing report processing"
  - "temporarily stopping report processing"
  - "disabling shared data sources"
  - "roles [Reporting Services], modifying"
  - "shared schedules [Reporting Services], pausing"
ms.assetid: 3cf9a240-24cc-46d4-bec6-976f82d8f830
author: markingmyname
ms.author: maghan
manager: kfile
---
# Pause Report and Subscription Processing
  You cannot pause a report or subscription directly. However, you can interrupt report and subscription processing prior to the process starting or when a data source connection is made. You can also prevent a report or subscription from processing by making it inaccessible to users.  
  
 The following strategies can be used to temporarily prevent a process from occurring.  
  
## Modify Role Assignments to Prevent Access  
 The best way to make a report unavailable is to temporarily remove the role assignment that provides access to the report. This approach can be used on all reports regardless of how the data source connection is made. This approach targets only the report, without affecting the operation of other reports or items.  
  
 To remove the role assignment, open the Security Properties page of the report in Report Manager. If the report inherits security from a parent, you can click **Edit Item Security** to create a restrictive security policy that omits role assignments that provide widespread access (for example, you can remove a role assignment that provides access to Everyone, and keep the role assignment that provides access to a small group of users, such as Administrators).  
  
## Disable a Shared Data Source  
 One advantage to using shared data sources is that you can disable it to prevent a report or data-driven subscription from running. Disabling a shared data source disconnects the report from its external source. While it is disabled, the data source is unavailable to all reports and subscriptions that use it. To disable a shared data source, open the data source in Report Manager and clear the **Enable this data source** check box.  
  
 Note that the report still loads even if the data source is unavailable. The report does not contain data, but users with appropriate permissions can access the property pages, security settings, report history, and subscription information associated with the report.  
  
## Pause a Shared Schedule  
 If a report or subscription runs from a shared schedule, you can pause the schedule to prevent processing. All report and subscription processing that is driven by the schedule is deferred until the schedule is resumed. For more information, see [Pause and Resume Shared Schedules](schedules.md).  
  
## See Also  
 [Reporting Services Report Server &#40;Native Mode&#41;](../report-server/reporting-services-report-server-native-mode.md)   
 [Report Manager  &#40;SSRS Native Mode&#41;](../report-manager-ssrs-native-mode.md)   
 [Security Properties Page, Items &#40;Report Manager&#41;](../security-properties-page-items-report-manager.md)  
  
  
