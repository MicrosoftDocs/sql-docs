---
title: "Configure firewall rules before running the TSQL Debugger | Microsoft Docs"
ms.custom: ""
ms.date: "10/20/2016"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.technology: scripting
ms.reviewer: ""
ms.topic: conceptual
f1_keywords:
  - "vs.debug.error.sqlde_accessdenied"
  - "vs.debug.error.sqlde_register_failed"
  - "vs.debug.error.sqlde_firewall.remotemachines"
helpviewer_keywords:
  - "Transact-SQL debugger, remote connections"
  - "Windows Firewall [Database Engine], Transact-SQL debugger"
  - "Transact-SQL debugger, Windows Firewall"
  - "Transact-SQL debugger, configuring"
  - "ports [SQL Server], Transact-SQL debugger"
  - "TCP/IP [SQL Server], port numbers"
ms.assetid: f50e0b0d-eaf0-4f4a-be83-96f5be63e7ea
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Configure firewall rules before running the TSQL Debugger
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

Windows Firewall rules must be configured to enable [!INCLUDE[tsql](../../includes/tsql-md.md)] debugging when connected to an instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] that is running on a different computer than the [!INCLUDE[ssDE](../../includes/ssde-md.md)] Query Editor.

## Configuring the Transact-SQL Debugger

The [!INCLUDE[tsql](../../includes/tsql-md.md)] debugger includes both server-side and client-side components. The server-side debugger components are installed with each instance of the Database Engine from [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] Service Pack 2 (SP2) or later. The client-side debugger components are included:

-   When you install the client-side tools from [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] or later.

-   When you install Microsoft Visual Studio 2010 or later.

-   When you install [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] from the web download.

There are no configuration requirements to run the [!INCLUDE[tsql](../../includes/tsql-md.md)] debugger when [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)] is running on the same computer as the instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]. However, to run the [!INCLUDE[tsql](../../includes/tsql-md.md)] debugger when connected to a remote instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)], program and port rules in the Windows Firewall must be enabled on both computers. These rules may be created by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] setup. If you get errors attempting to open a remote debugging session, ensure the following firewall rules are defined on your computer.

Use the **Windows Firewall with Advanced Security** application to manage the firewall rules. In both [!INCLUDE[win7](../../includes/win7-md.md)] and [!INCLUDE[winserver2008r2](../../includes/winserver2008r2-md.md)], open **Control Panel**, open **Windows Firewall**, and select **Advanced settings**. In [!INCLUDE[winserver2008r2](../../includes/winserver2008r2-md.md)] you can also open **Service Manager**, expand **Configuration** in the left pane, and expand **Windows Firewall with Advanced Security**.

> [!CAUTION]
>  Enabling rules in the Windows Firewall may expose your computer to security threats that the firewall is designed to block. Enabling rules for remote debugging unblocks the ports and programs listed in this topic.

## Firewall Rules on the Server
On the computer that is running the instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)], use **Windows Firewall with Advanced Security** to specify the following information:

-   Add an inbound program rule for sqlservr.exe. You must have a rule for each instance that needs to support remote debugging sessions.

    1.  In **Windows Firewall with Advanced Security**, in the left pane, right-click **Inbound Rules**, and then select **New Rule** in the action pane.

    2.  In the **Rule Type** dialog, select **Program**, and then click **Next**.

    3.  In the **Program** dialog, select **This program path:** and enter the full path to sqlservr.exe for this instance. By default, sqlservr.exe is installed in C:\Program Files\Microsoft SQL Server\MSSQL13.*InstanceName*\MSSQL\Binn, where *InstanceName* is MSSQLSERVER for the default instance, and the instance name for any named instance.

    4.  In the **Action** dialog, select **Allow the connection**, and click **Next**.

    5.  In the **Profile** dialog, select any profiles that describe the computer connection environment when you want to open a debugging session with the instance, and click **Next**.

    6.  In the **Name** dialog, type a name and description for this rule and click **Finish**.

    7.  In the **Inbound Rules** list, right click the rule you created, and then select **Properties** in the action pane.

    8.  Select the **Protocols and Ports** tab.

    9. Select **TCP** in the **Protocol type:** box, select **RPC Dynamic Ports** in the **Local port:** box, click **Apply**, and then click **OK**.

-   Add an inbound program rule for svchost.exe to enable DCOM communications from remote debugger sessions.

    1.  In **Windows Firewall with Advanced Security**, in the left pane, right-click **Inbound Rules**, and then select **New Rule** in the action pane.

    2.  In the **Rule Type** dialog, select **Program**, and then click **Next**.

    3.  In the **Program** dialog, select **This program path:** and enter the full path to svchost.exe. By default, svchost.exe is installed in %systemroot%\System32\svchost.exe.

    4.  In the **Action** dialog, select **Allow the connection**, and click **Next**.

    5.  In the **Profile** dialog, select any profiles that describe the computer connection environment when you want to open a debugging session with the instance, and click **Next**.

    6.  In the **Name** dialog, type a name and description for this rule and click **Finish**.

    7.  In the **Inbound Rules** list, right click the rule you created, and then select **Properties** in the action pane.

    8.  Select the **Protocols and Ports** tab.

    9. Select **TCP** in the **Protocol type:** box, select **RPC Endpoint Mapper** in the **Local port:** box, click **Apply**, and then click **OK**.

-   If the domain policy requires network communications to be done through IPsec, you must also add inbound rules opening UDP port 4500 and UDP port 500.

## Firewall Rules on the Client

On the computer that is running the [!INCLUDE[ssDE](../../includes/ssde-md.md)] Query Editor, the SQL Server setup or [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)] setup may have configured the Windows Firewall to allow remote debugging.

If you get errors attempting to open a remote debugging session, you can manually configure the program and port exceptions by using **Windows Firewall with Advanced Security** to configure firewall rules:

-   Add a program entry for svchost:

    1.  In **Windows Firewall with Advanced Security**, in the left pane, right-click **Inbound Rules**, and then select **New Rule** in the action pane.

    2.  In the **Rule Type** dialog, select **Program**, and then click **Next**.

    3.  In the **Program** dialog, select **This program path:** and enter the full path to svchost.exe. By default, svchost.exe is installed in %systemroot%\System32\svchost.exe.

    4.  In the **Action** dialog, select **Allow the connection**, and click **Next**.

    5.  In the **Profile** dialog, select any profiles that describe the computer connection environment when you want to open a debugging session with the instance, and click **Next**.

    6.  In the **Name** dialog, type a name and description for this rule and click **Finish**.

    7.  In the **Inbound Rules** list, right click the rule you created, and then select **Properties** in the action pane.

    8.  Select the **Protocols and Ports** tab.

    9. Select **TCP** in the **Protocol type:** box, select **RPC Endpoint Mapper** in the **Local port:** box, click **Apply**, and then click **OK**.

-   Add a program entry for the application hosting the [!INCLUDE[ssDE](../../includes/ssde-md.md)] Query Editor. If you need to open remote debugging sessions from both [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] and [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)] on the same computer, you must add a program rule for both:

    1.  In **Windows Firewall with Advanced Security**, in the left pane, right-click **Inbound Rules**, and then select **New Rule** in the action pane.

    2.  In the **Rule Type** dialog, select **Program**, and then click **Next**.

    3.  In the **Program** dialog, select **This program path:** and enter one of these three values.

        -   For [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], enter the full path to ssms.exe. By default, ssms.exe is installed in C:\Program Files (x86)\Microsoft SQL Server\130\Tools\Binn\Management Studio.

        -   For [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)] enter the full path to devenv.exe:

            1.  By default, the devenv.exe for Visual Studio 2010 is in C:\Program Files (x86)\Microsoft Visual Studio 10.0\Common7\IDE.

            2.  By default, the devenv.exe for Visual Studio 2012 is in C:\Program Files (x86)\Microsoft Visual Studio 11.0\Common7\IDE

            3.  You can find the path to ssms.exe from the shortcut you use to launch [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. You can find the path to devenv.exe from the shortcut you use to launch [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)]. Right click the shortcut and select **Properties**. The executable and path are listed in the **Target** box.

    4.  In the **Action** dialog, select **Allow the connection**, and click **Next**.

    5.  In the **Profile** dialog, select any profiles that describe the computer connection environment when you want to open a debugging session with the instance, and click **Next**.

    6.  In the **Name** dialog, type a name and description for this rule and click **Finish**.

    7.  In the **Inbound Rules** list, right click the rule you created, and then select **Properties** in the action pane.

    8.  Select the **Protocols and Ports** tab.

    9. Select **TCP** in the **Protocol type:** box, select **RPC Dynamic Ports** in the **Local port:** box, click **Apply**, and then click **OK**.

## Requirements for Starting the Debugger

All attempts to start the [!INCLUDE[tsql](../../includes/tsql-md.md)] debugger must also meet the following requirements:

* [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)] must be running under a Windows account that is a member of the sysadmin fixed server roll.

* The [!INCLUDE[ssDE](../../includes/ssde-md.md)] Query Editor window must be connected by using either a Windows Authentication or [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication login that is a member of the sysadmin fixed server role.

* The [!INCLUDE[ssDE](../../includes/ssde-md.md)] Query Editor window must be connected to an instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] from [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] Service Pack 2 (SP2) or later. You cannot run the debugger when the Query Editor window is connected to an instance that is in single-user mode.

* The server needs to communicate back to the client via RPC. The account under which SQL Server service is running should have authenticate permissions to the client

## See Also

- [Transact-SQL Debugger](../../relational-databases/scripting/transact-sql-debugger.md)
- [Run the Transact-SQL Debugger](../../relational-databases/scripting/run-the-transact-sql-debugger.md)
- [Step Through Transact-SQL Code](../../relational-databases/scripting/step-through-transact-sql-code.md)
- [Transact-SQL Debugger Information](../../relational-databases/scripting/transact-sql-debugger-information.md)
- [Database Engine Query Editor &#40;SQL Server Management Studio&#41;](../../relational-databases/scripting/database-engine-query-editor-sql-server-management-studio.md)