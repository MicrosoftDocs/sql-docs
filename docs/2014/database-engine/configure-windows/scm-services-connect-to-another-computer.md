---
title: "Connect to Another Computer (SQL Server Configuration Manager) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
helpviewer_keywords: 
  - "connections [SQL Server], other computers"
ms.assetid: c4c1e94f-4f5f-431e-8b5b-d5ff97baf723
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Connect to Another Computer (SQL Server Configuration Manager)
  This topic describes how to connect to another computer in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]. Follow the first procedure to open the Windows Computer Management [!INCLUDE[msCoName](../../includes/msconame-md.md)] Management Console (mmc), connect to the computer, and expand the Services and Applications tree. Follow the second procedure to create a file with a link to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager on a remote computer.  
  
> [!NOTE]  
>  Some actions cannot be performed by Configuration Manager when connecting remotely.  
  
 To start, stop, pause, or resume services on another computer, you can also connect to the server with [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], right-click the server or [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent and then click the desired action.  
  
##  <a name="SSMSProcedure"></a>  
  
#### To connect to another computer with Windows Computer Management  
  
1.  On the **Start** menu, right-click **My Computer**, and then click **Manage.**  
  
2.  In **Computer Management**, right-click **Computer Management (Local)**, and then click **Connect to another computer**.  
  
3.  In the **Select Computer** dialog box, in the **Another computer** text box, type the name of the computer you want to manage, and then click **OK**.  
  
     Computer Management displays the services running on the remote computer. The top-level node changes to **Computer Management** \<*remotecomputer*>.  
  
4.  In the console tree, expand **Services and Applications**, and then expand **SQL Server Configuration Manager** to manage the remote computer's services.  
  
#### To save a link to SQL Server Configuration Manager for another computer  
  
1.  On the **Start** menu, click **Run**.  
  
2.  In the **Open** box, type `mmc -a` to open the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Management Console in author mode.  
  
3.  On the **File** menu, click **Add/Remove Snap-in**.  
  
4.  In the **Add/Remove Snap-in** window, click **Add**.  
  
5.  In the **Add Standalone Snap-in** window, click **Computer Management** and then click **Add**.  
  
6.  In the **Computer Management** window, click **Another computer**, type the name of the remote computer you wish to manage, and then click **Finish**.  
  
7.  In the **Add Standalone Snap-in** window, click **Close**.  
  
8.  In the **Add/Remove Snap-in** window, click **OK**.  
  
9. Expand **Computer Management (***\<computer name>***)**, and **Services and Applications**.  
  
10. Right-click **SQL Server Configuration Manager**, and then click **New Window from here**.  
  
11. On the **Window** menu, click **Console Root**, to switch back to the first window, and delete the window.  
  
12. On the **File** menu, click **Save As**, and save the file in the desired folder, with an appropriate name with the `.msc` file extension. Close the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Management Console.  
  
13. To open [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager on the target computer, double-click the file. If desired, save a link to the file on the desktop or in the **Start** menu.  
  
> [!CAUTION]  
>  When using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager on a remote computer, the computer name is not obvious and it is possible to mistakenly stop or configure the wrong computer. On the **Service** tab, check the **Host Name** box to confirm the computer name before modifying a service.  
  
## See Also  
 [Configure WMI to Show Server Status in SQL Server Tools](../../ssms/configure-wmi-to-show-server-status-in-sql-server-tools.md)  
  
  
