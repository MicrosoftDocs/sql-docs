---
# required metadata
title: Install SQL Server on Red Hat Enterprise Linux - SQL Server vNext CTP1 | Microsoft Docs
description: Describes how to install SQL Server vNext CTP1 on Red Hat Enterprise Linux 7.2.
author: rothja 
ms.author: jroth 
manager: jhubbard
ms.date: 11/15/2016
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

This topic provides a walkthrough of how to install SQL Server vNext CTP1 on Red Hat Enterprise Linux (RHEL) 7.2.

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
   
5. After the package installation finishes, run the configuration script and follow the prompts.

   ```bash
   sudo /opt/mssql/bin/sqlservr-setup
   ```

6. Once the configuration is done, verify that the service is running:

   ```bash
   systemctl status mssql-server
   ```
   
7. You may need to open a port on the firewall on RHEL.  If you are using firewalld as your firewall you can use these commands.

   ```bash
   sudo firewall-cmd --zone=public --add-port=1433/tcp --permanent
   sudo firewall-cmd --reload
   ```

## Next steps

- [Install tools](sql-server-linux-setup-tools.md#RHEL)

- If you already have SQL Server tools, [connect to the SQL Server](sql-server-linux-connect-and-query-sqlcmd.md).

