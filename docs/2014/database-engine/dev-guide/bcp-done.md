---
title: "bcp_done | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
api_name: 
  - "bcp_done"
api_location: 
  - "sqlncli11.dll"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "bcp_done function"
ms.assetid: e59b3f16-5b59-40da-880f-f3edf657d1ee
caps.latest.revision: 27
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# bcp_done
  Ends a bulk copy from program variables to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] performed with [bcp_sendrow](../../../2014/database-engine/dev-guide/bcp-sendrow.md).  
  
## Syntax  
  
```  
  
DBINT bcp_done (  
    HDBC   
hdbc  
);  
  
```  
  
## Arguments  
 *hdbc*  
 Is the bulk copy-enabled ODBC connection handle.  
  
## Returns  
 The number of rows permanently saved after the last call to [bcp_batch](../../../2014/database-engine/dev-guide/bcp-batch.md) or -1 in case of error.  
  
## Remarks  
 Call **bcp_done** after the last call to [bcp_sendrow](../../../2014/database-engine/dev-guide/bcp-sendrow.md) or [bcp_moretext](../../../2014/database-engine/dev-guide/bcp-moretext.md). Failure to call **bcp_done** after copying all data results in errors.  
  
## See Also  
 [Bulk Copy Functions](../../../2014/database-engine/dev-guide/bulk-copy-functions.md)  
  
  