---
title: "LOCALDB_ERROR_CALLER_IS_NOT_OWNER"
description: "LOCALDB_ERROR_CALLER_IS_NOT_OWNER"
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: vanto
ms.date: 07/14/2025
ms.service: sql
ms.subservice: performance
ms.topic: "reference"
---
# LOCALDB_ERROR_CALLER_IS_NOT_OWNER

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

## Details

| Category | Value |
| --- | --- |
| Product Name | SQL Server |
| Event ID | 282 |
| Event Source | SQL Server Local Database Runtime 12.0 |
| Component | Local Database Runtime API |
| Message Text | API caller is not Local Database instance owner. |

## Explanation

User needs to be the instance owner to be able to execute the requested operation.

## User action

Contact the instance owner for help.
