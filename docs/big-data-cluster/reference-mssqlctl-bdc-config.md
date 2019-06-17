---
title: mssqlctl bdc config reference
titleSuffix: SQL Server big data clusters
description: Reference article for mssqlctl bdc commands.
author: rothja
ms.author: jroth
manager: jroth
ms.date: 06/26/2019
ms.topic: reference
ms.prod: sql
ms.technology: big-data-cluster
---

# mssqlctl bdc config

[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

The following article provides reference for the **bdc config** commands in the **mssqlctl** tool. For more information about other **mssqlctl** commands, see [mssqlctl reference](reference-mssqlctl.md).

## Commands
|     |     |
| --- | --- |
[mssqlctl bdc config show](#mssqlctl-bdc-config-show) | Gets the Big Data Cluster's current configuration.
[mssqlctl bdc config init](#mssqlctl-bdc-config-init) | Initializes a Big Data Cluster configuration profile that can be used with cluster create.
[mssqlctl bdc config list](#mssqlctl-bdc-config-list) | Lists available configuration profile choices.
[mssqlctl bdc config section](reference-mssqlctl-bdc-config-section.md) | Commands for working with individual sections of the Big Data Cluster configuration profile.
## mssqlctl bdc config show
Gets the Big Data Cluster's current configuration profile and outputs it to the target directory or pretty prints it to the console.
```bash
mssqlctl bdc config show [--target -t] 
                         [--force -f]
```
### Examples
Show the BDC config in your console
```bash
mssqlctl bdc config show
```
### Optional Parameters
#### `--target -t`
Output file to store the result in. Default: directed to stdout.
#### `--force -f`
Force overwrite of the target file.
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
## mssqlctl bdc config init
Initializes a Big Data Cluster configuration  profile that can be used with cluster create. The specific source of the configuration profile can be specified in the arguments from 3 choices.
```bash
mssqlctl bdc config init [--target -t] 
                         [--source -s]  
                         [--force -f]
```
### Examples
Guided BDC config init experience - you will receive prompts for needed values.
```bash
mssqlctl bdc config init
```
BDC config init with arguments, creates a configuration profile of aks-dev-test in ./custom.
```bash
mssqlctl bdc config init --source aks-dev-test --target custom
```
### Optional Parameters
#### `--target -t`
File path of where you would like the config profile placed, defaults to cwd with custom-config.json.
#### `--source -s`
Config profile source: ['aks-dev-test', 'kubeadm-dev-test', 'minikube-dev-test']
#### `--force -f`
Force overwrite of the target file.
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
## mssqlctl bdc config list
Lists available configuration profile choices for use in `bdc config init`
```bash
mssqlctl bdc config list [--config-profile -c] 
                         
```
### Examples
Shows all available configuration profile names.
```bash
mssqlctl bdc config list
```
Shows json of a specific configuration profile.
```bash
mssqlctl bdc config list --config-profile aks-dev-test
```
### Optional Parameters
#### `--config-profile -c`
Default config profile: ['aks-dev-test', 'kubeadm-dev-test', 'minikube-dev-test']
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