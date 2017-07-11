---
title: "sys.dm_linux_proc_sql_threads (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/29/2017"
ms.prod: "sql-server-2017"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.dm_linux_proc_sql_threads"
  - "sys.dm_linux_proc_sql_threads_TSQL"
  - "dm_linux_proc_sql_threads"
  - "dm_linux_proc_sql_threads_TSQL"
ms.assetid: 02af60c0-1a7c-41c7-827a-99924b0c31e3
caps.latest.revision: 8
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
ROBOTS: NOINDEX
---
# sys.dm_linux_proc_sql_threads (Transact-SQL)
[!INCLUDE[tsql-appliesto-ssLinx-xxxx-xxxx-xxx](../../includes/tsql-appliesto-sslinx-xxxx-xxxx-xxx.md)]

This dmv is based off of the linux stat file for every thread under `/proc/self/task` (aka [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)]). It has one row per thread. 

> [!NOTE]  
> Implementation of this DMV has been delayed. Expect it, or something similar in a future CTP.

|Column name |Data type |Description |  
|-----------------|---------------|-----------------|  
|**pid** |**bigint** |The Process ID. |
|**comm** |**nvarchar(256)** |The filename of the executable, in parentheses. This is visible whether or not the executable is swapped out. |
|**state** |**nvarchar(256)** |One of the following characters, indicating the process state: <br>R - Running <br>S - Sleeping in an interruptible wait <br>D - Waiting in uninterruptible disk sleep <br>Z - Zombie <br>T - Stopped (on a signal) or (before Linux 2.6.33) trace stopped <br>t - Tracing stop (Linux 2.6.33 onward) <br>W - Paging (only before Linux 2.6.0) <br>X - Dead (from Linux 2.6.0 onward) <br>x - Dead (Linux 2.6.33 to 3.13 only) <br>K - Wakekill (Linux 2.6.33 to 3.13 only) <br>W - Waking (Linux 2.6.33 to 3.13 only) <br>P - Parked (Linux 3.9 to 3.13 only) |
|**ppid** |**bigint** |The PID of the parent of this process. |
|**pgrp** |**bigint** |The process group ID of the process. |
|**session** |**bigint** |The session ID of the process. |
|**tty_nr** |**bigint** |The controlling terminal of the process. (The minor device number is contained in the combination of bits 31 to 20 and 7 to 0; the major device number is in bits 15 to 8.) |
|**tpgid** |**bigint** |The ID of the foreground process group of the controlling terminal of the process. |
|**flags** |**bigint** |The kernel flags word of the process. For bit meanings, see the `PF_*` defines in the Linux kernel source file `include/linux/sched.h`. Details depend on the kernel version. |
|**minflt** |**bigint** |The number of minor faults the process has made which have not required loading a memory page from disk. |
|**cminflt** |**bigint** |The number of minor faults that the process's waited-for children have made. |
|**majflt** |**bigint** |The number of major faults the process has made which have required loading a memory page from disk. |
|**cmajflt** |**bigint** |The number of major faults that the process's waited-for children have made. |
|**utime** |**bigint** |Amount of time that this process has been scheduled in user mode, measured in clock ticks (divide by `sysconf(_SC_CLK_TCK)`). This includes guest time, `guest_time` (time spent running a virtual CPU, see below), so that applications that are not aware of the guest time field do not lose that time from their calculations. |
|**stime** |**bigint** |Amount of time that this process has been scheduled in kernel mode, measured in clock ticks (divide by `sysconf(_SC_CLK_TCK)`). |
|**cutime** |**bigint** |Amount of time that this process's waited-for children have been scheduled in user mode, measured in clock ticks (divide by `sysconf(_SC_CLK_TCK)`). (See also times(2).) This includes guest time, `cguest_time` (time spent running a virtual CPU, see below). |
|**cstime** |**bigint** |Amount of time that this process's waited-for children have been scheduled in kernel mode, measured in clock ticks (divide by `sysconf(_SC_CLK_TCK)`). |
|**priority** |**bigint** |(Explanation for Linux 2.6) For processes running a real-time scheduling policy (policy below; see `sched_setscheduler(2)`), this is the negated scheduling priority, minus one; that is, a number in the range −2 to −100, corresponding to real-time priorities 1 to 99. For processes running under a non-real-time scheduling policy, this is the raw nice value (`setpriority(2)`) as represented in the kernel. The kernel stores nice values as numbers in the range 0 (high) to 39 (low), corresponding to the user-visible nice range of −20 to 19. Before Linux 2.6, this was a scaled value based on the scheduler weighting given to this process. |
|**nice** |**bigint** |The nice value (see `setpriority(2)`), a value in the range 19 (low priority) to −20 (high priority). |
|**num_threads** |**bigint** |Number of threads in this process (since Linux 2.6). Before kernel 2.6, this field was hard coded to 0 as a placeholder for an earlier removed field. |
|**itrealvalue** |**bigint** |The time in jiffies before the next `SIGALRM` is sent to the process due to an interval timer. Since kernel 2.6.17, this field is no longer maintained, and is hard coded as 0. |
|**starttime** |**bigint** |The time the process started after system boot. In kernels before Linux 2.6, this value was expressed in jiffies. Since Linux 2.6, the value is expressed in clock ticks (divide by `sysconf(_SC_CLK_TCK)`). |
|**vsize** |**bigint** |Virtual memory size in bytes. |
|**rss** |**bigint** |Resident Set Size: number of pages the process has in real memory. This is just the pages which count toward text, data, or stack space. This does not include pages which have not been demand-loaded in, or which are swapped out. |
|**rsslim** |**decimal** |Current soft limit in bytes on the `rss` of the process; see the description of `RLIMIT_RSS` in `getrlimit(2)`. |
|**startcode** |**bigint** |The address above which program text can run. |
|**endcode** |**bigint** |The address below which program text can run. |
|**startstack** |**bigint** |The address of the start (i.e., bottom) of the stack. |
|**kstkesp** |**bigint** |The current value of `ESP` (stack pointer), as found in the kernel stack page for the process. |
|**kstkeip** |**bigint** |The current `EIP` (instruction pointer). |
|**signal** |**bigint** |The bitmap of pending signals, displayed as a decimal number. Obsolete, because it does not provide information on real-time signals; use `/proc/[pid]/status` instead. |
|**blocked** |**bigint** |The bitmap of blocked signals, displayed as a decimal number. Obsolete, because it does not provide information on real-time signals; use `/proc/[pid]/status` instead. |
|**sigignore** |**bigint** |The bitmap of ignored signals, displayed as a decimal number. Obsolete, because it does not provide information on real-time signals; use `/proc/[pid]/status` instead. |
|**sigcatch** |**bigint** |The bitmap of caught signals, displayed as a decimal number. Obsolete, because it does not provide information on real-time signals; use `/proc/[pid]/status` instead. |
|**wchan** |**bigint** |This is the "channel" in which the process is waiting. It is the address of a location in the kernel where the process is sleeping. The corresponding symbolic name can be found in `/proc/[pid]/wchan`. |
|**nswap** |**bigint** |Number of pages swapped (not maintained). |
|**cnswap** |**bigint** |Cumulative `nswap` for child processes (not maintained). |
|**exit_signal** |**bigint** |Signal to be sent to parent when we die.	 |
|**processor** |**bigint** |CPU number last executed on. |
|**rt_priority** |**bigint** |Real-time scheduling priority, a number in the range 1 to 99 for processes scheduled under a real-time policy, or 0, for non-real-time processes (see `sched_setscheduler(2)`). |
|**policy** |**bigint** |Scheduling policy (see `sched_setscheduler(2)`). Decode using the `SCHED_*` constants in linux/sched.h. |
|**delaycct_blkio_ticks** |**bigint** |Aggregated block I/O delays, measured in clock ticks (centiseconds). |
|**guest_time** |**bigint** |Guest time of the process (time spent running a virtual CPU for a guest operating system), measured in clock ticks (divide by `sysconf(_SC_CLK_TCK)`). |
|**cguest_time** |**bigint** |Guest time of the process's children, measured in clock ticks (divide by `sysconf(_SC_CLK_TCK)`). |
|**start_data** |**bigint** |Address above which program initialized and uninitialized (`BSS`) data are placed. |
|**end_data** |**bigint** |Address below which program initialized and uninitialized (`BSS`) data are placed. |
|**start_brk** |**bigint** |Address above which program heap can be expanded with `brk(2)`. |
|**arg_start** |**bigint** |Address above which program command-line arguments (`argv`) are placed. |
|**arg_end** |**bigint** |Address below program command-line arguments (`argv`) are placed. |
|**env_start** |**bigint** |Address above which program environment is placed. |
|**env_end** |**bigint** |Address below which program environment is placed. |
|**exit_code** |**bigint** |The thread's exit status in the form reported by `waitpid(2)`. |


## Remarks

Returns an empty row set when called on a Windows computer.

## Permissions

Requires `VIEW SERVER STATE` permission.

## Examples   

 
The following queries, identify [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] threads that are using large amounts of resources. 

```tsql
SELECT * FROM sys.dm_linux_proc_sql_threads ORDER BY majflt DESC;
SELECT * FROM sys.dm_linux_proc_sql_threads ORDER BY minflt DESC;
SELECT * FROM sys.dm_linux_proc_sql_threads ORDER BY utime DESC;
SELECT * FROM sys.dm_linux_proc_sql_threads ORDER BY stime DESC;
```

## See Also  

[Linux Process Dynamic Management Views (Transact-SQL)](../../relational-databases/system-dynamic-management-views/linux-process-dynamic-management-views-transact-sql.md)   

