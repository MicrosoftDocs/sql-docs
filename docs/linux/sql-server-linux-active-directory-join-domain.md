---
title: Join SQL Server on Linux to Active Directory
titleSuffix: SQL Server
description: 
author: Dylan-MSFT
ms.author: Dylan.Gray
ms.reviewer: rothja
ms.date: 04/01/2019
manager: craigg
ms.topic: conceptual
ms.prod: sql
ms.technology: linux
---

# Join SQL Server on a Linux host to an Active Directory domain

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-linuxonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-linuxonly.md)]

This article provides general guidance on how to join a SQL Server Linux host machine to an Active Directory (AD) domain. There are two methods: use a built-in SSSD package or use third-party Active Directory providers. Examples of third-party domain join products are [PowerBroker Identity Services (PBIS)](https://www.beyondtrust.com/), [One Identity](https://www.oneidentity.com/products/authentication-services/), and [Centrify](https://www.centrify.com/). This guide includes steps to check your Active Directory configuration. However, it is not intended to provide instructions on how to join a machine to a domain when using third-party utilities.

## Prerequisites

Before you configure Active Directory authentication, you need to set up an Active Directory domain controller, Windows, on your network. Then join your SQL Server on Linux host to an Active Directory domain.

> [!IMPORTANT]
> The sample steps described in this article are for guidance only. Actual steps may slightly differ in your environment depending on how your overall environment is configured. Engage your system and domain administrators for your environment for specific configuration, customization, and any required troubleshooting.

## Check the connection to a domain controller

Check that you can contact the domain controller with both the short and fully qualified names of the domain:

```bash
ping contoso
ping contoso.com
```

> [!TIP]
> This tutorial uses **contoso.com** and **CONTOSO.COM** as example domain and realm names, respectively. It also uses **DC1.CONTOSO.COM** as the example fully qualified domain name of the domain controller. You must replace these names with your own values.

If either of these name checks fail, update your domain search list. The following sections provide instructions for Ubuntu, Red Hat Enterprise Linux (RHEL), and SUSE Linux Enterprise Server (SLES) respectively.

### Ubuntu

1. Edit the **/etc/network/interfaces** file, so that your Active Directory domain is in the domain search list:

   ```/etc/network/interfaces
   # The primary network interface
   auto eth0
   iface eth0 inet dhcp
   dns-nameservers **<AD domain controller IP address>**
   dns-search **<AD domain name>**
   ```

   > [!NOTE]
   > The network interface, `eth0`, might differ for different machines. To find out which one you're using, run **ifconfig**. Then copy the interface that has an IP address and transmitted and received bytes.

1. After editing this file, restart the network service:

   ```bash
   sudo ifdown eth0 && sudo ifup eth0
   ```

1. Next, check that your **/etc/resolv.conf** file contains a line like the following example:

   ```/etc/resolv.conf
   search contoso.com com  
   nameserver **<AD domain controller IP address>**
   ```

### RHEL

1. Edit the **/etc/sysconfig/network-scripts/ifcfg-eth0** file, so that your Active Directory domain is in the domain search list. Or edit another interface config file as appropriate:

   ```/etc/sysconfig/network-scripts/ifcfg-eth0
   PEERDNS=no
   DNS1=**<AD domain controller IP address>**
   DOMAIN="contoso.com com"
   ```

1. After editing this file, restart the network service:

   ```bash
   sudo systemctl restart network
   ```

1. Now check that your **/etc/resolv.conf** file contains a line like the following example:

   ```/etc/resolv.conf
   search contoso.com com  
   nameserver **<AD domain controller IP address>**
   ```

1. If you still cannot ping the domain controller, find the fully qualified domain name and IP address of the domain controller. An example domain name is **DC1.CONTOSO.COM**. Add the following entry to **/etc/hosts**:

   ```/etc/hosts
   **<IP address>** DC1.CONTOSO.COM CONTOSO.COM CONTOSO
   ```

### SLES

1. Edit the **/etc/sysconfig/network/config** file, so that your Active Directory domain controller IP is used for DNS queries and your Active Directory domain is in the domain search list:

   ```/etc/sysconfig/network/config
   NETCONFIG_DNS_STATIC_SEARCHLIST=""
   NETCONFIG_DNS_STATIC_SERVERS="**<AD domain controller IP address>**"
   ```

1. After editing this file, restart the network service:

   ```bash
   sudo systemctl restart network
   ```

1. Next, check that your **/etc/resolv.conf** file contains a line like the following example:

   ```/etc/resolv.conf
   search contoso.com com
   nameserver **<AD domain controller IP address>**
   ```

## Check that the reverse DNS is properly configured

The following command should return the fully qualified domain name (FQDN) of the host that runs SQL Server. An example is **SqlHost.contoso.com**.

```bash
host **<IP address of SQL Server host>**
# **<reversed IP address>**.in-addr.arpa domain name pointerSqlHost.contoso.com.
```

If this command does not return your host's FQDN, or if the FQDN is incorrect, add a reverse DNS entry for your SQL Server on Linux host to your DNS server.

## Join to the AD domain

After the basic configuration and connectivity with domain controller is verified, there are two options for joining a SQL Server Linux host machine with Active Directory domain controller:

- [Option 1: Use an SSSD package](#option1)
- [Option 2: Use third-party openldap provider utilities](#option2)

### <a id="option1"></a> Option 1: Use SSSD package to join AD domain

This method joins the SQL Server host to an AD domain using **realmd** and **sssd** packages.

> [!NOTE]
> This is the preferred method of joining a Linux host to an AD domain controller.

Use the following steps to join a SQL Server host to an Active Directory domain:

1. Use [realmd](https://www.freedesktop.org/software/realmd/docs/guide-active-directory-join.md) to join your host machine to your AD Domain. You must first install both the **realmd** and Kerberos client packages on the SQL Server host machine using your Linux distribution's package manager:

   **RHEL:**

   ```base
   sudo yum install realmd krb5-workstation
   ```

   **SUSE:**

   ```bash
   sudo zypper install realmd krb5-client
   ```

   **Ubuntu:**

   ```bash
   sudo apt-get install realmd krb5-user software-properties-common python-software-properties packagekit
   ```

1. If the Kerberos client package installation prompts you for a realm name, enter your domain name in uppercase.

1. After you confirm that your DNS is configured properly, join the domain by running the following command. You must authenticate using an AD account that has sufficient privileges in AD to join a new machine to the domain. Specifically, this command creates a new computer account in AD, creates the **/etc/krb5.keytab** host keytab file, and configures the domain in **/etc/sssd/sssd.conf**:

   ```bash
   sudo realm join contoso.com -U 'user@CONTOSO.COM' -v
   ```

   You should see the message, `Successfully enrolled machine in realm`.

   The following table lists some error messages that you could receive and suggestions on resolving them:

   | Error message | Recommendation |
   |---|---|
   | `Necessary packages are not installed` | Install those packages using your Linux distribution's package manager before running the realm join command again. |
   | `Insufficient permissions to join the domain` | Check with a domain administrator that you have sufficient permissions to join Linux machines to your domain. |
   | `KDC reply did not match expectations` | You may not have specified the correct realm name for the user. Realm names are case-sensitive, usually uppercase, and can be identified with the command realm discover contoso.com. |

   SQL Server uses SSSD and NSS for mapping user accounts and groups to security identifiers (SIDs). SSSD must be configured and running for SQL Server to create AD logins successfully. **realmd** usually does this automatically as part of joining the domain, but in some cases, you must do this separately.

   For more information, see how to [configure SSSD manually](https://access.redhat.com/articles/3023951), and [configure NSS to work with SSSD](https://access.redhat.com/documentation/red_hat_enterprise_linux/7/html/system-level_authentication_guide/configuring_services#Configuration_Options-NSS_Configuration_Options).

1. Verify that you can now gather information about a user from the domain, and that you can acquire a Kerberos ticket as that user. The following example uses **id**, [kinit](https://web.mit.edu/kerberos/krb5-1.12/doc/user/user_commands/kinit.html), and [klist](https://web.mit.edu/kerberos/krb5-1.12/doc/user/user_commands/klist.html) commands for this.

   ```bash
   id user@contoso.com

   uid=1348601103(user@contoso.com) gid=1348600513(domain group@contoso.com) groups=1348600513(domain group@contoso.com)

   kinit user@CONTOSO.COM

   Password for user@CONTOSO.COM:

   klist
   Ticket cache: FILE:/tmp/krb5cc_1000
   Default principal: user@CONTOSO.COM
   ```

   > [!NOTE]
   > - If **id user@contoso.com** returns, `No such user`, make sure that the SSSD service started successfully by running the command `sudo systemctl status sssd`. If the service is running and you still see the error, try enabling verbose logging for SSSD. For more information, see the Red Hat documentation for [Troubleshooting SSSD](https://access.redhat.com/documentation/Red_Hat_Enterprise_Linux/7/html/System-Level_Authentication_Guide/trouble.html#SSSD-Troubleshooting).
   >
   > - If **kinit user@CONTOSO.COM** returns, `KDC reply did not match expectations while getting initial credentials`, make sure you specified the realm in uppercase.

For more information, see the Red Hat documentation for [Discovering and Joining Identity Domains](https://access.redhat.com/documentation/Red_Hat_Enterprise_Linux/7/html/Windows_Integration_Guide/realmd-domain.html).

### <a id="option2"></a> Option 2: Use third-party openldap provider utilities

You can use third-party utilities such as [PBIS](https://www.beyondtrust.com/), [VAS](https://www.oneidentity.com/products/authentication-services/), or [Centrify](https://www.centrify.com/). This article does not cover steps for each individual utility. You must first use one of these utilities to join the Linux host for SQL Server to the domain before continuing forward.  

SQL Server does not use third-party integrator's code or library for any AD-related queries. SQL Server always queries AD using openldap library calls directly in this setup. The third-party integrators are only used to join the Linux host to AD domain, and SQL Server does not have any direct communication with these utilities.

> [!IMPORTANT]
> Please see the recommendations for using the **mssql-conf** `network.disablesssd` configuration option in the **Additional configuration options** section of the article [Use Active Directory authentication with SQL Server on Linux](sql-server-linux-active-directory-authentication.md#additionalconfig).

Verify that your **/etc/krb5.conf** is configured correctly. For most third-party Active Directory providers, this configuration is done automatically. However, check **/etc/krb5.conf** for the following values to prevent any future issues:

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

This article covers the prerequisite of how to configure a SQL Server on a Linux host machine with Active Directory Authentication. To finish configuring SQL Server on Linux to support Active Directory accounts, follow the instructions at [Use Active Directory authentication with SQL Server on Linux](sql-server-linux-active-directory-authentication.md).