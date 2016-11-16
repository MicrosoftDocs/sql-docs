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
ms.assetid: eff8e226-185f-46d4-a3e3-e18b7a439e63

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
# Install SQL Server tools on Linux

The following steps install the command-line tools, Microsoft ODBC drivers, and their dependencies. The mssql-tools package contains:

* sqlcmd: Command-line query utility.
* bcp: Bulk import-export utility.

Install the tools for your platform.

* [RHEL](#RHEL)
* [Ubuntu](#ubuntu)

## <a name="RHEL">Install tools on RHEL</a>

1. Enter superuser mode.

   ```
   sudo su
   ```
1. Download the Microsoft Red Hat repository configuration file.

   ```bash
   curl https://packages.microsoft.com/config/rhel/7/prod.repo > /etc/yum.repos.d/msprod.repo
   ```

1. Exit superuser mode.

   ```bash
   exit
   ```

1. Run the following commands to install mssql-tools.

   ```bash
   sudo yum install mssql-tools
   ```


## <a name="ubuntu">Install tools on Ubuntu</a>

1. Enter superuser mode.

   ```bash
   sudo su
   ```

1. Import the public repository GPG keys:

   ```bash
   curl https://packages.microsoft.com/keys/microsoft.asc | apt-key add -
   ```

1. Register the Microsoft Ubuntu repository:

   ```bash
   curl https://packages.microsoft.com/config/ubuntu/16.04/prod.list > /etc/apt/sources.list.d/msprod.list
   ```

1. Exit superuser mode:

   ```bash
   exit
   ```

1. Update the sources list and run the installation command:

   ```bash
   sudo apt-get update 
   sudo apt-get install mssql-tools
   ```


## Next steps

After installation, connect to the SQL Server instance to create and manage databases. To get started, see [Connect and query SQL Server on Linux](sql-server-linux-connect-and-query.md).

