---
title: azdata arc sql mi reference
titleSuffix: SQL Server big data clusters
description: Reference article for azdata arc sql mi commands.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: seanw
ms.date: 09/22/2020
ms.topic: reference
ms.prod: sql
ms.technology: big-data-cluster
---

# azdata arc sql mi

Applies to `azdata`

The following article provides reference for the **sql** commands in the **azdata** tool. For more information about other **azdata** commands, see [azdata reference](reference-azdata.md)

## Commands

|Command|Description|
| --- | --- |
[azdata arc sql mi create](#azdata-arc-sql-mi-create) | Create a SQL managed instance.
[azdata arc sql mi edit](#azdata-arc-sql-mi-edit) | Edit the configuration of a SQL managed instance.
[azdata arc sql mi delete](#azdata-arc-sql-mi-delete) | Delete a SQL managed instance.
[azdata arc sql mi show](#azdata-arc-sql-mi-show) | Show the details of a SQL managed instance.
[azdata arc sql mi list](#azdata-arc-sql-mi-list) | List SQL managed instances.
[azdata arc sql mi config](reference-azdata-arc-sql-mi-config.md) | Configuration commands.
## azdata arc sql mi create
To set the password of the SQL managed instance, please set the environment variable AZDATA_PASSWORD
```bash
azdata arc sql mi create --name -n 
                         [--path]  
                         
[--cores-limit -cl]  
                         
[--cores-request -cr]  
                         
[--memory-limit -ml]  
                         
[--memory-request -mr]  
                         
[--storage-class-data -scd]  
                         
[--storage-class-logs -scl]  
                         
[--storage-class-data-logs -scdl]  
                         
[--storage-class-backups -scb]  
                         
[--volume-size-data -vsd]  
                         
[--volume-size-logs -vsl]  
                         
[--volume-size-data-logs -vsdl]  
                         
[--volume-size-backups -vsb]  
                         
[--no-external-endpoint]  
                         
[--dev]  
                         
[--no-wait]
```
### Examples
Create a SQL managed instance.
```bash
azdata arc sql mi create -n sqlmi1
```
### Required Parameters
#### `--name -n`
The name of the SQL managed instance.
### Optional Parameters
#### `--path`
The path to the src file for the SQL managed instance json file.
#### `--cores-limit -cl`
The cores limit of the managed instance as an integer.
#### `--cores-request -cr`
The request for cores of the managed instance as an integer.
#### `--memory-limit -ml`
The limit of the capacity of the managed instance as an integer.
#### `--memory-request -mr`
The request for the capcity of the managed instance as an integer amount of memory in GBs.
#### `--storage-class-data -scd`
The storage class to be used for data (.mdf). If no value is specified, then no storage class will be specified, which will result in Kubernetes using the default storage class.
#### `--storage-class-logs -scl`
The storage class to be used for logs (/var/log). If no value is specified, then no storage class will be specified, which will result in Kubernetes using the default storage class.
#### `--storage-class-data-logs -scdl`
The storage class to be used for database logs (.ldf). If no value is specified, then no storage class will be specified, which will result in Kubernetes using the default storage class.
#### `--storage-class-backups -scb`
The storage class to be used for backups (/var/opt/mssql/backups). If no value is specified, then no storage class will be specified, which will result in Kubernetes using the default storage class.
#### `--volume-size-data -vsd`
The size of the storage volume to be used for data as a positive number followed by Ki (kilobytes), Mi (megabytes), or Gi (gigabytes).
#### `--volume-size-logs -vsl`
The size of the storage volume to be used for logs as a positive number followed by Ki (kilobytes), Mi (megabytes), or Gi (gigabytes).
#### `--volume-size-data-logs -vsdl`
The size of the storage volume to be used for data logs as a positive number followed by Ki (kilobytes), Mi (megabytes), or Gi (gigabytes).
#### `--volume-size-backups -vsb`
The size of the storage volume to be used for backups as a positive number followed by Ki (kilobytes), Mi (megabytes), or Gi (gigabytes).
#### `--no-external-endpoint`
If specified, no external service will be created. Otherwise, an external service will be created using the same service type as the data controller.
#### `--dev`
If this is specified, then it is considered a dev instance and will not be billed for.
#### `--no-wait`
If given, the command will not wait for the instance to be in a ready state before returning.
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
## azdata arc sql mi edit
Edit the configuration of a SQL managed instance.
```bash
azdata arc sql mi edit --name -n 
                       [--path]  
                       
[--cores-limit -cl]  
                       
[--cores-request -cr]  
                       
[--memory-limit -ml]  
                       
[--memory-request -mr]  
                       
[--dev]  
                       
[--no-wait]
```
### Examples
Edit the configuration of a SQL managed instance.
```bash
azdata arc sql mi edit --path ./spec.json -n sqlmi1
```
### Required Parameters
#### `--name -n`
The name of the SQL managed instance that is being edited. The name under which your instance is deployed cannot be changed.
### Optional Parameters
#### `--path`
The path to the src file for the SQL managed instance json file.
#### `--cores-limit -cl`
The cores limit of the managed instance as an integer.
#### `--cores-request -cr`
The request for cores of the managed instance as an integer.
#### `--memory-limit -ml`
The limit of the capacity of the managed instance as an integer.
#### `--memory-request -mr`
The request for the capcity of the managed instance as an integer amount of memory in GBs.
#### `--dev`
If this is specified, then it is considered a dev instance and will not be billed for.
#### `--no-wait`
If given, the command will not wait for the instance to be in a ready state before returning.
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
## azdata arc sql mi delete
Delete a SQL managed instance.
```bash
azdata arc sql mi delete --name -n 
                         
```
### Examples
Delete a SQL managed instance.
```bash
azdata arc sql mi delete -n sqlmi1
```
### Required Parameters
#### `--name -n`
The name of the SQL managed instance to be deleted.
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
## azdata arc sql mi show
Show the details of a SQL managed instance.
```bash
azdata arc sql mi show --name -n 
                       [--path -p]
```
### Examples
Show the details of a SQL managed instance.
```bash
azdata arc sql mi show -n sqlmi1
```
### Required Parameters
#### `--name -n`
The name of the SQL managed instance to be shown.
### Optional Parameters
#### `--path -p`
A path where the full specification for the SQL managed instance should be written. If omitted, the specification will be written to standard output.
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
## azdata arc sql mi list
List SQL managed instances.
```bash
azdata arc sql mi list 
```
### Examples
List SQL managed instances.
```bash
azdata arc sql mi list
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

## Next steps

For more information about other **azdata** commands, see [azdata reference](reference-azdata.md). 

For more information about how to install the **azdata** tool, see [Install azdata](..\install\deploy-install-azdata.md).

