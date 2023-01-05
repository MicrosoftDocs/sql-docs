---
title: azdata bdc spark batch reference
titleSuffix: SQL Server Big Data Clusters
description: Reference article for azdata bdc spark batch commands.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: seanw
ms.date: 10/05/2021
ms.service: sql
ms.subservice: big-data-cluster
ms.topic: reference
---

# azdata bdc spark batch

Applies to [!INCLUDE [azure-data-cli-azdata](../../includes/azure-data-cli-azdata.md)]

The following article provides reference for the **sql** commands in the **azdata** tool. For more information about other **azdata** commands, see [azdata reference](reference-azdata.md)

## Commands

|Command|Description|
| --- | --- |
[azdata bdc spark batch create](#azdata-bdc-spark-batch-create) | Create a new Spark batch.
[azdata bdc spark batch list](#azdata-bdc-spark-batch-list) | List all the batches in Spark.
[azdata bdc spark batch info](#azdata-bdc-spark-batch-info) | Get information about an active Spark batch.
[azdata bdc spark batch log](#azdata-bdc-spark-batch-log) | Get execution logs for a Spark batch.
[azdata bdc spark batch state](#azdata-bdc-spark-batch-state) | Get execution state for a Spark batch.
[azdata bdc spark batch delete](#azdata-bdc-spark-batch-delete) | Delete a Spark batch.
## azdata bdc spark batch create
This creates a new batch Spark job that executes the provided code.
```bash
azdata bdc spark batch create --file -f 
                              [--class-name -c]  
                              
[--arguments -a]  
                              
[--jar-files -j]  
                              
[--py-files -p]  
                              
[--files]  
                              
[--driver-memory]  
                              
[--driver-cores]  
                              
[--executor-memory]  
                              
[--executor-cores]  
                              
[--executor-count]  
                              
[--archives]  
                              
[--queue -q]  
                              
[--name -n]  
                              
[--config]
```
### Examples
Create a new Spark batch.
```bash
azdata spark batch create --code "2+2"
```
### Required Parameters
#### `--file -f`
Path to file to execute.
### Optional Parameters
#### `--class-name -c`
Name of the class to execute when passing in one or more jar files.
#### `--arguments -a`
List of arguments.  To pass in a list JSON encode the values.  Example: '["entry1", "entry2"]'.
#### `--jar-files -j`
List of jar file paths.  To pass in a list JSON encode the values.  Example: '["entry1", "entry2"]'.
#### `--py-files -p`
List of python file paths.  To pass in a list JSON encode the values.  Example: '["entry1", "entry2"]'.
#### `--files`
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
#### `--archives`
List of archives paths.  To pass in a list JSON encode the values.  Example: '["entry1", "entry2"]'.
#### `--queue -q`
Name of the Spark queue to execute the session in.
#### `--name -n`
Name of the Spark session.
#### `--config`
List of name value pairs containing Spark configuration values.  Encoded as JSON dictionary.  Example: '{"name":"value", "name2":"value2"}'.
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
## azdata bdc spark batch list
List all the batches in Spark.
```bash
azdata bdc spark batch list 
```
### Examples
List all the active batches.
```bash
azdata spark batch list
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
## azdata bdc spark batch info
This gets the information for an Spark batch with the given ID.  The batch id is returned from 'spark batch create'.
```bash
azdata bdc spark batch info --batch-id -i 
                            
```
### Examples
Get batch info for batch with ID of 0.
```bash
azdata spark batch info --batch-id 0
```
### Required Parameters
#### `--batch-id -i`
Spark batch ID number.
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
## azdata bdc spark batch log
This gets the batch log entries for a Spark batch with the given ID.  The batch id is returned from 'spark batch create'.
```bash
azdata bdc spark batch log --batch-id -i 
                           
```
### Examples
Get batch log for batch with ID of 0.
```bash
azdata spark batch log --batch-id 0
```
### Required Parameters
#### `--batch-id -i`
Spark batch ID number.
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
## azdata bdc spark batch state
This gets the batch state for a Spark batch with the given ID.  The batch id is returned from 'spark batch create'.
```bash
azdata bdc spark batch state --batch-id -i 
                             
```
### Examples
Get batch state for batch with ID of 0.
```bash
azdata spark batch state --batch-id 0
```
### Required Parameters
#### `--batch-id -i`
Spark batch ID number.
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
## azdata bdc spark batch delete
This deletes a Spark batch. The batch id is returned from 'spark batch create'.
```bash
azdata bdc spark batch delete --batch-id -i 
                              
```
### Examples
Delete a batch.
```bash
azdata spark batch delete --batch-id 0
```
### Required Parameters
#### `--batch-id -i`
Spark batch ID number.
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