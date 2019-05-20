---
title: mssqlctl cluster config reference
titleSuffix: SQL Server big data clusters
description: Reference article for mssqlctl cluster commands.
author: rothja
ms.author: jroth
manager: craigg
ms.date: 05/22/2019
ms.topic: reference
ms.prod: sql
ms.technology: big-data-cluster
---

# mssqlctl cluster config

[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

The following article provides reference for the **cluster config** commands in the **mssqlctl** tool. For more information about other **mssqlctl** commands, see [mssqlctl reference](reference-mssqlctl.md).

## Commands
|     |     |
| --- | --- |
[mssqlctl cluster config show](#mssqlctl-cluster-config-show) | Gets the SQL Server Big Data Cluster's current configuration.
[mssqlctl cluster config init](#mssqlctl-cluster-config-init) | Initializes a cluster configuration profile that can be used with cluster create.
[mssqlctl cluster config list](#mssqlctl-cluster-config-list) | Lists available configuration file choices.
[mssqlctl cluster config section](reference-mssqlctl-cluster-config-section.md) | Commands for working with individual sections of the cluster configuration file.
## mssqlctl cluster config show
Gets the SQL Server Big Data Cluster's current configuration file and outputs it to the target file or pretty prints it to the console.
```bash
mssqlctl cluster config show [--target -t] 
                             [--force -f]
```
### Examples
Show the cluster config in your console
```bash
mssqlctl cluster config show
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
## mssqlctl cluster config init
Initializes a cluster configuration  profile that can be used with cluster create. The specific source of the configuration profile can be specified in the arguments from 3 choices.
```bash
mssqlctl cluster config init [--target -t] 
                             [--src -s]  
                             [--force -f]
```
### Examples
Guided cluster config init experience - you will receive prompts for needed values.
```bash
mssqlctl cluster config init
```
Cluster config init with arguments, creates a configuration profile of aks-dev-test in ./custom.json.
```bash
mssqlctl cluster config init --src aks-dev-test.json --target custom.json
```
### Optional Parameters
#### `--target -t`
File path of where you would like the config profile placed, defaults to cwd with custom-config.json.
#### `--src -s`
Config profile source: ['aks-dev-test.json', 'kubeadm-dev-test.json', 'minikube-dev-test.json']
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
## mssqlctl cluster config list
Lists available configuration file choices for use in cluster config init
```bash
mssqlctl cluster config list [--config-file -c] 
                             
```
### Examples
Shows all available configuration profile names.
```bash
mssqlctl cluster config list
```
Shows json of a specific configuration profile.
```bash
mssqlctl cluster config list --config-file aks-dev-test.json
```
### Optional Parameters
#### `--config-file -c`
Default config file: ['aks-dev-test.json', 'kubeadm-dev-test.json', 'minikube-dev-test.json']
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