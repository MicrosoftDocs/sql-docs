---
title: azdata sql reference
titleSuffix: SQL Server Big Data Clusters
description: Reference article for azdata sql commands.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: seanw
ms.date: 10/05/2021
ms.service: sql
ms.subservice: big-data-cluster
ms.topic: reference
---

# azdata sql

Applies to [!INCLUDE [azure-data-cli-azdata](../../includes/azure-data-cli-azdata.md)]

The following article provides reference for the **sql** commands in the **azdata** tool. For more information about other **azdata** commands, see [azdata reference](reference-azdata.md)

## Commands

|Command|Description|
| --- | --- |
[azdata sql shell](#azdata-sql-shell) | The SQL CLI allows the user to interact with SQL Server and Azure SQL via T-SQL.
[azdata sql query](#azdata-sql-query) | The SQL CLI allows the user to interact with SQL Server and Azure SQL via T-SQL.
## azdata sql shell
The SQL CLI allows the user to interact with SQL Server and Azure SQL via T-SQL.
```bash
azdata sql shell [--username -u] 
                 [--database -d]  
                 
[--server -s]  
                 
[--integrated -e]  
                 
[--mssqlclirc]  
                 
[--row-limit]  
                 
[--less-chatty]  
                 
[--auto-vertical-output]  
                 
[--encrypt -n]  
                 
[--trust-server-certificate -c]  
                 
[--connect-timeout -l]  
                 
[--application-intent -k]  
                 
[--multi-subnet-failover -m]  
                 
[--packet-size]  
                 
[--dac-connection -a]  
                 
[--input-file -i]  
                 
[--output-file]  
                 
[--enable-sqltoolsservice-logging]  
                 
[--prompt]
```
### Examples
Example command line to start the interactive experience.
```bash
azdata sql shell
```
Example command line using a provided server, user, and database
```bash
azdata sql shell --server localhost --username sa --database master         
```
### Optional Parameters
#### `--username -u`
Username to connect to the database.
#### `--database -d`
Database name to connect to.
#### `--server -s`
SQL Server instance name or address.
#### `--integrated -e`
Use integrated authentication on Windows.
#### `--mssqlclirc`
Location of mssqlclirc config file.
#### `--row-limit`
Set threshold for row limit prompt. Use 0 to disable prompt.
#### `--less-chatty`
Skip intro on startup and goodbye on exit.
#### `--auto-vertical-output`
Automatically switch to vertical output mode if the result is wider than the terminal width.
#### `--encrypt -n`
SQL Server uses SSL encryption for all data if the server has a certificate installed.
#### `--trust-server-certificate -c`
The channel will be encrypted while bypassing walking the certificate chain to validate trust.
#### `--connect-timeout -l`
Time in seconds to wait for a connection to the server before terminating request.
#### `--application-intent -k`
Declares the application workload type when connecting to a database in a SQL Server Availability Group.
#### `--multi-subnet-failover -m`
If application is connecting to Always On AG on different subnets, setting this provides faster detection and connection to currently active server.
#### `--packet-size`
Size in bytes of the network packets used to communicate with SQL Server.
#### `--dac-connection -a`
Connect to SQL Server using the dedicated administrator connection.
#### `--input-file -i`
Specifies the file that contains a batch of SQL statements for processing.
#### `--output-file`
Specifies the file that receives output from a query.
#### `--enable-sqltoolsservice-logging`
Enables diagnostic logging for the SqlToolsService.
#### `--prompt`
Prompt format (Default: \d>
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
## azdata sql query
The SQL CLI allows the user to interact with SQL Server and Azure SQL via T-SQL.
```bash
azdata sql query -q 
                 [--database -d]  
                 
[--username -u]  
                 
[--server -s]  
                 
[--integrated -e]
```
### Examples
Example command line to select the list of tables names.
```bash
azdata sql query --server localhost --username sa --database master -q "SELECT name FROM SYS.TABLES"
```
### Required Parameters
#### `-q`
T-SQL query to execute.
### Optional Parameters
#### `--database -d`
Database name to connect to.
`master`
#### `--username -u`
Username to connect to the database.
#### `--server -s`
SQL Server instance name or address.
#### `--integrated -e`
Use integrated authentication on Windows.
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
