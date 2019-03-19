---
title: "Lesson 2: Add Data | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: 13c3a8cc-b1db-4aba-ad9b-038b7971be8d
author: minewiskan
ms.author: owend
manager: craigg
---
# Lesson 2: Add Data
  In this lesson, you will use the Table Import Wizard in [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)] to connect to the AdventureWorksDW SQL database, select data, preview, and filter the data, and then import the data into your model workspace.  
  
 By using the Table Import Wizard, you can import data from a variety of relational sources: Access, SQL, Oracle, Sybase, Informix, DB2, Teradata, and more. The steps for importing data from each of these relational sources are very similar to what is described below. Additionally, data can be selected using a stored procedure.  
  
 To learn more about importing data and the different types of data sources you can import from, see [Data Sources &#40;SSAS Tabular&#41;](data-sources-ssas-tabular.md).  
  
 Estimated time to complete this lesson: **20 minutes**  
  
## Prerequisites  
 This topic is part of a tabular modeling tutorial, which should be completed in order. Before performing the tasks in this lesson, you should have completed the previous lesson: [Lesson 1: Create a New Tabular Model Project](lesson-1-create-a-new-tabular-model-project.md).  
  
## Create a Connection  
  
#### To create a connection to a the AdventureWorksDW2012 database  
  
1.  In [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)], click on the **Model** menu, and then click **Import from Data Source**.  
  
     This launches the Table Import Wizard which guides you through setting up a connection to a data source. If **Import from Data Source** is greyed out, double click **Model.bim** in **Solution Explorer** to open the model in the designer.  
  
2.  In the **Table Import Wizard**, under **Relational Databases**, click **Microsoft SQL Server**, and then click **Next**.  
  
3.  In the **Connect to a Microsoft SQL Server Database** page, in **Friendly Connection Name**, type `Adventure Works DB from SQL`.  
  
4.  In **Server name**, type the name of the server you installed the AdventureWorksDW database.  
  
5.  In the **Database name** field, click the down arrow and select **AdventureWorksDW**, and then click **Next**.  
  
6.  In the **Impersonation Information** page, you need to specify the credentials Analysis Services will use to connect to the data source when importing and processing data. Verify **Specific Windows user name and password** is selected, and then in **User Name** and **Password**, enter your Windows logon credentials, and then click **Next**.  
  
    > [!NOTE]  
    >  Using a Windows user account and password provides the most secure method of connecting to a data source. For more information, see [Impersonation &#40;SSAS Tabular&#41;](tabular-models/impersonation-ssas-tabular.md).  
  
7.  In the **Choose How to Import the Data** page, verify **Select from a list of tables and views to choose the data to import** is selected. You want to select from a list of tables and views, so click **Next** to display a list of all the source tables in the source database.  
  
8.  In the **Select Tables and Views** page, select the check box for the following tables: **DimCustomer**, **DimDate**, **DimGeography**, **DimProduct**, **DimProductCategory**, **DimProductSubcategory**, and **FactInternetSales**.  
  
9. We want to give the tables in the model more easily understood names. Click on the cell in the **Friendly Name** column for **DimCustomer**. Rename the table by removing "Dim" from DimCustomer.  
  
10. Rename the other tables:  
  
    |Source name|Friendly Name|  
    |-----------------|-------------------|  
    |DimDate|Date|  
    |DimGeography|Geography|  
    |DimProduct|Product|  
    |DimProductCategory|Product Category|  
    |DimProductSubcategory|Product Subcategory|  
    |FactInternetSales|Internet Sales|  
  
     **DO NOT** click **Finish**.  
  
 Now that you have connected to the database, selected the tables to import, and given the tables friendly names, go to the next section, [Filter the Table Data prior to Importing](#FilterData).  
  
##  <a name="FilterData"></a> Filter the Table Data  
 The DimCustomer table that you are importing from the database contains a subset of the data from the original SQL Server Adventure Works database. You will filter out some of the columns from the DimCustomer table that aren't necessary. When possible, you will want to filter out data that will not be used in order to save in-memory space used by the model.  
  
#### To filter the table data prior to importing  
  
1.  Select the row for the **Customer** table, and then click **Preview & Filter**. The **Preview Selected Table** window opens with all the columns in the DimCustomer source table displayed.  
  
2.  Clear the checkbox at the top of the following columns:  
  
    |Customer|  
    |--------------|  
    |**SpanishEducation**|  
    |**FrenchEducation**|  
    |**SpanishOccupation**|  
    |**FrenchOccupation**|  
  
     Since the values for these columns are not relevant to Internet sales analysis, there is no need to import these columns. Eliminating unnecessary columns will make your model smaller.  
  
3.  Verify that all other columns are checked, and then click **OK**.  
  
     Notice the words **Applied filters** are now displayed in the **Filter Details** column in the **Customer** row; if you click on that link you'll see a text description of the filters you just applied.  
  
4.  Filter the remaining tables by clearing the checkboxes for the following columns in each table:  
  
    |Date|  
    |----------|  
    |**DateKey**|  
    |**SpanishDayNameOfWeek**|  
    |**FrenchDayNameOfWeek**|  
    |**SpanishMonthName**|  
    |**FrenchMonthName**|  
  
    |Geography|  
    |---------------|  
    |**SpanishCountryRegionName**|  
    |**FrenchCountryRegionName**|  
    |**IpAddressLocator**|  
  
    |Product|  
    |-------------|  
    |**SpanishProductName**|  
    |**FrenchProductName**|  
    |**FrenchDescription**|  
    |**ChineseDescription**|  
    |**ArabicDescription**|  
    |**HebrewDescription**|  
    |**ThaiDescription**|  
    |**GermanDescription**|  
    |**JapaneseDescription**|  
    |**TurkishDescription**|  
  
    |Product Category|  
    |----------------------|  
    |**SpanishProductCategoryName**|  
    |**FrenchProductCategoryName**|  
  
    |Product Subcategory|  
    |-------------------------|  
    |**SpanishProductSubcategoryName**|  
    |**FrenchProductSubcategoryName**|  
  
    |Internet Sales|  
    |--------------------|  
    |**OrderDateKey**|  
    |**DueDateKey**|  
    |**ShipDateKey**|  
  
 Now that you have previewed and filtered out unnecessary data, you can import the data. Go to the next section **Import the Selected Tables and Column Data**.  
  
##  <a name="Import"></a> Import the Selected Tables and Column Data  
 You can now import the selected data. The wizard imports the table data along with any relationships between tables. New tables and columns are created in the model using the friendly names you specified, and data that you filtered out will not be imported.  
  
#### To import the selected tables and column data  
  
1.  Review your selections. If everything looks OK, click **Finish**.  
  
     While importing the data, the wizard displays how many rows have been fetched. When all the data has been imported, a message indicating success is displayed.  
  
    > [!TIP]  
    >  To see the relationships that were automatically created between the imported tables, on the **Data preparation** row, click **Details**.  
  
2.  Click **Close**.  
  
     The wizard closes and the model designer is visible. Each table has been added as a new tab in the model designer.  
  
## Save the Model Project  
 It is important to frequently save your model project.  
  
#### To save the model project  
  
-   In [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)], click on the **File** menu, and then click **Save All**.  
  
## Next Step  
 To continue this tutorial, go to the next lesson: [Lesson 3: Rename Columns](rename-columns.md).  
  
  
