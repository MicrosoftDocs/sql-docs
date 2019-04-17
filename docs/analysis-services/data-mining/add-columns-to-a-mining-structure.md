---
title: "Add Columns to a Mining Structure | Microsoft Docs"
ms.date: 05/01/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: data-mining
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Add Columns to a Mining Structure
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  Use Data Mining Designer in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] to add columns to a mining structure after you have defined it in the Data Mining Wizard. You can add any column that exists in the data source view that was used to define the mining structure.  
  
> [!NOTE]  
>  You can add multiple copies of columns to a mining structure; however, you should avoid using more than one instance of the column within the same model, to avoid false correlations between the source and the derived column.  
  
### To add a column to a mining structure  
  
1.  Select the **Mining Structure** tab in Data Mining Designer.  
  
2.  Right-click the mining structure and select **Add a Column**.  
  
     The **Select a Column** dialog box opens.  
  
3.  Under **Source table**, select the table in the data source view where the column resides.  
  
4.  Under **Source column**, select the column that you want to add to the mining structure.  
  
5.  [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
> [!NOTE]  
>  If you add a column that already exists, a copy will be included in the structure, and the name appended with a "1". You can change the name of the copied column to something more descriptive by typing a new name in the **Name** property of the mining structure column.  
  
## See Also  
 [Mining Structure Tasks and How-tos](../../analysis-services/data-mining/mining-structure-tasks-and-how-tos.md)  
  
  
