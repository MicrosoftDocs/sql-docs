---
title: azdata arc sql endpoint reference
titleSuffix: SQL Server big data clusters
description: Reference article for azdata arc sql endpoint commands.
ms.prod: sql
ms.technology: big-data-cluster
---

# azdata arc sql endpoint

[!INCLUDE[tsql-appliesto-ssver15-{{sql_version}}](../includes/tsql-appliesto-ssver15-{{sql_version}}.md)] 

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
JMESPath query string. See [http://jmespath.org/](http://jmespath.org/) for more information and examples.
#### `--verbose`
Increase logging verbosity. Use --debug for full debug logs.

## Next steps

For more information about other **azdata** commands, see [azdata reference](reference-azdata.md). For more information about how to install the **azdata** tool, see [Install azdata to manage SQL Server 2019 big data clusters](deploy-install-azdata.md).
