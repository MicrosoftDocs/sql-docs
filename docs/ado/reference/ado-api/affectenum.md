---
title: "AffectEnum | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "AffectEnum"
helpviewer_keywords: 
  - "AffectEnum enumeration [ADO]"
ms.assetid: 1ab921a0-6c57-43b4-9291-701b2599f3e8
author: MightyPen
ms.author: genemi
manager: craigg
---
# AffectEnum
Specifies which records are affected by an operation.  
  
|Constant|Value|Description|  
|--------------|-----------|-----------------|  
|**adAffectAll**|3|If there is not a [Filter](../../../ado/reference/ado-api/filter-property.md) applied to the **Recordset**, affects all records.<br /><br /> If the **Filter** property is set to a string criteria (such as "Author='Smith'"), then the operation affects visible records in the current chapter.<br /><br /> If the **Filter** property is set to a member of the [FilterGroupEnum](../../../ado/reference/ado-api/filtergroupenum.md) or an array of bookmarks, then the operation will affect all rows of the **Recordset**. **Note:  adAffectAll** is hidden in the Visual Basic Object Browser.|  
|**adAffectAllChapters**|4|Affects all records in all sibling chapters of the **Recordset**, including those not visible via any **Filter** that is currently applied.|  
|**adAffectCurrent**|1|Affects only the current record.|  
|**adAffectGroup**|2|Affects only records that satisfy the current [Filter](../../../ado/reference/ado-api/filter-property.md) property setting. You must set the **Filter** property to a **FilterGroupEnum** value or an array of **Bookmarks** to use this option.|  
  
## ADO/WFC Equivalent  
 Package: **com.ms.wfc.data**  
  
|Constant|  
|--------------|  
|AdoEnums.Affect.ALL|  
|AdoEnums.Affect.ALLCHAPTERS|  
|AdoEnums.Affect.CURRENT|  
|AdoEnums.Affect.GROUP|  
  
## Applies To  
  
|||  
|-|-|  
|[CancelBatch Method (ADO)](../../../ado/reference/ado-api/cancelbatch-method-ado.md)|[Delete Method (ADO Recordset)](../../../ado/reference/ado-api/delete-method-ado-recordset.md)|  
|[Resync Method](../../../ado/reference/ado-api/resync-method.md)|[UpdateBatch Method](../../../ado/reference/ado-api/updatebatch-method.md)|
