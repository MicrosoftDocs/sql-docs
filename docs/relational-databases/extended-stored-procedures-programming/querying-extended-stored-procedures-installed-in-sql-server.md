---
title: "Querying extended stored procedures"
description: Querying Extended Stored Procedures Installed in SQL Server
author: VanMSFT
ms.author: vanto
ms.date: "03/14/2017"
ms.service: sql
ms.topic: "reference"
helpviewer_keywords:
  - "extended stored procedures [SQL Server], querying"
---
# Querying Extended Stored Procedures Installed in SQL Server
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
    
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use CLR integration instead.  
  
 A [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] authenticated user can display the currently defined extended stored procedures and the name of the DLL to which each belongs by running the **sp_helpextendedproc** system procedure. For example, the following example returns the DLL to which **xp_hello** belongs:  
  
```  
sp_helpextendedproc 'xp_hello'  
```  
  
 If **sp_helpextendedproc** is executed without specifying an extended stored procedure, all the extended stored procedures and their DLLs are displayed.  
  
> [!IMPORTANT]  
>  Any authenticated account in SQL Server database engine can view information for all extended stored procedures when executing the procedure without passing any parameters in it:
> sp_helpextendedproc
> GO  
  
## See Also  
 [sp_helpextendedproc &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helpextendedproc-transact-sql.md)   
 [sp_addextendedproc &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addextendedproc-transact-sql.md)   
 [sp_dropextendedproc &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dropextendedproc-transact-sql.md)  
  
  
