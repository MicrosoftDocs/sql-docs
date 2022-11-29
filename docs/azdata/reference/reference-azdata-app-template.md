---
title: azdata app template reference
titleSuffix: SQL Server Big Data Clusters
description: Reference article for azdata app template commands.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: seanw
ms.date: 10/05/2021
ms.service: sql
ms.subservice: big-data-cluster
ms.topic: reference
---

# azdata app template

Applies to [!INCLUDE [azure-data-cli-azdata](../../includes/azure-data-cli-azdata.md)]

The following article provides reference for the **sql** commands in the **azdata** tool. For more information about other **azdata** commands, see [azdata reference](reference-azdata.md)

## Commands

|Command|Description|
| --- | --- |
[azdata app template list](#azdata-app-template-list) | Fetch supported templates.
[azdata app template pull](#azdata-app-template-pull) | Download supported templates.
## azdata app template list
Fetch supported templates under the specified [URL] github repository.
```bash
azdata app template list [--url -u] 
                         
```
### Examples
Fetch all templates under the default template repository location.
```bash
azdata app template list
```
Fetch all templates under a different repository location.
```bash
azdata app template list --url https://github.com/diffrent/templates.git
```
### Optional Parameters
#### `--url -u`
Specify a different template repository location. Default: https://github.com/Microsoft/SQLBDC-AppDeploy.git
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
## azdata app template pull
Download supported templates under the specified [URL] github repository.
```bash
azdata app template pull [--name -n] 
                         [--url -u]  
                         
[--destination -d]
```
### Examples
Download all templates under the default template repository location.
```bash
azdata app template pull
```
Download all templates under a different repository location.
```bash
azdata app template list --url https://github.com/diffrent/templates.git
```
Download individual template by name.
```bash
azdata app template pull --name ssis            
```
### Optional Parameters
#### `--name -n`
Template name. For a full list off supported template names, run `azdata app template list`
#### `--url -u`
Specify a different template repository location. Default: https://github.com/Microsoft/SQLBDC-AppDeploy.git
#### `--destination -d`
Where to place the application skeleton template.
`./templates`
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