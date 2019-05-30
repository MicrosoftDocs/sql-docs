---
title: "Add a table to an Analysis Services tabular model | Microsoft Docs"
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
# Add a table
[!INCLUDE[ssas-appliesto-sqlas-aas](../../includes/ssas-appliesto-sqlas-aas.md)]
  This article describes how to add a table from a data source from which you have previously imported data into your model. To add a table from the same data source, you can use the existing data source connection. It is recommended you always use a single connection when importing any number of tables from a single data source.  
  
### To add a table from an existing data source  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], click the **Model** menu, and then click **Existing Connections**.  
  
2.  On the **Existing Connections** page, select the connection to the data source that has the table you want to add, and then click **Open**.  
  
3.  On the **Select Tables and Views** page, select the table from the data source you want to add to your model.  
  
    > [!NOTE]  
    >  The **Select Tables and Views** page will not show tables that were previously imported as checked.  If you select a table that was previously imported using this connection, and you did not give the table a different friendly name, a 1 will be appended to the friendly name. You do not need to re-select any tables that were previously imported by using this connection.  
  
4.  If necessary, use **Preview & Filter** to select only certain columns or apply filters to the data to be imported.  
  
5.  Click **Finish** to import the new table.  
  
> [!NOTE]  
>  When importing multiple tables at the same time from a single data source, any relationships between those tables at the source will automatically be created in the model. When adding a table later; however, you may need to manually create relationships in the model between newly added tables and the tables that were previously imported.  
  
## See also  
 [Import Data](http://msdn.microsoft.com/library/6617b2a2-9f69-433e-89e0-4c5dc92982cf)   
 [Delete a Table](../../analysis-services/tabular-models/delete-a-table-ssas-tabular.md)  
  
  
