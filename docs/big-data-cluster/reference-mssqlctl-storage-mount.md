---
title: mssqlctl storage mount reference
titleSuffix: SQL Server big data clusters
description: Reference article for mssqlctl storage mount commands.
author: rothja
ms.author: jroth
manager: craigg
ms.date: 04/23/2019
ms.topic: reference
ms.prod: sql
ms.technology: big-data-cluster
---

# mssqlctl storage mount

[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

The following article provides reference for the **storage mount** commands in the **mssqlctl** tool. For more information about other **mssqlctl** commands, see [mssqlctl reference](reference-mssqlctl.md).

## Commands
|     |     |
| --- | --- |
[mssqlctl storage mount create](#mssqlctl-storage-mount-create) | Create mounts of remote stores in HDFS.
[mssqlctl storage mount delete](#mssqlctl-storage-mount-delete) | Delete mounts of remote stores in HDFS.
[mssqlctl storage mount status](#mssqlctl-storage-mount-status) | Status of mount(s).
## mssqlctl storage mount create
Create mounts of remote stores in HDFS.
```bash
mssqlctl storage mount create --remote-uri 
                              --mount-path  
                              [--credential-file]
```
### Examples
To mount container "data" in ADLS Gen 2 account "adlsv2example" on HDFS path /mounts/adlsv2/data using the shared key
```bash
mssqlctl storage mount create --remote-uri abfs://data@adlsv2example.dfs.core.windows.net/
    --mount-path /mounts/adlsv2/data --credentials credential_file
```
To mount a remote HDFS cluster (hdfs://namenode1:8080/) on local HDFS path /mounts/hdfs/
```bash
mssqlctl storage mount create --remote-uri hdfs://namenode1:8080/ --mount-path /mounts/hdfs/
```
### Required Parameters
#### `--remote-uri`
URI of the remote store that is to be mounted (source of mount).
#### `--mount-path`
HDFS path where mount has to be created (destination of mount).
### Optional Parameters
#### `--credential-file`
File that contains the credentials to access the remote store. The credentials have to be specified as key=value pairs with one key=value per line. Any equals in the keys or values have to be escaped. No credentials are required by default. The required keys depend on the type of remote store being mounted and the type of authorization used.
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
## mssqlctl storage mount delete
Delete mounts of remote stores in HDFS.
```bash
mssqlctl storage mount delete --mount-path 
                              
```
### Examples
Delete mount created at /mounts/adlsv2/data for a ADLS Gen 2 storage account.
```bash
mssqlctl storage mount delete --mount-path /mounts/adlsv2/data
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
## mssqlctl storage mount status
Status of mount(s).
```bash
mssqlctl storage mount status [--mount-path] 
                              
```
### Examples
Get mount status by path
```bash
mssqlctl storage mount status --mount-path /mounts/hdfs
```
Get status of all mounts.
```bash
mssqlctl storage mount status
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