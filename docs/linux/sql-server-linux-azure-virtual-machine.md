---
# required metadata

title: Provision a Linux VM in Azure for SQL Server - SQL Server vNext CTP1 | Microsoft Docs
description: 
author: rothja 
ms.author: jroth 
manager: jhubbard
ms.date: 11/16/2016
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
Azure provides Linux virtual machine images that have SQL Server vNext CTP1 installed. This topic provides a short walkthrough on how to use the SQL Server virtual machine image for Red Hat Enterprise Linux. 

## Create a Red Hat Enterprise Linux VM with SQL Server installed

Open the [Azure portal](https://portal.azure.com/).

1. Click **New** on the left.

2. In the **New** blade, click **Compute**

3. Click **See All** next to the **Featured Apps** heading.

   ![See all VM images](./media/sql-server-linux-azure-virtual-machine/azure-compute-blade.png)

4. Select **SQL Server vNext on Red Hat Enterprise Linux 7.2**.

5. Click **Create** and fill in the settings.

## <a id="putty"></a> Connect to the VM using SSH/Putty

If you already use a BASH shell, connect to the Azure VM using the **ssh** command. Replace the example machine user name and IP address with ones that match your Linux VM in the command below. You can find the IP address of your VM in the Azure portal.   

    ```bash
    ssh -l AzureAdmin 100.55.555.555
    ```

If you are running on Windows and do not have a BASH shell, you can install an SSH client, such as PuTTY.

1. [Download and install PuTT](http://www.chiark.greenend.org.uk/~sgtatham/putty/download.html).

2. Run PuTTY.

3. On the PuTTY configuration screen enter your VM's public IP address.

4. Click Open and enter your username and password at the prompts.

For more information about connecting to Linux VMs, see [Create a Linux VM on Azure using the Portal](https://docs.microsoft.com/azure/virtual-machines/virtual-machines-linux-quick-create-portal#ssh-to-the-vm).

## Configure SQL Server on the VM

1. Connect to the Linux virtual machine you just created. The public IP address of your VM is displayed in the properties for your VM in the Azure portal.

   > [!TIP]
   > If you're not sure how to connect, see the section of this Azure tutorial that explains There is also information at the end of this article on how to use [Putty/SSH](#putty) for a Windows client.  

2. Navigate to the SQL Server directory

   ```bash
   cd /opt/mssql/bin
   ```
   
3. Setup SQL Server

   ```bash
   sudo ./sqlservr-setup 
   ```
   
   Accept the License and enter a password for the system administrator account. You can start the server when prompted.

   > [!NOTE] 
   > The firewall on the VM is already open but you need to open it at the azure level. There is information on how to do this [here](https://docs.microsoft.com/azure/virtual-machines/virtual-machines-windows-nsg-quickstart-portal).

4. Install SQL Server Tools using the instructions [here](sql-server-linux-setup-tools.md#RHEL).

5. Connect to your localhost with your SA username and password. For more information, see [Connect and query SQL Server on Linux with sqlcmd](sql-server-linux-connect-and-query-sqlcmd.md).
   
## Resources
For more information about Linux virtual machines in Azure, see the [Linux Virtual Machine Documentation](https://docs.microsoft.com/en-us/azure/virtual-machines/linux/).
