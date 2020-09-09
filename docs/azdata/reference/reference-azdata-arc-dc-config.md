---
title: azdata arc dc config reference
titleSuffix: SQL Server big data clusters
description: Reference article for azdata arc dc config commands.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: seanw
ms.date: 06/22/2020
ms.topic: reference
ms.prod: sql
ms.technology: big-data-cluster
---

# azdata arc dc config

Applies to `azdata`

The following article provides reference for the **sql** commands in the **azdata** tool. For more information about other **azdata** commands, see [azdata reference](reference-azdata.md)

## Commands

|Command|Description|
| --- | --- |
[azdata arc dc config init](#azdata-arc-dc-config-init) | Initializes a data controller configuration profile that can be used with control create.
[azdata arc dc config list](#azdata-arc-dc-config-list) | Lists available configuration profile choices.
[azdata arc dc config add](#azdata-arc-dc-config-add) | Add a value for a json path in a config file.
[azdata arc dc config remove](#azdata-arc-dc-config-remove) | Remove a value for a json path in a config file.
[azdata arc dc config replace](#azdata-arc-dc-config-replace) | Replace a value for a json path in a config file.
[azdata arc dc config patch](#azdata-arc-dc-config-patch) | Patches a config file based on a json patch file.
## azdata arc dc config init
Initializes a data controller configuration  profile that can be used with control create. The specific source of the configuration profile can be specified in the arguments.
```bash
azdata arc dc config init [--path -p] 
                          [--source -s]  
                          
[--force -f]
```
### Examples
Guided data controller config init experience - you will receive prompts for needed values.
```bash
azdata arc dc config init
```
arc dc config init with arguments, creates a configuration profile of aks-dev-test in ./custom.
```bash
azdata arc dc config init --source aks-dev-test --path custom
```
### Optional Parameters
#### `--path -p`
File path of where you would like the config profile placed, defaults to <cwd>/custom.
#### `--source -s`
Config profile source: ['azure-arc-aks-default-storage', 'azure-arc-eks', 'azure-arc-aks-premium-storage', 'azure-arc-azure-openshift', 'azure-arc-aks-dev-test', 'azure-arc-kubeadm', 'azure-arc-openshift', 'azure-arc-kubeadm-dev-test', 'azure-arc-ake']
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
JMESPath query string. See [http://jmespath.org/](http://jmespath.org) for more information and examples.
#### `--verbose`
Increase logging verbosity. Use --debug for full debug logs.
## azdata arc dc config list
Lists available configuration profile choices for use in `arc dc config init`
```bash
azdata arc dc config list [--config-profile -c] 
                          
```
### Examples
Shows all available configuration profile names.
```bash
azdata arc dc config list
```
Shows json of a specific configuration profile.
```bash
azdata arc dc config list --config-profile aks-dev-test
```
### Optional Parameters
#### `--config-profile -c`
Default config profile: ['azure-arc-aks-default-storage', 'azure-arc-eks', 'azure-arc-aks-premium-storage', 'azure-arc-azure-openshift', 'azure-arc-aks-dev-test', 'azure-arc-kubeadm', 'azure-arc-openshift', 'azure-arc-kubeadm-dev-test', 'azure-arc-ake']
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
## azdata arc dc config add
Adds the value at the json path in the config file.  All examples below are given in Bash.  If using another command line, please be aware that you may need to escapequotations appropriately.  Alternatively, you may use the patch file functionality.
```bash
azdata arc dc config add --path -p 
                         --json-values -j
```
### Examples
Ex 1 - Add data controller storage.
```bash
azdata arc dc config add --path custom/control.json --json-values "spec.storage={"accessMode":"ReadWriteOnce","className":"managed-premium","size":"10Gi"}"
```
### Required Parameters
#### `--path -p`
Data controller config file path of the config you would like to set, i.e. custom/control.json
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
## azdata arc dc config remove
Removes the value at the json path in the config file.  All examples below are given in Bash.  If using another command line, please be aware that you may need to escapequotations appropriately.  Alternatively, you may use the patch file functionality.
```bash
azdata arc dc config remove --path -p 
                            --json-path -j
```
### Examples
Ex 1 - Remove data controller storage.
```bash
azdata arc dc config remove --path custom/control.json --json-path ".spec.storage"
```
### Required Parameters
#### `--path -p`
Data controller config file path of the config you would like to set, i.e. custom/control.json
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
## azdata arc dc config replace
Replaces the value at the json path in the config file.  All examplesbelow are given in Bash.  If using another command line, please be aware that you may need to escapequotations appropriately.  Alternatively, you may use the patch file functionality.
```bash
azdata arc dc config replace --path -p 
                             --json-values -j
```
### Examples
Ex 1 - Replace the port of a single endpoint (Data Controller Endpoint).
```bash
azdata arc dc config replace --path custom/control.json --json-values "$.spec.endpoints[?(@.name=="Controller")].port=30080"
```
Ex 2 - Replace data controller storage.
```bash
azdata arc dc config replace --path custom/control.json --json-values "spec.storage={"accessMode":"ReadWriteOnce","className":"managed-premium","size":"10Gi"}"
```
### Required Parameters
#### `--path -p`
Data controller config file path of the config you would like to set, i.e. custom/control.json
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
## azdata arc dc config patch
Patches the config file according to the given patch file. Please consult http://jsonpatch.com/ for a better understanding of how the paths should be composed. The replace operation can use conditionals in its path due to the jsonpath library https://jsonpath.com/. All patch json files must start with a key of "patch" that has an array of patches with their corresponding op (add, replace, remove), path, and value. The "remove" op does not require a value, just a path. Please see the examples below.
```bash
azdata arc dc config patch --path 
                           --patch-file -p
```
### Examples
Ex 1 - Replace the port of a single endpoint (Data Controller Endpoint) with patch file.
```bash
azdata arc dc config patch --path custom/control.json --patch ./patch.json

    Patch File Example (patch.json):
        {"patch":[{"op":"replace","path":"$.spec.endpoints[?(@.name=="Controller")].port","value":30080}]}
```
Ex 2 - Replace data controller storage with patch file.
```bash
azdata arc dc config patch --path custom/control.json --patch ./patch.json

    Patch File Example (patch.json):
        {"patch":[{"op":"replace","path":".spec.storage","value":{"accessMode":"ReadWriteMany","className":"managed-premium","size":"10Gi"}}]}
```
### Required Parameters
#### `--path`
Data controller config file path of the config you would like to set, i.e. custom/control.json
#### `--patch-file -p`
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

For more information about how to install the **azdata** tool, see [Install azdata](..\install\deploy-install-azdata.md).
