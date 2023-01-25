---
title: azdata context reference
titleSuffix: SQL Server Big Data Clusters
description: Reference article for azdata context commands.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: seanw
ms.date: 10/05/2021
ms.service: sql
ms.subservice: big-data-cluster
ms.topic: reference
---

# azdata context

Applies to [!INCLUDE [azure-data-cli-azdata](../../includes/azure-data-cli-azdata.md)]

The following article provides reference for the **sql** commands in the **azdata** tool. For more information about other **azdata** commands, see [azdata reference](reference-azdata.md)

## Commands

|Command|Description|
| --- | --- |
[azdata context list](#azdata-context-list) | Lists the available contexts in the user profile.
[azdata context delete](#azdata-context-delete) | Deletes the context with the given namespace from the user profile.
[azdata context set](#azdata-context-set) | Sets the context with the given namespace as the active context in the user profile.
## azdata context list
You may set or delete any of these with `azdata context set` or `azdata context delete`. To login to a new context, use `azdata login`.
```bash
azdata context list [--active -a] 
                    
```
### Examples
Lists all available contexts in the user profile.
```bash
azdata context list
```
Lists the active context in the user profile.
```bash
azdata context list --active
```
### Optional Parameters
#### `--active -a`
List only the currently active context.
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
## azdata context delete
If the deleted context is active, the user will need to set a new active context. To see contexts available to set or delete `azdata context list`. When a namespace is provided that matches multiple context namespaces, you must provide all [--namespace --username --endpoint] for uniqueness in order to delete that context.
```bash
azdata context delete --namespace -ns 
                      [--endpoint -e]  
                      
[--username -u]
```
### Examples
Deletes the context in the user profile based on a unique namespace.
```bash
azdata context delete --namespace contextNamespace
```
Deletes the context in the user profile based on the namespace, username, and controller endpoint.
```bash
azdata context set --namespace contextNamespace --username johndoe --endpoint https://<ip or domain name>:30080
```
### Required Parameters
#### `--namespace -ns`
Namespace of the context which you'd like to delete.
### Optional Parameters
#### `--endpoint -e`
Cluster controller endpoint "https://host:port".
#### `--username -u`
Account user.
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
## azdata context set
To see contexts available to set `azdata context list`. If no contexts are listed, you need to login in order to create a context in your user profile `azdata login`. What you login to will become your active context. If you login to multiple entities, you can then switch between active contexts with this command. To see your currently active context `azdata context list --active`. When a namespace is provided that matches multiple context namespaces, you must provide all [--namespace --username --endpoint] for uniqueness in order to set the active context.
```bash
azdata context set --namespace -ns 
                   [--endpoint -e]  
                   
[--username -u]
```
### Examples
Sets the active context in the user profile based on a unique namespace.
```bash
azdata context set --namespace contextNamespace
```
Sets the active context in the user profile based on the namespace, username, and controller endpoint.
```bash
azdata context set --namespace contextNamespace --username johndoe --endpoint https://<ip or domain name>:30080     
```
### Required Parameters
#### `--namespace -ns`
Namespace of the context which you'd like to set.
### Optional Parameters
#### `--endpoint -e`
Cluster controller endpoint "https://host:port".
#### `--username -u`
Account user.
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