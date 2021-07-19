---
title: "Release notes for mssql-tools on Linux and macOS"
description: "Learn what's new and changed in released versions of the Microsoft SQL Server Tools."
ms.custom: ""
ms.date: "01/29/2021"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: v-daenge
ms.technology: connectivity
ms.topic: conceptual
author: David-Engel
ms.author: v-daenge
manager: kenvh
---
# Release notes for the Microsoft SQL Server tools on Linux and macOS

[!INCLUDE[Driver_ODBC_Download](../../../includes/driver_odbc_download.md)]

This article lists and describes what's new in the versioned releases of the [!INCLUDE[msCoName](../../../includes/msconame_md.md)] SQL Server Tools on Linux and macOS.

## 17.7.1.1, January 2021

| Feature added | Details |
| :------------ | :------ |
| Sqlcmd Bugfix | Fixed input redirection bug and empty lines leading to repeated execution. |
| Sqlcmd Bugfix | Fixed mistaken error reporting for r, p, X and k options under certain formatting. |
| Sqlcmd -z/-Z "Password" Option | Now supported. |
| &nbsp; | &nbsp; |

## 17.6.1.1, July 2020

| Feature added | Details |
| :------------ | :------ |
| Sqlcmd Command Line Parser Updated | Fixed bugs where unexpected behavior occurred when using certain options in different orders. |
| Sqlcmd Error Messages Updated | Fixed various inconsistencies in how errors in sqlcmd were returned. |
| Sqlcmd -Y Option Fixed | Fixed issue where -Y option was ineffective |
| Sqlcmd Column Name Truncation Fixed | Fixed issue where column names would be truncated incorrectly |
| Sqlcmd Linux Exit Codes | Fixed issue where process exit code was missing on Linux |
| &nbsp; | &nbsp; |

## Next steps

Learn more about connecting with [BCP](connecting-with-bcp.md) and [SQLCMD](connecting-with-sqlcmd.md)!
