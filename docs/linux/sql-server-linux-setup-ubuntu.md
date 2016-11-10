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
First, install the mssql-server Package on Ubuntu.

1. Enter superuser mode.

        $ sudo su

    > [!IMPORTANT]
    > The following steps will change when we get ready to release. They will look something like this. There will be steps for importing the public keys and registering the repository before the apt-get update / apt-get install commands. 

2. Download the configuration script and make it executable.

        # curl -O https://private-repo.microsoft.com/tools/configure-mssql-repo-2.sh
        # chmod a+x configure-mssql-repo-2.sh

3. Pass the unique URL provided for you in your invitation email as a parameter to the script. This URL is labeled as **“Package script configuration URL parameter”**:

        # ./configure-mssql-repo-2.sh <URL provided in email>

4. Exit superuser mode.

        # exit

5. Run the following commands to install SQL Server:

        $ sudo apt-get update
        $ sudo apt-get install -y mssql-server

6. You will receive a prompt to accept the End-User License Agreement. Select “Yes” to continue the installation.

7. Enter the System Administrator password for the instance. The username for the System Administrator account is ‘sa’. 

8. Confirm the System Administrator password.

> [!NOTE]
> Alternatively, you can run this command to install without being prompted for the license agreement and the System Administrator password:
>
>    $ sudo ACCEPT_EULA=Y MSSQL_SERVER_SA_PASSWORD=\<your pwd\> apt-get install mssql-server -y

## Verify the installation
Use the following command to print the installed package name and version:

    $ dpkg-query -W | grep mssql

## <a id="tools"></a> Install the tools
The following steps installs the command-line tools, Microsoft ODBC drivers, and their dependencies. For the details on what gets installed, see [Command-line tools and ODBC drivers](sql-server-linux-setup.md#tools).

> [!NOTE]
> If you install these packages from the same Linux machine as your SQL Server installation, you do NOT need to perform steps 1-2 which were done as part of the SQL Server installation.

> [!IMPORTANT]
> The following steps will change when we get ready to release.

2. Download the configuration script and make it executable.

        # curl -O https://private-repo.microsoft.com/tools/configure-mssql-repo-2.sh
        # sudo chmod a+x configure-mssql-repo-2.sh

3. Pass the unique URL provided for you in your invitation email as a parameter to the script. This URL is labeled as **“Package script configuration URL parameter”**:

        # sudo ./configure-mssql-repo-2.sh <URL provided in email>

4. Add the private preview tools repository, and install the SQL Command Line Utilities with its dependencies by running the commands below.

        echo "deb https://private-repo.microsoft.com/ubuntu/mssql-tools-private-preview mssql main" | sudo tee /etc/apt/sources.list.d/mssql-tools.list 

5. Update the sources list and run the installation command:

        $ sudo apt-get update 
        $ sudo apt-get install mssql-tools

If you are install these packages from the same Linux machine as your SQL Server installation, you can run a test by connecting to your local SQL Server instance (localhost) with your **SA** username and password:

    $ sqlcmd -S localhost -U SA -P <password>

Type **exit** to return to the command-line.

## Next steps
After installation, connect to the SQL Server instance to create and manage databases. To get started, see [Connect and query SQL Server on Linux](sql-server-linux-connect-and-query.md).
