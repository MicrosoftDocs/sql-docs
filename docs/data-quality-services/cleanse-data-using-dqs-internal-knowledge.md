---
title: "Cleanse Data Using DQS (Internal) Knowledge | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "data-quality-services"
ms.reviewer: ""
ms.technology: data-quality-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dqs.dqproject.interactivecleansing.f1"
  - "sql13.dqs.dqproject.map.f1"
  - "sql13.dqs.dqproject.correction.f1"
  - "sql13.dqs.dqproject.export.f1"
ms.assetid: c96b13ad-02a6-4646-bcc7-b4a8d490f5cc
author: leolimsft
ms.author: lle
manager: craigg
---
# Cleanse Data Using DQS (Internal) Knowledge

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

  This topic describes how to cleanse your data by using a data quality project in [!INCLUDE[ssDQSnoversion](../includes/ssdqsnoversion-md.md)] (DQS). Data cleansing is performed on your source data using a knowledge base that has been built in DQS against a high-quality data set. For more information, see [Building a Knowledge Base](../data-quality-services/building-a-knowledge-base.md).  
  
 Data cleansing is performed in four stages: a *mapping* stage in which you identify the data source to be cleansed, and map it to required domains in a knowledge base, a *computer-assisted cleansing* stage where DQS applies the knowledge base to the data to be cleansed, and proposes/makes changes to the source data, an *interactive cleansing* stage where data stewards can analyze the data changes, and accept/reject the data changes, and finally the *export* stage that lets you export the cleansed data. Each of these processes is performed on a separate page of the cleansing activity wizard, enabling you to move back and forth to different pages, to re-run the process, and to close out of a specific cleansing process and then return to the same stage of the process. DQS provides you with statistics about the source data and the cleansing results that enable you to make informed decisions about data cleansing.  
  
## Before You Begin  
  
###  <a name="Prerequisites"></a> Prerequisites  
  
-   You must have specified appropriate threshold values for the cleansing activity. For information about doing so, see [Configure Threshold Values for Cleansing and Matching](../data-quality-services/configure-threshold-values-for-cleansing-and-matching.md).  
  
-   A DQS knowledge base must be available on [!INCLUDE[ssDQSServer](../includes/ssdqsserver-md.md)] against which you want to compare, and cleanse your source data. Additionally, the knowledge base must contain knowledge about the type of data that you want to cleanse. For example, if you want to cleanse your source data that contains US addresses, you must have a knowledge base that was created against a "high-quality" sample data for US addresses.  
  
-   Microsoft Excel must be installed on the [!INCLUDE[ssDQSClient](../includes/ssdqsclient-md.md)] computer if the source data to be cleansed is in an Excel file. Otherwise, you will not be able to select the Excel file in the mapping stage. The files created by Microsoft Excel can have an extension of .xlsx, .xls, or .csv. If the 64-bit version of Excel is used, only Excel 2003 files (.xls) are supported; Excel 2007 or 2010 files (.xlsx) are not supported. If you are using 64-bit version of Excel 2007 or 2010, save the file as an .xls file or a .csv file, or install a 32-bit version of Excel instead.  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 You must have the dqs_kb_editor or dqs_kb_operator role on the DQS_MAIN database to perform data cleansing.  
  
##  <a name="Create"></a> Create a Cleansing Data Quality Project  
 You must use a data quality project to perform data cleansing operation. To create a cleansing data quality project:  
  
1.  Follow steps 1-3 in the topic [Create a Data Quality Project](../data-quality-services/create-a-data-quality-project.md).  
  
2.  In step 3.d, select the **Cleansing** activity.  
  
3.  Click **Create** to create a cleansing data quality project.  
  
 This creates a cleansing data quality project, and opens up the **Map** page of the cleansing data quality wizard.  
  
##  <a name="Mapping"></a> Mapping Stage  
 In the mapping stage, you specify the connection to the source data to be cleansed, and map the columns in the source data with the appropriate domains in the selected knowledge base.  
  
1.  On the **Map** page of the cleansing data quality wizard, select your source data to be cleansed: **SQL Server** or **Excel File**:  
  
    1.  **SQL Server**: Select **DQS_STAGING_DATA** as the source database if you have copied your source data to this database, and then select appropriate table/view that contains your source data. Otherwise, select your source database and appropriate table/view. Your source database must be present in the same SQL Server instance as [!INCLUDE[ssDQSServer](../includes/ssdqsserver-md.md)] to be available in the **Database** drop-down list.  
  
    2.  **Excel File**: Click **Browse**, and select the Excel file that contains the data to be cleansed. Microsoft Excel must be installed on the [!INCLUDE[ssDQSClient](../includes/ssdqsclient-md.md)] computer to select an Excel file. Otherwise, the **Browse** button will not be available, and you will be notified beneath this text box that Microsoft Excel is not installed. Also, leave the **Use first row as header** check box selected if the first row of the Excel file contains header data.  
  
2.  Under **Mappings**, map the data columns in your source data with appropriate domains in the knowledge base by selecting a source column from the drop-down list in the **Source Column** column, and then selecting a domain from the drop-down list in the **Domain** column in the same row. Repeat this step to map all the columns in your source data with appropriate domains in the knowledge base. If required, you can click the **Add a column mapping** icon to add rows to the mapping table.  
  
    > [!NOTE]  
    >  You can map your source data to a DQS domain for performing data cleansing only if the source data type is supported in DQS, and matches with the DQS domain data type. For information about supported source data types, see [Supported SQL Server and SSIS Data Types for DQS Domains](../data-quality-services/supported-sql-server-and-ssis-data-types-for-dqs-domains.md).  
  
3.  Click the **Preview data source** icon to see the data in the SQL Server table or view that you selected, or the Excel worksheet that you selected.  
  
4.  Click **View/Select Composite Domains** to view a list of the composite domains that are mapped to a source column. This button is available only if you have at least one composite domain mapped to a source column.  
  
5.  Click **Next** to proceed to the computer-assisted cleansing stage (**Cleanse** page).  
  
##  <a name="ComputerAssisted"></a> Computer-Assisted Cleansing Stage  
 In the computer-assisted cleansing stage, you run an automated data cleansing process that analyzes source data against the mapped domains in the knowledge base, and makes/proposes data changes.  
  
1.  On the **Cleanse** page of the data quality wizard, click **Start** to run the computer-assisted cleansing process. DQS uses advanced algorithms and confidence levels based on the threshold levels specified to analyze your data against the selected knowledge base, and then cleanse it. For detailed information about how computer-assisted cleansing happens in DQS, see [Computer-assisted Cleansing](../data-quality-services/data-cleansing.md#ComputerAssisted) in [Data Cleansing](../data-quality-services/data-cleansing.md).  
  
    > [!IMPORTANT]  
    >  -   After the data analysis has completed, the **Start** button turns into a **Restart** button. If the results from the previous analysis have not been saved as yet, clicking **Restart** will cause that previous data to be lost. As the analysis is running, do not leave the page or the analysis process will be terminated.  
    > -   If the knowledge base used for the cleansing project was updated and published after the time that the cleansing project was created, clicking **Start** prompts you whether to use the latest knowledge base for cleansing. This can typically happen if you created a data quality project using a knowledge base, closed the cleansing project mid-way by clicking **Close**, and then reopened the data quality project at a later point to perform cleansing. In the meantime, the knowledge base used in the cleansing project was updated and published.  
    >   
    >      Similarly, if the knowledge base used for the cleansing project was updated and published after the last time you ran the computer-assisted cleansing, clicking **Restart** prompts you whether to use the latest knowledge base for cleansing.  
    >   
    >      In both the cases, click **Yes** to use the updated knowledge base for the computer-assisted cleansing. Additionally, if there are any conflicts between current mappings and the updated knowledge base (such as domains were deleted or domain data type was changed), the message also prompts you to fix the current mappings to use the updated knowledge base. Clicking **Yes** takes you to the **Map** page where you can fix the mappings before continuing with the computer-assisted cleansing.  
  
2.  During the computer-assisted cleansing stage, you can switch on the profiler by clicking the **Profiler** tab to view real-time data profiling and notifications. For more information, see [Profiler Statistics](#Profiler).  
  
3.  If you are not satisfied with the results, then click **Back** to return to the **Map** page, modify one or more mappings as necessary, return to the **Cleanse** page, and then click **Restart**.  
  
4.  After the computer-assisted cleansing process is complete, click **Next** to proceed to the interactive cleansing stage (**Manage and View Results** page).  
  
##  <a name="Interactive"></a> Interactive Cleansing Stage  
 In the interactive cleansing stage, you can see the changes that DQS has proposed and decide whether to implement them or not by approving or rejecting the changes. On the left pane of the **Manage and view results** page, DQS displays a list of all the domains that you mapped earlier in the mapping stage along with the number of values in the source data analyzed against each domain during the computer-assisted cleansing stage. On the right pane of the **Manage and view results** page, based on adherence to the domain rules, syntax error rules, and advanced algorithms, DQS categorizes the data under five tabs using the *confidence level*. The confidence level indicates the extent of certainty of DQS for the correction or suggestion, and is based on the following threshold values:  
  
-   **Auto Correction threshold**: Any value that has a confidence level above this threshold is automatically corrected by DQS. However, the data steward can override the change during interactive cleansing. You can specify the auto correction threshold value in the **General Settings** tab in the **Configuration** screen. For more information, see [Configure Threshold Values for Cleansing and Matching](../data-quality-services/configure-threshold-values-for-cleansing-and-matching.md).  
  
-   **Auto Suggestion threshold**:  Any value that has a confidence level above this threshold, but below the auto correction threshold, is suggested as a replacement value. DQS will make the change only if the data steward approves it. You can specify the auto suggestion threshold value in the **General Settings** tab in the **Configuration** screen. For more information, see [Configure Threshold Values for Cleansing and Matching](../data-quality-services/configure-threshold-values-for-cleansing-and-matching.md).  
  
-   **Other**:  Any value below the auto suggestion threshold value is left unchanged by DQS.  
  
 Based on the confidence level, the values are displayed under the following five tabs:  
  
|Tab|Description|  
|---------|-----------------|  
|**Suggested**|Displays the domain values for which DQS found the suggested values that have a confidence level higher than the *auto-suggestion threshold* value but lower than the *auto-correction threshold* value.<br /><br /> The suggested values are displayed in the **Correct To** column against the original value. You can click the radio button in the **Approve** or **Reject** column against a value in the upper grid to accept or reject the suggestion for all the instances of the value. In this case, the accepted value moves to the **Corrected** tab and the rejected value moves to the **Invalid** tab.|  
|**New**|Displays the valid domain for which DQS does not have enough information, and therefore cannot be mapped to any other tab. Further, this tab also contains values that have confidence level less than the *auto-suggestion threshold* value, but high enough to be marked as valid.<br /><br /> If you think the value is correct, click the radio button in the **Approve** column. Else, click the radio button in the **Reject** column. The accepted value moves to the **Correct** tab and the rejected value moves to the **Invalid** tab. You can also manually type the correct value as a replacement for the original value in the **Correct To** column against the value, and then click the radio button in the **Approve** column to accept the change. In this case, the value moves to the **Corrected** tab.|  
|**Invalid**|Displays the domain values that were marked as invalid in the domain in the knowledge base or values that failed a domain rule. This tab also contains values that were rejected by the user in any of the other four tabs.<br /><br /> However, if you think the value is correct, click the radio button in the **Approve** column. The accepted value moves to the **Correct** tab. You can also manually type the correct value as a replacement for the original value in the **Correct To** column against the value, and then click the radio button in the **Approve** column to accept the change. In this case, the value moves to the **Corrected** tab.|  
|**Corrected**|Displays the domain values that are corrected by DQS during the automated cleansing process as DQS found a correction for the value with confidence level above the auto-correction threshold value.<br /><br /> The corrected values are displayed in the **Correct To** column against the original value. By default, the radio button in the **Approve** column against the value is selected. If required, you can reject the proposed correction by clicking the radio button in the **Reject** column to move it to the **Invalid** tab, or manually type correct value in the **Correct To** column, and then click the radio button in the **Approve** column to accept the change, and move it to the **Corrected** tab.|  
|**Correct**|Displays the domain values that were found correct. For example, the value matched a domain value. This tab also contains values that were approved by the user by clicking the radio button in the **Approve** column in the **New** and **Invalid** tabs.<br /><br /> By default, the radio button in the **Approve** column is selected against each value. However, if you think that a value in this tab is incorrect, you can either click the radio button in the **Reject** column against the value to move it to the **Invalid** tab, or manually type the correct value as a replacement for the value in the **Correct To** column against the value, and then click the radio button in the **Approve** column to accept the change, and move it to the **Corrected** tab.|  
  
 To interactively cleanse the data:  
  
1.  On the **Manage and view results** page of the cleansing data quality wizard, click on a domain name in the left pane.  
  
2.  Review the domain values under the five tabs, and take appropriate action as explained earlier.  
  
    -   The right-upper pane displays the following information for each value in the selected domain: original value, number of instances (records), a box to specify another (correct) value, the confidence level (not available for the values under the **Correct** tab), the reason for the DQS action on the value, and the option to approve and reject the corrections and suggestions for the value.  
  
        > [!TIP]  
        >  You can approve or reject all the values in the selected domain in the upper-right pane by clicking **Approve all terms** or **Reject all terms** icon respectively. Alternately, you can right-click a value in the selected domain, and click **Accept all** or **Reject all** in the shortcut menu.  
  
    -   The lower pane displays individual occurrences of the domain value selected in the right-upper pane. The following information is displayed: a box to specify another (correct) value, the confidence level (not available for the values under the **Correct** tab), the reason for the DQS action on the value, option to approve and reject the corrections and suggestions for the value, and the original value.  
  
3.  If you enabled the **Speller** feature for a domain while creating it, wavy red underscores are displayed against such domain values that are identified as potential error. The underscore is displayed for the entire value. For example, if "New York" is incorrectly spelled as "Neu York", the speller will display red underscore under "Neu York", and not just "Neu". If you right-click the value, you will see suggested corrections. If there are more than 5 suggestions, you can click **More suggestions** in the context menu to view the rest of them. As with the error display, the suggestions are replacements for the whole value. For example, "New York" will be displayed as a suggestion in the previous example, and not just "New". You can pick one of the suggestions or add a value to the dictionary to be displayed for that value. Values are stored in dictionary at a user account level. When you select a suggestion from the speller context menu, the selected suggestion will be added to the **Correct To** column. However, if you select a suggestion in the **Correct To** column, the value in the column is replaced by the selected suggestion.  
  
     The speller feature is enabled by default in the interactive cleansing stage. You can disable speller in the interactive cleansing stage by clicking the **Enable/Disable Speller** icon, or right-clicking in the domain values area, and then clicking **Speller** in the shortcut menu. To enable it back again, do the same.  
  
    > [!NOTE]  
    >  The speller feature is only available in the upper pane (domain values). Moreover, you cannot enable or disable speller for composite domains. The child domains in a composite domain that are of string type, and are enabled for the speller feature, will have the speller functionality enabled in the interactive cleansing stage, by default.  
  
4.  During the interactive cleansing stage, you can switch on the profiler by clicking the **Profiler** tab to view real-time data profiling and notifications. For more information, see [Profiler Statistics](#Profiler).  
  
5.  After you have reviewed all the domain values, click **Next** to proceed to the export stage.  
  
##  <a name="Export"></a> Export Stage  
 In the export stage, you specify the parameters for exporting your cleansed data: what and where to export.  
  
1.  On the **Export** page of the cleansing data quality wizard, select the destination type for exporting your cleansed data: **SQL Server**, **CSV File**, or **Excel File**.  
  
    > [!IMPORTANT]  
    >  If you are using 64-bit version of Excel, you cannot export your cleansed data to an Excel file; you can export only to a SQL Server database or to a .csv file.  
  
    1.  **SQL Server**: Select **DQS_STAGING_DATA** as the destination database if you want to export your data here, and then specify a table name that will be created to store your exported data. Otherwise, select another database if you want to export data to a different database, and then specify a table name that will be created to store your exported data. Your destination database must be present in the same SQL Server instance as [!INCLUDE[ssDQSServer](../includes/ssdqsserver-md.md)] to be available in the **Database** drop-down list.  
  
    2.  **CSV File**: Click **Browse**, and specify the name and location of the .csv file where you want to export the cleansed data. You can also type the file name for the .csv file along with the full path where you want to export the cleansed data. For example, "c:\ExportedData.csv". The file is saved on the computer where [!INCLUDE[ssDQSServer](../includes/ssdqsserver-md.md)] is installed.  
  
    3.  **Excel File**: Click **Browse**, and specify the name and location of the Excel file where you want to export the cleansed data. You can also type the file name for the Excel file along with the full path where you want to export the cleansed data. For example, "c:\ExportedData.xlsx". The file is saved on the computer where [!INCLUDE[ssDQSServer](../includes/ssdqsserver-md.md)] is installed.  
  
2.  Select the **Standardize Output** check box to standardize the output based on the output format selected for the domain. For example, change the string value to upper case or capitalize the first letter of the word. For information about specifying the output format of a domain, see the **Format Output to** list in [Set Domain Properties](../data-quality-services/set-domain-properties.md).  
  
3.  Next, select the data output: export just the cleansed data or export cleansed data along with the cleansing information.  
  
    -   **Data Only**: Click the radio button to export just the cleansed data.  
  
    -   **Data and Cleansing Info**: Click the radio button to export the following data for each domain:  
  
        -   **\<Domain>_Source**: The original value in the domain.  
  
        -   **\<Domain>_Output**: The cleansed values in the domain.  
  
        -   **\<Domain>_Reason**: The reason specified for the correction of the value.  
  
        -   **\<Domain>_Confidence**: The confidence level for all the terms that were corrected. It is displayed as the decimal value equivalent to the corresponding percentage value. For example, a confidence level of 95% will be displayed as .9500000.  
  
        -   **\<Domain>_Status**: The status of the domain value after data cleansing. For example, **Suggested**, **New**, **Invalid**, **Corrected**, or **Correct**.  
  
        -   **Record Status**: Apart from having a status field for each mapped domain **(\<DomainName>_Status**), the **Record Status** field displays the status for a record. If any of the domain's status in the record is *New* or *Correct*, the **Record Status** is set to *Correct*. If any of the domain's status in the record is *Suggested*, *Invalid*, or *Corrected*, the **Record Status** is set to the respective value. For example, if any of the domain's status in the record is *Suggested*, the **Record Status** is set to *Suggested*.  
  
            > [!NOTE]  
            >  If you use reference data service for the cleansing operation, some additional data about the domain value is also available for exporting. For more information, see [Cleanse Data Using Reference Data &#40;External&#41; Knowledge](../data-quality-services/cleanse-data-using-reference-data-external-knowledge.md).  
  
4.  Click **Export** to export data to the selected data destination. If you selected:  
  
    -   **SQL Server** as the data destination, a new table with the specified name will be created in the selected database.  
  
    -   **CSV File** as the data destination, a .csv file will be created at the location on the [!INCLUDE[ssDQSServer](../includes/ssdqsserver-md.md)] computer with the file name that you specified earlier in the **CSV File** name box.  
  
    -   **Excel File** as the data destination, an Excel file will be created at the location on the [!INCLUDE[ssDQSServer](../includes/ssdqsserver-md.md)] computer with the file name that you specified earlier in the **Excel file name** box.  
  
5.  Click **Finish** to close the data quality project.  
  
##  <a name="Profiler"></a> Profiler Statistics  
 The **Profiler** tab provides statistics that indicate the quality of the source data. Profiling helps you assess the effectiveness of the data cleansing activity, and you can potentially determine the extent to which data cleansing was able to improve the quality of the data.  
  
 The **Profiler** tab provides the following statistics for the source data, by field and domain:  
  
-   **Records**: How many records in the data sample were analyzed for the data cleansing activity  
  
-   **Correct Records**: How many records were found to be correct  
  
-   **Corrected Records**: How many records were corrected  
  
-   **Suggested Records**: How many records were suggested  
  
-   **Invalid Records**: How many records were invalid  
  
 The field statistics include the following:  
  
-   **Field**: Name of the field in the source data  
  
-   **Domain**: Name of the domain that maps to the field  
  
-   **Corrected Values**: The number of domain values that were corrected  
  
-   **Suggested Values**: The number of domain values that were suggested  
  
-   **Completeness**: The completeness of each source field that is mapped for the cleansing activity  
  
-   **Accuracy**: The accuracy of each source field that is mapped for the cleansing activity  
  
 DQS profiling provides two data quality dimensions: *completeness* (the extent to which data is present) and *accuracy* (the extent to which data can be used for its intended use). If profiling is telling you that a field is relatively incomplete, you might want to remove it from the knowledge base of a data quality project. Profiling may not provide reliable completeness statistics for composite domains. If you need completeness statistics, use single domains instead of composite domains. If you want to use composite domains, you may want to create one knowledge base with single domains for profiling, to determine completeness, and create another domain with a composite domain for the cleansing process. For example, profiling could show 95% completeness for address records using a composite domain, but there could be a much higher level of incompleteness for one of the columns, for example, a postal (zip) code column. In this example, you might want to measure the completeness of the zip code column with a single domain. Profiling will likely provide reliable accuracy statistics for composite domains because you can measure accuracy for multiple columns together. The value of this data is in the composite aggregation, so you may want to measure the accuracy with a composite domain.  
  
 Accuracy statistics will likely require more interpretation if you are not using a reference data service. If you are using a reference data service for data cleansing, you will have a level of trust in accuracy statistics. For more information about data cleansing using reference data service, see [Cleanse Data Using Reference Data &#40;External&#41; Knowledge](../data-quality-services/cleanse-data-using-reference-data-external-knowledge.md).  
  
### Cleansing Notifications  
 The following conditions result in notifications:  
  
-   There are no corrections or suggestions for a field. You might want to remove it from mapping, run knowledge discovery first, or use another knowledge base.  
  
-   There are relatively few corrections or suggestions for a field. You might want to remove it from mapping, run knowledge discovery first, or use another knowledge base.  
  
-   The accuracy level of the field is very low. You might want to verify the mapping, or consider running knowledge discovery first.  
  
 For more information about profiling, see [Data Profiling and Notifications in DQS](../data-quality-services/data-profiling-and-notifications-in-dqs.md).  
  
  
