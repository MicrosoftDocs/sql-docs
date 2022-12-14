---
title: azdata bdc spark session reference
titleSuffix: SQL Server Big Data Clusters
description: Reference article for azdata bdc spark session commands.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: seanw
ms.date: 10/05/2021
ms.service: sql
ms.subservice: big-data-cluster
ms.topic: reference
---

# azdata bdc spark session

Applies to [!INCLUDE [azure-data-cli-azdata](../../includes/azure-data-cli-azdata.md)]

The following article provides reference for the **sql** commands in the **azdata** tool. For more information about other **azdata** commands, see [azdata reference](reference-azdata.md)

## Commands

|Command|Description|
| --- | --- |
[azdata bdc spark session create](#azdata-bdc-spark-session-create) | Create a new Spark session.
[azdata bdc spark session list](#azdata-bdc-spark-session-list) | List all the active sessions in Spark.
[azdata bdc spark session info](#azdata-bdc-spark-session-info) | Get information about an active Spark session.
[azdata bdc spark session log](#azdata-bdc-spark-session-log) | Get execution logs for an active Spark session.
[azdata bdc spark session state](#azdata-bdc-spark-session-state) | Get execution state for an active Spark session.
[azdata bdc spark session delete](#azdata-bdc-spark-session-delete) | Delete a Spark session.
## azdata bdc spark session create
This creates a new interactive Spark session. The caller must specify the type of Spark session. This session lives beyond the lifetime of a azdata  execution and must be deleted with 'spark session delete'
```bash
azdata bdc spark session create [--session-kind -k] 
                                [--jar-files -j]  
                                
[--py-files -p]  
                                
[--files -f]  
                                
[--driver-memory]  
                                
[--driver-cores]  
                                
[--executor-memory]  
                                
[--executor-cores]  
                                
[--executor-count]  
                                
[--archives -a]  
                                
[--queue -q]  
                                
[--name -n]  
                                
[--config -c]  
                                
[--timeout-seconds -t]
```
### Examples
Create a session.
```bash
azdata bdc spark session create --session-kind pyspark
```
### Optional Parameters
#### `--session-kind -k`
Name of the type of session to create.  One of spark, pyspark, sparkr, or sql.
#### `--jar-files -j`
List of jar file paths.  To pass in a list JSON encode the values.  Example: '["entry1", "entry2"]'.
#### `--py-files -p`
List of python file paths.  To pass in a list JSON encode the values.  Example: '["entry1", "entry2"]'.
#### `--files -f`
List of file paths.  To pass in a list JSON encode the values.  Example: '["entry1", "entry2"]'.
#### `--driver-memory`
Amount of memory to allocate to the driver.  Specify units as part of value.  Example 512M or 2G.
#### `--driver-cores`
Amount of CPU cores to allocate to the driver.
#### `--executor-memory`
Amount of memory to allocate to the executor.  Specify units as part of value.  Example 512M or 2G.
#### `--executor-cores`
Amount of CPU cores to allocate to the executor.
#### `--executor-count`
Number of instances of the executor to run.
#### `--archives -a`
List of archives paths.  To pass in a list JSON encode the values.  Example: '["entry1", "entry2"]'.
#### `--queue -q`
Name of the Spark queue to execute the session in.
#### `--name -n`
Name of the Spark session.
#### `--config -c`
List of name value pairs containing Spark configuration values.  Encoded as JSON dictionary.  Example: '{"name":"value", "name2":"value2"}'.
#### `--timeout-seconds -t`
Session idle timeout in seconds.
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
## azdata bdc spark session list
List all the active sessions in Spark.
```bash
azdata bdc spark session list 
```
### Examples
List all the active sessions.
```bash
azdata spark session list
```
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
## azdata bdc spark session info
This gets the session information for an active Spark session with the given ID.  The session id is returned from 'spark session create'.
```bash
azdata bdc spark session info --session-id -i 
                              
```
### Examples
Get session info for session with ID of 0.
```bash
azdata spark session info --session-id 0
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
## azdata bdc spark session log
This gets the session log entries for an active Spark session with the given ID.  The session id is returned from 'spark session create'.
```bash
azdata bdc spark session log --session-id -i 
                             
```
### Examples
Get session log for session with ID of 0.
```bash
azdata spark session log --session-id 0
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
## azdata bdc spark session state
This gets the session state for an active Spark session with the given ID.  The session id is returned from 'spark session create'.
```bash
azdata bdc spark session state --session-id -i 
                               
```
### Examples
Get session state for session with ID of 0.
```bash
azdata spark session state --session-id 0
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
## azdata bdc spark session delete
This deletes an interactive Spark session. The session id is returned from 'spark session create'.
```bash
azdata bdc spark session delete --session-id -i 
                                
```
### Examples
Delete a session.
```bash
azdata spark session delete --session-id 0
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

## Next steps

For more information about other **azdata** commands, see [azdata reference](reference-azdata.md). 

For more information about how to install the **azdata** tool, see [Install azdata](..\install\deploy-install-azdata.md).
