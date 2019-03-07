---
title: "Creating Extended Stored Procedures | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
ms.topic: "reference"
helpviewer_keywords: 
  - "warnings [SQL Server]"
  - "extended stored procedures [SQL Server], debugging"
  - "extended stored procedures [SQL Server], creating"
  - "messages [SQL Server], extended stored procedures"
ms.assetid: 9f7c0cdb-6d88-44c0-b049-29953ae75717
author: rothja
ms.author: jroth
manager: craigg
---
# Creating Extended Stored Procedures
    
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureDontUse](../../includes/ssnotedepfuturedontuse-md.md)] Use CLR Integration instead.  
  
 An extended stored procedure is a function with a prototype:  
  
 SRVRETCODE *xp_extendedProcName* **(**SRVPROC **\*);**  
  
 Using the prefix xp_ is optional. Extended stored procedure names are case sensitive when referenced in [!INCLUDE[tsql](../../includes/tsql-md.md)] statements, regardless of code page/sort order installed on the server. When you build a DLL:  
  
-   If an entry point is necessary, write a DllMain function.  
  
     This function is optional; if you do not provide it in source code, the compiler links its own version, which does nothing but return TRUE. If you provide a DllMain function, the operating system calls this function when a thread or process attaches to or detaches from the DLL.  
  
-   All functions called from outside the DLL (all extended stored procedure Efunctions) must be exported.  
  
     You can export a function by listing its name in the EXPORTS section of a .def file, or you can prefix the function name in the source code with __declspec(dllexport), a Microsoft compiler extension (Note that \__declspec() begins with two underscores).  
  
 These files are required for creating an extended stored procedure DLL.  
  
|File|Description|  
|----------|-----------------|  
|Srv.h|Extended Stored Procedure API header file|  
|Opends60.lib|Import library for Opends60.dll|  
  
 To create an extended stored procedure DLL, create a project of type Dynamic Link Library. For more information about creating a DLL, see the development environment documentation.  
  
 It is highly recommended that all extended stored procedure DLLs implement and export the following function:  
  
```  
__declspec(dllexport) ULONG __GetXpVersion()  
{  
   return ODS_VERSION;  
}  
```  
  
> [!NOTE]  
>  __declspec(dllexport) is a Microsoft-specific compiler extension. If your compiler does not support this directive, you should export this function in your DEF file under the EXPORTS section.  
  
 When [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is started with the trace flag -T260 or if a user with system administrator privileges runs DBCC TRACEON (260), and if the extended stored procedure DLL does not support __GetXpVersion(), a warning message (Error 8131: Extended stored procedure DLL '%' does not export \__GetXpVersion().) is printed to the error log. (Note that \__GetXpVersion() begins with two underscores.)  
  
 If the extended stored procedure DLL exports __GetXpVersion(), but the version returned by the function is less than that required by the server, a warning message stating the version returned by the function and the version expected by the server is printed to the error log. If you get this message, you are returning an incorrect value from \__GetXpVersion(), or you are compiling with an older version of srv.h.  
  
> [!NOTE]  
>  SetErrorMode, a [!INCLUDE[msCoName](../../includes/msconame-md.md)] Win32 function, should not be called in extended stored procedures.  
  
 It is recommended that a long-running extended stored procedure should call srv_got_attention periodically so that the procedure can terminate itself if the connection is killed or the batch is aborted.  
  
 To debug an extended stored procedure DLL, copy it to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\Binn directory. To specify the executable for the debugging session, enter the path and file name of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] executable file (for example, C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\Binn\Sqlservr.exe). For information about sqlservr arguments, see [sqlservr Application](../../tools/sqlservr-application.md).  
  
## See Also  
 [srv_got_attention &#40;Extended Stored Procedure API&#41;](../extended-stored-procedures-reference/srv-got-attention-extended-stored-procedure-api.md)  
  
  
