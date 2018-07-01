---
title: "ReadWriteMode Element | Microsoft Docs"
ms.date: 05/08/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: xmla
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# ReadWriteMode Element
[!INCLUDE[ssas-appliesto-sqlas-aas](../../../includes/ssas-appliesto-sqlas-aas.md)]
  The **ReadWriteMode** database property specifies whether the database is in **ReadWrite** mode or in **ReadOnly** mode. These are the only two possible values of the property.  
  
## Syntax  
  
```xml  
  
<Database>  
...  
   <ddlns_100_0:ReadWriteMode>...</ddlns_100_0:ReadWriteMode>  
...  
</Database>  
```  
  
## Element characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String (enumeration)|  
|Default value|ReadWrite|  
|Cardinality|0-1: Optional element that can occur more than once.|  
  
## Element relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Database](../../../analysis-services/xmla/xml-elements-properties/database-element-xmla.md)|  
|Child elements|None|  
  
## Remarks  
 Databases are created in **ReadWrite** mode only. Databases cannot be created in **ReadOnly** mode.  
  
 The value of the **ReadWriteMode** element is limited to one of the strings listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*ReadOnly*|No changes or updates can be applied to the database.|  
|*ReadWrite*|Changes and updates can be applied to the database.|  
  
## See also
 [Attach Element](../../../analysis-services/xmla/xml-elements-commands/attach-element.md)   
 [Attach and Detach Analysis Services Databases](../../../analysis-services/multidimensional-models/attach-and-detach-analysis-services-databases.md)   
 [Move an Analysis Services Database](../../../analysis-services/multidimensional-models/move-an-analysis-services-database.md)   
 [Database ReadWriteModes](../../../analysis-services/multidimensional-models/database-readwritemodes.md)   
 [Switch an Analysis Services database between ReadOnly and ReadWrite modes](../../../analysis-services/multidimensional-models/switch-an-analysis-services-database-between-readonly-and-readwrite-modes.md)  
  
  
