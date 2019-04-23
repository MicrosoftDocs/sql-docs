---
title: mssqlctl app reference
titleSuffix: SQL Server big data clusters
description: Reference article for mssqlctl app commands.
author: rothja
ms.author: jroth
manager: craigg
ms.date: 04/24/2019
ms.topic: reference
ms.prod: sql
ms.technology: big-data-cluster
---

# mssqlctl app

[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

The following article provides reference for the **app** commands in the **mssqlctl** tool. For more information about other **mssqlctl** commands, see [mssqlctl reference](reference-mssqlctl.md).

## Commands
|     |     |
| --- | --- |
[mssqlctl app template](reference-mssqlctl-app-template.md) | Templates.
[mssqlctl app init](#mssqlctl-app-init) | Kickstart new application skeleton.
[mssqlctl app create](#mssqlctl-app-create) | Create application.
[mssqlctl app update](#mssqlctl-app-update) | Update application.
[mssqlctl app list](#mssqlctl-app-list) | List application(s).
[mssqlctl app delete](#mssqlctl-app-delete) | Delete application.
[mssqlctl app run](#mssqlctl-app-run) | Run application.
[mssqlctl app describe](#mssqlctl-app-describe) | Describe application.
## mssqlctl app init
Helps you to kickstart new application skeleton and/or spec files based on runtime environments.
```bash
mssqlctl app init [--spec -s] 
                  [--name -n]  
                  [--version -v]  
                  [--template -t]  
                  [--destination -d]  
                  [--url -u]
```
### Examples
Scaffold a new application `spec.yaml` only.
```bash
mssqlctl app init --spec
```
Scaffold a new R application application skeleton based on the `r` template.
```bash
mssqlctl app init --name reduce --template r
```
Scaffold a new Python application application skeleton based on the `python` template.
```bash
mssqlctl app init --name reduce --template python
```
Scaffold a new SSIS application application skeleton based on the `ssis` template.
```bash
mssqlctl app init --name reduce --template ssis            
```
### Optional Parameters
#### `--spec -s`
Generate just an application spec.yaml.
#### `--name -n`
Application name.
#### `--version -v`
Application version.
#### `--template -t`
Template name. For a full list off supported template names run `mssqlctl app template list`
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
JMESPath query string. See [http://jmespath.org/](http://jmespath.org/]) for more information and examples.
#### `--verbose`
Increase logging verbosity. Use --debug for full debug logs.
## mssqlctl app create
Create an application.
```bash
mssqlctl app create --spec -s 
                    
```
### Examples
Create a new application from a directory containing a valid spec.yaml deployment specification.
```bash
mssqlctl app create --spec /path/to/dir/with/spec/yaml
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
JMESPath query string. See [http://jmespath.org/](http://jmespath.org/]) for more information and examples.
#### `--verbose`
Increase logging verbosity. Use --debug for full debug logs.
## mssqlctl app update
Update an application.
```bash
mssqlctl app update [--spec -s] 
                    [--yes -y]
```
### Examples
Update an existing application from a directory containing a valid spec.yaml deployment specification.
```bash
mssqlctl app update --spec /path/to/dir/with/spec/yaml    
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
JMESPath query string. See [http://jmespath.org/](http://jmespath.org/]) for more information and examples.
#### `--verbose`
Increase logging verbosity. Use --debug for full debug logs.
## mssqlctl app list
List an application(s).,
```bash
mssqlctl app list [--name -n] 
                  [--version -v]
```
### Examples
List application by name and version.
```bash
mssqlctl app list --name reduce  --version v1
```
List all application versions by name.
```bash
mssqlctl app list --name reduce
```
List all application versions by name.
```bash
mssqlctl app list
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
JMESPath query string. See [http://jmespath.org/](http://jmespath.org/]) for more information and examples.
#### `--verbose`
Increase logging verbosity. Use --debug for full debug logs.
## mssqlctl app delete
Delete an application.
```bash
mssqlctl app delete --name -n 
                    --version -v
```
### Examples
Delete application by name and version.
```bash
mssqlctl app delete --name reduce --version v1    
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
JMESPath query string. See [http://jmespath.org/](http://jmespath.org/]) for more information and examples.
#### `--verbose`
Increase logging verbosity. Use --debug for full debug logs.
## mssqlctl app run
Run an application.
```bash
mssqlctl app run --name -n 
                 --version -v  
                 [--inputs]
```
### Examples
Run application with no input parameters.
```bash
mssqlctl app run --name reduce --version v1
```
Run application with 1 input parameter.
```bash
mssqlctl app run --name reduce --version v1 --inputs x=10
```
Run application with multiple input parameters.
```bash
mssqlctl app run --name reduce --version v1 --inputs x=10,y5.6    
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
JMESPath query string. See [http://jmespath.org/](http://jmespath.org/]) for more information and examples.
#### `--verbose`
Increase logging verbosity. Use --debug for full debug logs.
## mssqlctl app describe
Describe an application.
```bash
mssqlctl app describe [--spec -s] 
                      [--name -n]  
                      [--version -v]
```
### Examples
Describe the application.
```bash
mssqlctl app describe --name reduce --version v1    
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
JMESPath query string. See [http://jmespath.org/](http://jmespath.org/]) for more information and examples.
#### `--verbose`
Increase logging verbosity. Use --debug for full debug logs.

## Next steps

For more information about other **mssqlctl** commands, see [mssqlctl reference](reference-mssqlctl.md). For more information about how to install the **mssqlctl** tool, see [Install mssqlctl to manage SQL Server 2019 big data clusters](deploy-install-mssqlctl.md).