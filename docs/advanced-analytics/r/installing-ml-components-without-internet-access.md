---
title: "Installing machine learning components without internet access | Microsoft Docs"
ms.custom: ""
ms.date: "09/14/2017"
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
# Installing machine learning components without internet access

Because the R and Python components provided with SQL Server 2016 and SQL Server 2017 are open source, Microsoft does not install R or Python components by default.

Instead, we provide the related installers and bundled packages as a convenience on the Microsoft Download Center and other trusted sites. You must consent to the appropriate license, and then SQL Server setup installs R or Python components for you.

This topic provides the download locations for the installers and an overview of the offline setup process.

## Installation process

Typically, setup of the machine components used in SQL Server 2016 and SQL Server 2017 requires an internet connection. When SQL Server setup runs, if you have selected any of the machine learning options, setup checks for the Python or R installers, as well as any other required components.

+ **If the computer has an internet connection

    SQL Server locates and download the components for you, then installs them during setup. You must accept the license terms separately for each open source component (R or Python) that you install.

+ If the computer does not have internet access

    You must download additional installers before continuing with setup. At minimum, download the R or Python installers that are supported for the version of SQL Server that you are installing.

    Depending on your server's configuration, you might need additional components.  See [Additional components](#bkmk_OtherComponents) for details.

    After you have downloaded the installers, you use them when installing the feature as part of SQL Server setup.

### Step 1. Obtain additional installers

For **R** in SQL Server 2016 and SQL Server 2017, you'll need to get two different installers. The SQL Server setup wizard ensures that they are installed in the correct order.

+ Installers with **SRO** in the name provide the open source components.
+ Installers with **SRS** in the name contain components provided by Microsoft, including those for database integration.

For **Python** in SQL Server 2017, download the single CAB file, and any prerequisites.

1. Download the installers from the [Microsoft Download Center sites](#installerlocs) onto a computer with internet access, and save the installer rather than running it.
2. Copy the installer (CAB) files to the computer where you intend to install machine learning components.
3. In SQL Server 2016, the setup wizard installed English by default. To install using a different language required modification of the installer file name, as described here: [Modifications required for different language locales](#modslocales).
    For SQL Server 2017, the correct language is identified based on the instance locale.
4. Download any additional components that are required, such as MPI or .NET Core.
5. Optionally, you can download the archived source code for the open source components, but this is not required for SQL Server setup, and can be completed at any time. For more information, see [R Server for Windows](https://docs.microsoft.com/r-server/install/r-server-install-windows).

For a step-by-step walkthrough of the offline installation process for R Services in SQL Server 2016, we recommend article by the [SQL Server Customer Advisory Team](https://blogs.msdn.microsoft.com/sqlcat/2016/10/20/do-it-right-deploying-sql-server-r-services-on-computers-without-internet-access/). the article includes screenshots and also covers patching and slipstream setup scenarios.

### Step 2. Run offline setup using the SQL Server setup wizard

1. Run the SQL Server setup wizard.
2. When the setup wizard displays the licensing page, click  **Accept**.
3. A dialog box opens that prompts you for the **Install Path** of the required packages.
4. click **Browse** to locate the folder containing the installer files you copied earlier.
5. If the correct files are found, you can click **Next** to indicate that the components are available.
10. Complete the SQL Server setup wizard.
11. Perform the required post-installation steps to make sure the service is enabled.

## <a name="installerlocs"></a>Where to download machine learning components

> [!NOTE]
> Be sure to get the files that match the version of SQL Server you are installing.
> 
> Support for Python is provided beginning with SQL Server 2017 CTP 2.0. Earlier versions, including SQL Server 2016, do not support Python.

+ [To get R components for SQL Server 2016](#bkmk_2016Installers)

+ [To get R or Python components for SQL Server 2017](#bkmk_2017Installers)

### <a name="bkmk_2017Installers"></a>Downloads for SQL Server 2017

Release  |Download link  |
---------|---------|
**SQL Server 2017 CTP 1**     |
Microsoft R Open     |[SRO_3.3.0.16000_1033.cab](https://go.microsoft.com/fwlink/?LinkId=836819)
Microsoft R Server     |[SRS_9.0.0.16000_1033.cab](https://go.microsoft.com/fwlink/?LinkId=836818)
**SQL Server 2017 CTP 1.1** |
Microsoft R Open     |[SRO_3.3.2.0_1033.cab](https://go.microsoft.com/fwlink/?LinkId=834568)
Microsoft R Server     |[SRS_9.0.1.16000_1033.cab](https://go.microsoft.com/fwlink/?LinkId=834567)
**SQL Server 2017 CTP 1.4** |
Microsoft R Open     |[SRO_3.3.2.100_1033.cab](https://go.microsoft.com/fwlink/?LinkId=842483)
Microsoft R Server     |[SRS_9.0.2.100_1033.cab](https://go.microsoft.com/fwlink/?LinkId=842482)
**SQL Server 2017 CTP 2.0** |
Microsoft R Open     |[SRO_3.3.3.0_1033.cab](https://go.microsoft.com/fwlink/?LinkId=842800)
Microsoft R Server     |[SRS_9.1.0.0_1033.cab](https://go.microsoft.com/fwlink/?LinkId=842799)
Microsoft Python Open     |[SPO_9.1.0.0_1033.cab](https://go.microsoft.com/fwlink/?LinkId=842828)
Microsoft Python Server    |[SPS_9.1.0.0__1033.cab](https://go.microsoft.com/fwlink/?LinkId=842848)
**SQL Server 2017 CTP 3.0** |
Microsoft R Open     |no change; use CTP 2.0|
Microsoft R Server     |no change; use CTP 2.0|
Microsoft Python Open     |no change; use CTP 2.0|
Microsoft Python Server    |no change; use CTP 2.0|
**SQL Server 2017 CTP 4.0** |
Microsoft R Open     |no change; use CTP 2.0|
Microsoft R Server     |no change; use CTP 2.0|
Microsoft Python Open     |no change; use CTP 2.0|
Microsoft Python Server    |no change; use CTP 2.0|
**SQL Server 2017 RTM** |
Microsoft R Open     |[SRO_3.3.3.24_1033.cab](https://go.microsoft.com/fwlink/?LinkId=851496)|
Microsoft R Server      |[SRS_9.2.0.24_1033.cab](https://go.microsoft.com/fwlink/?LinkId=851507)|
Microsoft Python Open     |[SPO_9.2.0.24_1033.cab](https://go.microsoft.com/fwlink/?LinkId=851502) |
Microsoft Python Server    |[SPS_9.2.0.24_1033.cab](https://go.microsoft.com/fwlink/?LinkId=851508) |

### <a name="bkmk_2016Installers"></a>Downloads for SQL Server 2016

Release  |Download link  |
---------|---------|
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
Microsoft R Open     |no change; use previous|
Microsoft R Server     | no change; use previous |
**SQL Server 2016 CU 4**     |
Microsoft R Open     |[SRO_3.2.2.13000_1033.cab](https://go.microsoft.com/fwlink/?LinkId=831785)|
Microsoft R Server     |[SRS_8.0.3.13000_1033.cab](https://go.microsoft.com/fwlink/?LinkId=831676)|
**SQL Server 2016 CU 5**     |
Microsoft R Open     |no change; use previous|
Microsoft R Server     |no change; use previous|
**SQL Server 2016 CU 6**     |
Microsoft R Open     |no change; use previous|
Microsoft R Server     |[SRS_8.0.3.14000_1033.cab](https://go.microsoft.com/fwlink/?LinkId=850316)  |
**SQL Server 2016 CU 7**     |
Microsoft R Open     |no change; use previous|
Microsoft R Server     |no change; use previous |
**SQL Server 2016 SP 1**     |
Microsoft R Open     |[SRO_3.2.2.15000_1033.cab](https://go.microsoft.com/fwlink/?LinkId=824879)
Microsoft R Server     |[SRS_8.0.3.15000_1033.cab](https://go.microsoft.com/fwlink/?LinkId=824881)
**SQL Server 2016 SP 1 CU1**     |
Microsoft R Open     |no change; use previous|
Microsoft R Server     |no change; use previous|
**SQL Server 2016 SP 1 CU2**     |
Microsoft R Open     |[SRO_3.2.2.16000_1033.cab](https://go.microsoft.com/fwlink/?LinkId=836819)|
Microsoft R Server    |[SRS_8.0.3.16000_1033.cab](https://go.microsoft.com/fwlink/?LinkId=836818)|
**SQL Server 2016 SP 1 CU3**     |
Microsoft R Open     |no change; use previous|
Microsoft R Server     |no change; use previous|
**SQL Server 2016 SP 1 CU4 and GDR**     |
Microsoft R Open     |no change; use previous|
Microsoft R Server    |[SRS_8.0.3.17000_1033.cab](https://go.microsoft.com/fwlink/?LinkId=850317)

If you would like to view the source code for Microsoft R, it is available for download as an archive in .tar format: [Download R Server installers](https://docs.microsoft.com/r-server/install/r-server-install-windows#download)

### <a name = "bkmk_OtherComponents"></a>Additional prerequisites

Depending on your environment, you might need to make local copies of installers for the following prerequisites.

Component  |Version
---------|---------
[Microsoft AS OLE DB Provider for SQL Server 2016](https://go.microsoft.com/fwlink/?linkid=834405)     |  13.0.1601.5
[Microsoft .NET Core](https://go.microsoft.com/fwlink/?linkid=834319)     | 1.0.1
[Microsoft MPI](https://go.microsoft.com/fwlink/?linkid=834316)     | 7.1.12437.25
[Microsoft Visual C++ 2013 Redistributable](https://go.microsoft.com/fwlink/?linkid=799853)     | 12.0.30501.0
[Microsoft Visual C++ 2015 Redistributable](https://go.microsoft.com/fwlink/?linkid=828641)     | 14.0.23026.0


## <a name="modslocales"></a>Installing for different language locales

If you download the .CAB files as part of SQL Server setup on a computer with internet access, the setup wizard detects the local language and automatically changes the language of the installer.

However, depending on your version of SQL Server, you might need to take additional steps to install the localized R components on a computer without internet access.

+ **For SQL Server 2016**

   After you download the R installers to a local share, you might need to manually edit the name of the downloaded files to insert the correct language identifier for the language you are installing.

    For example, to install the Japanese version of SQL Server, you would change the name of the file from SRS_8.0.3.0_**1033**.cab to SRS_8.0.3.0_**1041**.cab.

    > [!IMPORTANT]
    > This issue applies only to early versions and was fixed in later releases.
    > Only use this workaround if the installer returns a message that it cannot install the correct language.

+ **For SQL Server 2017**

    Download the .CAB file for the R or Python components.
    
    The language is detected based on server locale. The correct locale is automatically installed using the downloaded .CAB file.

## Slipstream upgrades

Slipstream setup refers to the ability to apply a patch or update to a failed instance installation, to repair existing problems. The advantage of this method is that the SQL Server is updated at the same time that you perform setup, avoiding a separate restart later.

+ If the server does not have Internet access, you must download the SQL Server installer, and then download matching versions of the R component installers **before** beginning the update process.  The R components are not included by default with SQL Server.

+ If you are *adding* these components to an *existing* installation, use the updated version of the SQL Server installer, and the corresponding updated version of the additional components. When you specify that the R feature is to be installed, the installer looks for the matching version of the installers for the machine learning components.

## Command-line arguments for setup

When performing an unattended setup, you must provide the following command-line arguments. However, you do not need to set any additional flags to install additional required components; prerequisites such as .NET core are installed silently by default.

**Location of installers**

- `/UPDATESOURCE` to specify the location of the local file containing the SQL Server update installer
- `/MRCACHEDIRECTORY` to specify the folder containing the R component CAB files

**R components in SQL Server 2016**

- `/ADVANCEDANALYTICS` to get engine support for external scripts
- `/IACCEPTROPENLICENSETERMS="True"` to accept the separate R licensing agreement

**R components in SQL Server 2017**

- `/ADVANCEDANALYTICS` to get engine support for external scripts
- `/SQL_INST_MR` to use R
- `/IACCEPTROPENLICENSETERMS="True"` to accept the separate R licensing agreement

**Python components in SQL Server 2017**

- `/ADVANCEDANALYTICS` to get engine support for external scripts
- `/SQL_INST_MPY` to use Python
- `/IACCEPTPYTHONLICENSETERMS="True"` to accept the separate R licensing agreement


> [!NOTE]
> You cannot change the service account for Launchpad by using parameters in SQL Server setup. We recommend that you install using the default service accounts, and then modify the service account using SQL Server Configuration Manager. After doing so, be sure to restart the Launchpad service.

## See also

[Install Microsoft R Server](https://docs.microsoft.com/r-server/install/r-server-install-windows)

This article by the R Services Support team demonstrates how to perform an unattended install or upgrade of R services in SQL Server 2016: [Deploying R Services on Computers without Internet Access](https://blogs.msdn.microsoft.com/sqlcat/2016/10/20/do-it-right-deploying-sql-server-r-services-on-computers-without-internet-access/).