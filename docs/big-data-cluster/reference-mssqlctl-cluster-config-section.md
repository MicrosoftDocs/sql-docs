---
title: mssqlctl cluster config section reference
titleSuffix: SQL Server big data clusters
description: Reference article for mssqlctl cluster config section commands.
author: rothja
ms.author: jroth
manager: craigg
ms.date: 04/24/2019
ms.topic: reference
ms.prod: sql
ms.technology: big-data-cluster
---

# mssqlctl cluster config section

[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

The following article provides reference for the **cluster config section** commands in the **mssqlctl** tool. For more information about other **mssqlctl** commands, see [mssqlctl reference](reference-mssqlctl.md).

## Commands
|     |     |
| --- | --- |
[mssqlctl cluster config section get](#mssqlctl-cluster-config-section-get) | Gets a section from a config file.
[mssqlctl cluster config section set](#mssqlctl-cluster-config-section-set) | Sets a section for a config file.
## mssqlctl cluster config section get
Gets the specified section from the selected config file according to the given json path.
```bash
mssqlctl cluster config section get --json-path -j 
                                    --config-file -f  
                                    [--target -t]
```
### Required Parameters
#### `--json-path -j`
The json key path that leads to the section or value you want from the config file, i.e. key1.key2.key3. Uses the jsonpath query language, https://github.com/h2non/jsonpath-ng, for example: -j '$.spec.pools[?(@.spec.type == "Master")]..endpoints'
#### `--config-file -f`
Cluster config file path.
### Optional Parameters
#### `--target -t`
File path of where you would like the section file placed.
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
## mssqlctl cluster config section set
Sets the specified section in the selected config file according to the given json path.
```bash
mssqlctl cluster config section set --config-file -f 
                                    [--json-values -j]  
                                    [--patch-file -p]
```
### Required Parameters
#### `--config-file -f`
Cluster config file path of the config you would like to set
### Optional Parameters
#### `--json-values -j`
A key value pair list of json paths to values: key1.subkey1=value1,key2.subkey2=value2. You may provide inline json values such as: key='{"kind":"cluster","name":"test-cluster"}' or provide a file path, such as key=./values.json
#### `--patch-file -p`
Path to a patch json file that is based off the jsonpatch library and jsonpath: https://github.com/stefankoegl/python-json-patch , https://github.com/h2non/jsonpath-ng - A simple example: {"patch": [{"op": "add", "path": "metadata.name", "value": "test"},{"op": "add", "path": "metadata.array", "value": [1, 2, 3]}]} Please include the key "patch" and have the value be an array of patches you intend to make.  It will be executed as such. A more complex example: {"patch": [{"op": "replace", "path": "$.spec.pools[?(@.spec.type == 'Master')]..endpoints","value": {"name": "New Endpoint"}}]}
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