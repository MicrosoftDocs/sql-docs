---
title: azdata extension reference
titleSuffix: SQL Server Big Data Clusters
description: Reference article for azdata extension commands.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: seanw
ms.date: 10/05/2021
ms.service: sql
ms.subservice: big-data-cluster
ms.topic: reference
---

# azdata extension

Applies to [!INCLUDE [azure-data-cli-azdata](../../includes/azure-data-cli-azdata.md)]

The following article provides reference for the **sql** commands in the **azdata** tool. For more information about other **azdata** commands, see [azdata reference](reference-azdata.md)

## Commands

|Command|Description|
| --- | --- |
[azdata extension add](#azdata-extension-add) | Add an extension.
[azdata extension remove](#azdata-extension-remove) | Remove an extension.
[azdata extension list](#azdata-extension-list) | List all installed extensions.
## azdata extension add
Add an extension.
```bash
azdata extension add --source -s 
                     [--index]  
                     
[--pip-proxy]  
                     
[--pip-extra-index-urls]  
                     
[--yes -y]
```
### Examples
Add extension from URL.
```bash
azdata extension add --source https://contoso.com/some_ext-0.0.1-py2.py3-none-any.whl
```
Add extension from local disk.
```bash
azdata extension add --source ~/some_ext-0.0.1-py2.py3-none-any.whl
```
Add extension from local disk and use pip proxy for dependencies.
```bash
azdata extension add --source ~/some_ext-0.0.1-py2.py3-none-any.whl --pip-proxy https://user:pass@proxy.server:8080
```
### Required Parameters
#### `--source -s`
Path to a extension wheel on disk or URL to an extension
### Optional Parameters
#### `--index`
Base URL of the Python Package Index (default https://pypi.org/simple). This should point to a repository compliant with PEP 503 (the simple repository API) or a local directory laid out in the same format.
#### `--pip-proxy`
Proxy for pip to use for extension dependencies in the form of [user:passwd@]proxy.server:port
#### `--pip-extra-index-urls`
Space-separated list of extra URLs of package indexes to use. This should point to a repository compliant with PEP 503 (the simple repository API) or a local directory laid out in the same format.
#### `--yes -y`
Do not prompt for confirmation.
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
## azdata extension remove
Remove an extension.
```bash
azdata extension remove --name -n 
                        [--yes -y]
```
### Examples
Remove an extension.
```bash
azdata extension remove --name some-ext
```
### Required Parameters
#### `--name -n`
Name of the extension
### Optional Parameters
#### `--yes -y`
Do not prompt for confirmation.
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
## azdata extension list
List all installed extensions.
```bash
azdata extension list 
```
### Examples
List extensions.
```bash
azdata extension list
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