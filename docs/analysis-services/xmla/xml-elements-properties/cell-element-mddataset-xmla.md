---
title: "Cell Element (MDDataSet) (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "Cell Element (MDDataSet)"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "microsoft.xml.analysis.cell"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#Cell"
  - "urn:schemas-microsoft-com:xml-analysis#Cell"
helpviewer_keywords: 
  - "Cell element"
ms.assetid: c4ea08a4-f653-4ade-be07-b91eb5b1ef32
caps.latest.revision: 13
author: "jeannt"
ms.author: "jeannt"
manager: "erikre"
---
# Cell Element (MDDataSet) (XMLA)
  Contains information about a single cell contained by a parent [CellData](../../../analysis-services/xmla/xml-elements-properties/celldata-element-xmla.md) element.  
  
## Syntax  
  
```xml  
  
<CellData>  
   <Cell CellOrdinal="unsignedInt">  
      <!-- Zero or more cell property values -->  
      <!-- or -->  
      <Error>...</Error>  
   </Cell>  
</CellData>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|0-n: Optional element that can occur more than once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[CellData](../../../analysis-services/xmla/xml-elements-properties/celldata-element-xmla.md)|  
|Child elements|Zero or more cell property values or [Error](../../../analysis-services/xmla/xml-elements-properties/error-element-xmla.md)|  
  
## Attributes  
  
|Attribute|Description|  
|---------------|-----------------|  
|CellOrdinal|Required **unsignedInt** attribute. The ordinal position of the cell within the multidimensional dataset.|  
  
## Remarks  
 In the parent **root** element, the **Axes** element is followed by the **CellData** element, a collection of **Cell** elements that contain the property values for each cell returned in the multidimensional dataset. The **Cell** element contains the **CellOrdinal** attribute, which indicates the zero-based ordinal position of the cell within the multidimensional dataset, and one element for each cell property value associated with the cell. Each cell property value in the **Cell** element is defined by a separate XML element. The value of the cell property is the data contained by the XML element, and the name of the cell property, as defined in the **CellInfo** element of the parent root element, corresponds to the name of the XML element.  
  
 The following syntax describes a cell property value:  
  
```  
<CellProperty xsi:type="string">value</CellProperty>  
```  
  
 The data type of a cell property value is specified only for the VALUE cell property. The data types of other cell properties are determined by the cell property definition included in the **CellInfo** element. A cell property value element can be excluded if a default value has been specified (by including a **Default** element for a cell property definition contained in the **CellInfo** element) for a cell property, or if no default value has been specified and the value of the cell property is null.  
  
## Cell Property Errors  
 If a cell property cannot be returned due to an error that occurs on the instance of [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)], such as a calculation error that prevents the value from being returned for a given cell, an **Error** element replaces the contents of the cell property in question. The following XML example describes a cell property error:  
  
```  
<Cell CellOrdinal="0">  
   <Value xsi:type="xsd:double">  
      <Error>  
         <ErrorCode>2148497527</ErrorCode>  
         <Description>Unknown error</Description>  
      </Error>  
   </Value>  
</Cell>  
```  
  
## Calculating Cell Ordinal Values  
 The axis reference for a cell can be calculated based on a **CellOrdinal** attribute value. Conceptually, cells are numbered in a dataset as if the dataset were a *p*-dimensional array, where *p* is the number of axes. Cells are addressed in row-major order.  
  
 Suppose that a query requests four measures on columns and a crossjoin of two states with four quarters on rows. In following the dataset result, the **CellOrdinal** property for the part of the dataset result shown in bold text is the set {9, 10, 11, 13, 14, 15, 17, 18, 19}. This is the set because the cells are numbered in row-major order, starting with a **CellOrdinal** of 0 for the upper left cell.  
  
|State|Quarter|Unit sales|Store cost|Store sales|Sales count|  
|-----------|-------------|----------------|----------------|-----------------|-----------------|  
|California|Q1|16890|14431.09|36175.2|5498|  
||Q2|18052|15332.02|38396.75|5915|  
||Q3|18370|**15672.83**|**39394.05**|**6014**|  
||Q4|21436|**18094.5**|**45201.84**|**7015**|  
|Oregon|Q1|19287|**16081.07**|**40170.29**|**6184**|  
||Q2|15079|12678.96|31772.88|4799|  
||Q3|16940|14273.78|35880.46|5432|  
||Q4|16353|13738.68|34453.44|5196|  
|Washington|Q1|30114|25240.08|63282.86|9906|  
||Q2|29479|24953.25|62496.64|9654|  
||Q3|30538|25958.26|64997.38|10007|  
||Q4|34235|29172.72|73016.34|11217|  
  
 Applying the formula shown in the figure, axis k = 0 has Uk = 4 members, and axis k = 1 has Uk = 8 tuples. P = 2 is the total number of axes in the query. Taking the cell that is {California, Q3, Store Cost} as S0, the initial summation is i = 0 to 1. For i = 0, the tuple ordinal on axis 0 of {Store Cost} is 1. For i = 1, the tuple ordinal of {CA, Q3} is 2.  
  
 For i = 0, Ei = 1, so for i = 0 the sum is 1 * 1 = 1 and for i = 1, the sum is 2 (tuple ordinal) times 4 (the value of Ei computed as 1 \* 4), or 8. The sum of 1 + 8 is then 9, the cell ordinal for that cell.  
  
## Example  
 The following example demonstrates the structure of the **Cell** element, including the VALUE, FORMATTED_VALUE, and FORMAT_STRING cell property values for each cell.  
  
```  
<CellData>  
   <Cell CellOrdinal="0">  
      <Value xsi:type="xsd:double">16890</Value>  
      <FmtValue>16,890.00</FmtValue>  
      <FormatString>Standard</FormatString>  
   </Cell>  
   <Cell CellOrdinal="1">  
      <Value xsi:type="xsd:int">50</Value>  
      <FmtValue>50</FmtValue>  
      <FormatString>Standard</FormatString>  
   </Cell>  
   <Cell CellOrdinal="2">  
      <Value xsi:type="xsd:double">36175.2</Value>  
      <FmtValue>$36,175.20</FmtValue>  
      <FormatString>Currency</FormatString>  
   </Cell>  
</CellData>  
```  
  
## See Also  
 [MDDataSet Data Type &#40;XMLA&#41;](../../../analysis-services/xmla/xml-data-types/mddataset-data-type-xmla.md)   
 [Properties &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/xml-elements-properties.md)  
  
  