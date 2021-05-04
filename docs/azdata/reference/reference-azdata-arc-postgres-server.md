---
title: azdata arc postgres server reference
titleSuffix: SQL Server big data clusters
description: Reference article for azdata arc postgres server commands.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: seanw
ms.date: 04/29/2021
ms.topic: reference
ms.prod: sql
ms.technology: big-data-cluster
---

# azdata arc postgres server

Applies to [!INCLUDE [azure-data-cli-azdata](../../includes/azure-data-cli-azdata.md)]

The following article provides reference for the **sql** commands in the **azdata** tool. For more information about other **azdata** commands, see [azdata reference](reference-azdata.md)

## Commands

|Command|Description|
| --- | --- |
[azdata arc postgres server create](#azdata-arc-postgres-server-create) | Create an Azure Arc enabled PostgreSQL Hyperscale server group.
[azdata arc postgres server edit](#azdata-arc-postgres-server-edit) | Edit the configuration of an Azure Arc enabled PostgreSQL Hyperscale server group.
[azdata arc postgres server delete](#azdata-arc-postgres-server-delete) | Delete an Azure Arc enabled PostgreSQL Hyperscale server group.
[azdata arc postgres server show](#azdata-arc-postgres-server-show) | Show the details of an Azure Arc enabled PostgreSQL Hyperscale server group.
[azdata arc postgres server list](#azdata-arc-postgres-server-list) | List Azure Arc enabled PostgreSQL Hyperscale server groups.
[azdata arc postgres server config](reference-azdata-arc-postgres-server-config.md) | Configuration commands.
## azdata arc postgres server create
To set the password of the server group, please set the environment variable AZDATA_PASSWORD
```bash
azdata arc postgres server create --name -n 
                                  [--path]  
                                  
[--cores-limit -cl]  
                                  
[--cores-request -cr]  
                                  
[--memory-limit -ml]  
                                  
[--memory-request -mr]  
                                  
[--storage-class-data -scd]  
                                  
[--storage-class-logs -scl]  
                                  
[--storage-class-backups -scb]  
                                  
[--volume-claim-mounts -vcm]  
                                  
[--extensions]  
                                  
[--volume-size-data -vsd]  
                                  
[--volume-size-logs -vsl]  
                                  
[--volume-size-backups -vsb]  
                                  
[--workers -w]  
                                  
[--engine-version -ev]  
                                  
[--no-external-endpoint]  
                                  
[--port]  
                                  
[--no-wait]  
                                  
[--engine-settings -es]  
                                  
[--coordinator-engine-settings -ces]  
                                  
[--worker-engine-settings -wes]
```
### Examples
Create an Azure Arc enabled PostgreSQL Hyperscale server group.
```bash
azdata arc postgres server create -n pg1
```
Create an Azure Arc enabled PostgreSQL Hyperscale server group with engine settings. Both below examples are valid.
```bash
azdata arc postgres server create -n pg1 --engine-settings "key1=val1"
azdata arc postgres server create -n pg1 --engine-settings "key2=val2"
```
Create a PostgreSQL server group with volume claim mounts.
```bash
azdata arc postgres server create -n pg1 --volume-claim-mounts backup-pvc:backup
```
Create a PostgreSQL server group with specific memory-limit for different node roles.
```bash
azdata arc postgres server create -n pg1 --memory-limit "coordinator=2Gi,w=1Gi" --workers 1
```
### Required Parameters
#### `--name -n`
Name of the Azure Arc enabled PostgreSQL Hyperscale server group.
### Optional Parameters
#### `--path`
The path to the source json file for the Azure Arc enabled PostgreSQL Hyperscale server group. This is optional.
#### `--cores-limit -cl`
The maximum number of CPU cores for Azure Arc enabled PostgreSQL Hyperscale server group that can be used per node. Fractional cores are supported. Optionally a comma-separated list of roles with values can be specified in format <role>=<value>. Valid roles are: "coordinator" or "c", "worker" or "w". If no roles are specified, settings will apply to all nodes of the PostgreSQL Hyperscale server group.
#### `--cores-request -cr`
The minimum number of CPU cores that must be available per node to schedule the service. Fractional cores are supported. Optionally a comma-separated list of roles with values can be specified in format <role>=<value>. Valid roles are: "coordinator" or "c", "worker" or "w". If no roles are specified, settings will apply to all nodes of the PostgreSQL Hyperscale server group.
#### `--memory-limit -ml`
The memory limit of the Azure Arc enabled PostgreSQL Hyperscale server group as a number followed by Ki (kilobytes), Mi (megabytes), or Gi (gigabytes). Optionally a comma-separated list of roles with values can be specified in format <role>=<value>. Valid roles are: "coordinator" or "c", "worker" or "w". If no roles are specified, settings will apply to all nodes of the PostgreSQL Hyperscale server group.
#### `--memory-request -mr`
The memory request of the Azure Arc enabled PostgreSQL Hyperscale server group as a number followed by Ki (kilobytes), Mi (megabytes), or Gi (gigabytes). Optionally a comma-separated list of roles with values can be specified in format <role>=<value>. Valid roles are: "coordinator" or "c", "worker" or "w". If no roles are specified, settings will apply to all nodes of the PostgreSQL Hyperscale server group.
#### `--storage-class-data -scd`
The storage class to be used for data persistent volumes.
#### `--storage-class-logs -scl`
The storage class to be used for logs persistent volumes.
#### `--storage-class-backups -scb`
The storage class to be used for backup persistent volumes.
#### `--volume-claim-mounts -vcm`
A comma-separated list of volume claim mounts. A volume claim mount is a pair of an existing persistent volume claim (in the same namespace) and volume type (and optional metadata depending on the volume type) separated by colon.The persistent volume will be mounted in each pod for the PostgreSQL server group. The mount path may depend on the volume type.
#### `--extensions`
A comma-separated list of the Postgres extensions that should be loaded on startup. Please refer to the postgres documentation for supported values.
#### `--volume-size-data -vsd`
The size of the storage volume to be used for data as a positive number followed by Ki (kilobytes), Mi (megabytes), or Gi (gigabytes).
#### `--volume-size-logs -vsl`
The size of the storage volume to be used for logs as a positive number followed by Ki (kilobytes), Mi (megabytes), or Gi (gigabytes).
#### `--volume-size-backups -vsb`
The size of the storage volume to be used for backups as a positive number followed by Ki (kilobytes), Mi (megabytes), or Gi (gigabytes).
#### `--workers -w`
The number of worker nodes to provision in a server group. In Preview, reducing the number of worker nodes is not supported. Refer to documentation for additional details.
#### `--engine-version -ev`
Must be 11 or 12. The default value is 12.
`12`
#### `--no-external-endpoint`
If specified, no external service will be created. Otherwise, an external service will be created using the same service type as the data controller.
#### `--port`
Optional.
#### `--no-wait`
If given, the command will not wait for the instance to be in a ready state before returning.
#### `--engine-settings -es`
A comma separated list of Postgres engine settings in the format 'key1=val1, key2=val2'.
#### `--coordinator-engine-settings -ces`
A comma separated list of Postgres engine settings in the format 'key1=val1, key2=val2' to be applied to 'coordinator' node role. When node role specific settings are specified, default settings will be ignored and overridden with the settings provided here.
#### `--worker-engine-settings -wes`
A comma separated list of Postgres engine settings in the format 'key1=val1, key2=val2' to be applied to 'worker' node role. When node role specific settings are specified, default settings will be ignored and overridden with the settings provided here.
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
## azdata arc postgres server edit
Edit the configuration of an Azure Arc enabled PostgreSQL Hyperscale server group.
```bash
azdata arc postgres server edit --name -n 
                                [--path]  
                                
[--workers -w]  
                                
[]  
                                
[--cores-limit -cl]  
                                
[--cores-request -cr]  
                                
[--memory-limit -ml]  
                                
[--memory-request -mr]  
                                
[--extensions]  
                                
[--port]  
                                
[--no-wait]  
                                
[--engine-settings -es]  
                                
[--replace-engine-settings -res]  
                                
[--coordinator-engine-settings -ces]  
                                
[--worker-engine-settings -wes]  
                                
[--admin-password]
```
### Examples
Edit the configuration of an Azure Arc enabled PostgreSQL Hyperscale server group.
```bash
azdata arc postgres server edit --src ./spec.json -n pg1
```
Edit an Azure Arc enabled PostgreSQL Hyperscale server group with engine settings for the coordinator node.
```bash
azdata arc postgres server edit -n pg1 --coordinator-engine-settings "key2=val2"
```
Edits an Azure Arc enabled PostgreSQL Hyperscale server group and replaces existing engine settings with new setting key1=val1.
```bash
azdata arc postgres server edit -n pg1 --engine-settings "key1=val1" --replace-engine-settings
```
### Required Parameters
#### `--name -n`
Name of the Azure Arc enabled PostgreSQL Hyperscale server group that is being edited. The name under which your instance is deployed cannot be changed.
### Optional Parameters
#### `--path`
The path to the source json file for the Azure Arc enabled PostgreSQL Hyperscale server group. This is optional.
#### `--workers -w`
The number of worker nodes to provision in a server group. In Preview, reducing the number of worker nodes is not supported. Refer to documentation for additional details.
#### ``
Engine version cannot be changed. --engine-version can be used in conjunction with --name to identify a PostgreSQL Hyperscale server group when two server groups of different engine version have the same name. --engine-version is optional and when used to identify a server group, it must be 11 or 12.
#### `--cores-limit -cl`
The maximum number of CPU cores for Azure Arc enabled PostgreSQL Hyperscale server group that can be used per node, fractional cores are supported. To remove the cores_limit, specify its value as empty string. Optionally a comma-separated list of roles with values can be specified in format <role>=<value>. Valid roles are: "coordinator" or "c", "worker" or "w". If no roles are specified, settings will apply to all nodes of the PostgreSQL Hyperscale server group.
#### `--cores-request -cr`
The minimum number of CPU cores that must be available per node to schedule the service, fractional cores are supported. To remove the cores_request, specify its value as empty string. Optionally a comma-separated list of roles with values can be specified in format <role>=<value>. Valid roles are: "coordinator" or "c", "worker" or "w". If no roles are specified, settings will apply to all nodes of the PostgreSQL Hyperscale server group.
#### `--memory-limit -ml`
The memory limit for Azure Arc enabled PostgreSQL Hyperscale server group as a number followed by Ki (kilobytes), Mi (megabytes), or Gi (gigabytes). To remove the memory_limit, specify its value as empty string. Optionally a comma-separated list of roles with values can be specified in format <role>=<value>. Valid roles are: "coordinator" or "c", "worker" or "w". If no roles are specified, settings will apply to all nodes of the PostgreSQL Hyperscale server group.
#### `--memory-request -mr`
The memory request for Azure Arc enabled PostgreSQL Hyperscale server group as a number followed by Ki (kilobytes), Mi (megabytes), or Gi (gigabytes). To remove the memory_request, specify its value as empty string. Optionally a comma-separated list of roles with values can be specified in format <role>=<value>. Valid roles are: "coordinator" or "c", "worker" or "w". If no roles are specified, settings will apply to all nodes of the PostgreSQL Hyperscale server group.
#### `--extensions`
A comma-separated list of the Postgres extensions that should be loaded on startup. Please refer to the postgres documentation for supported values.
#### `--port`
Optional.
#### `--no-wait`
If given, the command will not wait for the instance to be in a ready state before returning.
#### `--engine-settings -es`
A comma separated list of Postgres engine settings in the format 'key1=val1, key2=val2'. The provided settings will be merged with the existing settings. To remove a setting, provide an empty value like 'removedKey='. If you change an engine setting that requires a restart, the service will be restarted to apply the settings immediately.
#### `--replace-engine-settings -res`
When specified with --engine-settings, will replace all existing custom engine settings with new set of settings and values.
#### `--coordinator-engine-settings -ces`
A comma separated list of Postgres engine settings in the format 'key1=val1, key2=val2' to be applied to 'coordinator' node role. When node role specific settings are specified, default settings will be ignored and overridden with the settings provided here.
#### `--worker-engine-settings -wes`
A comma separated list of Postgres engine settings in the format 'key1=val1, key2=val2' to be applied to 'worker' node role. When node role specific settings are specified, default settings will be ignored and overridden with the settings provided here.
#### `--admin-password`
If given, the Azure Arc enabled PostgreSQL Hyperscale server group's admin password will be set to the value of the AZDATA_PASSWORD environment variable if present and a prompted value otherwise.
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
## azdata arc postgres server delete
Delete an Azure Arc enabled PostgreSQL Hyperscale server group.
```bash
azdata arc postgres server delete --name -n 
                                  []  
                                  
[--force -f]
```
### Examples
Delete an Azure Arc enabled PostgreSQL Hyperscale server group.
```bash
azdata arc postgres server delete -n pg1
```
### Required Parameters
#### `--name -n`
Name of the Azure Arc enabled PostgreSQL Hyperscale server group.
### Optional Parameters
#### ``
--engine-version can be used in conjunction with --name to identify a PostgreSQL Hyperscale server group when two server groups of different engine version have the same name. --engine-version is optional and when used to identify a server group, it must be 11 or 12.
#### `--force -f`
Force delete the Azure Arc enabled PostgreSQL Hyperscale server group without confirmation.
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
## azdata arc postgres server show
Show the details of an Azure Arc enabled PostgreSQL Hyperscale server group.
```bash
azdata arc postgres server show --name -n 
                                []  
                                
[--path -p]
```
### Examples
Show the details of an Azure Arc enabled PostgreSQL Hyperscale server group.
```bash
azdata arc postgres server show -n pg1
```
### Required Parameters
#### `--name -n`
Name of the Azure Arc enabled PostgreSQL Hyperscale server group.
### Optional Parameters
#### ``
--engine-version can be used in conjunction with --name to identify a PostgreSQL Hyperscale server group when two server groups of different engine version have the same name. --engine-version is optional and when used to identify a server group, it must be 11 or 12.
#### `--path -p`
A path where the full specification for the Azure Arc enabled PostgreSQL Hyperscale server group should be written. If omitted, the specification will be written to standard output.
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
## azdata arc postgres server list
List Azure Arc enabled PostgreSQL Hyperscale server groups.
```bash
azdata arc postgres server list 
```
### Examples
List Azure Arc enabled PostgreSQL Hyperscale server groups.
```bash
azdata arc postgres server list
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
