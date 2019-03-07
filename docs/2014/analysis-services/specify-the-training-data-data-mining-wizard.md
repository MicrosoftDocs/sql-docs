---
title: "Specify the Training Data (Data Mining Wizard) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dm.dmwizard.specifytrainingdata.f1"
ms.assetid: cb04deeb-0f89-4bba-b3f1-efccada16825
author: minewiskan
ms.author: owend
manager: craigg
---
# Specify the Training Data (Data Mining Wizard)
  Use the **Specify the Training Data** page to identify which columns to include in the mining structure. You can select columns to include in the structure even if you do not use them in all models. For example, if you want to drill through to the columns from the mining model, you can include them in the structure but not in the model.  
  
 At least one key column is required for each table that is included in the structure. The column that you pick as the key depends on whether the table is a case table or a nested table. If the table is a nested table, the key is often also the predictable column, not the relational foreign key. To learn about nested keys, see [Nested Tables &#40;Analysis Services - Data Mining&#41;](data-mining/nested-tables-analysis-services-data-mining.md).  
  
> [!NOTE]  
>  Different mining algorithms use keys differently. To learn about the different kinds of keys, see [Content Types &#40;Data Mining&#41;](data-mining/content-types-data-mining.md).  
  
 **For More Information:** [Mining Structures &#40;Analysis Services - Data Mining&#41;](data-mining/mining-structures-analysis-services-data-mining.md), [Mining Model Columns](data-mining/mining-model-columns.md), [Data Mining Wizard &#40;Analysis Services - Data Mining&#41;](data-mining/data-mining-wizard-analysis-services-data-mining.md), [Create a Relational Mining Structure](data-mining/create-a-relational-mining-structure.md)  
  
## Options  
 **Tables/Columns**  
 Displays the tables and columns that you selected on the previous page of the wizard.  
  
 **\<check box>**  
 Select the columns to include in the mining structure.  
  
 If your data source includes nested tables or multiple views, expand the column list to view the nested tables.  
  
 **Key**  
 Select to use the column as a unique identifier for the data.  
  
 For a case table, the key is usually the unique identifier.  
  
 For nested table, the **Key** indicates the identifier of a row in the context of the associated case.  
  
 **Input**  
 Select to use the column in creating predictions.  
  
> [!NOTE]  
>  This column is only available when you are creating a mining model together with the mining structure.  
  
 **Predictable**  
 Select to enable the table or column to be predicted based on additional future input.  
  
 If you also mark a nested table as predictable, the whole nested table becomes predictable. If no columns in the nested table are marked as input or predictable, the nested table will appear in the mining structure, but will be ignored in the model.  
  
 **Note** This column is only available when you are creating a mining model together with the mining structure.  
  
 **Suggest**  
 Click to open the **Suggest Related Columns** dialog box, which performs an analysis on a sample of data to identify input columns that show the greatest relationship to the selected **Predictable** column based on entropy. This analysis also applies to nested table columns or mining structures that are based on OLAP sources.  
  
 **Note** This column only available when you are creating a mining model together with the mining structure.  
  
## See Also  
 [Data Mining Wizard F1 Help &#40;Analysis Services - Data Mining&#41;](data-mining-wizard-f1-help-analysis-services-data-mining.md)   
 [Suggest Related Columns &#40;Data Mining Wizard&#41;](suggest-related-columns-data-mining-wizard.md)   
 [Specify Table Types &#40;Data Mining Wizard&#41;](specify-table-types-data-mining-wizard.md)   
 [Specify the Column's Content and Data Type &#40;Data Mining Wizard&#41;](specify-the-column-s-content-and-data-type-data-mining-wizard.md)  
  
  
