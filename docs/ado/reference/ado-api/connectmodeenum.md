---
title: "ConnectModeEnum"
description: "ConnectModeEnum"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "ConnectModeEnum"
helpviewer_keywords:
  - "ConnectModeEnum enumeration [ADO]"
apitype: "COM"
---
# ConnectModeEnum
Specifies the available permissions for modifying data in a [Connection](./connection-object-ado.md), opening a [Record](./record-object-ado.md), or specifying values for the [Mode](./mode-property-ado.md) property of the **Record** and [Stream](./stream-object-ado.md) objects.  
  
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

:::row:::
    :::column:::
        [Mode Property (ADO)](./mode-property-ado.md)  
        [Open Method (ADO Record)](./open-method-ado-record.md)  
    :::column-end:::
    :::column:::
        [Open Method (ADO Stream)](./open-method-ado-stream.md)  
        [Stream Object (ADO)](./stream-object-ado.md)  
    :::column-end:::
:::row-end:::