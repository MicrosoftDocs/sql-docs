---
title: "XactAttributeEnum | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "XactAttributeEnum"
helpviewer_keywords: 
  - "XactAttributeEnum enumeration [ADO]"
ms.assetid: e7dcecd3-7dc7-445c-b922-f700c3067fbc
author: MightyPen
ms.author: genemi
manager: craigg
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
