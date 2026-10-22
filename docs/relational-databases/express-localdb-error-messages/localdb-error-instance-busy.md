---
title: "LOCALDB_ERROR_INSTANCE_BUSY"
description: "LOCALDB_ERROR_INSTANCE_BUSY"
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: vanto
ms.date: 07/14/2025
ms.service: sql
ms.subservice: performance
ms.topic: "reference"
---
# LOCALDB_ERROR_INSTANCE_BUSY

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

## Details

| Attribute | Value |
| --- | --- |
| Product Name | SQL Server |
| Event ID | 274 |
| Event Source | SQL Server Local Database Runtime 12.0 |
| Component | Local Database Runtime API |
| Message Text | Requested operation on Local Database instance cannot be performed because specified instance is currently in use. Stop the instance and try again. |

## Explanation

The specified instance is running.

## User action

Stop the specified Local Database Runtime instance and try again.
