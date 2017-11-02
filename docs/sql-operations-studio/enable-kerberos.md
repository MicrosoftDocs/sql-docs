---
title: Use Active Directory Authentication - Kerberos 
description: Learn how to enable Kerberos to use Active Directory Authentication for SQL Workbench
services: sql-database
author: stevestein
ms.author: sstein
manager: craigg
ms.reviewer: achatter, alayu, erickang, sanagama, sstein, meetb
ms.service: data-tools
ms.workload: data-tools
ms.prod: NEEDED
ms.topic: article
ms.date: 10/08/2017
---
# Connect Carbon to your SQL Server using Windows authentication - Kerberos 

Carbon supports connecting to SQL Server using Kerberos.

In order to use Integrated Authentication (aka Windows Authentication) on macOS or Linux you will need to setup a **Kerberos ticket** linking your current user to a Windows domain account. 

# Step1: Get the Kerberos Key Distribution Center

Find the Kerberos KDC (Key Distribution Center) configuration value. Run the following on a Windows PC that is joined to your Active Directory Domain, 

Start `cmd.exe` and run `nltest`.

```
nltest /dsgetdc:DOMAIN.COMPANY.COM (where “DOMAIN.COMPANY.COM” maps to your domain’s name)

Sample Output
DC: \\dc-33.domain.company.com
Address: \\2111:4444:2111:33:1111:ecff:ffff:3333
...
The command completed successfully
```
Copy the DC name which is the required KDC configuration value, in this case dc-33.domain.company.com

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

View the availalbe tickets using kinit. If the kinit was successful, you should see a ticket. 

```bash
klist

krbtgt/DOMAIN.COMPANY.COM@ DOMAIN.COMPANY.COM.
```

## Step 5: Connect using Carbon

* Create a new connection profile

* Choose Integrated as the authentication type

If all goes well and the steps above worked, you should be able to connect successfully!
// Add screenshot



## Next steps
For information about Carbon, see [Carbon Overview](overview.md)
