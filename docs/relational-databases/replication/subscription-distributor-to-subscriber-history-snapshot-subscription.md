---
title: "Subscription, Distributor to Subscriber History (Snapshot Subscription) | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
f1_keywords: 
  - "sql13.rep.monitor.subscription.pubtodist.snapshot.f1"
ms.assetid: d3575964-f287-4bcf-8d2e-f81a33141b25
author: "MashaMSFT"
ms.author: "mathoma"
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Subscription, Distributor to Subscriber History (Snapshot Subscription)
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]
  The **Distributor to Subscriber History** tab displays detailed information on the Distribution Agent, including status, history, informational messages, and any error messages.  
  
## Options  
 Select which Distribution Agent sessions to view from the **View** menu, and then select a specific session in the grid labeled **Sessions of the Distribution Agent**. Detailed information on this session is displayed in the grid labeled **Actions in the selected session**. If the selected session ended in an error, the text area labeled **Error details or message of the selected session** is also displayed.  
  
 **View**  
 Select which Distribution Agent sessions to view.  
  
 **Status**  
 The status of the Distribution Agent. The following list shows the possible status values:  
  
-   Error  
  
-   Completed  
  
-   Retrying  
  
-   Running  
  
 **Start Time**  
 The start time of the session.  
  
 **End Time**  
 The end time of the session. If the agent has not stopped, this field is empty.  
  
 **Duration**  
 The amount of time the Distribution Agent has run in this session. The time represents elapsed time if the agent is currently running and the total time of the session if the agent session had ended.  
  
 **Error Message**  
 If a session ended in an error, this field displays the last error message logged by the Distribution Agent. If a session did not end in an error, this field is blank.  
  
 **Action Message**  
 All informational messages and error messages that the Distribution Agent has logged during the selected session.  
  
 **Action Time**  
 The time at which the action described in the **Action Message** column was performed.  
  
 **Error details or message of the selected session**  
 Displayed only if the selected session displays a value of **Error** in the **Status** column. This text area displays detailed error information and the command that was attempted at the time of the error. It also includes links to additional content related to the error.  
  
## See Also  
 [Start the Replication Monitor](../../relational-databases/replication/monitor/start-the-replication-monitor.md)   
 [View information and perform tasks using Replication Monitor](../../relational-databases/replication/monitor/view-information-and-perform-tasks-replication-monitor.md)   
 [Monitoring Replication](../../relational-databases/replication/monitor/monitoring-replication.md)   
 [Replication Agents Overview](../../relational-databases/replication/agents/replication-agents-overview.md)  
  
  
