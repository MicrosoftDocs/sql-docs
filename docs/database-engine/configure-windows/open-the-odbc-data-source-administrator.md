---
title: "Open and Use ODBC Data Source Administrator"
description: Find out how to open the ODBC Data Source Administrator on various operating systems. You can use this Windows component to create and manage ODBC data sources, and see which ODBC drivers are available.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: vanto
ms.date: 02/26/2026
ms.service: sql
ms.subservice: configuration
ms.topic: how-to
helpviewer_keywords:
  - "ODBC Data Source Administrator"
  - "opening ODBC Data Source Administrator"
---
# Open and use ODBC Data Source Administrator

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article describes how to open the ODBC Data Source Administrator. The ODBC Data Source Administrator is a Windows component. Use the ODBC Data Source Administrator to create and manage ODBC data sources.

## Open the ODBC Data Source Administrator in Windows 10

1. On the **Start** page, type **ODBC Data Sources**. The *ODBC Data Sources Desktop App* should appear as a choice.

## Open the ODBC Data Source Administrator in Windows Server

1. On the **Start** menu, point to **Administrative Tools**, and then select **ODBC Data Sources**.

## Check the ODBC SQL Server driver version (Windows)

Your computer might contain a variety of ODBC drivers, from [!INCLUDE [msCoName](../../includes/msconame-md.md)] and from other companies. The following describes how to use the Windows **ODBC Data Source Administrator** to check the version of the installed ODBC drivers.

### Check the ODBC SQL Server driver version

- In the **ODBC Data Source Administrator**, select the **Drivers** tab.

  Information for the Microsoft [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] entry is displayed in the **Version** column.

### Understand ODBC driver version numbers

The **Version** column in the ODBC Data Source Administrator displays the internal file version number, which differs from the marketing version name used in product downloads and documentation.

The following table maps common marketing version names to their corresponding internal file version patterns:

| Marketing version | Internal file version pattern |
|---|---|
| ODBC Driver 18 for SQL Server | 2022.*x*.*x* |
| ODBC Driver 17 for SQL Server | 2017.*x*.*x* |
| ODBC Driver 13.1 for SQL Server | 2015.*x*.*x* |
| ODBC Driver 13 for SQL Server | 2014.*x*.*x* |

For example, if the **Version** column shows `2017.177.02.01`, the installed driver corresponds to **ODBC Driver 17 for SQL Server**.

For detailed release history, see [Release notes for the Microsoft ODBC Driver for SQL Server on Windows](../../connect/odbc/windows/release-notes-odbc-sql-server-windows.md).

## Related content

- [ODBC Data Source Administrator](../../odbc/admin/odbc-data-source-administrator.md)
