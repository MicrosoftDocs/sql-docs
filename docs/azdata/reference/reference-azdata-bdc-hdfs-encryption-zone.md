---
title: azdata bdc hdfs encryption-zone reference
titleSuffix: SQL Server Big Data Clusters
description: Reference article for azdata bdc hdfs encryption-zone commands.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: seanw
ms.date: 10/05/2021
ms.service: sql
ms.subservice: big-data-cluster
ms.topic: reference
---

# azdata bdc hdfs encryption-zone

Applies to [!INCLUDE [azure-data-cli-azdata](../../includes/azure-data-cli-azdata.md)]

The following article provides reference for the **sql** commands in the **azdata** tool. For more information about other **azdata** commands, see [azdata reference](reference-azdata.md)

## Commands

|Command|Description|
| --- | --- |
[azdata bdc hdfs encryption-zone create](#azdata-bdc-hdfs-encryption-zone-create) | Converts an HDFS folder to an encryption zone.
[azdata bdc hdfs encryption-zone list](#azdata-bdc-hdfs-encryption-zone-list) | List all encryption zones and the keys associated with encryption zones.
[azdata bdc hdfs encryption-zone get-file-encryption-info](#azdata-bdc-hdfs-encryption-zone-get-file-encryption-info) | Get the encryption information for a given HDFS file path.
[azdata bdc hdfs encryption-zone status](#azdata-bdc-hdfs-encryption-zone-status) | Get the re-encryption status for the HDFS cluster.
[azdata bdc hdfs encryption-zone reencrypt](#azdata-bdc-hdfs-encryption-zone-reencrypt) | Controls the re-encryption of the encryption zone specified by the --path argument.
## azdata bdc hdfs encryption-zone create
Converts an HDFS folder to an encryption zone by using the provided key to encrypt the files in the encryption zone.
```bash
azdata bdc hdfs encryption-zone create --path -p 
                                       --keyname -k
```
### Examples
To convert an existing folder /user/securefolder to an encryption zone, using a key called securelake
```bash
azdata bdc hdfs encryption-zone create --path /home/securefolder --keyname securelake
```
### Required Parameters
#### `--path -p`
Path of the HDFS folder to be converted to encryption zone.
#### `--keyname -k`
Hadoop KMS key to be used to protect the encryption zone.
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
## azdata bdc hdfs encryption-zone list
List all encryption zones and the keys associated with encryption zones.
```bash
azdata bdc hdfs encryption-zone list 
```
### Examples
List all encryption zones.
```bash
azdata bdc hdfs encryption-zone list
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
## azdata bdc hdfs encryption-zone get-file-encryption-info
Get the encryption information for a given HDFS file path.
```bash
azdata bdc hdfs encryption-zone get-file-encryption-info --path -p 
                                                         
```
### Examples
Get the encryption information for a file at /user/securefolder/data.csv.
```bash
azdata bdc hdfs encryption-zone get-file-encryption-info --path /user/securefolder/data.csv
```
### Required Parameters
#### `--path -p`
HDFS file path for which encryption information should be retrieved.
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
## azdata bdc hdfs encryption-zone status
Get the re-encryption status for the HDFS cluster.
```bash
azdata bdc hdfs encryption-zone status 
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
## azdata bdc hdfs encryption-zone reencrypt
Controls the re-encryption of the encryption zone specified by the --path argument.
```bash
azdata bdc hdfs encryption-zone reencrypt --path -p 
                                          --action -a
```
### Examples
Start the re-encryption of the encryption zone securelake.
```bash
azdata bdc hdfs encryption-zone reencrypt --path /securelake --action start
```
Cancel the re-encryption of the encryption zone securelake.
```bash
azdata bdc hdfs encryption-zone reencrypt --path /securelake --action cancel
```
### Required Parameters
#### `--path -p`
Path of the HDFS encryption zone folder which should be re-encrypted
#### `--action -a`
Re-encryption action to be performed. Valid values are start and cancel
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