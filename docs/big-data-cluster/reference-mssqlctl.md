---
title: mssqlctl reference
titleSuffix: SQL Server big data clusters
description: Reference article for mssqlctl commands.
author: rothja
ms.author: jroth
manager: craigg
ms.date: 02/28/2019
ms.topic: reference
ms.prod: sql
ms.technology: big-data-cluster
---

# mssqlctl

[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

The following article provides reference for the **mssqlctl** tool for [SQL Server 2019 big data clusters (preview)](big-data-cluster-overview.md). For more information about how to install the **mssqlctl** tool, see [Install mssqlctl to manage SQL Server 2019 big data clusters](deploy-install-mssqlctl.md).

## Commands
|     |     |
| --- | --- |
|[mssqlctl app](app.md) | Create, delete, run, and manage applications. |
|[mssqlctl cluster](cluster.md) | Select, manage, and operate clusters. |
[mssqlctl login](#mssqlctl-login) | Log in to the cluster.
[mssqlctl logout](#mssqlctl-logout) | Log out of cluster.
|[mssqlctl storage](storage.md) | Manage cluster storage. |
## mssqlctl login
Log in to the cluster.
```bash
mssqlctl login [--username -u] 
               [--password -p]  
               [--endpoint -e]
```
### Examples
Log in interactively.
```bash
mssqlctl login
```
Log in with user name and password.
```bash
mssqlctl login -u johndoe@contoso.com -p VerySecret
```
Log in with user name, password, and cluster endpoint.
```bash
mssqlctl login -u johndoe@contoso.com -p VerySecret --endpoint https://host.com:12800
```
### Optional Parameters
#### `--username -u`
Account user.
#### `--password -p`
Password credentials.
#### `--endpoint -e`
Cluster host and port (ex) "http://host:port".
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
## mssqlctl logout
Log out of cluster.
```bash
mssqlctl logout 
```
### Examples
Log out this user.
```bash
mssqlctl logout
```
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

For more information about how to install the **mssqlctl** tool, see [Install mssqlctl to manage SQL Server 2019 big data clusters](deploy-install-mssqlctl.md).