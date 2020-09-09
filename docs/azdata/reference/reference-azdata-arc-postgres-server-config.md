---
title: azdata arc postgres server config reference
titleSuffix: SQL Server big data clusters
description: Reference article for azdata arc postgres server config commands.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: seanw
ms.date: 06/22/2020
ms.topic: reference
ms.prod: sql
ms.technology: big-data-cluster
---

# azdata arc postgres server config

Applies to `azdata`

The following article provides reference for the **sql** commands in the **azdata** tool. For more information about other **azdata** commands, see [azdata reference](reference-azdata.md)

## Commands

|Command|Description|
| --- | --- |
[azdata arc postgres server config init](#azdata-arc-postgres-server-config-init) | Initializes the CRD and specification files for a PostgreSQL server group.
[azdata arc postgres server config add](#azdata-arc-postgres-server-config-add) | Add a value for a json path in a config file.
[azdata arc postgres server config remove](#azdata-arc-postgres-server-config-remove) | Remove a value for a json path in a config file.
[azdata arc postgres server config replace](#azdata-arc-postgres-server-config-replace) | Replace a value for a json path in a config file.
[azdata arc postgres server config patch](#azdata-arc-postgres-server-config-patch) | Patches a config file based on a json patch file.
## azdata arc postgres server config init
Initializes the CRD and specification files for a PostgreSQL server group.
```bash
azdata arc postgres server config init --path -p 
                                       [--engine-version -ev]
```
### Examples
Initializes the CRD and specification files for a PostgreSQL server group.
```bash
azdata arc postgres server config init --path ./template
```
### Required Parameters
#### `--path -p`
A path where the CRD and specification for the PostgreSQL server group should be written.
### Optional Parameters
#### `--engine-version -ev`
Must be 11 or 12. The default value is 12.
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
## azdata arc postgres server config add
Adds the value at the json path in the config file.  All examples below are given in Bash.  If using another command line, please be aware that you may need to escapequotations appropriately.  Alternatively, you may use the patch file functionality.
```bash
azdata arc postgres server config add --path -p 
                                      --json-values -j
```
### Examples
Ex 1 - Add storage.
```bash
azdata arc postgres server config add --path custom/spec.json --json-values "spec.storage={"accessMode":"ReadWriteOnce","className":"managed-premium","size":"10Gi"}"
```
### Required Parameters
#### `--path -p`
Path to the custom resource specification, i.e. custom/spec.json
#### `--json-values -j`
A key value pair list of json paths to values: key1.subkey1=value1,key2.subkey2=value2. You may provide inline json values such as: key='{"kind":"cluster","name":"test-cluster"}' or provide a file path, such as key=./values.json. Add does NOT support conditionals.  If the inline value you are providing is a key value pair itself with "=" and "," please escape those characters.  For example, key1="key2\=val2\,key3\=val3". Please see http://jsonpatch.com/ for examples of how your path should look.  If you would like to access an array, you must do so by indicating the index, such as key.0=value
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
## azdata arc postgres server config remove
Removes the value at the json path in the config file.  All examples below are given in Bash.  If using another command line, please be aware that you may need to escapequotations appropriately.  Alternatively, you may use the patch file functionality.
```bash
azdata arc postgres server config remove --path -p 
                                         --json-path -j
```
### Examples
Ex 1 - Remove storage.
```bash
azdata arc postgres server config remove --path custom/spec.json --json-path ".spec.storage"
```
### Required Parameters
#### `--path -p`
Path to the custom resource specification, i.e. custom/spec.json
#### `--json-path -j`
A list of json paths based on the jsonpatch library that indicates which values you would like removed, such as: key1.subkey1,key2.subkey2. Remove does NOT support conditionals. Please see http://jsonpatch.com/ for examples of how your path should look.  If you would like to access an array, you must do so by indicating the index, such as key.0=value
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
## azdata arc postgres server config replace
Replaces the value at the json path in the config file.  All examplesbelow are given in Bash.  If using another command line, please be aware that you may need to escapequotations appropriately.  Alternatively, you may use the patch file functionality.
```bash
azdata arc postgres server config replace --path -p 
                                          --json-values -j
```
### Examples
Ex 1 - Replace the port of a single endpoint.
```bash
azdata arc postgres server config replace --path custom/spec.json --json-values "$.spec.endpoints[?(@.name=="Controller")].port=30080"
```
Ex 2 - Replace storage.
```bash
azdata arc postgres server config replace --path custom/spec.json --json-values "spec.storage={"accessMode":"ReadWriteOnce","className":"managed-premium","size":"10Gi"}"
```
### Required Parameters
#### `--path -p`
Path to the custom resource specification, i.e. custom/spec.json
#### `--json-values -j`
A key value pair list of json paths to values: key1.subkey1=value1,key2.subkey2=value2. You may provide inline json values such as: key='{"kind":"cluster","name":"test-cluster"}' or provide a file path, such as key=./values.json. Replace supports conditionals through the jsonpath library.  To use this, start your path with a $. This will allow you to do a conditional such as -j $.key1.key2[?(@.key3=="someValue"].key4=value. If the inline value you are providing is a key value pair itself with "=" and "," please escape those characters.  For example, key1="key2\=val2\,key3\=val3". You may see examples below. For additional help, please see: https://jsonpath.com/
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
## azdata arc postgres server config patch
Patches the config file according to the given patch file. Please consult http://jsonpatch.com/ for a better understanding of how the paths should be composed. The replace operation can use conditionals in its path due to the jsonpath library https://jsonpath.com/. All patch json files must start with a key of "patch" that has an array of patches with their corresponding op (add, replace, remove), path, and value. The "remove" op does not require a value, just a path. Please see the examples below.
```bash
azdata arc postgres server config patch --path -p 
                                        --patch-file
```
### Examples
Ex 1 - Replace the port of a single endpoint with patch file.
```bash
azdata arc postgres server config patch --path custom/spec.json --patch ./patch.json

    Patch File Example (patch.json):
        {"patch":[{"op":"replace","path":"$.spec.endpoints[?(@.name=="Controller")].port","value":30080}]}
```
Ex 2 - Replace storage with patch file.
```bash
azdata arc postgres server config patch --path custom/spec.json --patch ./patch.json

    Patch File Example (patch.json):
        {"patch":[{"op":"replace","path":".spec.storage","value":{"accessMode":"ReadWriteMany","className":"managed-premium","size":"10Gi"}}]}
```
### Required Parameters
#### `--path -p`
Path to the custom resource specification, i.e. custom/spec.json
#### `--patch-file`
Path to a patch json file that is based off the jsonpatch library: http://jsonpatch.com/. You must start your patch json file with a key called "patch", whose value is an array of patch operations you intend to make. For the path of a patch operation, you may use dot notation, such as key1.key2 for most operations. If you would like to do a replace operation, and you are replacing a value in an array that requires a conditional, please use the jsonpath notation by beginning your path with a $. This will allow you to do a conditional such as $.key1.key2[?(@.key3=="someValue"].key4. Please see the examples below. For additional help with conditionals, please see: https://jsonpath.com/.
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
