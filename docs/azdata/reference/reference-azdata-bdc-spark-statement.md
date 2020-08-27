---
title: azdata bdc spark statement reference
titleSuffix: SQL Server big data clusters
description: Use this reference article to understand SQL commands in the azdata tool, specifically the bdc spark statement commands. 
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: mihaelab
ms.date: 06/22/2020
ms.topic: reference
ms.prod: sql
ms.technology: big-data-cluster
---

# azdata bdc spark statement

[!INCLUDE[SQL Server 2019](../includes/applies-to-version/sqlserver2019.md)]

The following article provides reference for the `sql` commands in the `azdata` tool. For more information about other `azdata` commands, see [azdata reference](reference-azdata.md).

## Commands
| Command | Description |
| --- | --- |
[azdata bdc spark statement list](#azdata-bdc-spark-statement-list) | List all the statements in the given Spark session.
[azdata bdc spark statement create](#azdata-bdc-spark-statement-create) | Create a new Spark statement in the given session.
[azdata bdc spark statement info](#azdata-bdc-spark-statement-info) | Get information about the requested statement in the given Spark session.
[azdata bdc spark statement cancel](#azdata-bdc-spark-statement-cancel) | Cancel a statement within the given Spark session.
## azdata bdc spark statement list
List all the statements in the given Spark session.
```bash
azdata bdc spark statement list --session-id -i 
                                
```
### Examples
List all the session statements.
```bash
azdata spark statement list --session-id 0
```
### Required Parameters
#### `--session-id -i`
Spark session ID number.
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
## azdata bdc spark statement create
This creates and executes a new statement in the given session.  If the execute is quick the result contains the output from the execution.  Otherwise the result can be retrieved using 'spark session info' after the statement is complete.
```bash
azdata bdc spark statement create --session-id -i 
                                  --code -c
```
### Examples
Run a statement.
```bash
azdata spark statement create --session-id 0 --code "2+2"
```
### Required Parameters
#### `--session-id -i`
Spark session ID number.
#### `--code -c`
String containing code to execute as part of the statement.
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
## azdata bdc spark statement info
This gets the execution status and execution results if the statement has completed. The statement id is returned from 'spark statement create'.
```bash
azdata bdc spark statement info --session-id -i 
                                --statement-id -s
```
### Examples
Get statement info for session with ID of 0 and statement ID of 0.
```bash
azdata spark statement info --session-id 0 --statement-id 0
```
### Required Parameters
#### `--session-id -i`
Spark session ID number.
#### `--statement-id -s`
Spark statement ID number within the given session ID.
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
## azdata bdc spark statement cancel
This cancels a statement within the given Spark session. The statement id is returned from 'spark statement create'.
```bash
azdata bdc spark statement cancel --session-id -i 
                                  --statement-id -s
```
### Examples
Cancel a statement.
```bash
azdata spark statement cancel --session-id 0 --statement-id 0
```
### Required Parameters
#### `--session-id -i`
Spark session ID number.
#### `--statement-id -s`
Spark statement ID number within the given session ID.
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
