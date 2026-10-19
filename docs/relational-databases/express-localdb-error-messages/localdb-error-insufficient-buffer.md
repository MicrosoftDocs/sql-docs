---
title: "LOCALDB_ERROR_INSUFFICIENT_BUFFER"
description: "LOCALDB_ERROR_INSUFFICIENT_BUFFER"
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: vanto
ms.date: 07/14/2025
ms.service: sql
ms.subservice: performance
ms.topic: "reference"
---
# LOCALDB_ERROR_INSUFFICIENT_BUFFER

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

## Details

| Attribute | Value |
| --- | --- |
| Product Name | SQL Server |
| Event ID | 276 |
| Event Source | SQL Server Local Database Runtime 12.0 |
| Component | Local Database Runtime API |
| Message Text | The buffer passed to the Local Database instance API method has insufficient size. |

## Explanation

The input buffer is too short, and truncation was not requested.

## User action

Provide a buffer of the specified size.
