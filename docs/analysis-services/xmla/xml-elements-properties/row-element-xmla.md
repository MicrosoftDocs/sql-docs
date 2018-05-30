---
title: "row Element (XMLA) | Microsoft Docs"
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
# row Element (XMLA)
[!INCLUDE[ssas-appliesto-sqlas-aas](../../../includes/ssas-appliesto-sqlas-aas.md)]
  Contains a single row of data for a [root](../../../analysis-services/xmla/xml-elements-properties/root-element-xmla.md) element that contains tabular data returned by a [Discover](../../../analysis-services/xmla/xml-elements-methods-discover.md) or [Execute](../../../analysis-services/xmla/xml-elements-methods-execute.md) method call.  
  
## Syntax  
  
```xml  
  
<root xmlns="urn:schemas-microsoft-com:xml-analysis:rowset">  
   <row>  
      <!-- One or more column elements -->  
   </row>  
</root>  
```  
  
## Element characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|0-n: Optional element that can occur more than once.|  
  
## Element relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[root](../../../analysis-services/xmla/xml-elements-properties/root-element-xmla.md) (using the [Rowset](../../../analysis-services/xmla/xml-data-types/rowset-data-type-xmla.md) data type)|  
|Child elements|One or more column elements.|  
  
## Remarks  
 Each row returned by a **root** element that contains tabular data has a corresponding **row** element. Each column in the **root** element is represented by a separate XML element. The value of the column for the **row** element is the data contained by the XML element, and the name of the column corresponds to the name of the XML element.  
  
 There are two ways to express a null value for a column within a row:  
  
-   A missing column element implies that the column is null.  
  
-   The column element may use the `xsi:nil='true'` attribute to indicate that it has a null value.  
  
 For example, if a row has a single column called, Store_Name, and its value is NULL, it can be represented as either:  
  
```  
<row>  
</row>  
```  
  
 Or:  
  
```  
<row>  
   <Store_name xsi:nil='true'/>  
</row>  
```  
  
 If a column element contains an error, an **Error** element provides information about an error, as described in the following example:  
  
```  
<row>   <Store_name>  
      <Error xmlns="urn:schemas-microsoft-com:xml-analysis:exception">  
         <ErrorCode>3238658054</ErrorCode>  
         <Description>The object [X] was not found in the cube when [X] was parsed.</Description>  
      </Error>  
   </Store_name>  
</row>  
```  
  
 For more information about column naming and schema information for tabular data, see [Rowset Data Type &#40;XMLA&#41;](../../../analysis-services/xmla/xml-data-types/rowset-data-type-xmla.md).  
  
## See also
 [Properties &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/xml-elements-properties.md)  
  
  
