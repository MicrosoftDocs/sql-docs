---
title: "Log Reader Agent | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
f1_keywords: 
  - "sql12.rep.monitor.logreaderagent.f1"
helpviewer_keywords: 
  - "Log Reader Agent dialog box"
ms.assetid: 300a3c46-0e48-4334-99c0-9ee690d2ef4f
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Log Reader Agent
  The **Log Reader Agent** dialog box displays detailed information on the Log Reader Agent, including status, history, informational messages, and any error messages.  
  
## Options  
 Select which Log Reader Agent sessions to view from the **View** menu, and then select a specific session in the grid labeled **Sessions of the Log Reader Agent**. Detailed information on this session is displayed in the grid labeled **Actions in the selected session**. If the selected session ended in an error, the text area labeled **Error details or message of the selected session** is also displayed.  
  
 **View**  
 Select which Log Reader Agent sessions to view. The Log Reader Agent typically runs continuously, therefore there might be only one session to view.  
  
 **Status**  
 The status of the Log Reader Agent. The following list shows the possible status values:  
  
-   Error  
  
-   Retrying failed commands  
  
-   Not running  
  
-   Running  
  
 **Start Time**  
 The start time of the session.  
  
 **End Time**  
 The end time of the session. If the agent has not stopped, this field is empty.  
  
 **Duration**  
 The amount of time the Log Reader Agent has run in this session. The time represents elapsed time if the agent is currently running and the total time of the session if the agent session has ended.  
  
 **Error Message**  
 If a session ended in an error, this field displays the last error message logged by the Log Reader Agent. If a session did not end in an error, this field is blank.  
  
 **Action Message**  
 All informational messages and error messages that the Log Reader Agent has logged during the selected session.  
  
 **Action Time**  
 The time at which the action described in the **Action Message** column was performed.  
  
 **Error details or message of the selected session**  
 Displayed only if the selected session displays a value of **Error** in the **Status** column. This text area displays detailed error information and the command that was attempted at the time of the error. It also includes links to additional content related to the error.  
  
## See Also  
 [Start the Replication Monitor](monitor/start-the-replication-monitor.md)   
 [View Information and Perform Tasks using Replication Monitor](monitor/view-information-and-perform-tasks-replication-monitor.md)   
 [Monitoring Replication](monitoring-replication.md)   
 [Replication Agents Overview](agents/replication-agents-overview.md)  
  
  
