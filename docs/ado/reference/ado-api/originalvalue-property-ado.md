---
title: "OriginalValue Property (ADO) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Field20::OriginalValue"
helpviewer_keywords: 
  - "OriginalValue property [ADO]"
ms.assetid: 6e33c6ec-14d9-4b1d-ba9b-cb99862e7bac
author: MightyPen
ms.author: genemi
manager: craigg
---
# OriginalValue Property (ADO)
Indicates the value of a [Field](../../../ado/reference/ado-api/field-object.md) that existed in the record before any changes were made.  
  
## Return Value  
 Returns a **Variant** value that represents the value of a field prior to any change.  
  
## Remarks  
 Use the **OriginalValue** property to return the original field value for a field from the current record.  
  
 In *immediate update mode* (in which the provider writes changes to the underlying data source after you call the [Update](../../../ado/reference/ado-api/update-method.md) method), the **OriginalValue** property returns the field value that existed prior to any changes (that is, since the last **Update** method call). This is the same value that the [CancelUpdate](../../../ado/reference/ado-api/cancelupdate-method-ado.md) method uses to replace the [Value](../../../ado/reference/ado-api/value-property-ado.md) property.  
  
 In *batch update mode* (in which the provider caches multiple changes and writes them to the underlying data source only when you call the [UpdateBatch](../../../ado/reference/ado-api/updatebatch-method.md) method), the **OriginalValue** property returns the field value that existed prior to any changes (that is, since the last **UpdateBatch** method call). This is the same value that the [CancelBatch](../../../ado/reference/ado-api/cancelbatch-method-ado.md) method uses to replace the **Value** property. When you use this property with the [UnderlyingValue](../../../ado/reference/ado-api/underlyingvalue-property.md) property, you can resolve conflicts that arise from batch updates.  
  
## Record  
 For [Record](../../../ado/reference/ado-api/record-object-ado.md) objects, the **OriginalValue** property will be empty for fields added before [Update](../../../ado/reference/ado-api/update-method.md) is called.  
  
## Applies To  
 [Field Object](../../../ado/reference/ado-api/field-object.md)  
  
## See Also  
 [OriginalValue and UnderlyingValue Properties Example (VB)](../../../ado/reference/ado-api/originalvalue-and-underlyingvalue-properties-example-vb.md)   
 [OriginalValue and UnderlyingValue Properties Example (VC++)](../../../ado/reference/ado-api/originalvalue-and-underlyingvalue-properties-example-vc.md)   
 [UnderlyingValue Property](../../../ado/reference/ado-api/underlyingvalue-property.md)
