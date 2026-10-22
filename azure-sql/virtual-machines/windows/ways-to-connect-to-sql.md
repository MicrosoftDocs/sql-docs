---
title: "Connect to a SQL Server Virtual Machine "
description: Learn how to connect to your SQL Server virtual machine on Azure. The scenarios differ depending on the networking configuration and the location of the client.
author: dplessMSFT
ms.author: dpless
ms.reviewer: vanto
ms.date: 01/23/2026
ms.service: azure-vm-sql-server
ms.subservice: management
ms.topic: how-to
ms.custom:
  - sfi-image-nochange
tags: azure-resource-manager
---
# Connect to a SQL Server virtual machine on Azure

[!INCLUDE [appliesto-sqlvm](../../includes/appliesto-sqlvm.md)]

## Overview

This article describes how to connect to SQL Server on Azure Virtual Machines (VMs). It covers some [general connectivity scenarios](#connection-scenarios). If you need to troubleshoot or configure connectivity outside of the portal, see the [manual configuration](#manual) at the end of this article.

If you would rather have a full walkthrough of both provisioning and connectivity, see [Provision a SQL Server virtual machine on Azure](create-sql-vm-portal.md).

## Connection scenarios

The way a client connects to a SQL Server VM differs depending on the location of the client and the networking configuration.

If you provision a SQL Server VM in the Azure portal, you have the option of specifying the type of **SQL connectivity**.

:::image type="content" source="./media/ways-to-connect-to-sql/sql-vm-portal-connectivity.png" alt-text="Screenshot showing the public SQL connectivity option during provisioning.":::

Your options for connectivity include:

| Option | Description |
| --- | --- |
| **Public** | Connect to SQL Server over the internet. |
| **Private** | Connect to SQL Server in the same virtual network. |
| **Local** | Connect to SQL Server locally on the same virtual machine. |

The following sections explain the **Public** and **Private** options in more detail.

## Connect to SQL Server over the internet

If you want to connect to your SQL Server database engine from the internet, select **Public** for the **SQL connectivity** type in the portal during provisioning. The portal automatically does the following steps:

- Enables the TCP/IP protocol for SQL Server.
- Configures a firewall rule to open the SQL Server TCP port (default 1433).
- Enables SQL Server authentication, required for public access.
- Configures the network security group on the VM to all TCP traffic on the SQL Server port.

> [!IMPORTANT]  
> The virtual machine images for the SQL Server Developer and Express editions don't automatically enable the TCP/IP protocol. For Developer and Express editions, you must use SQL Server Configuration Manager to [manually enable the TCP/IP protocol](#manualtcp) after creating the VM.

Any client with internet access can connect to the SQL Server instance by specifying either the public IP address of the virtual machine or any DNS label assigned to that IP address. If the SQL Server port is 1433, you don't need to specify it in the connection string. The following connection string connects to a SQL VM with a DNS label of `sqlvmlabel.eastus.cloudapp.azure.com` using SQL authentication (you could also use the public IP address).

```text
Server=sqlvmlabel.eastus.cloudapp.azure.com;Integrated Security=false;User ID=<login_name>;Password=<password>
```

Although this string enables connectivity for clients over the internet, this doesn't imply that anyone can connect to your SQL Server instance. Outside clients have to use the correct username and password. However, for additional security, you can avoid the well-known port 1433. For example, if you were to configure SQL Server to listen on port 1500 and establish proper firewall and network security group rules, you could connect by appending the port number to the server name. The following example alters the previous one by adding a custom port number, **1500**, to the server name:

```text
Server=sqlvmlabel.eastus.cloudapp.azure.com,1500;Integrated Security=false;User ID=<login_name>;Password=<password>
```

> [!NOTE]  
> When you query SQL Server on Azure VM over the internet, all outgoing data from the Azure datacenter is subject to normal [pricing on outbound data transfers](https://azure.microsoft.com/pricing/details/data-transfers/).

## Connect to SQL Server within a virtual network

When you choose **Private** for the **SQL connectivity** type in the portal, Azure configures most of the settings identical to **Public**. The one difference is that there's no network security group rule to allow outside traffic on the SQL Server port (default 1433).

> [!IMPORTANT]  
> The virtual machine images for the SQL Server Developer and Express editions don't automatically enable the TCP/IP protocol. For Developer and Express editions, you must use SQL Server Configuration Manager to [manually enable the TCP/IP protocol](#manualtcp) after creating the VM.

Private connectivity is often used in conjunction with a [virtual network](/azure/virtual-network/virtual-networks-overview), which enables several scenarios. You can connect VMs in the same virtual network, even if those VMs exist in different resource groups. And with a [site-to-site VPN](/azure/vpn-gateway/tutorial-site-to-site-portal), you can create a hybrid architecture that connects VMs with on-premises networks and machines.

Virtual networks also enable you to join your Azure VMs to a domain. This is the only way to use Windows authentication to SQL Server. The other connection scenarios require SQL authentication with user names and passwords.

Assuming that you've configured DNS in your virtual network, you can connect to your SQL Server instance by specifying the SQL Server VM computer name in the connection string. The following example also assumes that Windows authentication has been configured, and the user has been granted access to the SQL Server instance.

```text
Server=sqlvm;Integrated Security=true
```

<a id="manualtcp"></a>

## Enable TCP/IP for Developer and Express editions

The portal enables TCP/IP for most SQL Server images. However, the portal does not automatically enable TCP/IP for Developer and Express edition images. Use SQL Server Configuration Manager to enable TCP/IP after creating the VM.

First, connect to the SQL Server virtual machine with Bastion.

[!INCLUDE [Connect to SQL Server VM with Bastion](../../includes/virtual-machines-sql-server-remote-desktop-connect.md)]

Next, enable the TCP/IP protocol with **SQL Server Configuration Manager**.

[!INCLUDE [enable TCP/IP protocol with SSCM](../../includes/virtual-machines-sql-server-connection-tcp-protocol.md)]

## Connect with SSMS

The following steps show how to create an optional DNS label for your Azure VM, and then connect with SQL Server Management Studio (SSMS).

[!INCLUDE [Connect to SQL Server in a VM Resource Manager](../../includes/virtual-machines-sql-server-connection-steps-resource-manager.md)]

<a id="manual"></a>

## Manual configuration and troubleshooting

Although the portal provides options to automatically configure connectivity, it's useful to know how to manually configure connectivity. Understanding the requirements can also aid troubleshooting.

The following table lists the requirements to connect to SQL Server on Azure VM.

| Requirement | Description |
| --- | --- |
| [Enable SQL Server authentication mode](/sql/database-engine/configure-windows/change-server-authentication-mode#use-ssms) | SQL Server authentication is needed to connect to the VM remotely unless you have configured Active Directory on a virtual network. |
| [Create a SQL login](/sql/relational-databases/security/authentication-access/create-a-login) | If you're using SQL authentication, you need a SQL login with a user name and password that also has permissions to your target database. |
| [Enable TCP/IP protocol](#manualtcp) | SQL Server must allow connections over TCP. |
| [Enable firewall rule for the SQL Server port](/sql/database-engine/configure-windows/configure-a-windows-firewall-for-database-engine-access) | The firewall on the VM must allow inbound traffic on the SQL Server port (default 1433). |
| [Create a network security group rule for TCP 1433](/azure/virtual-network/manage-network-security-group#create-a-security-rule) | You must allow the VM to receive traffic on the SQL Server port (default 1433) if you want to connect over the internet. Local and virtual-network-only connections don't require this. When configuring connectivity manually for internet access, creating the NSG rule is the only portal step required to allow TCP 1433; other configuration (SQL auth, firewall, TCP/IP) must be done on the VM. |

> [!TIP]  
> The steps in the preceding table are done for you when you configure connectivity in the portal. Use these steps only to confirm your configuration or to set up connectivity manually for SQL Server.

<a id="connect-to-a-sql-server-on-azure-vm-using-azure-ad"></a>

## Connect to a SQL Server on Azure VM using Microsoft Entra ID

[Enable authentication](configure-azure-ad-authentication-for-sql-vm.md) with Microsoft Entra ID ([formerly Azure Active Directory](/entra/fundamentals/new-name)) for your SQL Server on Azure Virtual Machines via the Azure portal. SQL Server with Microsoft Entra authentication is only supported on [!INCLUDE [sssql22-md](../../../docs/includes/sssql22-md.md)] and later versions.

## Related content

- [Provision SQL Server on Azure VM (Azure portal)](create-sql-vm-portal.md)
- [What is SQL Server on Azure Windows Virtual Machines?](sql-server-on-azure-vm-iaas-what-is-overview.md)
