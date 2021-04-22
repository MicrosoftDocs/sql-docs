---
title: azdata arc dc reference
titleSuffix: SQL Server big data clusters
description: Reference article for azdata arc dc commands.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: seanw
ms.date: 04/06/2021
ms.topic: reference
ms.prod: sql
ms.technology: big-data-cluster
---

# azdata arc dc

Applies to [!INCLUDE [azure-data-cli-azdata](../../includes/azure-data-cli-azdata.md)]

The following article provides reference for the **sql** commands in the **azdata** tool. For more information about other **azdata** commands, see [azdata reference](reference-azdata.md)

## Commands

|Command|Description|
| --- | --- |
[azdata arc dc create](#azdata-arc-dc-create) | Create data controller.
[azdata arc dc delete](#azdata-arc-dc-delete) | Delete data controller.
[azdata arc dc endpoint](reference-azdata-arc-dc-endpoint.md) | Endpoint commands.
[azdata arc dc status](reference-azdata-arc-dc-status.md) | Status commands.
[azdata arc dc config](reference-azdata-arc-dc-config.md) | Configuration commands.
[azdata arc dc debug](reference-azdata-arc-dc-debug.md) | Debug commands.
[azdata arc dc export](#azdata-arc-dc-export) | Export metrics, logs or usage.
[azdata arc dc upload](#azdata-arc-dc-upload) | Upload exported data file.
## azdata arc dc create
Create data controller - kube config is required on your system along with the following environment variables ['AZDATA_USERNAME', 'AZDATA_PASSWORD'].
```bash
azdata arc dc create 
```
### Examples
Data controller deployment.
```bash
azdata arc dc create
```
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
## azdata arc dc delete
Delete data controller - kube config is required on your system.
```bash
azdata arc dc delete 
```
### Examples
Data controller deployment.
```bash
azdata arc dc delete
```
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
## azdata arc dc export
Export metrics, logs or usage to a file.
```bash
azdata arc dc export 
```
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
## azdata arc dc upload
Upload data file exported from a data controller to Azure.
```bash
azdata arc dc upload 
```
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

