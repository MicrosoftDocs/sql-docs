---
title: mssqlctl cluster reference
titleSuffix: SQL Server 2019 big data clusters
description: Reference article for mssqlctl cluster commands.
author: rothja
ms.author: jroth
manager: craigg
ms.date: 02/28/2019
ms.topic: reference
ms.prod: sql
ms.technology: big-data-cluster
---

# mssqlctl cluster

The following article provides reference for the **cluster** commands in the **mssqlctl** tool. For more information about other **mssqlctl** commands, see [mssqlctl reference](reference-mssqlctl.md).

## <a id="commands"></a> Commands

|||
|---|---|
| [create](#create) | Create cluster. |
| [delete](#delete) | Delete cluster. |
| [config](reference-mssqlctl-cluster-config.md) | Cluster configuration commands. |
| [debug](reference-mssqlctl-cluster-debug.md) | Debug commands. |

## <a id="create"></a> mssqlctl cluster create

Create cluster.

```
mssqlctl cluster create
   --name
   [--accept-eula]
```

### Parameters

| Parameters | Description |
|---|---|
| **--name -n** | Cluster name, used for kubernetes namespace. |
| **--accept-eula -e** | Do you accept the license terms? \[yes/no\].  Allowed values: no, yes. Required. |

## <a id="delete"></a> mssqlctl cluster delete

Delete cluster.

```
mssqlctl cluster delete
   --name
   [--force]
```

### Parameters

| Parameters | Description |
|---|---|
| **--name -n** | Cluster name, used for kubernetes namespace. Required. |
| **--force -f** | Force delete cluster. |

## Next steps

For more information about other **mssqlctl** commands, see [mssqlctl reference](reference-mssqlctl.md). For more information about how to install the **mssqlctl** tool, see [Install mssqlctl to manage SQL Server 2019 big data clusters](deploy-install-mssqlctl.md).