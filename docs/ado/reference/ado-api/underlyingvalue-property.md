---
title: "UnderlyingValue Property"
description: "UnderlyingValue Property"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "Field20::GetUnderlyingValue"
  - "Field20::get_UnderlyingValue"
  - "Field20::UnderlyingValue"
helpviewer_keywords:
  - "UnderlyingValue property"
apitype: "COM"
---
# UnderlyingValue Property
Indicates the current value of a [Field](./field-object.md) object in the database.  
  
## Return Value  
 Returns a **Variant** value that indicates the value of the **Field**.  
  
## Remarks  
 Use the **UnderlyingValue** property to return the current field value from the database. The field value in the **UnderlyingValue** property is the value that is visible to your transaction and may be the result of a recent update by another transaction. This may differ from the [OriginalValue](./originalvalue-property-ado.md) property, which reflects the value that was originally returned to the [Recordset](./recordset-object-ado.md).  
  
 This is similar to using the [Resync](./resync-method.md) method, but the **UnderlyingValue** property returns only the value for a specific field from the current record. This is the same value that the [Resync](./resync-method.md) method uses to replace the [Value](./value-property-ado.md) property.  
  
 When you use this property with the **OriginalValue** property, you can resolve conflicts that arise from batch updates.  
  
## Record  
 For [Record](./record-object-ado.md) objects, this property will be empty for fields added before [Update](./update-method.md) is called.  
  
## Applies To  
 [Field Object](./field-object.md)  
  
## See Also  
 [OriginalValue and UnderlyingValue Properties Example (VB)](./originalvalue-and-underlyingvalue-properties-example-vb.md)   
 [OriginalValue and UnderlyingValue Properties Example (VC++)](./originalvalue-and-underlyingvalue-properties-example-vc.md)   
 [OriginalValue Property (ADO)](./originalvalue-property-ado.md)   
 [Resync Method](./resync-method.md)