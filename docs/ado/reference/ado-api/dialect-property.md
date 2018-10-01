---
title: "Dialect Property | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "_Command::Dialect"
helpviewer_keywords: 
  - "Dialect property"
ms.assetid: 329c3a71-ba88-4009-b04f-2f52195a5957
author: MightyPen
ms.author: genemi
manager: craigg
---
# Dialect Property
Indicates the dialect of the [CommandText](../../../ado/reference/ado-api/commandtext-property-ado.md) or [CommandStream](../../../ado/reference/ado-api/commandstream-property-ado.md) properties. The dialect defines the syntax and general rules that the provider uses to parse the string or stream.  
  
## Settings and Return Values  
 The **Dialect** property contains a valid GUID that represents the dialect of the command text or stream. The default value for this property is {C8B521FB-5CF3-11CE-ADE5-00AA0044773D}, which indicates that the provider should choose how to interpret the command text or stream.  
  
## Remarks  
 ADO does not query the provider when the user reads the value of this property; it returns a string representation of the value currently stored in the [Command](../../../ado/reference/ado-api/command-object-ado.md) object.  
  
 When the user sets the **Dialect** property, ADO validates the GUID and raises an error if the value supplied is not a valid GUID. See the documentation for your provider to determine the GUID values supported by the **Dialect** property.  
  
## Applies To  
 [Command Object (ADO)](../../../ado/reference/ado-api/command-object-ado.md)  
  
## See Also  
 [Execute Method (ADO Command)](../../../ado/reference/ado-api/execute-method-ado-command.md)
