---
# required metadata

title: Install SQL Server on Linux (Red Hat) | SQL Server vNext CTP1
description: 
author: rothja 
ms.author: jroth 
manager: jhubbard
ms.date: 10-27-2016
ms.topic: article
ms.prod: sql-non-specified
ms.service: 
ms.technology: 
ms.assetid: 

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
# Install SQL Server on Linux (Red Hat)

This topic provides a walkthrough of how to install SQL Server vNext CTP1 on Red Hat Enterprise Linux (RHEL) 7.

## Install SQL Server
To install the mssql-server Package on Ubuntu, follow these steps:

1. Enter superuser mode.

        $ sudo su

    > [!IMPORTANT]
    > The following steps will change when we get ready to release. They will look something like this. There will be steps for importing the public keys and registering the repository before the apt-get update / apt-get install commands. 

2. Register the Microsoft SQL Server Red Hat repository by creating a repository file:

        $ vi /etc/yum.repos.d/mssql-server.repo

        [mssql-server]
        name=mssql-server
        baseurl=https://repo.microsoft.com/rhel7/mssql-server
        gpgcheck=1
        enabled=1
        gpgkey=https://repo.microsoft.com/keys/dpgswdist.v1.asc

3. Exit superuser mode.

        # exit

4. Run the following commands to install SQL Server:

        $ sudo yum update
        $ sudo yum install -y mssql-server

5. After the package installation finishes, run the configuration script and follow the prompts to accept the End-User License Agreement and set the initial password:

        $ sudo /opt/mssql/bin/sqlservr-setup

6. Once the configuration is done, verify that the service is running:

        $ systemctl status mssql-server

## Verify the installation
Use the following command to print the installed package name and version:

    $ rpm -qa | grep mssql

## Post-install configuration
After installation on Red Hat Enterprise Linux you will need to do the following to accept the license agreement and provide the system administrator (SA) password.

1. Run the configuration script to accept the license agreement and provide the System Administrator (SA) password:

        $ cd /opt/mssql/bin
        $ sudo ./sqlservr-setup

2. Enable and start the SQL Server service:

        $ sudo systemctl enable mssql-server
        $ sudo systemctl start mssql-server

3. You will need to open a port on the firewall on RHEL.  If you are using firewalld as your firewall you can use these commands.

        $ sudo firewall-cmd --zone=public --add-port=1433/tcp --permanent
        $ sudo firewall-cmd --reload

## <a id="tools"></a> Install the tools
The following steps installs the command-line tools, Microsoft ODBC drivers, and their dependencies. For the details on what gets installed, see [Command-line tools and ODBC drivers](sql-server-linux-setup.md#tools).

> [!IMPORTANT]
> The following steps will change when we get ready to release.

1. Start a terminal session as root:

        $ sudo su

2. Download an installation script for the SQL Server tools and ODBC drivers installation:

        # wget https://gallery.technet.microsoft.com/ODBC-Driver-13-for-SQL- 8d067754/file/162203/1/install.sh

3. Run the installation script:

        # sh install.sh

4. Once completed, exit the root mode.

        # exit

If you are install these packages from the same Linux machine as your SQL Server installation, you can run a test by connecting to your local SQL Server instance (localhost) with your **SA** username and password:

    $ sqlcmd -S localhost -U SA -P <password>

Type **exit** to return to the command-line.

To uninstall the tools, download the uninstall script and follow the same steps as described above:

    $ sudo su
    # wget https://gallery.technet.microsoft.com/ODBC-Driver-13-Tools-e419eed1/file/153767/2/uninstall.sh
    # sh uninstall.sh 
    # exit 

## Next steps
After installation, connect to the SQL Server instance to create and manage databases. To get started, see [Connect and query SQL Server on Linux](sql-server-linux-connect-and-query.md).

