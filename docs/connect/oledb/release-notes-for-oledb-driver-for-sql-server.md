---
title: "Release notes (OLE DB Driver for SQL Server) | Microsoft Docs"
ms.date: "02/27/2020"
ms.prod: sql
ms.technology: connectivity
ms.topic: conceptual
ms.reviewer: genemi
author: mateusz-kmiecik
ms.author: v-makmie
---
# Release notes for the Microsoft OLE DB Driver for SQL Server

[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

[!INCLUDE[Driver_OLEDB_Download](../../includes/driver_oledb_download.md)]

This page discusses what was added in each version of the Microsoft OLE DB Driver for SQL Server.

<!--
USE THE TABLE FORMAT!
Hello, from now on, please use the table-based format standard for all new Release Notes content.
See section "## 18.2.1" for a live example in this article.
Thank you. For questions, contact GeneMi. (2019/03/16)
-->

## 18.3.0

![download](../../ssdt/media/download.png) [Download x64 installer](https://go.microsoft.com/fwlink/?linkid=2117515)  
![download](../../ssdt/media/download.png) [Download x86 installer](https://go.microsoft.com/fwlink/?linkid=2117517)  

Released: October 2019

If you need to download the installer in a language other than the one detected for you, you can use these direct links.  
For the x64 driver: [Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2117515&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2117515&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2117515&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2117515&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2117515&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2117515&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2117515&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2117515&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2117515&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2117515&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2117515&clcid=0x40a)  
For the x86 driver: [Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2117517&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2117517&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2117517&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2117517&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2117517&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2117517&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2117517&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2117517&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2117517&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2117517&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2117517&clcid=0x40a)  

### Features added

| Feature added | Details |
| :------------ | :------ |
| Azure Active Directory authentication support (`ActiveDirectoryInteractive`, `ActiveDirectoryMSI`). | [Using Azure Active Directory](features/using-azure-active-directory.md). |
| Include Azure Active Directory Authentication Library (adal.dll) in the installer | Now included in the base driver installation, this will upgrade existing installations of the Microsoft Active Directory Authentication Library for SQL Server, removing it from the list of installed applications in Windows. |
| &nbsp; | &nbsp; |

### Bugs fixed

| Bug fixed | Details |
| :-------- | :------ |
| Fixed drop index logic in [IIndexDefinition::DropIndex](https://go.microsoft.com/fwlink/?linkid=2106448). | Previous versions of the OLE DB driver can't drop a primary key index when the schema ID and the user ID of the owner of the index aren't equal. |
| &nbsp; | &nbsp; |

## Previous Releases

Download previous OLE DB Driver versions by clicking the download links in the following sections:

## 18.2.3

![download](../../ssdt/media/download.png) [Download x64 installer](https://go.microsoft.com/fwlink/?linkid=2119554)  
![download](../../ssdt/media/download.png) [Download x86 installer](https://go.microsoft.com/fwlink/?linkid=2119738)  

Released: June 2019

If you need to download the installer in a language other than the one detected for you, you can use these direct links.  
For the x64 driver: [Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2119554&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2119554&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2119554&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2119554&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2119554&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2119554&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2119554&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2119554&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2119554&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2119554&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2119554&clcid=0x40a)  
For the x86 driver: [Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2119738&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2119738&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2119738&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2119738&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2119738&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2119738&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2119738&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2119738&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2119738&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2119738&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2119738&clcid=0x40a)  

### Features added

| Feature added | Details |
| :------------ | :------ |
| Support for driver upgrades from the SQL Server removable media. | This improvement allows driver upgrades directly from the SQL Server removable media. |
| &nbsp; | &nbsp; |

## 18.2.2

![download](../../ssdt/media/download.png) [Download x64 installer](https://go.microsoft.com/fwlink/?linkid=2118512)  
![download](../../ssdt/media/download.png) [Download x86 installer](https://go.microsoft.com/fwlink/?linkid=2118415)  

Released: May 2019

If you need to download the installer in a language other than the one detected for you, you can use these direct links.  
For the x64 driver: [Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2118512&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2118512&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2118512&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2118512&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2118512&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2118512&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2118512&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2118512&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2118512&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2118512&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2118512&clcid=0x40a)  
For the x86 driver: [Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2118415&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2118415&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2118415&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2118415&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2118415&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2118415&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2118415&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2118415&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2118415&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2118415&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2118415&clcid=0x40a)  

### Bugs fixed

| Bug fixed | Details |
| :-------- | :------ |
| Fixed non-interactive Azure Active Directory authentication in multithreaded apartment (MTA). | The OLE DB Driver 18.2.1 incorrectly tries to change the COM concurrency model on an apartment that was previously initialized as multithreaded (MTA). As a result, in an application that makes more than one subsequent call to [CoInitialize](https://go.microsoft.com/fwlink/?linkid=2092520) or [CoInitializeEx](https://go.microsoft.com/fwlink/?linkid=2092521) prior to calling the [IDBInitialize::Initialize](https://go.microsoft.com/fwlink/?linkid=2092522) interface, the driver fails to connect when using any of the Azure Active Directory authentication modes. |
| &nbsp; | &nbsp; |

## 18.2.1

![download](../../ssdt/media/download.png) [Download x64 installer](https://go.microsoft.com/fwlink/?linkid=2118511)  
![download](../../ssdt/media/download.png) [Download x86 installer](https://go.microsoft.com/fwlink/?linkid=2118278)  

Released: February 2019

If you need to download the installer in a language other than the one detected for you, you can use these direct links.  
For the x64 driver: [Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2118511&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2118511&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2118511&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2118511&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2118511&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2118511&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2118511&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2118511&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2118511&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2118511&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2118511&clcid=0x40a)  
For the x86 driver: [Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2118278&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2118278&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2118278&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2118278&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2118278&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2118278&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2118278&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2118278&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2118278&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2118278&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2118278&clcid=0x40a)  

### Features added

| Feature added | Details |
| :------------ | :------ |
| Support for UTF-8 server encoding. | [UTF-8 Support in OLE DB Driver for SQL Server](features/utf-8-support-in-oledb-driver-for-sql-server.md). |
| Azure Active Directory authentication support. | [Using Azure Active Directory](features/using-azure-active-directory.md). |
| &nbsp; | &nbsp; |

## 18.1.0

![download](../../ssdt/media/download.png) [Download x64 installer](https://go.microsoft.com/fwlink/?linkid=2118506)  
![download](../../ssdt/media/download.png) [Download x86 installer](https://go.microsoft.com/fwlink/?linkid=2118509)  

Released: July 2018

If you need to download the installer in a language other than the one detected for you, you can use these direct links.  
For the x64 driver: [Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2118506&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2118506&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2118506&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2118506&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2118506&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2118506&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2118506&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2118506&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2118506&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2118506&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2118506&clcid=0x40a)  
For the x86 driver: [Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2118509&2118509=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2118509&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2118509&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2118509&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2118509&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2118509&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2118509&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2118509&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2118509&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2118509&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2118509&clcid=0x40a)  

### Features added

| Feature added | Details |
| :------------ | :------ |
| Support for the `UseFMTONLY` connection string keyword, and for the `SSPROP_INIT_USEFMTONLY` initialization property. | `UseFMTONLY` controls how metadata is retrieved when connecting to [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and newer.<br/><br/>For more information, see: [Using Connection String Keywords with OLE DB Driver for SQL Server](applications/using-connection-string-keywords-with-oledb-driver-for-sql-server.md). |
| &nbsp; | &nbsp; |

### Bugs fixed

| Bug fixed | Details |
| :-------- | :------ |
| Fixed incorrect version of the BCP format file. | The OLE DB Driver 18.0 incorrectly sets the version of the BCP format file to 18.0, instead of to 11.0.<br/>Format files generated by the OLE DB Driver 18.0 cannot be read by the OLE DB Driver 18.1.<br/>If you need to use format files generated by the previous version of the driver with the new driver, you can manually edit the files to change the version to 11.0. |
| &nbsp; | &nbsp; |

## 18.0.2

![download](../../ssdt/media/download.png) [Download x64 installer](https://go.microsoft.com/fwlink/?linkid=2118504)  
![download](../../ssdt/media/download.png) [Download x86 installer](https://go.microsoft.com/fwlink/?linkid=2118277)  

Released: March 2018

If you need to download the installer in a language other than the one detected for you, you can use these direct links.  
For the x64 driver: [Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2118504&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2118504&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2118504&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2118504&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2118504&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2118504&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2118504&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2118504&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2118504&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2118504&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2118504&clcid=0x40a)  
For the x86 driver: [Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2118277&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2118277&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2118277&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2118277&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2118277&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2118277&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2118277&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2118277&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2118277&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2118277&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2118277&clcid=0x40a)  

### Features added

| Feature added | Details |
| :------------ | :------ |
| Support for the `MultiSubnetFailover` connection string keyword, and the `SSPROP_INIT_MULTISUBNETFAILOVER` initialization property. | For more information, see:<br/>&bull; &nbsp; [OLE DB Driver for SQL Server Support for High Availability, Disaster Recovery](features/oledb-driver-for-sql-server-support-for-high-availability-disaster-recovery.md),<br/>&bull; &nbsp; [Using Connection String Keywords with OLE DB Driver for SQL Server](applications/using-connection-string-keywords-with-oledb-driver-for-sql-server.md). |
| &nbsp; | &nbsp; |

## See also

[Microsoft OLE DB Driver for SQL Server](oledb-driver-for-sql-server.md)
