---
title: "Set Thresholds and Warnings in Replication Monitor | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "alerts [SQL Server replication]"
  - "Merge Agent, thresholds and warnings"
  - "Distribution Agent, thresholds and warnings"
  - "thresholds [SQL Server replication]"
  - "Replication Monitor, thresholds and warnings"
  - "monitoring performance [SQL Server replication], thresholds and warnings"
ms.assetid: 3a409c2c-b77e-4001-b81a-1dcd918618ec
author: "MashaMSFT"
ms.author: "mathoma"
manager: craigg
---
# Set Thresholds and Warnings in Replication Monitor
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Replication Monitor displays status information for publications and subscriptions. By default, Replication Monitor displays warnings only for uninitialized subscriptions, but you can enable warnings for other conditions. It is recommended that you enable warnings for your topology, so that you are informed about status and performance in a timely manner.  
  
 When you enable a warning, you specify a threshold. When that threshold is met or exceeded, a warning is displayed (unless an issue with a higher priority needs to be displayed). In addition to displaying a warning in Replication Monitor, reaching a threshold can also trigger an alert. You can enable warnings for the following conditions:  
  
-   Imminent subscription expiration  
  
     This applies to all types of replication. If the specified threshold is met or exceeded, the subscription status is displayed as **Expiring soon/Expired**.  
  
-   Exceeding the specified latency (the amount of time that elapses between a transaction being committed at the Publisher and the corresponding transaction being committed at the Subscriber).  
  
     This applies to transactional replication. If the specified threshold is met or exceeded, the subscription status is displayed as **Performance critical**.  
  
-   Exceeding the specified synchronization time.  
  
     This applies to merge replication. If the specified threshold is met or exceeded, the status is displayed as **Long-running merge**. You can specify different thresholds for dialup and Local Area Network (LAN) connections.  
  
-   Falling short of processing the specified number of rows in a given amount of time.  
  
     This applies to merge replication. If the specified threshold is met or exceeded, the status is displayed as **Performance critical**. You can specify different thresholds for dialup and LAN connections.  
  
 For more information on the warnings **Performance critical** and **Long-running merge**, see [Monitor Performance with Replication Monitor](../../../relational-databases/replication/monitor/monitor-performance-with-replication-monitor.md).  
  
 **In This Topic**  
  
-   [Set thresholds and warnings for a transactional publication](#Transactional)  
  
-   [Set thresholds and warnings for a merge publication](#Merge)  
  
-   [Set thresholds and warnings for a snapshot publication](#Snapshot)  
  
##  <a name="Transactional"></a> To set thresholds and warnings for a transactional publication  
  
1.  Expand a Publisher group in the left pane, expand a Publisher, and then click a publication.  
  
2.  Click the **Warnings** tab. To view more information about the options on this tab click **Help** on the menu bar.  
  
3.  Enable a warning by selecting the appropriate check box: **Warn if a subscription will expire within the threshold** or **Warn if latency exceeds the threshold**.  
  
4.  Set a threshold for the warnings in the **Threshold** column. For example, if you selected **Warn if latency exceeds the threshold** in step 3, you could select a latency of **60 seconds** in the **Threshold** column.  
  
5.  Click **Save Changes**.  
  
#### To configure an alert for a threshold  
  
1.  Click **Configure Alerts**.  
  
2.  In the **Configure Replication Alerts** dialog box, select an alert, and then click **Configure**.  
  
     This dialog box displays alerts for all publication types, including alerts that are not related to monitoring thresholds. For more information, see [Use Alerts for Replication Agent Events](../../../relational-databases/replication/agents/use-alerts-for-replication-agent-events.md).  
  
3.  Set options in the **\<AlertName> alert properties** dialog box:  
  
    -   On the **General** page, click **Enable**; specify which database the alert should apply to.  
  
    -   On the **Response** page, specify whether an e-mail should be sent and/or a job should be executed.  
  
    -   On the **Options** page, customize the text of the response.  
  
4.  [!INCLUDE[clickOK](../../../includes/clickok-md.md)]  
  
5.  Click **Close**.  
  
##  <a name="Merge"></a> Set thresholds and warnings for a merge publication  
  
1.  Expand a Publisher group in the left pane, expand a Publisher, and then click a publication.  
  
2.  Click the **Warnings** tab. To view more information about the options on this tab, click **Help** on the menu bar.  
  
3.  Enable a warning by selecting the appropriate check box:  
  
    -   **Warn if a subscription will expire within the threshold**  
  
    -   **Warn if a merge length for dialup connections exceeds the threshold**  
  
    -   **Warn if a merge length for LAN connections exceeds the threshold**  
  
    -   **Warn if rows merged per second for LAN connections is less than the threshold**  
  
    -   **Warn if rows merged per second for dialup connections is less than the threshold**  
  
4.  Set thresholds for the warnings in the **Threshold** column. For example, if you selected **Warn if a merge length for dialup connections exceeds the threshold** in step 3, you could select a time of **10 minutes** in the **Threshold** column.  
  
5.  Click **Save Changes**.  
  
#### To configure an alert for a threshold  
  
1.  Click **Configure Alerts**.  
  
2.  In the **Configure Replication Alerts** dialog box, select an alert, and then click **Configure**.  
  
     This dialog box displays alerts for all publication types, including alerts that are not related to monitoring thresholds.  
  
3.  Set options in the **\<AlertName> alert properties** dialog box:  
  
    -   On the **General** page, click **Enable**; specify which database the alert should apply to.  
  
    -   On the **Response** page, specify whether an e-mail should be sent and/or a job should be executed.  
  
    -   On the **Options** page, customize the text of the response.  
  
4.  [!INCLUDE[clickOK](../../../includes/clickok-md.md)]  
  
5.  Click **Close**.  
  
##  <a name="Snapshot"></a> Set thresholds and warnings for a snapshot publication  
  
1.  Expand a Publisher group in the left pane, expand a Publisher, and then click a publication.  
  
2.  Click the **Warnings** tab. To view more information about the options on this tab click **Help** on the top menu.  
  
3.  Enable a warning by selecting the check box **Warn if a subscription will expire within the threshold**.  
  
4.  Set a threshold for the warning in the **Threshold** column. For example, you could select a value of **70%** in the **Threshold** column.  
  
5.  Click **Save Changes**.  
  
#### To configure an alert for a threshold  
  
1.  Click **Configure Alerts**.  
  
2.  In the **Configure Replication Alerts** dialog box, select an alert, and then click **Configure**.  
  
     This dialog box displays alerts for all publication types, including alerts that are not related to monitoring thresholds. For more information, see [Use Alerts for Replication Agent Events](../../../relational-databases/replication/agents/use-alerts-for-replication-agent-events.md).  
  
3.  Set options in the **\<AlertName> alert properties** dialog box:  
  
    -   On the **General** page, click **Enable**; specify which database the alert should apply to.  
  
    -   On the **Response** page, specify whether an e-mail should be sent and/or a job should be executed.  
  
    -   On the **Options** page, customize the text of the response.  
  
4.  [!INCLUDE[clickOK](../../../includes/clickok-md.md)]  
  
5.  Click **Close**.  
  
## See Also  
 [Monitoring Replication](../../../relational-databases/replication/monitor/monitoring-replication.md)  
  
  
