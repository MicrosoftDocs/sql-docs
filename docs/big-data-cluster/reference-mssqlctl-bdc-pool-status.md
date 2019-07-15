---
title: mssqlctl bdc pool status reference
titleSuffix: SQL Server big data clusters
description: Reference article for mssqlctl bdc pool status commands.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: mihaelab
ms.date: 06/26/2019
ms.topic: reference
ms.prod: sql
ms.technology: big-data-cluster
---

# mssqlctl bdc pool status

[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

The following article provides reference for the **bdc pool status** commands in the **mssqlctl** tool. For more information about other **mssqlctl** commands, see [mssqlctl reference](reference-mssqlctl.md).

## Commands
|     |     |
| --- | --- |
[mssqlctl bdc pool status show](#mssqlctl-bdc-pool-status-show) | Pool status.
## mssqlctl bdc pool status show
Pool status.
```bash
mssqlctl bdc pool status show --kind -k 
                              [--name -n]
```
### Examples
Get status of the storage pool.
```bash
mssqlctl bdc pool status show --kind storage --name default
```
Get status of the data pool.
```bash
mssqlctl bdc pool status show --kind data --name default
```
Get status of the compute pool.
```bash
mssqlctl bdc pool status show --kind compute --name default
```
Get status of the master pool.
```bash
mssqlctl bdc pool status show --kind master --name default
```
Get status of the spark pool.
```bash
mssqlctl bdc pool status show --kind spark --name default
```
### Required Parameters
#### `--kind -k`
BDC pool kind.
### Optional Parameters
#### `--name -n`
BDC pool name.
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

For more information about how to install the **mssqlctl** tool, see [Install mssqlctl to manage SQL Server 2019 big data clusters](deploy-install-mssqlctl.md).