---
title: azdata arc postgres backup reference
titleSuffix: SQL Server big data clusters
description: Reference article for azdata arc postgres backup commands.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: seanw
ms.date: 06/02/2021
ms.topic: reference
ms.prod: sql
ms.technology: big-data-cluster
---

# azdata arc postgres backup

Applies to [!INCLUDE [azure-data-cli-azdata](../../includes/azure-data-cli-azdata.md)]

The following article provides reference for the **sql** commands in the **azdata** tool. For more information about other **azdata** commands, see [azdata reference](reference-azdata.md)

## Commands

|Command|Description|
| --- | --- |
[azdata arc postgres backup create](#azdata-arc-postgres-backup-create) | Create a backup of an Azure Arc enabled PostgreSQL Hyperscale server group.
[azdata arc postgres backup delete](#azdata-arc-postgres-backup-delete) | Delete a backup of an Azure Arc enabled PostgreSQL Hyperscale server group.
[azdata arc postgres backup list](#azdata-arc-postgres-backup-list) | List backups of an Azure Arc enabled PostgreSQL Hyperscale server group.
[azdata arc postgres backup restore](#azdata-arc-postgres-backup-restore) | Restore a backup of an Azure Arc enabled PostgreSQL Hyperscale server group.
## azdata arc postgres backup create
Create a backup of an Azure Arc enabled PostgreSQL Hyperscale server group.
```bash
azdata arc postgres backup create --server-name -sn 
                                  [--name -n]  
                                  
[--incremental -i]  
                                  
[--no-wait]
```
### Examples
Creates a backup for service 'pg'.
```bash
azdata arc postgres backup create -sn pg
```
Creates a named backup for service 'pg'.
```bash
azdata arc postgres backup create -sn pg -n backup1
```
### Required Parameters
#### `--server-name -sn`
Name of the Azure Arc enabled PostgreSQL Hyperscale server group.
### Optional Parameters
#### `--name -n`
Name of the backup. This parameter is optional.
#### `--incremental -i`
If given, the command will take an incremental backup. The default is to take a full backup.
#### `--no-wait`
If given, the command will not wait for the backup to complete before returning.
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
## azdata arc postgres backup delete
Delete a backup of an Azure Arc enabled PostgreSQL Hyperscale server group.
```bash
azdata arc postgres backup delete --server-name -sn 
                                  [--name -n]  
                                  
[-id]
```
### Examples
Delete a backup of an Azure Arc enabled PostgreSQL Hyperscale server group.
```bash
azdata arc postgres backup delete -sn pg -id e07dd3940e374bd9acbc86869cbc123d
```
### Required Parameters
#### `--server-name -sn`
Name of the Azure Arc enabled PostgreSQL Hyperscale server group.
### Optional Parameters
#### `--name -n`
Name of the backup. This parameter is mutually exclusive with -id.
#### `-id`
ID of the backup to be deleted. This parameter is mutually exclusive with --name.
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
## azdata arc postgres backup list
List backups of an Azure Arc enabled PostgreSQL Hyperscale server group.
```bash
azdata arc postgres backup list --server-name -sn 
                                
```
### Examples
List backups of an Azure Arc enabled PostgreSQL Hyperscale server group.
```bash
azdata arc postgres backup list -sn pg
```
### Required Parameters
#### `--server-name -sn`
Name of the Azure Arc enabled PostgreSQL Hyperscale server group.
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
## azdata arc postgres backup restore
Restore a backup of an Azure Arc enabled PostgreSQL Hyperscale server group.
```bash
azdata arc postgres backup restore --server-name -sn 
                                   [--backup-id -id]  
                                   
[--source-server-name -ssn]  
                                   
[--time -t]
```
### Examples
Restore a backup by ID
```bash
azdata arc postgres backup restore -sn pg -id 123e4567e89b12d3a456426655440000
```
Restore a backup by time (point-in-time restore)
```bash
azdata arc postgres backup restore -sn pg-dst -ssn pg-src --time "2020-11-18 17:25:34Z"
```
Restore a backup by time span (point-in-time restore)
```bash
azdata arc postgres backup restore -sn pg-dst -ssn pg-src --time 1d
```
### Required Parameters
#### `--server-name -sn`
Name of the Azure Arc enabled PostgreSQL Hyperscale server group.
### Optional Parameters
#### `--backup-id -id`
ID of the backup. If not specified, the most recent backup taken will be restored.
#### `--source-server-name -ssn`
Name of the source Azure Arc enabled PostgreSQL Hyperscale server group. If not provided, the backup will be restored in place on the server group identified by --server-name.
#### `--time -t`
The point in time to restore to, given either as a timestamp or a number and suffix (m for minutes, h for hours, d for days, and w for weeks). E.g. 1.5h goes back 90 minutes. If specified, --source-server-name must be given to restore the backup from a separate Azure Arc enabled PostgreSQL Hyperscale server group.
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