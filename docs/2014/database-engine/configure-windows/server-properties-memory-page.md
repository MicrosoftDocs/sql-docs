---
title: "Server Properties (Memory Page) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
f1_keywords: 
  - "sql12.swb.serverproperties.memory.f1"
ms.assetid: 46a77d4e-ab92-49d3-a14b-423462e50715
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Server Properties (Memory Page)
  Use this page to view or modify your server memory options. When **Minimum server memory** is set to 0 and **Maximum server memory** is set to 2147483647 MB, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can take advantage of the optimum amount of memory at any given time, subject to how much memory the operating system and other applications are currently using. As the load on the computer and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] changes, so does the memory allocated. You can further limit this dynamic memory allocation to the minimum and maximum values specified below.  
  
## Options  
 **Minimum server memory (in MB)**  
 Specifies that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] should start with at least the minimum amount of allocated memory and not release memory below this value. Set this value based on the size and activity of your instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Always set the option to a reasonable value to ensure that the operating system does not request too much memory from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and inhibit Windows performance.  
  
 **Maximum server memory (in MB)**  
 Specifies the maximum amount of memory [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can allocate when it starts and while it runs. This configuration option can be set to a specific value if you know there are multiple applications running at the same time as [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and you want to guarantee that these applications have sufficient memory to run. If these other applications, such as Web or e-mail servers, request memory only as needed, then do not set the option, because [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] will release memory to them as needed. However, applications often use whatever memory is available when they start and do not request more if needed. If an application that behaves in this manner runs on the same computer at the same time as [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], set the option to a value that guarantees that the memory required by the application is not allocated by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The minimum amounts of memory you can specify for **max server memory** are 64 megabytes (MB) for 32-bit systems and 128 MB for 64-bit systems.  
  
 **Index creation memory (in KB, 0 = dynamic memory)**  
 Specifies the amount of memory (in KB) to use during index creation sorts. The default value of zero enables dynamic allocation and should work in most cases without additional adjustment; however, the user can enter a different value from 704 to 2147483647.  
  
> [!NOTE]  
>  Values from 1 to 703 are not allowed. If a value in this range is entered, the field overrides the entered value with 704.  
  
 **Minimum memory per query (in KB)**  
 Specifies the amount of memory (in KB) to allocate for the execution of a query. The user can set the value from 512 to 2147483647 KB. The default value is 1024 KB.  
  
 **Configured Values**  
 Displays the configured values for the options on this pane. If you change these values, click **Running Values** to see whether the changes have taken effect. If they have not, the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] must be restarted first.  
  
 **Running Values**  
 Shows the currently running values for the options on this pane. These values are read-only.  
  
## See Also  
 [Server Configuration Options &#40;SQL Server&#41;](server-configuration-options-sql-server.md)   
 [Server Memory Server Configuration Options](server-memory-server-configuration-options.md)  
  
  
