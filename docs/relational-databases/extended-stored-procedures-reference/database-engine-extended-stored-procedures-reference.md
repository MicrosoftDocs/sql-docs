---
title: "Extended Stored Procedures Programmer's Reference"
description: Learn how the Database Engine Extended Stored Procedures provide a server-based application programming interface (API) for expanding SQL Server functionality.
author: VanMSFT
ms.author: vanto
ms.date: "03/17/2017"
ms.service: sql
ms.subservice: stored-procedures
ms.topic: "reference"
helpviewer_keywords:
  - "extended stored procedures [SQL Server], listed"
ms.assetid: 4e9d0374-0927-4f17-bab9-2215b1b8fea8
---
# Database Engine Extended Stored Procedures - Reference
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
    
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use CLR integration instead.  
  
 The [!INCLUDE[msCoName](../../includes/msconame-md.md)] Extended Stored Procedure API, previously part of Open Data Services, provides a server-based application programming interface (API) for extending [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] functionality. The API consists of C and C++ functions and macros used to build applications.  
  
 With the emergence of newer and more powerful technologies such as CLR integration, the need for extended stored procedures has largely been replaced.  
  
> [!IMPORTANT]  
>  You should thoroughly review the source code of extended stored procedures, and you should test the compiled DLLs before you install them on a production server. For information about security review and testing, see this [Microsoft Web site](https://go.microsoft.com/fwlink/?LinkID=54761&amp;clcid=0x409https://msdn.microsoft.com/security/).  
  
## In This Section  
  
:::row:::
    :::column:::
        [Data Types](../../relational-databases/extended-stored-procedures-reference/data-types-extended-stored-procedure-api.md)
    :::column-end:::
    :::column:::
        [srv_pfield](../../relational-databases/extended-stored-procedures-reference/srv-pfield-extended-stored-procedure-api.md)
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        [srv_alloc](../../relational-databases/extended-stored-procedures-reference/srv-alloc-extended-stored-procedure-api.md)
    :::column-end:::
    :::column:::
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        [srv_convert](../../relational-databases/extended-stored-procedures-reference/srv-convert-extended-stored-procedure-api.md)
    :::column-end:::
    :::column:::
        [srv_pfieldex](../../relational-databases/extended-stored-procedures-reference/srv-pfieldex-extended-stored-procedure-api.md)
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        [srv_describe](../../relational-databases/extended-stored-procedures-reference/srv-describe-extended-stored-procedure-api.md)
    :::column-end:::
    :::column:::
        [srv_rpcdb](../../relational-databases/extended-stored-procedures-reference/srv-rpcdb-extended-stored-procedure-api.md)
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        [srv_getbindtoken](../../relational-databases/extended-stored-procedures-reference/srv-getbindtoken-extended-stored-procedure-api.md)
    :::column-end:::
    :::column:::
        [srv_rpcname](../../relational-databases/extended-stored-procedures-reference/srv-rpcname-extended-stored-procedure-api.md)
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        [srv_got_attention](../../relational-databases/extended-stored-procedures-reference/srv-got-attention-extended-stored-procedure-api.md)
    :::column-end:::
    :::column:::
        [srv_rpcnumber](../../relational-databases/extended-stored-procedures-reference/srv-rpcnumber-extended-stored-procedure-api.md)
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
    :::column-end:::
    :::column:::
        [srv_rpcoptions](../../relational-databases/extended-stored-procedures-reference/srv-rpcoptions-extended-stored-procedure-api.md)
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        [srv_message_handler](../../relational-databases/extended-stored-procedures-reference/srv-message-handler-extended-stored-procedure-api.md)
    :::column-end:::
    :::column:::
        [srv_rpcowner](../../relational-databases/extended-stored-procedures-reference/srv-rpcowner-extended-stored-procedure-api.md)
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        [srv_paramdata](../../relational-databases/extended-stored-procedures-reference/srv-paramdata-extended-stored-procedure-api.md)
    :::column-end:::
    :::column:::
        [srv_rpcparams](../../relational-databases/extended-stored-procedures-reference/srv-rpcparams-extended-stored-procedure-api.md)
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        [srv_paraminfo](../../relational-databases/extended-stored-procedures-reference/srv-paraminfo-extended-stored-procedure-api.md)
    :::column-end:::
    :::column:::
        [srv_senddone](../../relational-databases/extended-stored-procedures-reference/srv-senddone-extended-stored-procedure-api.md)
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        [srv_paramlen](../../relational-databases/extended-stored-procedures-reference/srv-paramlen-extended-stored-procedure-api.md)
    :::column-end:::
    :::column:::
        [srv_sendmsg](../../relational-databases/extended-stored-procedures-reference/srv-sendmsg-extended-stored-procedure-api.md)
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        [srv_parammaxlen](../../relational-databases/extended-stored-procedures-reference/srv-parammaxlen-extended-stored-procedure-api.md)
    :::column-end:::
    :::column:::
        [srv_sendrow](../../relational-databases/extended-stored-procedures-reference/srv-sendrow-extended-stored-procedure-api.md)
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        [srv_paramname](../../relational-databases/extended-stored-procedures-reference/srv-paramname-extended-stored-procedure-api.md)
    :::column-end:::
    :::column:::
        [srv_setcoldata](../../relational-databases/extended-stored-procedures-reference/srv-setcoldata-extended-stored-procedure-api.md)
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        [srv_paramnumber](../../relational-databases/extended-stored-procedures-reference/srv-paramnumber-extended-stored-procedure-api.md)
    :::column-end:::
    :::column:::
        [srv_setcollen](../../relational-databases/extended-stored-procedures-reference/srv-setcollen-extended-stored-procedure-api.md)
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        [srv_paramset](../../relational-databases/extended-stored-procedures-reference/srv-paramset-extended-stored-procedure-api.md)
    :::column-end:::
    :::column:::
        [srv_setutype](../../relational-databases/extended-stored-procedures-reference/srv-setutype-extended-stored-procedure-api.md)
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        [srv_paramsetoutput](../../relational-databases/extended-stored-procedures-reference/srv-paramsetoutput-extended-stored-procedure-api.md)
    :::column-end:::
    :::column:::
        [srv_willconvert](../../relational-databases/extended-stored-procedures-reference/srv-willconvert-extended-stored-procedure-api.md)
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        [srv_paramstatus](../../relational-databases/extended-stored-procedures-reference/srv-paramstatus-extended-stored-procedure-api.md)
    :::column-end:::
    :::column:::
        [srv_wsendmsg](../../relational-databases/extended-stored-procedures-reference/srv-wsendmsg-extended-stored-procedure-api.md)
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        [srv_paramtype](../../relational-databases/extended-stored-procedures-reference/srv-paramtype-extended-stored-procedure-api.md)
    :::column-end:::
    :::column:::
    :::column-end:::
:::row-end:::
  
