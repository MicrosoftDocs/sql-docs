---
title: azdata arc dc debug reference
titleSuffix: SQL Server big data clusters
description: Reference article for azdata arc dc debug commands.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: seanw
ms.date: 04/29/2021
ms.topic: reference
ms.prod: sql
ms.technology: big-data-cluster
---

# azdata arc dc debug

Applies to [!INCLUDE [azure-data-cli-azdata](../../includes/azure-data-cli-azdata.md)]

The following article provides reference for the **sql** commands in the **azdata** tool. For more information about other **azdata** commands, see [azdata reference](reference-azdata.md)

## Commands

|Command|Description|
| --- | --- |
[azdata arc dc debug copy-logs](#azdata-arc-dc-debug-copy-logs) | Copy logs.
[azdata arc dc debug dump](#azdata-arc-dc-debug-dump) | Trigger memory dump.
## azdata arc dc debug copy-logs
Copy the debug logs from the data controller - Kubernetes configuration is required on your system.
```bash
azdata arc dc debug copy-logs 
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
## azdata arc dc debug dump
Trigger memory dump and copy it out from container - Kubernetes configuration is required on your system.
```bash
azdata arc dc debug dump 
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