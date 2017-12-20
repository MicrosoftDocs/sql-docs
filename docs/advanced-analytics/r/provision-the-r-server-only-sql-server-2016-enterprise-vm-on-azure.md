---
title: "Provision a virtual machine for machine learning on Azure | Microsoft Docs"
ms.custom: ""
ms.date: "10/31/2017"
ms.reviewer: 
ms.suite: sql
ms.prod: machine-learning-services
ms.prod_service: machine-learning-services
ms.component: r
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: c8826df7-aa67-4768-baa9-bdc875c4a766
caps.latest.revision: 12
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# Provision a virtual machine for machine learning on Azure

Virtual machines on Azure are a convenient option for quickly configuring a complete server environment for machine learning solutions.

This article lists the virtual machine images that contain SQL Server with machine learning, as well as some related VMs.

This article also provides answers to common questions about modifying or upgrading an existing SQL Server instance in a virtual machine.

+ [List of current virtual machines](#bkmk_list)

## Provision a virtual machine with SQL Server and machine learning

If you are new to using Azure VMs, we recommend that you see these articles for more information about using the portal and configuring a virtual machine.

+ [Virtual Machines - Getting Started](https://azure.microsoft.com/documentation/learning-paths/virtual-machines/)
+ [Getting started with Windows Virtual Machines](https://azure.microsoft.com/documentation/articles/virtual-machines-windows-hero-tutorial/)

Be sure to use the new version of the Azure portal, or the Azure Marketplace. Some images are not available when browsing the Azure Gallery on the classic portal.

**Create the virtual machine**

1. Open the Azure portal: [portal.azure.com](https:portal.azure.com).

2. Click **Virtual Machines**, or click **New**.

3. Click **Add**.

4. In the search box at the top of the page, type "Machine Learning Server", or "SQL Server" to see a list of related virtual machines.

5. Review the requirements, and click **Create** to get started.

6. After the virtual machine has been created and is running, click the **Connect** button to open a connection and log in to the new machine.

5. After you connect, you can install additional R or Python packages, or configure your preferred development tool.

### Connect to the virtual machine

The way a client connects to SQL Server running on a virtual machine differs depending on the location of the client and the networking configuration.

For more information, see [Connect to a SQL Server virtual machine on Azure](https://docs.microsoft.com/azure/virtual-machines/windows/sql/virtual-machines-windows-sql-connect).

## Frequently Asked Questions

This section contains some common questions about machine learning virtual machines from Microsoft.

### Can I install a virtual machine with SQL Server 2017?

A Windows-based virtual machine for SQL Server 2017 Enterprise Edition that includes Machine Learning Services is available beginning November 2017. 

For announcements about new data science virtual machines, watch these blog sites:

+ [Cortana Intelligence and Machine Learning](https://blogs.technet.microsoft.com/machinelearning/)
+ [Data Platform Insider](https://blogs.technet.microsoft.com/dataplatforminsider/)

### Adding SQL Server to an existing virtual machine

In addition to creating a virtual machine using an image that already includes SQL Server machine learning, you can install SQL Server on an existing virtual machine and enable the machine learning features. We recommend Enterprise or Developer edition, to avoid resource constraints. Installation also requires that you use your own license key.

When you run setup, be sure to select the machine learning feature and at least one language (R or Python). Some additional steps are required to enable machine learning services to communicate with SQL Server, and to enable networking on the virtual machine.

For more information, see [Installing SQL Server R Services on an Azure virtual machine](../r/installing-sql-server-r-services-on-an-azure-virtual-machine.md).

### Using machine learning in Azure SQL database

Beginning in fall 2017, Azure SQL Database supports using R to train models and use them for prediction. 

R Services in-database is available as a preview feature only, and has some limitations compared to the on-premises edition of SQL Server. For more information, see [Azure SQL DB](../r/using-r-in-azure-sql-database.md).

### Can I upgrade the SQL Server version on a virtual machine?

Although the SQL Server 2016 images support R, if you want to use Python, you can upgrade to SQL Server 2017, which also upgrades other machine learning components.

To update the version of SQL Server that is installed, open the SQL Server Installation Center on the virtual machine, and select the **Upgrade** option. Depending on which virtual machine you created, a license might be required.

For more information, see [Upgrade SQL Server by using the installation wizard](https://docs.microsoft.com/sql/database-engine/install-windows/upgrade-sql-server-using-the-installation-wizard-setup).

### Can I upgrade just the machine learning components?

When new upgrades are published for RevoScaleR, MicrosoftML, or revoscalepy, you can upgrade the machine learning components used by SQL Server, by using a process known as _binding_. This does not change your SQL Server version, but it does change the support policy for the instance.

For more information, see [Use SqlBindR to upgrade machine learning components on SQL Server](../r/use-sqlbindr-exe-to-upgrade-an-instance-of-sql-server.md).

### How do I access data in an Azure storage account?

When you need to use data from your Azure storage account, there are several options for accessing or moving the data:

+ Copy the data from your storage account to the local file system using a utility, such as [AzCopy](https://azure.microsoft.com/documentation/articles/storage-use-azcopy/#copy-files-in-azure-file-storage-with-azcopy-preview-version-only). 

+ Add the files to a file share on your storage account and then mount the file share as a network drive on your VM. For more information, see [Mounting Azure files](https://azure.microsoft.com/documentation/articles/storage-dotnet-how-to-use-files/). 

### How do I use data from Azure Data Lake Storage (ADLS)?

You can read data from ADLS storage using RevoScaleR, by using webHDFS to reference the storage account the same way that you would an HDFS file system. For more information, see this article: [Using R to perform FileSystem Operations on Azure Data Lake Store](https://blogs.msdn.microsoft.com/microsoftrservertigerteam/2017/03/14/using-r-to-perform-filesystem-operations-on-azure-data-lake-store/).

### I can't find the RRE virtual machine

The "RRE for Windows Virtual Machine" that was previously available in the Azure Marketplace has been replaced by the "Machine Learning Server for Windows" image.

Machine Learning Server images are also available for Linux CentOS version 7.2, Linux RedHat version 7.2, and Ubuntu version 16.04.

For more information, see [Machine Learning Server in the Cloud](https://docs.microsoft.com/machine-learning-server/install/machine-learning-server-in-the-cloud)

### Configuring Machine Learning Server or R Server to support web services

If you use a virtual machine that includes Machine Learning Server, additional configuration might be required to use web service deployment, remote execution, or to use the virtual machine as a deployment server in your organization.

For instructions, see [Configuring Machine Learning Server to operationalize analytics](https://docs.microsoft.com/machine-learning-server/operationalize/configure-machine-learning-server-one-box).

Additional configuration is not needed if you just want to use packages such as RevoScaleR or MicrosoftML.

## <a name="bkmk_list"></a> List of virtual machines

Currently, the following virtual machines are available for machine learning with SQL Server:

|Name| Comments|
|----|----|----|
| **SQL Server 2016**| ***  |
|SQL Server 2016 SP1 Enterprise on Windows|R Services for integrated advanced analytics.|
|BYOL SQL Server 2016 SP1 Enterprise on Windows Server |R Services for integrated advanced analytics. |
|Free License: SQL Server 2016 SP1 Developer on Windows Server 2016 |R Services for integrated advanced analytics. |
| Data Science Virtual Machine - Windows 2012|Contains popular tools for data science, including Microsoft R Server Developer Edition, SQL Server 2016 Developer edition, the Anaconda Python distribution, Julia Pro developer edition, and Jupyter notebooks for R.| 
| Data Science Virtual Machine - Windows 2016|Includes SQL Server 2016 Developer Edition, with support for in-database R analytics.|
|**SQL Server 2017**| ***   |
|SQL Server 2017 Enterprise Windows Server 2016| Machine Learning Services with Python and R language support.|
|BYOL SQL Server 2017 Enterprise Windows Server 2016|Machine Learning Services with Python and R language support.|
| Free SQL Server License: SQL Server 2017 Developer on Windows Server|Machine Learning Services with Python and R language support.|
| **Other**| *** |
| Machine Learning Server Only SQL Server 2017 Enterprise|Similar to the SQL Server 2016 Enterprise image, but contains the standalone version of Machine Learning Server and has the core ScaleR and Operationalization functionality optimized for Windows environments.|
| Machine Learning Server for Windows|Contains the standalone version of Machine Learning Server, with operationalization features optimized for Windows environments.|
|Data Science Virtual Machine |Linux editions include R Server. Linux virtual machines that include SQL Server 2017 cannot run R or Python code in SQL Server. However, you can perform scoring on a trained model using the T-SQL PREDICT function. For more information, see [Native scoring in SQl Server](../sql-native-scoring.md).|
