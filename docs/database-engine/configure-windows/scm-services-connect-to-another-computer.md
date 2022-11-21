---
title: "Connect to Another Computer (SQL Server Configuration Manager)"
description: "Find out how to manage a remote computer's services. See how to use SQL Server Configuration Manager or SQL Server Management Studio for this task."
author: rwestMSFT
ms.author: randolphwest
ms.date: "11/19/2019"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
helpviewer_keywords:
  - "connections [SQL Server], other computers"
---
# SCM Services - Connect to Another Computer

 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article describes how to connect to another computer in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)]. Follow the first procedure to open the Windows Computer Management [!INCLUDE[msCoName](../../includes/msconame-md.md)] Management Console (mmc), connect to the computer, and expand the Services and Applications tree. Follow the second procedure to create a file with a link to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager on a remote computer.

> [!NOTE]
> Some actions cannot be performed by Configuration Manager when connecting remotely.

To start, stop, pause, or resume services on another computer, you can also connect to the server with [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], right-click the server or [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent and then click the desired action.

## <a name="SSMSProcedure"></a>

### To connect to another computer with Windows Computer Management

1. Right-click the **Start** menu button, and then click **Computer Management (Local)**.
2. On the **Action** menu, click **Connect to another computer**.
3. In the **Select Computer** dialog box, in the **Another computer** text box, type the name of the computer you want to manage, and then click **OK**.

   Computer Management displays the services running on the remote computer. The top-level node changes to **Computer Management** \<*remotecomputer*>.

4. In the console tree, expand **Services and Applications**, and then expand **SQL Server Configuration Manager** to manage the remote computer's services.

### To save a link to SQL Server Configuration Manager for another computer

1. On the **Start** menu, click **Run**.

2. In the **Open** box, type **mmc -a** (type **mmc /32 -a** on a 64-bit computer) to open the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Management Console in author mode.
3. On the **File** menu, click **Add/Remove Snap-in**.
4. In the **Add/Remove Snap-in** window, click **Add**.
5. In the **Add Standalone Snap-in** window, click **Computer Management** and then click **Add**.
6. In the **Computer Management** window, click **Another computer**, type the name of the remote computer you wish to manage, and then click **Finish**.
7. In the **Add Standalone Snap-in** window, click **Close**.
8. In the **Add/Remove Snap-in** window, click **OK**.
9. Expand **Computer Management (**_\<computer name>_**)**, and **Services and Applications**.
10. Right-click **SQL Server Configuration Manager**, and then click **New Window from here**.
11. On the **Window** menu, click **Console Root**, to switch back to the first window, and delete the window.
12. On the **File** menu, click **Save As**, and save the file in the desired folder, with an appropriate name with the **.msc** file extension. Close the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Management Console.
13. To open [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager on the target computer, double-click the file. If desired, save a link to the file on the desktop or in the **Start** menu.

> [!CAUTION]
> When using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager on a remote computer, the computer name is not obvious and it is possible to mistakenly stop or configure the wrong computer. On the **Service** tab, check the **Host Name** box to confirm the computer name before modifying a service.

## See also

- [Configure WMI to Show Server Status in SQL Server Tools](../../ssms/configure-wmi-to-show-server-status-in-sql-server-tools.md)
