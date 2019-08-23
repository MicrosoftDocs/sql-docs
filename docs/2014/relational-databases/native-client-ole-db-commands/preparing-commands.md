---
title: "Preparing Commands | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "SQL Server Native Client OLE DB provider, commands"
  - "prepared statements [SQL Server Native Client]"
  - "commands [OLE DB]"
  - "command preparation [SQL Server Native Client]"
ms.assetid: 09ec0c6c-0a44-4766-b9b7-5092f676ee54
author: MightyPen
ms.author: genemi
manager: craigg
---
# Preparing Commands
  The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider supports command preparation for optimized multiple execution of a single command; however, command preparation generates overhead, and a consumer does not need to prepare a command to execute it more than once. In general, a command should be prepared if it will be executed more than three times.  
  
 For performance reasons, the command preparation is deferred until the command is executed. This is the default behavior. Any errors in the command being prepared are not known until the command is executed or a metaproperty operation is performed. Setting the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] property SSPROP_DEFERPREPARE to FALSE can turn off this default behavior.  
  
 In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], when a command is executed directly (without preparing it first), an execution plan is created and cached. If the SQL statement is executed again, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] has an efficient algorithm to match the new statement with the existing execution plan in the cache, and reuses the execution plan for that statement.  
  
 For prepared commands, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provides native support for preparing and executing command statements. When you prepare a statement, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] creates an execution plan, caches it, and returns a handle to this execution plan to the provider. The provider then uses this handle to execute the statement repeatedly. No stored procedures are created. Because the handle directly identifies the execution plan for an SQL statement instead of matching the statement to the execution plan in the cache (as is the case for direct execution), it is more efficient to prepare a statement than to execute it directly, if you know the statement will be executed more than a few times.  
  
 In [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)], the prepared statements cannot be used to create temporary objects and cannot reference system stored procedures that create temporary objects, such as temporary tables. These procedures must be executed directly.  
  
 Some commands should never be prepared. For example, commands that specify stored procedure execution or include invalid text for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] stored procedure creation should not be prepared.  
  
 If a temporary stored procedure is created, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider executes the temporary stored procedure, returning results as if the statement itself was executed.  
  
 Temporary stored procedure creation is controlled by the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider -specific initialization property SSPROP_INIT_USEPROCFORPREP. If the property value is either SSPROPVAL_USEPROCFORPREP_ON or SSPROPVAL_USEPROCFORPREP_ON_DROP, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider attempts to create a stored procedure when a command is prepared. Stored procedure creation succeeds if the application user has sufficient [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] permissions.  
  
 For consumers that infrequently disconnect, creation of temporary stored procedures can require significant resources of **tempdb**, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] system database in which temporary objects are created. When the value of SSPROP_INIT_USEPROCFORPREP is SSPROPVAL_USEPROCFORPREP_ ON, temporary stored procedures created by the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider are dropped only when the session that created the command loses its connection to the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. If that connection is the default connection created on data source initialization, the temporary stored procedure is dropped only when the data source becomes uninitialized.  
  
 When the value of SSPROP_INIT_USEPROCFORPREP is SSPROPVAL_USEPROCFORPREP_ON_DROP, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider temporary stored procedures are dropped when one of the following occurs:  
  
-   The consumer uses **ICommandText::SetCommandText** to indicate a new command.  
  
-   The consumer uses **ICommandPrepare::Unprepare** to indicate that it no longer requires the command text.  
  
-   The consumer releases all references to the command object using the temporary stored procedure.  
  
 A command object has at most one temporary stored procedure in **tempdb**. Any existing temporary stored procedure represents the current command text of a specific command object.  
  
## See Also  
 [Commands](commands.md)  
  
  
