---
title: "Create a relationship in Analysis Services tabular models | Microsoft Docs"
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
# Create a relationship 
[!INCLUDE[ssas-appliesto-sqlas-aas](../../includes/ssas-appliesto-sqlas-aas.md)]
  If the tables in your data source do not have existing relationships, or if you add new tables, you can use the tools in the model designer to create new relationships. For information about how relationships are used in tabular models, see [Relationships](../../analysis-services/tabular-models/relationships-ssas-tabular.md).  
  
## Create a relationship between two tables  
  
#### To create a relationship between two tables in Diagram View (Click and drag)  
  
1.  In [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)], click the **Model** menu, then click **Model View**, and then click **Diagram View**.  
  
2.  Click (and hold) on a column within a table, then drag the cursor to a related lookup column in a related lookup table, and then release. The relationship will be created in the correct order automatically.  
  
#### To create a relationship between two tables in Diagram View (Right-click)  
  
1.  In [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)], click the **Model** menu, then click **Model View**, and then click **Diagram View**.  
  
2.  Right-click a table heading or column, and then click **Create Relationship**.  
  
3.  In the **Create Relationship** dialog box, click the down arrow for **Table**, and select a table from the dropdown list.  
  
     In a "one-to-many" relationship, this table should be on the "many" side.  
  
4.  For **Column**, select the column that contains the data that is related to **Related Lookup Column**. The column is automatically selected if you right-clicked on a column to create the relationship.  
  
5.  For **Related Lookup Table**, select a table that has at least one column of data that is related to the table you just selected for **Table**.  
  
     In a "one-to-many" relationship, this table should be on the "one" side, meaning that the values in the selected column do not contain duplicates. If you attempt to create the relationship in the wrong order (one-to-many instead of many-to-one), an icon will appear next to the **Related Lookup Column** field. Reverse the order to create a valid relationship.  
  
6.  For **Related Lookup Column**, select a column that has unique values that match the values in the column you selected for **Column**.  
  
7.  Click **Create**.  
  
#### To create a relationship between two tables in Data View  
  
1.  In [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)], click the **Table** menu, and then click **Create Relationships**.  
  
2.  In the **Create Relationship** dialog box, click the down arrow for **Table**, and select a table from the dropdown list.  
  
     In a "one-to-many" relationship, this table should be on the "many" side.  
  
3.  For **Column**, select the column that contains the data that is related to **Related Lookup Column**.  
  
4.  For **Related Lookup Table**, select a table that has at least one column of data that is related to the table you just selected for **Table**.  
  
     In a "one-to-many" relationship, this table should be on the "one" side, meaning that the values in the selected column do not contain duplicates. If you attempt to create the relationship in the wrong order (one-to-many instead of many-to-one), an icon will appear next to the **Related Lookup Column** field. Reverse the order to create a valid relationship.  
  
5.  For **Related Lookup Column**, select a column that has unique values that match the values in the column you selected for **Column**.  
  
6.  Click **Create**.  
  
## See also  
 [Delete relationships](../../analysis-services/tabular-models/delete-relationships-ssas-tabular.md)   
 [Relationships](../../analysis-services/tabular-models/relationships-ssas-tabular.md)  
  
  
