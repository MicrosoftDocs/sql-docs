---
title: "AffectEnum"
description: "AffectEnum"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "AffectEnum"
helpviewer_keywords:
  - "AffectEnum enumeration [ADO]"
apitype: "COM"
---
# AffectEnum
Specifies which records are affected by an operation.  
  
|Constant|Value|Description|  
|--------------|-----------|-----------------|  
|**adAffectAll**|3|If there is not a [Filter](./filter-property.md) applied to the **Recordset**, affects all records.<br /><br /> If the **Filter** property is set to a string criteria (such as "Author='Smith'"), then the operation affects visible records in the current chapter.<br /><br /> If the **Filter** property is set to a member of the [FilterGroupEnum](./filtergroupenum.md) or an array of bookmarks, then the operation will affect all rows of the **Recordset**. **Note:  adAffectAll** is hidden in the Visual Basic Object Browser.|  
|**adAffectAllChapters**|4|Affects all records in all sibling chapters of the **Recordset**, including those not visible via any **Filter** that is currently applied.|  
|**adAffectCurrent**|1|Affects only the current record.|  
|**adAffectGroup**|2|Affects only records that satisfy the current [Filter](./filter-property.md) property setting. You must set the **Filter** property to a **FilterGroupEnum** value or an array of **Bookmarks** to use this option.|  
  
## ADO/WFC Equivalent  
 Package: **com.ms.wfc.data**  
  
|Constant|  
|--------------|  
|AdoEnums.Affect.ALL|  
|AdoEnums.Affect.ALLCHAPTERS|  
|AdoEnums.Affect.CURRENT|  
|AdoEnums.Affect.GROUP|  
  
## Applies To  

:::row:::
    :::column:::
        [CancelBatch Method (ADO)](./cancelbatch-method-ado.md)  
        [Delete Method (ADO Recordset)](./delete-method-ado-recordset.md)  
    :::column-end:::
    :::column:::
        [Resync Method](./resync-method.md)  
        [UpdateBatch Method](./updatebatch-method.md)  
    :::column-end:::
:::row-end:::