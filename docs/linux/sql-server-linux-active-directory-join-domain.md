---
title: Join SQL Server on Linux to Active Directory
titleSuffix: SQL Server
description: This article provides guidance joining a SQL Server Linux host machine to an Active Directory domain. You can use a built-in SSSD package or use third-party Active Directory providers.
author: tejasaks
ms.author: tejasaks
ms.reviewer: vanto, randolphwest
ms.date: 09/27/2022
ms.service: sql
ms.subservice: linux
ms.topic: conceptual
---
# Join SQL Server on a Linux host to an Active Directory domain

[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]

This article provides general guidance on how to join a SQL Server Linux host machine to an Active Directory domain. There are two methods: use a built-in SSSD package or use third-party Active Directory providers. Examples of third-party domain join products are [PowerBroker Identity Services (PBIS)](https://www.beyondtrust.com/), [One Identity](https://www.oneidentity.com/products/authentication-services/), and [Centrify](https://www.centrify.com/). This guide includes steps to check your Active Directory configuration. However, it is not intended to provide instructions on how to join a machine to a domain when using third-party utilities.

## Prerequisites

Before you configure Active Directory authentication, you need to set up an Active Directory domain controller, Windows, on your network. Then join your SQL Server on Linux host to an Active Directory domain.

> [!IMPORTANT]  
> The sample steps described in this article are for guidance only and refer to Ubuntu 16.04, Red Hat Enterprise Linux (RHEL) 7.x and SUSE Enterprise Linux (SLES) 12 operating systems. Actual steps may slightly differ in your environment depending on how your overall environment is configured and operating system version. For example, Ubuntu 18.04 uses netplan while Red Hat Enterprise Linux (RHEL) 8.x uses nmcli among other tools to manage and configure network. It is recommended to engage your system and domain administrators for your environment for specific tooling, configuration, customization, and any required troubleshooting.
>
> For information on configuring Active Directory with newer versions of Ubuntu, RHEL, or SLES, see [Configure Active Directory authentication with SQL Server on Linux using adutil](sql-server-linux-ad-auth-adutil-tutorial.md).

### Reverse DNS (RDNS)

When you set up a computer running Windows Server as a domain controller, you might not have a RDNS zone by default. Ensure that an applicable RDNS zone exists for both the domain controller and the IP address of the Linux machine that will be running SQL Server.

Also ensure that a PTR record that points to your domain controllers exists.

## Check the connection to a domain controller

Check that you can contact the domain controller by using both the short and the fully qualified names of the domain, and by using the hostname of the domain controller. The IP of the domain controller also should resolve to the FQDN of the domain controller:

```bash
ping contoso
ping contoso.com
ping dc1.contoso.com
nslookup <IP address of dc1.contoso.com>
```

> [!TIP]  
> This tutorial uses `contoso.com` and `CONTOSO.COM` as example domain and realm names, respectively. It also uses `DC1.CONTOSO.COM` as the example fully qualified domain name of the domain controller. You must replace these names with your own values.

If any of these name checks fail, update your domain search list. The following sections provide instructions for Ubuntu, Red Hat Enterprise Linux (RHEL), and SUSE Linux Enterprise Server (SLES) respectively.

### Ubuntu 16.04

1. Edit the `/etc/network/interfaces` file, so that your Active Directory domain is in the domain search list:

   ```/etc/network/interfaces
   # The primary network interface
   auto eth0
   iface eth0 inet dhcp
   dns-nameservers <Domain controller IP address>
   dns-search <Active Directory domain name>
   ```

   > [!NOTE]  
   > The network interface, `eth0`, might differ for different machines. To find out which one you're using, run **ifconfig**. Then copy the interface that has an IP address and transmitted and received bytes.

1. After editing this file, restart the network service:

   ```bash
   sudo ifdown eth0 && sudo ifup eth0
   ```

1. Next, check that your `/etc/resolv.conf` file contains a line like the following example:

   ```/etc/resolv.conf
   search contoso.com com
   nameserver <Domain controller IP address>
   ```

### Ubuntu 18.04

1. Edit the [sudo vi /etc/netplan/******.yaml] file, so that your Active Directory domain is in the domain search list:

   ```/etc/netplan/******.yaml
   network:
     ethernets:
       eth0:
               dhcp4: true

               dhcp6: true
               nameservers:
                       addresses: [<Domain controller IP address>]
                       search: [<Active Directory domain name>]
     version: 2
   ```

   > [!NOTE]  
   > The network interface, `eth0`, might differ for different machines. To find out which one you're using, run **ifconfig**. Then copy the interface that has an IP address and transmitted and received bytes.

1. After editing this file, restart the network service:

   ```bash
   sudo netplan apply
   ```

1. Next, check that your `/etc/resolv.conf` file contains a line like the following example:

   ```/etc/resolv.conf
   search contoso.com com
   nameserver <Domain controller IP address>
   ```

### RHEL 7.x

1. Edit the `/etc/sysconfig/network-scripts/ifcfg-eth0` file, so that your Active Directory domain is in the domain search list. Or edit another interface config file as appropriate:

   ```/etc/sysconfig/network-scripts/ifcfg-eth0
   PEERDNS=no
   DNS1=<Domain controller IP address>
   DOMAIN="contoso.com com"
   ```

1. After editing this file, restart the network service:

   ```bash
   sudo systemctl restart network
   ```

1. Now check that your `/etc/resolv.conf` file contains a line like the following example:

   ```/etc/resolv.conf
   search contoso.com com
   nameserver <Domain controller IP address>
   ```

1. If you still cannot ping the domain controller, find the fully qualified domain name and IP address of the domain controller. An example domain name is `DC1.CONTOSO.COM`. Add the following entry to `/etc/hosts`:

   ```/etc/hosts
   <IP address> DC1.CONTOSO.COM CONTOSO.COM CONTOSO
   ```

### SLES 12

1. Edit the `/etc/sysconfig/network/config` file, so that your domain controller IP is used for DNS queries and your Active Directory domain is in the domain search list:

   ```/etc/sysconfig/network/config
   NETCONFIG_DNS_STATIC_SEARCHLIST=""
   NETCONFIG_DNS_STATIC_SERVERS="<Domain controller IP address>"
   ```

1. After editing this file, restart the network service:

   ```bash
   sudo systemctl restart network
   ```

1. Next, check that your `/etc/resolv.conf` file contains a line like the following example:

   ```/etc/resolv.conf
   search contoso.com com
   nameserver <Domain controller IP address>
   ```

## Join to the Active Directory domain

After the basic configuration and connectivity with domain controller is verified, there are two options for joining a SQL Server Linux host machine with the Active Directory domain controller:

- [Option 1: Use an SSSD package](#option1)
- [Option 2: Use third-party openldap provider utilities](#option2)

### <a id="option1"></a> Option 1: Use SSSD package to join Active Directory domain

This method joins the SQL Server host to an Active Directory domain using **realmd** and **sssd** packages.

> [!NOTE]  
> This is the preferred method of joining a Linux host to an Active Directory domain controller.

Use the following steps to join a SQL Server host to an Active Directory domain:

1. Use [realmd](https://www.freedesktop.org/software/realmd/docs/guide-active-directory-join) to join your host machine to your Active Directory Domain. You must first install both the **realmd** and Kerberos client packages on the SQL Server host machine using your Linux distribution's package manager:

   **RHEL:**

   ```base
   sudo yum install realmd krb5-workstation
   ```

   **SLES 12:**

   Note that these steps are specific for SLES 12, which is the only officially supported version of SUSE for Linux.

   ```bash
   sudo zypper addrepo https://download.opensuse.org/repositories/network/SLE_12/network.repo
   sudo zypper refresh
   sudo zypper install realmd krb5-client sssd-ad
   ```

   **Ubuntu 16.04:**

   ```bash
   sudo apt-get install realmd krb5-user software-properties-common python-software-properties packagekit
   ```

   **Ubuntu 18.04:**

   ```bash
   sudo apt-get install realmd krb5-user software-properties-common python3-software-properties packagekit
   sudo apt-get install adcli libpam-sss libnss-sss sssd sssd-tools
   ```

1. If the Kerberos client package installation prompts you for a realm name, enter your domain name in uppercase.

1. After you confirm that your DNS is configured properly, join the domain by running the following command. You must authenticate using an Active Directory account that has sufficient privileges in Active Directory to join a new machine to the domain. This command creates a new computer account in Active Directory, creates the `/etc/krb5.keytab` host keytab file, configures the domain in `/etc/sssd/sssd.conf`, and updates `/etc/krb5.conf`.

   Because of an issue with **realmd**, first set the machine hostname to the FQDN instead of to the machine name. Otherwise, **realmd** might not create all required SPNs for the machine and DNS entries won't automatically update, even if your domain controller supports dynamic DNS updates.

   ```bash
   sudo hostname <old hostname>.contoso.com
   ```

   After running the above command, your /etc/hostname file should contain \<old hostname\>.contoso.com.

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

   SQL Server uses SSSD and NSS for mapping user accounts and groups to security identifiers (SIDs). SSSD must be configured and running for SQL Server to create Active Directory logins successfully. **realmd** usually does this automatically as part of joining the domain, but in some cases, you must do this separately.

   For more information, see how to [configure SSSD manually](https://access.redhat.com/articles/3023951), and [configure NSS to work with SSSD](https://access.redhat.com/documentation/en-us/red_hat_enterprise_linux/7/html/system-level_authentication_guide/configuring_services#Configuration_Options-NSS_Configuration_Options).

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
   > If `id user\@contoso.com` returns, `No such user`, make sure that the SSSD service started successfully by running the command `sudo systemctl status sssd`. If the service is running and you still see the error, try enabling verbose logging for SSSD. For more information, see the Red Hat documentation for [Troubleshooting SSSD](https://access.redhat.com/documentation/en-us/red_hat_enterprise_linux/7/html/system-level_authentication_guide/trouble#SSSD-Troubleshooting).
   >
   > If `kinit user\@CONTOSO.COM` returns, `KDC reply did not match expectations while getting initial credentials`, make sure you specified the realm in uppercase.

For more information, see the Red Hat documentation for [Discovering and Joining Identity Domains](https://access.redhat.com/documentation/en-us/red_hat_enterprise_linux/7/html/windows_integration_guide/realmd-domain).

### <a id="option2"></a> Option 2: Use third-party openldap provider utilities

You can use third-party utilities such as [PBIS](https://www.beyondtrust.com/), [VAS](https://www.oneidentity.com/products/authentication-services/), or [Centrify](https://www.centrify.com/). This article does not cover steps for each individual utility. You must first use one of these utilities to join the Linux host for SQL Server to the domain before continuing forward.

SQL Server does not use third-party integrator's code or library for any Active Directory-related queries. SQL Server always queries Active Directory using openldap library calls directly in this setup. The third-party integrators are only used to join the Linux host to Active Directory domain, and SQL Server does not have any direct communication with these utilities.

> [!IMPORTANT]  
> Please see the recommendations for using the **mssql-conf** `network.disablesssd` configuration option in the Additional configuration options section of the article [Use Active Directory authentication with SQL Server on Linux](sql-server-linux-active-directory-authentication.md#additionalconfig).

Verify that your `/etc/krb5.conf` is configured correctly. For most third-party Active Directory providers, this configuration is done automatically. However, check `/etc/krb5.conf` for the following values to prevent any future issues:

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

## Check that the reverse DNS is properly configured

The following command should return the fully qualified domain name (FQDN) of the host that runs SQL Server. An example is `SqlHost.contoso.com`.

```bash
host <IP address of SQL Server host>
```

The output of this command should be similar to `<reversed IP address>.in-addr.arpa domain name pointer SqlHost.contoso.com`. If this command does not return your host's FQDN, or if the FQDN is incorrect, add a reverse DNS entry for your SQL Server on Linux host to your DNS server.

## Next steps

This article covers the prerequisite of how to configure a SQL Server on a Linux host machine with Active Directory Authentication. To finish configuring SQL Server on Linux to support Active Directory accounts, follow the instructions at [Use Active Directory authentication with SQL Server on Linux](sql-server-linux-active-directory-authentication.md).
