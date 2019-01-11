---
title: Get started with SQL Server (on Linux) in the Cloud
titleSuffix: SQL Server
description: This quickstart shows how to run SQL Server on Linux in the cloud of your choice.
author: MikeRayMSFT 
ms.author: mikeray 
manager: craigg
ms.date: 10/25/2017
ms.topic: conceptual
ms.prod: sql  
ms.custom: "sql-linux, seodec18"
ms.technology: linux
ms.assetid:
---
# Quickstart: Run SQL Server in the cloud

  > [!div class="nextstepaction"]
  > [Please share your feedback about the SQL Docs Table of Contents!](https://aka.ms/sqldocsurvey)

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-linuxonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-linuxonly.md)]

In this quickstart, you will install SQL Server on Red Hat Enterprise Linux (RHEL), SUSE Linux Enterprise Server (SLES), or Ubuntu in the cloud of your choice. Go to [Provision a Linux SQL Server virtual machine in the Azure portal](https://docs.microsoft.com/azure/virtual-machines/linux/sql/provision-sql-server-linux-virtual-machine?toc=%2fsql%2flinux%2ftoc.json) to run SQL Server on Linux in Azure.

> [!NOTE]
> If you choose to run a paid edition of SQL Server, then you need to bring your own license (BYOL).

## Amazon Web Services
1.	Create a Linux AMI with at least 2 GB of memory from the marketplace 
    * [RHEL 7.3+](https://aws.amazon.com/marketplace/pp/B00KWBZVK6)
    * [SLES v12 SP2](https://aws.amazon.com/marketplace/pp/B00PMM99PI)
    * [Ubuntu 16.04](https://aws.amazon.com/marketplace/pp/B01JBL2M0O)
1.	Connect to the AMI with ssh
1.	Follow the quickstart for the Linux distribution you chose: 
    * [RHEL](quickstart-install-connect-red-hat.md)
    * [SLES](quickstart-install-connect-suse.md)
    * [Ubuntu](quickstart-install-connect-ubuntu.md)
1.	Configure for remote connections: 
    * Open the [Amazon EC2 console]( https://console.aws.amazon.com/ec2/)
    * In the navigation pane, choose **Security Groups**. 
    * Choose **Inbound, Edit, Add Rule**
    * Add an inbound rule to allow traffic on the port on which SQL Server listens (default TCP port 1433)

    
## Digital Ocean
1. Log in to the [control panel](https://cloud.digitalocean.com/login) and click create a droplet
1. Choose a Ubuntu 16.04 droplet with at least 2 GB of memory
1. Connect to the droplet with ssh
1. Follow the [Ubuntu quickstart](quickstart-install-connect-ubuntu.md)
1. Configure for remote connections:
    * At the top of the Control Panel, follow the **Networking** link and then select **Firewalls**
    * Add an inbound rule to allow traffic on the port on which SQL Server listens (default TCP port 1433)
    
## Google Cloud Platform
1.	Create a Linux image with at least 2 GB of memory from the Cloud Launcher 
    * [RHEL 7.3+](https://console.cloud.google.com/launcher/details/rhel-cloud/rhel-7)
    * [SLES v12 SP2](https://console.cloud.google.com/launcher/details/suse-cloud/sles-12)
    * [Ubuntu 16.04](https://console.cloud.google.com/launcher/details/ubuntu-os-cloud/ubuntu-xenial)
1.	Connect to the image with ssh
1.	Follow the quickstart for the Linux distribution you chose: 
    * [RHEL](quickstart-install-connect-red-hat.md)
    * [SLES](quickstart-install-connect-suse.md)
    * [Ubuntu](quickstart-install-connect-ubuntu.md)
1.	Configure for remote connections: 
    * Go to the [Firewall Rules](https://console.cloud.google.com/networking/firewalls)
    * Add an inbound rule to allow traffic on the port on which SQL Server listens (default tcp: 1433)
