---
title: "Add Columns to a Mining Structure | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "mining structures [Analysis Services], columns"
  - "columns [data mining], mining structure columns"
  - "adding columns"
ms.assetid: 3f879344-9f66-4178-851a-e8c5ccccf4cb
author: minewiskan
ms.author: owend
manager: craigg
---
# Add Columns to a Mining Structure
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
 [Mining Structure Tasks and How-tos](mining-structure-tasks-and-how-tos.md)  
  
  
