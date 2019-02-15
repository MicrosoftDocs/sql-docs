---
title: "Create a Matching Policy | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: data-quality-services
ms.topic: conceptual
f1_keywords: 
  - "sql12.dqs.kb.kbmatchingresults.f1"
  - "sql12.dqs.kb.kbmatchingmap.f1"
  - "sql12.dqs.kb.kbmatchingpolicy.f1"
ms.assetid: cce77a06-ca31-47b6-8146-22edf001d605
author: leolimsft
ms.author: lle
manager: craigg
---
# Create a Matching Policy
  This topic describes how to build a matching policy in a knowledge base in [!INCLUDE[ssDQSnoversion](../includes/ssdqsnoversion-md.md)] (DQS). You prepare for the matching process in DQS by running the Matching Policy activity on sample data. In this activity you create and test one or more matching rules in the policy, and then publish the knowledge base to make the matching rules publicly available for use. There can be only one matching policy in a knowledge base, but that policy can contain multiple matching rules.  
  
 Matching policy creation is performed in three stages: a mapping process in which you identify the data source and map domains to columns, a matching policy process in which you create one or more matching rules and test each matching rule separately, and a matching results process in which you run all matching rules together, and if satisfied with them, add the policy to the knowledge base. Each of these processes is performed on a separate page of the Matching Policy activity wizard, enabling you to move back and forth to different pages, to re-run the process, and to close out of a specific matching policy process and return to the same stage of the process. After testing all rules together, if desired you can return to the **Matching Policy** page, tweak an individual rule, test it again separately, and then return to the **Matching Results** page to run all rules together once again. DQS provides you with statistics about the source data, the matching rules, and the matching results that enable you to make informed decisions about the matching policy, so you can refine it.  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Prerequisites"></a> Prerequisites  
 Microsoft Excel must be installed on the [!INCLUDE[ssDQSClient](../includes/ssdqsclient-md.md)] computer if the source data is in an Excel file. Otherwise, you will not be able to select the Excel file in the mapping stage. The files created by Microsoft Excel can have an extension of .xlsx, .xls, or .csv. If the 64-bit version of Excel is used, only Excel 2003 files (.xls) are supported; Excel 2007 or 2010 files (.xlsx) are not supported. If you are using 64-bit version of Excel 2007 or 2010, save the file as an .xls file or a .csv file, or install a 32-bit version of Excel instead.  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 You must have the dqs_kb_editor or the dqs_administrator role on the DQS_MAIN database to create a matching policy.  
  
##  <a name="MatchingRules"></a> How to Set Matching Rule Parameters  
 Creating a matching rule is an iterative process in which you enter the factors used to determine if one record is a match for another. You can enter conditions for any domain in a table. When DQS performs matching on two records, it will compare the values in the fields mapped to the domains that are included in the matching rule. DQS analyzes the values in each field in the rule, and then uses the factors entered in the rule for each domain to calculate a final matching score. If the matching score for the two records compared is greater than the minimum matching score, then the two fields are considered matches.  
  
 The factors that you enter in a matching rule include the following:  
  
-   Weight: For each domain in the rule, enter a numerical weight that determines how the matching analysis for the domain will be compared to that for each other domain in the rule. The weight indicates the contribution of the field's score to the overall matching score between two records. The calculated scores assigned to each source field are summed together for a composite matching score for the two records. For each field that is not a prerequisite (with a similarity of exact or similar), set the weight between 10 and 100. The sum of the weights of the domains that are not prerequisites must be equal to 100. If the value is a prerequisite, the weight is set to 0 and cannot be changed.  
  
-   Similarity of Exact: Select **Exact** if the values in the same field of two different records must be identical for the values to be considered to be a match. If identical, the matching score for that domain will be set to "100", and DQS will use that score and the scores for the other domains in the rule to determine the aggregate matching score. If not identical, the matching score for that domain will be set to "0", and processing of the rule will proceed to the next condition. If you set up a matching rule for a numeric domain and you select **Similar**, you can enter a tolerance either as a percentage or an integer. For a domain of type date, you can enter a tolerance as a day, month, or year (integer) if you select **Similar**; there is no percentage tolerance for a date domain. If you select **Exact**, you do not have this option.  
  
-   Similarity of Similar: Select **Similar** if two values in the same field of two different records can be considered a match even if the values are not identical. When DQS runs the rule, it will calculate a matching score for that domain, and will use that score and the scores for the other domains in the rule to determine the aggregate matching score. The minimum similarity between the values of a field is 60%. If the calculated matching score for a field of two records is less than 60, the similarity score is automatically set to 0. If you are setting up a matching rule for a numeric field, and you select **Similar**, you can enter a tolerance as a percentage or integer. If you are setting up a matching rule for a date field, and you select **Similar**, you can enter a numerical tolerance.  
  
-   Prerequisite: Select **Prerequisite** to specify that the values in the same field in two different records must return a 100% match, or the records are not considered a match and the other clauses in the rule are disregarded. When **Prerequisite** is selected, the weight field for the domain is removed so that you cannot define a weight for the domain. You must reset one or more domain weights so that the sum of weights is equal to 100. Prerequisite domains do not contribute to the record matching score. The record matching score is determined by comparing the values in fields for which the Similarity is set to Similar or Exact. When you make a field a prerequisite, the Similarity for that domain is automatically set to Exact.  
  
 The minimum matching score is the threshold at or above which two records are considered to be a match (and the status for the records is set to "Matched"). Enter an integer value in increments of "1" or click the up or down arrow to increase or decrease the value in increments of "10". The minimum value is 80. If the matching score is below 80, the two records are not considered a match. You cannot change the range of the minimum matching score in this page. The lowest min. matching score is 80. You can, however, change the lowest minimum matching score within the Administration page (if you are a DQS administrator).  
  
 Creating a matching rule is an iterative process because you may need to change the relative weights of the domains in the rule, or the similarity or the prerequisite property for a domain, or the min. matching score for the rule, in order to achieve the results that you need. You may also find that you need to create multiple rules, each of which will be run to create the matching score. It may be difficult to achieve the result you need with only one rule. Multiple rules will provide different views of a required match. With multiple rules, you may be able to include fewer domains in each rule, use higher weights for each domain, and achieve better results. If the data is less accurate and less complete, you may need more rules to find required matches. If the data is more accurate and complete, you need fewer rules.  
  
 Profiling gives insights on completeness and uniqueness. Consider completeness and uniqueness in tandem. Use completeness and uniqueness data to determine what weight to give a field in the matching process. If there is a high level of uniqueness in a field, using the field in a matching policy can decrease the matching results, so you may want to set the weight for that field to a relatively small value. If you have a low level of uniqueness for a column, but low completeness, you may not want to include a domain for that column. With a low level of uniqueness, but a high level of completeness, you may want to include the domain. Some columns, such as gender, may naturally have a low level of uniqueness. For more information, see [Profiler and Results Tabs](#Tabs).  
  
##  <a name="Starting"></a> First Step: Starting a Matching Policy  
 You perform the matching policy activity in the knowledge base management area of the [!INCLUDE[ssDQSClient](../includes/ssdqsclient-md.md)] application.  
  
1.  [!INCLUDE[ssDQSInitialStep](../includes/ssdqsinitialstep-md.md)] [Run the Data Quality Client Application](../../2014/data-quality-services/run-the-data-quality-client-application.md).  
  
2.  In the [!INCLUDE[ssDQSClient](../includes/ssdqsclient-md.md)] home screen, click **New knowledge base** to create a matching policy in a new knowledge base. Enter a name for the knowledge base, enter a description, and set **Create knowledge base from** as desired. Click **Matching Policy** for the activity. Click **Next** to proceed.  
  
3.  Click **Open knowledge base** to create or modify the matching policy in an existing knowledge base. Select the knowledge base, select **Matching Policy**, and then click **Next**. You can also click a knowledge base under **Recent Knowledge Base**. If you open a knowledge base that was closed while a matching policy was being worked on, you will proceed to the stage that the matching policy activity was closed in (as indicated by the **State** column for the knowledge base in the knowledge base table or in the knowledge base name under **Recent Knowledge Base**). If you open a knowledge base that includes a matching policy and was finished, you will go to the **Matching Policy** page. If you open a knowledge base that does not include a matching policy and was finished, you will go to the **Mapping** Page.  
  
##  <a name="MatchingStage"></a> Mapping Stage  
 In the mapping stage you identify the source of the data that you will create the matching policy for, and you map source columns to domains to make the domains available for the matching policy activity.  
  
1.  On the **Map** page, to create a policy for a database, leave **Data Source** as **SQL Server**, select the database that you want to create the policy for in **Database**, and then select the table or view in **Table/View**. The source database must be present in the same SQL Server instance as [!INCLUDE[ssDQSServer](../includes/ssdqsserver-md.md)]. Otherwise, it will not appear in the drop-down list.  
  
2.  To create a policy for the data in an Excel spreadsheet, select **Excel File** for **Data Source**, click **Browse** and select the Excel file, and leave **Use first row as header** selected if appropriate. In **Worksheet**, select the worksheet in the Excel file that will be the source of the data. Microsoft Excel must be installed on the Data Quality Client computer to select an Excel file. Otherwise, the Browse button will not be available, and you will be notified beneath this text box that Microsoft Excel is not installed.  
  
3.  Under **Mappings**, select a field for **Source Column**, and then click the **Create Domain** icon.  
  
4.  Under **Mappings**, select a field in the data source for **Source Column**, and then select the corresponding domain. Repeat for all domains that you use in the matching process. Create domains as necessary by clicking **Create a Domain** or **Create a Composite Domain**.  
  
    > [!NOTE]  
    >  You can map your source data to a DQS domain while creating a matching policy only if the source data type is supported in DQS, and matches with the DQS domain data type. For information about supported data types in DQS, see [Supported SQL Server and SSIS Data Types for DQS Domains](../../2014/data-quality-services/supported-sql-server-and-ssis-data-types-for-dqs-domains.md).  
  
5.  Click the **plus (+)** control to add a row to the Mappings table or the **minus (-)** control to remove a row.  
  
6.  Click **Preview data source** to see the data in the SQL Server table or view that you selected, or the Excel worksheet that you selected.  
  
7.  Click **View/Select Composite Domains** to view a list of the composite domains available in the knowledge base and select as appropriate for mapping.  
  
8.  Click **Next** to proceed to the matching policy stage.  
  
    > [!NOTE]  
    >  Click **Close** to save the stage of the matching project, and return to the DQS home page. The next time you open this project, it will start from the same stage. Click **Cancel** to end the matching activity, losing your work, and return to the DQS home page.  
  
##  <a name="MatchingPolicyStage"></a> Matching Policy Stage  
 You create matching rules and test them individually in the Matching Policy page. When you test a matching rule on the **Matching Policy** page, you will see a matching results table that shows the clusters that DQS has identified for the selected rule. The table shows each record in the cluster with the mapping domain values and matching score, and the initial pivot record for the cluster. You can also display profiling data for the matching process as a whole, the conditions in each matching rule, and statistics on the results for each matching rule separately. You can filter on the master rule data that you want.  
  
 For more information on how matching rules work, see [How to Set Matching Rule Parameters](#MatchingRules).  
  
1.  On the **Matching Policy** page, click the **Create a matching rule** icon.  
  
2.  Enter a name and description for the rule.  
  
3.  Increase the value of the **Min. matching score** if you want to make the matching requirements more stringent. For more information about the minimum matching score, see [How to Set Matching Rule Parameters](#MatchingRules).  
  
4.  Click the **Add a new domain element** icon.  
  
5.  Select a domain or composite domain to enter rule values for.  
  
    > [!NOTE]  
    >  You can select a composite domain only if each single domain in the composite domain has been mapped to a source column.  
  
6.  For **Similarity**, select **Similar** if two values in the same field of two different records can be considered a match even if not identical. Select **Exact** if two values in the same field of two different records must be identical to be considered to be a match. (For more information, see [How to Set Matching Rule Parameters](#MatchingRules).)  
  
7.  For **Weight**, enter a value that determines the contribution of a domain's matching score to the overall matching score for two records.  
  
    > [!NOTE]  
    >  When you define a weight for a composite domain, you can enter a different weight for each single domain in the composite domain, in which case the composite domain is not given a separate weight, or you can enter a single weight for the composite domain, in which the single domains in the composite domain are not given separate weights.  
  
8.  Select **Prerequisite** to specify that the values for the field in the two records must return a 100% match, else the records are not considered a match and the other clauses in the rule are disregarded. If the **Similarity** is **Similar**, it will change to **Exact**, and the weight will be removed because the match must be 100%.  
  
9. Repeat steps 4 through 8 for all other domains that will be part of the matching rule. Ensure that the sum of the weights for all domains in the rule equals 100.  
  
10. Select **Overlapping clusters** from the drop-down list to display the pivot records and following records for all clusters when matching is executed, even if groups of clusters have records in common. Select **Non overlapping clusters** to display clusters that have records in common as a single cluster when matching is executed.  
  
11. Click **Reload data from source** to copy data from the data source into the staging table and re-index it when you run the matching policy. Click **Execute on previous data** to run a matching policy without copying the data into the staging table and re-indexing the data. **Execute on previous data** is disabled for the first run of the matching policy, or if you change mapping in the **Map** page, and then press **Yes** in the following popup. In both of those cases, you must re-index. It is not necessary to re-index if the matching policy has not changed. Executing on previous data can help performance.  
  
12. Click **Start** to run the matching process for the selected rule. When the process is complete, the table displays the Record ID, Cluster number, and data columns (including those not in the matching rule) for each record in a cluster. The pivot row in the cluster is considered to be the prime candidate for surviving the de-duplication process. Each additional row in a cluster is considered a duplicate; its matching score (compared to the pivot record) is provided in the results table. The cluster number is that same as the record ID for the pivot record in the cluster.  
  
13. You can work with the data in the **Matching Results** table as follows:  
  
    -   In **Filter**, select **Matched** to show all matched rows and their score. Rows that are not considered matches (that have a matching score less than the minimum matching score) are not shown in the matching results table. Select **Unmatched** to show all unmatched rows, not matched rows.  
  
    -   In the **Percent Drop Down Box**, select a percentage from the drop-down list, in increments of "5". All rows with a matching score that is greater than or equal to that percentage will be displayed in the matching results table.  
  
    -   If you double-click a record in the matching results table, DQS displays a **Matching Score Details** popup that displays the pivot record and source record (and the values in all their fields), the score between them, and a drill-down of the record matching. The drill-down displays the values in each field of the pivot record and source record so you can compare them, and shows the matching score that each field contributes to the overall matching score for the two records.  
  
14. View the statistics in the **Profiler** and **Matching Results** tabs to ensure that you are achieving the results that you need. For more information, see [Profiler and Results Tabs](#Tabs).  
  
15. If the rule needs to be changed, change it in the Rule Editor, and click **Restart**.  
  
    > [!NOTE]  
    >  After the first analysis has completed, the **Start** button turns into a **Restart** button. If the results from the previous analysis have not been saved as yet, clicking **Restart** will cause that previous data to be lost. As the analysis is running, do not leave the page or the analysis process will be terminated.  
  
16. The **Matching Results** tab displays statistics for the last two runs of the rule. If you have run the matching rule more than once with different settings, compare the statistics for the current rule and the previous rule. If you find that the results from the previous rule were better, click **Restore Previous rule** to restore the conditions of the previous rule, returning the rule to its previous state before editing. The current rule conditions will be lost. This enables you to tune the policy based on the last two matching runs, decreasing the time that you spend tuning the matching policy.  
  
17. If you want another rule to be added to the matching policy, repeat from step 1.  
  
18. Click **Next** to proceed to the matching results stage.  
  
##  <a name="MatchingResultsStage"></a> Matching Results Stage  
 You test all your matching rules at once in the **Matching Results** page. Before you do so, you can specify that the rule test run identify overlapping or non-overlapping clusters. If you are running the rules multiple times, you can execute the rule on data reloaded from the source or on previous data.  
  
 When you test the matching rules on the **Matching Results** page, you will see a matching results table that shows the clusters that DQS has identified for all rules. The table shows each record in the cluster with the mapping domain values and matching score, and the initial pivot record for the cluster. You can also display profiling data for the matching rules as a whole, the conditions in each matching rule, and statistics on the results for all matching rules.  
  
1.  On the **Matching Results** page, select **Overlapping clusters** from the drop-down list to display the pivot records and following records for all clusters when matching is executed, even if groups of clusters have records in common. Select **Non overlapping clusters** to display clusters that have records in common as a single cluster when matching is executed.  
  
2.  Click **Reload data from source** to copy data from the data source into the staging table and re-index it when you run the matching policy. Click **Execute on previous data** to run a matching policy without copying the data into the staging table and re-indexing the data. **Execute on previous data** is disabled for the first run of the matching policy, or if you change mapping in the **Map** page, and then press **Yes** in the following popup. In both of those cases, you must re-index. It is not necessary to re-index if the matching policy has not changed. Executing on previous data can help performance.  
  
3.  Click **Start** to run the matching process for all rules that you have defined. The **Matching Results** table displays the record ID, cluster number, and data columns (including those not in the matching rule) for each record in a cluster. The leading record in the cluster is selected randomly. (You determine the surviving record by selected the survivorship rule on the **Export** page when you run the matching project.) Each additional row in a cluster is considered a duplicate; its matching score (compared to the pivot record) is provided in the results table.  
  
4.  You can work with the data in the **Matching Results** table as follows:  
  
    -   In **Filter**, select **Matched** to show all matched rows and their score. Rows that are not considered matches (that have a matching score less than the minimum matching score) are not shown in the matching results table. Select **Unmatched** to show all unmatched rows, not matched rows.  
  
    -   In the **Percent Drop Down Box**, select a percentage from the drop-down list, in increments of "5". All rows with a matching score that is greater than or equal to that percentage will be displayed in the matching results table.  
  
    -   If you double-click a record in the matching results table, DQS displays a **Matching Score Details** popup that displays the pivot record and source record (and the values in all their fields), the score between them, and a drill-down of the record matching. The drill-down displays the values in each field of the pivot record and source record so you can compare them, and shows the matching score that each field contributes to the overall matching score for the two records.  
  
5.  View the statistics in the **Profiler** and **Matching Results** tabs to ensure that you are achieving the results that you need. Click the **Matching Rules** tab to see what the domain settings for each rule are. For more information, see [Profiler and Results Tabs](#Tabs).  
  
6.  If you are not satisfied with the results of all rules, then click **Back** to return to the **Matching Policy** page, modify one or more rules as necessary, return to the **Matching Results** page, and then click **Restart**.  
  
    > [!NOTE]  
    >  After the analysis has completed, the **Start** button turns into a **Restart** button. If the results from the previous analysis have not been saved as yet, clicking **Restart** will cause that previous data to be lost.  
  
7.  If you are satisfied with the results of all rules, click **Finish** to complete the matching policy process, and then click one of the following:  
  
    -   **Yes - Publish the knowledge base and exit**: The knowledge base will be published for the current user or others to use. The knowledge base will not be locked, the state of the knowledge base (in the knowledge base table) will be set to empty, and both the Domain Management and Knowledge Discovery activities will be available. You will be returned to the Open Knowledge Base screen.  
  
    -   **No - Save the work on the knowledge base and exit**: Your work will be saved, the knowledge base will remained locked, and the state of the knowledge base will be set to **In work**. Both the Domain Management and Knowledge Discovery activities will be available. You will be returned to the home page.  
  
    -   **Cancel - Stay on the current screen**: The popup will be closed and you will be returned to the Domain Management screen.  
  
8.  Click **Close** to save your work, and return to the DQS home page. The state of the knowledge base will show the string "Matching Policy - ", and the current state. If you clicked **Close** while you are in the **Matching Result** screen, the state will show: "Matching Policy - Results". If you clicked close while you are in the **Matching Policy** screen, the state will show: "Matching Policy - Matching Policy". After clicking **Close**, to perform the **Knowledge Discovery** activity, you would have to return to the **Matching policy** activity, click **Finish**, and then click either **Yes** to publish the knowledge base or **No** to save the work on the knowledge base and exit.  
  
    > [!NOTE]  
    >  If you click **Close** while a matching process is running, the matching process will not terminate when you click **Close**. You can reopen the knowledge base and see either that the process is still running, or if completed, that the results are displayed. If the process has not completed, the screen will display the progress.  
  
9. Click **Cancel** to terminate the Matching Policy activity, losing your work, and return to the DQS home page.  
  
##  <a name="FollowUp"></a> Follow Up: After Creating a Matching Policy  
 After you create a matching policy, you can run a matching project based upon the knowledge base that contains the matching policy. For more information, see [Run a Matching Project](../../2014/data-quality-services/run-a-matching-project.md).  
  
##  <a name="Tabs"></a> Profiler and Results Tabs  
 The Profiler and Results tab contain statistics for both the Matching Policy and the Matching Results pages.  
  
###  <a name="Profiler"></a> Profiler Tab  
 Click the **Profiler** tab to display statistics for the source database and for each field included in the policy rule. The statistics will be updated as the policy rule is run.  
  
 For more information on how to interpret the following statistics, see [How to Set Matching Rule Parameters](#MatchingRules).  
  
 The source database statistics include the following:  
  
-   **Records**: The total number of records in the source database  
  
-   **Total Values**: The total number of values in the fields of the data source  
  
-   **New Values**: The total number of values that are new since the previous run, and their percentage of the whole  
  
-   **Unique Values**: The total number of unique values in the fields, and their percentage of the whole  
  
-   **New Unique Values**: The total number of unique values that are new in the fields, and their percentage of the whole  
  
 The field statistics include the following:  
  
-   **Field name**  
  
-   **Domain name**  
  
-   **New**: The number of new values and the percent of new values compared to existing values in the domain  
  
-   **Unique**: The number of unique records in the field and their percentage of the total  
  
-   **Completeness**: The completeness of each source field that is mapped for the matching exercise  
  
###  <a name="Notifications"></a> Matching Policy Notifications  
 For the matching policy activity, the following conditions result in notifications:  
  
-   The field is empty in all records; it is recommended that you eliminate it from mapping.  
  
-   The field completeness score is very low; you may want to eliminate it from mapping.  
  
-   All values in a field are invalid; you should verify the mapping and the relevancy of domain rules to the field contents.  
  
-   There is a low level of valid values in the field; you should verify the mapping and the relevancy of domain rules to the field contents.  
  
-   There is a high level of uniqueness in this field. Using this field in matching policy can decrease the matching results.  
  
###  <a name="ResultsTab"></a> Matching Results Tab  
 Click the **Matching Results** tab to display statistics for the matching policy rule run, and the previous rule run. If you have run the same rule more than once with different parameters, the matching results table will display statistics for both runs, enabling you to compare them. You can also restore the previous rule if you would like.  
  
 The statistics include the following:  
  
-   The total number of records in the database  
  
-   The total number of matching records in the database  
  
-   The number of records in the database that are not considered to be duplicates  
  
-   The number of clusters discovered  
  
-   The average cluster size (number of duplicate records divided by number of clusters)  
  
-   The fewest number of duplicates in a cluster  
  
-   The greatest number of duplicates in a cluster  
  
  
