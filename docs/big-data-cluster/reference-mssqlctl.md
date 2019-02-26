---
title: mssqlctl reference
titleSuffix: SQL Server 2019 big data clusters
description: Reference article for mssqlctl commands.
author: rothja
ms.author: jroth
manager: craigg
ms.date: 02/27/2019
ms.topic: reference
ms.prod: sql
ms.technology: big-data-cluster
---

# mssqlctl

The following article provides reference for the **mssqlctl** tool for SQL Server 2019 big data clusters (preview).

## <a id="commands"></a> Commands

|||
|---|---|
| [app](reference-mssqlctl-app.md) | Create, delete, run, and manage applications. |
| [cluster](reference-mssqlctl-cluster.md) | Select, manage, and operate clusters. |
| [login](#login) | Log in to the cluster. |
| [logout](#logout) | Log out of cluster. |
| [storage](reference-mssqlctl-storage.md) | Manage cluster storage. |

## <a id="login"></a> mssqlctl login

Log in to the cluster.

```
mssqlctl login
   --endpoint
   --password
   --username
```

### Parameters

| Parameter | Description |
|---|---|
|**--endpoint -e**| Cluster host and port (ex) `http://host:port"`. |
|**--password -p**| Password credentials. |
|**--username -u**| Account user. |

### Examples

Log in interactively.

```
mssqlctl login
```

Log in with user name and password.

```
mssqlctl login -u johndoe@contoso.com -p VerySecret
```

Log in with user name, password, and cluster endpoint.

```
mssqlctl login -u johndoe@contoso.com -p VerySecret --endpoint https://host.com:12800
```

## <a id="logout"></a> mssqlctl logout

Log out of cluster.

```
mssqlctl logout
   --username -u
```

### Parameters

| Parameters | Description |
|---|---|
| **--username -u** | Account user, if missing, logout the current active account. |

### Examples

Log out this user.

```
mssqlctl logout --username admin
```

## Next steps

For more information about how to install the **mssqlctl** tool, see [Install mssqlctl to manage SQL Server 2019 big data clusters](deploy-install-mssqlctl.md).