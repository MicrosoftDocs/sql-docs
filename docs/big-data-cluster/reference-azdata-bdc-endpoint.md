---
title: azdata bdc endpoint reference
titleSuffix: SQL Server big data clusters
description: Use this reference to understand SQL commands in the azdata tool, specifically the bdc endpoint commands.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: mihaelab
ms.date: 06/22/2020
ms.topic: reference
ms.prod: sql
ms.technology: big-data-cluster
---

# azdata bdc endpoint

[!INCLUDE[SQL Server 2019](../includes/applies-to-version/sqlserver2019.md)]

The following article provides reference for the `sql` commands in the `azdata` tool. For more information about other `azdata` commands, see [azdata reference](reference-azdata.md).

## Commands
| Command | Description |
| --- | --- |
[azdata bdc endpoint list](#azdata-bdc-endpoint-list) | Lists the endpoints for the Big Data Cluster.
## azdata bdc endpoint list
Lists the endpoints for the Big Data Cluster.
```bash
azdata bdc endpoint list [--endpoint-name -e] 
                         
```
### Optional Parameters
#### `--endpoint-name -e`
Big data cluster endpoint name.
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

For more information about other `azdata` commands, see [azdata reference](reference-azdata.md). For more information about how to install the `azdata` tool, see [Install azdata to manage SQL Server 2019 big data clusters](deploy-install-azdata.md).
