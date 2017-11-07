---
title: "srv_pfieldex (Extended Stored Procedure API) | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "srv_pfieldex"
apilocation: 
  - "opends60.dll"
apitype: "DLLExport"
dev_langs: 
  - "C++"
helpviewer_keywords: 
  - "srv_pfieldex"
ms.assetid: d4e9a34b-b3a3-434f-8556-768bd20d145a
caps.latest.revision: 30
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# srv_pfieldex (Extended Stored Procedure API)
    
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureDontUse](../../includes/ssnotedepfuturedontuse-md.md)] Use CLR integration instead.  
  
 Returns a pointer to data containing the requested SRV_PROC field.  
  
## Syntax  
  
```  
  
void *srv_pfieldex(SRV_PROC *   
srvproc  
, int   
field  
, int *   
len  
);  
```  
  
## Arguments  
 *srvproc*  
 Is a pointer to the SRV_PROC structure that is the handle for a particular client connection. The structure contains information the Extended Stored Procedure API library uses to manage communication and data between the application and the client.  
  
 *field*  
 Specifies the *srvproc* field to return.  
  
|Field|Description|Return-type|  
|-----------|-----------------|------------------|  
|SRV_MSGLCID|Current session message LCID.|ULONG*|  
|SRV_INSTANCENAME|Instance name (if named); otherwise, returns NULL.|WCHAR*|  
  
 *len*  
 Is a pointer to an **int** variable that contains the length of the returned *field* value in bytes. If *len* is NULL, the length is not returned. When NULL is returned **len* is set to 0.  
  
## Returns  
 A pointer to data whose type depends on *field*. NULL is returned when *len* is NULL or *srvproc* is NULL. If the *field* is unknown, NULL is returned. When NULL is returned **len* is set to 0.  
  
> [!IMPORTANT]  
>  The buffer returned from the server should be read only. Otherwise, the server state may be corrupted.  
  
## Remarks  
 **Security Note** You should thoroughly review the source code of extended stored procedures, and you should test the compiled DLLs before you install them on a production server. For information about security review and testing, see this [Microsoft Web site](http://go.microsoft.com/fwlink/?LinkID=54761&amp;clcid=0x409http://msdn.microsoft.com/security/).  
  
  