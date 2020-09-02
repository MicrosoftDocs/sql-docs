---
title: "Release notes for OLE DB Driver"
description: "This release notes article describes the changes in each release of the Microsoft OLE DB Driver for SQL Server."
ms.date: "09/30/2020"
ms.prod: sql
ms.technology: connectivity
ms.topic: conceptual
ms.reviewer: genemi
author: mateusz-kmiecik
ms.author: v-makmie
---
# Release notes for the Microsoft OLE DB Driver for SQL Server

[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

This page discusses what was added in each version of the Microsoft OLE DB Driver for SQL Server.

<!--
USE THE TABLE FORMAT!
Hello, from now on, please use the table-based format standard for all new Release Notes content.
See section "## 18.2.1" for a live example in this article.
Thank you. For questions, contact GeneMi. (2019/03/16)
-->

## 18.5.0
![download](../../ssms/media/download-icon.png) [Download x64 installer](https://go.microsoft.com/fwlink/?linkid=2135577)  
![download](../../ssms/media/download-icon.png) [Download x86 installer](https://go.microsoft.com/fwlink/?linkid=2135722)  

Released: September 2020

If you need to download the installer in a language other than the one detected for you, you can use these direct links.  
    For the x64 driver: [Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2135577&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2135577&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2135577&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2135577&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2135577&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2135577&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2135577&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2135577&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2135577&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2135577&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2135577&clcid=0x40a)  
    For the x86 driver: [Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2135722&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2135722&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2135722&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2135722&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2135722&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2135722&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2135722&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2135722&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2135722&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2135722&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2135722&clcid=0x40a)  

### Features added

| Feature added | Details |
| :------------ | :------ |
| Support for [SQL Data Discovery and Classification](https://docs.microsoft.com/sql/relational-databases/security/sql-data-discovery-and-classification) | [Using data classification](features/using-data-classification.md) |
| Azure Active Directory authentication support (`ActiveDirectoryServicePrincipal`) | [Using Azure Active Directory](features/using-azure-active-directory.md) |

### Bugs fixed

| Bug fixed | Details |
| :-------- | :------ |
| Fixed an issue with embedded NUL characters. | Fixed a bug, which resulted in the driver returning an incorrect length of strings with embedded NUL characters. |
| Fixed a memory leak in the [IBCPSession](ole-db-interfaces/ibcpsession-ole-db.md) interface. | Fixed a memory leak in the [IBCPSession](ole-db-interfaces/ibcpsession-ole-db.md) interface involving bulk copy operations of `sql_variant` data type. |
| Fixed bugs, which resulted in `SSPROP_INTEGRATEDAUTHENTICATIONMETHOD` and `SSPROP_MUTUALLYAUTHENTICATED` properties not returning correct values. | Fix bugs, which resulted in incorrect values being returned when attempting to read the values of these properties. Details about these properties can be found [here](ole-db\service-principal-names-spns-in-client-connections-ole-db.md#data-source-properties).|

## Previous Releases

## 18.4.0
![download](../../ssms/media/download-icon.png) [Download x64 installer](https://go.microsoft.com/fwlink/?linkid=2129954)  
![download](../../ssms/media/download-icon.png) [Download x86 installer](https://go.microsoft.com/fwlink/?linkid=2131003)  

Released: May 2020

If you need to download the installer in a language other than the one detected for you, you can use these direct links.  
For the x64 driver: [Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2129954&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2129954&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2129954&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2129954&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2129954&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2129954&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2129954&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2129954&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2129954&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2129954&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2129954&clcid=0x40a)  
For the x86 driver: [Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2131003&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2131003&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2131003&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2131003&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2131003&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2131003&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2131003&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2131003&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2131003&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2131003&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2131003&clcid=0x40a)  

### Features added

| Feature added | Details |
| :------------ | :------ |
| Support for Transparent Network IP Resolution (TNIR) |[Transparent Network IP Resolution (TNIR)](features/using-transparent-network-ip-resolution.md)|
| Support for UTF-8 client encoding | [UTF-8 Support in OLE DB Driver for SQL Server](features/utf-8-support-in-oledb-driver-for-sql-server.md) |

### Bugs fixed

| Bug fixed | Details |
| :-------- | :------ |
| Fixed various bugs in the [ISequentialStream](https://docs.microsoft.com/previous-versions/windows/desktop/ms718035(v=vs.85)) interface. | A few bugs affecting multibyte code pages resulted in the interface prematurely reporting the end of the stream during the read operation.|
| Fixed a memory leak in the [IOpenRowset::OpenRowset](https://docs.microsoft.com/previous-versions/windows/desktop/ms716724(v=vs.85)) interface. | Fixed a memory leak in the [IOpenRowset::OpenRowset](https://docs.microsoft.com/previous-versions/windows/desktop/ms716724(v=vs.85)) interface when the `SSPROP_IRowsetFastLoad` property was enabled. |
| Fixed a bug in scenarios involving a `sql_variant` data type and non-ASCII strings. | Executing certain scenarios involving a `sql_variant` data type and non-ASCII strings may result in data corruption. For details, see: [Known issues](ole-db-data-types/ssvariant-structure.md#known-issues). |
| Fixed issues with the *Test Connection* button in the [UDL configuration dialog](help-topics/data-link-pages.md). | The *Test Connection* button in the [UDL configuration dialog](help-topics/data-link-pages.md) now honors initialization properties set in the *All* tab. |
| Fixed the `SSPROP_INIT_PACKETSIZE` property default value handling. | Fixed an unexpected error when the `SSPROP_INIT_PACKETSIZE` property was set to its default value of `0`. For details about this property, see [Initialization and Authorization Properties](ole-db-data-source-objects/initialization-and-authorization-properties.md). |
| Fixed buffer overflow issues in [IBCPSession](ole-db-interfaces/ibcpsession-ole-db.md). | Fixed buffer overflow issues when using malformed data files. |
| Fixed accessibility issues. | Fixed accessibility issues in the installer UI and the [SQL Server Login dialog](help-topics/sql-server-login-dialog.md) (reading content, tab stops). |

## 18.3.0

![download](../../ssms/media/download-icon.png) [Download x64 installer](https://go.microsoft.com/fwlink/?linkid=2117515)  
![download](../../ssms/media/download-icon.png) [Download x86 installer](https://go.microsoft.com/fwlink/?linkid=2117517)  

Released: October 2019

If you need to download the installer in a language other than the one detected for you, you can use these direct links.  
For the x64 driver: [Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2117515&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2117515&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2117515&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2117515&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2117515&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2117515&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2117515&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2117515&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2117515&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2117515&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2117515&clcid=0x40a)  
For the x86 driver: [Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2117517&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2117517&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2117517&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2117517&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2117517&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2117517&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2117517&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2117517&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2117517&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2117517&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2117517&clcid=0x40a)  

### Features added

| Feature added | Details |
| :------------ | :------ |
| Azure Active Directory authentication support (`ActiveDirectoryInteractive`, `ActiveDirectoryMSI`) | [Using Azure Active Directory](features/using-azure-active-directory.md) |
| Include Azure Active Directory Authentication Library (adal.dll) in the installer | Now included in the base driver installation, the OLE DB installer will upgrade existing installations of the Microsoft Active Directory Authentication Library for SQL Server, removing it from the list of installed applications in Windows. |
| &nbsp; | &nbsp; |

### Bugs fixed

| Bug fixed | Details |
| :-------- | :------ |
| Fixed drop index logic in [IIndexDefinition::DropIndex](https://go.microsoft.com/fwlink/?linkid=2106448). | Previous versions of the OLE DB driver can't drop a primary key index when the schema ID and the user ID of the owner of the index aren't equal. |
| &nbsp; | &nbsp; |

Download previous OLE DB Driver versions by clicking the download links in the following sections:

## 18.2.3

![download](../../ssms/media/download-icon.png) [Download x64 installer](https://go.microsoft.com/fwlink/?linkid=2119554)  
![download](../../ssms/media/download-icon.png) [Download x86 installer](https://go.microsoft.com/fwlink/?linkid=2119738)  

Released: June 2019

If you need to download the installer in a language other than the one detected for you, you can use these direct links.  
For the x64 driver: [Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2119554&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2119554&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2119554&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2119554&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2119554&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2119554&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2119554&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2119554&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2119554&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2119554&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2119554&clcid=0x40a)  
For the x86 driver: [Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2119738&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2119738&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2119738&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2119738&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2119738&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2119738&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2119738&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2119738&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2119738&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2119738&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2119738&clcid=0x40a)  

### Features added in 18.2.3

| Feature added | Details |
| :------------ | :------ |
| Support for driver upgrades from the SQL Server removable media | This improvement allows driver upgrades directly from the SQL Server removable media. |
| &nbsp; | &nbsp; |

## 18.2.2

![download](../../ssms/media/download-icon.png) [Download x64 installer](https://go.microsoft.com/fwlink/?linkid=2118512)  
![download](../../ssms/media/download-icon.png) [Download x86 installer](https://go.microsoft.com/fwlink/?linkid=2118415)  

Released: May 2019

If you need to download the installer in a language other than the one detected for you, you can use these direct links.  
For the x64 driver: [Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2118512&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2118512&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2118512&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2118512&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2118512&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2118512&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2118512&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2118512&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2118512&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2118512&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2118512&clcid=0x40a)  
For the x86 driver: [Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2118415&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2118415&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2118415&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2118415&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2118415&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2118415&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2118415&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2118415&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2118415&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2118415&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2118415&clcid=0x40a)  

### Bugs fixed in 18.2.2

| Bug fixed | Details |
| :-------- | :------ |
| Fixed non-interactive Azure Active Directory authentication in multithreaded apartment (MTA). | The OLE DB Driver 18.2.1 incorrectly tries to change the COM concurrency model on an apartment that was previously initialized as multithreaded (MTA). As a result, in an application that makes more than one subsequent call to [CoInitialize](https://go.microsoft.com/fwlink/?linkid=2092520) or [CoInitializeEx](https://go.microsoft.com/fwlink/?linkid=2092521) prior to calling the [IDBInitialize::Initialize](https://go.microsoft.com/fwlink/?linkid=2092522) interface, the driver fails to connect when using any of the Azure Active Directory authentication modes. |
| &nbsp; | &nbsp; |

## 18.2.1

![download](../../ssms/media/download-icon.png) [Download x64 installer](https://go.microsoft.com/fwlink/?linkid=2118511)  
![download](../../ssms/media/download-icon.png) [Download x86 installer](https://go.microsoft.com/fwlink/?linkid=2118278)  

Released: February 2019

If you need to download the installer in a language other than the one detected for you, you can use these direct links.  
For the x64 driver: [Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2118511&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2118511&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2118511&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2118511&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2118511&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2118511&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2118511&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2118511&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2118511&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2118511&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2118511&clcid=0x40a)  
For the x86 driver: [Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2118278&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2118278&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2118278&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2118278&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2118278&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2118278&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2118278&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2118278&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2118278&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2118278&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2118278&clcid=0x40a)  

### Features added in 18.2.1

| Feature added | Details |
| :------------ | :------ |
| Support for UTF-8 server encoding | [UTF-8 Support in OLE DB Driver for SQL Server](features/utf-8-support-in-oledb-driver-for-sql-server.md) |
| Azure Active Directory authentication support | [Using Azure Active Directory](features/using-azure-active-directory.md) |
| &nbsp; | &nbsp; |

## 18.1.0

![download](../../ssms/media/download-icon.png) [Download x64 installer](https://go.microsoft.com/fwlink/?linkid=2118506)  
![download](../../ssms/media/download-icon.png) [Download x86 installer](https://go.microsoft.com/fwlink/?linkid=2118509)  

Released: July 2018

If you need to download the installer in a language other than the one detected for you, you can use these direct links.  
For the x64 driver: [Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2118506&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2118506&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2118506&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2118506&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2118506&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2118506&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2118506&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2118506&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2118506&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2118506&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2118506&clcid=0x40a)  
For the x86 driver: [Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2118509&2118509=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2118509&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2118509&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2118509&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2118509&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2118509&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2118509&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2118509&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2118509&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2118509&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2118509&clcid=0x40a)  

### Features added in 18.1.0

| Feature added | Details |
| :------------ | :------ |
| Support for the `UseFMTONLY` connection string keyword, and for the `SSPROP_INIT_USEFMTONLY` initialization property | `UseFMTONLY` controls how metadata is retrieved when connecting to [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and newer.<br/><br/>For more information, see: [Using Connection String Keywords with OLE DB Driver for SQL Server](applications/using-connection-string-keywords-with-oledb-driver-for-sql-server.md). |
| &nbsp; | &nbsp; |

### Bugs fixed in 18.1.0

| Bug fixed | Details |
| :-------- | :------ |
| Fixed incorrect version of the BCP format file. | The OLE DB Driver 18.0 incorrectly sets the version of the BCP format file to 18.0, instead of to 11.0.<br/>Format files generated by the OLE DB Driver 18.0 cannot be read by the OLE DB Driver 18.1.<br/>If you need to use format files generated by the previous version of the driver with the new driver, you can manually edit the files to change the version to 11.0. |
| &nbsp; | &nbsp; |

## 18.0.2

![download](../../ssms/media/download-icon.png) [Download x64 installer](https://go.microsoft.com/fwlink/?linkid=2118504)  
![download](../../ssms/media/download-icon.png) [Download x86 installer](https://go.microsoft.com/fwlink/?linkid=2118277)  

Released: March 2018

If you need to download the installer in a language other than the one detected for you, you can use these direct links.  
For the x64 driver: [Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2118504&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2118504&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2118504&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2118504&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2118504&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2118504&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2118504&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2118504&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2118504&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2118504&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2118504&clcid=0x40a)  
For the x86 driver: [Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2118277&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2118277&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2118277&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2118277&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2118277&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2118277&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2118277&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2118277&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2118277&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2118277&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2118277&clcid=0x40a)  

### Features added in 18.0.2

| Feature added | Details |
| :------------ | :------ |
| Support for the `MultiSubnetFailover` connection string keyword, and the `SSPROP_INIT_MULTISUBNETFAILOVER` initialization property. | For more information, see:<br/>&bull; &nbsp; [OLE DB Driver for SQL Server Support for High Availability, Disaster Recovery](features/oledb-driver-for-sql-server-support-for-high-availability-disaster-recovery.md),<br/>&bull; &nbsp; [Using Connection String Keywords with OLE DB Driver for SQL Server](applications/using-connection-string-keywords-with-oledb-driver-for-sql-server.md). |
| &nbsp; | &nbsp; |

## See also

[Microsoft OLE DB Driver for SQL Server](oledb-driver-for-sql-server.md)
