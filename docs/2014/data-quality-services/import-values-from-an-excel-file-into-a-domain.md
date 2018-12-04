---
title: "Import Values from an Excel File into a Domain | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: data-quality-services
ms.topic: conceptual
f1_keywords: 
  - "sql12.dqs.kb.failingvalues.f1"
  - "sql12.dqs.kb.importfailing.f1"
  - "sql12.dqs.kb.importselect.f1"
ms.assetid: 04cde693-2043-477f-8417-fcc463ca7195
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Import Values from an Excel File into a Domain
  This topic describes how to import values from an Excel file into a domain in [!INCLUDE[ssDQSnoversion](../includes/ssdqsnoversion-md.md)] (DQS). Using an Excel file to import domain values into the [!INCLUDE[ssDQSClient](../includes/ssdqsclient-md.md)] application simplifies the knowledge generation process, saving time and effort. It enables people who have a list of valid data values in an Excel file or a text file to import those values into a domain. From an Excel file you can import domain values into a domain or domains into a knowledge base. (See [Import Domains from an Excel File in Knowledge Discovery](../../2014/data-quality-services/import-domains-from-an-excel-file-in-knowledge-discovery.md) for more information about importing domains into a knowledge base.) Exporting to an Excel file is not supported.  
  
 You can import data values in two ways:  
  
-   Create a new domain and then import values into it from an Excel file, in which case all values are added to the domain.  
  
-   Import values into an existing, populated domain, in which case only new values are imported. All values that already exist will not be imported.  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Prerequisites"></a> Prerequisites  
 To import domains from an Excel file, Excel must be installed on the computer that the [!INCLUDE[ssDQSClient](../includes/ssdqsclient-md.md)] application is installed on in order to import domain values or a complete domain; you must have created an Excel file with domain values (see [How the import works](#How)); and you must have created and opened a knowledge base to import the domain into.  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 You must have the dqs_kb_editor or the dqs_administrator role on the DQS_MAIN database to import domains values from an Excel file.  
  
##  <a name="Import"></a> Import values from an Excel file into a domain  
  
1.  [!INCLUDE[ssDQSInitialStep](../includes/ssdqsinitialstep-md.md)] [Run the Data Quality Client Application](../../2014/data-quality-services/run-the-data-quality-client-application.md).  
  
2.  In the [!INCLUDE[ssDQSClient](../includes/ssdqsclient-md.md)] home screen, open a knowledge base in the Domain Management activity.  
  
3.  If adding values to a new domain, create a new domain using the **Create a Domain** icon, and then select the new domain in the domain list.  
  
4.  If adding values to an existing domain, select the domain in the domain list.  
  
5.  Click the **Domain Values** tab, click the **Import Values** icon in the icon bar, and then click **Import valid values from Excel**.  
  
6.  In the **Import Domain Values** dialog box, click **Browse**.  
  
7.  In the **Select file** dialog box, move to the folder that contains the Excel file that you want to import domain values from, select the file (with a .xlsx, .xls, or .csv extension), and then click **Open**. The file must be either on the client that you run DQS from, or in a share file that the user has access to.  
  
8.  From the **Worksheet** drop-down list, select the worksheet that you are importing from.  
  
9. Select **Use first row as header** if the first row in the spreadsheet represents the domain name, and all other rows represent valid domain values.  
  
10. Click **OK**. A progress bar is displayed, with an indication of how many values have been imported successfully, how many were not imported, and the total number of values. Click the **Cancel** button to cancel the process.  
  
11. Verify that "Import complete" is displayed in the **Import Domain Values** dialog box. See which values were successfully imported, and which were not, in this dialog box. It indicates the name of the file and the file's path, the completion status of the operation, how many values have been imported successfully, how many values were not imported, and the total number of values processed.  
  
12. For those values that were not successfully imported, click **Log** to display the **Import Domain Values - Failing Values** dialog box to see why the import operation failed. The **Failing Value** column shows the values that failed to be imported from an Excel file into a domain, and the **Reason** column explains why the import failed. Click **Copy to clipboard** to copy the **Failing Value** table onto the clipboard, from which you can copy it into another program, such as an Excel spreadsheet or a Notepad file. Click **OK** to close the **Failing Values** dialog box.  
  
13. Click **OK** to complete the import operation and close the dialog box. When the import has completed successfully, the domain values list on the **Domain Values** page is refreshed and will include the new imported values. The filter is changed to **All Values** and **Show Only New** is selected. When **Show Only New** is selected after the import operation, only the values imported from the Excel file will be displayed.  
  
14. Click **Finish** to add the values to the knowledge base.  
  
##  <a name="FollowUp"></a> Follow Up: After Importing Values from an Excel File into a Domain  
 After you import values into a domain, you can perform other domain management tasks on the domain, you can perform knowledge discovery to add knowledge to the domain, or you can add a matching policy to the domain. For more information, see [Perform Knowledge Discovery](../../2014/data-quality-services/perform-knowledge-discovery.md), [Managing a Domain](../../2014/data-quality-services/managing-a-domain.md), or [Create a Matching Policy](../../2014/data-quality-services/create-a-matching-policy.md).  
  
##  <a name="Synonyms"></a> Importing Synonyms  
 Synonyms are imported as follows:  
  
-   First, all values are imported, then the synonym connection is established.  
  
-   If it is impossible to connect synonym values, an error will appear in the log screen. It is possible that the leading values and the synonyms in the file will be imported to the domain, but will not be set as synonyms.  
  
 The following apply to the process of setting synonym connections:  
  
-   If the leading value in the Excel file already exists in the domain as a synonym of a different value, you will have to set the synonyms manually (e.g., in the Excel file we want that value A will be the leading value for value B, but in the domain value A appears as a synonym of value C). In addition to setting synonyms manually after the import completes, you can also unlink values that are at present synonyms (for example, unlink values A and C above), and then import the file.  
  
-   If the synonym is already connected to a different leading value, you will have to set the synonyms manually.  
  
-   If the values cannot be connected manually in the application for any reason, it will not be applicable through the import operation.  
  
##  <a name="How"></a> How the import works  
 The following values are imported by this operation:  
  
 In the import operation, DQS imports from an Excel file as follows:  
  
-   Correct values and new values are imported. If one or more of the imported domain values already exists, the values will not be imported.  
  
-   A value that contradicts a domain rule will be imported as an invalid value.  
  
-   A value will not be imported from the file if the value is not of the domain's data type or is null.  
  
-   Values are imported in the order in which they appear in the file.  
  
-   Each row represents a domain value.  
  
-   The first row either represents domain names or is the first data value or record, depending upon the setting of the **Use First Row as header** checkbox. If you select **Use First Row as header** when using an .xslx or .xls file, any column names that are null will be automatically converted into F*n*, and any columns that are duplicate will have a number appended to them.  
  
-   If you cancel the import operation before it has completed, the operation will be rolled back and no data will be imported.  
  
-   The values in the first column are imported into the domain. If in addition to the first column, one or more additional columns are populated, then the values in those columns will be added as synonyms (see [Importing Synonyms](#Synonyms)).  
  
    -   The expected format is that the first column will be leading values and the second column and above will be synonyms.  
  
    -   You can import multiple synonyms in the same row or in different rows. For example, if you want to import "NYC" and "New York City" as synonyms for "New York", you can import a single row with "New York" in column 1, "NYC" in column 2, and "New York City" in column 3; or you can import one row with "New York" in column 1 and "NYC" in column 2, and another row with "New York" in column 1 and "New York City" in column 2. Note that if the value "New York" already exists in the domain, only the synonyms will be added, and the user will not receive an error during the import process telling him that the value already exist. If the first value does not already exist, it will be added to the domain.  
  
 The following rules apply to the Excel file being used for the import:  
  
-   The Excel file can have the extension .xlsx, .xls, or .csv. Microsoft Excel must be installed on the computer that the [!INCLUDE[ssDQSClient](../includes/ssdqsclient-md.md)] application is installed on in order to import domain values or a complete domain. Excel versions 2003 and later are supported. If the 64-bit version of Excel is used, only Excel 2003 files are supported; Excel 2007 or 2010 files are not supported.  
  
-   Excel files of type .xlsx are not supported for an Excel 64-bit installation. If you are using 64-bit Excel, save the spreadsheet file as an .xls file or a .csv file, or install an Excel 32-bit installation instead.  
  
-   In .xlsx and .xls files, the data type of the column is determined by the first eight rows. If the column data type of the first eight rows is mixed, the column type will be string. If a cell for row 9 and higher does not conform to that data type, it will be given a null value.  
  
-   In .csv files, the data type is determined by the most prevalent data type in the first eight rows.  
  
-   If the Excel file is not in the right format or is corrupted, the import operation will result in an error.  
  
  
