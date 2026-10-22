---
title: "LOCALDB_ERROR_VERSION_REQUESTED_NOT_INSTALLED"
description: "LOCALDB_ERROR_VERSION_REQUESTED_NOT_INSTALLED"
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: vanto
ms.date: 07/14/2025
ms.service: sql
ms.subservice: performance
ms.topic: "reference"
---
# LOCALDB_ERROR_VERSION_REQUESTED_NOT_INSTALLED

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

## Details

| Attribute | Value |
| --- | --- |
| Product Name | SQL Server |
| Event ID | 273 |
| Event Source | SQL Server Local Database Runtime 12.0 |
| Component | Local Database Runtime API |
| Message Text | The Local Database version available on this workstation is lower than the requested Local Database version. |

## Explanation

The specified patch level isn't installed.

## User action

Install the version of Local Database Runtime with the requested patch level (or above).
