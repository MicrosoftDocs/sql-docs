---
title: "My Subscriptions Page (Report Manager) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: 491a85a3-f323-4155-a0a8-de2779899995
author: markingmyname
ms.author: maghan
manager: craigg
---
# My Subscriptions Page (Report Manager)
  Use the My Subscriptions page to view all of your subscriptions in one place. From this page, you can access and modify or delete any subscription that you own. You own only the subscriptions that you create. You cannot access those of other users, nor can you access subscriptions that you use but do not own (for example, if your name has been added to an existing subscription defined by another user). You cannot create subscriptions from this page. For more information about creating subscriptions, see the [New Subscription or Edit Subscription Page &#40;Report Manager&#41;](../../2014/reporting-services/new-subscription-or-edit-subscription-page-report-manager.md).  
  
 By default, subscriptions are sorted in alphabetical order by report name. Click a different column heading to change how subscriptions are sorted. If you have no subscriptions or if permission to create or manage subscriptions is disabled, no subscriptions appear on the page.  
  
> [!NOTE]  
>  This feature is not available in every edition of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. For a list of features that are supported by the editions of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], see [Features Supported by the Editions of SQL Server 2014](../../2014/getting-started/features-supported-by-the-editions-of-sql-server-2014.md).  
  
## Navigation  
 Use the following procedure to navigate to this location in the user interface (UI).  
  
### To open the My Subscriptions page  
  
1.  Open Report Manager.  
  
2.  At the top of the page, in the right-hand corner, click My Subscriptions.  
  
    > [!NOTE]  
    >  My Subscriptions is always available, even if you lack permission to create subscriptions.  
  
## Options  
 **Delete**  
 Select the check box next to each subscription that you want to delete and click **Delete**.  
  
 **Edit**  
 Click to view or edit the description.  
  
 **Report**  
 Shows the report that is specified in the subscription. Click the report name to view the report.  
  
 **Description**  
 Shows a description of the subscription. Click the description to view or edit the subscription information for the report.  
  
 **Folder**  
 Shows the folder that contains the report that is specified in the subscription. Click the folder name to view the contents of the folder.  
  
 **Trigger**  
 Identifies criteria that cause the subscription to run. A **TimedSubscription** trigger is based on a schedule that defines when the subscription runs. A **SnapshotUpdated** trigger is based on an update to a report snapshot.  
  
 **Last Run**  
 Shows the last time that the subscription was processed.  
  
 **Status**  
 Shows the status of the subscription. Usually, the status value is either "New" or the date and time at which the subscription last ran.  
  
 A status value of "Bad Data" occurs when the subscription includes a pointer to encrypted values that are no longer valid (that is, to the stored credentials used to run the report). Existing encrypted values become unusable when the symmetric keys used to encrypt and decrypt data are recreated on the report server.  
  
 A subscription cannot be processed if it has been deactivated. To update the subscription and make it operational, open and then save the subscription.  
  
## See Also  
 [Subscriptions and Delivery &#40;Reporting Services&#41;](subscriptions/subscriptions-and-delivery-reporting-services.md)   
 [Report Manager F1 Help](../../2014/reporting-services/report-manager-f1-help.md)  
  
  
