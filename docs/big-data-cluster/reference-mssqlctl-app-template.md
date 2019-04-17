---
title: mssqlctl app template reference
titleSuffix: SQL Server big data clusters
description: Reference article for mssqlctl app template commands.
author: rothja
ms.author: jroth
manager: craigg
ms.date: 02/28/2019
ms.topic: reference
ms.prod: sql
ms.technology: big-data-cluster
---

# mssqlctl app template

[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

The following article provides reference for the **app template** commands in the **mssqlctl** tool. For more information about other **mssqlctl** commands, see [mssqlctl reference](reference-mssqlctl.md).

## <a id="commands"></a> Commands

|||
|---|---|
| [list](#list) | Fetch supported templates. |
| [pull](#pull) | Download supported templates. |

## <a id="list"></a> mssqlctl app template list

Fetch supported templates.

```
mssqlctl app template list
   --url
```

### Parameters

| Parameters | Description |
|---|---|
| **--url -u** | Specify a different template repository location. Default: https://github.com/Microsoft/sql-server-samples.git. |

### Examples

Fetch all templates under the default template repository location.

```
mssqlctl app template list
```

Fetch all templates under a different repository location.

```
mssqlctl app template list --url https://github.com/diffrent/templates.git
```

## <a id="pull"></a> mssqlctl app template pull

Download supported templates.

```
mssqlctl app template pull
   --destination
   --name
   --url
```

### Parameters

| Parameters | Description |
|---|---|
| **--destination -d** | Where to place the application skeleton template.  Default: ./templates. |
| **--name -n** | Template name. For a full list off supported template names run `mssqlctl app template list`. |
| **--url -u** | Specify a different template repository location. Default:
https://github.com/Microsoft/sql-server-samples.git. |

### Examples

Download all templates under the default template repository location.

```
mssqlctl app template pull
```

Download all templates under a different repository location.

```
mssqlctl app template list --url https://github.com/diffrent/templates.git
```

Download individual template by name.

```
mssqlctl app template pull --name ssis
```

## Next steps

For more information about other **mssqlctl** commands, see [mssqlctl reference](reference-mssqlctl.md). For more information about how to install the **mssqlctl** tool, see [Install mssqlctl to manage SQL Server 2019 big data clusters](deploy-install-mssqlctl.md).