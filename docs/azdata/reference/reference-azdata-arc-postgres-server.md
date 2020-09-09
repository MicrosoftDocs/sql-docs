---
title: azdata arc postgres server reference
titleSuffix: SQL Server big data clusters
description: Reference article for azdata arc postgres server commands.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: seanw
ms.date: 06/22/2020
ms.topic: reference
ms.prod: sql
ms.technology: big-data-cluster
---

# azdata arc postgres server

Applies to `azdata`

The following article provides reference for the **sql** commands in the **azdata** tool. For more information about other **azdata** commands, see [azdata reference](reference-azdata.md)

## Commands

|Command|Description|
| --- | --- |
[azdata arc postgres server create](#azdata-arc-postgres-server-create) | Create a PostgreSQL server group.
[azdata arc postgres server edit](#azdata-arc-postgres-server-edit) | Edit the configuration of a PostgreSQL server group.
[azdata arc postgres server delete](#azdata-arc-postgres-server-delete) | Delete a PostgreSQL server group.
[azdata arc postgres server show](#azdata-arc-postgres-server-show) | Show the details of a PostgreSQL server group.
[azdata arc postgres server list](#azdata-arc-postgres-server-list) | List PostgreSQL server groups.
[azdata arc postgres server endpoint](reference-azdata-arc-postgres-server-endpoint.md) | Manage PostgreSQL server group endpoints.
[azdata arc postgres server config](reference-azdata-arc-postgres-server-config.md) | Configuration commands.
[azdata arc postgres server backup](reference-azdata-arc-postgres-server-backup.md) | Manage PostgreSQL server group backups.
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
                                  
[--extensions]  
                                  
[--volume-size-data -vsd]  
                                  
[--volume-size-logs -vsl]  
                                  
[--volume-size-backups -vsb]  
                                  
[--workers -w]  
                                  
[--engine-version -ev]  
                                  
[--no-external-endpoint]  
                                  
[--dev]  
                                  
[--port]  
                                  
[--no-wait]  
                                  
[--engine-settings -e]
```
### Examples
Create a PostgreSQL server group.
```bash
azdata arc postgres server create -n pg1
```
Create a PostgreSQL server group with engine settings. Both below examples are valid.
```bash
azdata arc postgres server create -n pg1 --engine-settings "key1=val1"
azdata arc postgres server create -n pg1 --engine-settings "key2=val2"
```
### Required Parameters
#### `--name -n`
Name of the instance.
### Optional Parameters
#### `--path`
The path to the source json file for the PostgreSQL server group. This is optional.
#### `--cores-limit -cl`
The maximum number of CPU cores for postgres instance that can be used per node. Fractional cores are supported.
#### `--cores-request -cr`
The minimum number of CPU cores that must be available per node to schedule the service. Fractional cores are supported.
#### `--memory-limit -ml`
The memory limit of the postgres instance as a number followed by Ki (kilobytes), Mi (megabytes), or Gi (gigabytes).
#### `--memory-request -mr`
The memory request of the postgres instance as a number followed by Ki (kilobytes), Mi (megabytes), or Gi (gigabytes).
#### `--storage-class-data -scd`
The storage classes to be used for data persistent volumes.
#### `--storage-class-logs -scl`
The storage classes to be used logs persistent volumes.
#### `--storage-class-backups -scb`
The storage classes to be used backup persistent volumes.
#### `--extensions`
A comma-separated list of the Postgres extensions that should be loaded on startup. Please refer to the postgres documentation for supported values.
#### `--volume-size-data -vsd`
The volume size for the storage classes to be used for data.
#### `--volume-size-logs -vsl`
The volume size for the storage classes to be used for logs.
#### `--volume-size-backups -vsb`
The volume size for the storage classes to be used for backups.
#### `--workers -w`
The number of worker nodes to provision in a sharded cluster, or zero (the default) for single-node Postgres.
#### `--engine-version -ev`
Must be 11 or 12. The default value is 12.
#### `--no-external-endpoint`
If specified, no external service will be created. Otherwise, an external service will be created using the same service type as the data controller.
#### `--dev`
If this is specified, then it is considered a dev instance and will not be billed for.
#### `--port`
Optional.
#### `--no-wait`
If given, the command will not wait for the instance to be in a ready state before returning.
#### `--engine-settings -e`
A comma separated list of Postgres engine settings in the format 'key1=val1, key2=val2'.
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
Edit the configuration of a PostgreSQL server group.
```bash
azdata arc postgres server edit --name -n 
                                [--path]  
                                
[--workers -w]  
                                
[--engine-version -ev]  
                                
[--cores-limit -cl]  
                                
[--cores-request -cr]  
                                
[--memory-limit -ml]  
                                
[--memory-request -mr]  
                                
[--extensions]  
                                
[--no-external-endpoint]  
                                
[--dev]  
                                
[--port]  
                                
[--no-wait]  
                                
[--engine-settings -e]  
                                
[--replace-engine-settings -re]  
                                
[--admin-password]
```
### Examples
Edit the configuration of a PostgreSQL server group.
```bash
azdata arc postgres server edit --src ./spec.json -n pg1
```
Edit a PostgreSQL server group with engine settings.
```bash
azdata arc postgres server edit -n pg1 --engine-settings "key2=val2"
```
Edits a PostgreSQL server group and replaces existing engine settings with new setting key1=val1.
```bash
azdata arc postgres server edit -n pg1 --engine-settings "key1=val1" --replace-engine-settings
```
### Required Parameters
#### `--name -n`
Name of the PostgreSQL server group that is being edited. The name under which your instance is deployed cannot be changed.
### Optional Parameters
#### `--path`
The path to the source json file for the PostgreSQL server group. This is optional.
#### `--workers -w`
The number of worker nodes to provision in a sharded cluster, or zero (the default) for single-node Postgres.
#### `--engine-version -ev`
Must be 11 or 12. Engine version cannot be changed. It is possible to create multiple PostgreSQL server groups with the same name but different engine version. Engine version will be used in conjunction with name to uniquely identify which PostgreSQL server group to edit in such case. Otherwise engine version can be optional.
#### `--cores-limit -cl`
The maximum number of CPU cores for postgres instance that can be used per node, fractional cores are supported. To remove the cores_limit, specify its value as empty string.
#### `--cores-request -cr`
The minimum number of CPU cores that must be available per node to schedule the service, fractional cores are supported. To remove the cores_request, specify its value as empty string.
#### `--memory-limit -ml`
The memory limit for postgres instance as a number followed by Ki (kilobytes), Mi (megabytes), or Gi (gigabytes). To remove the memory_limit, specify its value as empty string.
#### `--memory-request -mr`
The memory request for postgres instance as a number followed by Ki (kilobytes), Mi (megabytes), or Gi (gigabytes). To remove the memory_request, specify its value as empty string.
#### `--extensions`
A comma-separated list of the Postgres extensions that should be loaded on startup. Please refer to the postgres documentation for supported values.
#### `--no-external-endpoint`
If specified, no external service will be created. Otherwise, an external service will be created using the same service type as the data controller.
#### `--dev`
If this is specified, then it is considered a dev instance and will not be billed for.
#### `--port`
Optional.
#### `--no-wait`
If given, the command will not wait for the instance to be in a ready state before returning.
#### `--engine-settings -e`
A comma separated list of Postgres engine settings in the format 'key1=val1, key2=val2'. The provided settings will be merged with the existing settings. To remove a setting, provide an empty value like 'removedKey='. If you change an engine setting that requires a restart, the service will be restarted to apply the settings immediately.
#### `--replace-engine-settings -re`
When specified with --engine-settings, will replace all existing custom engine settings with new set of settings and values.
#### `--admin-password`
If given, the Postgres server's admin password will be set to the value of the AZDATA_PASSWORD environment variable if present and a prompted value otherwise.
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
Delete a PostgreSQL server group.
```bash
azdata arc postgres server delete --name -n 
                                  [--engine-version -ev]
```
### Examples
Delete a PostgreSQL server group.
```bash
azdata arc postgres server delete -n pg1
```
### Required Parameters
#### `--name -n`
Name of the PostgreSQL server group.
### Optional Parameters
#### `--engine-version -ev`
Must be 11 or 12. It is possible to create multiple PostgreSQL server groups with the same name but different engine version. Engine version will be used in conjunction with name to uniquely identify which PostgreSQL server group to delete in such case. Otherwise engine version can be optional.
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
Show the details of a PostgreSQL server group.
```bash
azdata arc postgres server show --name -n 
                                [--engine-version -ev]  
                                
[--path -p]
```
### Examples
Show the details of a PostgreSQL server group.
```bash
azdata arc postgres server show -n pg1
```
### Required Parameters
#### `--name -n`
Name of the PostgreSQL server group.
### Optional Parameters
#### `--engine-version -ev`
Must be 11 or 12. It is possible to create multiple PostgreSQL server groups with the same name but different engine version. Engine version will be used in conjunction with name to uniquely identify which PostgreSQL server group to show in such case. Otherwise engine version can be optional.
#### `--path -p`
A path where the full specification for the PostgreSQL server group should be written. If omitted, the specification will be written to standard output.
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
List PostgreSQL server groups.
```bash
azdata arc postgres server list 
```
### Examples
List PostgreSQL server groups.
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

For more information about how to install the **azdata** tool, see [Install azdata](deploy-install-azdata.md).
