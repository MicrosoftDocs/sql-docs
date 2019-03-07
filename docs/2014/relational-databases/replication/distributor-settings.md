---
title: "'Distributor Settings' dialog box | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
f1_keywords: 
  - "sql12.rep.monitor.DistributorSettings.f1"
helpviewer_keywords: 
  - "Distributor Settings dialog box"
ms.assetid: 8276a521-bdd1-4783-bdb6-7ab43499c0ca
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# SQL Server Replication 'Distributor Settings' dialog box
  The **Distributor Settings** dialog box enables you to change settings for Distributors that were added to the left pane in Replication Monitor.  
  
## Options  
 **Connect automatically when Replication Monitor starts**  
 Select to let Replication Monitor connect to the Distributor and retrieve status information.  
  
 **Connection**  
 Click to display the **Connect to Server** dialog box. This enables you to view and change the connection properties and credentials that Replication Monitor uses to connect to the Distributor.  
  
 **Automatically refresh the status of this Distributor and its publications**  
 Select to let Replication Monitor automatically refresh the status for the Distributor. If this option is selected, Replication Monitor polls the Distributor for status information based on the polling interval set by the **Refresh rate** option. For more information about refresh in Replication Monitor, see [Caching, Refresh, and Replication Monitor Performance](monitor/caching-refresh-and-replication-monitor-performance.md).  
  
 **Refresh rate**  
 Enter a value (in seconds) to specify how frequently Replication Monitor should poll the Distributor for status. Lower values result in more frequent polling. This can affect performance at the Distributor if you are monitoring many Publishers. We recommend that you test your system to determine an appropriate value. The **Refresh rate** setting is also used if you select **Auto Refresh** in any of the detail windows in Replication Monitor.  
  
 **Show all Publishers of this Distributor in the following group**  
 Select a Publisher group from the list. The Publisher is displayed under this group in the left pane. Groups provide a way for you to organize Publishers and do not affect how replication functions.  
  
 **New Group**  
 Click to create a new Publisher group. A Publisher group provides a way for you to conveniently organize Publishers within Replication Monitor. Groups do not affect the replication of data or the relationship among servers in a replication topology.  
  
## See Also  
 [Start the Replication Monitor](monitor/start-the-replication-monitor.md)   
 [Monitoring Replication](monitoring-replication.md)  
  
  
