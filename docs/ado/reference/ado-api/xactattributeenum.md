---
title: "XactAttributeEnum | Microsoft Docs"
ms.prod: "sql-non-specified"
ms.technology:
  - "drivers"
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.suite: ""
ms.tgt_pltfrm: ""
ms.topic: "article"
apitype: "COM"
f1_keywords: 
  - "XactAttributeEnum"
helpviewer_keywords: 
  - "XactAttributeEnum enumeration [ADO]"
ms.assetid: e7dcecd3-7dc7-445c-b922-f700c3067fbc
caps.latest.revision: 11
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# XactAttributeEnum
Specifies the transaction attributes of a [Connection](../../../ado/reference/ado-api/connection-object-ado.md) object.  
  
|Constant|Value|Description|  
|--------------|-----------|-----------------|  
|**adXactAbortRetaining**|262144|Performs retaining aborts by calling [RollbackTrans](../../../ado/reference/ado-api/begintrans-committrans-and-rollbacktrans-methods-ado.md) to automatically start a new transaction. Not all providers support this behavior.|  
|**adXactCommitRetaining**|131072|Performs retaining commits by calling [CommitTrans](../../../ado/reference/ado-api/begintrans-committrans-and-rollbacktrans-methods-ado.md) to automatically start a new transaction. Not all providers support this behavior.|  
  
## ADO/WFC Equivalent  
 Package: **com.ms.wfc.data**  
  
|Constant|  
|--------------|  
|AdoEnums.XactAttribute.ABORTRETAINING|  
|AdoEnums.XactAttribute.COMMITRETAINING|  
  
## Applies To  
 [Attributes Property (ADO)](../../../ado/reference/ado-api/attributes-property-ado.md)