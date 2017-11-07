---
title: "Job Activity Monitor Refresh | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
f1_keywords: 
  - "sql13.swb.jobactivitymon.refresh.f1"
ms.assetid: 413a368e-fd2b-4e1f-b370-002cdbc85bab
caps.latest.revision: 11
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# Job Activity Monitor Refresh
  Use the **Refresh Settings** dialog box to configure how often Job Activity Monitor obtains new information about server activity. Job Activity Monitor must run queries on the monitored server to obtain information for the Job Activity Monitor grid. When the auto-refresh interval is set to less than 30 seconds, the time used to run these queries can affect server performance.  
  
 To open this dialog, click **View refresh settings**, in the **Status** section of Job Activity Monitor.  
  
## Options  
 **Auto-refresh every**  
 Check to initiate automatic refreshing of Activity Monitor information. This is off by default.  
  
 **seconds**  
 The number of seconds between auto-refresh attempts. Defaults to 60 seconds. Refreshes every 5 seconds when set to 5 or less.  
  
## See Also  
 [Monitor Job Activity](http://msdn.microsoft.com/library/71cb432b-631d-4b8b-9965-e731b3d8266d)  
  
  