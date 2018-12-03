---
title: "Administration Tool Command-line Options (Distributed Replay Utility) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: tools-other
ms.topic: conceptual
ms.assetid: c01b0ed3-67e4-4561-92d2-a8fbb086aca8
author: stevestein
ms.author: sstein
manager: craigg
---
# Administration Tool Command-line Options (Distributed Replay Utility)
  The [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Distributed Replay administration tool, `DReplay.exe`, is a command-line tool that you can use to communicate with the distributed replay controller. Use the administration tool to initiate, monitor, and cancel operations on the controller.  
  
 ![Topic link icon](../../database-engine/media/topic-link.gif "Topic link icon") For more information about the syntax conventions that are used with the administration tool syntax, see [Transact-SQL Syntax Conventions &#40;Transact-SQL&#41;](/sql/t-sql/language-elements/transact-sql-syntax-conventions-transact-sql).  
  
## Syntax  
  
```  
  
      dreplay {preprocess|replay|status|cancel} [options] [-?]}  
  
Usage:  
  
  dreplay preprocess [-mcontroller] -iinput_trace_file  
    -dcontroller_working_dir [-cconfig_file] [-fstatus_interval]  
  
  dreplay replay [-mcontroller] -dcontroller_working_dir [-o]  
    [-starget_server] -wclients [-cconfig_file]  
    [-fstatus_interval]  
  
  dreplay status [-mcontroller] [-fstatus_interval]  
  
  dreplay cancel [-mcontroller] [-q]   
```  
  
## Remarks  
 You can issue the following command-line options with `DReplay.exe`:  
  
 **preprocess**  
 Initiates the preprocess stage. The controller prepares the input trace data, which you captured from the production environment, for replay against the target server.  
  
 **replay**  
 Initiates the event replay stage. The controller dispatches replay data to the specified clients, launches the distributed replay, and synchronizes the clients. Optionally, each client that was selected records the replay activity and saves result trace files locally.  
  
 **status**  
 Queries the controller and displays the current status.  
  
 **cancel**  
 Cancels the current operation that is running on the controller.  
  
 For detailed syntax information that includes the command arguments and examples, see the following topics:  
  
-   [Preprocess Option &#40;Distributed Replay Administration Tool&#41;](preprocess-option-distributed-replay-administration-tool.md)  
  
-   [Replay Option &#40;Distributed Replay Administration Tool&#41;](replay-option-distributed-replay-administration-tool.md)  
  
-   [Status Option &#40;Distributed Replay Administration Tool&#41;](status-option-distributed-replay-administration-tool.md)  
  
-   [Cancel Option &#40;Distributed Replay Administration Tool&#41;](cancel-option-distributed-replay-administration-tool.md)  
  
 RPCs are replayed as RPCs and not as language events.  
  
## Permissions  
 You must run the administration tool as an interactive user, as either a local user or a domain user account. To use a local user account, the administration tool and controller must be running on the same computer.  
  
 For more information, see [Distributed Replay Security](distributed-replay-security.md).  
  
## See Also  
 [SQL Server Distributed Replay](sql-server-distributed-replay.md)  
  
  
