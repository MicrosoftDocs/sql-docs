---
title: mssqlctl storage mount reference
titleSuffix: SQL Server big data clusters
description: Reference article for mssqlctl storage commands.
author: rothja
ms.author: jroth
manager: craigg
ms.date: 02/28/2019
ms.topic: reference
ms.prod: sql
ms.technology: big-data-cluster
---

# mssqlctl storage mount

[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

The following article provides reference for the **storage mount** commands in the **mssqlctl** tool. For more information about other **mssqlctl** commands, see [mssqlctl reference](reference-mssqlctl.md).

## <a id="commands"></a> Commands

|||
|---|---|
| [create](#create) | Create mounts of remote stores in HDFS. |
| [delete](#delete) | Delete mounts of remote stores in HDFS. |
| [status](#status) | Status of mount(s). |

## <a id="create"></a> mssqlctl storage mount create

Create mounts of remote stores in HDFS.

```
mssqlctl storage mount create
   --local-path
   --remote-uri
   [--credential-file]
```

### Parameters

| Parameters | Description |
|---|---|
| **--local-path** | HDFS path where mount has to be create(destination of mount). Required. |
| **--remote-uri** | URI of the remote store that is to be mounted (source of mount). Required. |
| **--credential-file** | File that contains the credentials to access the remote store. The credentials have to be specified as key=value pairs with one key=value per line. Any equals in the keys or values have to be escaped. No credentials are required by default. The required keys depend on the type of remote store being mounted and the type of authorization used. |

### Examples

To mount container "data" in ADLS Gen 2 account "adlsv2example" on HDFS path `/mounts/adlsv2/data` using the shared key:

```
mssqlctl storage mount create --remote-uri abfs://data@adlsv2example.dfs.core.windows.net/ --local-path /mounts/adlsv2/data --credentials credential_file
```

To mount a remote HDFS cluster (`hdfs://namenode1:8080/`) on local HDFS path `/mounts/hdfs/`:

```
mssqlctl storage mount create --remote-uri hdfs://namenode1:8080/ --local-path /mounts/hdfs/
```

## <a id="delete"></a> mssqlctl storage mount delete

Delete mounts of remote stores in HDFS.

```
mssqlctl storage mount delete
   --local-path
```

### Parameters

| Parameters | Description |
|---|---|
| **--local-path** | The HDFS path corresponding to the mount that has to be deleted. Required. |

### Examples

Delete mount created at /mounts/adlsv2/data for a ADLS Gen 2 storage account.

```
mssqlctl storage mount delete --local-path /mounts/adlsv2/data
```

## <a id="status"></a> mssqlctl storage mount status

Status of mount(s).

```
mssqlctl storage mount status
   --local-path
```

### Parameters

| Parameters | Description |
|---|---|
| **--mount-path** | Mount path. Required. |

### Examples

Get mount status by path

```
mssqlctl storage mount status --mount-path /mounts/hdfs
```

Get status of all mounts.

```
mssqlctl storage mount status
```

## Next steps

For more information about other **mssqlctl** commands, see [mssqlctl reference](reference-mssqlctl.md). For more information about how to install the **mssqlctl** tool, see [Install mssqlctl to manage SQL Server 2019 big data clusters](deploy-install-mssqlctl.md).