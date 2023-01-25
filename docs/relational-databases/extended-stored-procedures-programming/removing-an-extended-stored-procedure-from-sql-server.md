---
title: "Removing an Extended Stored Procedure"
description: Removing an Extended Stored Procedure from SQL Server
author: VanMSFT
ms.author: vanto
ms.date: "03/14/2017"
ms.service: sql
ms.topic: "reference"
ms.custom: seo-dt-2019
helpviewer_keywords:
  - "deleting extended stored procedures"
  - "removing extended stored procedures"
  - "extended stored procedures [SQL Server], removing"
  - "dropping extended stored procedures"
ms.assetid: 7827e574-3f59-4279-9a9b-532582e041cb
---
# Removing an Extended Stored Procedure from SQL Server
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
    
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use CLR Integration instead.  
  
 To drop each extended stored procedure function in a user-defined extended stored procedure DLL, a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] system administrator must run the **sp_dropextendedproc** system stored procedure, specifying the name of the function and the name of the DLL in which that function resides. For example, this command removes the function **xp_hello**, located in a DLL named xp_hello.dll, from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]:  
  
```  
sp_dropextendedproc 'xp_hello'  
```  
  
 Beginning with [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)], **sp_dropextendedproc** does not drop system extended stored procedures. Instead, the system administrator should deny EXECUTE permission on the extended stored procedure to the **public** role.  
  
## See Also  
 [sp_dropextendedproc &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dropextendedproc-transact-sql.md)  
  
  
