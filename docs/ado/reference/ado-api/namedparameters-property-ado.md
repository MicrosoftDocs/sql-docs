---
title: "NamedParameters Property (ADO)"
description: "NamedParameters Property (ADO)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "_Command::NamedParameters"
helpviewer_keywords:
  - "NamedParameters property [ADO]"
apitype: "COM"
---
# NamedParameters Property (ADO)
Indicates whether parameter names should be passed to the provider.  
  
## Remarks  
 When this property is true, ADO passes the value of the **Name** property of each parameter in the **Parameter** collection for the [Command Object](./command-object-ado.md). The provider uses a parameter name to match parameters in the **CommandText** or **CommandStream** properties. If this property is false (the default), parameter names are ignored and the provider uses the order of parameters to match values to parameters in the **CommandText** or **CommandStream** properties.  
  
## Applies To  
 [Command Object (ADO)](./command-object-ado.md)  
  
## See Also  
 [CommandText Property (ADO)](./commandtext-property-ado.md)   
 [CommandStream Property (ADO)](./commandstream-property-ado.md)   
 [Parameters Collection (ADO)](./parameters-collection-ado.md)