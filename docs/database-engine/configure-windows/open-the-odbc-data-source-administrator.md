---
title: "Open and use ODBC Data Source Administrator"
description: Find out how to open the ODBC Data Source Administrator on various operating systems. You can use this Windows component to create and manage ODBC data sources, and see which ODBC drivers are available.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: vanto
ms.date: 09/16/2024
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
helpviewer_keywords:
  - "ODBC Data Source Administrator"
  - "opening ODBC Data Source Administrator"
---
# Open and use ODBC Data Source Administrator

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article describes how to open the ODBC Data Source Administrator. The ODBC Data Source Administrator is a Windows component. Use the ODBC Data Source Administrator to create and manage ODBC data sources.

## To open the ODBC Data Source Administrator in Windows 10

1. On the **Start** page, type **ODBC Data Sources**. The *ODBC Data Sources Desktop App* should appear as a choice.

## To open the ODBC Data Source Administrator in Windows 7

1. On the **Start** menu, select **Control Panel**.

1. In **Control Panel**, select **Administrative Tools**.

1. In **Administrative Tools**, select **Data Sources (ODBC)**.

## To open the ODBC Data Source Administrator in Windows Server

1. On the **Start** menu, point to **Administrative Tools**, and then select **ODBC Data Sources**.

## Check the ODBC SQL Server Driver Version (Windows)

Your computer might contain a variety of ODBC drivers, from [!INCLUDE [msCoName](../../includes/msconame-md.md)] and from other companies. The following describes how to use the Windows **ODBC Data Source Administrator** to check the version of the installed ODBC drivers.

### To check the ODBC SQL Server driver version

- In the **ODBC Data Source Administrator**, select the **Drivers** tab.

     Information for the Microsoft [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] entry is displayed in the **Version** column.

## Related content

- [ODBC Data Source Administrator](../../odbc/admin/odbc-data-source-administrator.md)
