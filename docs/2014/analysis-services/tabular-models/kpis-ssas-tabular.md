---
title: "KPIs (SSAS Tabular) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: a0524602-5239-45a7-8c44-2477302a3637
author: minewiskan
ms.author: owend
manager: craigg
---
# KPIs (SSAS Tabular)
  A *KPI* (Key Performance Indicator), in a tabular model, is used to gauge performance of a value, defined by a *Base* measure, against a *Target* value, also defined by a measure or by an absolute value. This topic provides tabular model authors a basic understanding of KPIs in a tabular model.  
  
 Sections in this topic:  
  
-   [Benefits](#bkmk_benefits)  
  
-   [Example](#bkmk_example)  
  
-   [Create and Edit KPIs](#bkmk_create)  
  
-   [Related Tasks](#bkmk_related_tasks)  
  
##  <a name="bkmk_benefits"></a> Benefits  
 In business terminology, a Key Performance Indicator (KPI) is a quantifiable measurement for gauging business objectives. A KPI is frequently evaluated over time. For example, the sales department of an organization may use a KPI to measure monthly gross profit against projected gross profit. The accounting department may measure monthly expenditures against revenue to evaluate costs, and a human resources department may measure quarterly employee turnover. Each is an example of a KPI. Business professionals frequently consume KPIs that are grouped together in a business scorecard to obtain a quick and accurate historical summary of business success or to identify trends.  
  
 A KPI in a tabular model includes:  
  
 **Base Value**  
 A Base value is defined by a measure that resolves to a value. This value, for example, can be an aggregate of actual Sales or a calculated measure such as Profit for a given period.  
  
 **Target value**  
 A Target value is defined by a measure that resolves to a value, or by an absolute value. For example, a target value could be the amount by which the business managers of an organization want to increase sales or profit.  
  
 **Status thresholds**  
 A Status threshold is defined by the range between a low and high threshold or by a fixed value. The Status threshold displays with a graphic to help users easily determine the status of the Base value compared to the Target value.  
  
##  <a name="bkmk_example"></a> Example  
 The sales manager at Adventure Works wants to create a PivotTable that she can use to quickly display whether or not sales employees are meeting their sales quota for a given period (year). For each sales employee, she wants the PivotTable to display the actual sales amount in dollars, the sales quota amount in dollars, and a simple graphic display showing the status of whether or not each sales employee is below, at, or above their sales quota. She wants to be able to slice the data by year.  
  
 To do this, the sales manager enlists the help of her organization's BI solution developer to add a Sales KPI to the AdventureWorks Tabular Model. The sales manager will then use [!INCLUDE[ofprexcel](../../includes/ofprexcel-md.md)] to connect to the Adventure Works Tabular Model as a data source and create a PivotTable with the fields (measures and KPI) and slicers to analyze whether or not the sales force is meeting their quotas.  
  
 In the model, a measure on the SalesAmount column in the FactResellerSales table, which gives the actual sales amount in dollars for each sales employee is created. This measure will define the Base value of the KPI.  
  
 The Sales measure is created with the following formula:  
  
```  
Sales:=Sum(FactResellerSales[SalesAmount])  
```  
  
 The SalesAmountQuota column in the FactSalesQuota table has a sales amount quota defined for each employee. The values in this column will serve as the Target measure (value) in the KPI.  
  
 The SalesAmountQuota measure is created with the following formula:  
  
```  
Target SalesAmountQuota:=Sum(FactSalesQuota[SalesAmountQuota])  
```  
  
> [!NOTE]  
>  There is a relationship between the EmployeeKey column in the FactSalesQuota table and the EmployeeKey in the DimEmployees table. This relationship is necessary so that each sales employee in the DimEmployee table is represented in the FactSalesQuota table.  
  
 Now that measures have been created to serve as the Base value and Target value of the KPI, the Sales measure is extended to a new Sales KPI. In the Sales KPI, the Target SalesAmountQuota measure is defined as the Target value. The Status threshold is defined as a range by percentage, the target of which is 100% meaning actual sales defined by the Sales measure met the quota amount defined in the Target SalesAmoutnQuota measure. Low and High percentages are defined on the status bar, and a graphic type is selected.  
  
 The sales manager can now create a PivotTable adding the KPI's Base value, Target value, and Status to the Values field. The Employees column is added to the RowLabel field, and the CalendarYear column is added as a Slicer.  
  
 The sales manager can now slice by year the actual sales amount, sales quota amount, and status for each sales employee. She can analyze sales trends over years to determine whether or not she needs to adjust the sales quota for a sales employee.  
  
##  <a name="bkmk_create"></a> Create and Edit KPIs  
 To create KPIs, in the model designer, you will use the Key Performance Indicator dialog box. Since KPIs must be associated with a measure, you create a KPI by extending a measure that evaluates to a Base value, and then either creating a measure that evaluates to a Target value or by entering an absolute value. After the Base measure (value) and Target value is defined, you can then define the status threshold parameters between the Base and Target values. The status is displayed in a graphical format using selectable icons, bars, graphs, or colors. The Base and Target values, as well as the Status can then be added to a report or PivotTable as values that can be sliced against other data fields.  
  
 To view the Key Performance Indicator dialog box, in the measure grid for a table, right click a measure that will serve as the Base value, and then click **Create KPI**. After a measure has been extended to a KPI as a Base value, an icon will appear alongside the measure name in the measure grid identifying the measure as associated with a KPI.  
  
##  <a name="bkmk_related_tasks"></a> Related Tasks  
  
|Topic|Description|  
|-----------|-----------------|  
|[Create and Manage KPIs &#40;SSAS Tabular&#41;](kpis-ssas-tabular.md)|Describes how to create a KPI with a Base measure, a Target measure, and status thresholds.|  
  
## See Also  
 [Measures &#40;SSAS Tabular&#41;](measures-ssas-tabular.md)   
 [Perspectives &#40;SSAS Tabular&#41;](perspectives-ssas-tabular.md)  
  
  
