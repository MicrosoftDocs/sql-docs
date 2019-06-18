---
title: mssqlctl sql reference
titleSuffix: SQL Server big data clusters
description: Reference article for mssqlctl sql commands.
author: rothja
ms.author: jroth
manager: jroth
ms.date: 06/26/2019
ms.topic: reference
ms.prod: sql
ms.technology: big-data-cluster
---

# mssqlctl sql

[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

The following article provides reference for the **sql** commands in the **mssqlctl** tool. For more information about other **mssqlctl** commands, see [mssqlctl reference](reference-mssqlctl.md).

## Commands
|     |     |
| --- | --- |
[mssqlctl sql shell](#mssqlctl-sql-shell) | The SQL DB CLI allows the user to interact with SQL Server via T-SQL.
[mssqlctl sql query](#mssqlctl-sql-query) | The query command allows execution of a T-SQL query.
## mssqlctl sql shell
The SQL DB CLI allows the user to interact with SQL Server via T-SQL.
```bash
mssqlctl sql shell 
```
### Examples
Example command line to start the interactive experience.
```bash
mssqlctl sql shell
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
## mssqlctl sql query
The query command allows execution of a T-SQL query.
```bash
mssqlctl sql query --database -d 
                   -q
```
### Examples
Select the list of tables names.  Database defaults to master.
```bash
mssqlctl sql query 'SELECT name FROM SYS.TABLES'
```
### Required Parameters
#### `--database -d`
Database to run query in.  Default is master.
#### `-q`
T-SQL query to execute.
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