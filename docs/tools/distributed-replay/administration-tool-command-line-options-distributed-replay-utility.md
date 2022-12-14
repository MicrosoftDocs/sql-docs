---
title: Administration tool command-line options
description: The SQL Server Distributed Replay administration tool, DReplay.exe, is a command-line tool to communicate with the distributed replay controller.
titleSuffix: SQL Server Distributed Replay
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

# Administration Tool Command-line Options (Distributed Replay Utility)

[!INCLUDE [sqlserver2016-2019-only](../../includes/applies-to-version/sqlserver2016-2019-only.md)]

[!INCLUDE [distributed-replay-sql-server-2022](../../includes/distributed-replay-sql-server-2022.md)]

The Microsoft SQL Server Distributed Replay administration tool, **DReplay.exe**, is a command-line tool to communicate with the distributed replay controller. Use the administration tool to initiate, monitor, and cancel operations on the controller.

![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") For more information about the syntax conventions that are used with the administration tool syntax, see [Transact-SQL Syntax Conventions &#40;Transact-SQL&#41;](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md).

## Syntax

```dos
dreplay {preprocess|replay|status|cancel} [options] [-?]}

Usage:

  dreplay preprocess [-m controller] -i input_trace_file  
    -d controller_working_dir [-c config_file] [-f status_interval]

  dreplay replay [-m controller] -d controller_working_dir [-o]  
    [-s target_server] -w clients [-c config_file]  
    [-f status_interval]

  dreplay status [-m controller] [-f status_interval]

  dreplay cancel [-m controller] [-q]   
```

## Remarks

You can issue the following command-line options with **DReplay.exe**:  

**preprocess**  
Initiates the preprocess stage. The controller prepares the input trace data, which you captured from the production environment, for replay against the target server. 

**replay**  
Initiates the event replay stage. The controller dispatches replay data to the specified clients, launches the distributed replay, and synchronizes the clients. Optionally, each client that was selected records the replay activity and saves result trace files locally.

**status**  
Queries the controller and displays the current status.

**cancel**  
Cancels the current operation that is running on the controller.

For detailed syntax information that includes the command arguments and examples, see the following topics:  

- [Preprocess Option &#40;Distributed Replay Administration Tool&#41;](../../tools/distributed-replay/preprocess-option-distributed-replay-administration-tool.md)

- [Replay Option &#40;Distributed Replay Administration Tool&#41;](../../tools/distributed-replay/replay-option-distributed-replay-administration-tool.md)

- [Status Option &#40;Distributed Replay Administration Tool&#41;](../../tools/distributed-replay/status-option-distributed-replay-administration-tool.md)

- [Cancel Option &#40;Distributed Replay Administration Tool&#41;](../../tools/distributed-replay/cancel-option-distributed-replay-administration-tool.md)

RPCs are replayed as RPCs and not as language events.

## Permissions

You must run the administration tool as an interactive user, as either a local user or a domain user account. To use a local user account, the administration tool and controller must be running on the same computer.

For more information, see [Distributed Replay Security](../../tools/distributed-replay/distributed-replay-security.md).

## See also

- [SQL Server Distributed Replay](../../tools/distributed-replay/sql-server-distributed-replay.md)
