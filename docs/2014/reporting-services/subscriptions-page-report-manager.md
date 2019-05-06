---
title: "Subscriptions Page (Report Manager) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: cf3a6bd0-e0b2-4875-a532-63ef34cfa860
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Subscriptions Page (Report Manager)
  Use the Subscriptions page to list all of the subscriptions for the current report or shared data source. If you have sufficient permission (as conveyed by the "Manage all subscriptions" task), you can view the subscriptions of all users. Otherwise, this page shows only the subscriptions that you own.  
  
> [!NOTE]  
>  Other pages also contain subscription information. For more information, see [My Subscriptions Page &#40;Report Manager&#41;](../../2014/reporting-services/my-subscriptions-page-report-manager.md) to access all your subscriptions in one place or the [New Subscription or Edit Subscription Page &#40;Report Manager&#41;](../../2014/reporting-services/new-subscription-or-edit-subscription-page-report-manager.md) to create or edit a subscription.  
  
 Some options are visible only if there are existing subscriptions to work with. If no subscriptions are defined and you are accessing this page from a report, the **New Subscription** and **New Data-Driven Subscription** are the only options on the page.  
  
 Before you can create a new subscription, you must verify that the report data source uses stored credentials. Use the Data Sources properties page to store credentials. For more information, see [Data Sources Properties Page &#40;Report Manager&#41;](../../2014/reporting-services/data-sources-properties-page-report-manager.md).  
  
> [!NOTE]  
>  This feature is not available in every edition of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. For a list of features that are supported by the editions of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], see [Features Supported by the Editions of SQL Server 2014](../../2014/getting-started/features-supported-by-the-editions-of-sql-server-2014.md).  
  
## Navigation  
 Use the following procedure to navigate to this location in the user interface (UI).  
  
### To open the Subscriptions page for report  
  
1.  Open Report Manager, and locate the report for which you want to view or configure a subscription.  
  
2.  Hover over the report, and click the drop-down arrow.  
  
3.  In the drop-down menu, click **Manage**. This opens the General properties page for the report.  
  
4.  Select the **Subscriptions** tab.  
  
## Options  
 **Delete**  
 Click to delete a subscription. Before you can delete a subscription, select the check box next to each subscription that you want to delete.  
  
 **New Subscription**  
 Click to create a new subscription to the current report. This button is enabled when the report uses stored credentials or no credentials. This button is not available when you open the Subscriptions page for a shared data source.  
  
 **New Data-Driven Subscription**  
 Click to generate a subscriber list and delivery options from a command or query against a data store that contains this information. This button is enabled when the report uses stored credentials or no credentials. This button is not available when you open the Subscriptions page for a shared data source.  
  
 **Edit**  
 Click to view or edit the description.  
  
 **Report**  
 When you open this page from a shared data source, this column identifies the report for which the subscription is defined. The **Folder** column identifies the location of the report.  
  
 **Description**  
 Shows a description of the subscription.  
  
 **Trigger**  
 Identifies criteria that cause the subscription to run. A **TimedSubscription** trigger is based on a schedule that defines when the subscription runs. A **SnapshotUpdated** trigger is based on an update to a report snapshot.  
  
 **Owner**  
 Shows the name of the user who created the subscription.  
  
 **Last Run**  
 Shows the last time that the subscription was processed.  
  
 **Status**  
 Shows the status of the subscription. Usually, the status value is either New or the date and time at which the subscription last ran.  
  
 A status value of "Bad Data" occurs when the subscription includes a pointer to encrypted values that are no longer valid (that is, to the stored credentials used to run the report). Existing encrypted values become unusable when the symmetric keys used to encrypt and decrypt data are recreated on the report server.  
  
 A subscription cannot be processed if it has been deactivated. To update the subscription and make it operational, open and then save the subscription.  
  
## See Also  
 [Report Manager  &#40;SSRS Native Mode&#41;](../../2014/reporting-services/report-manager-ssrs-native-mode.md)   
 [Create, Modify, and Delete Standard Subscriptions &#40;Reporting Services in Native Mode&#41;](subscriptions/create-and-manage-subscriptions-for-native-mode-report-servers.md)   
 [Create, Modify, and Delete Schedules](subscriptions/create-modify-and-delete-schedules.md)   
 [Report Manager F1 Help](../../2014/reporting-services/report-manager-f1-help.md)  
  
  
