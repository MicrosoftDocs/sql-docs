---
title: azdata app reference
titleSuffix: SQL Server Big Data Clusters
description: Reference article for azdata app commands.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: seanw
ms.date: 10/05/2021
ms.service: sql
ms.subservice: big-data-cluster
ms.topic: reference
---

# azdata app

Applies to [!INCLUDE [azure-data-cli-azdata](../../includes/azure-data-cli-azdata.md)]

The following article provides reference for the **sql** commands in the **azdata** tool. For more information about other **azdata** commands, see [azdata reference](reference-azdata.md)

## Commands

|Command|Description|
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
azdata app init [--spec -s] 
                [--name -n]  
                
[--version -v]  
                
[--template -t]  
                
[--destination -d]  
                
[--url -u]
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
### Optional Parameters
#### `--spec -s`
Generate just an application spec.yaml.
#### `--name -n`
Application name.
#### `--version -v`
Application version.
#### `--template -t`
Template name. For a full list off supported template names run `azdata app template list`
#### `--destination -d`
Where to place the application skeleton. Default: current working directory.
#### `--url -u`
Specify a different template repository location. Default: https://github.com/Microsoft/SQLBDC-AppDeploy.git
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
## azdata app create
Create an application.
```bash
azdata app create --spec -s 
                  
```
### Examples
Create a new application from a directory containing a valid spec.yaml deployment specification.
```bash
azdata app create --spec /path/to/dir/with/spec/yaml
```
### Required Parameters
#### `--spec -s`
Path to a directory with a YAML spec file describing the application.
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
## azdata app update
Update an application.
```bash
azdata app update [--spec -s] 
                  [--yes -y]
```
### Examples
Update an existing application from a directory containing a valid spec.yaml deployment specification.
```bash
azdata app update --spec /path/to/dir/with/spec/yaml    
```
### Optional Parameters
#### `--spec -s`
Path to a directory with a YAML spec file describing the application.
#### `--yes -y`
Do not prompt for confirmation when updating an application from the CWD's spec.yaml file.
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
## azdata app list
List an application(s).,
```bash
azdata app list [--name -n] 
                [--version -v]
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
### Optional Parameters
#### `--name -n`
Application name.
#### `--version -v`
Application version.
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
## azdata app delete
Delete an application.
```bash
azdata app delete --name -n 
                  --version -v
```
### Examples
Delete application by name and version.
```bash
azdata app delete --name reduce --version v1    
```
### Required Parameters
#### `--name -n`
Application name.
#### `--version -v`
Application version.
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
## azdata app run
Run an application.
```bash
azdata app run --name -n 
               --version -v  
               
[--inputs]
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
### Required Parameters
#### `--name -n`
Application name.
#### `--version -v`
Application version.
### Optional Parameters
#### `--inputs`
Application input parameters in a CSV `name=value` format.
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
## azdata app describe
Describe an application.
```bash
azdata app describe [--spec -s] 
                    [--name -n]  
                    
[--version -v]
```
### Examples
Describe the application.
```bash
azdata app describe --name reduce --version v1    
```
### Optional Parameters
#### `--spec -s`
Path to a directory with a YAML spec file describing the application.
#### `--name -n`
Application name.
#### `--version -v`
Application version.
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