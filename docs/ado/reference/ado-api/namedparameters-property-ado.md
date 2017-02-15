---
title: "NamedParameters Property (ADO) | Microsoft Docs"
ms.prod: "sql-non-specified"
ms.technology:
  - "drivers"
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.suite: ""
ms.tgt_pltfrm: ""
ms.topic: "article"
apitype: "COM"
f1_keywords: 
  - "_Command::NamedParameters"
helpviewer_keywords: 
  - "NamedParameters property [ADO]"
ms.assetid: 42409387-026c-435f-a9b1-bf4167095875
caps.latest.revision: 13
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# NamedParameters Property (ADO)
Indicates whether parameter names should be passed to the provider.  
  
## Remarks  
 When this property is true, ADO passes the value of the **Name** property of each parameter in the **Parameter** collection for the [Command Object](../../../ado/reference/ado-api/command-object-ado.md). The provider uses a parameter name to match parameters in the **CommandText** or **CommandStream** properties. If this property is false (the default), parameter names are ignored and the provider uses the order of parameters to match values to parameters in the **CommandText** or **CommandStream** properties.  
  
## Applies To  
 [Command Object (ADO)](../../../ado/reference/ado-api/command-object-ado.md)  
  
## See Also  
 [CommandText Property (ADO)](../../../ado/reference/ado-api/commandtext-property-ado.md)   
 [CommandStream Property (ADO)](../../../ado/reference/ado-api/commandstream-property-ado.md)   
 [Parameters Collection (ADO)](../../../ado/reference/ado-api/parameters-collection-ado.md)