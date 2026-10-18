---
title: "LOCALDB_ERROR_INSTANCE_ALREADY_SHARED"
description: "LOCALDB_ERROR_INSTANCE_ALREADY_SHARED"
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: vanto
ms.date: 07/14/2025
ms.service: sql
ms.subservice: performance
ms.topic: "reference"
---
# LOCALDB_ERROR_INSTANCE_ALREADY_SHARED

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

## Details

| Attribute | Value |
| --- | --- |
| Product Name | SQL Server |
| Event ID | 284 |
| Event Source | SQL Server Local Database Runtime 12.0 |
| Component | Local Database Runtime API |
| Message Text | The specified Local Database Instance is already shared. |

## Explanation

The specified Local Database Instance is already shared with a different shared name.

## User action

Unshare the shared instance before sharing it again with a different shared name.
