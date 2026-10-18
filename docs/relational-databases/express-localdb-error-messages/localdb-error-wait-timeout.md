---
title: "LOCALDB_ERROR_WAIT_TIMEOUT"
description: "LOCALDB_ERROR_WAIT_TIMEOUT"
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: vanto
ms.date: 07/14/2025
ms.service: sql
ms.subservice: performance
ms.topic: "reference"
---
# LOCALDB_ERROR_WAIT_TIMEOUT

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

## Details

| Attribute | Value |
| --- | --- |
| Product Name | SQL Server |
| Event ID | 277 |
| Event Source | SQL Server Local Database Runtime 12.0 |
| Component | Local Database Runtime API |
| Message Text | Timeout occurred inside the Local Database instance API method. |

## Explanation

A time-out occurred while trying to acquire synchronization locks.

## User action

Retry the requested operation again.
