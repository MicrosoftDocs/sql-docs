---
title: CAB downloads for SQL Server cumulative updates | Microsoft Docs
description: CAB downloads for SQL Server 2017 Machine Learning Services and SQL Server 2016 R Services.
ms.prod: sql
ms.technology: machine-learning
  
ms.date: 08/02/2018
ms.topic: conceptual
author: HeidiSteen
ms.author: heidist
manager: cgronlun
---

# CAB downloads for cumulative updates on SQL Server in-database analytics instances

Database engine instances configured for in-database analytics have extra components for R and Python integration, installed and serviced through .cab file by SQL Server Setup. On servers connected to the internet, updates can be applied through Windows update. Disconnected servers are updated manually. This article provides donwload links to .cab files for each cumulative update of SQL Server 2017 Machine Learning Services (R and Python) or SQL Server 2016 R Services. 

For SQL Server 2017 installations, the initial release is the baseline installation, over which you can install any cumulative update. 
For SQL Server 2016 R Services, you can start with the initial release, SP 1, or SP 2. For more information about .cab file installation, see [Install SQL Server machine learning components without internet access](sql-ml-component-install-without-internet-access.md).

CAB files are listed in reverse chronological order for your convenience. Put the CAB files in a convenient folder such as **Downloads** or the setup user's temp folder: C:\Users<user-name>\AppData\Local\Temp.

## SQL Server 2017 .cabs

Release  |Download link  |
---------|---------|
**SQL Server 2017 CU7** |
Microsoft R Open     |no change; use previous|
Microsoft R Server      |no change; use previous|
Microsoft Python Open     |no change; use previous|
Microsoft Python Server    |no change; use previous|
**SQL Server 2017 CU6** |
Microsoft R Open     |no change; use previous|
Microsoft R Server      |[SRS_9.2.0.600_1033.cab](https://go.microsoft.com/fwlink/?LinkId=871074&clcid=1033)|
Microsoft Python Open     |no change; use previous|
Microsoft Python Server    |[SPS_9.2.0.600_1033.cab](https://go.microsoft.com/fwlink/?LinkId=871073&clcid=1033)|
**SQL Server 2017 CU5** |
Microsoft R Open     |no change; use previous|
Microsoft R Server      |[SRS_9.2.0.500_1033.cab](https://go.microsoft.com/fwlink/?LinkId=869052&clcid=1033)|
Microsoft Python Open     |no change; use previous|
Microsoft Python Server    |[SPS_9.2.0.500_1033.cab](https://go.microsoft.com/fwlink/?LinkId=869053&clcid=1033)|
**SQL Server 2017 CU4** |
Microsoft R Open     |no change; use previous|
Microsoft R Server      |[SRS_9.2.0.400_1033.cab](https://go.microsoft.com/fwlink/?LinkId=866212&clcid=1033)|
Microsoft Python Open     |no change; use previous|
Microsoft Python Server    |[SPS_9.2.0.400_1033.cab](https://go.microsoft.com/fwlink/?LinkId=866213&clcid=1033)|
**SQL Server 2017 CU3** |
Microsoft R Open     |[SRO_3.3.3.300_1033.cab](https://go.microsoft.com/fwlink/?LinkId=863894)|
Microsoft R Server      |[SRS_9.2.0.300_1033.cab](https://go.microsoft.com/fwlink/?LinkId=863893)|
Microsoft Python Open     |no change; use previous|
Microsoft Python Server    |[SPS_9.2.0.300_1033.cab](https://go.microsoft.com/fwlink/?LinkId=863892)|
**SQL Server 2017 CU2** |
Microsoft R Open     |no change; use previous|
Microsoft R Server      |no change; use previous|
Microsoft Python Open     |no change; use previous|
Microsoft Python Server    |no change; use previous|
**SQL Server 2017 CU1** |
Microsoft R Open     |no change; use previous|
Microsoft R Server      |[SRS_9.2.0.100_1033.cab](https://go.microsoft.com/fwlink/?LinkId=851501)|
Microsoft Python Open     |no change; use previous |
Microsoft Python Server    |[SPS_9.2.0.100_1033.cab](https://go.microsoft.com/fwlink/?LinkId=851500) |
**SQL Server 2017 initial release** |
Microsoft R Open     |[SRO_3.3.3.24_1033.cab](https://go.microsoft.com/fwlink/?LinkId=851496)|
Microsoft R Server      |[SRS_9.2.0.24_1033.cab](https://go.microsoft.com/fwlink/?LinkId=851507)|
Microsoft Python Open     |[SPO_9.2.0.24_1033.cab](https://go.microsoft.com/fwlink/?LinkId=851502) |
Microsoft Python Server    |[SPS_9.2.0.24_1033.cab](https://go.microsoft.com/fwlink/?LinkId=851508) |


<a name="bkmk_2016Installers"></a>

## SQL Server 2016 .cabs

For SQL Server 2016 R Services, baseline releases are either the RTM version or a service pack version. Cumulative updates 1-7 are installed over the RTM release. 

> [!IMPORTANT]
> 
> When installing SQL Server 2016 SP1 CU4 or SP1 CU5 offline, download SRO_3.2.2.16000_1033.cab. If you downloaded SRO_3.2.2.13000_1033.cab from FWLINK 831785 as indicated in the setup dialog box, rename the file as SRO_3.2.2.16000_1033.cab before installing the Cumulative Update.

Release  |Download link  |
---------|---------|
**SQL Server 2016 SP 1 CU5**     |
Microsoft R Open     |no change; use previous|
Microsoft R Server    |no change; use previous |
**SQL Server 2016 SP 1 CU4 and GDR**     |
Microsoft R Open     |no change; use previous|
Microsoft R Server    |[SRS_8.0.3.17000_1033.cab](https://go.microsoft.com/fwlink/?LinkId=850317)
**SQL Server 2016 SP 1 CU3**     |
Microsoft R Open     |no change; use previous|
Microsoft R Server     |no change; use previous|
**SQL Server 2016 SP 1 CU2**     |
Microsoft R Open     |[SRO_3.2.2.16000_1033.cab](https://go.microsoft.com/fwlink/?LinkId=836819)|
Microsoft R Server    |[SRS_8.0.3.16000_1033.cab](https://go.microsoft.com/fwlink/?LinkId=836818)|
**SQL Server 2016 SP 1 CU1**     |
Microsoft R Open     |no change; use previous|
Microsoft R Server     |no change; use previous|
**SQL Server 2016 SP 1**     |
Microsoft R Open     |[SRO_3.2.2.15000_1033.cab](https://go.microsoft.com/fwlink/?LinkId=824879)
Microsoft R Server     |[SRS_8.0.3.15000_1033.cab](https://go.microsoft.com/fwlink/?LinkId=824881)
**SQL Server 2016 CU 7**     |
Microsoft R Open     |no change; use previous|
Microsoft R Server     |no change; use previous |
**SQL Server 2016 CU 6**     |
Microsoft R Open     |no change; use previous|
Microsoft R Server     |[SRS_8.0.3.14000_1033.cab](https://go.microsoft.com/fwlink/?LinkId=850316)  |
**SQL Server 2016 CU 5**     |
Microsoft R Open     |no change; use previous|
Microsoft R Server     |no change; use previous|
**SQL Server 2016 CU 4**     |
Microsoft R Open     |[SRO_3.2.2.13000_1033.cab](https://go.microsoft.com/fwlink/?LinkId=831785)|
Microsoft R Server     |[SRS_8.0.3.13000_1033.cab](https://go.microsoft.com/fwlink/?LinkId=831676)|
**SQL Server 2016 CU 3**     |
Microsoft R Open     |no change; use previous|
Microsoft R Server     | no change; use previous |
**SQL Server 2016 CU 2**     |
Microsoft R Open     |[SRO_3.2.2.12000_1033.cab](https://go.microsoft.com/fwlink/?LinkId=827398)
Microsoft R Server     |[SRS_8.0.3.12000_1033.cab](https://go.microsoft.com/fwlink/?LinkId=827399)
**SQL Server 2016 CU 1**     |
Microsoft R Open     |[SRO_3.2.2.10000_1033.cab](https://go.microsoft.com/fwlink/?LinkId=808803)
Microsoft R Server     |[SRS_8.0.3.10000_1033.cab](https://go.microsoft.com/fwlink/?LinkId=808805)
**SQL Server 2016 RTM**     |
Microsoft R Open     |[SRO_3.2.2.803_1033.cab](https://go.microsoft.com/fwlink/?LinkId=761266)
Microsoft R Server     |[SRS_8.0.3.0_1033.cab](https://go.microsoft.com/fwlink/?LinkId=735051)

If you would like to view the source code for Microsoft R, it is available for download as an archive in .tar format: [Download R Server installers](https://docs.microsoft.com/machine-learning-server/install/r-server-install-windows#download)

# See also

[Install SQL Server machine learning components without internet access](sql-ml-component-install-without-internet-access.md)
