---
title: "Set Fast Parse | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
ms.assetid: dcd1dc09-6eaf-440b-9ce6-fef779ff794f
author: janinezhang
ms.author: janinez
manager: craigg
---
# Set Fast Parse
  The fast parse property must be set for each column of the source or transformation that uses fast parse. To set the property, use the Advanced editor of the Flat File source and Data Conversion transformation.  
  
### To set fast parse  
  
1.  Right-click the Flat File source or Data Conversion transformation, and then click **Show Advanced Editor**.  
  
2.  In the **Advanced Editor** dialog box, click the **Input and Output Properties** tab.  
  
3.  In the **Inputs and Outputs** pane, click the column for which you want to enable fast parse.  
  
4.  In the Properties window, expand the **Custom Properties** node, and then set the `FastParse` property to `True`.  
  
5.  Click **OK**.  
  
  
