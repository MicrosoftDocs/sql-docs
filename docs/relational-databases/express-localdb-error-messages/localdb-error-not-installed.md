---
title: "LOCALDB_ERROR_NOT_INSTALLED"
description: "LOCALDB_ERROR_NOT_INSTALLED"
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: vanto
ms.date: 07/14/2025
ms.service: sql
ms.subservice: performance
ms.topic: "reference"
---
# LOCALDB_ERROR_NOT_INSTALLED

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

## Details

| Attribute | Value |
| --- | --- |
| Product Name | SQL Server |
| Event ID | 278 |
| Event Source | SQL Server Local Database Runtime 12.0 |
| Component | Local Database Runtime API |
| Message Text | **Note:** The message text is empty, because this message means that the entire LocalDB API (including the `FormatMessage` function that maps `HRESULTS` into message text) isn't available. |

## Explanation

The Local Database Runtime isn't installed on the computer.

## User action

Install the Local Database Runtime and try the operation again.
