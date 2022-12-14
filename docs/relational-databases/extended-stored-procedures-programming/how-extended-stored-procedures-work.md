---
title: "How Extended Stored Procedures Work"
description: How Extended Stored Procedures Work
author: VanMSFT
ms.author: vanto
ms.date: "03/15/2017"
ms.service: sql
ms.topic: "reference"
helpviewer_keywords:
  - "extended stored procedures [SQL Server], about extended stored procedures"
ms.assetid: 6e946d8c-3268-4b59-8a1c-1637909cd701
---
# How Extended Stored Procedures Work

 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
    
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use CLR Integration instead.  
  
 The process by which an extended stored procedure works is:  
  
1.  When a client executes an extended stored procedure, the request is transmitted in tabular data stream (TDS) or Simple Object Access Protocol (SOAP) format from the client application to [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
2.  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] searches for the DLL associated with the extended stored procedure, and loads the DLL if it is not already loaded.  
  
3.  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] calls the requested extended stored procedure (implemented as a function inside the DLL).  
  
4.  The extended stored procedure passes result sets and return parameters back to the server by through the Extended Stored Procedure API.  

