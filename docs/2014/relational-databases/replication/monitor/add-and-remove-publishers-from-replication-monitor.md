---
title: "Add and Remove Publishers from Replication Monitor | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "Replication Monitor, adding and removing Publishers"
ms.assetid: fa36c4b4-bfa5-494e-92e3-07a02d7332c3
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Add and Remove Publishers from Replication Monitor
  The server from which you launch Replication Monitor is automatically added to the monitor if it is a Publisher. Additional Publishers can be added through the **Add Publisher** dialog box. After adding a Publisher, it is displayed in a group in the left pane of the monitor. The **My Publishers** group is included by default, but you can create new groups to manage one or more replication topologies. For information about starting Replication Monitor, see [Start the Replication Monitor](start-the-replication-monitor.md).  
  
### To add a SQL Server Publisher  
  
1.  Right-click the **Replication Monitor** node or a Publisher group node in the left pane, and then click **Add Publisher**.  
  
2.  In the **Add Publisher** dialog box, click **Add**, and then click **Add SQL Server Publisher**.  
  
3.  In the **Connect to Server** dialog box, enter the name of the Publisher, and then select the authentication type. If you select **SQL Server Authentication**, enter a login and password. The credentials you specify are saved by Replication Monitor to use when connecting to this server in the future. The Windows account or SQL Server login specified must be a member of the **sysadmin** fixed server role or a member of the **replmonitor** fixed database role in the distribution database.  
  
4.  Click **Connect**. If the Publisher uses a remote Distributor, you will be prompted to connect to the Distributor in the **Connect to Server** dialog box. The credentials you specify are saved by Replication Monitor to use when connecting to this server in the future. The Windows account or SQL Server login specified must be a member of the **sysadmin** fixed server role or a member of the **replmonitor** fixed database role in the distribution database.  
  
5.  The name of the Publisher and Distributor are displayed in the **Start monitoring the following Publisher(s)** grid.  
  
6.  To specify refresh and connection options for the Publisher, select the Publisher in the grid, and modify options as necessary. For more information about refresh options, see [Caching, Refresh, and Replication Monitor Performance](caching-refresh-and-replication-monitor-performance.md).  
  
7.  Select the group under which the Publisher should be displayed in Replication Monitor. To create a new group, click **New Group**, and then enter a group name; select the group in the **Show this Publisher(s) in the following group** list.  
  
8.  [!INCLUDE[clickOK](../../../includes/clickok-md.md)]  
  
### To add an Oracle Publisher  
  
1.  Right-click the **Replication Monitor** node or a Publisher group node in the left pane, and then click **Add Publisher**.  
  
2.  In the **Add Publisher** dialog box, click **Add**, and then click **Add Oracle Publisher**.  
  
3.  In the **Connect to Server** dialog box, enter the name of the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Distributor associated with the Oracle Publisher, and then select the authentication type. If you select **SQL Server Authentication**, enter a login and password. The credentials you specify are saved by Replication Monitor to use when connecting to this server in the future. The Windows account or SQL Server login specified must be a member of the **sysadmin** fixed server role or a member of the **replmonitor** fixed database role in the distribution database.  
  
4.  Click **Connect**.  
  
5.  The name of the Publisher and Distributor are displayed in the **Start monitoring the following Publisher(s)** grid.  
  
6.  To specify refresh and connection options for the Publisher, select the Publisher in the grid, and modify options as necessary. For more information about refresh options, see [Caching, Refresh, and Replication Monitor Performance](caching-refresh-and-replication-monitor-performance.md).  
  
7.  Select the group under which the Publisher should be displayed in Replication Monitor. To create a new group, click **New Group**, and then enter a group name; select the group in the **Show this Publisher(s) in the following group** list.  
  
8.  [!INCLUDE[clickOK](../../../includes/clickok-md.md)]  
  
### To add one or more Publishers that use the same Distributor  
  
1.  Right-click the **Replication Monitor** node or a Publisher group node in the left pane, and then click **Add Publisher**.  
  
2.  In the **Add Publisher** dialog box, click **Add**, and then click **Specify a Distributor and Add Its Publishers**.  
  
3.  In the **Connect to Server** dialog box, enter the name of the Distributor, and then select the authentication type. If you select **SQL Server Authentication**, enter a login and password. The credentials you specify are saved by Replication Monitor to use when connecting to this server in the future. The Windows account or SQL Server login specified must be a member of the **sysadmin** fixed server role or a member of the **replmonitor** fixed database role in the distribution database.  
  
4.  Click **Connect**.  
  
5.  The name of the Distributor and each Publisher are displayed in the **Start monitoring the following Publisher(s)** grid. If a Publisher has already been added to Replication Monitor, it does not appear in the grid.  
  
6.  To specify refresh and connection options for the Publisher, select the Publisher in the grid, and modify options as necessary. For more information about refresh options, see [Caching, Refresh, and Replication Monitor Performance](caching-refresh-and-replication-monitor-performance.md).  
  
7.  Select the group under which Publishers should be displayed in Replication Monitor. To create a new group, click **New Group**, and then enter a group name; select the group in the **Show this Publisher(s) in the following group** list.  
  
8.  [!INCLUDE[clickOK](../../../includes/clickok-md.md)]  
  
### To modify settings for the Publisher and Publisher Groups  
  
1.  Right-click a Publisher in the left pane, and then click **Publisher Settings**.  
  
2.  Make any changes in the **Publisher Settings** dialog box:  
  
    -   To change the credentials that Replication Monitor uses to connect to a server, click **Publisher Connection** or **Distributor Connection**, and then enter credentials in the **Connect to Server** dialog box.  
  
    -   To move a Publisher from one group to another, select the Publisher in the **Start monitoring the following Publisher(s)** grid, and then select the new group in the **Show this Publisher(s) in the following group** list.  
  
3.  [!INCLUDE[clickOK](../../../includes/clickok-md.md)]  
  
### To remove a Publisher from Replication Monitor  
  
1.  Right-click a Publisher in the left pane.  
  
2.  Click **Remove**.  
  
### To add a Publisher group to Replication Monitor  
  
1.  Publisher groups can be created only when adding a Publisher or modifying settings for a Publisher. See the how to procedures on adding a Publisher for more information.  
  
### To remove a Publisher group from Replication Monitor  
  
1.  Move all Publishers to a different group or remove them from Replication Monitor. For more information, see previous procedures in this topic.  
  
2.  Right-click the Publisher group, and then click **Remove**.  
  
## See Also  
 [Configure Distribution](../configure-distribution.md)   
 [Monitoring Replication](../monitoring-replication.md)  
  
  
