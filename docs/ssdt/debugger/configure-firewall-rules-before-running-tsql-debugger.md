---
title: Configure firewall rules before running the T-SQL debugger
description: Learn how to configure the Windows Firewall rules before running the Transact-SQL (T-SQL) debugger when connected to an SQL Server in SQL Server Management Studio.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/28/2024
ms.service: sql
ms.subservice: ssdt
ms.topic: conceptual
---
# Configure firewall rules before running the Transact-SQL debugger

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Windows Firewall rules must be configured to enable [!INCLUDE [tsql](../../includes/tsql-md.md)] debugging when connected to an instance of the [!INCLUDE [ssDE](../../includes/ssde-md.md)] that is running on a different computer than the [!INCLUDE [ssDE](../../includes/ssde-md.md)] Query Editor.

## Configure the Transact-SQL debugger

The [!INCLUDE [tsql](../../includes/tsql-md.md)] debugger includes both server-side and client-side components. The server-side debugger components are installed with each instance of the [!INCLUDE [ssdenoversion-md](../../includes/ssdenoversion-md.md)]. The client-side debugger components are included:

- When you install Microsoft Visual Studio 2019 or later versions

- When you install [!INCLUDE [ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] from the web download

There are no configuration requirements to run the [!INCLUDE [tsql](../../includes/tsql-md.md)] debugger when [!INCLUDE [ssBIDevStudio](../../includes/ssbidevstudio-md.md)] is running on the same computer as the instance of the [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)]. However, to run the [!INCLUDE [tsql](../../includes/tsql-md.md)] debugger when connected to a remote instance of the [!INCLUDE [ssDE](../../includes/ssde-md.md)], program and port rules in the Windows Firewall must be enabled on both computers. If you get errors attempting to open a remote debugging session, ensure the following firewall rules are defined on your computer.

Use the **Windows Firewall with Advanced Security** application to manage the firewall rules. In both [!INCLUDE [win7](../../includes/win7-md.md)] and [!INCLUDE [winserver2008r2](../../includes/winserver2008r2-md.md)], open **Control Panel**, open **Windows Firewall**, and select **Advanced settings**. In [!INCLUDE [winserver2008r2](../../includes/winserver2008r2-md.md)], you can also open **Service Manager**, expand **Configuration** in the left pane, and expand **Windows Firewall with Advanced Security**.

> [!CAUTION]  
> Enabling rules in the Windows Firewall can expose your computer to security threats that the firewall is designed to block. Enabling rules for remote debugging unblocks the ports and programs listed in this topic.

## Firewall rules on the server

On the computer that is running the instance of the [!INCLUDE [ssDE](../../includes/ssde-md.md)], use **Windows Firewall with Advanced Security** to specify the following information:

- Add an inbound program rule for `sqlservr.exe`. You must have a rule for each instance that needs to support remote debugging sessions.

   1. In **Windows Firewall with Advanced Security**, in the left pane, right-click **Inbound Rules**, and then select **New Rule** in the action pane.

   1. In the **Rule Type** dialog, select **Program**, and then select **Next**.

   1. In the **Program** dialog, select **This program path:** and enter the full path to `sqlservr.exe` for this instance. By default, `sqlservr.exe` is installed in `C:\Program Files\Microsoft SQL Server\MSSQL16.<InstanceName>\MSSQL\Binn`, where `<InstanceName>` is `MSSQLSERVER` for the default instance, and the instance name for any named instance.

   1. In the **Action** dialog, select **Allow the connection**, and select **Next**.

   1. In the **Profile** dialog, select any profiles that describe the computer connection environment when you want to open a debugging session with the instance, and select **Next**.

   1. In the **Name** dialog, type a name and description for this rule and select **Finish**.

   1. In the **Inbound Rules** list, right-click the rule you created, and then select **Properties** in the action pane.

   1. Select the **Protocols and Ports** tab.

   1. Select **TCP** in the **Protocol type:** box, select **RPC Dynamic Ports** in the **Local port:** box, select **Apply**, and then select **OK**.

- Add an inbound program rule for `svchost.exe` to enable Distributed Component Object Model (DCOM) communications from remote debugger sessions.

   1. In **Windows Firewall with Advanced Security**, in the left pane, right-click **Inbound Rules**, and then select **New Rule** in the action pane.

   1. In the **Rule Type** dialog, select **Program**, and then select **Next**.

   1. In the **Program** dialog, select **This program path:** and enter the full path to `svchost.exe`. By default, `svchost.exe` is installed in `%systemroot%\System32\svchost.exe`.

   1. In the **Action** dialog, select **Allow the connection**, and select **Next**.

   1. In the **Profile** dialog, select any profiles that describe the computer connection environment when you want to open a debugging session with the instance, and select **Next**.

   1. In the **Name** dialog, type a name and description for this rule and select **Finish**.

   1. In the **Inbound Rules** list, right-click the rule you created, and then select **Properties** in the action pane.

   1. Select the **Protocols and Ports** tab.

   1. Select **TCP** in the **Protocol type:** box, select **RPC Endpoint Mapper** in the **Local port:** box, select **Apply**, and then select **OK**.

- If the domain policy requires network communications to be done through IPsec, you must also add inbound rules opening UDP port 4500 and UDP port 500.

## Firewall rules on the client

On the computer that is running the [!INCLUDE [ssDE](../../includes/ssde-md.md)] Query Editor, configure the Windows Firewall to allow remote debugging.

If you get errors attempting to open a remote debugging session, you can manually configure the program and port exceptions by using **Windows Firewall with Advanced Security** to configure firewall rules:

- Add a program entry for svchost:

   1. In **Windows Firewall with Advanced Security**, in the left pane, right-click **Inbound Rules**, and then select **New Rule** in the action pane.

   1. In the **Rule Type** dialog, select **Program**, and then select **Next**.

   1. In the **Program** dialog, select **This program path:** and enter the full path to `svchost.exe`. By default, `svchost.exe` is installed in `%systemroot%\System32\svchost.exe`.

   1. In the **Action** dialog, select **Allow the connection**, and select **Next**.

   1. In the **Profile** dialog, select any profiles that describe the computer connection environment when you want to open a debugging session with the instance, and select **Next**.

   1. In the **Name** dialog, type a name and description for this rule and select **Finish**.

   1. In the **Inbound Rules** list, right-click the rule you created, and then select **Properties** in the action pane.

   1. Select the **Protocols and Ports** tab.

   1. Select **TCP** in the **Protocol type:** box, select **RPC Endpoint Mapper** in the **Local port:** box, select **Apply**, and then select **OK**.

- Add a program entry for the application hosting the [!INCLUDE [ssBIDevStudio](../../includes/ssbidevstudio-md.md)] [!INCLUDE [ssDE](../../includes/ssde-md.md)] Query Editor. If you need to open remote debugging sessions from multiple installations of [!INCLUDE [ssBIDevStudio](../../includes/ssbidevstudio-md.md)] on the same computer, you must add a program rule for both:

   1. In **Windows Firewall with Advanced Security**, in the left pane, right-click **Inbound Rules**, and then select **New Rule** in the action pane.

   1. In the **Rule Type** dialog, select **Program**, and then select **Next**.

   1. In the **Program** dialog, select **This program path:** and enter one of these three values.

       - For [!INCLUDE [ssBIDevStudio](../../includes/ssbidevstudio-md.md)] enter the full path to `devenv.exe`:

            1. By default, the `devenv.exe` for Visual Studio 2022 is in `C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\IDE`.

            1. By default, the `devenv.exe` for Visual Studio 2019 is in `C:\Program Files (x86)\Microsoft Visual Studio\2017\Professional\Common7\IDE`.

            1. You can find the path to `devenv.exe` from the shortcut you use to launch [!INCLUDE [ssBIDevStudio](../../includes/ssbidevstudio-md.md)]. Right-click the shortcut and select **Properties**. The executable and path are listed in the **Target** box.

   1. In the **Action** dialog, select **Allow the connection**, and select **Next**.

   1. In the **Profile** dialog, select any profiles that describe the computer connection environment when you want to open a debugging session with the instance, and select **Next**.

   1. In the **Name** dialog, type a name and description for this rule and select **Finish**.

   1. In the **Inbound Rules** list, right-click the rule you created, and then select **Properties** in the action pane.

   1. Select the **Protocols and Ports** tab.

   1. Select **TCP** in the **Protocol type:** box, select **RPC Dynamic Ports** in the **Local port:** box, select **Apply**, and then select **OK**.

## Requirements for starting the debugger

All attempts to start the [!INCLUDE [tsql](../../includes/tsql-md.md)] debugger must also meet the following requirements:

- [!INCLUDE [ssBIDevStudio](../../includes/ssbidevstudio-md.md)] must be running under a Windows account that is a member of the **sysadmin** fixed server role.

- The [!INCLUDE [ssDE](../../includes/ssde-md.md)] Query Editor window must be connected by using either a Windows Authentication or [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Authentication login that is a member of the **sysadmin** fixed server role.

- The [!INCLUDE [ssDE](../../includes/ssde-md.md)] Query Editor window must be connected to an instance of the [!INCLUDE [ssdenoversion-md](../../includes/ssdenoversion-md.md)]. You can't run the debugger when the Query Editor window is connected to an instance that is in single-user mode.

- The server needs to communicate back to the client via RPC. The account under which SQL Server service is running must have authenticated permissions to the client.

## Related content

- [Transact-SQL Debugger](./transact-sql-debugger.md)
- [Run the Transact-SQL Debugger](./run-transact-sql-debugger.md)
- [Step Through Transact-SQL Code](./step-through-transact-sql-code.md)
- [Debug Stored Procedures](debug-stored-procedures.md)
- [Transact-SQL Debugger Information](./transact-sql-debugger-information.md)
