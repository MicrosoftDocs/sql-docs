---
# required metadata
title: Install SQL Server on Red Hat Enterprise Linux - SQL Server | Microsoft Docs
description: Describes how to install SQL Server vNext CTP 1.2 on Red Hat Enterprise Linux 7.3.
author: rothja 
ms.author: jroth 
manager: jhubbard
ms.date: 11/16/2016
ms.topic: article
ms.prod: sql-linux
ms.technology: database-engine
ms.assetid: 92503f59-96dc-4f6a-b1b0-d135c43e935e

# optional metadata

# keywords: ""
# ROBOTS: ""
# audience: ""
# ms.devlang: ""
# ms.reviewer: ""
# ms.suite: ""
# ms.tgt_pltfrm: ""
# ms.custom: ""

---
# Install SQL Server on Red Hat Enterprise Linux

This topic provides a walkthrough of how to install SQL Server vNext CTP 1.2 on Red Hat Enterprise Linux (RHEL) 7.3.

> [!NOTE] 
> You need at least 3.25GB of memory to run SQL Server on Linux.
> SQL Server Engine has only been tested up to 256GB of memory at this time.


## Install SQL Server
To install the mssql-server package on RHEL, follow these steps:

1. Enter superuser mode.

   ```bash
   sudo su
   ```

2. Download the Microsoft SQL Server Red Hat repository configuration file:

   ```bash
   curl https://packages.microsoft.com/config/rhel/7/mssql-server.repo > /etc/yum.repos.d/mssql-server.repo
   ```
   
3. Exit superuser mode.

   ```bash
   exit
   ```

4. Run the following commands to install SQL Server:

   ```bash
   sudo yum install -y mssql-server
   ```
   
5. After the package installation finishes, run the configuration script and follow the prompts. Make sure to specify a strong password for the SA account (Minimum length 8 characters, including uppercase and lowercase letters, base 10 digits and/or non-alphanumeric symbols).

   ```bash
   sudo /opt/mssql/bin/sqlservr-setup
   ```

6. Once the configuration is done, verify that the service is running:

   ```bash
   systemctl status mssql-server
   ```
   
7. You may need to open a port on the firewall on RHEL.  If you are using **FirewallD** for your firewall, you can use the following commands.

   ```bash
   sudo firewall-cmd --zone=public --add-port=1433/tcp --permanent
   sudo firewall-cmd --reload
   ```

## Upgrade SQL Server

In order to upgrade the mssql-server package on RHEL, execute the following command:

   ```bash
   sudo yum update mssql-server
   ```

These commands will download the newest package and replace the binaries located under `/opt/mssql/`. The user generated databases and system databases will not be affected by this operation. 

## Uninstall SQL Server

In order to remove the mssql-server package on RHEL, follow these steps:

1. Run the `remove` command. This will delete the package and remove the files under `/opt/mssql/`. However, this command will not affect user-generated and system database files, which are located under `/var/opt/mssql`.
   ```bash
   sudo yum remove mssql-server
   ```

2. Removing the package will not delete the generated database files. If you want to delete the database files use the following command:
   ```bash
   sudo rm -rf /var/opt/mssql/
   ```

## Next steps

- [Install tools](sql-server-linux-setup-tools.md#RHEL)

- If you already have SQL Server tools, [connect to the SQL Server](sql-server-linux-connect-and-query-sqlcmd.md).

