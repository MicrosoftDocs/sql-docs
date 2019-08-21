---
title: azdata bdc status reference
titleSuffix: SQL Server big data clusters
description: Reference article for azdata bdc status commands.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: mihaelab
ms.date: 08/21/2019
ms.topic: reference
ms.prod: sql
ms.technology: big-data-cluster
---

# azdata bdc status

[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

The following article provides reference for the **bdc status** commands in the **azdata** tool. For more information about other **azdata** commands, see [azdata reference](reference-azdata.md).

## Commands
|     |     |
| --- | --- |
[azdata bdc status show](#azdata-bdc-status-show) | Shows the status of the Big Data Cluster.
## azdata bdc status show
Shows the status of the Big Data Cluster.
```bash
azdata bdc status show 
```
### Examples
BDC status where the user is logged in.
```bash
azdata bdc status show
```
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

For more information about other **azdata** commands, see [azdata reference](reference-azdata.md). For more information about how to install the **azdata** tool, see [Install azdata to manage [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)]](deploy-install-azdata.md).
