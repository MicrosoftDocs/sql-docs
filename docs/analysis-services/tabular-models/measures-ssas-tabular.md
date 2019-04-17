---
title: "Measures in Analysis Services tabular models | Microsoft Docs"
ms.date: 05/07/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: tabular-models
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Measures
[!INCLUDE[ssas-appliesto-sqlas-aas](../../includes/ssas-appliesto-sqlas-aas.md)]
  In tabular models, a measure is a calculation created using a DAX formula for use in a reporting client. Measures are evaluated based on fields, filters, and slicers users select in the reporting client application.  
  
##  <a name="bkmk_understanding"></a> Benefits  
 Measures can be based on standard aggregation functions, such as AVERAGE, COUNT, or SUM, or you can define your own formula by using DAX. In addition to the formula, each measure has properties defined by the measure data type, such as Name, Table Detail, Format, and Decimal Places.  
  
 When measures have been defined in a model, users can then add them to a report or PivotTable. Depending on perspectives and roles, measures appear in the Field List with their associated table, and are available to all users of the model. Measures are usually created in Fact tables; however, measures can be independent of the table it is associated with.  
  
 It is important to understand the fundamental differences between a calculated column and a measure. In a calculated column, a formula evaluates to a value for each row in the column. For example, in a FactSales table, a calculated column named TotalProfit with the following formula calculates a value for total profit for each row (one row per sale) in the FactSales table:  
  
```  
=[SalesAmount] - [TotalCost] - [ReturnAmount]  
```  
  
 The TotalProfit calculated column can then be used in a reporting client just as any other column.  
  
 A measure, on the other hand, evaluates to a value based on a user selection; a filter context set in a PivotTable or report. For example, a measure in the FactSales table is created with the following formula:  
  
```  
Sum of TotalProfit: =SUM([TotalProfit])  
```  
  
 A sales analyst, using Excel, wants to know the total profit for a category of products. Each product category is comprised of multiple products. The sales analyst selects the ProductCategoryName column and adds it to the Row Labels filter window of a PivotTable; a row for each product category is then displayed in the PivotTable. The user then selects the Sum of TotalProfit measure. A measure will by default be added to the Values filter window. The measure calculates the sum of total profit and displays the results for each product category. The sales analyst can then further filter the sum of total profit for each product category by using a slicer, for example, adding CalendarYear as a slicer to view the sum of total profit for each product category by year.  
  
|ProductCategoryName|Sum of TotalProfit|  
|-------------------------|------------------------|  
|Audio|$2,731,061,308.69|  
|Cameras and Camcorders|$620,623,675.75|  
|Computers|$392,999,044.59|  
|Tv and Video|$946,989,702.51|  
|**Grand Total**|**$4,691,673,731.53**|  
  
##  <a name="bkmk_def_mg"></a> Defining measures by using the measure grid  
 Measures are created at design time by using the measure grid in the model designer. Each table has a measure grid. By default, the measure grid is displayed below each table in the model designer. You can also choose not to view the measure grid for a particular table. To toggle the display of a table's measure grid, click the **Table** menu, and then click **Show Measure Grid**.  
  
 In the measure grid, you can create measures in the following ways:  
  
-   Click on an empty cell in the measure grid, and then type a DAX formula in the formula bar. When you click ENTER to complete the formula, the measure will then appear in the cell in the measure grid.  
  
-   Create a measure using a standard aggregation function by clicking on a column, then clicking on the AutoSum button (∑) on the toolbar, and then clicking on a standard aggregation function. Standard aggregations are: Sum, Average, Count, DistinctCount, Max, Min. Measures created using the AutoSum button will always appear in the Measure grid directly beneath the column.  
  
 By default, when using AutoSum, the name of the measure is defined by the name of the associated column followed by a colon, followed by the formula. The name can be changed in the formula bar or in the **Measure Name** property setting in the Properties window. When creating a measure by using a custom formula, you can type a name in the formula bar, followed by a colon, then followed by the formula, or you can type a name in the **Measure Name** property setting in the Properties window.  
  
 It's important to name measures carefully. The measure name will appear with the associated table in the Field List of the reporting client. A KPI will also be named according to the base measure. A measure cannot have the same name as any column in any table in the model.  
  
> [!TIP]  
>  You can group measures from multiple tables into one table by creating an empty table, and then moving or create new measures into it. Keep in-mind, you may need to include table names in DAX formulas when referencing columns in other tables.  
  
 If perspectives have been defined for the model, measures are not automatically added to any of those perspectives. You must manually add measures to a perspective by using the Perspectives dialog box. To learn more, see [Perspectives](../../analysis-services/tabular-models/perspectives-ssas-tabular.md).  
  
##  <a name="bkmk_properties"></a> Measure Properties  
 Each measure has properties that define it. Measure properties, along with the associated column properties can be edited in the Properties window. Measures have the following properties:  
  
|Property|Default setting|Description|  
|--------------|---------------------|-----------------|  
|**Description**|Blank|Description of the measure. The description will not appear with the measure in a reporting client.|  
|**Format**|Automatically determined from the data type of the column referenced in the formula expression.|Format of the measure. For example, currency or percentage.|  
|**Formula**|The formula entered in the formula bar when the measure was created.|The formula of the measure.|  
|**Measure Name**|If AutoSum is used, the measure name will precede the column name followed by a colon. If a custom formula is entered, type a name followed by a colon, and then type the formula.|The name of the measure as it is displayed in a reporting client Field List.|  
  
##  <a name="bkmk_KPI"></a> Using a Measure in a KPI  
 A KPI (Key Performance Indicator) is defined by a *Base* value, defined by a measure, against a *Target* value, also defined by a measure or by an absolute value. A KPI also includes *Status*, a calculation where the Base value evaluates against the Target value between thresholds, displayed in graphical format. KPIs are often used by business professionals to identify trends in critical business metrics.  
  
 Any measure can serve as the Base measure of a KPI. To create a KPI, in the measure grid, right-click a measure, and then click **Create KPI**. The Key Performance Indicator dialog box appears where you can then specify a target value (defined by a measure or an absolute value) and define status thresholds, and a graphic type. To learn more, see [KPIs](../../analysis-services/tabular-models/kpis-ssas-tabular.md).  
  
##  <a name="bkmk_rel_tasks"></a> Related tasks  
  
|Topic|Description|  
|-----------|-----------------|  
|[Create and Manage Measures](../../analysis-services/tabular-models/create-and-manage-measures-ssas-tabular.md)|Describes how to create and manage measures using the measure grid in the model designer.|  
  
## See Also  
 [KPIs](../../analysis-services/tabular-models/kpis-ssas-tabular.md)   
 [Create and Manage KPIs](../../analysis-services/tabular-models/create-and-manage-kpis-ssas-tabular.md)   
 [Calculated Columns](../../analysis-services/tabular-models/ssas-calculated-columns.md)  
  
  
