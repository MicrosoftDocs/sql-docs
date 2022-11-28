---
title: azdata bdc hdfs key reference
titleSuffix: SQL Server Big Data Clusters
description: Reference article for azdata bdc hdfs key commands.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: seanw
ms.date: 10/05/2021
ms.service: sql
ms.subservice: big-data-cluster
ms.topic: reference
---

# azdata bdc hdfs key

Applies to [!INCLUDE [azure-data-cli-azdata](../../includes/azure-data-cli-azdata.md)]

The following article provides reference for the **sql** commands in the **azdata** tool. For more information about other **azdata** commands, see [azdata reference](reference-azdata.md)

## Commands

|Command|Description|
| --- | --- |
[azdata bdc hdfs key create](#azdata-bdc-hdfs-key-create) | Create a HDFS key.
[azdata bdc hdfs key list](#azdata-bdc-hdfs-key-list) | Lists all Hadoop encryption zone keys.
[azdata bdc hdfs key roll](#azdata-bdc-hdfs-key-roll) | Roll a HDFS key.
[azdata bdc hdfs key describe](#azdata-bdc-hdfs-key-describe) | Shows details of an encryption zone key.
## azdata bdc hdfs key create
Create a HDFS key with given name, and given size.
```bash
azdata bdc hdfs key create --name -n 
                           [--size -size]
```
### Examples
To create a 256 bit key with name key1, azdata hdfs key create --name key1 --size 256
```bash
azdata hdfs key create --name key1 --size 256
```
### Required Parameters
#### `--name -n`
Name of the Hadoop encryption zone key. 
### Optional Parameters
#### `--size -size`
Bit length of the Hadoop encryption key, default length is 256.
`256`
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
## azdata bdc hdfs key list
Lists all Hadoop encryption zone keys.
```bash
azdata bdc hdfs key list 
```
### Examples
List all Hadoop encryption zone keys, azdata bdc hdfs key list
```bash
azdata bdc hdfs key list
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
## azdata bdc hdfs key roll
Roll a HDFS key with given name.
```bash
azdata bdc hdfs key roll --name -n 
                         
```
### Examples
To roll a key with name key1, azdata hdfs key roll --name key1.
```bash
azdata hdfs key roll --name key1
```
### Required Parameters
#### `--name -n`
Name of the encryption zone key to roll to a new version. 
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
## azdata bdc hdfs key describe
Shows details of an encryption zone key.
```bash
azdata bdc hdfs key describe --name -n 
                             
```
### Examples
To show the details of a key with name key1.
```bash
azdata hdfs key describe --name key1
```
### Required Parameters
#### `--name -n`
Name of the encryption zone key to describe. 
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