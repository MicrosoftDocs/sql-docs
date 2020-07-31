---
title: "Release Notes for ODBC Driver for SQL Server on Windows"
description: "This release notes article describes the changes in each release of the Microsoft ODBC driver for SQL Server on Windows."
ms.custom: ""
ms.date: "03/10/2020"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: b8459ed8-625e-4d8b-891c-e7e78c9977cc
ms.reviewer: "v-chojas"
author: v-makouz
ms.author: v-chojas
manager: kenvh
---
# Release Notes for Microsoft ODBC Driver for SQL Server on Windows

This release notes article describes what's new for the Microsoft ODBC driver for SQL Server on Windows.

<!--
PLEASE USE THE STANDARD 2-COLUMN TABLE FORMAT!

For all our Release Notes articles (What's New too?), we are standardizing on the 2-column format that you see here for version "## 17.3".

Going forward, all new additions to this article must use the 2-column format.

Also, use the shorter ## H2 title format, which eliminates all the redundant constants, and appends the date-added.
One beneift of shortness is the avoidance of the annoying wrapping of unnecessarily long H2 titles in the rightNav.
- OLD H2:  ## What's New in the [!INCLUDE[msCoName](../../../includes/msconame_md.md)] ODBC Driver 17.3 for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] on Windows
- NEW H2:  ## 17.3, February 2019

By the way, in GitHub, the file name is changing today 2019/03/30:
- FROM:  docs/connect/odbc/windows/release-notes.md
- TO  :  docs/connect/odbc/windows/release-notes-odbc-sql-server-windows.md

Thank you.
GeneMi (and CraigG).  2019/03/30.
-->

## 17.6

![download](../../../ssms/media/download-icon.png) [Download x64 installer](https://go.microsoft.com/fwlink/?linkid=2137027)  
![download](../../../ssms/media/download-icon.png) [Download x86 installer](https://go.microsoft.com/fwlink/?linkid=2137028)  

Version number: 17.6.1.1  
Released: July 31, 2020

If you need to download the installer in a language other than the one detected for you, you can use these direct links.  
For the x64 driver: [Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2137027&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2137027&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2137027&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2137027&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2137027&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2137027&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2137027&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2137027&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2137027&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2137027&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2137027&clcid=0x40a)  
For the x86 driver: [Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2137028&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2137028&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2137028&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2137028&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2137028&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2137028&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2137028&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2137028&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2137028&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2137028&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2137028&clcid=0x40a)

| Feature added | Details |
| :------- | :------ |
| Metadata caching for prepared statements | See [Using Always Encrypted](../using-always-encrypted-with-the-odbc-driver.md). |
| SQL_COPT_SS_AUTOBEGINTXN connection attribute to control whether automatic BEGIN TRANSACTION happens after ROLLBACK or COMMIT | See [DSN and Connection String Attributes and Keywords](../dsn-connection-string-attribute.md). |
| Bug fixes. | [Bug fixes](../bug-fixes.md). |
| &nbsp; | &nbsp; |

## Previous Releases

## 17.5.2

![download](../../../ssms/media/download-icon.png) [Download x64 installer](https://go.microsoft.com/fwlink/?linkid=2120137)  
![download](../../../ssms/media/download-icon.png) [Download x86 installer](https://go.microsoft.com/fwlink/?linkid=2120140)  

Version number: 17.5.2.1  
Released: March 6, 2019

If you need to download the installer in a language other than the one detected for you, you can use these direct links.  
For the x64 driver: [Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2120137&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2120137&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2120137&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2120137&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2120137&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2120137&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2120137&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2120137&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2120137&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2120137&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2120137&clcid=0x40a)  
For the x86 driver: [Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2120140&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2120140&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2120140&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2120140&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2120140&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2120140&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2120140&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2120140&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2120140&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2120140&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2120140&clcid=0x40a)  

### Features added in 17.5.2

| Feature added | Details |
| :------------ | :------ |
| Support authentication with Managed Identity for Azure Key Vault | See [Using Always Encrypted with the ODBC Driver](../using-always-encrypted-with-the-odbc-driver.md). |
| Support for additional Azure Key Vault endpoints | See [Using Always Encrypted with the ODBC Driver](../using-always-encrypted-with-the-odbc-driver.md). |
| Bug fixes. | See [Bug fixes](../bug-fixes.md). |
| &nbsp; | &nbsp; |

Download previous ODBC Driver versions by clicking the download links in the following sections:

## 17.5

![download](../../../ssms/media/download-icon.png) [Download x64 installer](https://go.microsoft.com/fwlink/?linkid=2120248)  
![download](../../../ssms/media/download-icon.png) [Download x86 installer](https://go.microsoft.com/fwlink/?linkid=2120353)  

Version number: 17.5.1.1  
Released: January 31, 2019

If you need to download the installer in a language other than the one detected for you, you can use these direct links.  
For the x64 driver: [Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2120248&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2120248&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2120248&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2120248&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2120248&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2120248&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2120248&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2120248&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2120248&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2120248&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2120248&clcid=0x40a)  
For the x86 driver: [Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2120353&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2120353&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2120353&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2120353&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2120353&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2120353&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2120353&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2120353&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2120353&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2120353&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2120353&clcid=0x40a)  

### Features added in 17.5

| Feature added | Details |
| :------------ | :------ |
| SQL_COPT_SS_SPID connection attribute to retrieve SPID without round trip to server | See [DSN and Connection String Attributes and Keywords](../dsn-connection-string-attribute.md). |
| Bug fixes. | See [Bug fixes](../bug-fixes.md). |
| &nbsp; | &nbsp; |

## 17.4.2

![download](../../../ssms/media/download-icon.png) [Download x64 installer](https://go.microsoft.com/fwlink/?linkid=2120354)  
![download](../../../ssms/media/download-icon.png) [Download x86 installer](https://go.microsoft.com/fwlink/?linkid=2120249)  

Version number: 17.4.2.1  
Released: October 2019

If you need to download the installer in a language other than the one detected for you, you can use these direct links.  
For the x64 driver: [Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2120354&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2120354&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2120354&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2120354&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2120354&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2120354&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2120354&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2120354&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2120354&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2120354&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2120354&clcid=0x40a)  
For the x86 driver: [Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2120249&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2120249&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2120249&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2120249&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2120249&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2120249&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2120249&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2120249&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2120249&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2120249&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2120249&clcid=0x40a)  

### Features added in 17.4.2

| Feature added | Details |
| :------------ | :------ |
| Support for additional Azure Key Vault endpoints | See [Using Always Encrypted with the ODBC Driver](../using-always-encrypted-with-the-odbc-driver.md). |
| Support for setting data classification version | See [Data Classification](../data-classification.md#bkmk-version). |
| Include Azure Active Directory Authentication Library (adal.dll) in the installer | Now included in the base driver installation, the ODBC installer will upgrade existing installations of the Microsoft Active Directory Authentication Library for SQL Server, removing it from the list of installed applications in Windows. |
| Bug fixes. | See [Bug fixes](../bug-fixes.md). |
| &nbsp; | &nbsp; |

## 17.4

![download](../../../ssms/media/download-icon.png) [Download x64 installer](https://go.microsoft.com/fwlink/?linkid=2120149)  
![download](../../../ssms/media/download-icon.png) [Download x86 installer](https://go.microsoft.com/fwlink/?linkid=2120150)  

Version number: 17.4.1.1  
Released: July 2019

If you need to download the installer in a language other than the one detected for you, you can use these direct links.  
For the x64 driver: [Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2120149&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2120149&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2120149&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2120149&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2120149&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2120149&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2120149&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2120149&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2120149&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2120149&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2120149&clcid=0x40a)  
For the x86 driver: [Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2120150&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2120150&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2120150&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2120150&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2120150&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2120150&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2120150&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2120150&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2120150&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2120150&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2120150&clcid=0x40a)  

### Features added in 17.4

| Feature added | Details |
| :------------ | :------ |
| Always Encrypted with Secure Enclaves. | See [Using Always Encrypted with the ODBC Driver](../using-always-encrypted-with-the-odbc-driver.md). |
| Configurable TCP Keep Alive settings. | See [Connecting to SQL Server](../linux-mac/connection-string-keywords-and-data-source-names-dsns.md). |
| Bug fixes. | See [Bug fixes](../bug-fixes.md). |
| &nbsp; | &nbsp; |

## 17.3

![download](../../../ssms/media/download-icon.png) [Download x64 installer](https://go.microsoft.com/fwlink/?linkid=2120355)  
![download](../../../ssms/media/download-icon.png) [Download x86 installer](https://go.microsoft.com/fwlink/?linkid=2120356)  

Version number: 17.3.1.1  
Released: February 2019

If you need to download the installer in a language other than the one detected for you, you can use these direct links.  
For the x64 driver: [Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2120355&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2120355&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2120355&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2120355&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2120355&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2120355&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2120355&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2120355&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2120355&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2120355&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2120355&clcid=0x40a)  
For the x86 driver: [Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2120356&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2120356&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2120356&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2120356&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2120356&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2120356&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2120356&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2120356&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2120356&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2120356&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2120356&clcid=0x40a)  

### Features added in 17.3

| Feature added | Details |
| :------------ | :------ |
| Azure Active Directory Managed Service Identity (system and user-assigned) authentication mode. | See [Using Azure Active Directory with the ODBC Driver](../using-azure-active-directory.md). |
| Ability to stream input parameters against Always Encrypted columns. | See [Limitations of the ODBC driver when using Always Encrypted](../using-always-encrypted-with-the-odbc-driver.md#limitations-of-the-odbc-driver-when-using-always-encrypted). |
| XA distributed transactions. | [Using XA Transactions](../use-xa-with-dtc.md). |
| Bug fixes. | See [Bug fixes](../bug-fixes.md). |
| &nbsp; | &nbsp; |

## 17.2

![download](../../../ssms/media/download-icon.png) [Download x64 installer](https://go.microsoft.com/fwlink/?linkid=2120250)  
![download](../../../ssms/media/download-icon.png) [Download x86 installer](https://go.microsoft.com/fwlink/?linkid=2120357)  

Version number: 17.2.0.1  
Released: July 2018

If you need to download the installer in a language other than the one detected for you, you can use these direct links.  
For the x64 driver: [Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2120250&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2120250&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2120250&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2120250&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2120250&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2120250&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2120250&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2120250&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2120250&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2120250&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2120250&clcid=0x40a)  
For the x86 driver: [Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2120357&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2120357&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2120357&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2120357&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2120357&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2120357&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2120357&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2120357&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2120357&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2120357&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2120357&clcid=0x40a)  

### Features added in 17.2

| Feature added | Details |
| :------------ | :------ |
| Data Classification for Azure SQL Database and SQL Server. | See [Data Classification](../data-classification.md). |
| Support for UTF-8 server encoding. | &nbsp; |
| Bug fixes. | See [Bug fixes](../bug-fixes.md). |
| &nbsp; | &nbsp; |

## 17.1

![download](../../../ssms/media/download-icon.png) [Download x64 installer](https://go.microsoft.com/fwlink/?linkid=2120151)  
![download](../../../ssms/media/download-icon.png) [Download x86 installer](https://go.microsoft.com/fwlink/?linkid=2120443)  

Version number: 17.1.0.1  
Released: March 2018

If you need to download the installer in a language other than the one detected for you, you can use these direct links.  
For the x64 driver: [Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2120151&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2120151&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2120151&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2120151&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2120151&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2120151&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2120151&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2120151&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2120151&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2120151&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2120151&clcid=0x40a)  
For the x86 driver: [Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2120443&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2120443&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2120443&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2120443&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2120443&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2120443&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2120443&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2120443&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2120443&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2120443&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2120443&clcid=0x40a)  

### Features added in 17.1

| Feature added | Details |
| :------------ | :------ |
| Support for `SQL_COPT_SS_CEKCACHETTL` and `SQL_COPT_SS_TRUSTEDCMKPATHS` connection attributes. | &bull; &nbsp; `SQL_COPT_SS_CEKCACHETTL`<br/>Allows controlling the time that the local cache of Column Encryption Keys exists, as well as flushing it.<br/><br/>&bull; &nbsp; `SQL_COPT_SS_TRUSTEDCMKPATHS`<br/>Allows the application to restrict AE operations to only use the specified list of Column Master Keys.<br/><br/> For more information, see [Using Always Encrypted with the ODBC Driver for SQL Server](../using-always-encrypted-with-the-odbc-driver.md). |
| Azure Active Directory Interactive Authentication Support | &nbsp; |
| Bug fixes. | See [Bug fixes](../bug-fixes.md). |
| &nbsp; | &nbsp; |

## 17.0

![download](../../../ssms/media/download-icon.png) [Download x64 installer](https://go.microsoft.com/fwlink/?linkid=2120444)  
![download](../../../ssms/media/download-icon.png) [Download x86 installer](https://go.microsoft.com/fwlink/?linkid=2120152)  

Version number: 17.0.1.1  
Released: February 2018

If you need to download the installer in a language other than the one detected for you, you can use these direct links.  
For the x64 driver: [Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2120444&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2120444&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2120444&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2120444&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2120444&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2120444&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2120444&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2120444&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2120444&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2120444&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2120444&clcid=0x40a)  
For the x86 driver: [Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2120152&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2120152&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2120152&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2120152&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2120152&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2120152&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2120152&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2120152&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2120152&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2120152&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2120152&clcid=0x40a)  

### Features added in 17.0

| Feature added | Details |
| :------------ | :------ |
| Always Encrypted support for BCP API. | &nbsp; |
| New connection string attribute `UseFMTOnly`. | Causes the driver to use legacy metadata in special cases that require temporary tables. |
| Support for Azure SQL Managed Instance. | See the following list of [Differences when using Managed Instance (ODBC version 17)](#diffs-managed-instance-17). |
| &nbsp; | &nbsp; |

| Dependency changed | Details |
| :------------ | :------ |
| Removed Microsoft online service sign-in assistant | The dependency has been removed. |
| &nbsp; | &nbsp; |

### <a name="diffs-managed-instance-17"></a> Differences when using Managed Instance (ODBC version 17)

This version of ODBC contains support for Azure SQL Managed Instance. See the following noted list of differences when using Managed Instance.

> [!NOTE]
> There are a number of differences when using Managed Instance:
>
> - FILESTREAM is not supported.
> - Local filesystem access is not supported, but is required for things like trace files.
> - Create UDT from local path is not supported.
> - Windows Integrated Authentication is not supported.
> - DTC is not supported.
> - `sa` account is not present (default account is called `cloudSA`).
> - TDS token ERROR (0xAA) returns incorrect server name.
> - Special characters in database name are not supported.
> - ALTER DATABASE [dbname1] MODIFY NAME = [dbname2] is not supported.
> - The error messages are always shown in English, regardless of language settings (same as Azure).

## 13.1

![download](../../../ssms/media/download-icon.png) [Download x64 installer](https://go.microsoft.com/fwlink/?linkid=2121020)  
![download](../../../ssms/media/download-icon.png) [Download x86 installer](https://go.microsoft.com/fwlink/?linkid=2120923)  

Version number: 13.1  

If you need to download the installer in a language other than the one detected for you, you can use these direct links.  
For the x64 driver: [Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2121020&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2121020&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2121020&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2121020&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2121020&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2121020&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2121020&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2121020&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2121020&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2121020&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2121020&clcid=0x40a)  
For the x86 driver: [Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2120923&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2120923&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2120923&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2120923&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2120923&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2120923&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2120923&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2120923&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2120923&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2120923&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2120923&clcid=0x40a)  

[Download the Microsoft Command Line Utilities 13.1 for SQL Server](https://www.microsoft.com/download/details.aspx?id=53591)

### Features added in 13.1

| Feature added | Details |
| :------------ | :------ |
| ODBC Driver 13.1 for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] adds support for [Always Encrypted](../using-always-encrypted-with-the-odbc-driver.md) and [Azure Active Directory](../using-azure-active-directory.md). | These added supports are available when connecting to Microsoft SQL Server 2016, or to a later version. |
| There are connection pooling keywords and attributes, that correspond to the supports for Always Encrypted and Azure Active Directory. | These keywords and attributes are described in [Driver Aware Connection Pooling in the ODBC Driver for SQL Server](driver-aware-connection-pooling-in-the-odbc-driver-for-sql-server.md). |
| &nbsp; | &nbsp; |

## 13

![download](../../../ssms/media/download-icon.png) [Download x64 installer](https://go.microsoft.com/fwlink/?linkid=2121118)  
![download](../../../ssms/media/download-icon.png) [Download x86 installer](https://go.microsoft.com/fwlink/?linkid=2120924)  

Version number: 13  

If you need to download the installer in a language other than the one detected for you, you can use these direct links.  
For the x64 driver: [Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2121118&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2121118&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2121118&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2121118&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2121118&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2121118&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2121118&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2121118&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2121118&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2121118&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2121118&clcid=0x40a)  
For the x86 driver: [Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2120924&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2120924&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2120924&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2120924&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2120924&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2120924&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2120924&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2120924&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2120924&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2120924&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2120924&clcid=0x40a)  

[Download the Microsoft Command Line Utilities 13 for SQL Server](https://www.microsoft.com/download/details.aspx?id=52680)

### Features added in 13

| Feature added | Details |
| :------------ | :------ |
| Adds support for Microsoft SQL Server 2016. | Retains the functionality of ODBC driver version 11. |
| &nbsp; | &nbsp; |

## 11

![download](../../../ssms/media/download-icon.png) [Download x64 installer](https://go.microsoft.com/fwlink/?linkid=2121206)  
![download](../../../ssms/media/download-icon.png) [Download x86 installer](https://go.microsoft.com/fwlink/?linkid=2121021)  

Version number: 11  

If you need to download the installer in a language other than the one detected for you, you can use these direct links.  
For the x64 driver: [Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2121206&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2121206&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2121206&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2121206&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2121206&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2121206&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2121206&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2121206&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2121206&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2121206&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2121206&clcid=0x40a)  
For the x86 driver: [Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2121021&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2121021&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2121021&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2121021&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2121021&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2121021&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2121021&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2121021&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2121021&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2121021&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2121021&clcid=0x40a)  

[Download the Microsoft Command Line Utilities 11 for SQL Server](https://www.microsoft.com/download/details.aspx?id=36433)  

### Features added in 11

| Feature added | Details |
| :------------ | :------ |
| Contains new features. | See [Features of the Microsoft ODBC Driver for SQL Server on Windows](features-of-the-microsoft-odbc-driver-for-sql-server-on-windows.md). |
| Contains all the features that shipped with ODBC in SQL Server 2012 Native Client. | &nbsp; |
| &nbsp; | &nbsp; |
