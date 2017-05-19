---
title: "Provision the R Server Only SQL Server 2016 Enterprise VM on Azure | Microsoft Docs"
ms.custom: ""
ms.date: "04/28/2017"
ms.prod: "r-server"
ms.reviewer: ""
ms.suite: ""
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
# Advanced Analytics Virtual Machines on Azure

Virtual machines on Azure are a convenient option for quickly configuring a complete server environment for machine learning solutions. This topic lists some virtual machine images that contain R Server, SQL Server with machine learning, or other data science tools from Microsoft .

> [!TIP]
> We recommend that you use the new version of the Azure portal, and the Azure Marketplace. Some images are not available when browsing the Azure Gallery on the classic portal.

The Azure Marketplace contains multiple virtual machines that support data science. This list is not intended to be comprehensive, but only provide the names of images that include either Microsoft R Server or SQL Server machine learning services, to facilitate discovery.

## How to Provision a VM

If you are new to using Azure VMs, we recommend that you see these articles for more information about using the portal and configuring a virtual machine.

+ [Virtual Machines - Getting Started](https://azure.microsoft.com/documentation/learning-paths/virtual-machines/)
+ [Getting Started with Windows Virtual Machines](https://azure.microsoft.com/en-us/documentation/articles/virtual-machines-windows-hero-tutorial/)

### Find an image

1. From the Azure dashboard, click **Marketplace**.

    - Click **Intelligence and analytics** or **Databases**, and then type "R" in the **Filter** control to see a list of R Server virtual machines.
    - Other possible strings for the **Filter** control are *data science* and *machine learning*
    - Use the % wildcard in search to find VM names that contain a target string, such as *R* or *Julia*.

2. To get R Server for Windows, select **R Server Only SQL Server 2017 Enterprise**.
  
    [R Server](https://msdn.microsoft.com/microsoft-r/rserver-whats-new) is licensed as a SQL Server Enterprise Edition feature, but version 9.1. is installed as a standalone server and serviced under the Modern Lifecycle support policy.

3. After the VM has been created and is running, click the **Connect** button to open a connection and log into the new machine.

4. After you connect, you might need to install additional R tools or development tools.

### Install additional R tools

By default, Microsoft R Server includes all the R tools installed with a base installation of R, including RTerm and RGui. A shortcut to RGui has been added to the desktop, if you want to get started using R right away.

However, you might wish to install additional R tools, such as RStudio, the R Tools for Visual Studio (RTVS), or Microsoft R Client. See the following links for download locations and instructions:
+ [R Tools for Visual Studio](https://docs.microsoft.com/visualstudio/rtvs/installation)
+ [Microsoft R Client](https://msdn.microsoft.com/microsoft-r/install-r-client-windows)
+ [RStudio for Windows](https://www.rstudio.com/products/rstudio/download/)

After installation is complete, be sure to change the default R runtime location so that all R development tools use the Microsoft R Server libraries.

### Configure server for web services

If the virtual machine includes R Server, additional configuration is required to use web service deployment, remote execution, or leverage R Server as a deployment server in your organization. For instructions, see [Configure R Server for Operationalization](https://msdn.microsoft.com/microsoft-r/operationalize/configuration-initial).

> [!NOTE]
> Additional configuration is not needed if you just want to use packages such as RevoScaleR or MicrosoftML.

## R Server Images

### Microsoft Data Science Virtual Machine

Comes configured with Microsoft R Server, as well as Python (Anaconda distribution), a Jupyter notebook server, Visual Studio Community Edition, Power BI Desktop, the Azure SDK, and SQL Server Express edition.

The Windows version runs on Windows Server 2012, and contains many special tools for modeling and analytics, including CNTK and mxnet, popular R packages such as xgboost, and Vowpal Wabbit.

### Linux Data Science Virtual Machine

Also contains popular tools for data science and development activities, including Microsoft R Open, Microsoft R Server Developer Edition, Anaconda Python, and Jupyter notebooks for Python, R and Julia.

This VM image was recently updated to include JupyterHub, open source software that enables use by multiple users, through different authentication methods, including local OS account authentication, and Github account authentication. JupyterHub is a particularly useful option if you want to run a training class and want all students to share the same server, but use separate notebooks and directories.

Images are provided for Ubuntu, Centos, and Centos CSP.

### R Server Only SQL Server 2017 Enterprise

This virtual machine includes a standalone installer for [R Server 9.1.](https://msdn.microsoft.com/microsoft-r/rserver-whats-new) that supports the new Modern Software Lifecycle licensing model.

 R Server is also available in images for Linux CentOS version 7.2, Linux RedHat version 7.2, and Ubuntu version 16.04.

 > [!NOTE]
 > These virtual machines replace the **RRE for Windows Virtual Machine** that was previously available in the Azure Marketplace.

## SQL Server Images

To use SQL Server R Services, you must install one of the SQL Server Enterprise or Developer edition virtual machines, and add the machine learning service, as described here: [Installing SQL Server R Services on an Azure Virtual Machine](../../advanced-analytics/r-services/installing-sql-server-r-services-on-an-azure-virtual-machine.md).

> [!NOTE]
> Currently, machine learning services are not supported on the Linux virtual machines for SQL Server 2017, or in Azure SQL Database. You must use either SQL Server 2016 SP1 or SQL Server 2017 for Windows.

The Data Science Virtual Machine also includes SQL Server 2016 with the R services feature already enabled.

The SQL Server 2017 Enterprise Edition image will be available after public release. However, you can use the SQl Server 2016 image, and upgrade your instance of R as described here: [Upgrade an Instance using SqlBindR](use-sqlbindr-exe-to-upgrade-an-instance-of-sql-server.md)


## Other VMs

### Deep Learning Toolkit for the Data Science Virtual Machine

This virtual machine contains the same machine learning tools available on the Data Science Virtual machine, but with GPU versions of mxnet, CNTK, TensorFlow, and Keras. This image can be used only on Azure GPU N-series instances. 

The deep learning toolkit also provides a set of sample deep learning solutions that use the GPU, including image recognition on the CIFAR-10 database and a character recognition sample on the MNIST database. GPU instances are currently available in South Central US, East US, West Europe, and Southeast Asia.

> [!IMPORTANT]
> GPU instances are currently only available in the South Central US.


## Frequently Asked Questions

### How do I access data in an Azure storage account?

When you need to use data from your Azure storage account, there are several options for accessing or moving the data:

+ Copy the data from your storage account to the local file system using a utility, such as [AzCopy](https://azure.microsoft.com/documentation/articles/storage-use-azcopy/#copy-files-in-azure-file-storage-with-azcopy-preview-version-only). 

+ Add the files to a file share on your storage account and then mount the file share as a network drive on your VM.  For more information, see [Mounting Azure files](https://azure.microsoft.com/documentation/articles/storage-dotnet-how-to-use-files/). 

### How do I use data from Azure Data Lake Storage (ADLS)?

You can read data from ADLS storage using ScaleR, if you reference the storage account the same way that you would an HDFS file system, by using webHDFS.  For more information, see this [setup guide](http://go.microsoft.com/fwlink/?LinkId=723452).

## See Also

[SQL Server R Services](https://msdn.microsoft.com/library/mt604845.aspx)

