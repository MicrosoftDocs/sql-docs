---
title: "Queue Reader Agent"
description: "Queue Reader Agent"
author: "MashaMSFT"
ms.author: "mathoma"
ms.date: 09/25/2024
ms.service: sql
ms.subservice: replication
ms.topic: ui-reference
ms.custom:
  - updatefrequency5
f1_keywords:
  - "sql13.rep.monitor.queuereaderagent.f1"
helpviewer_keywords:
  - "Queue Reader Agent dialog box"
---
# Queue Reader Agent
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  The **Queue Reader Agent** dialog box displays detailed information on the Queue Reader Agent, including status, history, informational messages, and any error messages.  
  
## Options  
 Select which Queue Reader Agent sessions to view from the **View** menu, and then select a specific session in the grid labeled **Sessions of the Queue Reader Agent**. Detailed information on this session is displayed in the grid labeled **Actions in the selected session**. If the selected session ended in an error, the text area labeled **Error details or message of the selected session** is also displayed.  
  
 **View**  
 Select which Queue Reader Agent sessions to view. The Queue Reader Agent typically runs continuously, therefore there might be only one session to view.  
  
 **Status**  
 The status of the Queue Reader Agent. The following list shows the possible status values:  
  
-   Error  
  
-   Retrying failed commands  
  
-   Not running  
  
-   Running  
  
 **Start Time**  
 The start time of the session.  
  
 **End Time**  
 The end time of the session. If the agent has not stopped, this field is empty.  
  
 **Duration**  
 The amount of time the Queue Reader Agent has run in this session. The time represents elapsed time if the agent is currently running and the total time of the session if the agent session has ended.  
  
 **Error Message**  
 If a session ended in an error, this field displays the last error message logged by the Queue Reader Agent. If a session did not end in an error, this field is blank.  
  
 **Action Message**  
 All informational messages and error messages that the Queue Reader Agent has logged during the selected session.  
  
 **Action Time**  
 The time at which the action described in the **Action Message** column was performed.  
  
 **Error details or message of the selected session**  
 Displayed only if the selected session displays a value of **Error** in the **Status** column. This text area displays detailed error information and the command that was attempted at the time of the error. It also includes links to additional content related to the error.  
  
## Related content

- [Start the Replication Monitor](../../relational-databases/replication/monitor/start-the-replication-monitor.md)
- [View information and perform tasks using Replication Monitor](../../relational-databases/replication/monitor/view-information-and-perform-tasks-replication-monitor.md)
- [Monitoring Replication](../../relational-databases/replication/monitor/monitoring-replication.md)
- [Replication Agents Overview](../../relational-databases/replication/agents/replication-agents-overview.md)
