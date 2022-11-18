---
title: "XactAttributeEnum"
description: "XactAttributeEnum"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "XactAttributeEnum"
helpviewer_keywords:
  - "XactAttributeEnum enumeration [ADO]"
apitype: "COM"
---
# XactAttributeEnum
Specifies the transaction attributes of a [Connection](./connection-object-ado.md) object.  
  
|Constant|Value|Description|  
|--------------|-----------|-----------------|  
|**adXactAbortRetaining**|262144|Performs retaining aborts by calling [RollbackTrans](./begintrans-committrans-and-rollbacktrans-methods-ado.md) to automatically start a new transaction. Not all providers support this behavior.|  
|**adXactCommitRetaining**|131072|Performs retaining commits by calling [CommitTrans](./begintrans-committrans-and-rollbacktrans-methods-ado.md) to automatically start a new transaction. Not all providers support this behavior.|  
  
## ADO/WFC Equivalent  
 Package: **com.ms.wfc.data**  
  
|Constant|  
|--------------|  
|AdoEnums.XactAttribute.ABORTRETAINING|  
|AdoEnums.XactAttribute.COMMITRETAINING|  
  
## Applies To  
 [Attributes Property (ADO)](./attributes-property-ado.md)