---
title: "Add a Nested Table to a Mining Structure | Microsoft Docs"
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
# Add a Nested Table to a Mining Structure
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  Use Data Mining Designer to add a nested table to a mining structure after it has been created by the Data Mining Wizard.  
  
### To add a nested table to a mining structure  
  
1.  Select the **Mining Structure** tab in Data Mining Designer.  
  
2.  Right-click the mining structure to which you want to add a table column.  
  
3.  Select **Add a Nested Table**.  
  
     The **Select a Nested Table Key Column** dialog box opens.  
  
4.  Under **Nested table**, select the table that you want to nest in the mining structure.  
  
5.  Under **Source column**, select the key column for the nested table.  
  
6.  Click **OK**.  
  
     A new table column that contains the key column is added to the mining structure. For information about how to add additional columns, see [Add Columns to a Mining Structure](../../analysis-services/data-mining/add-columns-to-a-mining-structure.md).  
  
## See Also  
 [Mining Structure Tasks and How-tos](../../analysis-services/data-mining/mining-structure-tasks-and-how-tos.md)  
  
  
