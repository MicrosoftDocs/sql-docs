---
title: mssqlctl cluster config reference
titleSuffix: SQL Server big data clusters
description: Reference article for mssqlctl cluster commands.
author: rothja
ms.author: jroth
manager: craigg
ms.date: 04/24/2019
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
[mssqlctl cluster config get](#mssqlctl-cluster-config-get) | Get cluster config - kube config is required on your system.
[mssqlctl cluster config init](#mssqlctl-cluster-config-init) | Initializes a cluster configuration.
[mssqlctl cluster config list](#mssqlctl-cluster-config-list) | Lists available configuration file choices.
[mssqlctl cluster config section](reference-mssqlctl-cluster-config-section.md) | Commands for working with individual sections of the configuration file.
## mssqlctl cluster config get
Gets the SQL Server Big Data Cluster's current configuration file.
```bash
mssqlctl cluster config get --name -n 
                            [--output-file -f]
```
### Required Parameters
#### `--name -n`
Cluster name, used for kubernetes namespace.
### Optional Parameters
#### `--output-file -f`
Output file to store the result in. Default: directed to stdout.
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
Initializes a cluster configuration file for the user based on the specified default type.
```bash
mssqlctl cluster config init [--target -t] 
                             [--src -s]
```
### Optional Parameters
#### `--target -t`
File path of where you would like the config file placed, defaults to cwd with custom-config.json.
#### `--src -s`
Config source: ['aks-dev-test.json', 'kubeadm-dev-test.json', 'minikube-dev-test.json']
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
mssqlctl cluster config list [--config-file -f] 
                             
```
### Optional Parameters
#### `--config-file -f`
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