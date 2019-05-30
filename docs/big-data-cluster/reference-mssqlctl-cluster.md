---
title: mssqlctl cluster reference
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

# mssqlctl cluster

[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

The following article provides reference for the **cluster** commands in the **mssqlctl** tool. For more information about other **mssqlctl** commands, see [mssqlctl reference](reference-mssqlctl.md).

## Commands
|     |     |
| --- | --- |
[mssqlctl cluster create](#mssqlctl-cluster-create) | Create cluster.
[mssqlctl cluster delete](#mssqlctl-cluster-delete) | Delete cluster.
[mssqlctl cluster config](reference-mssqlctl-cluster-config.md) | Cluster configuration commands.
[mssqlctl cluster endpoint](reference-mssqlctl-cluster-endpoint.md) | Endpoint commands.
[mssqlctl cluster status](reference-mssqlctl-cluster-status.md) | Status commands.
[mssqlctl cluster debug](reference-mssqlctl-cluster-debug.md) | Debug commands.
[mssqlctl cluster storage-pool](reference-mssqlctl-cluster-storage-pool.md) | Manage cluster storage-pools.
## mssqlctl cluster create
Create a SQL Server Big Data Cluster - kube config is required on your system along with the following environment variables ['CONTROLLER_USERNAME', 'CONTROLLER_PASSWORD', 'DOCKER_USERNAME', 'DOCKER_PASSWORD', 'MSSQL_SA_PASSWORD', 'KNOX_PASSWORD'].
```bash
mssqlctl cluster create [--config-file -c] 
                        [--accept-eula -a]  
                        [--node-label -l]  
                        [--force -f]
```
### Examples
Guided cluster deployment experience - you will receive prompts for needed values.
```bash
mssqlctl cluster create
```
Cluster deployment with arguments.
```bash
mssqlctl cluster create --accept-eula yes --config-file aks-dev-test.json
```
Cluster deployment with arguments - no prompts will be given as the --force flag is used.
```bash
mssqlctl cluster create --accept-eula yes --config-file aks-dev-test.json --force
```
### Optional Parameters
#### `--config-file -c`
Cluster config profile, used for deploying the cluster: ['aks-dev-test.json', 'kubeadm-dev-test.json', 'minikube-dev-test.json']
#### `--accept-eula -a`
Do you accept the license terms? [yes/no]. If you do not want to use this arg, you may set the environment variable ACCEPT_EULA to 'yes'
#### `--node-label -l`
Cluster node label, used to designate what nodes to deploy to.
#### `--force -f`
Force create, the user will not be prompted for any values and all issues will be printed as partof stderr.
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
Delete the SQL Server Big Data Cluster - kube config is required on your system along with the following environment variables ['CONTROLLER_USERNAME', 'CONTROLLER_PASSWORD'].
```bash
mssqlctl cluster delete --name -n 
                        [--force -f]
```
### Examples
Cluster deletion where the controller username and password are already set in your system environment.
```bash
mssqlctl cluster delete --name <cluster_name>
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
