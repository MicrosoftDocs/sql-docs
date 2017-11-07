---
title: "Provision a virtual machine for machine learning on Azure | Microsoft Docs"
ms.custom: ""
ms.date: "10/16/2017"
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
# Provision a virtual machine for machine learning on Azure

Virtual machines on Azure are a convenient option for quickly configuring a complete server environment for machine learning solutions. This article lists some virtual machine images that contain either R Server, Machine Learning Server, or SQL Server with machine learning.

This list is not intended to be comprehensive, but only provide the names of images that are related to Machine Learning Server or SQL Server Machine Learning Services, to facilitate discovery.

> [!TIP]
> We recommend that you use the new version of the Azure portal, and the Azure Marketplace. Some images are not available when browsing the Azure Gallery on the classic portal.

## How to provision a virtual machine

If you are new to using Azure VMs, we recommend that you see these articles for more information about using the portal and configuring a virtual machine.

+ [Virtual Machines - Getting Started](https://azure.microsoft.com/documentation/learning-paths/virtual-machines/)
+ [Getting Started with Windows Virtual Machines](https://azure.microsoft.com/documentation/articles/virtual-machines-windows-hero-tutorial/)

## Find a machine learning image

1. From the Azure portal (portal.azure.com), click **Virtual Machines**, or click **New**.

2. Locate the search box at the top of the page, which you can use to filter resources by name. 

3. Type “R Server” (or "ML Server") in the **Filter** control to see a list of related resources. Click **Search in Marketplace** to see virtual machines.

    > [!TIP]
    > 
    > Other possible strings for the Filter control are "data science" and "machine learning".
    > 
    > Use the `%` wildcard in search to find the names of virtual machines. For example, you can type `"`%Julia%` or `%R %`.

4. To get R Server for Windows, select **R Server Only SQL Server 2016 Enterprise**.
  
    [R Server](https://msdn.microsoft.com/microsoft-r/rserver-whats-new) is licensed as a SQL Server Enterprise Edition feature, but version 9.1 is installed as a standalone server and is serviced under the Modern Lifecycle support policy.

    > [!NOTE] 
    > 
    > This fall, look for the release of a new virtual machine that includes SQL Server 2017 and the 9.2.1 release of Machine Learning Server.
    > Until then, you can update the version of SQL Server installed on this virtual machine by using the SQL Server Installation Center and choosing the Upgrade option. For more information, see [Upgrade SQL Server by using the installation wizard](https://docs.microsoft.com/sql/database-engine/install-windows/upgrade-sql-server-using-the-installation-wizard-setup).

5. After the virtual machine has been created and is running, click the **Connect** button to open a connection and log in to the new machine.

5. After you connect, you can install additional R package, or  your preferred development tool.

### Install additional R tools

By default, Microsoft R Server includes all the R tools installed with a base installation of R, including RTerm and RGui. A shortcut to RGui has also been added to the desktop.

However, you might wish to install additional R tools, such as RStudio, R Tools for Visual Studio (RTVS), or Microsoft R Client. See the following links for download locations and instructions:

+ [R Tools for Visual Studio](https://docs.microsoft.com/visualstudio/rtvs/installation)
+ [Microsoft R Client](https://msdn.microsoft.com/microsoft-r/install-r-client-windows)
+ [RStudio for Windows](https://www.rstudio.com/products/rstudio/download/)

After installation is complete, be sure to change the default R runtime location so that all R development tools use the Microsoft R Server libraries.

### Configure R Server to support web services

Additional configuration is required to use web service deployment, remote execution, or to leverage R Server as a deployment server in your organization. For instructions, see [Configuring R Server to operationalize analytics](https://docs.microsoft.com/machine-learning-server/install/operationalize-r-server-one-box-config).

> [!NOTE]
> Additional configuration is not needed if you just want to use packages such as RevoScaleR or MicrosoftML.

## Other virtual machines

The following images are available from the Azure Marketplace and include fully configured machine learning tools, but do not necessarily include SQL Server.

### Data Science Virtual Machine

This image is preconfigured with Microsoft R Server, as well as Python (Anaconda distribution), a Jupyter notebook server, Visual Studio Community Edition, Power BI Desktop, the Azure SDK, and SQL Server Express edition.

The Windows version runs on Windows Server 2012, and contains many special tools for modeling and analytics, including [CNTK](https://www.microsoft.com/cognitive-toolkit/), [mxNet](https://mxnet.incubator.apache.org/), and popular R packages such as **xgboost**.

Linux editions are provided for Ubuntu, Centos, and Centos CSP, and contain many popular tools for data science and development activities.

For more information, see [Introduction to Azure Data Science Virtual Machine for Linux and Windows](https://docs.microsoft.com/azure/machine-learning/data-science-virtual-machine/provision-vm).

This image was recently updated to include: 

+ Support for Julia, the scalable, powerful data science language of the future 
+ JupyterHub, which is a useful option when you want to run a training class and want all students to share the same server, but use separate notebooks and directories.

For more information about supported tools and machine learning frameworks, see [Get to know your Data Science Virtual Machine](https://docs.microsoft.com/azure/machine-learning/data-science-virtual-machine/dsvm-tools-overview)

### R Server virtual machines

In addition to the **R Server Only SQL Server 2016 Enterprise** image, you can get standalone virtual machines that contain R Server. Images are available for Linux CentOS version 7.2, Linux RedHat version 7.2, and Ubuntu version 16.04.

For more information, see [Machine Learning Server in the Cloud](https://docs.microsoft.com/machine-learning-server/install/machine-learning-server-in-the-cloud)

 > [!NOTE]
 > These virtual machines replace the **RRE for Windows Virtual Machine** that was previously available in the Azure Marketplace.

### SQL Server virtual machines

There are two options for using SQL Server machine learning in Azure:

+ Get one of the virtual machine images that includes SQL Server R Services preinstalled.
+ Create an Azure virtual machine and install SQL Server Enterprise or Developer edition using your own license key. 
  
    Then, run setup again to add and enable the machine learning service, as described here: [Installing SQL Server R Services on an Azure virtual machine](../r/installing-sql-server-r-services-on-an-azure-virtual-machine.md).
+ Create an Azure SQL Database using a service tier that can support machine learning, and use the new R Services feature currently in preview. For more information, see [Azure SQL DB](../r/using-r-in-azure-sql-database.md).

> [!NOTE]
> Currently, SQL Server Machine Learning Services is not supported on the Linux virtual machines for SQL Server 2017. However, you can perform scoring on a trained model using the T-SQL PREDICT function. For more information, see [Native scoring in SQl Server](../sql-native-scoring.md). 

### Virtual machines for deep learning 

Previously, Microsoft provided the Deep Learning Toolkit for the Data Science Virtual Machine, which you could add to an existing Data Science Virtual Machine. This toolkit is now superseded by the Deep Learning Virtual machine, which contains popular deep learning tools:

+ GPU editions of deep learning frameworks like Microsoft Cognitive Toolkit, TensorFlow, Keras, and Caffe
+ Built-in GPU drivers
+ A collection of tools for image and text processing
+ Enterprise development tools such as Microsoft R Server Developer Edition, Anaconda Python, Jupyter notebooks for Python and R
+ Dev tools for Python, R, SQL Server, and much more
+ End-to-end samples for image and text understanding

The Deep Learning Virtual machine is available either on the Windows 2016 or Ubuntu Linux platforms. For more information, see [Deep Learning and AI frameworks](https://docs.microsoft.com/azure/machine-learning/data-science-virtual-machine/dsvm-deep-learning-ai-frameworks).

> [!IMPORTANT]
> 
> Deploying this virtual machine requires the Azure GPU NC-series virtual machine images, which are available in limited Azure regions. For information about availability, see [Products available by region](https://azure.microsoft.com/en-us/regions/services/). When you provision the virtual machine, be sure to use **HDD** as the disk type, not **SSD**.

## Frequently Asked Questions

This section contains some common questions about machine learning virtual machines from Microsoft.

### Can I install a virtual machine with SQL Server 2017?

A Windows-based virtual machine for SQL Server 2017 Enterprise Edition that includes Machine Learning Services will be available soon. Look for announcements on these blogs sites:

+ [Cortana Intelligence and machine Learning](https://blogs.technet.microsoft.com/machinelearning/)
+ [Data Platform Insider](https://blogs.technet.microsoft.com/dataplatforminsider/)

### How do I access data in an Azure storage account?

When you need to use data from your Azure storage account, there are several options for accessing or moving the data:

+ Copy the data from your storage account to the local file system using a utility, such as [AzCopy](https://azure.microsoft.com/documentation/articles/storage-use-azcopy/#copy-files-in-azure-file-storage-with-azcopy-preview-version-only). 

+ Add the files to a file share on your storage account and then mount the file share as a network drive on your VM.  For more information, see [Mounting Azure files](https://azure.microsoft.com/documentation/articles/storage-dotnet-how-to-use-files/). 

### How do I use data from Azure Data Lake Storage (ADLS)?

You can read data from ADLS storage using RevoScaleR, if you reference the storage account the same way that you would an HDFS file system, by using webHDFS.  For more information, see this article: [Using R to perform FileSystem Operations on Azure Data Lake Store](https://blogs.msdn.microsoft.com/microsoftrservertigerteam/2017/03/14/using-r-to-perform-filesystem-operations-on-azure-data-lake-store/).


