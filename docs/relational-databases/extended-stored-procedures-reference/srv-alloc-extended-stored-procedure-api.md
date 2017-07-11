---
title: "srv_alloc (Extended Stored Procedure API) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "srv_alloc"
apilocation: 
  - "opends60.dll"
apitype: "DLLExport"
dev_langs: 
  - "C++"
helpviewer_keywords: 
  - "srv_alloc"
ms.assetid: 91505c59-a273-452f-b71d-5e8205c21863
caps.latest.revision: 32
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# srv_alloc (Extended Stored Procedure API)
    
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureDontUse](../../includes/ssnotedepfuturedontuse-md.md)] Use CLR integration instead.  
  
 Allocates memory dynamically.  
  
## Syntax  
  
```  
  
void * srv_alloc ( DBINT  
size  
);  
```  
  
## Arguments  
 *size*  
 Specifies the number of bytes to allocate.  
  
## Returns  
 A pointer to the newly allocated space. If *size* bytes cannot be allocated, a null pointer is returned.  
  
## Remarks  
 The **srv_alloc** function is equivalent to the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows API  **GlobalAlloc** function. Normal Windows API C run-time memory management functions can be used in an Extended Stored Procedure API application.  
  
> [!IMPORTANT]  
>  You should thoroughly review the source code of extended stored procedures, and you should test the compiled DLLs before you install them on a production server. For information about security review and testing, see this [Microsoft Web site](http://go.microsoft.com/fwlink/?LinkID=54761&amp;clcid=0x409http://msdn.microsoft.com/security/).  
  
  