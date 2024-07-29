---
title: "Connect to another computer (SQL Server Configuration Manager)"
description: "Find out how to manage a remote computer's services. See how to use SQL Server Configuration Manager or SQL Server Management Studio for this task."
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/26/2024
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
helpviewer_keywords:
  - "connections [SQL Server], other computers"
---
# SQL Server Configuration Manager: Connect to another computer

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article describes how to connect to another computer in [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)]. Follow the first procedure to open the Windows Computer Management [!INCLUDE [msCoName](../../includes/msconame-md.md)] Management Console (mmc), connect to the computer, and expand the Services and Applications tree. Follow the second procedure to create a file with a link to the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager on a remote computer.

> [!NOTE]  
> Some actions can't be performed by Configuration Manager when connecting remotely.

To start, stop, pause, or resume services on another computer, you can also connect to the server with [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)], right-click the server or [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent and then select the desired action.

## <a id="SSMSProcedure"></a>

### <a id="to-connect-to-another-computer-with-windows-computer-management"></a> Connect to another computer with Windows Computer Management

1. Right-click the **Start** menu button, and then select **Computer Management (Local)**.
1. On the **Action** menu, select **Connect to another computer**.
1. In the **Select Computer** dialog box, in the **Another computer** text box, type the name of the computer you want to manage, and then select **OK**.

   Computer Management displays the services running on the remote computer. The top-level node changes to **Computer Management** \<*remotecomputer*>.

1. In the console tree, expand **Services and Applications**, and then expand **SQL Server Configuration Manager** to manage the remote computer's services.

### <a id="to-save-a-link-to-sql-server-configuration-manager-for-another-computer"></a> Save a link to SQL Server Configuration Manager for another computer

1. On the **Start** menu, select **Run**.

1. In the **Open** box, type **mmc -a** (type **mmc /32 -a** on a 64-bit computer) to open the [!INCLUDE [msCoName](../../includes/msconame-md.md)] Management Console in author mode.
1. On the **File** menu, select **Add/Remove Snap-in**.
1. In the **Add/Remove Snap-in** window, select **Add**.
1. In the **Add Standalone Snap-in** window, select **Computer Management** and then select **Add**.
1. In the **Computer Management** window, select **Another computer**, type the name of the remote computer you wish to manage, and then select **Finish**.
1. In the **Add Standalone Snap-in** window, select **Close**.
1. In the **Add/Remove Snap-in** window, select **OK**.
1. Expand **Computer Management (***\<computer name>***)**, and **Services and Applications**.
1. Right-click **SQL Server Configuration Manager**, and then select **New Window from here**.
1. On the **Window** menu, select **Console Root**, to switch back to the first window, and delete the window.
1. On the **File** menu, select **Save As**, and save the file in the desired folder, with an appropriate name with the **.msc** file extension. Close the [!INCLUDE [msCoName](../../includes/msconame-md.md)] Management Console.
1. To open [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager on the target computer, double-click the file. If desired, save a link to the file on the desktop or in the **Start** menu.

> [!CAUTION]  
> When using [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager on a remote computer, the computer name isn't obvious and it's possible to mistakenly stop or configure the wrong computer. On the **Service** tab, check the **Host Name** box to confirm the computer name before modifying a service.

## Related content

- [Configure WMI to Show Server Status in SQL Server Tools](../../ssms/configure-wmi-to-show-server-status-in-sql-server-tools.md)
