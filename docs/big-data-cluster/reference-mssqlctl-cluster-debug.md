---
title: mssqlctl cluster debug reference
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

# mssqlctl cluster debug

The following article provides reference for the **cluster debug** commands in the **mssqlctl** tool. For more information about other **mssqlctl** commands, see [mssqlctl reference](reference-mssqlctl.md).

## <a id="commands"></a> Commands

|||
|---|---|
| [copy-logs](#copy-logs) | Copy logs. |
| [dump](#dump) | Trigger logging dump. |

## <a id="copy-logs"></a> cluster debug copy-logs

Copy logs.

```
mssqlctl cluster debug copy-logs
   --namespace
   [--container]
   [--pod]
   [--target-folder]
   [--timeout]
```

### Parameters

| Parameters | Description |
|---|---|
| **--namespace -n** | Cluster name, used for kubernetes namespace. Required. |
| **--container -c** | Copy the logs for the containers with similar name, Optional, by default copies logs for all containers. Cannot be specified multiple times. If specified multiple times, last one will be used. |
| **--pod -p** | Copy the logs for the pods with similar name. Optional, by default copies logs for all pods. Cannot be specified multiple times. If specified multiple times, last one will be used. |
| **--target-folder -d** | Target folder path to copy logs to. Optional, by default creates the result in the local folder.  Cannot be specified multiple times. If specified multiple times, last one will be used. |
| **--timeout -t** | The number of seconds to wait for the command to complete. The default value is 0 which is unlimited. |

## <a id="dump"></a> cluster debug dump

Trigger logging dump.

```
mssqlctl cluster debug dump
   [--container]
   [--namespace]
   --target-folder
```

### Parameters

| Parameters | Description |
|---|---|
| **--container -c** | Copy the logs for the containers with similar name, Optional, by default copies logs for all containers. Cannot be specified multiple times. If specified multiple times, last one will be used.  Allowed values: mssql-controller. |
| **--namespace -n** | Cluster name, used for kubernetes namespace. Required. |
| **--target-folder -d** | Target folder path to copy logs to. Optional, by default creates the result in the local folder.  Cannot be specified multiple times. If specified multiple times, last one will be used.  Default: `./output/dump`. Required. |

## Next steps

For more information about other **mssqlctl** commands, see [mssqlctl reference](reference-mssqlctl.md). For more information about how to install the **mssqlctl** tool, see [Install mssqlctl to manage SQL Server 2019 big data clusters](deploy-install-mssqlctl.md).