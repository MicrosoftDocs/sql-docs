---
title: azdata bdc control status reference
titleSuffix: SQL Server big data clusters
description: Reference article for azdata bdc control status commands.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: seanw
ms.date: 06/22/2020
ms.topic: reference
ms.prod: sql
ms.technology: big-data-cluster
---

# azdata bdc control status

Applies to `azdata`

The following article provides reference for the **sql** commands in the **azdata** tool. For more information about other **azdata** commands, see [azdata reference](reference-azdata.md)

## Commands

|Command|Description|
| --- | --- |
[azdata bdc control status show](#azdata-bdc-control-status-show) | Control service status.
## azdata bdc control status show
Control service status.
```bash
azdata bdc control status show [--resource -r] 
                               [--all -a]
```
### Examples
Get status of service.
```bash
azdata bdc control status show
```
Get status of control service with all instances.
```bash
azdata bdc control status show --all
```
Get status of the control resource within the control service.
```bash
azdata bdc control status show --resource control
```
### Optional Parameters
#### `--resource -r`
Get this resource in this service.
#### `--all -a`
Show all the instances of each resource within the service.
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

For more information about other **azdata** commands, see [azdata reference](reference-azdata.md). 

For more information about how to install the **azdata** tool, see [Install azdata](deploy-install-azdata.md).
