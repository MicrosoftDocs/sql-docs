---
title: azdata bdc config reference
titleSuffix: SQL Server Big Data Clusters
description: Reference article for azdata bdc config commands.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: seanw
ms.date: 10/05/2021
ms.service: sql
ms.subservice: big-data-cluster
ms.topic: reference
---

# azdata bdc config

Applies to [!INCLUDE [azure-data-cli-azdata](../../includes/azure-data-cli-azdata.md)]

The following article provides reference for the **sql** commands in the **azdata** tool. For more information about other **azdata** commands, see [azdata reference](reference-azdata.md)

## Commands

|Command|Description|
| --- | --- |
[azdata bdc config init](#azdata-bdc-config-init) | Initializes a Big Data Cluster configuration profile that can be used with bdc create.
[azdata bdc config list](#azdata-bdc-config-list) | Lists available configuration profile choices.
[azdata bdc config show](#azdata-bdc-config-show) | Shows the BDC's current config or the config of a local file you specify, i.e. custom/bdc.json.
[azdata bdc config add](#azdata-bdc-config-add) | Add a value for a json path in a config file.
[azdata bdc config remove](#azdata-bdc-config-remove) | Remove a value for a json path in a config file.
[azdata bdc config replace](#azdata-bdc-config-replace) | Replace a value for a json path in a config file.
[azdata bdc config patch](#azdata-bdc-config-patch) | Patches a config file based on a json patch file.
## azdata bdc config init
Initializes a Big Data Cluster configuration  profile that can be used with bdc create. The specific source of the configuration profile can be specified in the arguments.
```bash
azdata bdc config init [--path -p] 
                       [--source -s]  
                       
[--force -f]  
                       
[--accept-eula -a]
```
### Examples
Guided BDC config init experience - you will receive prompts for needed values.
```bash
azdata bdc config init
```
BDC config init with arguments, creates a configuration profile of aks-dev-test in ./custom.
```bash
azdata bdc config init --source aks-dev-test --target custom
```
### Optional Parameters
#### `--path -p`
File path of where you would like the config profile placed, defaults to \<cwd\>/custom.
#### `--source -s`
Config profile source: ['kubeadm-dev-test', 'kubeadm-prod', 'openshift-prod', 'aks-dev-test-ha', 'aks-dev-test', 'aro-dev-test', 'openshift-dev-test', 'aro-dev-test-ha']
#### `--force -f`
Force overwrite of the target file.
#### `--accept-eula -a`
Do you accept the license terms? [yes/no]. If you do not want to use this arg, you may set the environment variable ACCEPT_EULA to 'yes'. The license terms for this product can be viewed at https://aka.ms/eula-azdata-en.
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
## azdata bdc config list
Lists available configuration profile choices for use in `bdc config init`
```bash
azdata bdc config list [--config-profile -c] 
                       [--type -t]  
                       
[--accept-eula -a]
```
### Examples
Shows all available configuration profile names.
```bash
azdata bdc config list
```
Shows json of a specific configuration profile.
```bash
azdata bdc config list --config-profile aks-dev-test
```
### Optional Parameters
#### `--config-profile -c`
Default config profile: ['kubeadm-dev-test', 'kubeadm-prod', 'openshift-prod', 'aks-dev-test-ha', 'aks-dev-test', 'aro-dev-test', 'openshift-dev-test', 'aro-dev-test-ha']
#### `--type -t`
What config type you would like to see.
#### `--accept-eula -a`
Do you accept the license terms? [yes/no]. If you do not want to use this arg, you may set the environment variable ACCEPT_EULA to 'yes'. The license terms for this product can be viewed at https://aka.ms/eula-azdata-en.
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
## azdata bdc config show
Shows the BDC's current config or the config of a local file you specify, i.e. custom/bdc.json. The command can also take in a json path if you would like to only get a section.  You can also specify a target file to output to.  If a target file is not specified, it will just be output to the terminal.
```bash
azdata bdc config show [--config-file -c] 
                       [--target -t]  
                       
[--json-path -j]  
                       
[--force -f]
```
### Examples
Show the BDC config in your console
```bash
azdata bdc config show
```
In a local config file, get a value at the end of a simple json key path.
```bash
azdata bdc config show --config-file custom-config/bdc.json --json-path "metadata.name" --target section.json
```
In a local config file, gets the resources within a service
```bash
azdata bdc config show --config-file custom-config/bdc.json --json-path "$.spec.services.sql.resources" --target section.json
```
### Optional Parameters
#### `--config-file -c`
Big data cluster config file path if you don't want the config of the cluster you are currently signed-in to, i.e. custom/bdc.json
#### `--target -t`
Output file to store the result in. Default: directed to stdout.
#### `--json-path -j`
The json key path that leads to the section or value you want from the config, i.e. key1.key2.key3. Uses the jsonpath query language, https://jsonpath.com/, for example: -j '$.spec.pools[?(@.spec.type == "Master")]..endpoints'
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
## azdata bdc config add
Adds the value at the json path in the config file.  All examples below are given in Bash.  If using another command line, please be aware that you may need to escape quotations appropriately.  Alternatively, you may use the patch file functionality.
```bash
azdata bdc config add --path -p 
                      --json-values -j
```
### Examples
Ex 1 - Add control plane storage.
```bash
azdata bdc config add --path custom/control.json --json-values "spec.storage={"accessMode":"ReadWriteOnce","className":"managed-premium","size":"10Gi"}"
```
### Required Parameters
#### `--path -p`
Big data cluster config file path of the config you would like to set, i.e. custom/bdc.json
#### `--json-values -j`
A key value pair list of json paths to values: key1.subkey1=value1,key2.subkey2=value2. You may provide inline json values such as: key='{"kind":"cluster","name":"test-cluster"}' or provide a file path, such as key=./values.json. Add does NOT support conditionals.  If the inline value you are providing is a key value pair itself with '=' and ',' please escape those characters.  For example, key1="key2\=val2\,key3\=val3". Please see http://jsonpatch.com/ for examples of how your path should look.  If you would like to access an array, you must do so by indicating the index, such as key.0=value
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
## azdata bdc config remove
Removes the value at the json path in the config file.  All examples below are given in Bash.  If using another command line, please be aware that you may need to escape quotations appropriately.  Alternatively, you may use the patch file functionality.
```bash
azdata bdc config remove --path -p 
                         --json-path -j
```
### Examples
Ex 1 - Remove control plane storage.
```bash
azdata bdc config remove --path custom/control.json --json-path ".spec.storage"
```
### Required Parameters
#### `--path -p`
Big data cluster config file path of the config you would like to set, i.e. custom/bdc.json
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
## azdata bdc config replace
Replaces the value at the json path in the config file.  All examples below are given in Bash.  If using another command line, please be aware that you may need to escape quotations appropriately.  Alternatively, you may use the patch file functionality.
```bash
azdata bdc config replace --path -p 
                          --json-values -j
```
### Examples
Ex 1 - Replace the port of a single endpoint (Controller Endpoint).
```bash
azdata bdc config replace --path custom/control.json --json-values "$.spec.endpoints[?(@.name=="Controller")].port=30080"
```
Ex 2 - Replace control plane storage.
```bash
azdata bdc config replace --path custom/control.json --json-values "spec.storage={"accessMode":"ReadWriteOnce","className":"managed-premium","size":"10Gi"}"
```
Ex 3 - Replace storage-0 resource spec, including replicas.
```bash
azdata bdc config replace --path custom/bdc.json --json-values "$.spec.resources.storage-0.spec={"replicas": 2,"storage": {"className": "managed-premium","size": "10Gi","accessMode": "ReadWriteOnce"},"type": "Storage"}"
```
### Required Parameters
#### `--path -p`
Big data cluster config file path of the config you would like to set, i.e. custom/bdc.json
#### `--json-values -j`
A key value pair list of json paths to values: key1.subkey1=value1,key2.subkey2=value2. You may provide inline json values such as: key='{"kind":"cluster","name":"test-cluster"}' or provide a file path, such as key=./values.json. Replace supports conditionals through the jsonpath library.  To use this, start your path with a $. This will allow you to do a conditional such as -j $.key1.key2[?(@.key3=='someValue'].key4=value. If the inline value you are providing is a key value pair itself with '=' and ',' please escape those characters.  For example, key1="key2\=val2\,key3\=val3". You may see examples below. For additional help, please see: https://jsonpath.com/
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
## azdata bdc config patch
Patches the config file according to the given patch file. Please consult http://jsonpatch.com/ for a better understanding of how the paths should be composed. The replace operation can use conditionals in its path due to the jsonpath library https://jsonpath.com/. All patch json files must start with a key of "patch" that has an array of patches with their corresponding op (add, replace, remove), path, and value. The "remove" op does not require a value, just a path. Please see the examples below.
```bash
azdata bdc config patch --path 
                        --patch-file -p
```
### Examples
Ex 1 - Replace the port of a single endpoint (Controller Endpoint) with patch file.
```bash
azdata bdc config patch --path custom/control.json --patch ./patch.json

    Patch File Example (patch.json):
        {"patch":[{"op":"replace","path":"$.spec.endpoints[?(@.name=="Controller")].port","value":30080}]}
```
Ex 2 - Replace control plane storage with patch file.
```bash
azdata bdc config patch --path custom/control.json --patch ./patch.json

    Patch File Example (patch.json):
        {"patch":[{"op":"replace","path":".spec.storage","value":{"accessMode":"ReadWriteMany","className":"managed-premium","size":"10Gi"}}]}
```
Ex 3 - Replace pool storage, including replicas (Storage Pool) with patch file.
```bash
azdata bdc config patch --path custom/bdc.json --patch ./patch.json

    Patch File Example (patch.json):
        {"patch":[{"op":"replace","path":"$.spec.resources.storage-0.spec","value":{"replicas": 2,"storage": {"className": "managed-premium","size": "10Gi","accessMode": "ReadWriteOnce"},"type": "Storage"}}]}
```
### Required Parameters
#### `--path`
Big data cluster config file path of the config you would like to set, i.e. custom/bdc.json
#### `--patch-file -p`
Path to a patch json file that is based off the jsonpatch library: http://jsonpatch.com/. You must start your patch json file with a key called "patch", whose value is an array of patch operations you intend to make. For the path of a patch operation, you may use dot notation, such as key1.key2 for most operations. If you would like to do a replace operation, and you are replacing a value in an array that requires a conditional, please use the jsonpath notation by beginning your path with a $. This will allow you to do a conditional such as $.key1.key2[?(@.key3=='someValue'].key4. Please see the examples below. For additional help with conditionals, please see: https://jsonpath.com/.
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