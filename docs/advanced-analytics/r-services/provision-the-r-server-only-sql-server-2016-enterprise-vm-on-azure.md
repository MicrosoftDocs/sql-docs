---
title: "Provision the R Server Only SQL Server 2016 Enterprise VM on Azure | Microsoft Docs"
ms.custom: ""
ms.date: "2016-12-22"
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
# Provision the R Server Only SQL Server 2016 Enterprise VM on Azure
Virtual machines on Azure are a convenient option for quickly configuring a complete server environment for R solutions. 

The Azure Marketplace contains several virtual machines that support data science and  include Microsoft R Server:

+ The **Microsoft Data Science Virtual Machine** is configured with Microsoft R Server, as well as Python (Anaconda distribution), a Jupyter notebook server, Visual Studio Community Edition, Power BI Desktop, the Azure SDK, and SQL Server Express edition. 

+ The **Linux Data Science Virtual Machine** also includes Microsoft R Server, as well as Weka, support for PySpark Jupyter, and mxnet for deep learning.

+ **Microsoft R Server 2016 for Linux** contains the latest version of R Server (version 9.0.1). Separate VMs are available for CentOS version 7.2 and  Ubuntu version 16.04. 

+ The **R Server Only SQL Server 2016 Enterprise** virtual machine includes a standalone installer for [R Server 9.0.1](https://msdn.microsoft.com/microsoft-r/rserver-whats-new) that supports the new Modern Software Lifecycle  licensing model. 

> [!NOTE]
> These virtual machines replace the **RRE for Windows Virtual Machine** that was previously available in the Azure Marketplace.
> 
>These instructions replace the previous instructions for version (8.0.3) of "R Server Only SQL Server 2016 Enterprise" containing the DeployR feature. If you already have an 8.0.3 VM, you can find provisioning instructions at [Provision version 8.0.3 R Server (standalone) SQL Server 2016 Enterprise feature](https://msdn.microsoft.com//microsoft-r/rserver-install-windows-vm803provisioning) on MSDN.


## Provision a Virtual Machine with R and SQL Server

If you are new to using Azure VMs, we recommend that you see this article for more information about using the portal and configuring a virtual machine.
[Virtual Machines - Getting Started](https://azure.microsoft.com/documentation/learning-paths/virtual-machines/)

### To create an R Server virtual machine from the Microsoft Azure Marketplace 
1. Click **Virtual machines**, and in the search box, type *R Server*.
2. Select one of the virtual machines that includes Microsoft R Server. 

      + To get Microsoft R Server for Windows, select **R Server Only SQL Server 2016 Enterprise**. [R Server for Windows version 9.0.1](https://msdn.microsoft.com/microsoft-r/rserver-whats-new) is licensed as SQL Server enterprise feature, but version 9.0.1 is installed as a standalone server and serviced under the Modern Software Lifecycle policy rather than SQL Server support.
      + To use SQL Server R Services, you must install one of the SQL Server 2016 Enterprise or Developer edition virtual machines as described here: [Installing SQL Server R Services on an Azure Virtual Machine](../../advanced-analytics/r-services/installing-sql-server-r-services-on-an-azure-virtual-machine.md).

3. Continue to provision the virtual machine as described in this article: [https://azure.microsoft.com/documentation/articles/virtual-machines-windows-hero-tutorial/](https://azure.microsoft.com/en-us/documentation/articles/virtual-machines-windows-hero-tutorial/) 
7. After the VM has been created and is running, click the **Connect** button to open a connection and log into the new machine.
8. When you connect, **Server Manager** is opened by default, but no additional server configuration is required. Close **Server Manager** to get to the desktop, and proceed with the next steps:
    + Install additional R tools or development tools
    + Operationalize R Server

### To locate the R Server VM in the Azure Classic Portal
1. Click **Virtual Machines**, and then click **NEW**.
2. In the **New** pane, **Compute** and **Virtual Machine** should already be selected. 
3. Click **From Gallery** to locate the VM image. You can type *R Server* in the search box, or click **Microsoft** and then scroll down until you see **R Server Only SQL Server 2016 Enterprise**.


## Install R Tools
By default, Microsoft R Server includes all the R tools installed with a base installation of R, including RTerm and RGui. A shortcut to RGui has been added to the desktop, if you want to get started using R right away.

However, you might wish to install additional R tools, such as RStudio, the R Tools for Visual Studio (RTVS) add-in to Visual Studio 2015, or Microsoft R Client. See the following links for download locations and instructions:
+ [Visual Studio 2015](https://www.visualstudio.com/downloads/)
 and [R Tools for Visual Studio](https://www.visualstudio.com/features/rtvs-vs.aspx)
+ [Microsoft R Client](https://msdn.microsoft.com/microsoft-r/install-r-client-windows)
+ [RStudio for Windows](https://www.rstudio.com/products/rstudio/download/)

After installation is complete, be sure to change the default R runtime location so that all R development tools use the Microsoft R Server libraries.

## Operationalize R Server for Windows on the Virtual Machine

If the virtual machine includes R Server, additional configuration is required to use web service deployment, remote execution, or leverage R Server as a deployment server in your organization. This step is necessary if you want to use functions in the new [mrsdeploy package](https://msdn.microsoft.com/microsoft-r/mrsdeploy/mrsdeploy) in version 9.0.1.

For instructions, see [Configure R Server for Operationalization](https://msdn.microsoft.com/microsoft-r/operationalize/configuration-initial).

> [!NOTE]
> Additional configuration is only necessary if you want to use mrsdeploy or other functionality described in [Operationalization of R Server](https://msdn.microsoft.com/microsoft-r/operationalize/about). Use of ReovScaleR, MicrosoftML for machine learning integration, olapR, or other R packages exclusive to Microsoft R do not require this step. 

## Frequently Asked Questions

### How do I access data in an Azure storage account?

When you need to use data from your Azure storage account, there are several options for accessing or moving the data:

+ Copy the data from your storage account to the local file system using a utility, such as [AzCopy](https://azure.microsoft.com/documentation/articles/storage-use-azcopy/#copy-files-in-azure-file-storage-with-azcopy-preview-version-only). 

+ Add the files to a file share on your storage account and then mount the file share as a network drive on your VM.  For more information, see [Mounting Azure files](https://azure.microsoft.com/documentation/articles/storage-dotnet-how-to-use-files/). 

### How do I use data from Azure Data Lake Storage (ADLS)?

You can read data from ADLS storage using ScaleR, if you reference the storage account the same way that you would an HDFS file system, by using webHDFS.  For more information, see this [setup guide](http://go.microsoft.com/fwlink/?LinkId=723452).

### Which virtual machine is best for me?

The Azure Portal provides multiple virtual machine images that include Microsoft R Server. Use the virtual machine that contains the tools you need.

**Data Science Virtual Machine**

Runs on Windows Server 2012. Contains the following tools for data science modeling and development activities:
- Microsoft R Server Developer Edition, [version 9.0.1](https://msdn.microsoft.com/microsoft-r/rserver-whats-new)
- Anaconda Python distribution
- Jupyter notebooks for Python and R
- Visual Studio Community Edition with Python, R and node.js tools
- Power BI desktop
- SQL Server 2016 Developer Edition
- CNTK, an Open Source Deep Learning toolkit from Microsoft
- mxnet
- ML algorithms like xgboost
- Vowpal Wabbit

The Azure SDK and libraries on the VM allows you to build your applications using various services in the cloud that are part of the Cortana Analytics Suite which includes Azure Machine Learning, Azure data factory, Stream Analytics and SQL Datawarehouse, Hadoop, Data Lake, Spark and more.

**Deep Learning tools kit for the Data Science Virtual Machine**

In addition to the features on the Data Science Virtual Machine, includes:
- Deep learning toolkit 
- GPU versions of mxnet and CNTK for use on Azure GPU N-series instances
- Sample deep learning solutions that use the GPU, including image recognition on the CIFAR-10 database and a character recognition sample on the MNIST database

The GPUs use discrete device assignment, which is well-suited to deep learning problems that require large training sets and expensive computational training efforts.  

> [!IMPORTANT]
> GPU instances are currently only available in the South Central US.

**Linux Data Science Virtual Machine**

There are two Linux virtual machines, one based on the Openlogic CentOS, and one on Ubuntu. This virtual machine includes the following tools for development and data science:


- Microsoft R Server Developer Edition, [version 9.0.1](https://msdn.microsoft.com/microsoft-r/rserver-whats-new)
- Anaconda Python distribution
- Jupyter notebooks for Python, R and Julia
- Local instance of Postgres database
- Azure command line tools
- Azure SDK for Python, R, Java, node.js, Ruby, PHP
- Run-times for Ruby, PHP, node.js, Java, Perl, Eclipse
- Standard editors such as vim, Emacs, and gedit
- libraries to access various Azure services 
- CNTK (a deep learning toolkit from Microsoft Research)
- Vowpal Wabbit 
- xgboost

You have full access to the virtual machine and the shell including *sudo* access for the account that is created during the provisioning of the VM. 

JupyterHub is preconfigured on the virtual machine. It offers a browser based experimentation and development environment for Python, Julia and R. 

A remote graphical desktop is also provided, preconfigured on the virtual machine. Some additional steps are required on the X2Go client. 

### Where can I find more information?

Additional documentation about Microsoft R can be found in the MSDSN library:  [R Server and ScaleR](https://msdn.microsoft.com/microsoft-r)  

To learn about R in general, see these resources: 
+ [DataCamp](http://www.datacamp.com) - Provides a free introductory and intermediate course in R, and a course on working with big data using Revolution R
+ [Stack Overflow](http://www.stackoverflow.com) - A good resource for R programming and ML tools questions.= 
+ [Cross Validated](https://stats.stackexchange.com/) - Site for questions about statistical issues in machine learning
+ [R Help mailing list archives](https://www.r-project.org/mail.html) and [MRAN website](https://mran.microsoft.com/documents/getting-started/) -  Good source of historical information such as archived questions 


## See Also
[SQL Server R Services](https://msdn.microsoft.com/library/mt604845.aspx)
