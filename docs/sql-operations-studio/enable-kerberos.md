---
title: Use Active Directory Authentication (Kerberos) when connecting with SQL Operations Studio (preview) | Microsoft Docs
description: Learn how to enable Kerberos to use Active Directory Authentication for SQL Operations Studio (preview)
keywords: 
ms.custom: "tools|sos"
ms.date: "11/08/2017"
ms.prod: "sql-non-specified"
ms.reviewer: "alayu; erickang; sstein"
ms.suite: "sql"
ms.tgt_pltfrm: ""
ms.topic: "article"
author: "meet-bhagdev"
ms.author: "meetb"
manager: craigg
ms.workload: "Inactive"
---
# Connect [!INCLUDE[name-sos](../includes/name-sos-short.md)] to your SQL Server using Windows authentication - Kerberos 

[!INCLUDE[name-sos](../includes/name-sos-short.md)] supports connecting to SQL Server using Kerberos.

In order to use Integrated Authentication (Windows Authentication) on macOS or Linux, you need to set up a **Kerberos ticket** linking your current user to a Windows domain account. 

## Get the Kerberos Key Distribution Center

Find the Kerberos KDC (Key Distribution Center) configuration value. Run the following command on a Windows computer that is joined to your Active Directory Domain: 

Start `cmd.exe` and run `nltest`.

```
nltest /dsgetdc:DOMAIN.COMPANY.COM (where “DOMAIN.COMPANY.COM” maps to your domain’s name)

Sample Output
DC: \\dc-33.domain.company.com
Address: \\2111:4444:2111:33:1111:ecff:ffff:3333
...
The command completed successfully
```
Copy the DC name that is the required KDC configuration value, in this case dc-33.domain.company.com

## Install the required packages

### Ubuntu

```bash 
sudo apt-get krb5-user
```

### RHEL
```bash 
sudo yum install realmd krb5-workstation
```

### SUSE
```bash 
sudo zypper install realmd krb5-client
```

### macOS
- Kerberos should be installed on your macOS. If your macOS does not have Kerberos installed, you can get it from the [download page](http://web.mit.edu/macdev/KfM/Common/Documentation/download.html).


## Configuring KDC in krb5.conf

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


## Testing the Ticket Granting Ticket retrieval

Get a Ticket Granting Ticket (TGT) from KDC.

```bash
kinit username@DOMAIN.COMPANY.COM
```

View the available tickets using kinit. If the kinit was successful, you should see a ticket. 

```bash
klist

krbtgt/DOMAIN.COMPANY.COM@ DOMAIN.COMPANY.COM.
```

## Connect using [!INCLUDE[name-sos](../includes/name-sos-short.md)]

* Create a new connection profile

* Choose **Windows Authentication** as the authentication type

* Complete the connection profile, click **Connect**

After succssfully connecting, your server appears in the Servers sidebar.
