---
title: Install SQL Server machine learning components without internet access | Microsoft Docs
description: Offline or disconnected Machine Learning R and Pytyon setup on isolated SQL Server instance.
ms.prod: sql
ms.technology: machine-learning
  
ms.date: 08/02/2018
ms.topic: conceptual
author: HeidiSteen
ms.author: heidist
manager: cgronlun
---
# Install SQL Server machine learning components without internet access
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

By default, installers connect to Microsoft download sites to get required and updated components for machine learning on SQL Server. If firewall constraints prevent the installer from reaching these sites, you can use an internet-connected device to download files, transfer files to an offline server, and then run setup.

## 1 - Get the installation media

[!INCLUDE[GetInstallationMedia](../../includes/getssmedia.md)]

> [!NOTE]  
> For local installations, you must run Setup as an administrator. If you install [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] from a remote share, you must use a domain account that has read and execute permissions on the remote share.  

<a name = "bkmk_OtherComponents"></a>

## 2 - Prerequisites

Depending on your environment, you might need to make local copies of installers for the following prerequisites.

Component  |Version
---------|---------
[Microsoft AS OLE DB Provider for SQL Server 2016](https://go.microsoft.com/fwlink/?linkid=834405)     |  13.0.1601.5
[Microsoft .NET Core](https://go.microsoft.com/fwlink/?linkid=834319)     | 1.0.1
[Microsoft MPI](https://go.microsoft.com/fwlink/?linkid=834316)     | 7.1.12437.25
[Microsoft Visual C++ 2013 Redistributable](https://go.microsoft.com/fwlink/?linkid=799853)     | 12.0.30501.0
[Microsoft Visual C++ 2015 Redistributable](https://go.microsoft.com/fwlink/?linkid=828641)     | 14.0.23026.0

 
 <a name="bkmk_ga_instalpatch"></a>

 ###  Install patch requirement 

Microsoft has identified a problem with the specific version of Microsoft VC++ 2013 Runtime binaries that are installed as a prerequisite by SQL Server. If this update to the VC runtime binaries is not installed, SQL Server may experience stability issues in certain scenarios. Before you install SQL Server follow the instructions at [SQL Server Release Notes](../../sql-server/sql-server-2016-release-notes.md#bkmk_ga_instalpatch) to see if your computer requires a patch for the VC runtime binaries.  


## 3a - Download 2017 .cabs

SQL Server 2017 machine learning instances consist of the database engine instance with extra components for R and Python integration. R and Python features are added through .cab files. For SQL Server 2017 installations, the initial release is the baseline installation, over which you can install any cumulative update. The name "cumulative update" is self-evident, but its worth noting that you can skip CUs. Only the initial release is required.

On an internet-connected server, download the .cab files required for an offline installation. 

Put the CAB files in a convenient folder such as **Downloads** or the setup user's temp folder: C:\Users<user-name>\AppData\Local\Temp.

Release  |Download link  |
---------|---------|
**SQL Server 2017 initial release** |
Microsoft R Open     |[SRO_3.3.3.24_1033.cab](https://go.microsoft.com/fwlink/?LinkId=851496)|
Microsoft R Server      |[SRS_9.2.0.24_1033.cab](https://go.microsoft.com/fwlink/?LinkId=851507)|
Microsoft Python Open     |[SPO_9.2.0.24_1033.cab](https://go.microsoft.com/fwlink/?LinkId=851502) |
Microsoft Python Server    |[SPS_9.2.0.24_1033.cab](https://go.microsoft.com/fwlink/?LinkId=851508) |
**SQL Server 2017 CU1** |
Microsoft R Open     |no change; use previous|
Microsoft R Server      |[SRS_9.2.0.100_1033.cab](https://go.microsoft.com/fwlink/?LinkId=851501)|
Microsoft Python Open     |no change; use previous |
Microsoft Python Server    |[SPS_9.2.0.100_1033.cab](https://go.microsoft.com/fwlink/?LinkId=851500) |
**SQL Server 2017 CU2** |
Microsoft R Open     |no change; use previous|
Microsoft R Server      |no change; use previous|
Microsoft Python Open     |no change; use previous|
Microsoft Python Server    |no change; use previous|
**SQL Server 2017 CU3** |
Microsoft R Open     |[SRO_3.3.3.300_1033.cab](https://go.microsoft.com/fwlink/?LinkId=863894)|
Microsoft R Server      |[SRS_9.2.0.300_1033.cab](https://go.microsoft.com/fwlink/?LinkId=863893)|
Microsoft Python Open     |no change; use previous|
Microsoft Python Server    |[SPS_9.2.0.300_1033.cab](https://go.microsoft.com/fwlink/?LinkId=863892)|
**SQL Server 2017 CU4** |
Microsoft R Open     |no change; use previous|
Microsoft R Server      |[SRS_9.2.0.400_1033.cab](https://go.microsoft.com/fwlink/?LinkId=866212&clcid=1033)|
Microsoft Python Open     |no change; use previous|
Microsoft Python Server    |[SPS_9.2.0.400_1033.cab](https://go.microsoft.com/fwlink/?LinkId=866213&clcid=1033)|
**SQL Server 2017 CU5** |
Microsoft R Open     |no change; use previous|
Microsoft R Server      |[SRS_9.2.0.500_1033.cab](https://go.microsoft.com/fwlink/?LinkId=869052&clcid=1033)|
Microsoft Python Open     |no change; use previous|
Microsoft Python Server    |[SPS_9.2.0.500_1033.cab](https://go.microsoft.com/fwlink/?LinkId=869053&clcid=1033)|
**SQL Server 2017 CU6** |
Microsoft R Open     |no change; use previous|
Microsoft R Server      |[SRS_9.2.0.600_1033.cab](https://go.microsoft.com/fwlink/?LinkId=871074&clcid=1033)|
Microsoft Python Open     |no change; use previous|
Microsoft Python Server    |[SPS_9.2.0.600_1033.cab](https://go.microsoft.com/fwlink/?LinkId=871073&clcid=1033)|
**SQL Server 2017 CU7** |
Microsoft R Open     |no change; use previous|
Microsoft R Server      |no change; use previous|
Microsoft Python Open     |no change; use previous|
Microsoft Python Server    |no change; use previous|

<a name="bkmk_2016Installers"></a>

## 3b - Download 2016 .cabs

For SQL Server 2016 R Services, baseline releases are either the RTM version or a service pack version. Cumulative updates 1-7 are installed over the RTM release. Cumulative updates 8-

> [!IMPORTANT]
> 
> When installing SQL Server 2016 SP1 CU4 or SP1 CU5 offline, download SRO_3.2.2.16000_1033.cab. If you downloaded SRO_3.2.2.13000_1033.cab from FWLINK 831785 as indicated in the setup dialog box, rename the file as SRO_3.2.2.16000_1033.cab before installing the Cumulative Update.

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
**SQL Server 2016 SP 1 CU5**     |
Microsoft R Open     |no change; use previous|
Microsoft R Server    |no change; use previous |

If you would like to view the source code for Microsoft R, it is available for download as an archive in .tar format: [Download R Server installers](https://docs.microsoft.com/machine-learning-server/install/r-server-install-windows#download)


## 4- Transfer files

Transfer the zipped SQL Server installation media and the .cab files you already downloaded to the computer on which you are installing setup.

Put the CAB files in a convenient folder such as **Downloads** or the setup user's temp folder: C:\Users<user-name>\AppData\Local\Temp.

Put the en_sql_server_2017.iso file in a convenient folder. Double-click **setup.exe** to begin installation.

## 5 - Run Setup

When you run SQL Server setup on a computer disconnected from the internet, Setup adds an **Offline installation** page to the wizard so that you can specify the location of the .cab files you copied in the previous step.

1. Start the SQL Server setup wizard.

2. When the setup wizard displays the licensing page for open source R or Python components, click  **Accept**. Acceptance of licensing terms allows you to proceed to the next step.

3. In the **Offline installation** page, in **Install Path**, specify the folder containing the .cab files you copied earlier.

4. Continue following the on-screen prompts to complete the installation.

After installation is finished, restart the service and then configure the server to enable script execution as described in [Install SQL Server 2017 Machine Learning Services (In-Database)](sql-machine-learning-services-windows-install.md) or [Install SQL Server 2016 R Services (In-Database)](sql-r-services-windows-install.md).

## Slipstream upgrades

Slipstream setup refers to the ability to apply a patch or update to a failed instance installation, to repair existing problems. The advantage of this method is that the SQL Server is updated at the same time that you perform setup, avoiding a separate restart later.

When a server does not have Internet access, you must download the SQL Server installer, and then download matching versions of the R component installers **before** beginning the update process. The R components are not included by default with SQL Server.

If you are adding these components to an existing installation, use the updated version of the SQL Server installer, and the corresponding updated version of the additional components. When you specify that the R feature is to be installed, the installer looks for the matching version of the installers for the machine learning components.

## Apply cumulative updates (offline installation)

+ [SQL Server 2016 updates](https://sqlserverupdates.com/sql-server-2016-updates/)
+ [SQL Server 2017 updates](https://sqlserverupdates.com/sql-server-2017-updates/)

Start with a baseline installation:
+ SQL Server 2016 RTM Database Engine
+ SQL Server 2016 SP1 Database Engine
+ SQL Server 2016 SP2 Database Engine
+ SQL Server 2017 initial release Database engine

Download .cabs 

+ Download .cabs for R and Python (SQL Server 2017 only) and transfer to the offline server.
+ Run SQL Server Setup:
    choosing options in the Feature tree for Database Engine and Machine Learning Services
    specifying the location of the .cab files
    accepting the EULAs

## Next steps

An initial offline installation of either SQL Server 2017 Machine Learning Services or SQL Server 2016 R Services requires the same configuration as an online installation:

+ [Restart the service](sql-machine-learning-services-windows-install.md#restart-the-service) (for SQL Server 2016, click [here](sql-r-services-windows-install.md#restart-the-service)).
+ [Enable external script execution](sql-machine-learning-services-windows-install.md#bkmk_enableFeature)  (for SQL Server 2016, click [here](sql-r-services-windows-install.md#bkmk_enableFeature)).
+ [Verify installation](sql-machine-learning-services-windows-install.md#verify-installation)  (for SQL Server 2016, click [here](sql-r-services-windows-install.md#verify-installation)).
+ [Additional configuration as needed](sql-machine-learning-services-windows-install.md#additional-configuration)  (for SQL Server 2016, click [here](sql-r-services-windows-install.md#bkmk_FollowUp)).

To check the installation status of the instance and fix common issues, see [Custom reports for SQL Server R Services](../r/monitor-r-services-using-custom-reports-in-management-studio.md).

For help with any unfamiliar messages or log entries, see [Upgrade and installation FAQ - Machine Learning Services](../r/upgrade-and-installation-faq-sql-server-r-services.md).


