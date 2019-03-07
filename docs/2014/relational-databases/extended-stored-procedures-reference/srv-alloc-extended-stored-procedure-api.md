---
title: "srv_alloc (Extended Stored Procedure API) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: stored-procedures
ms.topic: "reference"
api_name: 
  - "srv_alloc"
api_location: 
  - "opends60.dll"
topic_type: 
  - "apiref"
dev_langs: 
  - "C++"
helpviewer_keywords: 
  - "srv_alloc"
ms.assetid: 91505c59-a273-452f-b71d-5e8205c21863
author: rothja
ms.author: jroth
manager: craigg
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
>  You should thoroughly review the source code of extended stored procedures, and you should test the compiled DLLs before you install them on a production server. For information about security review and testing, see this [Microsoft Web site](https://go.microsoft.com/fwlink/?LinkID=54761&amp;clcid=0x409https://msdn.microsoft.com/security/).  
  
  
