---
description: "NamedParameters Property (ADO)"
title: "NamedParameters Property (ADO) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: ado
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "_Command::NamedParameters"
helpviewer_keywords: 
  - "NamedParameters property [ADO]"
ms.assetid: 42409387-026c-435f-a9b1-bf4167095875
author: rothja
ms.author: jroth
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