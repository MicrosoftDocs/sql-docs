---
title: Use Active Directory Authentication (Kerberos) when connecting with SQL Operations Studio | Microsoft Docs
description: Learn how to enable Kerberos to use Active Directory Authentication for SQL Operations Studio
keywords: 
ms.custom: "tools|sos"
ms.date: "11/01/2017"
ms.prod: "sql-non-specified"
ms.reviewer: "alayu; erickang; sanagama; sstein"
ms.suite: "sql"
ms.tgt_pltfrm: ""
ms.topic: "overview, quickstart, tutorial, or article"
author: "meet-bhagdev"
ms.author: "meetb"
manager: craigg
ms.workload: "Inactive"
---
# Connect [!INCLUDE[name-sos](../includes/name-sos-short.md)] to your SQL Server using Windows authentication - Kerberos 

[!INCLUDE[name-sos](../includes/name-sos-short.md)] supports connecting to SQL Server using Kerberos.

In order to use Integrated Authentication (Windows Authentication) on macOS or Linux you need to set up a **Kerberos ticket** linking your current user to a Windows domain account. 

# Step1: Get the Kerberos Key Distribution Center

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

# Step 2: Install the required packages

## Ubuntu

```bash 
sudo apt-get krb5-user
```

## RHEL
```bash 
sudo yum install realmd krb5-workstation
```

## SUSE
```bash 
sudo zypper install realmd krb5-client
```



## Step 3: Configuring KDC in krb5.conf

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

Note Domain must be in ALL CAPS


## Step 4: Testing the Ticket Granting Ticket retrieval

Get a Ticket Granting Ticket (TGT) from KDC.

```bash
kinit username@DOMAIN.COMPANY.COM
```

View the available tickets using kinit. If the kinit was successful, you should see a ticket. 

```bash
klist

krbtgt/DOMAIN.COMPANY.COM@ DOMAIN.COMPANY.COM.
```

## Step 5: Connect using [!INCLUDE[name-sos](../includes/name-sos-short.md)]

* Create a new connection profile

* Choose Integrated as the authentication type

If all goes well and the preceding steps worked, you should be able to connect successfully!
// Add screenshot



## Next steps
For information about [!INCLUDE[name-sos](../includes/name-sos-short.md)], see [[!INCLUDE[name-sos](../includes/name-sos-short.md)] Overview](overview.md)
