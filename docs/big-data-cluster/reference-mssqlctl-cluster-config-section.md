---
title: mssqlctl cluster config section reference
titleSuffix: SQL Server big data clusters
description: Reference article for mssqlctl cluster config section commands.
author: rothja
ms.author: jroth
manager: craigg
ms.date: 05/22/2019
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
[mssqlctl cluster config section show](#mssqlctl-cluster-config-section-show) | Gets a section from a config file.
[mssqlctl cluster config section set](#mssqlctl-cluster-config-section-set) | Sets a section for a config file.
## mssqlctl cluster config section show
Gets the specified section from the selected config file according to the given json path.
```bash
mssqlctl cluster config section show --json-path -j 
                                     --config-file -c  
                                     [--target -t]  
                                     [--force -f]
```
### Examples
Gets a value at the end of a simple json key path.
```bash
mssqlctl cluster config section show --config-file custom-config.json --json-path 'metadata.name' --target section.json
```
Gets a value at the end of a json key path with a conditional
```bash
mssqlctl cluster config section show --config-file custom-config.json  --json-path '$.spec.pools[?(@.spec.type=="Storage")].spec' --target section.json
```
### Required Parameters
#### `--json-path -j`
The json key path that leads to the section or value you want from the config file, i.e. key1.key2.key3. Uses the jsonpath query language, https://github.com/h2non/jsonpath-ng, for example: -j '$.spec.pools[?(@.spec.type == "Master")]..endpoints'
#### `--config-file -c`
Cluster config file path.
### Optional Parameters
#### `--target -t`
File path of where you would like the section file placed. Default: directed to stdout.
#### `--force -f`
Force overwrite of the target file.
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
Sets the specified section in the selected config file according to the given json path.  All examplesbelow are given in Bash.  If using another command line, please be aware that you may need to escapequotations appropriately.  Alternatively, you may use the patch file functionality.
```bash
mssqlctl cluster config section set --config-file -c 
                                    [--json-values -j]  
                                    [--patch-file -p]
```
### Examples
Ex 1 (inline) - Set the port of a single endpoint (Controller Endpoint).
```bash
mssqlctl cluster config section set --config-file custom-config.json --json-values '$.spec.controlPlane.spec.endpoints[?(@.name=="Controller")].port=30080'
```
Ex 1 (patch) - Set the port of a single endpoint (Controller Endpoint) with patch file.
```bash
mssqlctl cluster config section set --config-file custom-config.json --patch ./patch.json

    Patch File Example (patch.json): 
        {"patch":[{"op":"replace","path":"$.spec.controlPlane.spec.endpoints[?(@.name=='Controller')].port","value":30080}]}
```
Ex 2 (inline) - Set control plane storage.
```bash
mssqlctl cluster config section set --config-file custom-config.json --json-values 'spec.controlPlane.spec.storage=spec.controlPlane.spec.storage={"accessMode":"ReadWriteOnce","className":"managed-premium","size":"10Gi"}'
```
Ex 2 (patch) - Set control plane storage with patch file.
```bash
mssqlctl cluster config section set --config-file custom-config.json --patch ./patch.json

    Patch File Example (patch.json): 
        {"patch":[{"op":"replace","path":"spec.controlPlane.spec.storage","value":{"accessMode":"ReadWriteMany","className":"managed-premium","size":"10Gi"}}]}
```
Ex 3(inline) - Set pool storage, including replicas (Storage Pool).
```bash
mssqlctl cluster config section set --config-file custom-config.json --json-values '$.spec.pools[?(@.spec.type == "Storage")].spec={"replicas": 2,"storage": {"className": "managed-premium","size": "10Gi","accessMode": "ReadWriteOnce"},"type": "Storage"}'
```
Ex 3 (patch) - Set pool storage, including replicas (Storage Pool) with patch file.
```bash
mssqlctl cluster config section set --config-file custom-config.json --patch ./patch.json

    Patch File Example (patch.json): 
        {"patch":[{"op":"replace","path":"$.spec.pools[?(@.spec.type == 'Storage')].spec","value":{"replicas": 2,"storage": {"className": "managed-premium","size": "10Gi","accessMode": "ReadWriteOnce"},"type": "Storage"}}]}
```
### Required Parameters
#### `--config-file -c`
Cluster config file path of the config you would like to set
### Optional Parameters
#### `--json-values -j`
A key value pair list of json paths to values: key1.subkey1=value1,key2.subkey2=value2. You may provide inline json values such as: key='{"kind":"cluster","name":"test-cluster"}' or provide a file path, such as key=./values.json. If you would like to set a value that requires a conditional, please use the jsonpath notation by beginning your path with a $. This will allow you to do a conditional such as -j $.key1.key2[?(@.key3=='someValue'].key4=value. You may see examples below. For additional help, please see: https://jsonpath.com/
#### `--patch-file -p`
Path to a patch json file that is based off the jsonpatch library: http://jsonpatch.com/. You must start your patch json file with a key called "patch", whose value is an array of patch operations you intend to make. For the path of a patch operation, you may use dot notation, such as key1.key2 for most operations. If you would like to do a replace operation, and you are replacing a value in an array that requires a conditional, please use the jsonpath notation by beginning your path with a $. This will allow you to do a conditional such as $.key1.key2[?(@.key3=='someValue'].key4. Please see the examples below. For additional help, please see: https://jsonpath.com/.
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