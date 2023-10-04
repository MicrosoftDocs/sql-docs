---
title: "Cell Object (ADO MD)"
description: "Cell Object (ADO MD)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "Cell"
helpviewer_keywords:
  - "Cell object [ADO MD]"
apitype: "COM"
---
# Cell Object (ADO MD)
Represents the data at the intersection of axis coordinates contained in a cellset.  
  
## Remarks  
 A **Cell** object is returned by the [Item](./item-property-ado-md-cellset.md) property of a [Cellset](./cellset-object-ado-md.md) object.  
  
 With the collections and properties of a **Cell** object, you can do the following:  
  
-   Return the data in the **Cell** with the [Value](./value-property-ado-md.md) property.  
  
-   Return the string representing the formatted display of the **Value** property with the [FormattedValue](./formattedvalue-property-ado-md.md) property.  
  
-   Return the ordinal value of the **Cell** within the **Cellset** with the [Ordinal](./ordinal-property-ado-md-cell.md) property.  
  
-   Determine the position of the **Cell** within the [CubeDef](./cubedef-object-ado-md.md) with the [Positions](./positions-collection-ado-md.md) collection.  
  
-   Retrieve other information about the **Cell** with the standard ADO [Properties](../ado-api/properties-collection-ado.md) collection.  
  
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
  
-   [Properties, Methods, and Events](./cell-object-properties-methods-and-events.md)  
  
## See Also  
 [Axis Example (VBScript)](./axis-example-vbscript.md)   
 [Cellset Object (ADO MD)](./cellset-object-ado-md.md)   
 [Positions Collection (ADO MD)](./positions-collection-ado-md.md)   
 [Properties Collection (ADO)](../ado-api/properties-collection-ado.md)