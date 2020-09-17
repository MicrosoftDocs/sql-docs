---
title: azdata arc dc status reference
titleSuffix: SQL Server big data clusters
description: Reference article for azdata arc dc status commands.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: seanw
ms.date: 06/22/2020
ms.topic: reference
ms.prod: sql
ms.technology: big-data-cluster
---

# azdata arc dc status

Applies to `azdata`

The following article provides reference for the **sql** commands in the **azdata** tool. For more information about other **azdata** commands, see [azdata reference](reference-azdata.md)

## Commands

|Command|Description|
| --- | --- |
[azdata arc dc status show](#azdata-arc-dc-status-show) | Shows the status of the data controller.
## azdata arc dc status show
Shows the status of the data controller.
```bash
azdata arc dc status show [--namespace -ns] 
                          
```
### Examples
Shows the status of the data controller in a particular namespace.
```bash
azdata arc dc status show --namespace <ns>
```
### Optional Parameters
#### `--namespace -ns`
The Kubernetes namespace in which the data controller exists.
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
