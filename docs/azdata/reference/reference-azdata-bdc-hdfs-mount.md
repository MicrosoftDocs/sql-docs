---
title: azdata bdc hdfs mount reference
titleSuffix: SQL Server Big Data Clusters
description: Reference article for azdata bdc hdfs mount commands.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: seanw
ms.date: 10/05/2021
ms.service: sql
ms.subservice: big-data-cluster
ms.topic: reference
---

# azdata bdc hdfs mount

Applies to [!INCLUDE [azure-data-cli-azdata](../../includes/azure-data-cli-azdata.md)]

The following article provides reference for the **sql** commands in the **azdata** tool. For more information about other **azdata** commands, see [azdata reference](reference-azdata.md)

## Commands

|Command|Description|
| --- | --- |
[azdata bdc hdfs mount create](#azdata-bdc-hdfs-mount-create) | Create mounts of remote stores in HDFS.
[azdata bdc hdfs mount delete](#azdata-bdc-hdfs-mount-delete) | Delete mounts of remote stores in HDFS.
[azdata bdc hdfs mount status](#azdata-bdc-hdfs-mount-status) | Status of mount(s).
[azdata bdc hdfs mount refresh](#azdata-bdc-hdfs-mount-refresh) | Refresh the contents of a mount in HDFS.
## azdata bdc hdfs mount create
Create mounts of remote stores in HDFS. The credentials for accessing the remote store, if any, should be specified using the environment variable MOUNT_CREDENTIALS as a comma-separated list of key=value pairs. Any commas in the keys or values must be escaped.
```bash
azdata bdc hdfs mount create --remote-uri -r 
                             --mount-path -m
```
### Examples
To mount container "data" in ADLS Gen 2 account "adlsv2example" on HDFS path /mounts/adlsv2/data using the shared key
```bash
Set the MOUNT_CREDENTIALS environment variable as "fs.azure.abfs.account.name=<your-storage-account-name>.dfs.core.windows.net,fs.azure.account.key.<your-storage-account-name>.dfs.core.windows.net=<storage-account-access-key>".
azdata bdc hdfs mount create --remote-uri abfs://data@adlsv2example.dfs.core.windows.net/
    --mount-path /mounts/adlsv2/data
```
To mount a remote HDFS BDC (hdfs://namenode1:8080/) on local HDFS path /mounts/hdfs/
```bash
azdata bdc hdfs mount create --remote-uri hdfs://namenode1:8080/ --mount-path /mounts/hdfs/
```
### Required Parameters
#### `--remote-uri -r`
URI of the remote store that is to be mounted (source of mount).
#### `--mount-path -m`
HDFS path where mount has to be created (destination of mount).
### Global Arguments
#### `--debug`
Increase logging verbosity to show all debug logs.
#### `--help -h`
Show this help message and exit.
#### `--output -o`
Output format.  Allowed values: json, jsonc, table, tsv.  Default: json.
#### `--query -q`
JMESPath query string. See [http://jmespath.org/](http://jmespath.org) for more information and examples.
#### `--verbose`
Increase logging verbosity. Use --debug for full debug logs.
## azdata bdc hdfs mount delete
Delete mounts of remote stores in HDFS.
```bash
azdata bdc hdfs mount delete --mount-path -m 
                             
```
### Examples
Delete mount created at /mounts/adlsv2/data for a ADLS Gen 2 storage account.
```bash
azdata bdc hdfs mount delete --mount-path /mounts/adlsv2/data
```
### Required Parameters
#### `--mount-path -m`
the HDFS path corresponding to the mount that has to be deleted.
### Global Arguments
#### `--debug`
Increase logging verbosity to show all debug logs.
#### `--help -h`
Show this help message and exit.
#### `--output -o`
Output format.  Allowed values: json, jsonc, table, tsv.  Default: json.
#### `--query -q`
JMESPath query string. See [http://jmespath.org/](http://jmespath.org) for more information and examples.
#### `--verbose`
Increase logging verbosity. Use --debug for full debug logs.
## azdata bdc hdfs mount status
Status of mount(s).
```bash
azdata bdc hdfs mount status [--mount-path -m] 
                             
```
### Examples
Get mount status by path
```bash
azdata bdc hdfs mount status --mount-path /mounts/hdfs
```
Get status of all mounts.
```bash
azdata bdc hdfs mount status
```
### Optional Parameters
#### `--mount-path -m`
Mount path.
### Global Arguments
#### `--debug`
Increase logging verbosity to show all debug logs.
#### `--help -h`
Show this help message and exit.
#### `--output -o`
Output format.  Allowed values: json, jsonc, table, tsv.  Default: json.
#### `--query -q`
JMESPath query string. See [http://jmespath.org/](http://jmespath.org) for more information and examples.
#### `--verbose`
Increase logging verbosity. Use --debug for full debug logs.
## azdata bdc hdfs mount refresh
Refresh the contents of a mount in HDFS.
```bash
azdata bdc hdfs mount refresh --mount-path -m 
                              
```
### Examples
Refresh mount created at /mounts/adlsv2/data.
```bash
azdata bdc hdfs mount refresh --mount-path /mounts/adlsv2/data
```
### Required Parameters
#### `--mount-path -m`
HDFS path corresponding to the mount that has to be refreshed.
### Global Arguments
#### `--debug`
Increase logging verbosity to show all debug logs.
#### `--help -h`
Show this help message and exit.
#### `--output -o`
Output format.  Allowed values: json, jsonc, table, tsv.  Default: json.
#### `--query -q`
JMESPath query string. See [http://jmespath.org/](http://jmespath.org) for more information and examples.
#### `--verbose`
Increase logging verbosity. Use --debug for full debug logs.

## Next steps

For more information about other **azdata** commands, see [azdata reference](reference-azdata.md). 

For more information about how to install the **azdata** tool, see [Install azdata](..\install\deploy-install-azdata.md).