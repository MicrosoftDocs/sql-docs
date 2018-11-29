---
title: "Run a Matching Project | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "data-quality-services"
ms.reviewer: ""
ms.technology: data-quality-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dqs.matchingproject.map.f1"
  - "sql13.dqs.matchingproject.matching.f1"
  - "sql13.dqs.matchingproject.export.f1"
ms.assetid: 6aa9d199-83ce-4b5d-8497-71eef9258745
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Run a Matching Project

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

  This topic describes how to perform data matching in [!INCLUDE[ssDQSnoversion](../includes/ssdqsnoversion-md.md)] (DQS). The matching process identifies clusters of matching records based upon matching rules in the matching policy, designates one record from each cluster as the survivor based upon a survivorship rule, and exports the results. DQS performs the matching process, also called de-duplication, in a computer-assisted process, but you create matching rules interactively, and you select the survivorship rule from several choices, so you control the matching process.  
  
 Matching is performed in three stages: a mapping process in which you identify the data source and map domains to the data source, a matching process in which you run the matching analysis, and a survivorship and export process in which you designate the survivorship rule and export the matching results. Each of these processes is performed on a separate page of the Matching activity wizard, enabling you to move back and forth to different pages, to re-run the process, and to close out of a specific matching process and then return to the same stage of the process. DQS provides you with statistics about the source data, the matching rules, and the matching results that enable you to make informed decisions about matching, and refine the matching process.  
  
 You must prepare for matching by creating a matching policy with one or more matching rules, and running the policy on sample data. The matching project process is separate from the matching policy process, and a knowledge base is not populated with matching knowledge gained from the matching project. For more information about creating a matching policy, see [Create a Matching Policy](../data-quality-services/create-a-matching-policy.md).  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Prerequisites"></a> Prerequisites  
  
-   You must have created a knowledge base with a matching policy consisting of one or more matching rules.  
  
-   Microsoft Excel must be installed on the [!INCLUDE[ssDQSClient](../includes/ssdqsclient-md.md)] computer if the source data to be matched is in an Excel file. Otherwise, you will not be able to select the Excel file in the mapping stage. The files created by Microsoft Excel can have an extension of .xlsx, .xls, or .csv. If the 64-bit version of Excel is used, only Excel 2003 files (.xls) are supported; Excel 2007 or 2010 files (.xlsx) are not supported. If you are using 64-bit version of Excel 2007 or 2010, save the file as an .xls file or a .csv file, or install a 32-bit version of Excel instead.  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 You must have the dqs_kb_editor or the dqs_administrator role on the DQS_MAIN database to run a matching project.  
  
##  <a name="StartingaMatchingProject"></a> First Step: Starting a Matching Project  
 You perform the matching activity in a data quality project that you create in the DQS client application.  
  
1.  [!INCLUDE[ssDQSInitialStep](../includes/ssdqsinitialstep-md.md)] [Run the Data Quality Client Application](../data-quality-services/run-the-data-quality-client-application.md).  
  
2.  In the [!INCLUDE[ssDQSClient](../includes/ssdqsclient-md.md)] home screen, click **New Data Quality Project** to perform matching in a new data quality project. Enter a name for the data quality project, enter a description, and select the knowledge base that you want to use for matching in **Use knowledge base**. Click **Matching** for the activity. Click **Next** to proceed to the mapping stage.  
  
3.  Click **Open data quality project** to perform matching in an existing data quality project. Select the project and then click **Next**. (Or you can click a project under **Recent Data Quality Project**.) If you open a matching project that was closed, you will proceed to the stage that the matching project activity was closed in (as indicated by the **State** column in the project table or in the project name under **Recent Data Quality Project**). If you open a matching project that was finished, you will go to the **Export** page (and you cannot go back to previous screens).  
  
##  <a name="MappingStage"></a> Mapping Stage  
 In the mapping stage you identify the source of the data that you will run the matching analysis on, and you map source columns to domains to make the domains available for the matching activity.  
  
1.  On the **Map** page, to run matching on a database, leave **Data Source** as **SQL Server**, select the database that you want to run matching on, and then select the table. The source database must be present in the same SQL Server instance as the DQS server. Otherwise, it will not appear in the drop-down list.  
  
2.  To run matching on the data in an Excel spreadsheet, select **Excel File** for **Data Source**, click **Browse** and select the Excel file, and leave **Use first row as header** selected if appropriate. In **Worksheet**, select the worksheet in the Excel file that will be the source of the data. Excel must be installed on the [!INCLUDE[ssDQSClient](../includes/ssdqsclient-md.md)] computer to select an Excel file. If Excel is not installed on the [!INCLUDE[ssDQSClient](../includes/ssdqsclient-md.md)] computer, the **Browse** button will not be available, and you will be notified beneath this text box that Excel is not installed.  
  
3.  Under **Mappings**, select a field in the data source for **Source Column**, and then select the corresponding domain. Repeat for all domains that you use in the matching process. Each domain that is defined in the matching policy must be mapped to the appropriate source column. The Map page displays the domains that have been defined in the matching policy and the rules in the matching policy in the right-hand pane.  
  
    > [!NOTE]  
    >  You can map your source data to a DQS domain only if the source data type is supported in DQS, and matches with the DQS domain data type. For information about supported data types in DQS, see [Supported SQL Server and SSIS Data Types for DQS Domains](../data-quality-services/supported-sql-server-and-ssis-data-types-for-dqs-domains.md).  
  
4.  Click the **plus (+)** control to add a row to the Mappings table or the **minus (-)** control to remove a row.  
  
5.  Click **Preview data source** to see the data in the SQL Server table or view that you selected, or the Excel worksheet that you selected.  
  
6.  Click **View/Select Composite Domains** to view a list of the composite domains available in the knowledge base and select as appropriate for mapping.  
  
7.  Click **Next** to proceed to the matching stage.  
  
    > [!NOTE]  
    >  Click **Close** to save the stage of the matching project, and return to the DQS home page. The next time you open this project, it will start from the same stage. Click **Cancel** to end the matching activity, losing your work, and return to the DQS home page.  
  
##  <a name="MatchingStage"></a> Matching Stage  
 In this stage, you perform a computer-assisted matching process that shows you how many matches there are in the source data based upon the matching rules. This process will generate a matching results table that shows the clusters that DQS has identified, each record in the cluster with its record ID and its matching score, and the initial leading record for the cluster. The leading record in the cluster is selected randomly. You determine the surviving record by selecting the survivorship rule on the **Export** page when you run the matching project. Each additional row in a cluster is considered a match; its matching score (compared to the leading record) is provided in the results table. The cluster number is that same as the record ID for the leading record in the cluster.  
  
 In the matching results, you can filter on the data that you want, and reject matches that you do not want. You can display profiling data for the matching process as a whole, specifics about the matching rules that are applied, and statistics about the matching results as a whole. The matching process can identify overlapping or non-overlapping clusters, and if being run multiple times, can be executed on data newly copied from the source and re-indexed, or on previous data.  
  
1.  On the **Matching page**, select **Overlapping clusters** from the drop-down list to display the pivot records and following records for all clusters when matching is executed, even if groups of clusters have records in common. Select **Non overlapping clusters** to display clusters that have records in common as a single cluster when matching is executed.  
  
2.  Click **Reload data from source** (the default) to copy data from the data source into the staging table and re-index it when you run the matching project. Click **Execute on previous data** to run a matching project without copying the data into the staging table and re-indexing the data. **Execute on previous data** is disabled for the first run of the matching project, or if you change mapping in the **Map** page, and then press **Yes** in the following popup. In both of those cases, you must re-index. It is not necessary to re-index if the matching project has not changed. Executing on previous data can help performance.  
  
3.  Click **Start** to run matching on the selected data source.  
  
4.  Click **Stop** if you want to stop the matching project and discard the results.  
  
5.  After the matching process has completed, verify that the clusters in the **Matching Results** table are appropriate, and view the statistics in the **Profiler** and **Matching Results** tabs to ensure that you are achieving the results that you need. View the matched records by selecting **Matched** for **Filter** or view unmatched records by selecting **Unmatched**.  
  
6.  If you have multiple matching rules in the matching policy, click the **Matching Rules** tab to identify the icon for each rule, and then verify which rule identified a record as a match by identifying the rule in the **Rule** column of the **Matching Results** table.  
  
7.  If you select a non-pivot record in the table and click the **View Details** icon (or double-click the record), DQS will display a **Matching Score Details** popup that displays the record double-clicked and its pivot record (and the values in all their fields), the score between them, and a drill-down of the matching score contributions of each field. Double-clicking a pivot record will not display the popup.  
  
8.  Click the **Collapse All** icon to collapse the records displayed in the **Matching Results** table to include only pivot record, not the duplicate records. Click **Expand All** to expand the records displayed in the Matching Results table to include all duplicate records.  
  
9. To reject a record from the matching results, click the **Rejected** checkbox for the record.  
  
10. To change the minimum matching score that determines the level of matching that a record must have to be displayed, select the **Min. Matching Score** icon above the right-hand side of the table, and enter a higher number. The minimum matching score is set to 80% by default. Click **Refresh** to change the contents of the table.  
  
11. After the analysis has completed, the **Start** button turns into a **Restart** button. Click **Restart** to run the analysis project again. However, the results from the previous analysis have not been saved as yet, so clicking **Restart** will cause that previous data to be lost. To continue, click **Yes** in the popup. As the analysis is running, do not leave the page or the analysis process will be terminated.  
  
12. Click **Next** to proceed to the survivorship and export stage.  
  
##  <a name="SurvivorshipandExportStage"></a> Survivorship and Exporting Stage  
 In the survivorship process Data Quality Services determines a survivor record for each cluster, which will replace the other records that match it in the cluster. It then exports the matching and/or survivorship results to a table in the SQL Server database, a .csv file, or an Excel file.  
  
 Survivorship is optional. You can export the results without running survivorship, in which case DQS would use the pivot record that was designated in the matching analysis. If two or more records in a cluster comply with the survivorship rule, the survivorship process will select the lowest record ID among the conflicting records to be the survivor. You can export survivors to different files or tables using different survivorship rules.  
  
1.  On the **Export** page, select the destination where you want to export the matching data to in **Destination Type**: **SQL Server**, **CSV File**, or **Excel File**.  
  
    > [!IMPORTANT]  
    >  If you are using 64-bit version of Excel, you cannot export the matching data to an Excel file; you can export only to a SQL Server database or to a .csv file.  
  
2.  If you selected **SQL Server** for **Destination Type**, select the database to export the results to in **Database Name**.  
  
    > [!IMPORTANT]  
    >  The destination database must be present in the same SQL Server instance as the DQS server. Otherwise, it will not appear in the drop-down list.  
  
3.  Select the check box for **Matching Results** to export matching results (see above for an explanation) to the designated table in a SQL Server database or to the designated .csv or Excel file. Select the check box for **Survivorship Results** to export survivorship results (see above for an explanation) to the designated table in a SQL Server database or to the designated .csv or Excel file.  
  
     The following will be exported for matching results:  
  
    -   A list of clusters and the matched records in each cluster, including the rule name and the score. The pivot record will be marked as "Pivot". The clusters will appear first in the export list.  
  
    -   A list of the unmatched records, with "NULL" in the Score and Rule Name columns. These records will be appended to the export list after the clusters.  
  
     The following will be exported for survivorship results:  
  
    -   A list of the survivor records as determined by the survivorship process according to the survivorship rule. These records appear first in the export list.  
  
    -   A list of the unmatched records that are not included in the clusters of matched records. These records are appended after the survivor results.  
  
4.  If you selected **SQL Server** for **Destination Type**, enter the name of the tables that you want to export the results to in **Table Name**. If you export both matching results and survivorship results, the destination tables must have different names that are unique to the database.  
  
5.  If you selected **CSV File** for **Destination Type**, enter the file and path for the CSV file that you want to export to in **CSV File Name**.  
  
6.  If you selected **Excel File** for **Destination Type**, enter the file and path for the Excel file that you want to export to in **Excel File Name**. You cannot export to an Excel file if you are using 64-bit version of Excel.  
  
7.  Select the survivorship rule as follows:  
  
    -   Select **Pivot record** (the default) to identify the surviving record as the initial pivot record chosen arbitrarily by DQS.  
  
    -   Select **Most complete and longest record** to identify the surviving record as the one with the largest number of populated fields, and has the largest number of terms in each field. All source fields are checked, even those fields that were not mapped to a domain on the **Map** page.  
  
    -   Select **Most complete record** to identify the surviving record as the one with the largest number of populated fields. A populated field contains at least one value (string, numeric, or both). All source fields are checked, even those fields that were not mapped to a domain on the Map page. A populated field contains at least one value (string, numeric, or both).  
  
    -   Select **Longest record** to identify the surviving record as the one with the largest number of terms in its source fields. To determine the length of each record, DQS verifies the length of the terms in all source fields, even those fields that were not mapped to a domain on the **Map** page.  
  
8.  View the statistics in the **Profiler** tab to ensure that you are achieving the results that you need.  
  
9. Click **Export** to export the results. This displays a Matching Export dialog box that shows the progress and then the results of the export.  
  
    -   If you selected **SQL Server** as the data destination, a new table with the specified name will be created in the selected database.  
  
    -   If you selected **CSV File** as the data destination, a .csv file will be created at the location on the [!INCLUDE[ssDQSServer](../includes/ssdqsserver-md.md)] computer with the file name that you specified earlier in the **Csv file name** box.  
  
    -   If you selected **Excel File** as the data destination, an .xlsx file will be created at the location on the [!INCLUDE[ssDQSServer](../includes/ssdqsserver-md.md)] computer with the file name that you specified earlier in the **Excel file name** box.  
  
10. Verify that the export completed successfully, and then click **Close**.  
  
11. Click **Finish** to complete the matching project.  
  
    > [!NOTE]  
    >  If you have finished a matching project and then use it again, it will use the knowledge base in place when it was published. It will not use any changes that you have made to the knowledge base since you finished the project. To use those changes, or to use a new knowledge base, you will have to create a new matching project. On the other hand, if you have created, but not finished, a matching project, any changes that you have published to the matching policy will be used if you run matching in the project.  
  
##  <a name="FollowUp"></a> Follow Up: After Running a Matching Project  
 After you run a matching project, you can change the matching policy in the knowledge base, and create and run another matching project based upon the updated matching policy. For more information, see [Create a Matching Policy](../data-quality-services/create-a-matching-policy.md).  
  
##  <a name="Profiler"></a> Profiler and Results Tabs  
 The Profiler and Results tabs contain statistics for the matching process.  
  
### Profiler Tab  
 Click the **Profiler** tab to display statistics for the source database and for each field included in the policy rule. The statistics will be updated as the policy rule is run. Profiling will help you assess the effectiveness of the de-duplication process, helping determine the extent to which the process is able to improve the quality of the data. Accuracy in profiling is not important for a matching project.  
  
 The source database statistics include the following:  
  
-   **Records**: The total number of records in the database  
  
-   **Total Values**: The total number of values in the fields  
  
-   **New Values**: The total number of values that are new since the previous run, and their percentage of the whole  
  
-   **Unique Values**: The total number of unique values in the fields, and their percentage of the whole  
  
-   **New Unique Values**: The total number of unique values that are new in the fields, and their percentage of the whole  
  
 The field statistics include the following:  
  
-   **Field**: Name of the field that was included in the mappings.  
  
-   **Domain**: Name of the domain that was mapped to the field.  
  
-   **New**: The number of new matches found and their percentage of the total  
  
-   **Unique**: The number of unique records in the field and their percentaqe of the total  
  
-   **Completeness**: The percentage that the rule run is complete.  
  
### Matching Policy Notifications  
 For the matching policy activity, the following conditions result in notifications:  
  
-   The field is empty in all records; it is recommended that you eliminate it from mapping.  
  
-   The field completeness score is very low; you may want to eliminate it from mapping.  
  
-   All values in a field are invalid; you should verify the mapping and the relevancy of domain rules to the field contents.  
  
-   There is a low level of valid values in the field; you should verify the mapping and the relevancy of domain rules to the field contents.  
  
-   There is a high level of uniqueness in this field. Using this field in matching policy can decrease the matching results.  
  
### Matching Rules Tab  
 Click this tab to display a list of the rules in the matching policy and the conditions in a rule.  
  
 **Rules List**  
 Displays a list of all matching rules in the matching policy. Select one of the rules to display the conditions in the rule in the Matching Rule table.  
  
 **Matching Rule Table**  
 Displays each condition in the selected rule, including domain, similarity value, weight, and prerequisite selection.  
  
### Matching Results Tab  
 Click the **Matching Results** tab to display statistics for the analysis of the data source using the knowledge selected for the project and the matching rule or rules in that knowledge base. The statistics include the following:  
  
-   The total number of records in the database  
  
-   The total number of matching records in the database  
  
-   The number of records in the database that are not considered to be duplicates  
  
-   The number of clusters discovered  
  
-   The average cluster size (number of duplicate records divided by number of clusters)  
  
-   The fewest number of duplicates in a cluster  
  
-   The greatest number of duplicates in a cluster  
  
  
