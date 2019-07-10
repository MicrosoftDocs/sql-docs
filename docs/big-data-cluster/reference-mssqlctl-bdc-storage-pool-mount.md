---
title: mssqlctl bdc storage-pool mount reference
titleSuffix: SQL Server big data clusters
description: Reference article for mssqlctl bdc storage-pool mount commands.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: mihaelab
manager: jroth
ms.date: 06/26/2019
ms.topic: reference
ms.prod: sql
ms.technology: big-data-cluster
---

# mssqlctl bdc storage-pool mount

[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

The following article provides reference for the **bdc storage-pool mount** commands in the **mssqlctl** tool. For more information about other **mssqlctl** commands, see [mssqlctl reference](reference-mssqlctl.md).

## Commands
|     |     |
| --- | --- |
[mssqlctl bdc storage-pool mount create](#mssqlctl-bdc-storage-pool-mount-create) | Create mounts of remote stores in HDFS.
[mssqlctl bdc storage-pool mount delete](#mssqlctl-bdc-storage-pool-mount-delete) | Delete mounts of remote stores in HDFS.
[mssqlctl bdc storage-pool mount status](#mssqlctl-bdc-storage-pool-mount-status) | Status of mount(s).
## mssqlctl bdc storage-pool mount create
Create mounts of remote stores in HDFS. The credentials for accessing the remote store, if any, should be specified using the environment variable MOUNT_CREDENTIALS as a comma-separated list of key=value pairs. Any commas in the keys or values must be escaped.
```bash
mssqlctl bdc storage-pool mount create --remote-uri 
                                       --mount-path
```
### Examples
To mount container "data" in ADLS Gen 2 account "adlsv2example" on HDFS path /mounts/adlsv2/data using the shared key
```bash
Set the MOUNT_CREDENTIALS environment variable as "fs.azure.abfs.account.name=<your-storage-account-name>.dfs.core.windows.net,fs.azure.account.key.<your-storage-account-name>.dfs.core.windows.net=<storage-account-access-key>".
mssqlctl bdc storage-pool mount create --remote-uri abfs://data@adlsv2example.dfs.core.windows.net/
    --mount-path /mounts/adlsv2/data
```
To mount a remote HDFS BDC (hdfs://namenode1:8080/) on local HDFS path /mounts/hdfs/
```bash
mssqlctl bdc storage-pool mount create --remote-uri hdfs://namenode1:8080/ --mount-path /mounts/hdfs/
```
### Required Parameters
#### `--remote-uri`
URI of the remote store that is to be mounted (source of mount).
#### `--mount-path`
HDFS path where mount has to be created (destination of mount).
### Global Arguments
#### `--debug`
Increase logging verbosity to show all debug logs.
#### `--help -h`
Show this help message and exit.
#### `--output -o`
Output format.  Allowed values: json, jsonc, table, tsv.  Default: json.
#### `--query -q`
JMESPath query string. See [http://jmespath.org/](http://jmespath.org/]) for more information and examples.
#### `--verbose`
Increase logging verbosity. Use --debug for full debug logs.
## mssqlctl bdc storage-pool mount delete
Delete mounts of remote stores in HDFS.
```bash
mssqlctl bdc storage-pool mount delete --mount-path 
                                       
```
### Examples
Delete mount created at /mounts/adlsv2/data for a ADLS Gen 2 storage account.
```bash
mssqlctl bdc storage-pool mount delete --mount-path /mounts/adlsv2/data
```
### Required Parameters
#### `--mount-path`
the HDFS path corresponding to the mount that has to be deleted.
### Global Arguments
#### `--debug`
Increase logging verbosity to show all debug logs.
#### `--help -h`
Show this help message and exit.
#### `--output -o`
Output format.  Allowed values: json, jsonc, table, tsv.  Default: json.
#### `--query -q`
JMESPath query string. See [http://jmespath.org/](http://jmespath.org/]) for more information and examples.
#### `--verbose`
Increase logging verbosity. Use --debug for full debug logs.
## mssqlctl bdc storage-pool mount status
Status of mount(s).
```bash
mssqlctl bdc storage-pool mount status [--mount-path] 
                                       
```
### Examples
Get mount status by path
```bash
mssqlctl bdc storage-pool mount status --mount-path /mounts/hdfs
```
Get status of all mounts.
```bash
mssqlctl bdc storage-pool mount status
```
### Optional Parameters
#### `--mount-path`
Mount path.
### Global Arguments
#### `--debug`
Increase logging verbosity to show all debug logs.
#### `--help -h`
Show this help message and exit.
#### `--output -o`
Output format.  Allowed values: json, jsonc, table, tsv.  Default: json.
#### `--query -q`
JMESPath query string. See [http://jmespath.org/](http://jmespath.org/]) for more information and examples.
#### `--verbose`
Increase logging verbosity. Use --debug for full debug logs.

## Next steps

For more information about other **mssqlctl** commands, see [mssqlctl reference](reference-mssqlctl.md). For more information about how to install the **mssqlctl** tool, see [Install mssqlctl to manage SQL Server 2019 big data clusters](deploy-install-mssqlctl.md).