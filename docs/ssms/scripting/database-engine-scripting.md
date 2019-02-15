---
title: "Database Engine Scripting | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.technology: scripting
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "scripts [SQL Server], PowerShell"
  - "scripts [SQL Server]"
  - "scripting [SQL Server Database Engine]"
  - "scripting [SQL Server Database Engine], PowerShell"
ms.assetid: 9978a884-59a2-4e7f-a82a-335149f3a261
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Database Engine Scripting
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
  The [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] supports the [!INCLUDE[msCoName](../../includes/msconame-md.md)] PowerShell scripting environment to manage instances of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] and the objects in the instances. You can also build and run [!INCLUDE[ssDE](../../includes/ssde-md.md)] queries that contain [!INCLUDE[tsql](../../includes/tsql-md.md)] and XQuery in environments very similar to scripting environments.  
  
## SQL Server PowerShell  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] includes two [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] PowerShell snap-ins that implement:  
  
-   A [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] PowerShell provider that exposes the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] management object model hierarchies as PowerShell paths that are similar to file system paths. You can use the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] management object model classes to manage the objects represented at each node of the path.  
  
-   A set of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] cmdlets that implement [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] commands. One of the cmdlets is **Invoke-Sqlcmd**. This is used to run [!INCLUDE[ssDE](../../includes/ssde-md.md)] Query scripts to be run with the **sqlcmd** utility.  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provides these features for running PowerShell:  
  
-   The **sqlps** PowerShell module that can be imported to a PowerShell session, the module then loads the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] snap-ins. You can interactively run ad hoc PowerShell commands. You can run script files using a command such as .\MyFolder\MyScript.ps1.  
  
-   PowerShell script files can be used as input to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent PowerShell job steps that run the scripts either at scheduled intervals or in response to system events.  
  
-   The **sqlps** utility that starts PowerShell and imports the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] module. You can then perform all actions supported by the module. You can start the **sqlps** utility either in a command prompt or by right-clicking on the nodes in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Management Studio Object Explorer tree and selecting **Start PowerShell**.  
  
## Database Engine Queries  
 [!INCLUDE[ssDE](../../includes/ssde-md.md)] query scripts contain three types of elements:  
  
-   [!INCLUDE[tsql](../../includes/tsql-md.md)] language statements.  
  
-   XQuery language statements  
  
-   Commands and variables from the **sqlcmd** utility.  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provides three environments for building and running [!INCLUDE[ssDE](../../includes/ssde-md.md)] queries:  
  
-   You can interactively run and debug [!INCLUDE[ssDE](../../includes/ssde-md.md)] queries in the [!INCLUDE[ssDE](../../includes/ssde-md.md)] Query Editor in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. You can code and debug several statements in one session, then save all of the statements in a single script file.  
  
-   The **sqlcmd** command prompt utility lets you interactively run [!INCLUDE[ssDE](../../includes/ssde-md.md)] queries, and also run existing [!INCLUDE[ssDE](../../includes/ssde-md.md)] query script files.  
  
 [!INCLUDE[ssDE](../../includes/ssde-md.md)] query script files are typically coded interactively in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] by using the [!INCLUDE[ssDE](../../includes/ssde-md.md)] Query Editor. The file can later be opened in one of these environments:  
  
-   Use the [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] **File**/**Open** menu to open the file in a new [!INCLUDE[ssDE](../../includes/ssde-md.md)] Query Editor window.  
  
-   Use the **-i**_input_file_ parameter to run the file with the **sqlcmd** utility.  
  
-   Use the **-QueryFromFile** parameter to run the file with the **Invoke-Sqlcmd** cmdlet in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] PowerShell scripts.  
  
-   Use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent [!INCLUDE[tsql](../../includes/tsql-md.md)] job steps to run the scripts either at scheduled intervals or in response to system events.  
  
 In addition, you can use the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Generate Script Wizard to generate [!INCLUDE[tsql](../../includes/tsql-md.md)] scripts. You can right-click objects in the [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] Object Explorer, then select the **Generate Script** menu item. **Generate Script** launches the wizard, which guides you through the process of creating a script.  
  
## Database Engine Scripting Tasks  
  
|Task Description|Topic|  
|----------------------|-----------|  
|Describes how to use the code and text editors in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] to interactively develop, debug, and run [!INCLUDE[tsql](../../includes/tsql-md.md)] scripts|[Query and Text Editors &#40;SQL Server Management Studio&#41;](../../relational-databases/scripting/query-and-text-editors-sql-server-management-studio.md)|  
|Describes how to use the **sqlcmd** utility to run [!INCLUDE[tsql](../../includes/tsql-md.md)] scripts from the command prompt, including the ability to interactively develop scripts.|[sqlcmd How-to Topics](https://msdn.microsoft.com/library/dd7a2d2b-6327-4d77-ac5a-580d36073ad4)|  
|Describes how to integrate the SQL Server components into a Windows PowerShell environment and then build PowerShell scripts for managing SQL Server instances and objects.|[SQL Server PowerShell](../../relational-databases/scripting/sql-server-powershell.md)|  
|Describes how to use the **Generate and Publish Scripts** wizard to create [!INCLUDE[tsql](../../includes/tsql-md.md)] scripts that recreate one or more of the objects from a database.|[Generate Scripts &#40;SQL Server Management Studio&#41;](../../relational-databases/scripting/generate-scripts-sql-server-management-studio.md)|  
  
## See Also  
 [sqlcmd Utility](../../tools/sqlcmd-utility.md)   
 [Tutorial: Writing Transact-SQL Statements](../../t-sql/tutorial-writing-transact-sql-statements.md)  
  
  
