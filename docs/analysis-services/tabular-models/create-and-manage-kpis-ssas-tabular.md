---
title: "Create and manage KPIs in Analysis Services tabular models | Microsoft Docs"
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
# Create and manage KPIs 
[!INCLUDE[ssas-appliesto-sqlas-aas](../../includes/ssas-appliesto-sqlas-aas.md)]
  This article describes how to create, edit, or delete a KPI (Key Performance Indicator) in a tabular model. To create a KPI, you select a measure that evaluates to the KPI's Base value. You then use the Key Performance Indicator dialog box to select a second measure or an absolute value that evaluates to a target value. You can then define status thresholds that measure the performance between the Base and Target measures.  
  
## Tasks  
  
> [!IMPORTANT]  
>  Before creating a KPI, you must first create a Base measure that evaluates to value. You then extend the Base measure to a KPI. How to create measures are described in another topic, [Create and Manage Measures](../../analysis-services/tabular-models/create-and-manage-measures-ssas-tabular.md). A KPI also requires a target value. This value can be from another pre-defined measure or an absolute value. Once you have extended a Base measure to a KPI, you can then select the target value and define status thresholds in the Key Performance Indicator dialog box.  
  
###  <a name="bkmk_create_KPI"></a> To create a KPI  
  
1.  In the measure grid, right-click the measure that will serve as the Base measure (value), and then click **Create KPI**.  
  
2.  In the **Key Performance Indicator** dialog box, in **Define target value**, select from one of the following:  
  
     Select **Measure**, and then select a target measure from the listbox.  
  
     Select **Absolute value**, and then type a numerical value.  
  
3.  In **Define status thresholds**, click and slide the low and high threshold values.  
  
4.  In **Select icon style**, click an image type.  
  
5.  Click on **Descriptions**, and then type descriptions for KPI, Value, Status, and Target.  
  
> [!TIP]  
>  You can use the Analyze in Excel feature to test your KPI. For more information, see [Analyze in Excel](../../analysis-services/tabular-models/analyze-in-excel-ssas-tabular.md).  
  
###  <a name="bkmk_edit_KPI"></a> To edit a KPI  
  
-   In the measure grid, right-click the measure that serves as the Base measure (value) of the KPI, and then click **Edit KPI Settings**.  
  
###  <a name="bkmk_delete"></a> To delete a KPI and the base measure  
  
-   In the measure grid, right-click the measure that serves as the Base measure (value) of the KPI, and then click **Delete**.  
  
###  <a name="bkmk_delete_KPI"></a> To delete a KPI, but keep the base measure  
  
-   In the measure grid, right-click the measure that serves as the Base measure (value) of the KPI, and then click **Delete KPI**.  
  
## ALT shortcuts  
  
|UI section|Key command|  
|----------------|-----------------|  
|KPI base measure|ALT+B|  
|KPI Status|ALT+S|  
|Measure|ALT+M|  
|Absolute value|ALT+A|  
|Define status thresholds|ALT+U|  
|Select icon style|ALT+I|  
|Trend|ALT+T|  
|Descriptions|ALT+D|  
|Trend|ALT+T|  
  
## See also  
 [KPIs](../../analysis-services/tabular-models/kpis-ssas-tabular.md)   
 [Measures](../../analysis-services/tabular-models/measures-ssas-tabular.md)   
 [Create and managemeasures](../../analysis-services/tabular-models/create-and-manage-measures-ssas-tabular.md)  
  
  
