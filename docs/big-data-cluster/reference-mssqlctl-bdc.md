---
title: mssqlctl bdc reference
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

# mssqlctl bdc

[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

The following article provides reference for the **bdc** commands in the **mssqlctl** tool. For more information about other **mssqlctl** commands, see [mssqlctl reference](reference-mssqlctl.md).

## Commands
|     |     |
| --- | --- |
[mssqlctl bdc create](#mssqlctl-bdc-create) | Create Big Data Cluster.
[mssqlctl bdc delete](#mssqlctl-bdc-delete) | Delete Big Data Cluster.
[mssqlctl bdc config](reference-mssqlctl-bdc-config.md) | Configuration commands.
[mssqlctl bdc endpoint](reference-mssqlctl-bdc-endpoint.md) | Endpoint commands.
[mssqlctl bdc status](reference-mssqlctl-bdc-status.md) | Status commands.
[mssqlctl bdc debug](reference-mssqlctl-bdc-debug.md) | Debug commands.
[mssqlctl bdc storage-pool](reference-mssqlctl-bdc-storage-pool.md) | Storage pool commands.
[mssqlctl bdc control](reference-mssqlctl-bdc-control.md) | Control commands.
[mssqlctl bdc pool](reference-mssqlctl-bdc-pool.md) | Pool commands.
## mssqlctl bdc create
Create a SQL Server Big Data Cluster - kube config is required on your system along with the following environment variables ['CONTROLLER_USERNAME', 'CONTROLLER_PASSWORD', 'DOCKER_USERNAME', 'DOCKER_PASSWORD', 'MSSQL_SA_PASSWORD', 'KNOX_PASSWORD'].
```bash
mssqlctl bdc create [--config-profile -c] 
                    [--accept-eula -a]  
                    [--node-label -l]  
                    [--force -f]
```
### Examples
Guided BDC deployment experience - you will receive prompts for needed values.
```bash
mssqlctl bdc create
```
BDC deployment with arguments.
```bash
mssqlctl bdc create --accept-eula yes --config-profile aks-dev-test
```
BDC deployment with arguments - no prompts will be given as the --force flag is used.
```bash
mssqlctl bdc create --accept-eula yes --config-profile aks-dev-test --force
```
### Optional Parameters
#### `--config-profile -c`
BDC config profile, used for deploying the cluster: ['aks-dev-test', 'kubeadm-dev-test', 'minikube-dev-test']
#### `--accept-eula -a`
Do you accept the license terms? [yes/no]. If you do not want to use this arg, you may set the environment variable ACCEPT_EULA to 'yes'
#### `--node-label -l`
BDC node label, used to designate what nodes to deploy to.
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
## mssqlctl bdc delete
Delete the SQL Server Big Data Cluster - kube config is required on your system along with the following environment variables ['CONTROLLER_USERNAME', 'CONTROLLER_PASSWORD'].
```bash
mssqlctl bdc delete --name -n 
                    [--force -f]
```
### Examples
BDC deletion where the controller username and password are already set in your system environment.
```bash
mssqlctl bdc delete --name <cluster_name>
```
### Required Parameters
#### `--name -n`
BDC name, used for kubernetes namespace.
### Optional Parameters
#### `--force -f`
Force delete BDC.
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
