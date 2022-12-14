---
title: CAB download updates for offline install
description: Download Python and R CAB files for SQL Server Machine Learning Services. These CAB files contain updates to the Machine Learning Services (Python and R) feature and are used when installing SQL Server on a server without internet access.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 09/21/2022
ms.service: sql
ms.subservice: machine-learning-services
ms.topic: how-to
ms.custom:
  - seo-lt-2019
  - event-tier1-build-2022
monikerRange: ">=sql-server-2016"
---
# CAB downloads for offline installation of cumulative updates for SQL Server Machine Learning Services

[!INCLUDE [SQL Server 2016 2017 2019](../../includes/applies-to-version/sqlserver2016-2019-only.md)]

::: moniker range="=sql-server-2017||=sql-server-ver15"
Download Python and R CAB files for SQL Server Machine Learning Services. These CAB files contain updates to the Machine Learning Services (Python and R) feature and are used when installing SQL Server on a server without internet access.
::: moniker-end

::: moniker range="=sql-server-2016"
Download Python and R CAB files for SQL Server 2016 R Services. These CAB files contain updates to the the R Services feature and are used when installing SQL Server on a server without internet access.
::: moniker-end

This article applies to [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)], [!INCLUDE[sssql17-md](../../includes/sssql17-md.md)], and [!INCLUDE[sssql19-md](../../includes/sssql19-md.md)].

GDR releases of SQL Server will require the same component .cab file versions as the next earlier non-GDR release, such as a CU. For example, for the SQL Server 2019 CU16 GDR, use the same .cab download versions as SQL Server 2019 CU16.

::: moniker range=">=sql-server-ver16"
> [!IMPORTANT]  
> R and Python runtimes and packages are not shipped or installed by SQL Setup for [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)]. Instead, refer to [Install SQL Server 2022 Machine Learning Services (Python and R) on Windows](sql-machine-learning-services-windows-install-sql-2022.md) or [Install SQL Server Machine Learning Services (Python and R) on Linux](../../linux/sql-server-linux-setup-machine-learning.md).
::: moniker-end

Below you will find download links to CAB files for each cumulative update. For more information about offline installs, see [Install SQL Server machine learning components without internet access](sql-ml-component-install-without-internet-access.md#apply-cu).

## Prerequisites

::: moniker range=">=sql-server-2017"
Start with a baseline installation. On SQL Server Machine Learning Services, the initial release is the baseline installation.  
::: moniker-end

::: moniker range="=sql-server-2016"
Start with a baseline installation. On SQL Server 2016 R Services, you can start with the initial release, SP1, SP2, or SP3.  
::: moniker-end

You can also apply cumulative updates.

::: moniker range="=sql-server-ver15"

## SQL Server 2019 CABs

CAB files are listed in reverse chronological order. When you download the CAB files and transfer them to the target computer, place them in a convenient folder such as **Downloads** or the setup user's %temp% folder.

|Release | Component | Download link | Issues addressed |
|------- | --------- | ------------- | ---------------- |
|**[SQL Server 2019 CU16](https://support.microsoft.com/help/5011644)** |  |  |  |
| | Microsoft R Open      | [SRO_3.5.2.777_1033.cab](https://go.microsoft.com/fwlink/?linkid=2134897)  |  |
| | R Server              | [SRS_9.4.7.2008_1033.cab](https://go.microsoft.com/fwlink/?linkid=2191223)  |  |
| | Microsoft Python Open | [SPO_4.5.12.479_1033.cab](https://go.microsoft.com/fwlink/?LinkId=2118341) |  |
| | Python Server         | [SPS_9.4.7.2008_1033.cab](https://go.microsoft.com/fwlink/?linkid=2191314)  |  |
|**[SQL Server 2019 CU8-CU15](https://support.microsoft.com/help/5008996)** |  |  |  |
| | Microsoft R Open      | [SRO_3.5.2.777_1033.cab](https://go.microsoft.com/fwlink/?linkid=2134897)  |  |
| | R Server              | [SRS_9.4.7.958_1033.cab](https://go.microsoft.com/fwlink/?linkid=2136942)  |  |
| | Microsoft Python Open | [SPO_4.5.12.479_1033.cab](https://go.microsoft.com/fwlink/?LinkId=2118341) |  |
| | Python Server         | [SPS_9.4.7.958_1033.cab](https://go.microsoft.com/fwlink/?linkid=2136731)  |  |
|**[SQL Server 2019 CU5](https://support.microsoft.com/help/4552255)** |  |  |  |
| | Microsoft R Open      | [SRO_3.5.2.293_1033.cab](https://go.microsoft.com/fwlink/?LinkId=2118178)  |  |
| | R Server              | [SRS_9.4.7.804_1033.cab](https://go.microsoft.com/fwlink/?linkid=2122004)  |  |
| | Microsoft Python Open | [SPO_4.5.12.479_1033.cab](https://go.microsoft.com/fwlink/?LinkId=2118341) |  |
| | Python Server         | [SPS_9.4.7.804_1033.cab](https://go.microsoft.com/fwlink/?linkid=2121809)  |  |
|**[SQL Server 2019 CU3](https://support.microsoft.com/help/4538853)** |  |  |  |
| | Microsoft R Open      | [SRO_3.5.2.293_1033.cab](https://go.microsoft.com/fwlink/?LinkId=2118178)  |  |
| | R Server              | [SRS_9.4.7.717_1033.cab](https://go.microsoft.com/fwlink/?LinkId=2118177)  |  |
| | Microsoft Python Open | [SPO_4.5.12.479_1033.cab](https://go.microsoft.com/fwlink/?LinkId=2118341) |  |
| | Python Server         | [SPS_9.4.7.717_1033.cab](https://go.microsoft.com/fwlink/?LinkId=2118342)  |  |
|**[SQL Server 2019 CU2](https://support.microsoft.com/help/4536075)** |  |  |  |
| | Microsoft R Open      | [SRO_3.5.2.125_1033.cab](https://go.microsoft.com/fwlink/?LinkId=2085686)  |  |
| | R Server              | [SRS_9.4.7.35_1033.cab](https://go.microsoft.com/fwlink/?LinkId=2113250)   |  |
| | Microsoft Python Open | [SPO_4.5.12.692_1033.cab](https://go.microsoft.com/fwlink/?LinkId=2113303) |  |
| | Python Server         | [SPS_9.4.7.35_1033.cab](https://go.microsoft.com/fwlink/?LinkId=2113067)   |  |
|**[SQL Server 2019 CU1](https://support.microsoft.com/help/4527376)** |  |  |  |
| | Microsoft R Open      | [SRO_3.5.2.125_1033.cab](https://go.microsoft.com/fwlink/?LinkId=2085686)  |  |
| | R Server              | [SRS_9.4.7.25_1033.cab](https://go.microsoft.com/fwlink/?LinkId=2085792)   |  |
| | Microsoft Python Open | [SPO_4.5.12.120_1033.cab](https://go.microsoft.com/fwlink/?LinkId=2085793) |  |
| | Python Server         | [SPS_9.4.7.25_1033.cab](https://go.microsoft.com/fwlink/?LinkId=2085685)   |  |
|**Initial Release** | | | |
| | Microsoft R Open       | [SRO_3.5.2.125_1033.cab](https://go.microsoft.com/fwlink/?linkid=2085686)  |  |
| | R Server               | [SRS_9.4.7.25_1033.cab](https://go.microsoft.com/fwlink/?linkid=2085792)|   |
| | Microsoft Python Open  | [SPO_4.5.12.120_1033.cab](https://go.microsoft.com/fwlink/?linkid=2085793) |  |
| | Python Server          | [SPS_9.4.7.25_1033.cab](https://go.microsoft.com/fwlink/?linkid=2085685)   |  |

::: moniker-end

::: moniker range="=sql-server-2017"

## SQL Server 2017 CABs

CAB files are listed in reverse chronological order. When you download the CAB files and transfer them to the target computer, place them in a convenient folder such as **Downloads** or the setup user's %temp% folder.

|Release  |Component | Download link  | Issues addressed |
|---------|----------|----------------|------------------|
|**[SQL Server 2017 CU29](https://support.microsoft.com//help/5010786/)** |  |  |  |
| | Microsoft R Open      | [SRO_3.5.2.777_1033.cab](https://go.microsoft.com/fwlink/?linkid=2134897)  |  |
| | R Server              | [SRS_9.4.7.1162_1033.cab](https://go.microsoft.com/fwlink/?linkid=2174362)  |  |
| | Microsoft Python Open | [SPO_4.5.12.479_1033.cab](https://go.microsoft.com/fwlink/?LinkId=2118341) |  |
| | Python Server         | [SPS_9.4.7.1226_1033.cab](https://go.microsoft.com/fwlink/?linkid=2189383)  |Fixes `sp_execute_external_script` execution failures for Python by removing breaking numpy package version mismatch.  |
|**[SQL Server 2017 CU27](https://support.microsoft.com//help/5006944/)-[CU28](https://support.microsoft.com//help/5008084/)** |  |  |  |
| | Microsoft R Open      | [SRO_3.5.2.777_1033.cab](https://go.microsoft.com/fwlink/?linkid=2134897)  |  |
| | R Server              | [SRS_9.4.7.1162_1033.cab](https://go.microsoft.com/fwlink/?linkid=2174362)  |  |
| | Microsoft Python Open | [SPO_4.5.12.479_1033.cab](https://go.microsoft.com/fwlink/?LinkId=2118341) |  |
| | Python Server         | [SPS_9.4.7.1162_1033.cab](https://go.microsoft.com/fwlink/?linkid=2174361)  |  |
|**[SQL Server 2017 CU22](https://support.microsoft.com/help/4577467/)-[CU26](https://support.microsoft.com/help/5005226/)** |  |  |  |
| | Microsoft R Open      | [SRO_3.5.2.777_1033.cab](https://go.microsoft.com/fwlink/?linkid=2134897)  |  |
| | R Server              | [SRS_9.4.7.958_1033.cab](https://go.microsoft.com/fwlink/?linkid=2136942)  |  |
| | Microsoft Python Open | [SPO_4.5.12.479_1033.cab](https://go.microsoft.com/fwlink/?LinkId=2118341) |  |
| | Python Server         | [SPS_9.4.7.958_1033.cab](https://go.microsoft.com/fwlink/?linkid=2136731)  |  |
|**[SQL Server 2017 CU19](https://support.microsoft.com/help/4535007/)-[CU20](https://support.microsoft.com/help/4541283/)** |  |  |  |
| | Microsoft R Open | [SRO_3.3.3.1900_1033.cab](https://go.microsoft.com/fwlink/?LinkId=2106367&clcid=1033) | Fixes the bug where `sp_execute_external_script` executing an R script shows warning message |
| | R Server| [SRS_9.2.0.1900_1033.cab](https://go.microsoft.com/fwlink/?LinkId=2106460&clcid=1033) | No change from previous versions. |
| | Microsoft Python Open | [SPO_9.2.0.1400_1033.cab](https://go.microsoft.com/fwlink/?LinkId=2073897&clcid=1033) | No change from previous versions. |
| | Python Server | [SPS_9.2.0.1900_1033.cab](https://go.microsoft.com/fwlink/?LinkId=2106459&clcid=1033) | Fixes the bug where `sp_execute_external_script` executing a python script sometimes loses data when returning varbinary or binary data type back to SQL Server in the form of an OutputDataSet. |
|**[SQL Server 2017 CU14](https://support.microsoft.com/help/4484710/)-[CU15](https://support.microsoft.com/help/4498951/)-[CU16](https://support.microsoft.com/help/4508218/)-[CU17](https://support.microsoft.com/help/4515579/)-[CU18](https://support.microsoft.com/help/4527377/)** |  |  |  |
| | Microsoft R Open     | [SRO_3.3.3.1400_1033.cab](https://go.microsoft.com/fwlink/?LinkId=2073898&clcid=1033)| Binaries within the package are now signed. |
| | R Server      |[SRS_9.2.0.1400_1033.cab](https://go.microsoft.com/fwlink/?LinkId=2069739&clcid=1033)| Binaries within the package are now signed. |
| | Microsoft Python Open     | [SPO_9.2.0.1400_1033.cab](https://go.microsoft.com/fwlink/?LinkId=2073897&clcid=1033)| Binaries within the package are now signed. |
| | Python Server    |[SPS_9.2.0.1400_1033.cab](https://go.microsoft.com/fwlink/?LinkId=2071421&clcid=1033)| Binaries within the package are now signed.  |
|**[SQL Server 2017 CU13](https://support.microsoft.com/help/4466404)** |  |  |  |
| | Microsoft R Open     | [SRO_3.3.3.1300_1033.cab](https://go.microsoft.com/fwlink/?LinkId=863894)| No change from previous versions. |
| | R Server      |[SRS_9.2.0.1300_1033.cab](https://go.microsoft.com/fwlink/?LinkId=2038263&clcid=1033)| Contains a fix for upgrading an [operationalized standalone R Server](/machine-learning-server/what-is-operationalization), as installed through SQL Server Setup. Use the CU13 CABs and follow [these instructions](sql-machine-learning-standalone-windows-install.md#apply-cu) to apply the update. |
| | Microsoft Python Open     | [SPO_9.2.0.24_1033.cab](https://go.microsoft.com/fwlink/?LinkId=851502)| No change from previous versions. |
| | Python Server    |[SPS_9.2.0.1300_1033.cab](https://go.microsoft.com/fwlink/?LinkId=2038197&clcid=1033)| Contains a fix for upgrading an [operationalized standalone Python Server](/machine-learning-server/what-is-operationalization), as installed through SQL Server Setup. Use the CU13 CABs and follow [these instructions](sql-machine-learning-standalone-windows-install.md#apply-cu) to apply the update. |
|**[SQL Server 2017 CU10](https://support.microsoft.com/help/4342123)-[CU11](https://support.microsoft.com/help/4462262)-[CU12](https://support.microsoft.com/help/4464082)** |  |  |  |
| | Microsoft R Open     | [SRO_3.3.3.300_1033.cab](https://go.microsoft.com/fwlink/?LinkId=863894)| No change from previous versions. |
| | R Server      |[SRS_9.2.0.1000_1033.cab](https://go.microsoft.com/fwlink/?LinkId=2006287&clcid=1033)| Minor fixes.|
| | Microsoft Python Open     | [SPO_9.2.0.24_1033.cab](https://go.microsoft.com/fwlink/?LinkId=851502)| No change from previous versions. |
| | Python Server    |[SPS_9.2.0.1000_1033.cab](https://go.microsoft.com/fwlink/?LinkId=2006805&clcid=1033)| Python rx_data_step loses row order when duplicates are removed. <br/>SPEE fails datatype detection on clustered columnstore index. <br/>Returns an empty table when columns contain all null values. |
|**[SQL Server 2017 CU8](https://support.microsoft.com/help/4338363)-[CU9](https://support.microsoft.com/help/4341265)** |  |  |  |
| | Microsoft R Open     | [SRO_3.3.3.300_1033.cab](https://go.microsoft.com/fwlink/?LinkId=863894)| No change from previous versions. |
| | R Server      |[SRS_9.2.0.800_1033.cab](https://go.microsoft.com/fwlink/?LinkId=874708&clcid=1033)|
| | Microsoft Python Open     | [SPO_9.2.0.24_1033.cab](https://go.microsoft.com/fwlink/?LinkId=851502)| No change from previous versions. |
| | Python Server    |[SPS_9.2.0.800_1033.cab](https://go.microsoft.com/fwlink/?LinkId=874707&clcid=1033)|
|**[SQL Server 2017 CU6](https://support.microsoft.com/help/4101464)-[CU7](https://support.microsoft.com/help/4229789)** |  |  |  |
| | Microsoft R Open     | [SRO_3.3.3.300_1033.cab](https://go.microsoft.com/fwlink/?LinkId=863894)| No change from previous versions. |
| | R Server      |[SRS_9.2.0.600_1033.cab](https://go.microsoft.com/fwlink/?LinkId=871074&clcid=1033)|
| | Microsoft Python Open     | [SPO_9.2.0.24_1033.cab](https://go.microsoft.com/fwlink/?LinkId=851502)| No change from previous versions. |
| | Python Server    |[SPS_9.2.0.600_1033.cab](https://go.microsoft.com/fwlink/?LinkId=871073&clcid=1033)| DateTime data types in SPEES query.<br/>improved error messages in microsoftml when pre-trained models are missing.<br/> Fixes to revoscalepy transform functions and variables.|
|**[SQL Server 2017 CU5](https://support.microsoft.com/help/4092643)** |  |  |  |
| | Microsoft R Open     | [SRO_3.3.3.300_1033.cab](https://go.microsoft.com/fwlink/?LinkId=863894)| No change from previous versions. |
| | R Server      |[SRS_9.2.0.500_1033.cab](https://go.microsoft.com/fwlink/?LinkId=869052&clcid=1033)| Long path-related errors in rxInstallPackages.<br/>Connections in a loopback for RxExec.
| | Microsoft Python Open    | No change from previous versions. |
| | Python Server    |[SPS_9.2.0.500_1033.cab](https://go.microsoft.com/fwlink/?LinkId=869053&clcid=1033)| <br/>Connections in a loopback for rx_exec.
|**[SQL Server 2017 CU4](https://support.microsoft.com/help/4056498)** |  |   |  |
| | Microsoft R Open     | [SRO_3.3.3.300_1033.cab](https://go.microsoft.com/fwlink/?LinkId=863894)| No change from previous versions. |
| | R Server      |[SRS_9.2.0.400_1033.cab](https://go.microsoft.com/fwlink/?LinkId=866212&clcid=1033)|
| | Microsoft Python Open     |[SPO_9.2.0.24_1033.cab](https://go.microsoft.com/fwlink/?LinkId=851502)| No change from previous versions. |
| |  Python Server    |[SPS_9.2.0.400_1033.cab](https://go.microsoft.com/fwlink/?LinkId=866213&clcid=1033)|
|**[SQL Server 2017 CU3](https://support.microsoft.com/help/4052987)** |  |  |  |
| | Microsoft R Open     |[SRO_3.3.3.300_1033.cab](https://go.microsoft.com/fwlink/?LinkId=863894)|
| | R Server      |[SRS_9.2.0.300_1033.cab](https://go.microsoft.com/fwlink/?LinkId=863893)|
| | Microsoft Python Open     |[SPO_9.2.0.24_1033.cab](https://go.microsoft.com/fwlink/?LinkId=851502)| No change from previous versions. |
| | Python Server    |[SPS_9.2.0.300_1033.cab](https://go.microsoft.com/fwlink/?LinkId=863892)| Python model serialization in revoscalepy, using the [rx_serialize_model function](/machine-learning-server/python-reference/revoscalepy/rx-serialize-model).<br/>[Native scoring](../predictions/native-scoring-predict-transact-sql.md) support, plus enhancements to [real-time scoring](../predictions/real-time-scoring.md).  
|**[SQL Server 2017 CU1](https://support.microsoft.com/help/4038634)-[CU2](https://support.microsoft.com/help/4052574)** |  |  |  |
| | Microsoft R Open     | [SRO_3.3.3.24_1033.cab](https://go.microsoft.com/fwlink/?LinkId=851496)| No change from previous versions. |
| | R Server      |[SRS_9.2.0.100_1033.cab](https://go.microsoft.com/fwlink/?LinkId=851501)|
| | Microsoft Python Open     | [SPO_9.2.0.24_1033.cab](https://go.microsoft.com/fwlink/?LinkId=851502)| No change from previous versions. |
| | Python Server    |[SPS_9.2.0.100_1033.cab](https://go.microsoft.com/fwlink/?LinkId=851500) | Adds rx_create_col_info for returning schema information. <br/>Enhancements to [rx_exec](/machine-learning-server/python-reference/revoscalepy/rx-exec) to support parallel scenarios using the `RxLocalParallel` compute context.|
|**Initial release** |  |  |
| | Microsoft R Open     |[SRO_3.3.3.24_1033.cab](https://go.microsoft.com/fwlink/?LinkId=851496)|
| | R Server      |[SRS_9.2.0.24_1033.cab](https://go.microsoft.com/fwlink/?LinkId=851507)|
| | Microsoft Python Open     |[SPO_9.2.0.24_1033.cab](https://go.microsoft.com/fwlink/?LinkId=851502) |
| | Python Server    |[SPS_9.2.0.24_1033.cab](https://go.microsoft.com/fwlink/?LinkId=851508) |

::: moniker-end

::: moniker range="=sql-server-2016"

<a name="bkmk_2016Installers"></a>

## SQL Server 2016 CABs

For SQL Server 2016 R Services, baseline releases are either the RTM version or a service pack version.

|Release  |Download link  |
|---------|---------------|
|**SQL Server 2016 SP2 CU14-CU15**<br />&<br />**SQL Server 2016 SP3**     |
|Microsoft R Open      |[SRO_3.5.2.777_1033.cab](https://go.microsoft.com/fwlink/?linkid=2134897)|
|Microsoft R Server    |[SRS_9.4.7.958_1033.cab](https://go.microsoft.com/fwlink/?linkid=2136942)|
|**SQL Server 2016 SP2 CU6-CU13**     |
|Microsoft R Open     |[SRO_3.2.2.20100_1033.cab](https://go.microsoft.com/fwlink/?LinkId=2079936&clcid=1033)|
|Microsoft R Server    |[SRS_8.0.3.20100_1033.cab](https://go.microsoft.com/fwlink/?LinkId=2079933&clcid=1033)|
|**SQL Server 2016 SP2 CU1-CU5**     |
|Microsoft R Open     |[SRO_3.2.2.16000_1033.cab](https://go.microsoft.com/fwlink/?LinkId=836819)|
|Microsoft R Server    |[SRS_8.0.3.20000_1033.cab](https://go.microsoft.com/fwlink/?LinkId=866038)|
|**SQL Server 2016 SP2**     |
|Microsoft R Open     |[SRO_3.2.2.16000_1033.cab](https://go.microsoft.com/fwlink/?LinkId=836819)|
|Microsoft R Server    |[SRS_8.0.3.17000_1033.cab](https://go.microsoft.com/fwlink/?LinkId=850317)|
|**SQL Server 2016 SP1 CU14**     |
|Microsoft R Open     |[SRO_3.2.2.16100_1033.cab](https://go.microsoft.com/fwlink/?LinkId=2080130&clcid=1033)|
|Microsoft R Server    |[SRS_8.0.3.17200_1033.cab](https://go.microsoft.com/fwlink/?LinkId=2079935&clcid=1033)|
|**SQL Server 2016 SP1 CU1-CU13**     |
|Microsoft R Open     |[SRO_3.2.2.16000_1033.cab](https://go.microsoft.com/fwlink/?LinkId=836819)|
|Microsoft R Server    |[SRS_8.0.3.16000_1033.cab](https://go.microsoft.com/fwlink/?LinkId=836818)|
|**SQL Server 2016 SP1**     |
|Microsoft R Open     |[SRO_3.2.2.15000_1033.cab](https://go.microsoft.com/fwlink/?LinkId=824879)|
|Microsoft R Server     |[SRS_8.0.3.15000_1033.cab](https://go.microsoft.com/fwlink/?LinkId=824881)|
|**SQL Server 2016 CU4-CU9**     |
|Microsoft R Open     |[SRO_3.2.2.13000_1033.cab](https://go.microsoft.com/fwlink/?LinkId=831785)|
|Microsoft R Server     |[SRS_8.0.3.13000_1033.cab](https://go.microsoft.com/fwlink/?LinkId=831676)|
|**SQL Server 2016 CU2-CU3**     |
|Microsoft R Open     |[SRO_3.2.2.12000_1033.cab](https://go.microsoft.com/fwlink/?LinkId=827398)|
|Microsoft R Server     |[SRS_8.0.3.12000_1033.cab](https://go.microsoft.com/fwlink/?LinkId=827399)|
|**SQL Server 2016 CU1**     |
|Microsoft R Open     |[SRO_3.2.2.10000_1033.cab](https://go.microsoft.com/fwlink/?LinkId=808803)|
|Microsoft R Server     |[SRS_8.0.3.10000_1033.cab](https://go.microsoft.com/fwlink/?LinkId=808805)|
|**SQL Server 2016 RTM**     |
|Microsoft R Open     |[SRO_3.2.2.803_1033.cab](https://go.microsoft.com/fwlink/?LinkId=761266)|
|Microsoft R Server     |[SRS_8.0.3.0_1033.cab](https://go.microsoft.com/fwlink/?LinkId=735051)|

> [!NOTE]  
>  
> When installing SQL Server 2016 SP1 CU4 or SP1 CU5 offline, download SRO_3.2.2.16000_1033.cab. If you downloaded SRO_3.2.2.13000_1033.cab from FWLINK 831785 as indicated in the setup dialog box, rename the file as SRO_3.2.2.16000_1033.cab before installing the Cumulative Update.

::: moniker-end

## Next steps

- [Apply cumulative updates on computers without internet access](sql-ml-component-install-without-internet-access.md#apply-cu)

- [Apply cumulative updates on computers having internet connectivity](sql-ml-component-install-without-internet-access.md#apply-cu)

- [Apply cumulative updates to a standalone server](sql-machine-learning-standalone-windows-install.md#apply-cu)
