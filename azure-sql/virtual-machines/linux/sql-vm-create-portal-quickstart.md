---
title: "Quickstart: Create a Linux SQL Server VM in Azure"
description: This tutorial shows how to create a Linux SQL Server 2017 virtual machine in the Azure portal.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 09/25/2025
ms.service: azure-vm-sql-server
ms.subservice: deployment
ms.topic: quickstart
ms.custom:
  - mode-ui
  - linux-related-content
  - sfi-image-nochange
tags: azure-service-management
---

# Provision a Linux virtual machine running SQL Server in the Azure portal

[!INCLUDE [appliesto-sqlvm](../../includes/appliesto-sqlvm.md)]

> [!div class="op_single_selector"]
> - [Linux](sql-vm-create-portal-quickstart.md)
> - [Windows](../windows/sql-vm-create-portal-quickstart.md)

In this quickstart tutorial, you use the Azure portal to create a Linux virtual machine with SQL Server 2017 installed. You learn the following:

- [Create a Linux VM running SQL Server from the gallery](#create)
- [Connect to the new VM with ssh](#connect)
- [Change the SA password](#password)
- [Configure for remote connections](#remote)

## Prerequisites

If you don't have an Azure subscription, create a [free account](https://azure.microsoft.com/pricing/purchase-options/azure-account?cid=msft_learn) before you begin.

<a id="create"></a>

## Create a Linux VM

1. Sign in to the [Azure portal](https://portal.azure.com/).

1. In the left pane, select **Create a resource**.

1. In the **Create a resource** pane, select **Compute**.

1. Select **See all** next to the **Featured** heading.

   :::image type="content" source="media/sql-vm-create-portal-quickstart/virtual-machine-images.png" alt-text="Screenshot of all VM images.":::

1. In the Operating System filter, select Red Hat or SUSE or Ubuntu as shown above based on your requirement. In the example here, we have shown all three, but you can select one distribution that you prefer.

1. Select specific image that suits your needs.

1. Select **Create**.

### Set up your Linux VM

1. In the **Basics** tab, select your **Subscription** and **Resource Group**.

   :::image type="content" source="media/sql-vm-create-portal-quickstart/basics.png" alt-text="Screenshot of Basics window.":::

1. In **Virtual machine name**, enter a name for your new Linux VM.
1. Then, type or select the following values:
   - **Region**: Select the Azure region that's right for you.
   - **Availability options**: Choose the availability and redundancy option that's best for your apps and data.
   - **Change size**: Select this option to pick a machine size and when done, choose **Select**. For more information about VM machine sizes, see [VM sizes](/azure/virtual-machines/sizes).

     :::image type="content" source="media/sql-vm-create-portal-quickstart/vmsizes.png" alt-text="Screenshot of choosing a VM size." lightbox="media/sql-vm-create-portal-quickstart/vmsizes.png":::

   > [!TIP]  
   > For development and functional testing, use a VM size of **DS2** or higher. For performance testing, use **DS13** or higher.

   - **Authentication type**: Select **SSH public key**.

     > [!NOTE]  
     > You have the choice of using an SSH public key or a Password for authentication. SSH is more secure. For instructions on how to generate an SSH key, see [Create SSH keys on Linux and Mac for Linux VMs in Azure](/azure/virtual-machines/linux/mac-create-ssh-keys).

   - **Username**: Enter the Administrator name for the VM.
   - **SSH public key**: Enter your RSA public key.
   - **Public inbound ports**: Choose **Allow selected ports** and pick the **SSH (22)** port in the **Select public inbound ports** list. In this quickstart, this step is necessary to connect and complete the SQL Server configuration. If you want to remotely connect to SQL Server, you need to manually allow traffic to the default port (1433) used by Microsoft SQL Server for connections over the Internet after the virtual machine is created.

     :::image type="content" source="media/sql-vm-create-portal-quickstart/port-settings.png" alt-text="Screenshot of Inbound ports.":::

1. Make any changes you want to the settings in the following additional tabs or keep the default settings.
   - **Disks**
   - **Networking**
   - **Management**
   - **Guest config**
   - **Tags**

1. Select **Review + create**.
1. In the **Review + create** pane, select **Create**.

<a id="connect"></a>

## Connect to the Linux VM

If you already use a BASH shell, connect to the Azure VM using the **ssh** command. In the following command, replace the VM user name and IP address to connect to your Linux VM.

```bash
ssh azureadmin@40.55.55.555
```

You can find the IP address of your VM in the Azure portal.

:::image type="content" source="media/sql-vm-create-portal-quickstart/vmproperties.png" alt-text="Screenshot of IP address in Azure portal." lightbox="media/sql-vm-create-portal-quickstart/vmproperties.png":::

If you're running on Windows and don't have a BASH shell, install an SSH client, such as PuTTY.

1. [Download and install PuTTY](https://www.chiark.greenend.org.uk/~sgtatham/putty/latest.html).

1. Run PuTTY.

1. On the PuTTY configuration screen, enter your VM's public IP address.

1. Select **Open** and enter your username and password at the prompts.

For more information about connecting to Linux VMs, see [Create a Linux VM on Azure using the Azure portal](/azure/virtual-machines/linux/quick-create-portal).

> [!NOTE]  
> If you see a PuTTY security alert about the server's host key not being cached in the registry, choose from the following options. If you trust this host, select **Yes** to add the key to PuTTy's cache and continue connecting. If you want to carry on connecting just once, without adding the key to the cache, select **No**. If you don't trust this host, select **Cancel** to abandon the connection.

<a id="password"></a>

## Install SQL Server

After your Linux VM is created, you can install SQL Server 2017 or later on the VM. For instructions, see [Install SQL Server on Linux](/sql/linux/install-upgrade/setup).

## Add the tools to your path (optional)

Several SQL Server [packages](/sql/linux/install-upgrade/setup) are installed by default, including the SQL Server command-line tools package. The tools package contains the **sqlcmd** and **bcp** tools. For convenience, you can optionally add the tools path, `/opt/mssql-tools/bin/`, to your `PATH` environment variable.

Run the following commands to modify the `PATH` for both login sessions and interactive/non-login sessions:

```bash
echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bash_profile
echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bashrc
source ~/.bashrc
```

## Register with the SQL IaaS Agent extension

After installing SQL Server, register your Linux VM with the SQL IaaS Agent extension. This extension enables management features for SQL Server on Azure VMs. For instructions, see [Register with the SQL IaaS Agent extension for SQL Server on Azure Virtual Machines](sql-iaas-agent-extension-register-vm-linux.md).


<a id="remote"></a>

## Configure for remote connections

If you need to remotely connect to SQL Server on the Azure VM, you must configure an inbound rule on the network security group. The rule allows traffic on the port on which SQL Server listens (default of 1433). The following steps show how to use the Azure portal for this step.

> [!TIP]  
> If you selected the inbound port **MS SQL (1433)** in the settings during provisioning, these changes have been made for you. You can go to the next section on how to configure the firewall.

1. In the portal, select **Virtual machines**, and then select your SQL Server VM.
1. In the left navigation pane, under **Settings**, select **Networking**.
1. In the Networking window, select **Add inbound port** under **Inbound Port Rules**.

   :::image type="content" source="media/sql-vm-create-portal-quickstart/networking.png" alt-text="Screenshot of Inbound port rules." lightbox="media/sql-vm-create-portal-quickstart/networking.png":::

1. In the **Service** list, select **MS SQL**.

   :::image type="content" source="media/sql-vm-create-portal-quickstart/sqlnsgrule.png" alt-text="Screenshot of MS SQL security group rule.":::

1. Select **OK** to save the rule for your VM.

### Open the firewall on RHEL

This tutorial directed you to create a Red Hat Enterprise Linux (RHEL) VM. If you want to connect remotely to RHEL VMs, you also have to open up port 1433 on the Linux firewall.

1. [Connect](#connect) to your RHEL VM.

1. In the BASH shell, run the following commands:

   ```bash
   sudo firewall-cmd --zone=public --add-port=1433/tcp --permanent
   sudo firewall-cmd --reload
   ```

## Related content

- [Use SSMS on Windows to connect to SQL Server on Linux](/sql/linux/sql-server-linux-develop-use-ssms)
- [Use Visual Studio Code to create and run Transact-SQL scripts for SQL Server](/sql/linux/sql-server-linux-develop-use-vscode)
- [Overview of SQL Server 2017 on Linux](/sql/linux/sql-server-linux-overview)
- [Overview of SQL Server on Linux Azure Virtual Machines](sql-server-on-linux-vm-what-is-iaas-overview.md)
