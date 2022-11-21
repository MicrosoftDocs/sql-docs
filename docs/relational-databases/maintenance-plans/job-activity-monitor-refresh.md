---
title: "Job Activity Monitor Refresh"
description: Job Activity Monitor Refresh
author: MikeRayMSFT
ms.author: mikeray
ms.date: "03/01/2017"
ms.service: sql
ms.subservice: supportability
ms.topic: conceptual
f1_keywords:
  - "sql13.swb.jobactivitymon.refresh.f1"
ms.assetid: 413a368e-fd2b-4e1f-b370-002cdbc85bab
---
# Job Activity Monitor Refresh
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  Use the **Refresh Settings** dialog box to configure how often Job Activity Monitor obtains new information about server activity. Job Activity Monitor must run queries on the monitored server to obtain information for the Job Activity Monitor grid. When the auto-refresh interval is set to less than 30 seconds, the time used to run these queries can affect server performance.  
  
 To open this dialog, click **View refresh settings**, in the **Status** section of Job Activity Monitor.  
  
## Options  
 **Auto-refresh every**  
 Check to initiate automatic refreshing of Activity Monitor information. This is off by default.  
  
 **seconds**  
 The number of seconds between auto-refresh attempts. Defaults to 60 seconds. Refreshes every 5 seconds when set to 5 or less.  
  
## See Also  
 [Monitor Job Activity](../../ssms/agent/monitor-job-activity.md)  
  
  
