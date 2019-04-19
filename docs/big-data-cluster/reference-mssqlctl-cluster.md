---
title: mssqlctl cluster reference
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

# mssqlctl cluster

[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

The following article provides reference for the **cluster** commands in the **mssqlctl** tool. For more information about other **mssqlctl** commands, see [mssqlctl reference](reference-mssqlctl.md).

## Commands
|     |     |
| --- | --- |
[mssqlctl cluster create](#mssqlctl-cluster-create) | Create cluster.
[mssqlctl cluster delete](#mssqlctl-cluster-delete) | Delete cluster.
[mssqlctl cluster config](reference-mssqlctl-cluster-config.md) | Cluster configuration commands.
[mssqlctl cluster debug](reference-mssqlctl-cluster-debug.md) | Debug commands.
## mssqlctl cluster create
Create a SQL Server Big Data Cluster.
```bash
mssqlctl cluster create [--config-file -f] 
                        [--accept-eula -e]  
                        [--env-var -v]
```
### Optional Parameters
#### `--config-file -f`
Cluster config profile, used for deploying the cluster: ['aks-dev-test.json', 'kubeadm-dev-test.json', 'minikube-dev-test.json']
#### `--accept-eula -e`
Do you accept the license terms? [yes/no].
#### `--env-var -v`
Key/Value list (i.e. key1=value1,key2=value2) of environment variables to be set: ['CONTROLLER_USERNAME', 'CONTROLLER_PASSWORD', 'DOCKER_REGISTRY', 'DOCKER_REPOSITORY', 'DOCKER_USERNAME', 'DOCKER_PASSWORD', 'MSSQL_SA_PASSWORD', 'KNOX_PASSWORD']
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
## mssqlctl cluster delete
Delete the SQL Server Big Data Cluster.
```bash
mssqlctl cluster delete --name -n 
                        [--force -f]
```
### Required Parameters
#### `--name -n`
Cluster name, used for kubernetes namespace.
### Optional Parameters
#### `--force -f`
Force delete cluster.
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