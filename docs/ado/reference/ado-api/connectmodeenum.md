---
title: "ConnectModeEnum | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "ConnectModeEnum"
helpviewer_keywords: 
  - "ConnectModeEnum enumeration [ADO]"
ms.assetid: 3792c294-5161-4538-a908-22a5fc50b85f
author: MightyPen
ms.author: genemi
manager: craigg
---
# ConnectModeEnum
Specifies the available permissions for modifying data in a [Connection](../../../ado/reference/ado-api/connection-object-ado.md), opening a [Record](../../../ado/reference/ado-api/record-object-ado.md), or specifying values for the [Mode](../../../ado/reference/ado-api/mode-property-ado.md) property of the **Record** and [Stream](../../../ado/reference/ado-api/stream-object-ado.md) objects.  
  
|Constant|Value|Description|  
|--------------|-----------|-----------------|  
|**adModeRead**|1|Indicates read-only permissions.|  
|**adModeReadWrite**|3|Indicates read/write permissions.|  
|**adModeRecursive**|0x400000|Used in conjunction with the other *\*ShareDeny\** values (**adModeShareDenyNone**, **adModeShareDenyWrite**, or **adModeShareDenyRead**) to propagate sharing restrictions to all sub-records of the current **Record**. It has no affect if the **Record** does not have any children. A run-time error is generated if it is used with **adModeShareDenyNone** only. However, it can be used with **adModeShareDenyNone** when combined with other values. For example, you can use "**adModeRead** Or **adModeShareDenyNone** Or **adModeRecursive**".|  
|**adModeShareDenyNone**|16|Allows others to open a connection with any permissions. Neither read nor write access can be denied to others.|  
|**adModeShareDenyRead**|4|Prevents others from opening a connection with read permissions.|  
|**adModeShareDenyWrite**|8|Prevents others from opening a connection with write permissions.|  
|**adModeShareExclusive**|12|Prevents others from opening a connection.|  
|**adModeUnknown**|0|Default. Indicates that the permissions have not yet been set or cannot be determined.|  
|**adModeWrite**|2|Indicates write-only permissions.|  
  
## ADO/WFC Equivalent  
 Package: **com.ms.wfc.data**  
  
|Constant|  
|--------------|  
|AdoEnums.ConnectMode.READ|  
|AdoEnums.ConnectMode.READWRITE|  
|(There is no equivalent of AdoEnums.ConnectMode.RECURSIVE)|  
|AdoEnums.ConnectMode.SHAREDENYNONE|  
|AdoEnums.ConnectMode.SHAREDENYREAD|  
|AdoEnums.ConnectMode.SHAREDENYWRITE|  
|AdoEnums.ConnectMode.SHAREEXCLUSIVE|  
|AdoEnums.ConnectMode.UNKNOWN|  
|AdoEnums.ConnectMode.WRITE|  
  
## Applies To  
  
|||  
|-|-|  
|[Mode Property (ADO)](../../../ado/reference/ado-api/mode-property-ado.md)|[Open Method (ADO Record)](../../../ado/reference/ado-api/open-method-ado-record.md)|  
|[Open Method (ADO Stream)](../../../ado/reference/ado-api/open-method-ado-stream.md)|[Stream Object (ADO)](../../../ado/reference/ado-api/stream-object-ado.md)|
