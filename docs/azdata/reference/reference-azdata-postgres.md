---
title: azdata postgres reference
titleSuffix: SQL Server Big Data Clusters
description: Reference article for azdata postgres commands.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: seanw
ms.date: 10/05/2021
ms.service: sql
ms.subservice: big-data-cluster
ms.topic: reference
---

# azdata postgres

Applies to [!INCLUDE [azure-data-cli-azdata](../../includes/azure-data-cli-azdata.md)]

The following article provides reference for the **sql** commands in the **azdata** tool. For more information about other **azdata** commands, see [azdata reference](reference-azdata.md)

## Commands

|Command|Description|
| --- | --- |
[azdata postgres shell](#azdata-postgres-shell) | A command line shell interface for Postgres. See https://www.pgcli.com/
[azdata postgres query](#azdata-postgres-query) | The query command allows execution of PostgreSQL commands in a database session.
## azdata postgres shell
A command line shell interface for Postgres. See https://www.pgcli.com/
```bash
azdata postgres shell [--dbname -d] 
                      [--host]  
                      
[--port -p]  
                      
[--password -w]  
                      
[--no-password]  
                      
[--single-connection]  
                      
[--username -u]  
                      
[--pgclirc]  
                      
[--dsn]  
                      
[--list-dsn]  
                      
[--row-limit]  
                      
[--less-chatty]  
                      
[--prompt]  
                      
[--prompt-dsn]  
                      
[--list -l]  
                      
[--auto-vertical-output]  
                      
[--warn]  
                      
[--no-warn]
```
### Examples
Example command line to start the interactive experience.
```bash
azdata postgres shell
```
Example command line using a provided database and user
```bash
azdata postgres shell --dbname <database> --username <username> --host <host>
```
Example command line to start using a full connection-string.
```bash
azdata postgres shell --dbname postgres://user:passw0rd@example.com:5432/master 
```
### Optional Parameters
#### `--dbname -d`
Database name to connect to.
#### `--host`
Host address of the postgres database.
#### `--port -p`
Port number at which the postgres instance is listening.
#### `--password -w`
Force password prompt.
#### `--no-password`
Never prompt for password.
#### `--single-connection`
Do not use a separate connection for completions.
#### `--username -u`
Username to connect to the postgres database.
#### `--pgclirc`
Location of pgclirc file.
#### `--dsn`
Use DSN configured into the [alias_dsn] section of pgclirc file.
#### `--list-dsn`
List of DSN configured into the [alias_dsn] section of pgclirc file.
#### `--row-limit`
Set threshold for row limit prompt. Use 0 to disable prompt.
#### `--less-chatty`
Skip intro on startup and goodbye on exit.
#### `--prompt`
Prompt format (Default: "\u@\h:\d> ").
#### `--prompt-dsn`
Prompt format for connections using DSN aliases (Default: "\u@\h:\d> ").
#### `--list -l`
List available databases, then exit.
#### `--auto-vertical-output`
Automatically switch to vertical output mode if the result is wider than the terminal width.
#### `--warn`
Warn before running a destructive query.
#### `--no-warn`
Warn before running a destructive query.
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
## azdata postgres query
The query command allows execution of PostgreSQL commands in a database session.
```bash
azdata postgres query --q -q 
                      [--host]  
                      
[--dbname -d]  
                      
[--port -p]  
                      
[--username -u]
```
### Examples
List all tables in information_schema.
```bash
azdata postgres query --host <host> --username <username> -q "SELECT * FROM information_schema.tables"
```
### Required Parameters
#### `--q -q`
PostgreSQL query to execute.
### Optional Parameters
#### `--host`
Host address of the postgres database.
`localhost`
#### `--dbname -d`
Database to run query in.
#### `--port -p`
Port number at which the postgres instance is listening.
`5432`
#### `--username -u`
Username to connect to the postgres database.
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