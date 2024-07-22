---
title: Release notes for mssql-tools on Linux and macOS
description: "Learn what's new and changed in released versions of the Microsoft SQL Server Tools."
author: David-Engel
ms.author: davidengel
ms.reviewer: v-davidengel
ms.date: 07/31/2024
ms.service: sql
ms.subservice: connectivity
ms.custom: linux-related-content
ms.topic: conceptual
---
# Release notes for the Microsoft SQL Server tools on Linux and macOS

[!INCLUDE[Driver_ODBC_Download](../../../includes/driver_odbc_download.md)]

This article lists and describes what's new in the versioned releases of the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] SQL Server Tools on Linux and macOS.

## 18.4.1.1, July 2024

| New item | Details |
| :------- | :------ |
| Sqlcmd AAD rename | Renamed Azure Active Directory to Entra ID. |
| BCP AAD rename | Renamed Azure Active Directory to Entra ID. |

## 18.3.1.1, July 2023

| New item | Details |
| :------- | :------ |
| Alpine ARM64 | Only released for Alpine Linux. This version now supports the ARM64 platform on Alpine Linux. |

## 18.2.1.1, January 2023

| New item | Details |
| :------- | :------ |
| Sqlcmd Bugfix | Fixed issue with command line parameters not being hidden |

## 18.1.1.1, August 2022

| New item | Details |
| :------- | :------ |
| Sqlcmd Bugfix | Fixed issue where -M option required an argument. |
| Sqlcmd Bugfix | Fixed issue where nohup and SIGHUP can cause it to hang in certain situations. |
| Sqlcmd Bugfix | Fixed specifying input codepage with -f option. |
| Sqlcmd Bugfix | Fixed detection of current character encoding. |
| Sqlcmd Bugfix | Fixed input file codepage option. |

## 17.10.1.1, June 2022

| New item | Details |
| :------- | :------ |
| Sqlcmd Bugfix | Fixed issue where -M option required an argument. |
| Sqlcmd Bugfix | Fixed issue where nohup and SIGHUP can cause it to hang in certain situations. |

## 18.0.1.1, February 2022

| New item | Details |
| :------- | :------ |
| Sqlcmd Bugfix | Fixed extraneous trailing bytes after encoding conversion. |
| TDS 8.0 | Add support for TDS 8.0 strict encryption |
| Secure by default | Following the change in Microsoft ODBC Driver 18 for SQL Server to default Encrypt to `yes`, sqlcmd and bcp both require encryption and validate certificates, by default. In sqlcmd, use `-No` to connect with optional encryption. In bcp, use `-Yo` to connect with optional encryption. For more information, see [Connecting with sqlcmd](connecting-with-sqlcmd.md) and [Connecting with bcp](connecting-with-bcp.md). |

## 17.9.1.1, February 2022

| New item | Details |
| :------- | :------ |
| Sqlcmd Bugfix | Fixed extraneous trailing bytes after encoding conversion. |

## 17.8.1.2, October 2021

| New item | Details |
| :------- | :------ |
| Package update | Updated RPM packages for Red Hat 7, Red Hat 8, SUSE 12, and SUSE 15 to use SHA256 RPM signing. |

## 17.8.1.1, July 2021

| Feature added | Details |
| :------------ | :------ |
| Sqlcmd Token Authentication | Now supported. See [Connecting with sqlcmd](connecting-with-sqlcmd.md) |
| BCP Token Authentication | Now supported. See [Connecting with bcp](connecting-with-bcp.md) |

## 17.7.1.1, January 2021

| Feature added | Details |
| :------------ | :------ |
| Sqlcmd Bugfix | Fixed input redirection bug and empty lines leading to repeated execution. |
| Sqlcmd Bugfix | Fixed mistaken error reporting for r, p, X and k options under certain formatting. |
| Sqlcmd -z/-Z "Password" Option | Now supported. |

## 17.6.1.1, July 2020

| Feature added | Details |
| :------------ | :------ |
| Sqlcmd Command Line Parser Updated | Fixed bugs where unexpected behavior occurred when using certain options in different orders. |
| Sqlcmd Error Messages Updated | Fixed various inconsistencies in how errors in sqlcmd were returned. |
| Sqlcmd -Y Option Fixed | Fixed issue where -Y option was ineffective |
| Sqlcmd Column Name Truncation Fixed | Fixed issue where column names would be truncated incorrectly |
| Sqlcmd Linux Exit Codes | Fixed issue where process exit code was missing on Linux |

## Next steps

Learn more about connecting with [BCP](connecting-with-bcp.md) and [SQLCMD](connecting-with-sqlcmd.md)!
