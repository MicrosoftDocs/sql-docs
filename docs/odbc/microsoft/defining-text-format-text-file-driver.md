---
description: "Defining Text Format (Text File Driver)"
title: "Defining Text Format (Text File Driver) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "text format [ODBC]"
  - "text file driver [ODBC], text format"
ms.assetid: 3af46dad-52cc-4d5c-a27e-6315d65a74e6
author: David-Engel
ms.author: v-davidengel
---
# Defining Text Format (Text File Driver)
When the Text driver is used, you can use the **Define Text Format** dialog box to define the format for columns in a selected file. This dialog box enables you to specify the schema for each data table. This information is written to a Schema.ini file in the data source directory. A separate Schema.ini file is created for each text data source directory.  
  
> [!NOTE]  
>  The same default file format applies to all new text data tables. All files created by the CREATE TABLE statement inherit those same default format values, which are set by selecting file format values in the **Define Text Format** dialog box with \<default> chosen in the **Tables** list. The Text driver does not change the format of an existing text file to match the format defined in this dialog box, but returns an error when it uses the format, such as when it attempts to retrieve data from the text file.  
  
 The following options are available in the **Define Text Format** dialog box:  
  
|Option|Information|  
|------------|-----------------|  
|**Add**|Adds a column using the values in **Data Type**, **Name**, and **Width** from the dialog box, and if applicable, the Date Separator value from Schema.ini.|  
|**Characters**|**ANSI** or **OEM**. OEM specifies a non-ANSI character set. This defaults to OEM if the format of the item selected in the **Tables** list has not been previously defined by this dialog box.|  
|**Column Name Header**|Indicates whether the columns of the first row of the selected table are to be used as column names. Either **TRUE** or **FALSE**. Defaults to FALSE if the format of the item selected in the **Tables** list has not been previously defined by this dialog box.|  
|**Columns**|Lists the column names for each column in the selected table. The order of the columns reflects the order of the columns in the table. This list is enabled if a file has been selected in the **Tables** list.|  
|**Data Type**|Can be BIT, BYTE, CHAR, CURRENCY, DATE, FLOAT, INTEGER, LONGCHAR, SHORT, or SINGLE. Date data types can be in the following formats: "dd-mmm-yy", "mm-dd-yy", "mmm-dd-yy", "yyyy-mm-dd", or "yyyy-mmm-dd". "mm" denotes numbers for months; "mmm" denotes letters for months.|  
|**Delimiter**|Specifies the custom delimiter character to be used to separate columns. Enabled when the **Custom Delimited** format is selected. The delimiter can be only one character in length, and double quotation marks (") cannot be used as the delimiter character. (The delimiter cannot be specified in hexadecimal or decimal format.)|  
|**Format**|Either delimited or fixed length. If delimited, indicates the type of delimiter used: comma (CSV), tab, or special character (custom). This defaults to **CSV Delimited** if the format of the item selected in the **Tables** list has not been previously defined by this dialog box.<br /><br /> If **Format** is fixed-length and **Column Name Header** is TRUE, the first line must be comma-delimited.|  
|**Guess**|Automatically generates the column's data type, name, and width values for the columns in the selected table by scanning the table's contents according to the **Format** box selection. Enabled when the table format is delimited. Any previously defined columns in the **Columns** list are cleared and replaced with new entries. If **Column Name Header** is not selected, column names are generated automatically as "F1", "F2", and so on. No default value is shown in the **Data Type** box.<br /><br /> This functionality works only on columns that are less than 64,513 bytes.|  
|**Modify**|Modifies the selected column using the values in **Data Type**, **Name**, and **Width**.|  
|**Name**|Displays the name of the selected column. Can be used to specify a new column name for either an existing column or a new column.<br /><br /> If **Column Name Header** is TRUE, the column name displayed is ignored.|  
|**Remove**|Deletes the selected column.|  
|**Rows to Scan**|The number of rows that Setup or the driver will scan when setting the columns and column data types based upon existing data.<br /><br /> You can enter a number from 1 to 32767 for the number of rows to scan. This defaults to 25 if the format of the item selected in the **Tables** list has not been previously defined by this dialog box. (A number outside the limit will return an error.)|  
|**Tables**|Contains a list of all files in the directory selected in the **Text Setup** dialog box that match the list of extensions specified.<br /><br /> When \<default> is selected, and one of the following is true, the values of the table attributes in the **Tables** group are written to Schema.ini (no other entries in Schema.ini are touched):<br /><br /> -   There is no Schema.ini in the specified directory.<br />-   The Schema.ini file exists, but there is no section in Schema.ini for one of the Text files (with the specified extension) in the directory.<br />-   The section for a Text file exists in Schema.ini, but the body is empty.<br /><br /> When \<default> is selected, the **Columns** group is disabled.|  
|**Width**|The width of the column can be changed for CHAR or LONGCHAR columns. The width defaults to 1 if the format of the item selected in the **Tables** list has not been previously defined by this dialog box.<br /><br /> For other data types, the width control is disabled and no value is displayed.|
