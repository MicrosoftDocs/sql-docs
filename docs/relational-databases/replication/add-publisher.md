---
description: "Add Publisher"
title: "Add Publisher | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: conceptual
f1_keywords: 
  - "sql13.rep.monitor.addpublisher.f1"
helpviewer_keywords: 
  - "Add Publisher dialog box"
ms.assetid: 4b57e298-655f-42c2-82bc-25cdad94a194
author: "MashaMSFT"
ms.author: "mathoma"
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016"
---
# Add Publisher
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]
  The **Add Publisher** dialog box allows you to add to one or more Publishers to the left pane of Replication Monitor. After adding a Publisher, selecting the Publisher in the left pane shows information on the Publisher in the right pane.  
  
## Options  
 **Add**  
 Click to select a type of Publisher to add, which launches the **Connect to Server** dialog box. The options are:  
  
-   **Add SQL Server Publisher…**  
  
     Connect to the Publisher using the **Connect to Server** dialog box.  
  
-   **Add Oracle Publisher…**  
  
     Connect to the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Distributor associated with the Oracle Publisher using the **Connect to Server** dialog box.  
  
-   **Specify a Distributor and Add Its Publishers…**  
  
     Connect to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Distributor associated with one or more Publishers using the **Connect to Server** dialog box.  
  
 After successfully connecting to the Publisher or Distributor, the name of each Publisher and its Distributor are displayed in the grid at the top of the dialog box.  
  
> [!NOTE]  
>  The Distributor and Publisher often run on the same instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], but the Distributor can run on another instance (this configuration is referred to as a remote Distributor).  
  
 **Remove**  
 Select a Publisher in the grid at the top of the dialog box, and click **Remove** to remove the Publisher from the list of Publishers to be added.  
  
> [!NOTE]  
>  This button cannot be used to remove a Publisher that is already displayed in Replication Monitor. To remove a Publisher that is already displayed right-click the Publisher in the left pane of Replication Monitor and click **Remove**.  
  
 **Connect automatically when Replication Monitor starts**  
 Select to allow Replication Monitor to automatically connect to the Distributor and retrieve status information for the Publisher selected in the grid at the top of the dialog box. If this checkbox is cleared, you must manually connect after launching Replication Monitor: right-click the Publisher in the left pane of Replication Monitor, and click **Connect**.  
  
 **Automatically refresh the status of this Publisher and its publications**  
 Select to allow Replication Monitor to automatically refresh status for the Publisher selected in the grid at the top of the dialog box. If this option is selected, Replication Monitor polls the Distributor for status information on the Publisher and its publications. The polling interval is set by the **Refresh rate** option. For more information on refresh in Replication Monitor, see [Caching, Refresh, and Replication Monitor Performance](../../relational-databases/replication/monitor/caching-refresh-and-replication-monitor-performance.md).  
  
 **Refresh rate**  
 Enter a value (in seconds) to specify how frequently Replication Monitor should poll the Distributor for status. Lower values result in more frequent polling, which can affect performance at the Distributor if you are monitoring a large number of Publishers. It is recommended that you test your system to determine an appropriate value. The **Refresh rate** setting is also used if you select **Auto Refresh** in any of the detail windows in Replication Monitor.  
  
 **Show this Publisher in the following group**  
 Select a Publisher group from the list. The Publisher is displayed under this group in the left pane. Groups provide a way to organize Publishers and have no effect on how replication functions. If no groups are defined or you want to create a new one, click **New Group**.  
  
 **New Group**  
 Click to create a new Publisher group. A Publisher group provides a convenient way to organize Publishers within Replication Monitor. Groups do not affect the replication of data or the relationship among servers in a replication topology.  
  
## See Also  
 [Start the Replication Monitor](../../relational-databases/replication/monitor/start-the-replication-monitor.md)   
 [Monitoring Replication](../../relational-databases/replication/monitor/monitoring-replication.md)  
  
  
