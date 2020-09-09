---
title: azdata arc postgres server backup reference
titleSuffix: SQL Server big data clusters
description: Reference article for azdata arc postgres server backup commands.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: seanw
ms.date: 06/22/2020
ms.topic: reference
ms.prod: sql
ms.technology: big-data-cluster
---

# azdata arc postgres server backup

Applies to `azdata`

The following article provides reference for the **sql** commands in the **azdata** tool. For more information about other **azdata** commands, see [azdata reference](reference-azdata.md)

## Commands

|Command|Description|
| --- | --- |
[azdata arc postgres server backup create](#azdata-arc-postgres-server-backup-create) | Create a backup of a PostgreSQL server group.
[azdata arc postgres server backup restore](#azdata-arc-postgres-server-backup-restore) | Restore a backup of a PostgreSQL server group.
[azdata arc postgres server backup restorestatus](#azdata-arc-postgres-server-backup-restorestatus) | Get the status of the most recent restore operation, if any.
[azdata arc postgres server backup show](#azdata-arc-postgres-server-backup-show) | Show details about a backup of a PostgreSQL server group.
## azdata arc postgres server backup create
Create a backup of a PostgreSQL server group.
```bash
azdata arc postgres server backup create 
```
### Examples
Creates a backup for service 'pg'.
```bash
azdata arc postgres server backup create -sn pg
```
Creates a named backup for service 'pg'.
```bash
azdata arc postgres server backup create -sn pg -n backup1
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
## azdata arc postgres server backup restore
Restore a backup of a PostgreSQL server group.
```bash
azdata arc postgres server backup restore 
```
### Examples
Restore the most recent backup.
```bash
azdata arc postgres server restore -sn pg```
Restore a backup by ID
```bash
azdata arc postgres server restore -sn pg -id 123e4567e89b12d3a456426655440000```
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
## azdata arc postgres server backup restorestatus
Get the status of the most recent restore operation, if any.
```bash
azdata arc postgres server backup restorestatus 
```
### Examples
Get the recent restore status for service 'pg' by ID.
```bash
azdata arc postgres server backup restorestatus -sn pg -id 123e4567e89b12d3a456426655440000
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
## azdata arc postgres server backup show
Show details about a backup of a PostgreSQL server group.
```bash
azdata arc postgres server backup show 
```
### Examples
Gets a backup for service 'pg' by ID
```bash
azdata arc postgres server backup show -sn pg -id 123e4567e89b12d3a456426655440000
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
