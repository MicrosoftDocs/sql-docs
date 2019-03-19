---
title: Use third-party Active Directory providers with SQL Server on Linux
titleSuffix: SQL Server
description: This tutorial provides the configuration steps for Active Directory authentication with third-party providers
author: dylan-MSFT
ms.date: 07/25/2018
ms.author: dygray 
manager: mikehab
ms.topic: conceptual
ms.prod: sql
ms.custom: "sql-linux, seodec18"
ms.technology: linux
helpviewer_keywords: 
  - "Linux, AD authentication"
---
# Use third-party Active Directory providers with SQL Server on Linux

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-linuxonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-linuxonly.md)]

This article explains how to configure a [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] on a Linux host machine with Active Directory authentication when using third-party Active Directory providers. Examples are [PowerBroker Identity Services (PBIS)](https://www.beyondtrust.com/), [One Identity](https://www.oneidentity.com/products/authentication-services/), and [Centrify](https://www.centrify.com/). This guide includes steps to check your Active Directory configuration. It's not intended to instruct on how to join a machine to a domain. For detailed instructions on joining a [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] host to a domain by using realmd and SSSD, see [Use Active Directory authentication with SQL Server on Linux](sql-server-linux-active-directory-authentication.md).

## Prerequisites

Before you configure Active Directory authentication, you need to set up an Active Directory domain controller, Windows, on your network. Then join your [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] on Linux host to an Active Directory domain. You can use [PBIS](https://www.beyondtrust.com/), [VAS](https://www.oneidentity.com/products/authentication-services/), or [Centrify](https://www.centrify.com/).

> [!NOTE]
>
>This tutorial uses **`contoso.com`** and **`CONTOSO.COM`** as example domain and realm names, respectively. It also uses **`DC1.CONTOSO.COM`** as the example fully qualified domain name of the domain controller. You should replace these names with your own values.

## Check the connection to a domain controller

Check that you can contact the domain controller with both the short and fully qualified names of the domain:

```bash
ping contoso

ping contoso.com
```

If either of these name checks fail, update your domain search list:

- **Ubuntu**

  Edit the `/etc/network/interfaces` file, so that your Active Directory domain is in the domain search list: 

  ```/etc/network/interfaces
  <...>
  # The primary network interface
  auto eth0
  iface eth0 inet dhcp
  dns-nameservers **<AD domain controller IP address>**
  dns-search **<AD domain name>**
  ```

  > [!NOTE]  
  > The network interface, **eth0**, might differ for different machines. To find out which one you're using, run **ifconfig**. Then copy the interface that has an IP address and transmitted and received bytes.

  After editing this file, restart the network service:

  ```bash
  sudo ifdown eth0 && sudo ifup eth0
  ```

  Now check that your `/etc/resolv.conf` file contains a line like the following example:  

  ```/etc/resolv.conf
  search contoso.com com  
  nameserver **<AD domain controller IP address>**
  ```

- **RHEL**

  Edit the `/etc/sysconfig/network-scripts/ifcfg-eth0` file, so that your Active Directory domain is in the domain search list. Or edit  another interface config file as appropriate:

  ```/etc/sysconfig/network-scripts/ifcfg-eth0
  <...>
  PEERDNS=no
  DNS1=**<AD domain controller IP address>**
  DOMAIN="contoso.com com"
  ```

  After editing this file, restart the network service:

  ```bash
  sudo systemctl restart network
  ```

  Now check that your `/etc/resolv.conf` file contains a line like the following example:  

  ```/etc/resolv.conf
  search contoso.com com  
  nameserver **<AD domain controller IP address>**
  ```

  If you still can't ping the domain controller, find the fully qualified domain name and IP address of the domain controller. An example domain name is `DC1.CONTOSO.COM`. Add the following entry to `/etc/hosts`:

  ```/etc/hosts
  **<IP address>** DC1.CONTOSO.COM CONTOSO.COM CONTOSO
  ```

- **SLES**

  Edit the `/etc/sysconfig/network/config` file, so that your Active Directory domain controller IP is used for DNS queries, and your Active Directory domain is in the domain search list:

  ```/etc/sysconfig/network/config
  <...>
  NETCONFIG_DNS_STATIC_SEARCHLIST=""
  NETCONFIG_DNS_STATIC_SERVERS="**<AD domain controller IP address>**"
  ```

  After editing this file, restart the network service:

  ```bash
  sudo systemctl restart network
  ```

  Now check that your `/etc/resolv.conf` file contains a line like the following example:

  ```/etc/resolv.conf
  search contoso.com com
  nameserver **<AD domain controller IP address>**
  ```

## Check that the reverse DNS is properly configured

The following command should return the fully qualified domain name of the host that runs SQL Server. An example is **`SqlHost.contoso.com`**.

```bash
host **<IP address of SQL Server host>**
# **<reversed IP address>**.in-addr.arpa domain name pointerSqlHost.contoso.com.
```

If this command doesn't return your host's FQDN, or if the FQDN is incorrect, add a reverse DNS entry for you are [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] on Linux host to your DNS server.

## Check that your KRB5 configuration is correct

Check that your `/etc/krb5.conf` is configured correctly. For most third-party Active Directory providers, this configuration is done automatically. However, check `/etc/krb5.conf` for the following values to prevent any future issues:

```/etc/krb5.conf
[libdefaults]
default_realm = CONTOSO.COM

[realms]
CONTOSO.COM = {
}

[domain_realm]
contoso.com = CONTOSO.COM
.contoso.com = CONTOSO.COM
```

## Next steps

This article covers how to configure a [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] on a Linux host machine with Active Directory Authentication when using third-party Active Directory providers. To finish configuring [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] on Linux to support Active Directory accounts, follow the instructions at [Use Active Directory authentication with SQL Server on Linux](sql-server-linux-active-directory-authentication.md).

> [!div class="nextstepaction"]
> [Use Active Directory authentication with SQL Server on Linux](sql-server-linux-active-directory-authentication.md)

> [!NOTE]
>
> You can skip the **Join SQL Server host to Active Directory domain** section in [Use Active Directory authentication with SQL Server on Linux](sql-server-linux-active-directory-authentication.md) as you've just done that in this tutorial.
