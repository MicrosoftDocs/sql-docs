---
title: "Lesson 2: Connecting from Another Computer"
description: "Lesson 2: Connecting from Another Computer"
author: MashaMSFT
ms.author: mathoma
ms.reviewer: maghan, randolphwest
ms.date: 01/08/2024
ms.service: sql
ms.topic: conceptual
---

# Lesson 2: Connect from another computer

[!INCLUDE [sqlserver](../includes/applies-to-version/sqlserver.md)]

To enhance security, the [!INCLUDE [ssDE](../includes/ssde-md.md)] of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] Developer, Express, and Evaluation editions can't be accessed from another computer when initially installed. This lesson shows you how to enable the protocols, configure the ports, and configure the Windows Firewall for connecting from other computers.

This lesson contains the following tasks:

- [Enabling Protocols](#protocols)
- [Configuring a Fixed Port](#port)
- [Opening Ports in the Firewall](#firewall)
- [Connecting to the Database Engine from Another Computer](#otherComp)
- [Connecting Using the SQL Server Browser Service](#browser)

## <a id="protocols"></a> Enable protocols

To enhance security, [!INCLUDE [ssExpress](../includes/ssexpress-md.md)], Developer, and Evaluation editions install with only limited network connectivity. Connections to the [!INCLUDE [ssDE](../includes/ssde-md.md)] can be made from tools that are running on the same computer but not from other computers if you're planning to do your development work on the same computer as the [!INCLUDE [ssDE](../includes/ssde-md.md)], you don't have to enable additional protocols. [!INCLUDE [ssManStudio](../includes/ssmanstudio-md.md)] connects to the [!INCLUDE [ssDE](../includes/ssde-md.md)] by using the shared memory protocol. This protocol is already enabled.

If you plan to connect to the [!INCLUDE [ssDE](../includes/ssde-md.md)] from another computer, you must enable a protocol, such as TCP/IP.

#### How to enable TCP/IP connections from another computer

1. On the **Start** menu, point to **All Programs**, point to [!INCLUDE [ssCurrentUI](../includes/sscurrentui-md.md)], point to **Configuration Tools**, and then select **SQL Server Configuration Manager**.

   > [!NOTE]  
   > Check to see if you have both 32-bit and 64-bit options available.

   | Version | Path |
   | --- | --- |
   | [!INCLUDE [ssSQL22](../includes/sssql22-md.md)] | `C:\Windows\SysWOW64\SQLServerManager16.msc` |
   | [!INCLUDE [ssSQL19](../includes/sssql19-md.md)] | `C:\Windows\SysWOW64\SQLServerManager15.msc` |
   | [!INCLUDE [ssSQL17](../includes/sssql17-md.md)] | `C:\Windows\SysWOW64\SQLServerManager14.msc` |
   | [!INCLUDE [ssSQL16](../includes/sssql16-md.md)] | `C:\Windows\SysWOW64\SQLServerManager13.msc` |
   | [!INCLUDE [ssSQL14](../includes/sssql14-md.md)] | `C:\Windows\SysWOW64\SQLServerManager12.msc` |
   | [!INCLUDE [ssSQL11](../includes/sssql11-md.md)] | `C:\Windows\SysWOW64\SQLServerManager11.msc` |

1. In **SQL Server Configuration Manager**, expand **SQL Server Network Configuration**, and then select **Protocols for** *\<InstanceName\>*.

   The default (unnamed) instance is listed as `MSSQLSERVER`. If you installed a named instance, the name you provided is listed. [!INCLUDE [ssexpress-md](../includes/ssexpress-md.md)] installs as `SQLEXPRESS`, unless you changed the name during setup.

1. In the list of protocols, right-click the protocol you want to enable (**TCP/IP**), and then select **Enable**.

   > [!NOTE]  
   > Restart the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] service after you make changes to network protocols; however, this is completed in the next task.

## <a id="port"></a> Configure a fixed port

To enhance security, Windows and Windows Server turn on the Windows Firewall. When you want to connect to this instance from another computer, you must open a communication port in the firewall. The default instance of the [!INCLUDE [ssDE](../includes/ssde-md.md)] listens on port 1433; therefore, you don't have to configure a fixed port. However, named instances, including [!INCLUDE [ssExpress](../includes/ssexpress-md.md)] listen on dynamic ports. Before you can open a port in the firewall, you must first configure the [!INCLUDE [ssDE](../includes/ssde-md.md)] to listen on a specific port known as a fixed port or a static port; otherwise, the [!INCLUDE [ssDE](../includes/ssde-md.md)] might listen on a different port each time it starts. For more information about firewalls, the default Windows Firewall settings, and a description of the TCP ports that affect the Database Engine, Analysis Services, Reporting Services, and Integration Services, see [Configure the Windows Firewall to allow SQL Server access](../sql-server/install/configure-the-windows-firewall-to-allow-sql-server-access.md).

> [!NOTE]  
> Port number assignments are managed by the Internet Assigned Numbers Authority and are listed at [https://www.iana.org](https://go.microsoft.com/fwlink/?LinkId=48844). Port numbers should be assigned from numbers 49152 through 65535.

### Configure SQL Server to listen on a specific port

1. In [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] Configuration Manager, expand **SQL Server Network Configuration**, and then select the server instance you want to configure.

1. In the right pane, double-click **TCP/IP**.

1. In the **TCP/IP Properties** dialog box, select the **IP Addresses** tab.

1. In the **TCP Port** box of the **IP All** section, type an available port number. For this tutorial, we use `49172`.

1. Select **OK** to close the dialog box, and select **OK** to the warning that the service must be restarted.

1. In the left pane, select **SQL Server Services**.

1. In the right pane, right-click the instance of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)], then select **Restart**. When the [!INCLUDE [ssDE](../includes/ssde-md.md)] restarts, it listens on port `49172`.

## <a id="firewall"></a> Open ports in the firewall

Firewall systems help prevent unauthorized access to computer resources. To connect to [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] from another computer when a firewall is on, you must open a port in the firewall.

> [!IMPORTANT]  
> Opening firewall ports can expose your server to malicious attacks. Be sure to understand firewall systems before opening ports. For more information, see [Security Considerations for a SQL Server Installation](../sql-server/install/security-considerations-for-a-sql-server-installation.md).

After you configure the [!INCLUDE [ssDE](../includes/ssde-md.md)] to use a fixed port, follow the following instructions to open that port in your Windows Firewall. (You don't have to configure a fixed port for the default instance, because it defaults to TCP port 1433.)

### Open a port in the Windows Firewall for TCP access (Windows 7)

1. On the **Start** menu, select **Run**, type **WF.msc**, and then select **OK**.

1. In **Windows Firewall with Advanced Security**, in the left pane, right-click **Inbound Rules**, and then select **New Rule** in the action pane.

1. In the **Rule Type** dialog box, select **Port**, and then select **Next**.

1. In the **Protocol and Ports** dialog box, select **TCP**. Select **Specific local ports**, and then type the port number of the instance of the [!INCLUDE [ssDE](../includes/ssde-md.md)]. Type 1433 for the default instance. Type **49172** if you're configuring a named instance and configuring a fixed port in the previous task. Select **Next**.

1. In the **Action** dialog box, select **Allow the connection**, and then select **Next**.

1. In the **Profile** dialog box, select any profiles that describe the computer connection environment when you want to connect to the [!INCLUDE [ssDE](../includes/ssde-md.md)], and then select **Next**.

1. In the **Name** dialog box, type a name and description for this rule, and then select **Finish**.

### Open a port in the Windows Firewall for TCP access (Windows 10)

To open a port in the Windows Firewall for TCP access on a Windows 10 computer, follow these steps:

1. Access Windows Firewall Settings:

   - Select the **Windows key** on your keyboard or the **Windows icon** in the taskbar to open the Start menu.

1. Type `Windows Security`:

   - In the Start menu search bar, type *Windows Security* and select **Enter**. This opens the Windows Security app.

1. Open Windows Security Firewall & Network Protection:

   - Select **Firewall & network protection** in the Windows Security app in the left sidebar.

1. Select **Allow an app through firewall**:

   - Under **Firewall & network protection**, select **Allow an app through firewall**.

1. Change settings (administrator permission):

   - You might need administrator permission to make changes. Select the **Change settings** button if prompted and provide your admin credentials.

1. Find the program or port:

   - In the **Allowed apps and features** section, scroll down to find the program or port you want to open. If you're opening a port for a specific application, look for the application in the list. If opening a custom port, you need to create a rule, otherwise skip to step 12.

1. Create a new rule (for custom ports):

   - You must create a new rule if the program or port you want to open isn't listed. Select **Allow another app...** or **Allow another program...** depending on your specific requirement.

1. Choose the program or port:

   - If you're opening a port, choose **Ports** and specify the port number and whether it's TCP or UDP. If you're allowing an application, browse to the executable file of the application.

1. Name the rule:

   - Give your rule a name so you can identify it quickly.

1. Specify action:

   - Choose **Allow the connection** to open the port for TCP access.

1. Save the rule:

   - Select **Next** and then **Finish** to create the rule.

1. Verify the new rule:

   - In the **Allowed apps and features** section, ensure the newly created rule is listed with the desired port or program and is enabled.

1. Close Windows Security:

   - Close the Windows Security app.

1. Test the Port Access:

   - To ensure the port is open, you can use a network utility or application that relies on the specific port to see if it can establish a connection.

Following these steps, you can open a specific port in the Windows Firewall for TCP access on your Windows 10 computer. Remember to exercise caution when modifying firewall settings, as it can affect the security of your system. Only open ports when necessary and for trusted applications or services.

> [!NOTE]
> For more information about configuring the firewall including instructions for [!INCLUDE [winvista](../includes/winvista-md.md)], see [Configure a Windows Firewall for Database Engine Access](../database-engine/configure-windows/configure-a-windows-firewall-for-database-engine-access.md). For more information about the default Windows firewall settings, and a description of the TCP ports that affect the Database Engine, Analysis Services, Reporting Services, and Integration Services, see [Configure the Windows Firewall to allow SQL Server access](../sql-server/install/configure-the-windows-firewall-to-allow-sql-server-access.md).

## <a id="otherComp"></a> Connect to the Database Engine from another computer

Now that you have configured the [!INCLUDE [ssDE](../includes/ssde-md.md)] to listen on a fixed port, and have opened that port in the firewall, you can connect to [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] from another computer.

With [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] Browser service running on the server computer, when the firewall has opened UDP port 1434, the connection can be made by using the computer name and instance name. To enhance security, our example doesn't use the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] Browser service.

### Connect to the Database Engine from another computer

1. On a second computer that contains the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] client tools, log in with an account authorized to connect to [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)], and open [!INCLUDE [ssManStudio](../includes/ssmanstudio-md.md)].

1. In the **Connect to Server** dialog box, confirm **Database Engine** in the **Server type** box.

1. In the **Server name** box, type `tcp:` to specify the protocol, followed by the computer name, a comma, and the port number. To connect to the default instance, port 1433 is implied and can be omitted; therefore, type `tcp:<computer_name>`, where `<computer_name>` is the name of the computer. In our example for a named instance, type `tcp:<computer_name>,49172`.

   If you omit `tcp:` from the **Server name** box, then the client attempts all enabled protocols, in the order specified in the client configuration. For more information, see [Connect to the Database Engine](../sql-server/connect-to-database-engine.md).

   If an attempt is made to establish a connection with the instance name while connecting to the remote server, the [SQL Server Browser](../tools/configuration-manager/sql-server-browser-service.md) service must be running on the remote server. Instance name port mapping doesn't work if the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] Browser service is not running.

1. In the **Authentication** box, confirm **Windows Authentication**, and then select **Connect**.

## <a id="browser"></a> Connect using the SQL Server Browser Service

The [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] Browser service listens for incoming requests for [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] resources and provides information about [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] instances installed on the computer. When the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] Browser service is running, and users can connect to named instances by providing the computer name and instance name instead of the computer name and port number. Because [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] Browser receives unauthenticated UDP requests, it isn't always turned on during setup. For a description of the service and an explanation of when it turns on, see [SQL Server Browser Service (Database Engine and SSAS)](../database-engine/configure-windows/sql-server-browser-service-database-engine-and-ssas.md).

To use the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] Browser, you must follow the same steps as before and open UDP port 1434 in the firewall.

This concludes this brief tutorial on basic connectivity.

## Return to tutorials portal

[Tutorial: Getting Started with the Database Engine](tutorial-getting-started-with-the-database-engine.md)
