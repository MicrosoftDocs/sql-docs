---
# required metadata

title: Install SQL Server on Linux (Red Hat) | SQL Server vNext CTP1
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
# Install SQL Server on Linux (Red Hat)

This topic provides a walkthrough of how to install SQL Server vNext CTP1 on Red Hat Enterprise Linux (RHEL) 7.

## Install SQL Server
First, install the mssql-server Package on RHEL.

1. Enter superuser mode.

    $ sudo su

2. Download the configuration script and make it executable.

    # curl -O https://private-repo.microsoft.com/tools/configure-mssql-repo-2.sh
    # chmod a+x configure-mssql-repo-2.sh

3. Pass the unique URL provided for you in your invitation email as a parameter to the script. This URL is labeled as **“Package script configuration URL parameter”**:

    # ./configure-mssql-repo-2.sh \<URL provided in email\>

4. Exit superuser mode.

    # exit

5. Run the following commands to install SQL Server:

    $ sudo yum update
    $ sudo yum install -y mssql-server

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

## TODO
Section for upgrade
Section for installing tools

## Next steps
TBD   



