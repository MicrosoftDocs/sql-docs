---
title: Get started with SQL Server (on Linux) in the Cloud
titleSuffix: SQL Server
description: Learn how to install SQL Server on Red Hat Enterprise Linux (RHEL), SUSE Linux Enterprise Server (SLES), or Ubuntu in the cloud of your choice.
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 08/17/2022
ms.service: sql
ms.subservice: linux
ms.topic: conceptual
ms.custom: intro-get-started
---
# Quickstart: Run SQL Server in the cloud

[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]

In this quickstart, you will install SQL Server on Red Hat Enterprise Linux (RHEL), SUSE Linux Enterprise Server (SLES), or Ubuntu in the cloud of your choice. Go to [Provision a Linux SQL Server virtual machine in the Azure portal](/azure/azure-sql/virtual-machines/linux/sql-vm-create-portal-quickstart?toc=%252fsql%252ftoc%252ftoc.json) to run SQL Server on Linux in Azure.

> [!NOTE]  
> If you choose to run a paid edition of SQL Server, then you need to bring your own license (BYOL).

## Amazon Web Services

1. Create a Linux AMI with at least 2 GB of memory from the marketplace
   - [RHEL 7.7+](https://aws.amazon.com/marketplace/pp/prodview-ubouaa3jksbmc)
   - [SLES v12 SP5+](https://aws.amazon.com/marketplace/pp/prodview-cx42luwzj5s6s)
   - [Ubuntu 18.04](https://aws.amazon.com/marketplace/pp/prodview-pkjqrkcfgcaog)
1. Connect to the AMI with **ssh**
1. Follow the quickstart for the Linux distribution you chose:
   - [RHEL](quickstart-install-connect-red-hat.md)
   - [SLES](quickstart-install-connect-suse.md)
   - [Ubuntu](quickstart-install-connect-ubuntu.md)
1. Configure for remote connections:
   - Open the [Amazon EC2 console]( https://console.aws.amazon.com/ec2/)
   - In the navigation pane, choose **Security Groups**.
   - Choose **Inbound, Edit, Add Rule**
   - Add an inbound rule to allow traffic on the port on which SQL Server listens (default TCP port 1433)

## Digital Ocean

1. Log in to the [control panel](https://cloud.digitalocean.com/login) and select **Create a droplet**
1. Choose an Ubuntu 18.04 droplet with at least 2 GB of memory
1. Connect to the droplet with **ssh**
1. Follow the [Ubuntu quickstart](quickstart-install-connect-ubuntu.md)
1. Configure for remote connections:
   - At the top of the Control Panel, follow the **Networking** link and then select **Firewalls**
   - Add an inbound rule to allow traffic on the port on which SQL Server listens (default TCP port 1433)

## Google Cloud Platform

1. Create a Linux image with at least 2 GB of memory from the Cloud Launcher
   - [RHEL 7.7+](https://console.cloud.google.com/marketplace/details/rhel-cloud/rhel-7)
   - [SLES v12 SP5](https://console.cloud.google.com/marketplace/details/suse-cloud/sles-12)
   - [Ubuntu 18.04](https://console.cloud.google.com/marketplace/details/ubuntu-os-cloud/ubuntu-bionic)
1. Connect to the image with **ssh**
1. Follow the quickstart for the Linux distribution you chose:
   - [RHEL](quickstart-install-connect-red-hat.md)
   - [SLES](quickstart-install-connect-suse.md)
   - [Ubuntu](quickstart-install-connect-ubuntu.md)
1. Configure for remote connections:
   - Go to the [Firewall Rules](https://console.cloud.google.com/networking/firewalls)
   - Add an inbound rule to allow traffic on the port on which SQL Server listens (default TCP port 1433)
