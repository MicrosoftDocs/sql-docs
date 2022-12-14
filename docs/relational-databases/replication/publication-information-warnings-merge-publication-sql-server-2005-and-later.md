---
title: "Warnings (Merge Publication Information)"
description: Describes the 'Warnings' tab of the Merge Replication Publication Information page within SQL Server Management Studio on SQL Server 2005 and later. 
ms.custom: seo-lt-2019
ms.date: "03/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: conceptual
f1_keywords: 
  - "sql13.rep.monitor.publicationinfo.warningsandagents.merge.f1"
ms.assetid: 9bef3565-5f13-42ac-8723-ebe55b0c11e6
author: "MashaMSFT"
ms.author: "mathoma"
---
# Publication Information, Warnings (Merge Publication, SQL Server 2005 and Later)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  The **Warnings** tab is available for Distributors that are running [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] and later versions. The **Warnings** tab allows you to perform the following tasks for the selected publication:  
  
-   Enable warnings.  
  
-   Specify thresholds associated with warnings.  
  
-   Define alerts associated with warnings.  
  
## Warnings, Thresholds, and Alerts  
 By default, Replication Monitor displays warnings for uninitialized subscriptions: a status of **Uninitialized subscription** is displayed as a warning in the **Status** column of pages that include subscription information. For merge publications, you can specify whether these additional conditions result in warnings:  
  
-   Imminent subscription expiration.  
  
     This corresponds to the option **Warn if a subscription will expire within the threshold**. If the specified threshold is met or exceeded, the subscription status is displayed as **Expiring soon/Expired** (unless an issue with a higher priority needs to be displayed).  
  
-   Exceeding the specified synchronization time.  
  
     This corresponds to the options **Warn if a merge length for dialup connections exceeds the threshold** and **Warn if a merge length for LAN connections exceeds the threshold**. Both of these thresholds can be set, but only one is used during a given synchronization. The Merge Agent applies the appropriate threshold based on the connection type.  
  
     If the specified threshold is met or exceeded, the subscription status is displayed as **Long-running merge** (unless an issue with a higher priority needs to be displayed).  
  
-   Falling short of processing the specified number of rows in a given amount of time.  
  
     This corresponds to the options **Warn if rows merged per second for dialup connections is less than the threshold** and **Warn if a merge length for LAN connections exceeds the threshold**. Both of these thresholds can be set, but only one is used during a given synchronization. The Merge Agent applies the appropriate threshold based on the connection type.  
  
     If the specified threshold is met or exceeded, the subscription status is displayed as **Performance critical** (unless an issue with a higher priority needs to be displayed).  
  
 When you enable a warning, you also set a threshold. For example, if you enable the warning **Warn if a merge length for LAN connections exceeds the threshold**, set the maximum allowable length of time for a merge synchronization.  
  
 In addition to displaying a warning in Replication Monitor, reaching a threshold can also trigger an alert. Alerts are defined by clicking **Configure Alerts** and providing information in the **Configure Replication Alerts** dialog box.  
  
## Options  
 **Enabled**  
 Select to enable a warning and specify a threshold.  
  
 **Alert**  
 Select to enable the alert setting for a given replication warning.  
  
 **Warning**  
 A description of the warning associated with a threshold.  
  
 **Threshold**  
 Specify a value for the threshold.  
  
 **Configure Alerts**  
 Select a row in the **Warnings** grid, and click **Configure Alerts** to launch the **Configure Replication Alerts** dialog box. The dialog box allows you to define an alert, which is associated with the selected threshold and warning.  
  
 **Discard Changes**  
 Click to discard any changes to warnings and thresholds.  
  
> [!NOTE]  
>  Clicking **Discard Changes** does not affect alerts defined in the **Configure Replication Alerts** dialog box.  
  
 **Save Changes**  
 Click to save any changes to warnings and thresholds.  
  
## See Also  
 [Start the Replication Monitor](../../relational-databases/replication/monitor/start-the-replication-monitor.md)   
 [View Information and Perform Tasks using Replication Monitor](../../relational-databases/replication/monitor/view-information-and-perform-tasks-replication-monitor.md)   
 [Monitor Performance with Replication Monitor](../../relational-databases/replication/monitor/monitor-performance-with-replication-monitor.md)   
 [Monitoring Replication](../../relational-databases/replication/monitor/monitoring-replication.md)   
 [Set Thresholds and Warnings in Replication Monitor](../../relational-databases/replication/monitor/set-thresholds-and-warnings-in-replication-monitor.md)  
  
  
