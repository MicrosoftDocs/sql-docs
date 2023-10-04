---
title: "Dialect Property"
description: "Dialect Property"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "_Command::Dialect"
helpviewer_keywords:
  - "Dialect property"
apitype: "COM"
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
