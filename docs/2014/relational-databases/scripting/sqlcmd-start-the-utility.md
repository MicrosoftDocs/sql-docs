---
title: "Start the sqlcmd Utility | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
ms.assetid: 00d57437-7a29-4da1-b639-ee990db055fb
author: MightyPen
ms.author: genemi
manager: craigg
---
# Start the sqlcmd Utility
  To begin using `sqlcmd`, you must first launch the utility and connect to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. You can connect to either a default or named instance. The first step is to start the `sqlcmd` utility.  
  
> [!NOTE]  
>  Windows Authentication is the default authentication mode for `sqlcmd`. To use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication, you must specify a user name and password by using the **-U** and **-P** options.  
  
> [!NOTE]  
>  By default, [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)] installs as the named instance **sqlexpress**.  
  
 If you have not connected to this instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] before, you may have to configure [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to accept connections.  
  
### To start the sqlcmd utility and connect to a default instance of SQL Server  
  
1.  On the **Start** menu click **Run**. In the **Open** box type **cmd**, and then click **OK** to open a Command Prompt window.  
  
2.  At the command prompt, type `sqlcmd`.  
  
3.  Press ENTER.  
  
     You now have a trusted connection to the default instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that is running on your computer.  
  
     **1>** is the `sqlcmd` prompt that specifies the line number. Each time you press ENTER, the number increases by one.  
  
4.  To end the `sqlcmd` session, type `EXIT` at the `sqlcmd` prompt.  
  
### To start the sqlcmd utility and connect to a named instance of SQL Server  
  
1.  Open a Command Prompt window, and type `sqlcmd -S`*myServer\instanceName*. Replace *myServer\instanceName* with the name of the computer and the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that you want to connect to.  
  
2.  Press ENTER.  
  
     The `sqlcmd` prompt (1>) indicates that you are connected to the specified instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
    > [!NOTE]  
    >  Entered [!INCLUDE[tsql](../../includes/tsql-md.md)] statements are stored in a buffer. They are executed as a batch when the GO command is encountered.  
  
## See Also  
 [Run Transact-SQL Script Files Using sqlcmd](sqlcmd-run-transact-sql-script-files.md)  
  
  
