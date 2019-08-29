---
title: azdata control reference
titleSuffix: SQL Server big data clusters
description: Reference article for azdata control commands.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: mihaelab
ms.date: 08/28/2019
ms.topic: reference
ms.prod: sql
ms.technology: big-data-cluster
---

# azdata control

[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]  

This article is a reference article for **azdata**. 

## Commands
|     |     |
| --- | --- |
[azdata control create](#azdata-control-create) | Create control plane.
[azdata control delete](#azdata-control-delete) | Delete control plane.
## azdata control create
Create control plane - kube config is required on your system along with the following environment variables ['CONTROLLER_USERNAME', 'CONTROLLER_PASSWORD', 'MSSQL_SA_PASSWORD', 'KNOX_PASSWORD'].
```bash
azdata control create [--name -n] 
                      [--config-profile -c]  
                      [--accept-eula -a]  
                      [--node-label -l]  
                      [--force -f]
```
### Examples
Control deployment.
```bash
azdata control create
```
### Optional Parameters
#### `--name -n`
Control plane name, used for kubernetes namespaces.
#### `--config-profile -c`
Cluster config profile, used for deploying the cluster: ['aks-dev-test', 'kubeadm-prod', 'minikube-dev-test', 'kubeadm-dev-test']
#### `--accept-eula -a`
Do you accept the license terms? [yes/no]. If you do not want to use this arg, you may set the environment variable ACCEPT_EULA to 'yes'. The license terms for this product can be viewed at https://aka.ms/azdata-eula.
#### `--node-label -l`
Node label, used to designate what nodes to deploy to.
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
## azdata control delete
Delete control plane - kube config is required on your system.
```bash
azdata control delete --name -n 
                      [--force -f]
```
### Examples
Control deployment.
```bash
azdata control delete
```
### Required Parameters
#### `--name -n`
Control plane name, used for kubernetes namespace.
### Optional Parameters
#### `--force -f`
Force delete control plane.
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

- For more information about other **azdata** commands, see [azdata reference](reference-azdata.md). 

- For more information about how to install the **azdata** tool, see [Install azdata to manage SQL Server 2019 big data clusters](deploy-install-azdata.md).
