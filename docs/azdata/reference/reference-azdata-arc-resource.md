---
title: azdata arc resource reference
titleSuffix: SQL Server big data clusters
description: Reference article for azdata arc resource commands.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: seanw
ms.date: 06/22/2020
ms.topic: reference
ms.prod: sql
ms.technology: big-data-cluster
---

# azdata arc resource

Applies to `azdata`

The following article provides reference for the **sql** commands in the **azdata** tool. For more information about other **azdata** commands, see [azdata reference](reference-azdata.md)

## Commands

|Command|Description|
| --- | --- |
[azdata arc resource-kind list](#azdata-arc-resource-kind-list) | Lists the available custom resource kinds for Arc that can be defined and created.
[azdata arc resource-kind get](#azdata-arc-resource-kind-get) | Gets the Arc resource-kind's template file.
## azdata arc resource-kind list
Lists the available custom resource kinds for Arc that can be defined and created. After listing, you can proceed to getting the template file needed to define or create that custom resource.
```bash
azdata arc resource-kind list 
```
### Examples
Example command for listing the available custom resource kinds for Arc.
```bash
azdata arc resource-kind list
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
## azdata arc resource-kind get
Gets the Arc resource-kind's template file.
```bash
azdata arc resource-kind get --kind -k 
                             [--dest -d]
```
### Examples
Example command for getting an Arc resource-kind's CRD template file.
```bash
azdata arc resource-kind get --kind sqldb
```
### Required Parameters
#### `--kind -k`
The kind of arc resource you want the template file for.
### Optional Parameters
#### `--dest -d`
The directory where you"d like to place the template files.
`template`
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

For more information about how to install the **azdata** tool, see [Install azdata](deploy-install-azdata.md).
