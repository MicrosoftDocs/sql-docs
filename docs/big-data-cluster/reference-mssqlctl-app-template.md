---
title: mssqlctl app template reference
titleSuffix: SQL Server big data clusters
description: Reference article for mssqlctl app template commands.
author: rothja
ms.author: jroth
manager: craigg
ms.date: 04/23/2019
ms.topic: reference
ms.prod: sql
ms.technology: big-data-cluster
---

# mssqlctl app template

[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

The following article provides reference for the **app template** commands in the **mssqlctl** tool. For more information about other **mssqlctl** commands, see [mssqlctl reference](reference-mssqlctl.md).

## Commands
|     |     |
| --- | --- |
[mssqlctl app template list](#mssqlctl-app-template-list) | Fetch supported templates.
[mssqlctl app template pull](#mssqlctl-app-template-pull) | Download supported templates.
## mssqlctl app template list
Fetch supported templates under the specified [URL] github repository.
```bash
mssqlctl app template list [--url -u] 
                           
```
### Examples
Fetch all templates under the default template repository location.
```bash
mssqlctl app template list
```
Fetch all templates under a different repository location.
```bash
mssqlctl app template list --url https://github.com/diffrent/templates.git
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
JMESPath query string. See [http://jmespath.org/](http://jmespath.org/]) for more information and examples.
#### `--verbose`
Increase logging verbosity. Use --debug for full debug logs.
## mssqlctl app template pull
Download supported templates under the specified [URL] github repository.
```bash
mssqlctl app template pull [--name -n] 
                           [--url -u]  
                           [--destination -d]
```
### Examples
Download all templates under the default template repository location.
```bash
mssqlctl app template pull
```
Download all templates under a different repository location.
```bash
mssqlctl app template list --url https://github.com/diffrent/templates.git
```
Download individual template by name.
```bash
mssqlctl app template pull --name ssis            
```
### Optional Parameters
#### `--name -n`
Template name. For a full list off supported template namesrun `mssqlctl app template list`
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
JMESPath query string. See [http://jmespath.org/](http://jmespath.org/]) for more information and examples.
#### `--verbose`
Increase logging verbosity. Use --debug for full debug logs.

## Next steps

For more information about other **mssqlctl** commands, see [mssqlctl reference](reference-mssqlctl.md). For more information about how to install the **mssqlctl** tool, see [Install mssqlctl to manage SQL Server 2019 big data clusters](deploy-install-mssqlctl.md).