---
title: "SQL Server Replication 'Publisher Settings' dialog box | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
f1_keywords: 
  - "sql13.rep.monitor.publishersettings.f1"
helpviewer_keywords: 
  - "Publisher Settings dialog box"
ms.assetid: 4fb70427-082d-4179-82a1-34b235accc43
author: "MashaMSFT"
ms.author: "mathoma"
manager: craigg
---
# SQL Server Replication 'Publisher Settings' dialog box
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  The **Publisher Settings** dialog box allows you to change settings for Publishers that have been added to the left pane in Replication Monitor.  
  
## Options  
 **Publisher Connection**  
 Click to launch the **Connect to Server** dialog box, which allows you to view and change the connection properties and credentials Replication Monitor uses to connect to a Publisher.  
  
 **Distributor Connection**  
 Displayed only if the Publisher uses a remote Distributor. Click to launch the **Connect to Server** dialog box, which allows you to view and change the connection properties and credentials Replication Monitor uses to connect to the remote Distributor.  
  
 **Connect automatically when Replication Monitor starts**  
 Select to allow Replication Monitor to automatically connect to the Distributor and retrieve status information for the Publisher selected in the grid at the top of the dialog box. If this checkbox is cleared, you must manually connect after launching Replication Monitor: right-click the Publisher in the left pane of Replication Monitor, and click **Connect**.  
  
 **Automatically refresh the status of this Publisher and its publications**  
 Select to allow Replication Monitor to automatically refresh status for the Publisher selected in the grid at the top of the dialog box. If this option is selected, Replication Monitor polls the Distributor for status information on the Publisher and its publications. The polling interval is set by the **Refresh rate** option. For more information on refresh in Replication Monitor, see [Caching, Refresh, and Replication Monitor Performance](../../relational-databases/replication/monitor/caching-refresh-and-replication-monitor-performance.md).  
  
 **Refresh rate**  
 Enter a value (in seconds) to specify how frequently Replication Monitor should poll the Distributor for status. Lower values result in more frequent polling, which can affect performance at the Distributor if you are monitoring a large number of Publishers. It is recommended that you test your system to determine an appropriate value. The **Refresh rate** setting is also used if you select **Auto Refresh** in any of the detail windows in Replication Monitor.  
  
 **Show this Publisher in the following group**  
 Select a Publisher group from the list. The Publisher is displayed under this group in the left pane. Groups provide a way to organize Publishers and have no effect on how replication functions.  
  
 **New Group**  
 Click to create a new Publisher group. A Publisher group provides a convenient way to organize Publishers within Replication Monitor. Groups do not affect the replication of data or the relationship among servers in a replication topology.  
  
## See Also  
 [Start the Replication Monitor](../../relational-databases/replication/monitor/start-the-replication-monitor.md)   
 [Monitoring Replication](../../relational-databases/replication/monitor/monitoring-replication.md)  
  
  
