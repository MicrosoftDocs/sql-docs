---
title: Server properties (processors page)
description: Get acquainted with processor settings in SQL Server. Learn which options control the number of worker threads, the processor assignment, and other properties.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: drskwier, matteot
ms.date: 12/17/2020
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
f1_keywords:
  - "sql13.swb.serverproperties.processor.f1"
---

# Server Properties (Processors Page)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Use this page to view or modify your processor options. Processor affinity settings are only enabled when more than one processor is installed.  

## Options

### Processor Affinity
Assigns processors to specific threads to eliminating processor reloads and reduce thread migration across processors. For more information, see [affinity mask Server Configuration Option](../../database-engine/configure-windows/affinity-mask-server-configuration-option.md).

### I/O Affinity
Binds [!INCLUDE[msCoName](../../includes/msconame-md.md)] SQL Server disk I/Os to a specified subset of CPUs. For more information, see [affinity Input-Output mask Server Configuration Option](../../database-engine/configure-windows/affinity-input-output-mask-server-configuration-option.md).

### Automatically set processor affinity mask for all processors
Allows SQL Server to set the processor affinity.

### Automatically set I/O affinity mask for all processors
Allows SQL Server to set the I/O affinity.

### Maximum worker threads
0 allows SQL Server to dynamically set the number of worker threads. This setting is best for most systems. However, depending on your system configuration, setting this option to a specific value sometimes improves performance. For more information, see [Configure the max worker threads Server Configuration Option](../../database-engine/configure-windows/configure-the-max-worker-threads-server-configuration-option.md).  

### Boost SQL Server priority
Specifies whether SQL Server should run at a higher Microsoft Windows scheduling priority rather than other processes on the same computer. For more information, see [Configure the priority boost Server Configuration Option](../../database-engine/configure-windows/configure-the-priority-boost-server-configuration-option.md).  

> [!Note]
> This option is not available with SSMS 18.x and later versions.

### Use Windows fibers (lightweight pooling)
Use Windows fibers instead of threads for the SQL Server service. This is only available in Windows 2003 Server Edition. For more information, see [lightweight pooling Server Configuration Option](../../database-engine/configure-windows/lightweight-pooling-server-configuration-option.md).

> [!Note]
> This option is not available with SSMS 18.x and later versions.

### Configured Values
Displays the configured values for the options on this pane. If you change these values, select **Running Values** to see whether the changes have taken effect. If they have not, the instance of SQL Server must be restarted first.

### Running Values
View the currently running values for the options on this pane. These values are read-only.

## See Also
[Server Configuration Options &#40;SQL Server&#41;](../../database-engine/configure-windows/server-configuration-options-sql-server.md)  


