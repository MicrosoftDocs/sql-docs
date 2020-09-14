---
title: Connect your SQL Server using Windows authentication (Kerberos)
description: Learn how to connect Azure Data Studio to SQL Server using Microsoft Kerberos integrated authentication.
ms.prod: azure-data-studio
ms.technology: azure-data-studio
author: markingmyname
ms.author: maghan
ms.reviewer: alayu, maghan, sstein
ms.topic: conceptual
ms.custom: seodec18
ms.date: 09/24/2018
---

# Connect Azure Data Studio to your SQL Server using Windows authentication - Kerberos

Azure Data Studio supports connecting to SQL Server using Kerberos.

In order to use Integrated Authentication (Windows Authentication) on macOS or Linux, you need to set up a **Kerberos ticket** linking your current user to a Windows domain account.

## Prerequisites

- Access to a Windows domain-joined machine in order to query your Kerberos Domain Controller.
- SQL Server should be configured to allow Kerberos authentication. For the client driver running on Unix, integrated authentication is only supported using Kerberos. For more information, see [Using Kerberos integrated authentication to connect to SQL Server](../connect/jdbc/using-kerberos-integrated-authentication-to-connect-to-sql-server.md). There should be SPNs registered for each instance of Sql Server you are trying to connect to. For more information, see [Registering a Service Principal Name](/previous-versions/sql/sql-server-2008-r2/ms191153(v=sql.105)#SPN%20Formats).


## Checking if Sql Server has Kerberos Setup

Login to the host machine of Sql Server. From Windows Command Prompt, use the `setspn -L %COMPUTERNAME%` to list all the Service Principal Names for the host. You should see entries that begin with MSSQLSvc/HostName.Domain.com which means that Sql Server has registered an SPN and is ready to accept Kerberos authentication. 
- If you don't have access to the Host of the Sql Server, then from any other Windows OS joined to the same Active Directory, you could use the command `setspn -L <SQLSERVER_NETBIOS>` where <SQLSERVER_NETBIOS> is the computer name of the host of the Sql Server.


## Get the Kerberos Key Distribution Center

Find the Kerberos KDC (Key Distribution Center) configuration value. Run the following command on a Windows computer that is joined to your Active Directory Domain: 

Start `cmd.exe` and run `nltest`.

```
nltest /dsgetdc:DOMAIN.COMPANY.COM (where "DOMAIN.COMPANY.COM" maps to your domain's name)

Sample Output
DC: \\dc-33.domain.company.com
Address: \\2111:4444:2111:33:1111:ecff:ffff:3333
...
The command completed successfully
```
Copy the DC name that is the required KDC configuration value, in this case dc-33.domain.company.com

## Join your OS to the Active Directory Domain Controller

### Ubuntu
```bash
sudo apt-get install realmd krb5-user software-properties-common python-software-properties packagekit
```

Edit the `/etc/network/interfaces` file so that your AD domain controller's IP address is listed as a dns-nameserver. For example: 

```/etc/network/interfaces
<...>
# The primary network interface
auth eth0
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

Now check that your `/etc/resolv.conf` file contains a line like the following:  

```Code
nameserver **<AD domain controller IP address>**
```

```bash
sudo realm join contoso.com -U 'user@CONTOSO.COM' -v
<...>
* Success
```
   
### RedHat Enterprise Linux
```bash
sudo yum install realmd krb5-workstation
```

Edit the `/etc/sysconfig/network-scripts/ifcfg-eth0` file (or other interface config file as appropriate) so that your AD domain controller's IP address is listed as a DNS server:

```/etc/sysconfig/network-scripts/ifcfg-eth0
<...>
PEERDNS=no
DNS1=**<AD domain controller IP address>**
```

After editing this file, restart the network service:

```bash
sudo systemctl restart network
```

Now check that your `/etc/resolv.conf` file contains a line like the following:  

```Code
nameserver **<AD domain controller IP address>**
```

```bash
sudo realm join contoso.com -U 'user@CONTOSO.COM' -v
<...>
* Success
   
```

### macOS

- Join your macOS to the Active Directory Domain Controller by following these steps:

## Configure KDC in krb5.conf

Edit the `/etc/krb5.conf` in an editor of your choice. Configure the following keys

```bash
sudo vi /etc/krb5.conf

[libdefaults]
  default_realm = DOMAIN.COMPANY.COM
 
[realms]
DOMAIN.COMPANY.COM = {
   kdc = dc-33.domain.company.com
}
```

Then save the krb5.conf file and exit

> [!NOTE]
> Domain must be in ALL CAPS


## Test the Ticket Granting Ticket retrieval

Get a Ticket Granting Ticket (TGT) from KDC.

```bash
kinit username@DOMAIN.COMPANY.COM
```

View the available tickets using klist. If the kinit was successful, you should see a ticket. 

```bash
klist

krbtgt/DOMAIN.COMPANY.COM@ DOMAIN.COMPANY.COM.
```

## Connect using Azure Data Studio

* Create a new connection profile

* Choose **Windows Authentication** as the authentication type

* Complete the connection profile, click **Connect**

After successfully connecting, your server appears in the *Servers* sidebar.