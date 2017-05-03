---
title: "Installing Machine Learning Components without Internet Access | Microsoft Docs"
ms.custom: ""
ms.date: "04/20/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 0a90c438-d78b-47be-ac05-479de64378b2
caps.latest.revision: 30
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# Installing Machine Learning Components without Internet Access

Because the R and Python components provided with SQL Server 2016 or SQL Server 2017 are open source, Microsoft does not install R or Python components by default.

Instead, we provide the related installers and bundled packages as a convenience on the Microsoft Download Center and other trusted sites. You must consent to the appropriate license, and then SQL Server setup will install R or Python components for you.

This topic provides the download locations for the installers and an overview of the offline setup process.

## Installation Process

Typically, setup of the machine components used in SQL Server 2016 and SQL Server 2017 requires an Internet connection. When SQL Server setup runs, if you have selected any of the machine learnig options, setup will check for the Python or R installers, as well as any other required components. If there is an Internet connection, SQL Server will install them for you.

> [!IMPORTANT]
> On a server without Internet access, you must download additional installers before continuing with setup.

At minimum, you will need to download the R or Python installers that are supported for the version or build number of SQL Server that you are installing.

Depending on your server's configuration, you might need additional components, such as .NET Core.  See [Additional Components](#bkmk_OtherComponents) for details.

After you have downloaded the installers, you use them when installing the feature as part of SQL Server setup.

### Step 1. Obtain additional installers

For **R** in SQL Server 2016 and SQL Server 2017, you'll need to get two different installers. The SQL Server setup wizard will ensure that they are installed in the correct order.

+ Installers with **SRO** in the name provide the open source components.
+ Insallers with **SRS** in the name contain components provided by Microsoft, including those for database integration.


For **Python** in SQL Server 2017, download the single CAB file, and any prerequisites.


1. Download the installers from the [Microsoft Download Center sites](#installerlocs) onto a computer with Internet access, and save the installer rather than running it.
2. Copy the installer (CAB) files to the computer where you will install machine learning components.
3. Currently, the setup wizard installs English by default. To install using a different language, modify the installer file names as described here: [Modifications Required for Different Language Locales](#modslocales).
4. Download any additional components that are required, such as MPI or .NET Core.
5. Optionally, you can download the archived source code for the open source components, but this is not required for SQL Server setup, and can be completed at any time. For more information, see [R Server for Windows](https://msdn.microsoft.com/microsoft-r/rserver-install-windows).


> [!NOTE]
> Be sure to get the files that match the version of SQL Server you will be installing.
> 
> Support for Python is provided in SQL Server 2017 CTP 2.0. Earlier versions, including SQL Server 2016, do not support Python.

For a step-by-step walkthrough of the offline installation process for R Services in SQL Server 2016, we recommend article by the [SQL Server Customer Advisory Team](https://blogs.msdn.microsoft.com/sqlcat/2016/10/20/do-it-right-deploying-sql-server-r-services-on-computers-without-internet-access/). It also covers patching and slipstream setup scenarios.


### Step 2. Run offline setup using the SQL Server setup wizard

1. Run the SQL Server setup wizard.
2. When the setup wizard displays the licensing page, click  **Accept**.
3. A dialog box opens that prompts you for the **Install Path** of the required packages.
4. click **Browse** to locate the folder containing the installer files you copied earlier.
5. If the correct files are found, you can click **Next** to indicate that the components are available.
10. Complete the SQL Server setup wizard.
11. Perform the required post-installation steps to make sure the service is enabled.

## <a name="installerlocs"></a>Downloads

Release  |Download link  
---------|---------
**SQL Server 2016 RTM**     |
Microsoft R Open     |[SRO_3.2.2.803_1033.cab](https://go.microsoft.com/fwlink/?LinkId=761266)
Microsoft R Server     |[SRS_8.0.3.0_1033.cab](https://go.microsoft.com/fwlink/?LinkId=735051)
**SQL Server 2016 CU 1**     |
Microsoft R Open     |[SRO_3.2.2.10000_1033.cab](https://go.microsoft.com/fwlink/?LinkId=808803)
Microsoft R Server     |[SRS_8.0.3.10000_1033.cab](https://go.microsoft.com/fwlink/?LinkId=808805)
**SQL Server 2016 CU 2**     |
Microsoft R Open     |[SRO_3.2.2.12000_1033.cab](https://go.microsoft.com/fwlink/?LinkId=827398)
Microsoft R Server     |[SRS_8.0.3.12000_1033.cab](https://go.microsoft.com/fwlink/?LinkId=827399)
**SQL Server 2016 CU 3**     |
Microsoft R Open     |[SRO_3.2.2.13000_1033.cab](https://go.microsoft.com/fwlink/?LinkId=831785)
Microsoft R Server     |[SRS_8.0.3.13000_1033.cab](https://go.microsoft.com/fwlink/?LinkId=831676)  |
**SQL Server 2016 SP 1**     |
Microsoft R Open     |[SRO_3.2.2.15000_1033.cab](https://go.microsoft.com/fwlink/?LinkId=824879)
Microsoft R Server     |[SRS_8.0.3.15000_1033.cab](https://go.microsoft.com/fwlink/?LinkId=824881)
**SQL Server 2016 SP 1 GDR**     |
Microsoft R Open     |[SRO_3.2.2.16000_1033.cab](https://go.microsoft.com/fwlink/?LinkId=836819)
Microsoft R Server     |[SRS_8.0.3.16000_1033.cab](https://go.microsoft.com/fwlink/?LinkId=836818)
**SQL Server 2017 CTP 1**     |
Microsoft R Open     |[SRO_3.3.0.16000_1033.cab](https://go.microsoft.com/fwlink/?LinkId=836819)
Microsoft R Server     |[SRS_9.0.0.16000_1033.cab](https://go.microsoft.com/fwlink/?LinkId=836818)
**SQL Server 2017 CTP 1.1** |
Microsoft R Open     |[SRO_3.3.2.0_1033.cab](https://go.microsoft.com/fwlink/?LinkId=834568)
Microsoft R Server     |[SRS_9.0.1.16000_1033.cab](https://go.microsoft.com/fwlink/?LinkId=834567)
**SQL Server 2017 CTP 1.4** |
Microsoft R Open     |[SRO_xxxx_1033.cab](https://go.microsoft.com/fwlink/?LinkId=842483)
Microsoft R Server     |[SRS_xxx.xxx_1033.cab](https://go.microsoft.com/fwlink/?LinkId=842482)
**SQL Server 2017 CTP 2.0** |
Microsoft R Open     |[SRO_3.3.3.0_1033.cab](https://go.microsoft.com/fwlink/?LinkId=842800)
Microsoft R Server     |[SRS_9.1.0.0_1033.cab](https://go.microsoft.com/fwlink/?LinkId=842799)
Microsoft Python Open     |[SPO_9.1.0.0_1033.cab](https://go.microsoft.com/fwlink/?LinkId=842828)
Microsoft Python Server    |[SPS_9.1.0.0__1033.cab](https://go.microsoft.com/fwlink/?LinkId=842848)

If you would like to view the source code for Microsoft R, it is available for download as an archive in .tar format: [Download R Server installers](https://msdn.microsoft.com/microsoft-r/rserver-install-windows#download)

### <a name = "bkmk_OtherComponents"></a>Additional Prerequisites

Depending on your environment, you might need to make local copies of installers for the following prerequisites.

Component  |Version
---------|---------
[Microsoft AS OLE DB Provider for SQL Server 2016](https://go.microsoft.com/fwlink/?linkid=834405)     |  13.0.1601.5
[Microsoft .NET Core](https://go.microsoft.com/fwlink/?linkid=834319)     | 1.0.1
[Microsoft MPI](https://go.microsoft.com/fwlink/?linkid=834316)     | 7.1.12437.25
[Microsoft Visual C++ 2013 Redistributable](https://go.microsoft.com/fwlink/?linkid=799853)     | 12.0.30501.0
[Microsoft Visual C++ 2015 Redistributable](https://go.microsoft.com/fwlink/?linkid=828641)     | 14.0.23026.0


## <a name="modslocales"></a>Installing for Different Language Locales

If you download the .cab files as part of SQL Server setup on a computer with Internet access, the setup wizard detects the local language and automatically changes the language of the installer.

However, if you are installing one of the localized versions of SQL Server to a computer without Internet access and download the R installers to a local share, you must manually edit the name of the downloaded files and insert the correct language identifier for the language you are installing.

For example, if you are installing the Japanese version of SQL Server, you would change the name of the file from SRS_8.0.3.0_**1033**.cab to SRS_8.0.3.0_**1041**.cab.


## Slipstream Upgrades

Slipstream setup refers to the ability to apply a patch or update to a failed instance installation, to repair existing problems. The advantage of this method is that the SQL Server is updated at the same time that you perform setup, avoiding a separate restart later.

+ If the server does not have Internet access, you must download the SQL Server installer, and then download matching versions of the R component installers **before** beginning the update process.  The R components are not included by default with SQL Server.

+ If you are *adding* these components to an *existing* installation, use the updated version of the SQL Server installer, and the corresponding updated version of the additional components. When you specify that the R feature is to be installed, the installer will look for the matching version of the installers for the machine learning components.

## Command-line Arguments for Setup

When performing an unattended setup, you will need to provide the following command-line arguments. Note that you do not need to set any additional flags to install additional required components. Prerequisites such as .NET core are installed silently by default.

**Location of installers**

- `/UPDATESOURCE` to specify the location of the local file containing the SQL Server update installer
- `/MRCACHEDIRECTORY` to specify the folder containing the R component CAB files

**R components in SQL Server 2016**

- `/ADVANCEDANALYTICS` to get engine support for external scripts
- `/IACCEPTROPENLICENSETERMS="True"` to accept the separate R licensing agreement

**R components in SQL Server  SQL Server 2017**

- `/ADVANCEDANALYTICS` to get engine support for external scripts
- `/SQL_INST_MR` to use R
- `/IACCEPTROPENLICENSETERMS="True"` to accept the separate R licensing agreement

**Python components in SQL Server 2017**

- `/ADVANCEDANALYTICS` to get engine support for external scripts
- `/SQL_INST_MPY` to use Python
- `/IACCEPTPYTHONLICENSETERMS="True"` to accept the separate R licensing agreement

> [!TIP]
> This article by the R Services Support team demonstrates how to perform an unattended install or upgrade of R services in SQL Server 2016: [Deploying R Services on Computers without Internet Access](https://blogs.msdn.microsoft.com/sqlcat/2016/10/20/do-it-right-deploying-sql-server-r-services-on-computers-without-internet-access/).

## See Also

[Install Microsoft R Server](https://msdn.microsoft.com/microsoft-r/rserver-install-windows)

