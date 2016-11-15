---
# required metadata

title: Install SQL Server on Red Hat Enterprise Linux - SQL Server vNext CTP1 | Microsoft Docs
description: 
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
To install the mssql-server Package on Red Hat, follow these steps:

1. Enter superuser mode.

        $ sudo su

2. Download the Microsoft SQL Server Red Hat repository configuration file:

        $ curl https://packages.microsoft.com/config/rhel/7/mssql-server.repo > /etc/yum.repos.d/mssql-server.repo

        This file contains the following repository settings:

        [packages-microsoft-com-mssql-server]
        name=packages-microsoft-com-mssql-server
        baseurl=https://packages.microsoft.com/rhel/7/mssql-server/
        enabled=1

        gpgcheck=1
        gpgkey=https://packages.microsoft.com/keys/microsoft.asc

3. Exit superuser mode.

        # exit

4. Run the following commands to install SQL Server:

        $ sudo yum update
        $ sudo yum install -y mssql-server

5. After the package installation finishes, run the configuration script and follow the prompts to accept the End-User License Agreement, set the initial password, enable the service and confirm if the service is going to run on boot:

        $ sudo /opt/mssql/bin/sqlservr-setup

6. Once the configuration is done, verify that the service is running:

        $ systemctl status mssql-server

7. You will need to open a port on the firewall on RHEL.  If you are using firewalld as your firewall you can use these commands.

        $ sudo firewall-cmd --zone=public --add-port=1433/tcp --permanent
        $ sudo firewall-cmd --reload

## Verify the installation
Use the following command to print the installed package name and version:

    $ rpm -qa | grep mssql

## <a id="tools"></a> Install the tools
The following steps installs the command-line tools, Microsoft ODBC drivers, and their dependencies. For the details on what gets installed, see [Command-line tools and ODBC drivers](sql-server-linux-setup.md#tools).

1. Start a terminal session as root:

        $ sudo su

2. Create a file under /etc/yum.repos.d/mssql-tools.repo with the following contents:

        [mssql-tools]
        name=mssql-tools
        baseurl=https://apt-mo.trafficmanager.net/yumrepos/mssql-rhel7-release/
        enabled=1
        gpgcheck=1
        gpgkey=http://aka.ms/msodbcrhelpublickey/dpgswdist.v1.asc

3. Exit superuser mode:

        # exit

4. Run the following commands to install mssql-tools:

        $ sudo yum update
        $ sudo yum install mssql-tools

If you are install these packages from the same Linux machine as your SQL Server installation, you can run a test by connecting to your local SQL Server instance (localhost) with your **SA** username and password:

    $ sqlcmd -S localhost -U SA -P <password>

Type **exit** to return to the command-line.

## Next steps
After installation, connect to the SQL Server instance to create and manage databases. To get started, see [Connect and query SQL Server on Linux](sql-server-linux-connect-and-query.md).

