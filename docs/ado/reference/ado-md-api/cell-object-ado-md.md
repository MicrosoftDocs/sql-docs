---
title: "Cell Object (ADO MD) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Cell"
helpviewer_keywords: 
  - "Cell object [ADO MD]"
ms.assetid: dcc2f044-b785-4a29-9bc5-b673f66eedf9
author: MightyPen
ms.author: genemi
manager: craigg
---
# Cell Object (ADO MD)
Represents the data at the intersection of axis coordinates contained in a cellset.  
  
## Remarks  
 A **Cell** object is returned by the [Item](../../../ado/reference/ado-md-api/item-property-ado-md-cellset.md) property of a [Cellset](../../../ado/reference/ado-md-api/cellset-object-ado-md.md) object.  
  
 With the collections and properties of a **Cell** object, you can do the following:  
  
-   Return the data in the **Cell** with the [Value](../../../ado/reference/ado-md-api/value-property-ado-md.md) property.  
  
-   Return the string representing the formatted display of the **Value** property with the [FormattedValue](../../../ado/reference/ado-md-api/formattedvalue-property-ado-md.md) property.  
  
-   Return the ordinal value of the **Cell** within the **Cellset** with the [Ordinal](../../../ado/reference/ado-md-api/ordinal-property-ado-md-cell.md) property.  
  
-   Determine the position of the **Cell** within the [CubeDef](../../../ado/reference/ado-md-api/cubedef-object-ado-md.md) with the [Positions](../../../ado/reference/ado-md-api/positions-collection-ado-md.md) collection.  
  
-   Retrieve other information about the **Cell** with the standard ADO [Properties](../../../ado/reference/ado-api/properties-collection-ado.md) collection.  
  
 The **Properties** collection contains provider-supplied properties. The following table lists properties that might be available. The actual property list may differ depending upon the implementation of the provider. See the documentation for your provider for a more complete list of available properties.  
  
|Name|Description|  
|----------|-----------------|  
|BackColor|Background color used when displaying the cell.|  
|FontFlags|Bitmask detailing effects on the font.|  
|FontName|Font used to display the cell value.|  
|FontSize|Font size used to display the cell value.|  
|ForeColor|Foreground color used when displaying the cell.|  
|FormatString|Value in a formatted string.|  
  
 This section contains the following topic.  
  
-   [Properties, Methods, and Events](../../../ado/reference/ado-md-api/cell-object-properties-methods-and-events.md)  
  
## See Also  
 [Axis Example (VBScript)](../../../ado/reference/ado-md-api/axis-example-vbscript.md)   
 [Cellset Object (ADO MD)](../../../ado/reference/ado-md-api/cellset-object-ado-md.md)   
 [Positions Collection (ADO MD)](../../../ado/reference/ado-md-api/positions-collection-ado-md.md)   
 [Properties Collection (ADO)](../../../ado/reference/ado-api/properties-collection-ado.md)
