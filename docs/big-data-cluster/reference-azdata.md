---
title: azdata reference
titleSuffix: SQL Server big data clusters
description: Reference article for azdata commands.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: mihaelab
ms.date: 08/28/2019
ms.topic: reference
ms.prod: sql
ms.technology: big-data-cluster
---

# azdata

[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]  

## Commands
|     |     |
| --- | --- |
|[azdata notebook](reference-azdata-notebook.md) | Commands for viewing, running, and managing notebooks from a terminal. |
|[azdata sql](reference-azdata-sql.md) | The SQL DB CLI allows the user to interact with SQL Server via T-SQL. |
|[azdata app](reference-azdata-app.md) | Create, delete, run, and manage applications. |
|[azdata bdc](reference-azdata-bdc.md) | Select, manage, and operate SQL Server Big Data Clusters. |
|[azdata control](reference-azdata-control.md) | Create, delete, and manage control planes. |
[azdata login](#azdata-login) | Log in to the cluster's controller endpoint.
[azdata logout](#azdata-logout) | Log out of cluster.
## azdata login
When your cluster is deployed, it will list the controller endpoint during deployment, which you should use to login.  If you do not know the controller endpoint, you may login by having your cluster's kube config on your system in the default location of <user home>/.kube/config or use the KUBECONFIG env var, i.e. export KUBECONFIG=path/to/.kube/config.
```bash
azdata login [--cluster-name -n] 
             [--controller-username -u]  
             [--controller-endpoint -e]  
             [--accept-eula -a]
```
### Examples
Log in interactively. Cluster name will always be prompted for if not specified as an argument. If you have the CONTROLLER_USERNAME, CONTROLLER_PASSWORD, and ACCEPT_EULA env variables set on your system, these will not be prompted for. If you have the kube config on your system or are using the KUBECONFIG env var to specify the path to the config, the interactive experience will first try to use the config and then prompt you if the config fails.
```bash
azdata login
```
Log in (non-interactively). Log in with cluster name, controller user name, controller endpoint, and EULA acceptance set as arguments. The environment variable CONTROLLER_PASSWORD must be set.  If you do not want to specify the controller endpoint, please have the kube config on your machine in the default location of <user home>/.kube/config or use the KUBECONFIG env var, i.e. export KUBECONFIG=path/to/.kube/config.
```bash
azdata login --cluster-name ClusterName --controller-user johndoe@contoso.com  --controller-endpoint https://<ip>:30080 --accept-eula yes
```
Log in with kube config on machine, and env var set for CONTROLLER_USERNAME, CONTROLLER_PASSWORD, and ACCEPT_EULA.
```bash
azdata login -n ClusterName
```
### Optional Parameters
#### `--cluster-name -n`
Cluster name.
#### `--controller-username -u`
Account user. If you do not want to use this arg, you may set the environment variable CONTROLLER_USERNAME.
#### `--controller-endpoint -e`
Cluster controller endpoint "https://host:port". If you do not want to use this arg, you may use the kube config on your machine. Please ensure the config is located at the default location of <user home>/.kube/config or use the KUBECONFIG env var.
#### `--accept-eula -a`
Do you accept the license terms? [yes/no]. If you do not want to use this arg, you may set the environment variable ACCEPT_EULA to 'yes'. 
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
## azdata logout
Log out of cluster.
```bash
azdata logout 
```
### Examples
Log out this user.
```bash
azdata logout
```
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

- For more information about how to install the **azdata** tool, see [Install azdata to manage SQL Server 2019 big data clusters](deploy-install-azdata.md).
