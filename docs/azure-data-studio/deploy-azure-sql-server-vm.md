---
title: Deploy SQL Server on Azure Virtual Machine
description: This tutorial shows how you can create SQL Server on Azure Virtual Machines
author: ninarn
ms.author: ninarn
ms.reviewer: erinstellato
ms.date: 10/14/2020
ms.service: azure-data-studio
ms.topic: tutorial
ms.custom: intro-deployment
---

# Create SQL Server on Azure Virtual Machines using Azure Data Studio

You can create an SQL virtual machine (VM) using Azure Data Studio through the deployment wizard and notebooks.

## Pre-requisites

- [Azure Data Studio](download-azure-data-studio.md) is installed
- An active Azure account and subscription. If you don't have one, [create a free account](https://azure.microsoft.com/free/).

## Use the deployment wizard

Follow these steps to use the deployment wizard, which will guide you through the required settings in a simple UI experience.

First, find, and select Azure SQL VM in the deployment wizard.

1. In Azure Data Studio, select the Connections viewlet on the left-side navigation.

2. Select the **...** button at the top of the Connections panel and choose **New Deployment.**

3. In the deployment wizard, select the **Azure SQL VM** tile and check the terms acceptance checkbox

4. If prompted to, install the required tools and then select the **Select** button at the bottom.

Next, enter all of the required parameters in the Azure SQL VM wizard.

1. Sign in to your Azure account if you'ven't already. You can refresh your connection if you've issues on this page of the wizard.

2. Select your desired subscription, resource group, and region. Then select **Next**.

3. Enter a unique Virtual Machine name and your username and password credentials.

4. Select your preferred image, SKU, and version, and then select your preferred VM size. You can learn more about [available VM sizes](/azure/virtual-machines/sizes) to help you make your selection. Then select **Next**.

5. Either select an existing virtual network from the dropdown, or check the **New virtual network** checkbox to enter a name for a new virtual network.

6. Do the same for selecting or creating a subnet and public IP address.

7. If you would like to connect to your VM via Remote Desktop (RDP), check the **Enable RDP inbound port** checkbox. Then select **Next**.

8. Enter your preferred SQL Server settings. The recommendation for testing out this experience is to set SQL connectivity to **Public (internet)**, enter port 1433, and enable SQL authentication with your preferred username and password. Then select **Next**.

9. Review the parameters you've entered and then select **Script to Notebook**.

Once the Notebook opens, you can review the content and the code and make changes if you like. However making changes isn't recommended since it could cause validation errors.

The last step is to select **Run all** to run all cells in the Notebook. Once this completes, you should have a fully created and running:

- An Azure virtual machine
- A SQL virtual machine
- A Virtual Network, subnet, and public IP address
- A network security group and a network interface
- Access to RDP into the VM
- Access to a variety of SQL Server manageability features to easily control the Backup schedule, patching schedule, the SQL Server edition and licensing, the storage performance configurations, and much more.

## Next steps

To learn more about how to migrate your data to the new SQL VM, see the following article.

> [!div class="nextstepaction"]
> [Migrate a database to a SQL VM](/azure/azure-sql/virtual-machines/windows/migrate-to-vm-from-sql-server)

For other information about using SQL Server in Azure, see [SQL Server on Azure Virtual Machines](/azure/azure-sql/virtual-machines/windows/sql-server-on-azure-vm-iaas-what-is-overview) and the [Frequently Asked Questions](/azure/azure-sql/virtual-machines/windows/frequently-asked-questions-faq).
