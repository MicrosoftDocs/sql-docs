---
title: "OriginalValue Property (ADO)"
description: "OriginalValue Property (ADO)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "Field20::OriginalValue"
helpviewer_keywords:
  - "OriginalValue property [ADO]"
apitype: "COM"
---
# OriginalValue Property (ADO)
Indicates the value of a [Field](./field-object.md) that existed in the record before any changes were made.  
  
## Return Value  
 Returns a **Variant** value that represents the value of a field prior to any change.  
  
## Remarks  
 Use the **OriginalValue** property to return the original field value for a field from the current record.  
  
 In *immediate update mode* (in which the provider writes changes to the underlying data source after you call the [Update](./update-method.md) method), the **OriginalValue** property returns the field value that existed prior to any changes (that is, since the last **Update** method call). This is the same value that the [CancelUpdate](./cancelupdate-method-ado.md) method uses to replace the [Value](./value-property-ado.md) property.  
  
 In *batch update mode* (in which the provider caches multiple changes and writes them to the underlying data source only when you call the [UpdateBatch](./updatebatch-method.md) method), the **OriginalValue** property returns the field value that existed prior to any changes (that is, since the last **UpdateBatch** method call). This is the same value that the [CancelBatch](./cancelbatch-method-ado.md) method uses to replace the **Value** property. When you use this property with the [UnderlyingValue](./underlyingvalue-property.md) property, you can resolve conflicts that arise from batch updates.  
  
## Record  
 For [Record](./record-object-ado.md) objects, the **OriginalValue** property will be empty for fields added before [Update](./update-method.md) is called.  
  
## Applies To  
 [Field Object](./field-object.md)  
  
## See Also  
 [OriginalValue and UnderlyingValue Properties Example (VB)](./originalvalue-and-underlyingvalue-properties-example-vb.md)   
 [OriginalValue and UnderlyingValue Properties Example (VC++)](./originalvalue-and-underlyingvalue-properties-example-vc.md)   
 [UnderlyingValue Property](./underlyingvalue-property.md)