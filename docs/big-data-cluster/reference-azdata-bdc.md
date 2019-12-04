---
title: azdata bdc reference
titleSuffix: SQL Server big data clusters
description: Reference article for azdata bdc commands.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: mihaelab
ms.date: 11/04/2019
ms.topic: reference
ms.prod: sql
ms.technology: big-data-cluster
---

# azdata bdc

[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]  

The following article provides reference for the `bdc` commands in the `azdata` tool. For more information about other `azdata` commands, see [azdata reference](reference-azdata.md)

## Commands
|     |     |
| --- | --- |
[azdata bdc create](#azdata-bdc-create) | Create Big Data Cluster.
[azdata bdc delete](#azdata-bdc-delete) | Delete Big Data Cluster.
[azdata bdc upgrade](#azdata-bdc-upgrade) | Update the images deployed in each container in the SQL Server Big Data Cluster.
[azdata bdc config](reference-azdata-bdc-config.md) | Configuration commands.
[azdata bdc endpoint](reference-azdata-bdc-endpoint.md) | Endpoint commands.
[azdata bdc debug](reference-azdata-bdc-debug.md) | Debug commands.
[azdata bdc status](reference-azdata-bdc-status.md) | BDC status commands.
[azdata bdc control](reference-azdata-bdc-control.md) | Control service commands.
[azdata bdc sql](reference-azdata-bdc-sql.md) | Sql service commands.
[azdata bdc hdfs](reference-azdata-bdc-hdfs.md) | Hdfs service commands.
[azdata bdc spark](reference-azdata-bdc-spark.md) | Spark service commands.
[azdata bdc gateway](reference-azdata-bdc-gateway.md) | Gateway service commands.
[azdata bdc app](reference-azdata-bdc-app.md) | App service commands.
[azdata bdc spark](reference-azdata-bdc-spark.md) | The Spark commands allow the user to interact with the Spark system by creating and managing sessions, statements, and batches.
[azdata bdc hdfs](reference-azdata-bdc-hdfs.md) | The HDFS module provides commands to access an HDFS file system.
## azdata bdc create
Create a SQL Server Big Data Cluster - Kubernetes configuration is required on your system along with the following environment variables ['AZDATA_USERNAME', 'AZDATA_PASSWORD'].
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
Big data cluster config profile, used for deploying the cluster: ['kubeadm-dev-test', 'kubeadm-prod', 'aks-dev-test', 'aks-dev-test-ha']
#### `--accept-eula -a`
Do you accept the license terms? [yes/no]. If you do not want to use this arg, you may set the environment variable ACCEPT_EULA to 'yes'. The license terms for azdata can be viewed at https://aka.ms/eula-azdata-en and the license terms for big data cluster can be seen at - Enterprise: https://go.microsoft.com/fwlink/?linkid=2104292,Standard: https://go.microsoft.com/fwlink/?linkid=2104294,Developer: https://go.microsoft.com/fwlink/?linkid=2104079.
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
JMESPath query string. See [http://jmespath.org/](http://jmespath.org/) for more information and examples.
#### `--verbose`
Increase logging verbosity. Use --debug for full debug logs.
## azdata bdc delete
Delete the SQL Server Big Data Cluster - Kubernetes configuration is required on your system.
```bash
azdata bdc delete --name -n 
                  [--force -f]
```
### Examples
BDC delete.
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
JMESPath query string. See [http://jmespath.org/](http://jmespath.org/) for more information and examples.
#### `--verbose`
Increase logging verbosity. Use --debug for full debug logs.
## azdata bdc upgrade
Update the images deployed in each container in the SQL Server Big Data Cluster. The images updated are based on the docker image passed in. If the updated images are from a different docker image repository than the currently deployed images, then the "repository" parameter is also required.
```bash
azdata bdc upgrade --name -n 
                   --tag -t  
                   [--repository -r]
```
### Examples
BDC upgrade to a new image tag "cu2" from the same repository.
```bash
azdata bdc upgrade -t cu2
```
BDC upgrade to a new images with tag "cu2" from a new repository "foo/bar/baz".
```bash
azdata bdc upgrade -t cu2 -r foo/bar/baz
```
### Required Parameters
#### `--name -n`
Big data cluster name, used for kubernetes namespaces.
#### `--tag -t`
The target docker image tag to upgrade all container in the cluster to.
### Optional Parameters
#### `--repository -r`
The docker repository to have all containers in the cluster pull their images from.
### Global Arguments
#### `--debug`
Increase logging verbosity to show all debug logs.
#### `--help -h`
Show this help message and exit.
#### `--output -o`
Output format.  Allowed values: json, jsonc, table, tsv.  Default: json.
#### `--query -q`
JMESPath query string. See [http://jmespath.org/](http://jmespath.org/) for more information and examples.
#### `--verbose`
Increase logging verbosity. Use --debug for full debug logs.

## Next steps

For more information about other `azdata` commands, see [azdata reference](reference-azdata.md). For more information about how to install the `azdata` tool, see [Install azdata to manage SQL Server 2019 big data clusters](deploy-install-azdata.md).
