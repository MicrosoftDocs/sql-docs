---
title: azdata arc postgres endpoint reference
titleSuffix: SQL Server big data clusters
description: Reference article for azdata arc postgres endpoint commands.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: seanw
ms.date: 09/22/2020
ms.topic: reference
ms.prod: sql
ms.technology: big-data-cluster
---

# azdata arc postgres endpoint

Applies to `azdata`

The following article provides reference for the **sql** commands in the **azdata** tool. For more information about other **azdata** commands, see [azdata reference](reference-azdata.md)

## Commands

|Command|Description|
| --- | --- |
[azdata arc postgres endpoint list](#azdata-arc-postgres-endpoint-list) | List PostgreSQL server group endpoints.
## azdata arc postgres endpoint list
List PostgreSQL server group endpoints.
```bash
azdata arc postgres endpoint list --name -n 
                                  [--engine-version -ev]
```
### Examples
List PostgreSQL server group endpoints.
```bash
azdata arc postgres endpoint list -n postgres01
```
### Required Parameters
#### `--name -n`
Name of the PostgreSQL server group.
### Optional Parameters
#### `--engine-version -ev`
--engine-version can be used in conjunction with --name to identify a PostgreSQL Hyperscale server group when two server groups of different engine version have the same name. --engine-version is optional and when used to identify a server group, it must be 11 or 12.
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

