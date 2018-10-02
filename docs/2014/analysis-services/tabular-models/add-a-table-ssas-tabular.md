---
title: "Add a Table (SSAS Tabular) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: d713c432-db99-4983-acc1-52b0fdd58bd6
author: minewiskan
ms.author: owend
manager: craigg
---
# Add a Table (SSAS Tabular)
  This topic describes how to add a table from a data source from which you have previously imported data into your model. To add a table from the same data source, you can use the existing data source connection. It is recommended you always use a single connection when importing any number of tables from a single data source.  
  
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
  
## See Also  
 [Import Data &#40;SSAS Tabular&#41;](../import-data-ssas-tabular.md)   
 [Delete a Table &#40;SSAS Tabular&#41;](delete-a-table-ssas-tabular.md)  
  
  
