---
title: azdata arc sql endpoint reference
titleSuffix: SQL Server big data clusters
description: Reference article for azdata arc sql endpoint commands.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: seanw
ms.date: 04/06/2021
ms.topic: reference
ms.prod: sql
ms.technology: big-data-cluster
---

# azdata arc sql endpoint

Applies to [!INCLUDE [azure-data-cli-azdata](../../includes/azure-data-cli-azdata.md)]

The following article provides reference for the **sql** commands in the **azdata** tool. For more information about other **azdata** commands, see [azdata reference](reference-azdata.md)

## Commands

|Command|Description|
| --- | --- |
[azdata arc sql endpoint list](#azdata-arc-sql-endpoint-list) | List the SQL endpoints.
## azdata arc sql endpoint list
List the SQL endpoints.
```bash
azdata arc sql endpoint list [--name -n] 
                             
```
### Examples
List the endpoints for a SQL managed instance.
```bash
azdata arc sql endpoint list -n sqlmi1
```
### Optional Parameters
#### `--name -n`
The name of the SQL instance to be shown. If omitted, all endpoints for all instances will be shown.
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

