---
title: "Allow PolyBase export configuration option"
description: Set `allow polybase export` configuration option in SQL Server settings
author: MikeRayMSFT
ms.author: mikeray
ms.date: "07/10/2020"
ms.service: sql
ms.subservice: polybase
ms.topic: conceptual
---

# Set `allow polybase export` configuration option

[!INCLUDE [sqlserver2016](../../includes/applies-to-version/sqlserver2016.md)]

`allow polybase export` server configuration option  allows `INSERT` into a Hadoop external table. 

This feature does not support insert into other external data sources.

 The possible values are described in the following table: 

| Value | Meaning                                |
|-------|----------------------------------------|
| 0     | Disabled. This is the default setting. |
| 1     | Enabled                                |


The setting takes effect immediately.

## Example

The following example enables this setting.

```sql
sp_configure 'show advanced options', 1;
GO
RECONFIGURE;
GO
sp_configure 'allow polybase export', 0;
GO
RECONFIGURE;
GO
```

## Next steps

 [Exporting data](../../relational-databases/polybase/polybase-configure-hadoop.md#exporting-data)