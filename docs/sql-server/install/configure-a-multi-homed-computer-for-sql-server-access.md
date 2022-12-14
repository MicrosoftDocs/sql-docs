---
title: "Configure multi-homed computer for access"
description: Learn how to configure SQL Server and Windows Firewall to provide for network connections to an instance of SQL Server in a multi-homed environment.
ms.custom: "seo-lt-2019"
ms.date: "12/13/2019"
ms.service: sql
ms.reviewer: ""
ms.subservice: install
ms.topic: conceptual
helpviewer_keywords: 
  - "ports [SQL Server], multi-homed computer"
  - "multi-homed computer [SQL Server] configuring ports"
  - "firewall systems [Database Engine], multi-homed computer"
ms.assetid: ba369e5b-7d1f-4544-b7f1-9b098a1e75bc
author: rwestMSFT
ms.author: randolphwest
---
# Configure a Multi-Homed Computer for SQL Server Access
[!INCLUDE [SQL Server -Windows Only](../../includes/applies-to-version/sql-windows-only.md)]

  When a server must provide a connection to two or more networks or network subnets, a typical scenario uses a multi-homed computer. Frequently this computer is located in a perimeter network (also known as DMZ, demilitarized zone, or screened subnet). This article describes how to configure [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and Windows Firewall with Advanced Security to provide for network connections to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in a multi-homed environment.  
  
> [!NOTE]  
>  A multi-homed computer has multiple network adapters or has been configured to use multiple IP addresses for a single network adapter. A dual-homed computer has two network adapters or has been configured to use two IP addresses for a single network adapter.  
  
 Before you continue in this article, you should be familiar with the information provided in the article [Configure the Windows Firewall to Allow SQL Server Access](../../sql-server/install/configure-the-windows-firewall-to-allow-sql-server-access.md). This article contains basic information about how [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] components work with the firewall.  
  
 **Assumptions for this example:**  
  
-   There are two network adapters installed in the computer. One or more of the network adapters can be wireless. You can simulate having two network adapters by using the IP address of one network adapter, and using the loopback IP address (127.0.0.1) as the second network adapter.  
  
-   For simplicity, this example uses IPv4 addresses. The same procedures can be performed by using IPv6 addresses.  
  
    > [!NOTE]  
    >  IPv4 addresses are a series of four numbers known as octets. Each number is less than 255, separated by periods, such as 127.0.0.1. IPv6 addresses are a series of eight hexadecimal numbers separated by colons, such as fe80:4898:23:3:49a6:f5c1:2452:b994.  
  
-   Firewall rules could allow access through a specific port, such as port 1433. Or firewall rules could allow access to the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] program (sqlservr.exe). Neither method is better than the other. Because a server in a perimeter network is more vulnerable to attack than servers on an intranet, this article assumes that you want to have more precise control, and individually select the ports that you open. For that reason, this article assumes that you will configure [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to listen on a fixed port. For more information about the ports that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] uses, see [Configure the Windows Firewall to Allow SQL Server Access](../../sql-server/install/configure-the-windows-firewall-to-allow-sql-server-access.md).  
  
-   This example configures access to the [!INCLUDE[ssDE](../../includes/ssde-md.md)] by using TCP port 1433. The other ports that are the different [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] components use can be configured by using the same general steps.  
  
 **The general steps in this example are as follows:**  
  
-   Determine the IP addresses on the computer.  
  
-   Configure [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to listen on a specific TCP port.  
  
-   Configure Windows Firewall with Advanced Security.  
  
## Optional Procedures  
 If you already know the IP addresses available to your computer and that are used by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you can skip these procedures.  
  
#### To determine the IP addresses available on the computer  
  
1.  On the computer on which [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is installed, click **Start**, click **Run**, type **cmd** and then select **OK**.
  
2.  In the Command Prompt window, type **ipconfig,** and then press ENTER to list the IP addresses available on this computer.  
  
    > [!NOTE]  
    >  The **ipconfig** command sometimes lists many possible connections, including connections that are disconnected. The **ipconfig** command can list both IPv4 and IPv6 addresses.  
  
3.  Note the IPv4 addresses and IPv6 addresses that are being used. The other information in the list, such as temporary addresses, subnet masks, and default gateways is important information for configuring a TCP/IP network. But this information is not used in this example.  
  
#### To determine the IP addresses and ports used by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]  
  
1.  Click **Start**, point to **All Programs**, point to [!INCLUDE[ssCurrentUI](../../includes/sscurrentui-md.md)], point to **Configuration Tools**, and then click **[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager**.  
  
2.  In **[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager**, in the console pane, expand **[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Network Configuration**, expand **Protocols for \<instance name>**, and then double-click **TCP/IP**.  
  
3.  In the **TCP/IP Properties** dialog box, on the **IP Addresses** tab, several IP addresses appear in the format **IP1**, **IP2**, up to **IPAll**. One of these is for the IP address of the loopback adapter, 127.0.0.1. Additional IP addresses appear for each IP Address configured on the computer.  
  
4.  For any IP address if the **TCP Dynamic Ports** dialog box contains **0**, this indicates that the [!INCLUDE[ssDE](../../includes/ssde-md.md)] is listening on dynamic ports. This example uses fixed ports instead of dynamic ports which could change upon restart. Therefore if the **TCP Dynamic Ports** dialog box contains **0**, delete the 0.  
  
5.  Note the TCP port that is listed for each IP address that you want to configure. For this example, assume that both IP addresses are listening on the default port, 1433.  
  
6.  If you do not want [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to use some of the available ports, on the **Protocol** tab, change the **Listen All** value to **No**; and on the **IP Addresses** tab, change the **Active** value to **No** for the IP addresses that you do not want to use.  
  
## Configuring Windows Firewall with Advanced Security  
 After you know the IP addresses that the computer uses and the ports that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] uses, you can create firewall rules, and then configure those rules for specific IP addresses.  
  
#### To create a firewall rule  
  
1.  On the computer on which [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is installed, log on as an administrator..  
  
2.  Click **Start**, click **Run**, type **wf.msc**, and click **OK**.  
  
3.  In the **User Account Control** dialog box, click **Continue** to use the Administrator credentials to open the Windows Firewall with Advanced Security snap-in.  
  
4.  On the **Overview** page, confirm that the Windows Firewall is enabled.  
  
5.  In the left pane, click **Inbound Rules**.  
  
6.  Right-click **Inbound Rules**, and then click **New Rule** to open the **New Inbound Rule Wizard**.  
  
7.  You could create a rule for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] program. However, because this example uses a fixed port, select **Port**, and then click **Next**.  
  
8.  On the **Protocols and Ports** page, select **TCP**.  
  
9. Select **Specified local ports**. Type the port numbers separated by commas, and then click **Next**. In this example, you will configure the default port; therefore, enter **1433**.  
  
10. On the **Action** page, review the options. In this example, you are not using the firewall to force secure connections. Therefore, click **Allow the connection**, and then click **Next**.  
  
    > [!NOTE]  
    >  Your environment might require secure connections. If you select one of the secure connections options, you might have to configure a certificate and the **Force Encryption** option. For more information about secure connections, see [Enable Encrypted Connections to the Database Engine &#40;SQL Server Configuration Manager&#41;](../../database-engine/configure-windows/enable-encrypted-connections-to-the-database-engine.md) and [Enable Encrypted Connections to the Database Engine &#40;SQL Server Configuration Manager&#41;](../../database-engine/configure-windows/enable-encrypted-connections-to-the-database-engine.md).  
  
11. On the **Profile** page, select one or more profiles for the rule. If you are unfamiliar with firewall profiles, click the **Learn more about profiles** link in the firewall program.  
  
    -   If the computer is a server and is available only when it is connected to a domain, select **Domain**, and then click **Next**.  
  
    -   If the computer is a mobile computer (for example a laptop), it is likely to use multiple profiles when it connects to different networks. For a mobile computer, you can configure different access capabilities for different profiles. For example, you might allow access when the computer uses the Domain profile but not allow access when it uses the Public profile.  
  
12. On the **Name** page, provide a name and description for the rule, and then click **Finish**.  
  
13. Repeat this procedure to create another rule for each IP address that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] will use.  
  
 After you have created one or more rules, perform the following steps to configure each IP address on the computer to use a rule.  
  
#### To configure the firewall rule for a specific IP addresses  
  
1.  On the **Inbound Rules** page of the **Windows Firewall with Advanced Security**, right-click the rule that you just created, and then click **Properties**.  
  
2.  In the **Rule Properties** dialog box, select the **Scope** tab.  
  
3.  In the **Local IP address** area, select **These IP addresses**, and then click **Add**.  
  
4.  In the **IP Address** dialog box, select **This IP address or subnet**, and then type one of the IP addresses that you want to configure.  
  
5.  Select **OK**.
  
6.  In the **Remote IP address** area, select **These IP addresses**, and then click **Add**.  
  
7.  Use the **IP Address** dialog box to configure connectivity for the selected IP address on the computer. You can enable connections from specified IP addresses, ranges of IP addresses, whole subnets, or from certain computers. To configure this option correctly, you must have a good understanding of the network. For information about the network, see the network administrator.  
  
8.  To close the **IP Address** dialog box, click **OK**; and then click **OK** to close the **Rule Properties** dialog box.  
  
9. To configure the other IP addresses on a multi-homed computer, repeat this procedure by using another IP address and another rule.  
  
## See Also  
 [SQL Server Browser Service &#40;Database Engine and SSAS&#41;](../../database-engine/configure-windows/sql-server-browser-service-database-engine-and-ssas.md)   
 [Connect to SQL Server Through a Proxy Server &#40;SQL Server Configuration Manager&#41;](../../database-engine/configure-windows/connect-to-sql-server-through-a-proxy-server-sql-server-configuration-manager.md)  
  
  
