---
title: Previous releases of SQL Server Data Tools (SSDT)
description: Find out which versions of SSDT and SSDT-BI work with which versions of Visual Studio. See how to install different versions of SSDT and SSDT-BI.
ms.prod: sql
ms.technology: ssdt
ms.topic: conceptual
ms.assetid: 5d32e301-0f44-4916-b0db-76e8322c0ab7
author: dzsquared
ms.author: drskwier
ms.reviewer: maghan
ms.custom: seo-lt-2019
ms.date: 06/17/2020
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||=azuresqldb-mi-current"
---

# Previous releases of SQL Server Data Tools (SSDT and SSDT-BI)

[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

SQL Server Data Tools (SSDT) provides project templates and design surfaces for building SQL Server content types - relational databases, Analysis Services models, Reporting Services reports, and Integration Services packages.

SSDT is backwards compatible, so you can always use [the newest SSDT](download-sql-server-data-tools-ssdt.md) to design and deploy databases, models, reports, and packages that run on older versions of SQL Server.

Historically, the Visual Studio shell used to create SQL Server content types has been released under various names, including **SQL Server Data Tools**, **SQL Server Data Tools - Business Intelligence**, and **Business Intelligence Development Studio**. Previous versions came with distinct sets of project templates. To get all of the project templates together in one SSDT, you need [the newest version](download-sql-server-data-tools-ssdt.md). Otherwise, you probably need to install multiple previous versions to get all of the templates used in SQL Server. Only one shell is installed per version of Visual Studio; installing a second SSDT just adds the missing templates.

## Previous SSDT releases

Download previous SSDT versions by selecting the download link in the related section.

| SSDT version | Visual Studio version |
|--------------|-----------------------|
| [15.8](#ssdt-for-visual-studio-vs-2017) | 2017 |
| [17.4](#ssdt-for-visual-studio-vs-2015) | 2015 |
| [16.5](#ssdt-for-visual-studio-vs-2013) | 2013 |
| [11.1.50727.1](#ssdt-for-visual-studio-vs-2012) | 2012 |

### SSDT for Visual Studio (VS) 2017

**[Download SSDT for Visual Studio 2017 (15.8)](https://go.microsoft.com/fwlink/?linkid=2124319)**

This **SSDT for Visual Studio 2017** release can be installed in the following languages:

[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2124319&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2124319&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2124319&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2124319&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2124319&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2124319&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2124319&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2124319&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2124319&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2124319&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2124319&clcid=0x40a)

### SSDT for Visual Studio (VS) 2015

To install this version of SSDT, you must download an ISO image. The ISO file is a self-contained file that contains all of the components needed by SSDT and it can be downloaded using a restartable download manager, useful for situations with limited, or less reliable network bandwidth. Once downloaded, the ISO can be mounted as a drive.

Steps to install:

1. **[Download SSDT for Visual Studio 2015 (17.4)](https://go.microsoft.com/fwlink/?linkid=2132817)**.

2. Open the ISO image.

3. Run the *SSDTSetup.exe* file.

    ![ISO image](media/previous-releases-of-sql-server-data-tools-ssdt-and-ssdt-bi/iso-image.png)

This **SSDT for Visual Studio 2015** release can be installed in the following languages:

| Language | SHA256 Hash |
|----------|-------------|
| [Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2132817&clcid=0x804) | 79A958B122DDEC2857F1F4B9F0272A6BD2106BB17B4DA94CC68CACFCDDC16EAE |
| [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2132817&clcid=0x404) | 8F9661883F2D2D28D6928AE66242DB465DBB25696EDFE0348D222421CEAB000A |
| [English (United States)](https://go.microsoft.com/fwlink/?linkid=2132817&clcid=0x409) | 7727BA227A9E49C2DFCC1E9F2A10924CC472E3425653DC7DE8E4B830712B302E |
| [French](https://go.microsoft.com/fwlink/?linkid=2132817&clcid=0x40c) | 2DF6655819F604E8D20A396CA9FDFEE279C5A9E50776FFC143A5BA4C3E2F1849 |
| [German](https://go.microsoft.com/fwlink/?linkid=2132817&clcid=0x407) | 5C44502DEE8B31675D0B10B4CE8CA6F5D96A2692CC498B9859A77C24F30EF870 |
| [Italian](https://go.microsoft.com/fwlink/?linkid=2132817&clcid=0x410) | 6A616F6E3A1C7DD52457FB8C8692E5C1C551032FF46AD8C5112CF9364F17C631 |
| [Japanese](https://go.microsoft.com/fwlink/?linkid=2132817&clcid=0x411) | 1244A5EADF673E4BD36E9967A2008F65CA2A1D40E8E7DD4D17640A6FC1EACA9A |
| [Korean](https://go.microsoft.com/fwlink/?linkid=2132817&clcid=0x412) | 6E0E057A853F54AB87F3F8984954F590FCCD3BE2EC2139F02EFA085FEA6D3E61 |
| [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2132817&clcid=0x416) | 24122C092464B299F41A7644814F375F0CC2786877BCAE0282221FF8D73BD100 |
| [Russian](https://go.microsoft.com/fwlink/?linkid=2132817&clcid=0x419) | 2CDE208C241C3F13D2EC37294C10503C7A9D1222ED33F6FE54849169F30BE697 |
| [Spanish](https://go.microsoft.com/fwlink/?linkid=2132817&clcid=0x40a) | BD12844E6F0A0ECFD5815D01EDFB8018CE9B4A1044DE8DF96AC740D85E800FD6 |

### SSDT for Visual Studio (VS) 2013

To install this version of SSDT, you must download an ISO image. The ISO file is a self-contained file that contains all of the components needed by SSDT and it can be downloaded using a restartable download manager, useful for situations with limited, or less reliable network bandwidth. Once downloaded, the ISO can be mounted as a drive.

Steps to install:

1. **[Download SSDT for Visual Studio 2013 (16.5)](https://go.microsoft.com/fwlink/?linkid=832312)**

2. Open the ISO image.

3. Run the *SSDTSetup.exe* file.

    ![ISO image](media/previous-releases-of-sql-server-data-tools-ssdt-and-ssdt-bi/iso-image.png)

This **SSDT for Visual Studio 2013** release can be installed in the following languages:

| Language | SHA256 Hash |
|----------|-------------|
| [Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=832312&clcid=0x804) | 79A958B122DDEC2857F1F4B9F0272A6BD2106BB17B4DA94CC68CACFCDDC16EAE |
| [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=832312&clcid=0x404) | 8F9661883F2D2D28D6928AE66242DB465DBB25696EDFE0348D222421CEAB000A |
| [English (United States)](https://go.microsoft.com/fwlink/?linkid=832312&clcid=0x409) | 7727BA227A9E49C2DFCC1E9F2A10924CC472E3425653DC7DE8E4B830712B302E |
| [French](https://go.microsoft.com/fwlink/?linkid=832312&clcid=0x40c) | 2DF6655819F604E8D20A396CA9FDFEE279C5A9E50776FFC143A5BA4C3E2F1849 |
| [German](https://go.microsoft.com/fwlink/?linkid=832312&clcid=0x407) | 5C44502DEE8B31675D0B10B4CE8CA6F5D96A2692CC498B9859A77C24F30EF870 |
| [Italian](https://go.microsoft.com/fwlink/?linkid=832312&clcid=0x410) | 6A616F6E3A1C7DD52457FB8C8692E5C1C551032FF46AD8C5112CF9364F17C631 |
| [Japanese](https://go.microsoft.com/fwlink/?linkid=832312&clcid=0x411) | 1244A5EADF673E4BD36E9967A2008F65CA2A1D40E8E7DD4D17640A6FC1EACA9A |
| [Korean](https://go.microsoft.com/fwlink/?linkid=832312&clcid=0x412) | 6E0E057A853F54AB87F3F8984954F590FCCD3BE2EC2139F02EFA085FEA6D3E61 |
| [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=832312&clcid=0x416) | 24122C092464B299F41A7644814F375F0CC2786877BCAE0282221FF8D73BD100 |
| [Russian](https://go.microsoft.com/fwlink/?linkid=832312&clcid=0x419) | 2CDE208C241C3F13D2EC37294C10503C7A9D1222ED33F6FE54849169F30BE697 |
| [Spanish](https://go.microsoft.com/fwlink/?linkid=832312&clcid=0x40a) | BD12844E6F0A0ECFD5815D01EDFB8018CE9B4A1044DE8DF96AC740D85E800FD6 |

### SSDT for Visual Studio (VS) 2012

To install this version of SSDT, you must download an ISO image. The ISO file is a self-contained file that contains all of the components needed by SSDT and it can be downloaded using a restartable download manager, useful for situations with limited, or less reliable network bandwidth. Once downloaded, the ISO can be mounted as a drive.

Steps to install:

1. **[Download SSDT for Visual Studio 2012 (11.1.50727.1)](https://go.microsoft.com/fwlink/?linkid=518814)**

2. Open the ISO image.

3. Run the *SSDTSetup.exe* file.

    ![ISO image](media/previous-releases-of-sql-server-data-tools-ssdt-and-ssdt-bi/iso-image.png)

This **SSDT for Visual Studio 2012** release can be installed in the following languages:

| Language | SHA256 Hash |
|----------|-------------|
| [Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=518814&clcid=0x804) | 78F20325648CFF49D9C58A26186E0AC199D104B3CF634E5663B4B262BEC69C07 |
| [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=518814&clcid=0x404) | A2722380AF2EE1E8BB52B4FA54A61BAB411E5E5FD5B050108F81ED23DC87366D |
| [English (United States)](https://go.microsoft.com/fwlink/?linkid=518814&clcid=0x409) | 748EF78D3F9CC6FE360432C378EA690DE51AEB2C747E87D43E08448D26F93A91 |
| [French](https://go.microsoft.com/fwlink/?linkid=518814&clcid=0x40c) | 40E6504169BF618EDC7EB5B62D1215DC96726D6EEC3CA8EC3EEB49044E4B6FB7 |
| [German](https://go.microsoft.com/fwlink/?linkid=518814&clcid=0x407) | C45C974E6B8F9611BA2AC1EE90C5C507992BCE5693BF46F6C7C138591ED6A123 |
| [Italian](https://go.microsoft.com/fwlink/?linkid=518814&clcid=0x410) | C1B20CDB41C7B1B5DE76A71F9A091A6FDF5186B12F6AA269DA6338B3CB2D91A6 |
| [Japanese](https://go.microsoft.com/fwlink/?linkid=518814&clcid=0x411) | BF56958B904C1C5F28084BD0B16928B6CBFEC83EB3F0C913EC364FA335241943 |
| [Korean](https://go.microsoft.com/fwlink/?linkid=518814&clcid=0x412) | 9023B0656785C357A67E39EB76A2806B923C2BD17342D7226A8ADA384A9F4E28 |
| [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=518814&clcid=0x416) | ACAD9FE03729E289ECE821D92C56CFB1D7FCCEA8423ABF1E164BF3C67ABEFEFB |
| [Russian](https://go.microsoft.com/fwlink/?linkid=518814&clcid=0x419) | E2191D787BA833DF4A85B064C5373DC44099E76214FBF9505728702D4D6B83F0 |
| [Spanish](https://go.microsoft.com/fwlink/?linkid=518814&clcid=0x40a) | 6D81FB572A7003C54C29D2ACF076D2CED4A1CA80F329BFF9D41A806920D64EEE |

> [!Note]
> SSDT supports the two most recent versions of Visual Studio. With the release of Visual Studio 2019, SSDT versions for Visual Studio 2015 and earlier are no longer updated. SSDT for Visual Studio 2010 is no longer available. For more information, see the *FAQ* section of [this SSDT team blog post](https://blogs.msdn.microsoft.com/ssdt/2017/03/10/sql-server-data-tools-17-0-rc-and-ssdt-in-vs2017/).

## SQL BI: Analysis Services, Reporting Services, Integration services

BI templates are used to create SSAS models, SSRS reports, and SSIS packages. BI Designers are tied to specific releases of SQL Server. To use the newer BI features, install a newer versioned BI designer.

## BI Designers

[Download SSDT-BI for Visual Studio 2013](https://www.microsoft.com/download/details.aspx?id=42313) (SQL Server 2014, SQL Server 2012, SQL Server 2008, and 2008 R2)

[Download SSDT-BI for Visual Studio 2012](https://www.microsoft.com/download/details.aspx?id=36843) (SQL Server 2014, SQL Server 2012, SQL Server 2008, and 2008 R2

Business Intelligence Development Studio (BIDS) is installed via SQL Server Setup. There's no web download (SQL Server 2008, and 2008 R2).

For SQL Server 2012 or 2014, you can use either **SSDT-BI for Visual Studio 2012** or **SSDT-BI for Visual Studio 2013**. The only difference between the two is the Visual Studio version.

## Next steps

- [Download SQL Server Data Tools (SSDT)](../ssdt/download-sql-server-data-tools-ssdt.md)
- [SQL Server Data Tools (SSDT) Release Notes](release-notes-ssdt.md)
- [Download SQL Server Management Studio (SSMS)](../ssms/download-sql-server-management-studio-ssms.md)
- [Download Azure Data Studio](../azure-data-studio/download-azure-data-studio.md)
- [SQL Tools and Utilities](../tools/overview-sql-tools.md)