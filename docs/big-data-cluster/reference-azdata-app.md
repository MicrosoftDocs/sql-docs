---
title: azdata app reference
titleSuffix: SQL Server big data clusters
description: Reference article for azdata app commands.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: mihaelab
ms.date: 08/28/2019
ms.topic: reference
ms.prod: sql
ms.technology: big-data-cluster
---

# azdata app

[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)] 

The following article provides reference for the **sql** commands in the **azdata** tool. For more information about other **azdata** commands, see [azdata reference](reference-azdata.md)

## Commands
|     |     |
| --- | --- |
[azdata app template](reference-azdata-app-template.md) | Templates.
[azdata app init](#azdata-app-init) | Kickstart new application skeleton.
[azdata app create](#azdata-app-create) | Create application.
[azdata app update](#azdata-app-update) | Update application.
[azdata app list](#azdata-app-list) | List application(s).
[azdata app delete](#azdata-app-delete) | Delete application.
[azdata app run](#azdata-app-run) | Run application.
[azdata app describe](#azdata-app-describe) | Describe application.
## azdata app init
Helps you to kickstart new application skeleton and/or spec files based on runtime environments.
```bash
azdata app init 
```
### Examples
Scaffold a new application `spec.yaml` only.
```bash
azdata app init --spec
```
Scaffold a new R application application skeleton based on the `r` template.
```bash
azdata app init --name reduce --template r
```
Scaffold a new Python application application skeleton based on the `python` template.
```bash
azdata app init --name reduce --template python
```
Scaffold a new SSIS application application skeleton based on the `ssis` template.
```bash
azdata app init --name reduce --template ssis            
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
## azdata app create
Create an application.
```bash
azdata app create 
```
### Examples
Create a new application from a directory containing a valid spec.yaml deployment specification.
```bash
azdata app create --spec /path/to/dir/with/spec/yaml
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
## azdata app update
Update an application.
```bash
azdata app update 
```
### Examples
Update an existing application from a directory containing a valid spec.yaml deployment specification.
```bash
azdata app update --spec /path/to/dir/with/spec/yaml    
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
## azdata app list
List an application(s).,
```bash
azdata app list 
```
### Examples
List application by name and version.
```bash
azdata app list --name reduce  --version v1
```
List all application versions by name.
```bash
azdata app list --name reduce
```
List all application versions by name.
```bash
azdata app list
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
## azdata app delete
Delete an application.
```bash
azdata app delete 
```
### Examples
Delete application by name and version.
```bash
azdata app delete --name reduce --version v1    
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
## azdata app run
Run an application.
```bash
azdata app run 
```
### Examples
Run application with no input parameters.
```bash
azdata app run --name reduce --version v1
```
Run application with 1 input parameter.
```bash
azdata app run --name reduce --version v1 --inputs x=10
```
Run application with multiple input parameters.
```bash
azdata app run --name reduce --version v1 --inputs x=10,y5.6    
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
## azdata app describe
Describe an application.
```bash
azdata app describe 
```
### Examples
Describe the application.
```bash
azdata app describe --name reduce --version v1    
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

## Next steps

For more information about other **azdata** commands, see [azdata reference](reference-azdata.md). For more information about how to install the **azdata** tool, see [Install azdata to manage SQL Server 2019 big data clusters](deploy-install-azdata.md).
