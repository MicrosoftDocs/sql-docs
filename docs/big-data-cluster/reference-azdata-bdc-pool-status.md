---
title: azdata bdc pool status reference
titleSuffix: SQL Server big data clusters
description: Reference article for azdata bdc pool status commands.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: mihaelab
ms.date: 07/24/2019
ms.topic: reference
ms.prod: sql
ms.technology: big-data-cluster
---

# azdata bdc pool status

[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

The following article provides reference for the **bdc pool status** commands in the **azdata** tool. For more information about other **azdata** commands, see [azdata reference](reference-azdata.md).

## Commands
|     |     |
| --- | --- |
[azdata bdc pool status show](#azdata-bdc-pool-status-show) | Pool status.
## azdata bdc pool status show
Pool status.
```bash
azdata bdc pool status show --kind -k 
                            [--name -n]
```
### Examples
Get status of the storage pool.
```bash
azdata bdc pool status show --kind storage --name default
```
Get status of the data pool.
```bash
azdata bdc pool status show --kind data --name default
```
Get status of the compute pool.
```bash
azdata bdc pool status show --kind compute --name default
```
Get status of the master pool.
```bash
azdata bdc pool status show --kind master --name default
```
Get status of the spark pool.
```bash
azdata bdc pool status show --kind spark --name default
```
### Required Parameters
#### `--kind -k`
Big data cluster pool kind.
### Optional Parameters
#### `--name -n`
Big data cluster pool name.
`default`
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

For more information about how to install the **azdata** tool, see [Install azdata to manage [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)]](deploy-install-azdata.md).
