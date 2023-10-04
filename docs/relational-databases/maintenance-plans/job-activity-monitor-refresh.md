---
title: "Job Activity Monitor Refresh"
description: Job Activity Monitor Refresh
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 03/27/2023
ms.service: sql
ms.subservice: supportability
ms.topic: conceptual
f1_keywords:
  - "sql13.swb.jobactivitymon.refresh.f1"
---
# Job Activity Monitor Refresh

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Use the **Refresh Settings** dialog box to configure how often Job Activity Monitor obtains new information about server activity. Job Activity Monitor must run queries on the monitored server to obtain information for the Job Activity Monitor grid. When the auto-refresh interval is set to less than 30 seconds, the time used to run these queries can affect server performance.

To open this dialog, select **View refresh settings**, in the **Status** section of Job Activity Monitor.

## Options

- **Auto-refresh every**

  Check to initiate automatic refreshing of Activity Monitor information. This option is off by default.

- **seconds**

  The number of seconds between auto-refresh attempts. Defaults to 60 seconds. Refreshes every 5 seconds when set to 5 or less.

## See also

- [Monitor Job Activity](../../ssms/agent/monitor-job-activity.md)
