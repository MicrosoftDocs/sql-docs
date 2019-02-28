---
title: mssqlctl cluster config reference
titleSuffix: SQL Server 2019 big data clusters
description: Reference article for mssqlctl cluster commands.
author: rothja
ms.author: jroth
manager: craigg
ms.date: 02/27/2019
ms.topic: reference
ms.prod: sql
ms.technology: big-data-cluster
---

# mssqlctl cluster config

The following article provides reference for the **cluster config** commands in the **mssqlctl** tool. For more information about other **mssqlctl** commands, see [mssqlctl reference](reference-mssqlctl.md).

## <a id="commands"></a> Commands

|||
|---|---|
| [get](#get) | Get cluster. |

## <a id="get"></a> mssqlctl cluster config get

Get cluster.

```
mssqlctl cluster config get
   --name
   --output-file
```

### Parameters

| Parameters | Description |
|---|---|
| **--name -n** | Cluster name, used for kubernetes namespace. Required. |
| **--output-file -f** | Output file to store the result in. Required. |

## Next steps

For more information about other **mssqlctl** commands, see [mssqlctl reference](reference-mssqlctl.md). For more information about how to install the **mssqlctl** tool, see [Install mssqlctl to manage SQL Server 2019 big data clusters](deploy-install-mssqlctl.md).