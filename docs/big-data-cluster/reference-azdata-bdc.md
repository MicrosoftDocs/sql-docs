---
title: azdata bdc reference
titleSuffix: SQL Server big data clusters
description: Reference article for azdata bdc commands.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: mihaelab
ms.date: 07/24/2019
ms.topic: reference
ms.prod: sql
ms.technology: big-data-cluster
---

# azdata bdc

[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

The following article provides reference for the **bdc** commands in the **azdata** tool. For more information about other **azdata** commands, see [azdata reference](reference-azdata.md).

## Commands

|     |     |
| --- | --- |
[azdata bdc create](#azdata-bdc-create) | Create Big Data Cluster.
[azdata bdc delete](#azdata-bdc-delete) | Delete Big Data Cluster.
[azdata bdc config](reference-azdata-bdc-config.md) | Configuration commands.
[azdata bdc endpoint](reference-azdata-bdc-endpoint.md) | Endpoint commands.
[azdata bdc status](reference-azdata-bdc-status.md) | Status commands.
[azdata bdc debug](reference-azdata-bdc-debug.md) | Debug commands.
[azdata bdc control](reference-azdata-bdc-control.md) | Control commands.
[azdata bdc pool](reference-azdata-bdc-pool.md) | Pool commands.
[azdata bdc hdfs](reference-azdata-bdc-hdfs.md) | The HDFS module provides commands to access an HDFS file system.
[azdata bdc spark](reference-azdata-bdc-spark.md) | The Spark commands allow the user to interact with the Spark system by creating and managing sessions, statements, and batches.
## azdata bdc create
Create a SQL Server Big Data Cluster - kube config is required on your system along with the following environment variables ['CONTROLLER_USERNAME', 'CONTROLLER_PASSWORD', 'MSSQL_SA_PASSWORD', 'KNOX_PASSWORD'].
```bash
azdata bdc create [--name -n] 
                  [--config-profile -c]  
                  [--accept-eula -a]  
                  [--node-label -l]  
                  [--force -f]
```
### Examples

Guided BDC deployment experience - you will receive prompts for needed values.

```bash
azdata bdc create
```

BDC deployment with arguments.

```bash
azdata bdc create --accept-eula yes --config-profile aks-dev-test
```
BDC deployment with specified name instead of default name in the profile.
```bash
azdata bdc create --name <cluster_name> --accept-eula yes --config-profile aks-dev-test --force
```

BDC deployment with arguments - no prompts will be given as the --force flag is used.

```bash
azdata bdc create --accept-eula yes --config-profile aks-dev-test --force
```

### Optional Parameters
#### `--name -n`
Big data cluster name, used for kubernetes namespaces.
#### `--config-profile -c`
Big data cluster config profile, used for deploying the cluster: ['aks-dev-test', 'kubeadm-dev-test', 'minikube-dev-test']
#### `--accept-eula -a`
Do you accept the license terms? [yes/no]. If you do not want to use this arg, you may set the environment variable ACCEPT_EULA to 'yes'. The license terms for this product can be viewed at [https://go.microsoft.com/fwlink/?LinkId=2002534](https://go.microsoft.com/fwlink/?LinkId=2002534).
#### `--node-label -l`
Big data cluster node label, used to designate what nodes to deploy to.
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
## azdata bdc delete
Delete the SQL Server Big Data Cluster - kube config is required on your system along with the following environment variables ['CONTROLLER_USERNAME', 'CONTROLLER_PASSWORD'].
```bash
azdata bdc delete --name -n 
                  [--force -f]
```
### Examples
BDC deletion where the controller username and password are already set in your system environment.
```bash
azdata bdc delete --name <cluster_name>
```
### Required Parameters
#### `--name -n`
Big data cluster name, used for kubernetes namespace.
### Optional Parameters
#### `--force -f`
Force delete big data cluster.
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
