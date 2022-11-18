---
title: Cancel option admin tool
titleSuffix: SQL Server Distributed Replay
description: This article describes the cancel command-line option and syntax of the SQL Server Distributed Replay administration tool.
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

# Cancel Option (Distributed Replay Administration Tool)

[!INCLUDE [sqlserver2016-2019-only](../../includes/applies-to-version/sqlserver2016-2019-only.md)]

[!INCLUDE [distributed-replay-sql-server-2022](../../includes/distributed-replay-sql-server-2022.md)]

The Microsoft SQL Server Distributed Replay administration tool, **DReplay.exe**, is a command-line tool that you can use to communicate with the distributed replay controller. This topic describes the **cancel** command-line option and corresponding syntax.

The **cancel** option cancels the current operation that is running on the controller.

![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") For more information about the syntax conventions that are used with the administration tool syntax, see [Transact-SQL Syntax Conventions &#40;Transact-SQL&#41;](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md).

## Syntax

```dos

dreplay cancel [-m controller] [-q]   
```

### Parameters

**-m** *controller*  
The computer name of the controller. You can use "`localhost`" or "`.`" to refer to the local computer.

If the **-m** parameter isn't specified, the local computer is used.

**-q**  
Quiet mode. Doesn't prompt for confirmation.

The **-q** parameter is optional.

## Examples

In the following example, a cancel request is submitted in quiet mode. The value `localhost` indicates that the controller service is running on the same computer as the administration tool.

```dos
dreplay cancel -m localhost -q  
```

## Permissions

You must run the administration tool as an interactive user, as either a local user or a domain user account. To use a local user account, the administration tool and controller must be running on the same computer.

For more information, see [Distributed Replay Security](../../tools/distributed-replay/distributed-replay-security.md).

## See also

- [SQL Server Distributed Replay](../../tools/distributed-replay/sql-server-distributed-replay.md)
