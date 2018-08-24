---
title: Third Party AD Providers | Microsoft Docs
description: This tutorial provides the configuration steps for AD Authentication with third party providers
author: dylan-MSFT
ms.date: 07/25/2018
ms.author: dygray 
manager: mikehab
ms.topic: conceptual
ms.prod: sql
ms.component: ""
ms.suite: "sql"
ms.custom: "sql-linux"
ms.technology: linux
helpviewer_keywords: 
  - "Linux, AD authentication"
---
# Tutorial: Use Active Directory Authentication with SQL Server on Linux through Third Party AD Providers

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-linuxonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-linuxonly.md)]

This tutorial explains how to configure a [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] on Linux host machine with AD Authentication when using third party AD providers, such as [PowerBroker Identity Services (PBIS)](https://www.beyondtrust.com/), [Vintela Authentication Services (VAS)](https://www.oneidentity.com/products/authentication-services/), and [Centrify](https://www.centrify.com/). This guide includes steps to check your AD configuration, and it is not intended to instruct on how to join a machine to a domain. For detailed instructions on joining a [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] host to a domain using REALM and SSSD, see [Use Active Directory authentication with SQL Server on Linux](sql-server-linux-active-directory-authentication.md).

To setup AD Authentication with [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] on Linux, you need to do the following:

> [!div class="checklist"]
> * Join [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] host to an AD domain
> * Check configuration is compatible for AD Authentication
> * Create AD user for SQL Server and set SPN
> * Configure SQL Server service keytab
> * Create AD-based logins in Trasact-SQL
> * Connect to SQL Server using AD Authentication

In this tutorial, we will cover the second step listed above. For help with joining the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] host to an AD domain with SSSD, please look at [Use Active Directory authentication with SQL Server on Linux](sql-server-linux-active-directory-authentication.md). Alternatively, you can use a third party AD provider to join the AD domain, such as [PBIS](https://www.beyondtrust.com/), [VAS](https://www.oneidentity.com/products/authentication-services/), or [Centrify](https://www.centrify.com/).

For help with steps 3-6, please follow the steps at [Use Active Directory authentication with SQL Server on Linux](sql-server-linux-active-directory-authentication.md).

## Prerequisites

Before you configure AD Authentication, you need to set up an AD Domain Controller (Windows) on your network. You must also join your [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] on Linux host to an AD domain. You can use [PBIS](https://www.beyondtrust.com/), [VAS](https://www.oneidentity.com/products/authentication-services/), or [Centrify](https://www.centrify.com/).

>NOTE
>
>This walkthrough uses "contoso.com" and "CONTOSO.COM" as example domain and realm names respectively. It also uses "DC1.CONTOSO.COM" as the example fully qualified domain name of the domain controller. You should replace these with your own values.

## Check Connection to Domain Controller

Check you can contact the domain controller with both the short and fully qualified name of the domain.

   ```bash
   ping contoso

   ping contoso.com
   ```

   If either of these fail, update your domain search list

   - **Ubuntu**:

     Edit the `/etc/network/interfaces` file so that your AD Domain is in the domain search list: 

     ```/etc/network/interfaces
     <...>
     # The primary network interface
     auto eth0
     iface eth0 inet dhcp
     dns-nameservers **<AD domain controller IP address>**
     dns-search **<AD domain name>**
     ```

     > [!NOTE]
     > The network interface (eth0) might differ for different machines. To find out which one you are using, run ifconfig and copy the interface that has an IP address and transmitted and received bytes.

     After editing this file, restart the network service:

     ```bash
     sudo ifdown eth0 && sudo ifup eth0
     ```

     Now check that your `/etc/resolv.conf` file contains a line like the following example:  

     ```/etc/resolv.conf
     search contoso.com com  
     nameserver \*\*\<AD domain controller IP address\>\*\* 
     ```

   - **RHEL**:

     Edit the `/etc/sysconfig/network-scripts/ifcfg-eth0` file (or other interface config file as appropriate) so that your AD Domain is in the domain search list:

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
     nameserver \*\*\<AD domain controller IP address\>\*\*
     ```

   If you still cannot ping the domain controller, find the fully qualified domain name (e.g. DC1.CONTOSO.COM) and IP address of the domain controller and add the following entry to `/etc/hosts`

      ```/etc/hosts
      **<IP address>** DC1.CONTOSO.COM CONTOSO.COM CONTOSO
      ```

   - **SLES**:

     Edit the `/etc/sysconfig/network/config` file so that your AD domain controller IP will be used for DNS queries and the your AD domain is in the domain search list:

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
     nameserver \*\*\<AD domain controller IP address\>\*\*
     ```

## Check Reverse DNS is properly configured

The following command should return the fully qualified domain name of the host running SQL Server (eg. "SqlHost.contoso.com").

   ```bash
   host **<IP address of SQL Server host>**
   # **<reversed IP address>**.in-addr.arpa domain name pointerSqlHost.contoso.com.
   ```

   If this does not return your host's FQDN or if the FQDN is incorrect, please add a reverse DNS entry for your [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] on Linux host to your DNS server.

## Check your KRB5 configuration is correct

Check your `/etc/krb5.conf` is configured correctly. For most third party AD providers, this is done automatically. However, please check `/etc/krb5.conf` for the following values to prevent any future issues:

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

To setup AD Authentication with [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] on Linux, you need to do the following:

> [!div class="checklist"]
> * Join [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] host to an AD domain
> * Check configuration is compatible for AD Authentication
> * Create AD user for SQL Server and set SPN
> * Configure SQL Server service keytab
> * Create AD-based logins in Trasact-SQL
> * Connect to SQL Server using AD Authentication

In this tutorial, we will covered step #2. For help with steps #3-6, please follow the instructions at [Use Active Directory authentication with SQL Server on Linux](sql-server-linux-active-directory-authentication.md).

> [!div class="nextstepaction"]
> [Use Active Directory authentication with SQL Server on Linux](sql-server-linux-active-directory-authentication.md)

> NOTE
>
> You can skip the "Join SQL Server host to AD domain" section in [Use Active Directory authentication with SQL Server on Linux](sql-server-linux-active-directory-authentication.md)
 as you have just done that in this tutorial.