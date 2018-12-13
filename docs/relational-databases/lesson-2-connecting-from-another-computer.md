---
title: "Lesson 2: Connecting from Another Computer | Microsoft Docs"
ms.custom: ""
ms.date: "03/08/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: 
ms.topic: conceptual
ms.assetid: fd4ddeb8-0cb6-441b-9704-03575c07020f
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Lesson 2: Connecting from Another Computer
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
To enhance security, the [!INCLUDE[ssDE](../includes/ssde-md.md)] of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Developer, Express, and Evaluation editions cannot be accessed from another computer when initially installed. This lesson shows you how to enable the protocols, configure the ports, and configure the Windows Firewall for connecting from other computers.  
  
This lesson contains the following tasks:  
  
-   [Enabling Protocols](#protocols)  
  
-   [Configuring a Fixed Port](#port)  
  
-   [Opening Ports in the Firewall](#firewall)  
  
-   [Connecting to the Database Engine from Another Computer](#otherComp)  
  
-   [Connecting Using the SQL Server Browser Service](#browser)  
  
## <a name="protocols"></a>Enabling Protocols  
To enhance security, [!INCLUDE[ssExpress](../includes/ssexpress-md.md)], Developer, and Evaluation install with only limited network connectivity. Connections to the [!INCLUDE[ssDE](../includes/ssde-md.md)] can be made from tools that are running on the same computer, but not from other computers. If you are planning to do your development work on the same computer as the [!INCLUDE[ssDE](../includes/ssde-md.md)], you do not have to enable additional protocols. [!INCLUDE[ssManStudio](../includes/ssmanstudio-md.md)] will connect to the [!INCLUDE[ssDE](../includes/ssde-md.md)] by using the shared memory protocol. This protocol is already enabled.  
  
If you plan to connect to the [!INCLUDE[ssDE](../includes/ssde-md.md)] from another computer, you must enable a protocol, such as TCP/IP.  
  
#### How to enable TCP/IP connections from another computer  
  
1.  On the **Start** menu, point to **All Programs**, point to [!INCLUDE[ssCurrentUI](../includes/sscurrentui-md.md)], point to **Configuration Tools**, and then click **SQL Server Configuration Manager**.  
  
    > [!NOTE]  
    > You might have both 32 bit and 64 bit options available.  
  
    > [!NOTE]  
    > Because [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Configuration Manager is a snap-in for the [!INCLUDE[msCoName](../includes/msconame-md.md)] Management Console program and not a stand-alone program, [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Configuration Manager does not appear as an application in newer versions of Windows. The file name contains a number representing the version number of the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. To open Configuration Manager from the Run command, here are the paths to the last four versions when Windows is installed on the C drive.  
  
    |||  
    |-|-|  
    |[!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] 2016|C:\Windows\SysWOW64\SQLServerManager13.msc|  
    |[!INCLUDE[ssSQL14](../includes/sssql14-md.md)]|C:\Windows\SysWOW64\SQLServerManager12.msc|  
    |[!INCLUDE[ssSQL11](../includes/sssql11-md.md)]|C:\Windows\SysWOW64\SQLServerManager11.msc|  
    |[!INCLUDE[ssKatmai](../includes/sskatmai-md.md)]|C:\Windows\SysWOW64\SQLServerManager10.msc|  
  
2.  In **SQL Server Configuration Manager**, expand **SQL Server Network Configuration**, and then click **Protocols for** _<InstanceName>_.  
  
    The default instance (an unnamed instance) is listed as **MSSQLSERVER**. If you installed a named instance, the name you provided is listed. [!INCLUDE[ssExpressEd11](../includes/ssexpressed11-md.md)] installs as **SQLEXPRESS**, unless you changed the name during setup.  
  
3.  In the list of protocols, right-click the protocol you want to enable (**TCP/IP**), and then click **Enable**.  
  
    > [!NOTE]  
    > You must restart the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] service after you make changes to network protocols; however, this is completed in the next task.  
  
## <a name="port"></a>Configuring a Fixed Port  
To enhance security, Windows Server 2008, [!INCLUDE[wiprlhlong](../includes/wiprlhlong-md.md)], and Windows 7 all turn on the Windows Firewall. When you want to connect to this instance from another computer, you must open a communication port in the firewall. The default instance of the [!INCLUDE[ssDE](../includes/ssde-md.md)] listens on port 1433; therefore, you do not have to configure a fixed port. However, named instances including [!INCLUDE[ssExpress](../includes/ssexpress-md.md)] listen on dynamic ports. Before you can open a port in the firewall, you must first configure the [!INCLUDE[ssDE](../includes/ssde-md.md)] to listen on a specific port known as a fixed port or a static port; otherwise, the [!INCLUDE[ssDE](../includes/ssde-md.md)] might listen on a different port each time it is started. For more information about firewalls, the default Windows firewall settings, and a description of the TCP ports that affect the Database Engine, Analysis Services, Reporting Services, and Integration Services, see [Configure the Windows Firewall to Allow SQL Server Access](../sql-server/install/configure-the-windows-firewall-to-allow-sql-server-access.md).  
  
> [!NOTE]  
> Port number assignments are managed by the Internet Assigned Numbers Authority and are listed at [https://www.iana.org](https://go.microsoft.com/fwlink/?LinkId=48844). Port numbers should be assigned from numbers 49152 through 65535.  
  
#### Configure SQL Server to listen on a specific port  
  
1.  In [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Configuration Manager, expand **SQL Server Network Configuration**, and then click on the server instance you want to configure.  
  
2.  In the right pane, double-click **TCP/IP**.  
  
3.  In the **TCP/IP Properties** dialog box, click the **IP Addresses** tab.  
  
4.  In the **TCP Port** box of the **IPAll** section, type an available port number. For this tutorial, we will use **49172**.  
  
5.  Click **OK** to close the dialog box, and click **OK** to the warning that the service must be restarted.  
  
6.  In the left pane, click **SQL Server Services**.  
  
7.  In the right pane, right-click the instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], and then click **Restart**. When the [!INCLUDE[ssDE](../includes/ssde-md.md)] restarts, it will listen on port **49172**.  
  
## <a name="firewall"></a>Opening Ports in the Firewall  
Firewall systems help prevent unauthorized access to computer resources. To connect to [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] from another computer when a firewall is on, you must open a port in the firewall.  
  
> [!IMPORTANT]  
> Opening ports in your firewall can leave your server exposed to malicious attacks. Be sure to understand firewall systems before opening ports. For more information, see [Security Considerations for a SQL Server Installation](../sql-server/install/security-considerations-for-a-sql-server-installation.md).  
  
After you configure the [!INCLUDE[ssDE](../includes/ssde-md.md)] to use a fixed port, follow the following instructions to open that port in your Windows Firewall. (You do not have to configure a fixed port for the default instance, because it is already fixed on TCP port 1433.)  
  
#### To open a port in the Windows firewall for TCP access (Windows 7)  
  
1.  On the **Start** menu, click **Run**, type **WF.msc**, and then click **OK**.  
  
2.  In **Windows Firewall with Advanced Security**, in the left pane, right-click **Inbound Rules**, and then click **New Rule** in the action pane.  
  
3.  In the **Rule Type** dialog box, select **Port**, and then click **Next**.  
  
4.  In the **Protocol and Ports** dialog box, select **TCP**. Select **Specific local ports**, and then type the port number of the instance of the [!INCLUDE[ssDE](../includes/ssde-md.md)]. Type 1433 for the default instance. Type **49172** if you are configuring a named instance and configured a fixed port in the previous task. Click **Next**.  
  
5.  In the **Action** dialog box, select **Allow the connection**, and then click **Next**.  
  
6.  In the **Profile** dialog box, select any profiles that describe the computer connection environment when you want to connect to the [!INCLUDE[ssDE](../includes/ssde-md.md)], and then click **Next**.  
  
7.  In the **Name** dialog box, type a name and description for this rule, and then click **Finish**.  
  
For more information about configuring the firewall including instructions for [!INCLUDE[wiprlhlong](../includes/wiprlhlong-md.md)], see [Configure a Windows Firewall for Database Engine Access](../database-engine/configure-windows/configure-a-windows-firewall-for-database-engine-access.md). For more information about the default Windows firewall settings, and a description of the TCP ports that affect the Database Engine, Analysis Services, Reporting Services, and Integration Services, see [Configure the Windows Firewall to Allow SQL Server Access](../sql-server/install/configure-the-windows-firewall-to-allow-sql-server-access.md).  
  
## <a name="otherComp"></a>Connecting to the Database Engine from Another Computer  
Now that you have configured the [!INCLUDE[ssDE](../includes/ssde-md.md)] to listen on a fixed port, and have opened that port in the firewall, you can connect to [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] from another computer.  
  
When the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Browser service is running on the server computer, and when the firewall has opened UDP port 1434, the connection can be made by using the computer name and instance name. To enhance security, our example does not use the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Browser service.  
  
#### To connect to the Database Engine from another computer  
  
1.  On a second computer that contains the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] client tools, log in with an account authorized to connect to [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], and open [!INCLUDE[ssManStudio](../includes/ssmanstudio-md.md)].  
  
2.  In the **Connect to Server** dialog box, confirm **Database Engine** in the **Server type** box.  
  
3.  In the **Server name** box, type **tcp:** to specify the protocol, followed by the computer name, a comma, and the port number. To connect to the default instance, the port 1433 is implied and can be omitted; therefore, type **tcp:**_<computer_name>_. In our example for a named instance, type **tcp:**_<computer_name>_**,49172**.  
  
    > [!NOTE]  
    > If you omit **tcp:** from the **Server name** box, then the client will attempt all protocols that are enabled, in the order specified in the client configuration.  
  
4.  In the **Authentication** box, confirm **Windows Authentication**, and then click **Connect**.  
  
## <a name="browser"></a>Connecting Using the SQL Server Browser Service  
The [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Browser service listens for incoming requests for [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] resources and provides information about [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] instances installed on the computer. When the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Browser service is running, users can connect to named instances by providing the computer name and instance name, instead of the computer name and port number. Because [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Browser receives unauthenticated UDP requests, it is not always turned on during setup. For a description of the service and an explanation of when it is turned on, see [SQL Server Browser Service &#40;Database Engine and SSAS&#41;](../database-engine/configure-windows/sql-server-browser-service-database-engine-and-ssas.md).  
  
To use the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Browser, you must follow the same steps as before and open UDP port 1434 in the firewall.  
  
This concludes this brief tutorial on basic connectivity.  
  
## Return to Tutorials Portal  
[Tutorial: Getting Started with the Database Engine](../relational-databases/tutorial-getting-started-with-the-database-engine.md)  
  

