---
title: Create SQL Server on a Windows virtual machine in the Azure portal
description: This tutorial shows how to create a Windows virtual machine with SQL Server in the Azure portal.
author: bluefooted
ms.author: pamela
ms.reviewer: mathoma
ms.date: 10/14/2022
ms.service: virtual-machines-sql
ms.subservice: deployment
ms.topic: quickstart
ms.custom: mode-ui, ignite-2022
tags: azure-resource-manager
---

# Quickstart: Create SQL Server on a Windows virtual machine in the Azure portal

[!INCLUDE[appliesto-sqlvm](../../includes/appliesto-sqlvm.md)]


> [!div class="op_single_selector"]
> * [Windows](sql-vm-create-portal-quickstart.md)
> * [Linux](../linux/sql-vm-create-portal-quickstart.md)

This quickstart steps through creating a SQL Server virtual machine (VM) in the Azure portal. Follow the article to deploy either a conventional SQL Server on Azure VM, or SQL Server deployed to an [Azure confidential VM](sql-vm-create-confidential-vm-how-to.md). 


  > [!TIP]
  > - This quickstart provides a path for quickly provisioning and connecting to a SQL VM. For more information about other SQL VM provisioning choices, see the [Provisioning guide for SQL Server on Windows VM in the Azure portal](create-sql-vm-portal.md).
  > - If you have questions about SQL Server virtual machines, see the [Frequently Asked Questions](frequently-asked-questions-faq.yml).

## <a id="subscription"></a> Get an Azure subscription

If you don't have an Azure subscription, create a [free account](https://azure.microsoft.com/free/?WT.mc_id=A261C142F) before you begin.

## <a id="select"></a> Select a SQL Server VM image

1. Sign in to the [Azure portal](https://portal.azure.com) using your account.

1. Select **Azure SQL** in the left-hand menu of the Azure portal. If **Azure SQL** is not in the list, select **All services**, then type *Azure SQL* in the search box.
1. Select **+Add** to open the **Select SQL deployment option** page. You can view additional information by selecting **Show details** on the **SQL virtual machines** tile.
1. For conventional SQL Server VMs, select one of the versions labeled **Free SQL Server License...** from the drop-down. For confidential VMs, choose the `SQL Server 2019 Enterprise on Windows Server 2022 Database Engine Only` image from the drop-down. 

   ![Screenshot that shows where you select the Free SQL Server License: SQL Server 2017 Developer on Windows Server 2016 image.](./media/sql-vm-create-portal-quickstart/select-sql-2017-vm-image.png)

1. Select **Create**.

   ![New search window](./media/sql-vm-create-portal-quickstart/create-sql-2017-vm-image.png)

## <a id="configure"></a> Provide basic details

The instructions for basic details vary between deploying a conventional SQL Server on Azure VM, and [SQL Server on an Azure confidential VM](sql-vm-create-confidential-vm-how-to.md). 

### [Conventional VM](#tab/conventional-vm)

To deploy a conventional SQL Server on Azure VM, on the **Basics** tab, provide the following information:

1. In the **Project Details** section, select your Azure subscription and then select **Create new** to create a new resource group. Type _SQLVM-RG_ for the name.

   ![Subscription](./media/sql-vm-create-portal-quickstart/basics-project-details.png)

1. Under **Instance details**:
    1. Type _SQLVM_ for the **Virtual machine name**. 
    1. Choose a location for your **Region**. 
    1. For the purpose of this quickstart, leave **Availability options** set to _No infrastructure redundancy required_. To find out more information about availability options, see [Availability](/azure/virtual-machines/availability). 
    1. In the **Image** list, select the image with the version of SQL Server and operating system you want. For example, you can use an image with a label that begins with _Free SQL Server License:_.
    1. Choose to **Change size** for the **Size** of the virtual machine and select the **A2 Basic** offering. Be sure to clean up your resources once you're done with them to prevent any unexpected charges. 

   ![Instance details](./media/sql-vm-create-portal-quickstart/basics-instance-details.png)

1. Under **Administrator account**, provide a username, such as _azureuser_ and a password. The password must be at least 12 characters long and meet the [defined complexity requirements](/azure/virtual-machines/windows/faq#what-are-the-password-requirements-when-creating-a-vm-).

   ![Administrator account](./media/sql-vm-create-portal-quickstart/basics-administrator-account.png)

1. Under **Inbound port rules**, choose **Allow selected ports** and then select **RDP (3389)** from the drop-down. 

   ![Inbound port rules](./media/sql-vm-create-portal-quickstart/basics-inbound-port-rules.png)

### [Confidential VM](#tab/confidential-vm)

To deploy your SQL Server to an Azure confidential VM, on the **Basics** tab, provide the following information:

1. In the **Project Details** section, select your Azure subscription and then select **Create new** to create a new resource group. Type _SQLVM-RG_ for the name.

   ![Subscription](./media/sql-vm-create-portal-quickstart/basics-project-details.png)

1. Under **Instance details**:
    1. Type _SQLVM_ for the **Virtual machine name**. 
    1. Choose a location for your **Region**. To validate region supportability, look for the `ECadsv5-series` or `DCadsv5-series` in [VM products Available by Azure region](https://azure.microsoft.com/explore/global-infrastructure/products-by-region/?products=virtual-machines). 
    1. For **Security type**, choose **Confidential virtual machines** from the drop-down. If this option is grayed out, it's likely the chosen region does not currently support confidential VMs. Choose a different region from the drop-down. 
    1. For the purpose of this quickstart, leave **Availability options** set to _No infrastructure redundancy required_. To find out more information about availability options, see [Availability](/azure/virtual-machines/availability). 
    1. In the **Image** list, choose the `SQL Server 2019 Enterprise on Windows Server 2022 Database Engine Only` image. To change the SQL Server image, select **See all images** and then filter by **Security type = Confidential VMs** to identify all SQL Server images that support confidential VMs. 
    1. Leave the size at the default of `Standard_EC2ads_v5`. However, to see all available sizes, select **See all sizes** to identify all the VM sizes that support confidential VMs, as well as the sizes that do not. 

   :::image type="content" source="media/sql-vm-create-portal-quickstart/basic-instance-details-confidential.png" alt-text="Screen shot of the Azure portal showing instance details.":::


1. Under **Administrator account**, provide a username, such as _azureuser_ and a password. The password must be at least 12 characters long and meet the [defined complexity requirements](/azure/virtual-machines/windows/faq#what-are-the-password-requirements-when-creating-a-vm-).

   :::image type="content" source="media/sql-vm-create-portal-quickstart/basics-administrator-account.png" alt-text="Screen shot of the Azure portal, Administrator account":::


1. Under **Inbound port rules**, choose **Allow selected ports** and then select **RDP (3389)** from the drop-down. 

   :::image type="content" source="media/sql-vm-create-portal-quickstart/basics-inbound-port-rules.png" alt-text="Screen shot of the Azure portal, Inbound port rules.":::

### Disks

Configure confidential OS disk encryption. This is optional for test VMs but recommended for production environments. For greater details, review the [Quickstart: Deploy a confidential VM](/azure/confidential-computing/quick-create-confidential-vm-portal-amd#create-confidential-vm). 

1. On the tab **Disks**, configure the following settings:

    1. Under **Disk options**, enable **Confidential compute encryption** if you want to encrypt your VM's OS disk during creation.
    1. For **Confidential compute encryption type**, select the type of encryption to use.     
    1. If **Confidential disk encryption with a customer-managed key** is selected, create a **Confidential disk encryption set** before creating your confidential VM. 

1. (Optional) If necessary, create a **Confidential disk encryption set** as follows.

    1. [Create an Azure Key Vault](/azure/key-vault/general/quick-create-portal). For the pricing tier, select **Premium (includes support for HSM backed keys)**. Or, create [create an Azure Key Vault managed Hardware Security Module (HSM)](/azure/key-vault/managed-hsm/quick-create-cli).
        
    1. In the Azure portal, search for and select **Disk Encryption Sets**. 

    1. Select **Create**. 

    1. For **Subscription**, select which Azure subscription to use. 

    1. For **Resource group**, select or create a new resource group to use.
    
    1. For **Disk encryption set name**, enter a name for the set.

    1. For **Region**, select an available Azure region. 

    1. For **Encryption type**, select **Confidential disk encryption with a customer-managed key**.

    1. For **Key Vault**, select the key vault you already created. 

    1. Under **Key Vault**, select **Create new** to create a new key.

        > [!NOTE]
        > If you selected an Azure managed HSM previously, [use PowerShell or the Azure CLI to create the new key](/azure/confidential-computing/quick-create-confidential-vm-arm-amd) instead.

    1. For **Name**, enter a name for the key.

    1. For the key type, select **RSA-HSM**

    1. Select your key size

    1. Select **Create** to finish creating the key.

    1. Select **Review + create** to create new disk encryption set. Wait for the resource creation to complete successfully.
 
    1. Go to the disk encryption set resource in the Azure portal.

    1. Select the pink banner to grant permissions to Azure Key Vault. 

        > [!IMPORTANT]
        > You must perform this step to successfully create the confidential VM.

--- 

## SQL Server settings

On the **SQL Server settings** tab, configure the following options:

1. Under **Security & Networking**, select _Public (Internet_) for **SQL Connectivity** and change the port to `1401` to avoid using a well-known port number in the public scenario. 
1. Under **SQL Authentication**, select **Enable**. The SQL login credentials are set to the same user name and password that you configured for the VM. Use the default setting for [**Azure Key Vault integration**](azure-key-vault-integration-configure.md). **Storage configuration** is not available for the basic SQL Server VM image, but you can find more information about available options for other images at [storage configuration](storage-configuration.md#new-vms).  

   ![SQL server security settings](./media/sql-vm-create-portal-quickstart/sql-server-settings.png)


1. Change any other settings if needed, and then select **Review + create**. 

   ![Review + create](./media/sql-vm-create-portal-quickstart/review-create.png)


## Create the SQL Server VM

On the **Review + create** tab, review the summary, and select  **Create** to create SQL Server, resource group, and resources specified for this VM.

You can monitor the deployment from the Azure portal. The **Notifications** button at the top of the screen shows basic status of the deployment. Deployment can take several minutes. 

## Connect to SQL Server

1. In the portal, find the **Public IP address** of your SQL Server VM in the **Overview** section of your virtual machine's properties.

1. On a different computer connected to the Internet, open [SQL Server Management Studio (SSMS)](/sql/ssms/download-sql-server-management-studio-ssms).


1. In the **Connect to Server** or **Connect to Database Engine** dialog box, edit the **Server name** value. Enter your VM's public IP address. Then add a comma and add the custom port (**1401**) that you specified when you configured the new VM. For example, `11.22.33.444,1401`.

1. In the **Authentication** box, select **SQL Server Authentication**.

1. In the **Login** box, type the name of a valid SQL login.

1. In the **Password** box, type the password of the login.

1. Select **Connect**.

    ![ssms connect](./media/sql-vm-create-portal-quickstart/ssms-connect.png)

## <a id="remotedesktop"></a> Log in to the VM remotely

Use the following steps to connect to the SQL Server virtual machine with Remote Desktop:

[!INCLUDE [Connect to SQL Server VM with remote desktop](../../includes/virtual-machines-sql-server-remote-desktop-connect.md)]

After you connect to the SQL Server virtual machine, you can launch SQL Server Management Studio and connect with Windows Authentication using your local administrator credentials. If you enabled SQL Server Authentication, you can also connect with SQL Authentication using the SQL login and password you configured during provisioning.

Access to the machine enables you to directly change machine and SQL Server settings based on your requirements. For example, you could configure the firewall settings or change SQL Server configuration settings.

## Clean up resources

If you do not need your SQL VM to run continually, you can avoid unnecessary charges by stopping it when not in use. You can also permanently delete all resources associated with the virtual machine by deleting its associated resource group in the portal. This permanently deletes the virtual machine as well, so use this command with care. For more information, see [Manage Azure resources through portal](/azure/azure-resource-manager/management/manage-resource-groups-portal).


## Next steps

In this quickstart, you created a SQL Server virtual machine in the Azure portal. To learn more about how to migrate your data to the new SQL Server, see the following article.

> [!div class="nextstepaction"]
> [Migration guide: SQL Server to SQL Server on Azure Virtual Machines](../../migration-guides/virtual-machines/sql-server-to-sql-on-azure-vm-individual-databases-guide.md)
