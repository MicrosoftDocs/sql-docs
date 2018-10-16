---
title: "FieldEnum | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "FieldEnum"
helpviewer_keywords: 
  - "FieldEnum enumeration [ADO]"
ms.assetid: be4eda13-d4e4-4d6b-bb0d-3310b0a96fc2
author: MightyPen
ms.author: genemi
manager: craigg
---
# FieldEnum
Specifies the special fields referenced in a [Record](../../../ado/reference/ado-api/record-object-ado.md) object's [Fields](../../../ado/reference/ado-api/fields-collection-ado.md) collection.  
  
## Remarks  
 These constants provide a "shortcut" to accessing special fields associated with a **Record**. Retrieve the [Field](../../../ado/reference/ado-api/field-object.md) object from the **Fields** collection, and then obtain its contents with the **Field** object's [Value](../../../ado/reference/ado-api/value-property-ado.md) property.  
  
|Constant|Value|Description|  
|--------------|-----------|-----------------|  
|**adDefaultStream**|-1|References the field containing the default [Stream](../../../ado/reference/ado-api/stream-object-ado.md) object associated with a **Record**.|  
|**adRecordURL**|-2|References the field containing the absolute URL string for the current **Record**.|
