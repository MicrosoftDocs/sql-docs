---
title: "Quickstart: Create a Linux SQL Server VM in Azure"
description: This quickstart shows how to create a SQL Server on Linux virtual machine in the Azure portal.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 09/23/2026
ms.service: azure-vm-sql-server
ms.subservice: deployment
ms.topic: quickstart
ms.custom:
  - mode-ui
  - linux-related-content
  - sfi-image-nochange
tags: azure-service-management
ai-usage: ai-assisted
---

# Provision a Linux virtual machine running SQL Server in the Azure portal

[!INCLUDE [appliesto-sqlvm](../../includes/appliesto-sqlvm.md)]

> [!div class="op_single_selector"]
> - [Linux](sql-vm-create-portal-quickstart.md)
> - [Windows](../windows/sql-vm-create-portal-quickstart.md)

In this quickstart, you use the Azure portal to create a Linux virtual machine (VM) with SQL Server installed. You learn how to:

- [Create a Linux VM running SQL Server](#create)
- [Connect to the new VM with SSH](#connect)
- [Verify registration with the SQL IaaS Agent extension](#register)
- [Configure for remote connections](#remote)

> [!NOTE]  
> Deploying SQL Server on Linux VMs by using the Azure portal script-based experience is currently in preview. Precreated SQL Server on Linux Azure Marketplace images are deprecated. Instead, you start from a supported Linux base image, and Azure installs and configures SQL Server during VM provisioning. For more information, see [Azure portal deployment](sql-server-on-linux-vm-what-is-iaas-overview.md#azure-portal-deployment).

## Prerequisites

If you don't have an Azure subscription, create a [free account](https://azure.microsoft.com/pricing/purchase-options/azure-account?cid=msft_learn) before you begin.

<a id="create"></a>

## Create a Linux VM

You can start from the **Create a virtual machine** page in the Azure portal, or from the Azure SQL hub. Both options lead to the same guided SQL Server configuration.

### [Create a virtual machine](#tab/create-vm)

Use this option to start from the standard VM creation experience in the Azure portal.

1. Sign in to the [Azure portal](https://portal.azure.com/).

1. Go to **Virtual machines**, and then select **Create**. You can also select **Create a resource** > **Virtual machine**.

1. On the **Basics** tab, select your **Subscription** and **Resource group**.

   :::image type="content" source="media/sql-vm-create-portal-quickstart/basics.png" alt-text="Screenshot of the Basics tab.":::

1. In **Virtual machine name**, enter a name for your new Linux VM.

1. Enter or select the following values:
   - **Region**: Select the Azure region that's right for you.
   - **Availability options**: Choose the availability and redundancy option that's best for your apps and data.
   - **Image**: Select a supported Linux base image, such as RHEL 9, RHEL 10, Ubuntu 22.04, Ubuntu 24.04, or Ubuntu 26.04.
   - **Size**: Select a VM size. For more information, see [VM sizes](/azure/virtual-machines/sizes).

     :::image type="content" source="media/sql-vm-create-portal-quickstart/vmsizes.png" alt-text="Screenshot of choosing a VM size." lightbox="media/sql-vm-create-portal-quickstart/vmsizes.png":::

   > [!TIP]  
   > For development and functional testing, use a VM size of **DS2** or higher. For performance testing, use **DS13** or higher.

1. Configure the administrator account and inbound ports as described in [Configure authentication and ports](#configure-authentication-and-ports).

1. Go to the **Advanced** tab, and select **Install SQL Server**. The **SQL Server settings** tab becomes available.

1. On the **SQL Server settings** tab, configure your SQL Server deployment as described in [Configure SQL Server settings](#configure-sql-server-settings).

1. Select **Review + create**, review the configuration summary, and then select **Create**.

### [Azure SQL hub](#tab/azure-sql-hub)

Use this option to start from the SQL-first experience, where SQL Server on Linux is a deployment option.

1. Go to the [Azure SQL hub](https://aka.ms/azuresqlhub).

1. Under **SQL Server**, select **SQL Server on Azure Virtual Machines**, and then select **+ Create**.

1. Select **Show options**, and then make the following selections:
   - **Select image offer**: Choose **SQL Server on Linux**.
   - **Select SQL Server version**: Choose a version, such as SQL Server 2025 or SQL Server 2022.
   - **Select distribution**: Choose a supported distribution, such as RHEL 10, RHEL 9, Ubuntu 26.04, Ubuntu 24.04, or Ubuntu 22.04. New supported distributions appear automatically.
   - **Select edition**: Choose an edition that's supported for the selected version.

1. Continue to the **Create a virtual machine** page. The page is prefilled with the SQL Server and operating system settings you selected.

1. On the **Basics** tab, select your **Subscription** and **Resource group**, enter a **Virtual machine name**, and select a **Region** and **Size**.

1. Configure the administrator account and inbound ports as described in [Configure authentication and ports](#configure-authentication-and-ports).

1. Review and complete the remaining SQL Server options, such as licensing, connectivity, and advanced configuration, as described in [Configure SQL Server settings](#configure-sql-server-settings).

1. Select **Review + create**, review the configuration summary, and then select **Create**.

---

Azure provisions the VM, installs and configures SQL Server, and registers the VM with the SQL Server IaaS Agent extension.

### Configure authentication and ports

On the **Basics** tab, configure the following settings:

- **Authentication type**: Select **SSH public key**.

  > [!NOTE]  
  > You can use an SSH public key or a password for authentication. SSH is more secure. For instructions on how to generate an SSH key, see [Create SSH keys on Linux and Mac for Linux VMs in Azure](/azure/virtual-machines/linux/mac-create-ssh-keys).

- **Username**: Enter the administrator name for the VM.
- **SSH public key**: Enter your RSA public key.
- **Public inbound ports**: Choose **Allow selected ports**, and select the **SSH (22)** port in the **Select public inbound ports** list. In this quickstart, you need this step to connect to the VM. To remotely connect to SQL Server, you need to allow traffic to the default SQL Server port (1433) after you create the VM.

  :::image type="content" source="media/sql-vm-create-portal-quickstart/port-settings.png" alt-text="Screenshot of Inbound ports.":::

### Configure SQL Server settings

On the **SQL Server settings** tab, configure the following options. The portal shows only options that are valid for your selected operating system, SQL Server version, and licensing model.

- **SQL Server version**: Choose a version, such as SQL Server 2025 or SQL Server 2022.
- **Edition**: Choose Enterprise, Standard, Developer, Express, or Evaluation. Available editions depend on the selected version.
- **Licensing model**: Choose **Pay-as-you-go** (default) or **Azure Hybrid Benefit**. To choose Azure Hybrid Benefit, you must have a SQL Server license with [License Mobility through Software Assurance](https://azure.microsoft.com/pricing/license-mobility/).
- **Advanced configuration**: You can configure SQL Server options in the portal, or upload a custom `mssql.conf` file. For more information about `mssql.conf` settings, see [Configure SQL Server on Linux with the mssql-conf tool](/sql/linux/sql-server-linux-configure-mssql-conf).

You can also make changes to the settings on the **Disks**, **Networking**, **Management**, **Monitoring**, and **Tags** tabs, or keep the default settings.

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

## Add the tools to your path (optional)

Several SQL Server [packages](/sql/linux/install-upgrade/setup) are installed by default, including the SQL Server command-line tools package. The tools package contains the **sqlcmd** and **bcp** tools. For convenience, you can optionally add the tools path, `/opt/mssql-tools/bin/`, to your `PATH` environment variable.

Run the following commands to modify the `PATH` for both login sessions and interactive/non-login sessions:

```bash
echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bash_profile
echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bashrc
source ~/.bashrc
```

<a id="register"></a>

## Verify registration with the SQL IaaS Agent extension

When you deploy a VM by using the Azure portal deployment experience, Azure automatically registers it with the [SQL Server IaaS Agent extension](sql-server-iaas-agent-extension-linux.md). Registration creates a **SQL virtual machine** resource that's separate from the VM resource. You can then manage the SQL Server license type from Azure.

To verify registration, go to **SQL virtual machines** in the Azure portal and confirm that your VM is listed. For other options, see [Verify registration status](sql-iaas-agent-extension-register-vm-linux.md#verify-registration-status).

You can unregister the VM or register it again after deployment. For instructions, see [Register a Linux SQL Server VM with the SQL Server IaaS Agent extension](sql-iaas-agent-extension-register-vm-linux.md).

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

If you created a Red Hat Enterprise Linux (RHEL) VM and want to connect remotely, you also need to open port 1433 on the Linux firewall.

1. [Connect](#connect) to your RHEL VM.

1. In the BASH shell, run the following commands:

   ```bash
   sudo firewall-cmd --zone=public --add-port=1433/tcp --permanent
   sudo firewall-cmd --reload
   ```

## Related content

- [Use SSMS on Windows to connect to SQL Server on Linux](/sql/linux/sql-server-linux-develop-use-ssms)
- [Use Visual Studio Code to create and run Transact-SQL scripts for SQL Server](/sql/linux/sql-server-linux-develop-use-vscode)
- [Overview of SQL Server on Linux](/sql/linux/sql-server-linux-overview)
- [Overview of SQL Server on Linux Azure Virtual Machines](sql-server-on-linux-vm-what-is-iaas-overview.md)
