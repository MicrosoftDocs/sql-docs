---
title: "affinity Input-Output mask Server Configuration Option"
description: Learn about the affinity I/O mask option. Use it to enhance the performance of SQL Server threads that issue I/Os by binding disk I/O to specified CPUs.
author: rwestMSFT
ms.author: randolphwest
ms.date: "07/06/2017"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
helpviewer_keywords:
  - "affinity I/O mask option"
  - "processor affinity [SQL Server]"
  - "binding processors [SQL Server]"
  - "CPU affinity mask option"
---
# affinity Input-Output mask Server Configuration Option
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  To carry out multitasking, [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows sometimes move process threads among different processors. Although efficient from an operating system point of view, this activity can reduce [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] performance under heavy system loads, as each processor cache is repeatedly reloaded with data. Assigning processors to specific threads can improve performance under these conditions by eliminating processor reloads; such an association between a thread and a processor is called processor affinity.  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] supports processor affinity by means of two affinity mask options: **affinity mask** (also known as **CPU affinity mask**) and **affinity I/O mask**. For more information on the **affinity mask** option, see [affinity mask Server Configuration Option](../../database-engine/configure-windows/affinity-mask-server-configuration-option.md). CPU and I/O affinity support for servers with 33 to 64 processors requires the additional use of the [affinity64 mask Server Configuration Option](../../database-engine/configure-windows/affinity64-mask-server-configuration-option.md) and [affinity64 Input-Output mask Server Configuration Option](../../database-engine/configure-windows/affinity64-input-output-mask-server-configuration-option.md) respectively.  
  
> [!NOTE]  
>  Affinity support for servers with 33 to 64 processors is only available on 64-bit operating systems.  
  
 The **affinity I/O mask** option binds [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] disk I/O to a specified subset of CPUs. In high-end [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] online transactional processing (OLTP) environments, this extension can enhance the performance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] threads issuing I/Os. This enhancement does not support hardware affinity for individual disks or disk controllers.  
  
 The value for **affinity I/O mask** specifies which CPUs in a multiprocessor computer are eligible to process [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] disk I/O operations. The mask is a bitmap in which the rightmost bit specifies the lowest-order CPU(0), the bit to its immediate left specifies the next-lowest-order CPU(1), and so on. To configure more than 32 processors, set both the **affinity I/O mask** and the **affinity64 I/O mask**.  
  
 The values for **affinity I/O mask** are as follows:  
  
-   A 1-byte **affinity I/O mask** covers up to 8 CPUs in a multiprocessor computer.  
  
-   A 2-byte **affinity I/O mask** covers up to 16 CPUs in a multiprocessor computer.  
  
-   A 3-byte **affinity I/O mask** covers up to 24 CPUs in a multiprocessor computer.  
  
-   A 4-byte **affinity I/O mask** covers up to 32 CPUs in a multiprocessor computer.  
  
-   To cover more than 32 CPUs, configure a four-byte **affinity I/O mask** for the first 32 CPUs and up to a four-byte **affinity64 I/O mask** for the remaining CPUs.  
  
 A 1 bit in the affinity I/O pattern specifies that the corresponding CPU is eligible to perform [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] disk I/O operations; a 0 bit specifies that no [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] disk I/O operations should be scheduled for the corresponding CPU. When all bits are set to zero, or **affinity I/O mask** is not specified, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] disk I/O is scheduled to any of the CPUs eligible to process [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] threads.  
  
 Because setting the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] **affinity I/O mask** option is a specialized operation, it should be used only when necessary. In most cases, the Windows 2000 or Windows Server 2003 default affinity provides the best performance.  
  
 When specifying the **affinity I/O mask** option, you must use it with the **affinity mask** configuration option. Do not enable the same CPU in both the **affinity I/O mask** switch and the **affinity mask** option. The bits corresponding to each CPU should be in one of the following three states:  
  
-   0 in both the **affinity I/O mask** option and the **affinity mask** option.  
  
-   1 in the **affinity I/O mask** option and 0 in the **affinity mask** option.  
  
-   0 in the **affinity I/O mask** option and 1 in the **affinity mask** option.  
  
 The **affinity I/O mask** option is an advanced option. If you are using the **sp_configure** system stored procedure to change the setting, you can change **affinity I/O mask** only when **show advanced options** is set to 1. In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], reconfiguring the **affinity I/O mask** option requires a restart of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance.  
  
> [!CAUTION]  
>  Do not configure CPU affinity in the Windows operating system and also configure the affinity mask in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. These settings are attempting to achieve the same result, and if the configurations are inconsistent, you may have unpredictable results. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] CPU affinity is best configured using the **sp_configure** option in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
## See Also  
 [Monitor Resource Usage &#40;System Monitor&#41;](../../relational-databases/performance-monitor/monitor-resource-usage-system-monitor.md)   
 [RECONFIGURE &#40;Transact-SQL&#41;](../../t-sql/language-elements/reconfigure-transact-sql.md)   
 [Server Configuration Options &#40;SQL Server&#41;](../../database-engine/configure-windows/server-configuration-options-sql-server.md)   
 [sp_configure &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)  
  
  
