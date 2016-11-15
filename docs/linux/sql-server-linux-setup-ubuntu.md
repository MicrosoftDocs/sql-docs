---
# required metadata

title: Install SQL Server on Linux (Ubuntu) | SQL Server vNext CTP1
description: 
author: rothja 
ms.author: jroth 
manager: jhubbard
ms.date: 10/27/2016
ms.topic: article
ms.prod: sql-linux
ms.technology: database-engine
ms.assetid: 31c8c92e-12fe-4728-9b95-4bc028250d85

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
# Install SQL Server on Linux (Ubuntu)

This topic provides a walkthrough of how to install SQL Server vNext CTP1 on Ubuntu 16.04.

## Install SQL Server
To install the mssql-server Package on Ubuntu, follow these steps:

1. Enter superuser mode.

        $ sudo su

    > [!IMPORTANT]
    > The following steps will change when we get ready to release. They will look something like this. There will be steps for importing the public keys and registering the repository before the apt-get update / apt-get install commands. 

2. Import the public repository GPG keys:

        # curl https://packages.microsoft.com/keys/microsoft.asc | apt-key add -

3. Register the Microsoft SQL Server Ubuntu repository:

        # echo "deb [arch=amd64] https://packages.microsoft.com/ubuntu/16.04/mssql-server xenial main" > /etc/apt/sources.list.d/mssql-server.list

4. Exit superuser mode.

        # exit

5. Run the following commands to install SQL Server:

        $ sudo apt-get update
        $ sudo apt-get install -y mssql-server

6. After the package installation finishes, run the configuration script and follow the prompts to accept the End-User License Agreement, set the initial password, enable the service and confirm if the service is going to run on boot:

        $ sudo /opt/mssql/bin/sqlservr-setup

7. Once the configuration is done, verify that the service is running:

        $ systemctl status mssql-server

## Verify the installation
Use the following command to print the installed package name and version:

    $ dpkg-query -W | grep mssql

## <a id="tools"></a> Install the tools
The following steps installs the command-line tools, Microsoft ODBC drivers, and their dependencies. For the details on what gets installed, see [Command-line tools and ODBC drivers](sql-server-linux-setup.md#tools).

1. Enter superuser mode.

        $ sudo su

2. Register the mssql-tools repository:
        # echo "deb [arch=amd64] https://apt-mo.trafficmanager.net/repos/mssql-ubuntu-xenial-release/ xenial main" > /etc/apt/sources.list.d/mssqlpreview.list

3. Exit superuser mode:

        # exit

4. Import the mssql-tools repository GPG keys:

        $ sudo apt-key adv --keyserver apt-mo.trafficmanager.net --recv-keys 417A0893 

5. Update the sources list and run the installation command:

        $ sudo apt-get update 
        $ sudo apt-get install mssql-tools

If you are install these packages from the same Linux machine as your SQL Server installation, you can run a test by connecting to your local SQL Server instance (localhost) with your **SA** username and password:

    $ sqlcmd -S localhost -U SA -P <password>

Type **exit** to return to the command-line.

## Next steps
After installation, connect to the SQL Server instance to create and manage databases. To get started, see [Connect and query SQL Server on Linux](sql-server-linux-connect-and-query.md).
