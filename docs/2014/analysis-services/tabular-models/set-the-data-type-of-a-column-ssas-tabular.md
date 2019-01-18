---
title: "Set the Data Type of a Column (SSAS Tabular) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: 34e2d508-7b64-4503-a4f0-c6c6ad5f8a44
author: minewiskan
ms.author: owend
manager: craigg
---
# Set the Data Type of a Column (SSAS Tabular)
  When you import data or paste data into a model, the model designer will automatically detect and apply data types. After you have added the data to the model, you can manually modify the data type of a column to change how data is stored. If you just want to change the format of how the data is displayed without changing the way it is stored, you can do that instead.  
  
### To change the data type or display format for a column  
  
1.  In the model designer, select the column for which you want to change the data type or display format.  
  
2.  In the column **Properties** window, do one of the following:  
  
    -   In the **Data Format** property, select a different data format.  
  
    -   In the **Data Type** property, select a different data type.  
  
## Considerations when Changing Data Types  
 Sometimes when you try to change the data type of a column or select a data conversion, one of the following errors might occur:  
  
-   Failed to change data type  
  
-   Failed to change column data type  
  
 These errors might occur even if the data type is available as an option in the Data Type dropdown list. This section explains the cause of these errors and how you can correct them.  
  
### Understanding Automatically Determined Data Types  
 When you add data to a model, the model designer checks the columns of data to see what data types each column contains. If the data in that column is consistent, is assigns the most precise data type to the column.  
  
 However, if you add data from Excel or another source that does not enforce the use of a single data type within each column, the model designer will assign a data type that accommodates all values within the column. Therefore, if a column contains numbers of different types, such as integers, long numbers, and currency, the model designer will use a decimal data type. Alternatively, if a column mixes numbers and text, the model designer will use the text data type. The model designer does not provide a data type similar to the General data type available in Excel.  
  
 Therefore, if a column contains both numbers and text values, you will not be able to convert the column to a numeric data type.  
  
 The following data types are available in business intelligence semantic models:  
  
|Model data types|  
|----------------------|  
|Text<br /><br /> Decimal Number<br /><br /> Whole Number<br /><br /> Currency<br /><br /> TRUE/FALSE<br /><br /> Date|  
  
 If you find that your data has a wrong data type, or at least a different one than you wanted, you have several options:  
  
-   You can re-import the data. To do this, open the existing connection to the data source and re-import the column. Depending on the data source type, you might be able to apply a filter during import to remove problem values.  
  
-   You can create a DAX formula in a calculated column to create a new value of the desired data type. For example, the TRUNC function can be used to change a decimal number to a whole integer, or you can combine information functions and logical functions to test and convert values.  
  
### Understanding Data Conversion  
 If an error occurs when you select a data conversion option, it might be that the current data type of the column does not support the selected conversion. Not all conversions are allowed for all data types. For example, you can only change a column to a Boolean data type if the current data type of the column is either a number (whole or decimal) or text. Therefore, you must choose an appropriate data type for the data in the column.  
  
 After you choose an appropriate data type, the model designer will warn you about possible changes to your data, such as loss of precision, or truncation. Click OK to accept and change your data to the new data type.  
  
 If the data type is supported, but the model designer finds values that are not supported within the new data type, you will get another error, and will need to correct the data values before proceeding.  
  
 For detailed information about the data types used in business intelligence semantic models, how they are implicitly converted, and how different data types are used in formulas, see [Data Types Supported &#40;SSAS Tabular&#41;](data-types-supported-ssas-tabular.md).  
  
## See Also  
 [Data Types Supported &#40;SSAS Tabular&#41;](data-types-supported-ssas-tabular.md)  
  
  
