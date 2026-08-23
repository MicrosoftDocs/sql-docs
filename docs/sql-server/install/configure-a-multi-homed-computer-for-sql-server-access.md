---
title: "Configure Multi-Homed Computer for Access"
description: Learn how to configure SQL Server and Windows Firewall to provide for network connections to an instance of SQL Server in a multi-homed environment.
author: rwestMSFT
ms.author: randolphwest
ms.date: 03/31/2025
ms.service: sql
ms.subservice: install
ms.topic: how-to
helpviewer_keywords:
  - "ports [SQL Server], multi-homed computer"
  - "multi-homed computer [SQL Server] configuring ports"
  - "firewall systems [Database Engine], multi-homed computer"
---
# Configure a multi-homed computer for SQL Server access

[!INCLUDE [sql-windows-only](../../includes/applies-to-version/sql-windows-only.md)]

When a server must provide a connection to two or more networks or network subnets, a typical scenario uses a multi-homed computer. Frequently this computer is located in a perimeter network (also known as a screened subnet). This article describes how to configure [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] and Windows Firewall with Advanced Security to provide for network connections to an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] in a multi-homed environment.

> [!NOTE]  
> A multi-homed computer has multiple network adapters or has been configured to use multiple IP addresses for a single network adapter. A dual-homed computer has two network adapters or has been configured to use two IP addresses for a single network adapter.

## Prerequisites

Before you continue in this article, you should be familiar with the information provided in the article [Configure the Windows Firewall to allow SQL Server access](configure-the-windows-firewall-to-allow-sql-server-access.md). This article contains basic information about how [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] components work with the firewall.

**Assumptions for this example:**

- There are two network adapters installed in the computer. One or more of the network adapters can be wireless. You can simulate having two network adapters by using the IP address of one network adapter, and using the loopback IP address (127.0.0.1) as the second network adapter.

- For simplicity, this example uses IPv4 addresses. The same procedures can be performed by using IPv6 addresses.

  > [!NOTE]  
  > IPv4 addresses are a series of four numbers known as octets. Each number is less than 255, separated by periods, such as 127.0.0.1. IPv6 addresses are a series of eight hexadecimal numbers separated by colons, such as fe80:4898:23:3:49a6:f5c1:2452:b994.

- Firewall rules could allow access through a specific port, such as port 1433. Or firewall rules could allow access to the [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)] program (sqlservr.exe). Neither method is better than the other. Because a server in a perimeter network is more vulnerable to attack than servers on an intranet, this article assumes that you want to have more precise control, and individually select the ports that you open. For that reason, this article assumes that you'll configure [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to listen on a fixed port. For more information about the ports that [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] uses, see [Configure the Windows Firewall to allow SQL Server access](configure-the-windows-firewall-to-allow-sql-server-access.md).

- This example configures access to the [!INCLUDE [ssDE](../../includes/ssde-md.md)] by using TCP port 1433. The other ports that are the different [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] components use can be configured by using the same general steps.

**The general steps in this example are as follows:**

- Determine the IP addresses on the computer.

- Configure [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to listen on a specific TCP port.

- Configure Windows Firewall with Advanced Security.

## Optional procedures

If you already know the IP addresses available to your computer and that are used by [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], you can skip these procedures.

### Determine the IP addresses available on the computer

1. On the computer on which [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is installed, select **Start**, select **Run**, type **cmd** and then select **OK**.

1. In the Command Prompt window, type **ipconfig,** and then press ENTER to list the IP addresses available on this computer.

   The **ipconfig** command sometimes lists many possible connections, including connections that are disconnected. The **ipconfig** command can list both IPv4 and IPv6 addresses.

1. Note the IPv4 addresses and IPv6 addresses that are being used. The other information in the list, such as temporary addresses, subnet masks, and default gateways is important information for configuring a TCP/IP network. But this information isn't used in this example.

### Determine the IP addresses and ports used by SQL Server

1. Select **Start**, point to **All Programs**, point to [!INCLUDE [ssCurrentUI](../../includes/sscurrentui-md.md)], point to **Configuration Tools**, and then select **SQL Server Configuration Manager**.

1. In **SQL Server Configuration Manager**, in the console pane, expand **SQL Server Network Configuration**, expand **Protocols for \<instance name>**, and then double-click **TCP/IP**.

1. In the **TCP/IP Properties** dialog box, on the **IP Addresses** tab, several IP addresses appear in the format **IP1**, **IP2**, up to **IPAll**. One of these is for the IP address of the loopback adapter, 127.0.0.1. Additional IP addresses appear for each IP Address configured on the computer.

1. For any IP address if the **TCP Dynamic Ports** dialog box contains `0`, this indicates that the [!INCLUDE [ssDE](../../includes/ssde-md.md)] is listening on dynamic ports. This example uses fixed ports instead of dynamic ports which could change upon restart. Therefore if the **TCP Dynamic Ports** dialog box contains `0`, delete the `0`.

1. Note the TCP port that is listed for each IP address that you want to configure. For this example, assume that both IP addresses are listening on the default port, 1433.

1. If you don't want [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to use some of the available ports, on the **Protocol** tab, change the **Listen All** value to **No**; and on the **IP Addresses** tab, change the **Active** value to **No** for the IP addresses that you don't want to use.

## Configure Windows Firewall with Advanced Security

After you know the IP addresses that the computer uses and the ports that [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] uses, you can create firewall rules, and then configure those rules for specific IP addresses.

### Create a firewall rule

1. On the computer on which [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is installed, connect as an administrator..

1. Select **Start**, select **Run**, type **wf.msc**, and select **OK**.

1. In the **User Account Control** dialog box, select **Continue** to use the Administrator credentials to open the Windows Firewall with Advanced Security snap-in.

1. On the **Overview** page, confirm that the Windows Firewall is enabled.

1. In the left pane, select **Inbound Rules**.

1. Right-click **Inbound Rules**, and then select **New Rule** to open the **New Inbound Rule Wizard**.

1. You could create a rule for the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] program. However, because this example uses a fixed port, select **Port**, and then select **Next**.

1. On the **Protocols and Ports** page, select **TCP**.

1. Select **Specified local ports**. Type the port numbers separated by commas, and then select **Next**. In this example, you'll configure the default port; therefore, enter `1433`.

1. On the **Action** page, review the options. In this example, you aren't using the firewall to force secure connections. Therefore, select **Allow the connection**, and then select **Next**.

   Your environment might require secure connections. If you select one of the secure connections options, you might have to configure a certificate and the **Force Encryption** option. For more information about secure connections, see [Configure SQL Server Database Engine for encrypting connections](../../database-engine/configure-windows/configure-sql-server-encryption.md) and [Configure SQL Server Database Engine for encrypting connections](../../database-engine/configure-windows/configure-sql-server-encryption.md).

1. On the **Profile** page, select one or more profiles for the rule. If you're unfamiliar with firewall profiles, select the **Learn more about profiles** link in the firewall program.

   - If the computer is a server and is available only when it's connected to a domain, select **Domain**, and then select **Next**.

   - If the computer is a mobile computer (for example a laptop), it's likely to use multiple profiles when it connects to different networks. For a mobile computer, you can configure different access capabilities for different profiles. For example, you might allow access when the computer uses the Domain profile but not allow access when it uses the Public profile.

1. On the **Name** page, provide a name and description for the rule, and then select **Finish**.

1. Repeat this procedure to create another rule for each IP address that [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] will use.

After you have created one or more rules, perform the following steps to configure each IP address on the computer to use a rule.

### Configure the firewall rule for a specific IP addresses

1. On the **Inbound Rules** page of the **Windows Firewall with Advanced Security**, right-click the rule that you just created, and then select **Properties**.

1. In the **Rule Properties** dialog box, select the **Scope** tab.

1. In the **Local IP address** area, select **These IP addresses**, and then select **Add**.

1. In the **IP Address** dialog box, select **This IP address or subnet**, and then type one of the IP addresses that you want to configure.

1. Select **OK**.

1. In the **Remote IP address** area, select **These IP addresses**, and then select **Add**.

1. Use the **IP Address** dialog box to configure connectivity for the selected IP address on the computer. You can enable connections from specified IP addresses, ranges of IP addresses, whole subnets, or from certain computers. To configure this option correctly, you must have a good understanding of the network. For information about the network, see the network administrator.

1. To close the **IP Address** dialog box, select **OK**; and then select **OK** to close the **Rule Properties** dialog box.

1. To configure the other IP addresses on a multi-homed computer, repeat this procedure by using another IP address and another rule.

## Related content

- [SQL Server Browser service (Database Engine and SSAS)](../../database-engine/configure-windows/sql-server-browser-service-database-engine-and-ssas.md)
- [Connect to SQL Server through a proxy server (SQL Server Configuration Manager)](../../database-engine/configure-windows/connect-to-sql-server-through-a-proxy-server-sql-server-configuration-manager.md)
