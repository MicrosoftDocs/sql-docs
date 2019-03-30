---
title: "Release Notes for ODBC to SQL Server on Windows) | Microsoft Docs"
ms.custom: ""
ms.date: "02/27/2019"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: b8459ed8-625e-4d8b-891c-e7e78c9977cc
ms.reviewer: "v-jizho2, v-chojas, genemi"
author: v-makouz
ms.author: v-makouz
manager: kenvh
---
# Release Notes for ODBC to SQL Server on Windows

[!INCLUDE[Driver_ODBC_Download](../../../includes/driver_odbc_download.md)]

This release notes article describes what's new for the Microsoft ODBC driver to SQL Server on Windows.

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

## 17.3, February 2019

| Feature added | Details |
| :------------ | :------ |
| Azure Active Directory Managed Service Identity (system and user-assigned) authentication mode. | See [Using Azure Active Directory with the ODBC Driver](../using-azure-active-directory.md). |
| Ability to stream input parameters against Always Encrypted columns. | See [Limitations of the ODBC driver when using Always Encrypted](../using-always-encrypted-with-the-odbc-driver.md#limitations-of-the-odbc-driver-when-using-always-encrypted). |
| XA distributed transactions. | [Using XA Transactions](../use-xa-with-dtc.md). |
| Bug fixes. | See [Bug fixes](../bug-fixes.md). |
| &nbsp; | &nbsp; |

## 17.2, July 2018

| Feature added | Details |
| :------------ | :------ |
| Data Classification for Azure SQL Database and SQL Server. | See [Data Classification](../data-classification.md). |
| Support for UTF-8 server encoding. | &nbsp; |
| Bug fixes. | See [Bug fixes](../bug-fixes.md). |
| &nbsp; | &nbsp; |

## 17.1, March 2018

| Feature added | Details |
| :------------ | :------ |
| Support for `SQL_COPT_SS_CEKCACHETTL` and `SQL_COPT_SS_TRUSTEDCMKPATHS` connection attributes. | &bull; &nbsp; `SQL_COPT_SS_CEKCACHETTL`<br/>Allows controlling the time that the local cache of Column Encryption Keys exists, as well as flushing it.<br/><br/>&bull; &nbsp; `SQL_COPT_SS_TRUSTEDCMKPATHS`<br/>Allows the application to restrict AE operations to only use the specified list of Column Master Keys.<br/><br/> For more information, see [Using Always Encrypted with the ODBC Driver for SQL Server](../using-always-encrypted-with-the-odbc-driver.md). |
| Azure Active Directory Interactive Authentication Support | &nbsp; |
| Bug fixes. | See [Bug fixes](../bug-fixes.md). |
| &nbsp; | &nbsp; |

## 17, February 2018

| Feature added | Details |
| :------------ | :------ |
| Always Encrypted support for BCP API. | &nbsp; |
| New connection string attribute `UseFMTOnly`. | Causes the driver to use legacy metadata in special cases that require temporary tables. |
| Support for Azure SQL Managed Instance. | Extended Private Preview.<br/><br/>See the following list of [Differences when using Managed Instance (ODBC version 17)](#diffs-managed-instance-17). |
| &nbsp; | &nbsp; |

### <a name="diffs-managed-instance-17"></a> Differences when using Managed Instance (ODBC version 17)

This version of ODBC contains support for Azure SQL Managed Instance (Extended Private Preview). See the following noted list of differences when using Managed Instance.

> [!NOTE]
> There are a number of differences when using Managed Instance:
>
> - FILESTREAM is not supported.
> - Local filesystem access is not supported, but is required for things like tracefiles.
> - Create UDT from local path is not supported.
> - Windows Integrated Authentication is not supported.
> - DTC is not supported.
> - `sa` account is not present (default account is called `cloudSA`).
> - TDS token ERROR (0xAA) returns incorrect server name.
> - Special characters in database name are not supported.
> - ALTER DATABASE [dbname1] MODIFY NAME = [dbname2] is not supported.
> - The error messages are always shown in English, regardless of language settings (same as Azure).

## 13.1

| Feature added | Details |
| :------------ | :------ |
| ODBC Driver 13.1 for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] adds support for [Always Encrypted](../../../connect/odbc/using-always-encrypted-with-the-odbc-driver.md) and [Azure Active Directory](../../../connect/odbc/using-azure-active-directory.md). | These added supports are available when connecting to Microsoft SQL Server 2016, or to a later version. |
| There are connection pooling keywords and attributes, that correspond to the supports for Always Encrypted and Azure Active Directory. | These keywords and attribute are described in [Driver Aware Connection Pooling in the ODBC Driver for SQL Server](../../../connect/odbc/windows/driver-aware-connection-pooling-in-the-odbc-driver-for-sql-server.md). |
| &nbsp; | &nbsp; |

## 13

| Feature added | Details |
| :------------ | :------ |
| Adds support for Microsoft SQL Server 2016. | Retains the functionality of ODBC driver version 11. |
| &nbsp; | &nbsp; |

## 11

| Feature added | Details |
| :------------ | :------ |
| Contains new features. | See [Features of the Microsoft ODBC Driver for SQL Server on Windows](features-of-the-microsoft-odbc-driver-for-sql-server-on-windows.md). |
| Contains all the features that shipped with ODBC in SQL Server 2012 Native Client. | &nbsp; |
| &nbsp; | &nbsp; |
