---
# required metadata

title: Install SQL Server on Linux (Ubuntu) | SQL Server vNext CTP1
description: 
author: rothja 
ms.author: jroth 
manager: jhubbard
ms.date: 10-18-2016
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
# Install SQL Server on Linux (Ubuntu)

This topic provides a walkthrough of how to install SQL Server vNext CTP1 on Ubuntu 16.04.

## Install SQL Server
First, install the mssql-server Package on Ubuntu.

1. Enter superuser mode.

        $ sudo su

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

## TODO
Section for upgrade
Section for installing tools

## Next steps
TBD   
