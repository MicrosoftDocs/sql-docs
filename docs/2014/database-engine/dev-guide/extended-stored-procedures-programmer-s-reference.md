---
title: "Extended Stored Procedures Programmer&#39;s Reference | Microsoft Docs"
ms.custom: ""
ms.date: "03/09/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
topic_type: 
  - "apiref"
  - "apinav"
helpviewer_keywords: 
  - "extended stored procedures [SQL Server], listed"
ms.assetid: 4e9d0374-0927-4f17-bab9-2215b1b8fea8
caps.latest.revision: 38
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# Extended Stored Procedures Programmer&#39;s Reference
    
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureDontUse](../../includes/ssnotedepfuturedontuse-md.md)] Use CLR integration instead.  
  
 The [!INCLUDE[msCoName](../../includes/msconame-md.md)] Extended Stored Procedure API, previously part of Open Data Services, provides a server-based application programming interface (API) for extending [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] functionality. The API consists of C and C++ functions and macros used to build applications.  
  
 With the emergence of newer and more powerful technologies such as CLR integration, the need for extended stored procedures has largely been replaced.  
  
> [!IMPORTANT]  
>  You should thoroughly review the source code of extended stored procedures, and you should test the compiled DLLs before you install them on a production server. For information about security review and testing, see this [Microsoft Web site](http://go.microsoft.com/fwlink/?LinkID=54761&amp;clcid=0x409http://msdn.microsoft.com/security/).  
  
## In This Section  
  
|||  
|-|-|  
|[Data Types](../../../2014/database-engine/dev-guide/data-types-extended-stored-procedure-api.md)|[srv_pfield](../../../2014/database-engine/dev-guide/srv-pfield-extended-stored-procedure-api.md)|  
|[srv_alloc](../../../2014/database-engine/dev-guide/srv-alloc-extended-stored-procedure-api.md)||  
|[srv_convert](../../../2014/database-engine/dev-guide/srv-convert-extended-stored-procedure-api.md)|[srv_pfieldex](../../../2014/database-engine/dev-guide/srv-pfieldex-extended-stored-procedure-api.md)|  
|[srv_describe](../../../2014/database-engine/dev-guide/srv-describe-extended-stored-procedure-api.md)|[srv_rpcdb](../../../2014/database-engine/dev-guide/srv-rpcdb-extended-stored-procedure-api.md)|  
|[srv_getbindtoken](../../../2014/database-engine/dev-guide/srv-getbindtoken-extended-stored-procedure-api.md)|[srv_rpcname](../../../2014/database-engine/dev-guide/srv-rpcname-extended-stored-procedure-api.md)|  
|[srv_got_attention](../../../2014/database-engine/dev-guide/srv-got-attention-extended-stored-procedure-api.md)|[srv_rpcnumber](../../../2014/database-engine/dev-guide/srv-rpcnumber-extended-stored-procedure-api.md)|  
||[srv_rpcoptions](../../../2014/database-engine/dev-guide/srv-rpcoptions-extended-stored-procedure-api.md)|  
|[srv_message_handler](../../../2014/database-engine/dev-guide/srv-message-handler-extended-stored-procedure-api.md)|[srv_rpcowner](../../../2014/database-engine/dev-guide/srv-rpcowner-extended-stored-procedure-api.md)|  
|[srv_paramdata](../../../2014/database-engine/dev-guide/srv-paramdata-extended-stored-procedure-api.md)|[srv_rpcparams](../../../2014/database-engine/dev-guide/srv-rpcparams-extended-stored-procedure-api.md)|  
|[srv_paraminfo](../../../2014/database-engine/dev-guide/srv-paraminfo-extended-stored-procedure-api.md)|[srv_senddone](../../../2014/database-engine/dev-guide/srv-senddone-extended-stored-procedure-api.md)|  
|[srv_paramlen](../../../2014/database-engine/dev-guide/srv-paramlen-extended-stored-procedure-api.md)|[srv_sendmsg](../../../2014/database-engine/dev-guide/srv-sendmsg-extended-stored-procedure-api.md)|  
|[srv_parammaxlen](../../../2014/database-engine/dev-guide/srv-parammaxlen-extended-stored-procedure-api.md)|[srv_sendrow](../../../2014/database-engine/dev-guide/srv-sendrow-extended-stored-procedure-api.md)|  
|[srv_paramname](../../../2014/database-engine/dev-guide/srv-paramname-extended-stored-procedure-api.md)|[srv_setcoldata](../../../2014/database-engine/dev-guide/srv-setcoldata-extended-stored-procedure-api.md)|  
|[srv_paramnumber](../../../2014/database-engine/dev-guide/srv-paramnumber-extended-stored-procedure-api.md)|[srv_setcollen](../../../2014/database-engine/dev-guide/srv-setcollen-extended-stored-procedure-api.md)|  
|[srv_paramset](../../../2014/database-engine/dev-guide/srv-paramset-extended-stored-procedure-api.md)|[srv_setutype](../../../2014/database-engine/dev-guide/srv-setutype-extended-stored-procedure-api.md)|  
|[srv_paramsetoutput](../../../2014/database-engine/dev-guide/srv-paramsetoutput-extended-stored-procedure-api.md)|[srv_willconvert](../../../2014/database-engine/dev-guide/srv-willconvert-extended-stored-procedure-api.md)|  
|[srv_paramstatus](../../../2014/database-engine/dev-guide/srv-paramstatus-extended-stored-procedure-api.md)|[srv_wsendmsg](../../../2014/database-engine/dev-guide/srv-wsendmsg-extended-stored-procedure-api.md)|  
|[srv_paramtype](../../../2014/database-engine/dev-guide/srv-paramtype-extended-stored-procedure-api.md)||  
  
  