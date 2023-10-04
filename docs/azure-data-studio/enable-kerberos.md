---
title: Connect your SQL Server instance using Windows authentication (Kerberos)
description: Learn how to connect Azure Data Studio to your SQL Server instance by using Microsoft Kerberos integrated authentication.
author: markingmyname
ms.author: maghan
ms.reviewer: alayu, randolphwest
ms.date: 06/15/2023
ms.service: azure-data-studio
ms.topic: how-to
---
# Connect Azure Data Studio to SQL Server using Kerberos

Azure Data Studio supports connecting to SQL Server by using Kerberos.

To use integrated authentication (Windows Authentication) on macOS or Linux, you need to set up a *Kerberos ticket* that links your current user to a Windows domain account.

## Prerequisites

To get started, you need:

- Access to a Windows domain-joined machine to query your Kerberos domain controller.

- SQL Server should be configured to allow Kerberos authentication. For the client driver running on Unix, integrated authentication is supported only by using Kerberos. For more information, see [Using Kerberos integrated authentication to connect to SQL Server](../connect/jdbc/using-kerberos-integrated-authentication-to-connect-to-sql-server.md). There should be [service principal names (SPNs)](/windows/win32/ad/service-principal-names) registered for each instance of SQL Server you're trying to connect to. For more information, see [Register a Service Principal Name for Kerberos Connections](../database-engine/configure-windows/register-a-service-principal-name-for-kerberos-connections.md).

## Check if SQL Server has a Kerberos setup

Sign in to the host machine of SQL Server. From the Windows command prompt, use `setspn -L %COMPUTERNAME%` to list all the SPNs for the host. Verify there are entries that begin with `MSSQLSvc/HostName.contoso.com`. These entries mean that SQL Server has registered an SPN and is ready to accept Kerberos authentication.

If you don't have access to the host of the SQL Server instance, then from any other Windows OS joined to the same Active Directory, you could use the command `setspn -L <SQLSERVER_NETBIOS>`, where *<SQLSERVER_NETBIOS>* is the computer name of the host of the SQL Server instance.

## Get the Kerberos Key Distribution Center

Find the Kerberos Key Distribution Center (KDC) configuration value. Run the following command on a Windows computer that's joined to your Active Directory domain.

Run `nltest` from the command line, and replace "DOMAIN.CONTOSO.COM" with your domain's name.

```console
nltest /dsgetdc:DOMAIN.CONTOSO.COM
```

Output is similar to the following sample:

```output
DC: \\dc-33.domain.contoso.com
Address: \\2111:4444:2111:33:1111:ecff:ffff:3333
...
The command completed successfully
```

Copy the DC name that's the required KDC configuration value. In this case, it's dc-33.domain.contoso.com.

## Join your OS to the Active Directory domain controller

### [Ubuntu](#tab/ubuntu)

```bash
sudo apt-get install realmd krb5-user software-properties-common python-software-properties packagekit
```

Edit the `/etc/network/interfaces` file so that your Active Directory domain controller's IP address is listed as `dns-nameserver`. For example:

```output
<...>
# The primary network interface
auto eth0
iface eth0 inet dhcp
dns-nameservers **<AD domain controller IP address>**
dns-search **<AD domain name>**
```

> [!NOTE]  
> The network interface (eth0) might differ for different machines. To find out which one you're using, run ifconfig and copy the interface that has an IP address and transmitted and received bytes.

After editing this file, restart the network service:

```bash
sudo ifdown eth0 && sudo ifup eth0
```

Now check that your `/etc/resolv.conf` file contains a line like the following one:

```output
nameserver **<AD domain controller IP address>**
```

Join to the Active Directory domain:

```bash
sudo realm join contoso.com -U 'user@CONTOSO.COM' -v
```

Here's the expected output:

```output
<...>
* Success
```

### [Red Hat Enterprise Linux](#tab/rhel)

```bash
sudo yum install realmd krb5-workstation
```

Edit the `/etc/sysconfig/network-scripts/ifcfg-eth0` file (or other interface config file as appropriate) so that your Active Directory domain controller's IP address is listed as a DNS server:

```output
<...>
PEERDNS=no
DNS1=**<AD domain controller IP address>**
```

After editing this file, restart the network service:

```bash
sudo systemctl restart network
```

Now check that your `/etc/resolv.conf` file contains a line like the following one:

```output
nameserver **<AD domain controller IP address>**
```

Join to the Active Directory domain:

```bash
sudo realm join contoso.com -U 'user@CONTOSO.COM' -v
```

Here's the expected output:

```output
<...>
* Success
```

### [Configure KDC in krb5.conf with macOS](#tab/mac)

This section discusses the [Kerberos configuration file](http://web.mit.edu/macdev/KfM/Common/Documentation/preferences-osx.html).

Edit the `/etc/krb5.conf` file in an editor of your choice. Configure the following keys:

```bash
sudo vi /etc/krb5.conf
```

```output
[libdefaults]
  default_realm = DOMAIN.CONTOSO.COM

[realms]
DOMAIN.CONTOSO.COM = {
   kdc = dc-33.domain.contoso.com
}
```

Then save the `krb5.conf` file and exit.

> [!NOTE]  
> The domain must be in ALL CAPS.

---

## Test the ticket granting ticket retrieval

Get a Ticket Granting Ticket (TGT) from KDC.

```bash
kinit username@DOMAIN.CONTOSO.COM
```

View the available tickets by using `klist`. If the `kinit` was successful, you should see a ticket.

```bash
klist
```

Here's the expected output:

```output
krbtgt/DOMAIN.CONTOSO.COM@ DOMAIN.CONTOSO.COM.
```

## Connect by using Azure Data Studio

1. Create a new connection profile.

1. Select **Windows Authentication** as the authentication type.

1. For the Server, enter a fully qualified host name, in the format `hostname.DOMAIN.CONTOSO.COM`.

1. Complete the connection profile, and select **Connect**.

After successfully connecting, your server appears in the **SERVERS** sidebar.
