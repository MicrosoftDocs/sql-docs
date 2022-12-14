---
title: azdata reference
titleSuffix: SQL Server Big Data Clusters
description: Reference article for azdata commands.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: seanw
ms.date: 10/05/2021
ms.service: sql
ms.subservice: big-data-cluster
ms.topic: reference
---

# azdata

Applies to [!INCLUDE [azure-data-cli-azdata](../../includes/azure-data-cli-azdata.md)]

The following article provides reference for the **sql** commands in the **azdata** tool. For more information about other **azdata** commands, see [azdata reference](reference-azdata.md).

## Commands

|Command|Description|
| --- | --- |
|[azdata arc](/azure/azure-arc/data/reference/overview) | Commands for using Azure Arc for Azure data services. |
|[azdata sql](reference-azdata-sql.md) | The SQL DB CLI allows the user to interact with SQL Server via T-SQL. |
[azdata login](#azdata-login) | Log in to the cluster's controller endpoint and set its namespace as your active context. To use a password on login, you must set the AZDATA_PASSWORD environment variable.
[azdata logout](#azdata-logout) | Log out of cluster.
|[azdata context](reference-azdata-context.md) | Context management commands. |
|[azdata bdc](reference-azdata-bdc.md) | Select, manage, and operate SQL Server Big Data Clusters. |
|[azdata extension](reference-azdata-extension.md) | Manage and update CLI extensions. |
|[azdata app](reference-azdata-app.md) | Create, delete, run, and manage applications. |
|[azdata postgres](reference-azdata-postgres.md) | Postgres query runner and interactive shell. |
|[azdata notebook](reference-azdata-notebook.md) | Commands for viewing, running, and managing notebooks from a terminal. |
## azdata login
When your cluster is deployed, it will list the controller endpoint during deployment, which you should use to login.  If you do not know the controller endpoint, you may login by having your cluster's kube config on your system in the default location of \<user home\>/.kube/config or use the KUBECONFIG env var, i.e. export KUBECONFIG=path/to/.kube/config.  When you login, this cluster's namespace will be set to your active context.
```bash
azdata login [--auth] 
             [--endpoint -e]  
             
[--accept-eula -a]  
             
[--namespace -ns]  
             
[--username -u]  
             
[--principal -p]
```
### Examples
Login using basic authentication.
```bash
azdata login --auth basic --username johndoe --endpoint https://<ip or domain name>:30080
```
Login using active directory.
```bash
azdata login --auth ad --endpoint https://<ip or domain name>:30080                
```
Login using active directory with an explicit principal.
```bash
azdata login --auth ad --principal johndoe@COSTOSO.COM --endpoint https://<ip or domain name>:30080
```
Log in interactively. Cluster name will always be prompted for if not specified as an argument. If you have the AZDATA_USERNAME, AZDATA_PASSWORD, and ACCEPT_EULA env variables set on your system, these will not be prompted for. If you have the kube config on your system or are using the KUBECONFIG env var to specify the path to the config, the interactive experience will first try to use the config and then prompt you if the config fails.
```bash
azdata login
```
Log in (non-interactively). Log in with cluster name, controller user name, controller endpoint, and EULA acceptance set as arguments. The environment variable AZDATA_PASSWORD must be set.  If you do not want to specify the controller endpoint, please have the kube config on your machine in the default location of \<user home\>/.kube/config or use the KUBECONFIG env var, i.e. export KUBECONFIG=path/to/.kube/config.
```bash
azdata login --namespace ClusterName --username johndoe@contoso.com  --endpoint https://<ip or domain name>:30080 --accept-eula yes
```
Log in with kube config on machine, and env var set for AZDATA_USERNAME, AZDATA_PASSWORD, and ACCEPT_EULA.
```bash
azdata login -n ClusterName
```
### Optional Parameters
#### `--auth`
The authentication strategy. Basic or Active Directory authentication. Default is "basic" authentication.
#### `--endpoint -e`
Cluster controller endpoint "https://host:port". If you do not want to use this arg, you may use the kube config on your machine. Please ensure the config is located at the default location of \<user home\>/.kube/config or use the KUBECONFIG env var.
#### `--accept-eula -a`
Do you accept the license terms? [yes/no]. If you do not want to use this arg, you may set the environment variable ACCEPT_EULA to 'yes'. The license terms for this product can be viewed at https://aka.ms/eula-azdata-en.
#### `--namespace -ns`
Namespace of the cluster control plane.
#### `--username -u`
Account user. If you do not want to use this arg, you may set the environment variable AZDATA_USERNAME.
#### `--principal -p`
Your Kerberos realm. In most cases, your Kerberos realm is your domain name, in upper-case letters.
### Global Arguments
#### `--debug`
Increase logging verbosity to show all debug logs.
#### `--help -h`
Show this help message and exit.
#### `--output -o`
Output format.  Allowed values: json, jsonc, table, tsv.  Default: json.
#### `--query -q`
JMESPath query string. See [http://jmespath.org/](http://jmespath.org) for more information and examples.
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
JMESPath query string. See [http://jmespath.org/](http://jmespath.org) for more information and examples.
#### `--verbose`
Increase logging verbosity. Use --debug for full debug logs.

## Next steps

For more information about other **azdata** commands, see [azdata reference](reference-azdata.md). 

For more information about how to install the **azdata** tool, see [Install azdata](..\install\deploy-install-azdata.md).