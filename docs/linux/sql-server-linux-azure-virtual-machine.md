---
# required metadata

title: Provision a Linux VM in Azure for SQL Server | SQL Server vNext CTP1
description: 
author: rothja 
ms.author: jroth 
manager: jhubbard
ms.date: 11/03/2016
ms.topic: article
ms.prod: sql-linux
ms.technology: database-engine
ms.assetid: 222e23b2-51e7-429b-b8e5-61e0ebe7df9b

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
# Provision a Linux VM in Azure for SQL Server

## Create a Red Hat Enterprise Linux VM with SQL Server installed

Open the Azure portal

1. Click *New* on the left

2. Click *Virtual Machines*

3. Click *See All* under Featured Apps

4. Click on *SQL Server vNext on Red Hat Enterprise Linux 7.2*

5. Click *Create* and fill in the settings

## Configure SQL Server on the VM

Navigate to the SQL Server directory

    cd /opt/mssql/bin

Setup SQL Server

    sudo ./sqlservr-setup 

Accept the License and enter a password for the system administrator account. You can start the server when prompted.

> [!NOTE]
> The firewall on the VM is already open but you need to open it at the azure level. 
There is information on how to do this [here.](https://azure.microsoft.com/en-us/documentation/articles/virtual-machines-windows-nsg-quickstart-portal/)


Install SQL Server Tools using the instructions [here](sql-server-linux-setup-red-hat.md#tools)

Connect to your localhost with your SA username and password:

    $ sqlcmd -S localhost -U SA -P <password>

## Connect to a Linux VM using SSH/Putty from a Windows PC

PuTTY is a SSH client for Linux that you can run on your Windows PC.

1. Download and instlal PuTTY from [here](http://www.chiark.greenend.org.uk/~sgtatham/putty/download.html)

2. Run PuTTY

3. On the PuTTY configuration screen enter your VM's public IP address

4. Click Open and enter your username and password at the prompts

## Resources
Find Linux Virtual Machines documentation [here](https://azure.microsoft.com/en-us/documentation/services/virtual-machines/linux/)
