---
title: "sp_remoteoption (Transact-SQL)"
description: sp_remoteoption displays or changes options for a remote login defined on the local server running SQL Server.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/22/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_remoteoption_TSQL"
  - "sp_remoteoption"
helpviewer_keywords:
  - "sp_remoteoption"
dev_langs:
  - "TSQL"
---
# sp_remoteoption (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Displays or changes options for a remote login defined on the local server running [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

> [!NOTE]  
> [!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)]`sp_remoteoption` doesn't change any options and returns an error message. It's supported for backward compatibility only.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_remoteoption
    [ @remoteserver = ] N'remoteserver'
    [ , [ @loginame = ] N'loginame' ]
    [ , [ @remotename = ] N'remotename' ]
    [ , [ @optname = ] 'optname' ]
    [ , [ @optvalue = ] 'optvalue' ]
[ ; ]
```

## Remarks

This stored procedure returns the following error message:

`The trusted option in remote login mapping is no longer supported.`

## Related content

- [Linked Servers (Database Engine)](../linked-servers/linked-servers-database-engine.md)
