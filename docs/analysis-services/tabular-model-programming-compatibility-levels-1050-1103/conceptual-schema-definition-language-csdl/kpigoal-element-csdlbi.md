---
title: "KpiGoal Element (CSDLBI) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
applies_to: 
  - "SQL Server 2016 Preview"
ms.assetid: fd8afbe7-b57d-4b47-862d-eb7b2489c327
caps.latest.revision: 13
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# KpiGoal Element (CSDLBI)
  The KpiGoal element provides a reference to the column that is used to define the goal for a Key performance Indicator (KPI).  
  
 In CSDLBI, KPIs are based on measures, and the Measure element contains the formula (if any), while other metadata associated with the KPI are defined as part of the [KPI Element &#40;CSDLBI&#41;](../../../analysis-services/tabular-model-programming-compatibility-levels-1050-1103/conceptual-schema-definition-language-csdl/kpi-element-csdlbi.md).  The Kpigoal element is a subtype of the Kpi element.  
  
## Elements and Attributes  
 The following table lists the attributes that define the KpiGoal element.  
  
|Name|Is Required|Description|  
|----------|-----------------|-----------------|  
|PropertyRef|Yes|A reference to the column that contains the KPI goal value.<br /><br /> The Kpigoal element must contain exactly one PropertyRef element.<br /><br /> See [PropertyRef Element &#40;CSDLBI&#41;](../../../analysis-services/tabular-model-programming-compatibility-levels-1050-1103/conceptual-schema-definition-language-csdl/propertyref-element-csdlbi.md).|  
  
## Example  
 **Tabular**  
  
 The following sample, in CSDLBI version 1.1, shows a KPI from the Adventureworks sample model.  
  
```  
  
<Property Name="InternetCurrentSalesPerformance" Type="Double">  
  <bi:Measure>  
    <bi:Kpi StatusGraphic="Three Stars Colored">  
      <bi:KpiGoal>  
        <bi:PropertyRef Name="v_InternetCurrSalesPerf_Goal" />  
      </bi:KpiGoal>  
      <bi:KpiStatus>  
        <bi:PropertyRef Name="v_InternetCurrSalesPerf_Status" />  
      </bi:KpiStatus>  
    </bi:Kpi>  
  </bi:Measure>  
</Property>  
  
```  
  
## Example  
 **Multidimensional**  
  
 The following sample, in CSDLBI version 1.1, shows a KPI from the Contoso Operations cube.  
  
```  
<bi:Measure   
       Caption="Sum of SalesAmount"   
       ReferenceName="Sum of SalesAmount"   
       FormatString="\$#,0.00;(\$#,0.00);\$#,0.00">  
    <bi:Kpi   
       StatusGraphic="Three Circles Colored">  
      <bi:KpiGoal>  
         <bi:PropertyRef   
          Name="v_Sum_of_SalesAmount_Goal" />  
       </bi:KpiGoal>  
       <bi:KpiStatus>  
          <bi:PropertyRef   
          Name="v_Sum_of_SalesAmount_Status" />  
        </bi:KpiStatus>  
       </bi:Kpi>  
</bi:Measure>  
  
```  
  
## See Also  
 [KPI Element &#40;CSDLBI&#41;](../../../analysis-services/tabular-model-programming-compatibility-levels-1050-1103/conceptual-schema-definition-language-csdl/kpi-element-csdlbi.md)  
  
  