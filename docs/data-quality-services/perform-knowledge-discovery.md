---
title: "Perform Knowledge Discovery | Microsoft Docs"
ms.custom: ""
ms.date: "06/04/2013"
ms.prod: sql
ms.prod_service: "data-quality-services"
ms.reviewer: ""
ms.technology: data-quality-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dqs.kb.kbterms.f1"
  - "sql13.dqs.kb.viewselectcd.f1"
  - "sql13.dqs.kb.kbanalyze.f1"
  - "sql13.dqs.kb.kbmap.f1"
ms.assetid: 34a0ea16-02e6-46ed-90bc-dede68687f63
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Perform Knowledge Discovery

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

  This topic describes how to build a knowledge base through knowledge discovery. In the discovery process, [!INCLUDE[ssDQSnoversion](../includes/ssdqsnoversion-md.md)] (DQS) analyzes the data in a sample data source through a computer-assisted process, and adds the knowledge that it gains to the knowledge base. This knowledge can be modified and enhanced in the **Manage Domain Values** step of the knowledge discovery activity, or in the domain management activity.  
  
 Knowledge discovery is a wizard-driven process that includes three steps, each of which must be completed.  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Prerequisites"></a> Prerequisites  
 Microsoft Excel must be installed on the [!INCLUDE[ssDQSClient](../includes/ssdqsclient-md.md)] computer if the source data against which you are running the discovery is in an Excel file. Otherwise, you will not be able to select the Excel file in the mapping stage. The files created by Microsoft Excel can have an extension of .xlsx, .xls, or .csv. If the 64-bit version of Excel is used, only Excel 2003 files (.xls) are supported; Excel 2007 or 2010 files (.xlsx) are not supported. If you are using 64-bit version of Excel 2007 or 2010, save the file as an .xls file or a .csv file, or install a 32-bit version of Excel instead.  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 You must have the dqs_kb_editor role or the dqs_administrator on the DQS_MAIN database to create a knowledge base.  
  
##  <a name="FirstStep"></a> First step: Start Knowledge Discovery  
  
1.  [!INCLUDE[ssDQSInitialStep](../includes/ssdqsinitialstep-md.md)] [Run the Data Quality Client Application](../data-quality-services/run-the-data-quality-client-application.md).  
  
2.  If you want to perform knowledge discovery on a new knowledge base, click **New knowledge base**, enter the name and description, and specify what you are creating the knowledge base from, if applicable. If you want to perform knowledge discovery on an existing knowledge base, click **Open knowledge base**, and then select a knowledge base.  
  
3.  Select **Knowledge Discovery** as the activity, and then click **Create** to create the new knowledge base or **Open** to open an existing knowledge base.  
  
##  <a name="Mapping"></a> Mapping Stage  
  
1.  In the **Data Source** field, select **SQL Server** (the default) or **Excel file**.  
  
    > [!NOTE]  
    >  In this page, you make a connection to either a SQL Server or Excel data source, and then map between columns in the data source and a domain in the knowledge base. The Mappings table displays all the columns in the source database that will be analyzed to add knowledge to the corresponding domains. Mappings are made between columns in the data source and a domain in the knowledge base.  
  
2.  If the data source is **SQL Server**, proceed as follows:  
  
    1.  In the **Database** field select the source database that you want to analyze to create the knowledge base. The text box drop-down will list the databases that are available. The source database must be present in the same SQL Server instance as [!INCLUDE[ssDQSServer](../includes/ssdqsserver-md.md)]. Otherwise, it will not appear in the drop-down list.  
  
    2.  In the **Table/View** field select the table or view that you want to analyze to create the knowledge base. This table or view should be sample data, not an entire source database that you are performing data cleansing or matching on. The text box drop-down will list the tables and views that are available for the selected database.  
  
3.  If the data source is **Excel**, proceed as follows:  
  
    1.  Click **Browse** and select the Excel file that you want to analyze to create the knowledge base. Excel must be installed on the [!INCLUDE[ssDQSClient](../includes/ssdqsclient-md.md)] computer to select an Excel file. If Excel is not installed on the [!INCLUDE[ssDQSClient](../includes/ssdqsclient-md.md)] computer, the Browse button will not be available, and you will be notified beneath this text box that Excel is not installed.  
  
    2.  Select the **Use first row as header** checkbox if the first row of the Excel file contains header data.  
  
4.  In the **Mappings** table, map each source column that you want knowledge discovery to be performed on to a domain in the knowledge base, as follows:  
  
    1.  Create a mapping by selecting a source column from the drop-down list for the **Source Column** column of an empty row, and then selecting a domain from the drop-down list for the **Domain** column in the same row, if a domain exists. If no domain exists, click the **Create a domain** or **Create a composite domain** to create a domain. For more information, see [Create a Domain Rule](../data-quality-services/create-a-domain-rule.md) or [Create a Composite Domain](../data-quality-services/create-a-composite-domain.md).  
  
    2.  Repeat the previous step for each mapping. To change the number of rows in the table, click **Add a column mapping**, or select a row and click the **Remove selected column mapping**. If you click **Remove selected column mapping** when a populated row is selected, the selected row will be deleted even if there is an unpopulated row.  
  
        > [!NOTE]  
        >  You can map your source data to a DQS domain for performing knowledge discovery only if the source data type is supported in DQS, and matches with the DQS domain data type. For more information about supported data types, see [Supported SQL Server and SSIS Data Types for DQS Domains](../data-quality-services/supported-sql-server-and-ssis-data-types-for-dqs-domains.md).  
  
    3.  Click **View/select composite domains** to display the composite domains that have been defined. If no composite domains have been defined, the control will not be available.  
  
    4.  Click **Preview data source** to display in a popup all data in the data source that you selected in the **Table/View** or **Excel File** text box.  
  
5.  Click **Next** to proceed to the **Discover** page of the Knowledge Discovery wizard. You can also select the following:  
  
    -   Click **Cancel** to terminate the Knowledge Discovery activity, losing your work, and return to the DQS home page.  
  
    -   Click **Close** to return to the DQS home page while saving your work. The knowledge base will be locked to you, and the state of the knowledge base in the knowledge base table in the **Open Knowledge Base** screen will be **Discovery - Mapping**. After clicking **Close**, to perform the Domain Management activity, you would have to click **Knowledge Discovery** from the **Open knowledge base** screen, proceed to the **Knowledge Base Management: Manage Domain Terms** screen, click **Finish**, and then click either **Yes** to publish the knowledge base or **No** to save the work on the knowledge base and exit.  
  
##  <a name="Discover"></a> Discover Stage  
  
1.  Click **Start** to analyze the data source.  
  
    > [!NOTE]  
    >  Discovery is performed on the columns that were entered in the **Mappings** table on the **Map** page. The domain mapped to each column will be populated with knowledge drawn from discovery. If the domain is a composite domain, the knowledge will be added to the individual domains that the composite domain consists of.  
  
2.  As the discovery process is running, check the completion status that is displayed for each step of discovery: **Preprocessing Records**, **Running Domain Rules**, and **Running Discovery**. Percent complete and completion status will be shown for each of these stages.  
  
3.  When the analysis has completed, verify that the status line beneath the completion statistics indicates that it completed successfully.  
  
    > [!NOTE]  
    >  Leaving the screen before the file has been uploaded will terminate the file upload process.  
  
4.  After the analysis has completed, check the statistics in the **Profiler** tab to see the status of the data. For more information, see **Data Profiling and Notifications in DQS**.  
  
5.  After the analysis has completed, the **Start** button turns into a **Restart** button. Click **Restart** to run the analysis process again. However, the results from the previous analysis have not been saved as yet, so clicking **Restart** will cause that previous data to be lost. To continue, click **Yes** in the popup. As the analysis is running, do not leave the page or the analysis process will be terminated.  
  
6.  Click **Next** to proceed to the **Manage Domain Values** page of the Knowledge Discovery wizard. On this page you can modify the knowledge added to the domains of the knowledge base. You can also select the following:  
  
    -   Click **Cancel** to terminate the Knowledge Discovery activity, losing your work, and return to the DQS home page.  
  
    -   Click **Close** to return to the DQS home page while saving your work. The knowledge base will be locked to you, and the state of the knowledge base in the knowledge base table in the **Open Knowledge Base** screen will be **Discovery - Discover**. After clicking **Close**, to perform the Domain Management activity, you would have to click **Knowledge Discovery** from the **Open knowledge base** screen, proceed to the **Knowledge Base Management: Manage Domain Terms** screen, click **Finish**, and then click either **Yes** to publish the knowledge base or **No** to save the work on the knowledge base and exit.  
  
    -   Click to return to the **Discover** page.  
  
##  <a name="Manage"></a> Manage Data Discovery Results Stage  
 After you have performed the knowledge discovery activity, you can change values as follows:  
  
-   Add a domain value to the value list, or select a value and delete it from the list  
  
-   Change the status of a domain value from what the DQS discovery process designates it as, changing it to correct, in-error, or not valid  
  
-   Enter a replacement value for a value that is in-error or not valid  
  
-   Set two or more values as synonyms and change the leading value as set by the discovery process, with the result that the leading value will replace the synonym value if the **Use Leading Value** property was set when you created the domain  
  
-   Import domain values from an Excel file.  
  
 The **Value** table displays knowledge added to the knowledge base for a single domain. You select that domain in the domain list in the pane to the left. The columns in the field are the following:  
  
-   The **Value** column displays all values that the discovery process added to the selected domain from a field in the data sample. Any value that is projected as an error will be shown as a synonym to a value that is projected as correct.  
  
-   The **Frequency** column displays the number of instances of the value in the sample database field that the domain is mapped to. For a composite domain, only those values with a frequency greater than or equal to 20 are displayed. The Frequency data is available because the knowledge discovery process still has a connection to the sample database. Frequency data is not available in the domain table on the Domain Values tab of the Domain Management screen because the domain management process does not have a connection to the sample database.  
  
-   The **Type** column displays the status of the value, as determined by the discovery process. A green check indicates that the value is correct or corrected; a red cross indicates that the value is in error; and an orange triangle with an exclamation point indicates that the value is not valid. A value that is not valid does not conform to the data requirements for the domain. A value that is in error can be valid, but is not the correct value for data reasons.  
  
-   The **Correct To** column shows a correct value that the original value, marked as in error or not valid, will be changed to. DQS can propose the correct value as a result of the discovery process.  
  
 Manage the discovery results as follows:  
  
1.  In the **Domains List** pane on the left, select a domain to set domain values for. You can do the following to modify the values displayed.  
  
    -   Display the results that you want in the table, based on their status, by selecting the status in the **Filter** list.  
  
    -   Find the data that you want to check or modify by entering one more letters to search for in the Find text box. This will highlight have those letters wherever they occur in any value that is displayed.  
  
    -   Click **Show Only New** to restrict the values displayed in the table only to values that were discovered in the current session, not previous sessions.  
  
    -   Click the **Expand All** button to display all values in any group of synonyms when the current state is collapsed, or the **Collapse All** button to hide all but the leading value in any group of synonyms when the current state is expanded.  
  
    -   Click the **Show/Hide the Domain Values Changes History Panel** button to display a preview popup at the bottom of the values table that shows recent changes to the domain values collection.  
  
2.  Find any corrections that Data Quality Services has proposed by setting **Filter** to **Error**. Verify that the value is in fact in error, and that the value in the **Correct To** column is appropriate.  
  
3.  Set **Filter** to **All Values** and verify that the state of the values is appropriate. To change a value's state, select the value, and then click the **Set selected domain values as corrected** (check) button, the **set selected domain values as errors** (cross) button, or the **set selected domain values as invalid** (triangle) button.  
  
4.  To change a value's state, proceed as follows:  
  
    1.  **Set selected domain values as corrected**: To change a value's state from Error or Invalid to Correct, select the value, and then click the **Set selected domain values as corrected** (check) from the down-arrow in the icon bar or from the Type drop-down list. If the in-error or invalid value is grouped with a correct value, delete that value after the operation.  
  
    2.  **Set selected domain values as errors**: To change a value's state from Correct or Invalid to Error, select the value, and then click the **Set selected domain values as errors** (cross) icon from the down-arrow in the icon bar or from the Type drop-down list. You can either enter a correction in the **Correct to** column, or leave it blank.  
  
    3.  **Set selected domain values as invalid**: To change a value's state from Correct or Error to Invalid, select the value, and then click the **Set selected domain values as invalid** (triangle) icon from the down-arrow in the icon bar or from the Type drop-down list. You can either enter a correction in the **Correct to** column, or leave it blank.  
  
    4.  **Correct to**: After setting a value as in error or invalid, enter a new value in the **Correct To** column. DQS will add a new row for the replacement value, designate it as correct, and then group the two values. The new value will be shown as the leading value, with the leading value in bold and the in-error or invalid value indented.  
  
5.  To designate values as a group of synonyms, select multiple values that are correct, and then proceed as follows:  
  
    -   **Set selected domain values as synonyms**: Click to set the selected values as synonyms. DQS will designate one of the values as the leading value that the others will be replaced with.  
  
        > [!NOTE]  
        >  If you select two or more values in a group and another value outside the group, and then set them as synonyms, you will get an incorrect error message. After closing the error message popup, the values will be set correctly as synonyms.  
  
    -   **Break relation between selected synonyms**: Click to undo the synonym designation.  
  
    -   **Set selected domain value as a leading value of its group**: Change the leading value of the group by selecting a value in the group that is not designated as the leading value, and then clicking the **Set selected domain value as a leading value of its group** button.  
  
6.  **Speller**: If you have enabled the Speller in the Domain Properties page, find any values that have a wavy red underscore, the indication that the Speller is suggesting a correction. Right-click the value with the underscore, and select a correction if one applies. The value type becomes (or stays as) error, and the correction will be added to the **Correct to** column. Click the down arrow to see additional proposed corrections. Enter a correction manually to add it to the Speller dictionary, and be able to select it as a correction. For more information, see [Use the DQS Speller](../data-quality-services/use-the-dqs-speller.md) and [Set Domain Properties](../data-quality-services/set-domain-properties.md).  
  
    > [!NOTE]  
    >  To use the Speller, you can either enable it in the **Domain Properties** page, or if it is disabled in the **Domain Properties** page, you can click the **Enable/Disable Speller** icon on the **Manage Data Discovery Results** page to enable it on this page.  
  
7.  **Add new domain value**: Add a new value to the domain by clicking the **Add new domain value** button to add a row at the end of the table. After you enter a value, the row will be repositioned in alphabetical order.  
  
8.  **Import domain values from Excel**: Add new values from an Excel spreadsheet by clicking the down arrow for the **Import Values** icon, and then selecting **Import domain values from Excel**. Enter the file name, select **Use first row as header** if appropriate, and then click **OK**. For more information, see [Import Values from an Excel File into a Domain](../data-quality-services/import-values-from-an-excel-file-into-a-domain.md).  
  
9. **Import project values**: Add new values from a Data Quality Project by clicking the down arrow for the **Import Values** icon, and selecting **Import project values**. Enter the file name, select **Use first row as header** if appropriate, and then click **OK**. Select the project that you want to import values from, and then click **OK**. The imported values will be displayed. Click **Finish**. For more information, see Import Project Values into a Domain.  
  
10. **Delete selected domain value(s)**: Remove one or more existing values from the domain by selecting the values, and then clicking the **Delete selected domain value(s)** button. An entry of DQS_NULL cannot be deleted, so if you choose multiple values to delete, and an entry of DQS_NULL is one of them, the operation will fail.  
  
11. Click **Finish** to complete the knowledge discovery activity. A popup will be displayed if you have not reviewed each of the domains. Click **Yes** to continue reviewing or **No** to proceed. If you click No, another popup will be displayed enabling you to do the following:  
  
    1.  **Publish**: The knowledge base will be published for the current user or others to use. The knowledge base will not be locked, the state of the knowledge base (in the knowledge base table) will be set to empty, and both the Domain Management and Knowledge Discovery activities will be available. You will be returned to the home page. To complete the process, click **Yes** in the popup.  
  
    2.  **No**: Your work will be saved, the knowledge base will remained locked, and the state of the knowledge base will be set to In work. Both the Domain Management and Knowledge Discovery activities will be available. You will be returned to the home page.  
  
    3.  **Cancel**: The popup will be closed and you will be stay in the **Manage Domain Value** page.  
  
12. You can also click the following:  
  
    -   **Cancel** to terminate the Knowledge Discovery activity, losing your work, and return to the DQS home page.  
  
    -   **Close** to return to the DQS home page while saving your work. The knowledge base will be locked to you, and the state of the knowledge base in the knowledge base table in the **Open Knowledge Base** screen will be **Discovery - Value Management**.  
  
    -   Click **Back** to return to the **Discover** page. After clicking **Close**, to perform the Domain Management activity, you would have to click **Knowledge Discovery** from the **Open knowledge base** screen, proceed to the **Knowledge Base Management: Manage Domain Terms** screen, click **Finish**, and then click either **Yes** to publish the knowledge base or **No** to save the work on the knowledge base and exit.  
  
##  <a name="FollowUp"></a> Follow Up: After Performing Knowledge Discovery  
 After you have adding knowledge to the knowledge case in the computer-assisted knowledge discovery process, you can either use the knowledge base for a cleansing project immediately, or you can perform domain management before performing cleansing. For more information about data cleansing or domain management, see [Data Cleansing](../data-quality-services/data-cleansing.md) or [Managing a Domain](../data-quality-services/managing-a-domain.md).  
  
##  <a name="Meaning"></a> The Meaning of Correct, Error, and Invalid Values  
 Each value in the **Value** table of the **Domain Values** page is assigned a **Type** setting of **Correct**, **Error**, or **Invalid**. The type of the value is generated initially by the knowledge discovery activity, and you can change it as you see fit. The final type, based upon both discovery and interactive changes, is generated by the cleansing activity. These settings have the following meanings:  
  
-   **Correct:** This is a value that belongs to the domain and does not have any syntax errors. For example, "Chicago" in a City domain is correct.  
  
-   **Error:** This is a value that belongs to the domain, but is an incorrect value. For example, "Shicago" instead of "Chicago" in a City domain is in error. DQS designates a value as in error it detects a syntax error and an associated correction in the discovery process. Syntax errors include misspellings.  
  
-   **Invalid:** This is a value that does not belong to the domain, and does not have a correction. For example, the value "12345" in a City domain is invalid. DQS designates a value as invalid when it fails a domain rule.  
  
 You can manually change the Type of a value to either of the two other values. DQS does not enforce validity and error semantics on manual operations. You can enter a correction for an Invalid value without changing its status. You can designate a value as invalid even if it did not fail a domain rule. You can designate a value as in error even if the discovery process did not indicate that it has a syntax error. You can also remove a correction to an Error value, which is marked as Correct, without changing its status.  
  
 When you are performing interactive data cleansing in the **Manage and View Results** page of the **Cleansing** activity, both invalid and in-error values are included in the **Invalid** tab on the **Manage and View Results** page.  
  
##  <a name="Display"></a> How to Display the Appropriate Values  
 You can modify the display as follows:  
  
-   **Filter** the results that you want in the table, based on their status, by selecting the status in the **Filter** drop-down list.  
  
-   **Find** the data that you want to check or modify by entering one more letters to search for in the **Find** text box. This will highlight have those letters wherever they occur in any value that is displayed.  
  
-   Click **Show Only New** to restrict the values displayed in the table only to values that were discovered in the current session, not previous sessions.  
  
-   Click the **Expand All** button to display all values in any group of synonyms when the current state is collapsed.  
  
-   Click the **Collapse All** button to hide all but the leading value in any group of synonyms when the current state is expanded.  
  
-   Click the **Show/Hide the Domain Values Changes History Panel** button to display a preview popup at the bottom of the values table that shows recent changes to the domain values collection.  
  
##  <a name="Profiler"></a> Profiler Statistics  
 The Profiler tab provides statistics that indicate the quality of the source data. These statistics do not measure the quality of the knowledge base. Profiling in knowledge discovery gives insights on completeness and uniqueness. Profiling in knowledge discovery is not measuring accuracy. Profiling for knowledge management helps you assess the extent to which the data source is valuable for building and enhancing the knowledge in a knowledge base.  
  
 The **Profiler** tab provides the following statistics for the discovery process, by field and domain:  
  
-   **Records**: How many records in the data sample were discovered  
  
-   **Total Values**: How many total values were found for each field and in total  
  
-   **New Values**: How many of the total values for each field and all mapped fields were new since the last discovery process, and their percentage of the total values  
  
-   **Unique Values**: How many of the total values for each field and all mapped fields were unique, and their percentage of the total values  
  
-   **New Unique Values**: How many of the unique values for each field and all mapped fields were new since the last discovery process, and their percentage of the total values  
  
-   **Valid in Domain Values**: How many of the total values for each field and all mapped fields were valid, and their percentage of the total values  
  
 The field statistics include the following:  
  
-   **Field**: Name of the field in the source database  
  
-   **Domain**: Name of the domain that maps to the field  
  
-   **New**: The number of new values and the percent of new values compared to existing values in the field  
  
-   **Unique**: The number of unique records in the field and their percentaqe of the total  
  
-   **Valid in Domain**: The number of domain values that are valid and their percentage of the total  
  
-   **Completeness**: The completeness of each source field that is mapped for the matching exercise  
  
 Profiling in knowledge discovery gives insights on completeness. If profiling is telling you that a field is relatively incomplete, you might want to remove it from the knowledge base of a data quality project. Profiling may not provide reliable completeness statistics for composite domains. If you need completeness statistics, use single domains instead of composite domains. If you want to use composite domains, you may want to create one knowledge base with single domains for profiling, to determine completeness, and create another domain with a composite domain for the cleansing process. For example, profiling could show 95% completeness for address records using a composite domain, but there could be a much higher level of incompleteness for one of the columns, for example, a postal (zip) code column. In this example, you might want to measure the completeness of the zip code column with a single domain. Profiling will likely provide reliable accuracy statistics for composite domains because you can measure accuracy for multiple columns together. The value of this data is in the composite aggregation, so you may want to measure the accuracy with a composite domain.  
  
 Statistics are displayed in the Profiler tab in the following phases:  
  
-   In the **Pre-processing Records** phase, DQS loads the data and indexes it. This is done record by record or batch by batch, so progress can be displayed by records. During the execution of this step most of the profiling data can be generated, except for **Valid in Domain** values.  
  
-   In the **Running Domain Rules** phase, the **Valid in Domain** column is populated as domain rules are all executed as an atomic unit of each domain value.  
  
-   In the **Running Discovery** phase, no new data is updated in the Profiler tab. Any syntax errors encountered can be seen in the next step of the wizard, the **Manage Domain Values** phase.  
  
 For the knowledge discovery activity, the following conditions result in notifications:  
  
-   There are no new values in a field; it is recommended that you eliminate it from mapping.  
  
-   There are few new values in a field; you may want to eliminate it from mapping.  
  
-   A field is empty; it is recommended that you eliminate it from mapping.  
  
-   The field completeness score is very low; you may want to eliminate it from mapping.  
  
-   All values in a field are invalid; you should verify the mapping and the relevancy of domain rules to the field contents.  
  
-   There is a low level of valid values in the field; you should verify the mapping and the relevancy of domain rules to the field contents.  
  
 For more information about profiling, see [Data Profiling and Notifications in DQS](../data-quality-services/data-profiling-and-notifications-in-dqs.md).  
  
  
