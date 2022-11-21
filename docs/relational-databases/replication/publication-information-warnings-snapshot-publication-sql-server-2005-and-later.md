---
description: "Publication Information, Warnings (Snapshot Publication, SQL Server 2005 and Later)"
title: "Warnings (Snapshot - Replication Monitor)"
decription: Describes the 'Warnings' tab for a Snapshot Publication in the Replication Monitor found in SQL Server Management Studio (SSMS).
ms.custom: seo-lt-2019
ms.date: "03/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: conceptual
f1_keywords: 
  - "sql13.rep.monitor.publicationinfo.warningsandagents.snapshot.f1"
ms.assetid: 7aa2eb52-b6b7-4dd3-8483-8ef00d9f0435
author: "MashaMSFT"
ms.author: "mathoma"
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016"
---
# Publication Information, Warnings (Snapshot Publication, SQL Server 2005 and Later)
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]
  The **Warnings** tab is available for Distributors that are running [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] and later versions. The **Warnings** tab allows you to perform the following tasks for the selected publication:  
  
-   Enable warnings.  
  
-   Specify thresholds associated with warnings.  
  
-   Define alerts associated with warnings.  
  
## Warnings, Thresholds, and Alerts  
 By default, Replication Monitor displays warnings for uninitialized subscriptions: a status of **Uninitialized subscription** is displayed as a warning in the **Status** column of pages that include subscription information. For snapshot publications, you can also specify that imminent subscription expiration results in a warning by setting the option **Warn if a subscription will expire within the threshold**. If the specified threshold is met or exceeded, the subscription status is displayed as **Expiring soon/Expired** (unless an issue with a higher priority needs to be displayed).  
  
 In addition to displaying a warning in Replication Monitor, reaching a threshold can also trigger an alert. Alerts are defined by clicking **Configure Alerts** and providing information in the **Configure Replication Alerts** dialog box.  
  
## Options  
 **Enabled**  
 Select to enable a warning and specify a threshold.  
  
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
 [Monitoring Replication](../../relational-databases/replication/monitor/monitoring-replication.md)  
  
  
