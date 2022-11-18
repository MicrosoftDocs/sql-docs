---
title: Status Option in admin tool
titleSuffix: SQL Server Distributed Replay
description: This article describes the status command-line option and syntax of the SQL Server Distributed Replay administration tool, which displays the current status.
ms.service: sql
ms.subservice: distributed-replay
ms.topic: conceptual
author: markingmyname
ms.author: maghan
ms.reviewer: mikeray
ms.custom: seo-lt-2019
ms.date: 06/20/2022
monikerRange: ">= sql-server-2016 || >= sql-server-linux-2017"
---

# Status Option (Distributed Replay Administration Tool)

[!INCLUDE [sqlserver2016-2019-only](../../includes/applies-to-version/sqlserver2016-2019-only.md)]

[!INCLUDE [distributed-replay-sql-server-2022](../../includes/distributed-replay-sql-server-2022.md)]

The Microsoft [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Distributed Replay administration tool, **DReplay.exe**, is a command-line tool that you can use to communicate with the distributed replay controller. This topic describes the **status** command-line option and corresponding syntax.

The **status** option queries the controller and displays the current status.

![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") For more information about the syntax conventions that are used with the administration tool syntax, see [Transact-SQL Syntax Conventions &#40;Transact-SQL&#41;](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md).

## Syntax

```dos

dreplay status [-m controller] [-f status_interval]  
```

### Parameters

**-m** _controller_  
Specifies the computer name of the controller. You can use "`localhost`" or "`.`" to refer to the local computer.

If the **-m** parameter isn't specified, the local computer is used.

**-f** _status_interval_  
Specifies the frequency (in seconds) at which to display the status.

If the **-f** parameter isn't specified, the default interval is 30 seconds.

## Examples

In the following example, the current status is displayed every 60 seconds. The value `localhost` indicates that the controller service is running on the same computer as the administration tool.

```dos
dreplay status -m localhost -f 60  
```

## Permissions

You must run the administration tool as an interactive user, as either a local user or a domain user account. To use a local user account, the administration tool and controller must be running on the same computer.

For more information, see [Distributed Replay Security](../../tools/distributed-replay/distributed-replay-security.md).

## See also

- [SQL Server Distributed Replay](../../tools/distributed-replay/sql-server-distributed-replay.md)
- [Transact-SQL Debugger](../../ssms/scripting/transact-sql-debugger.md)
