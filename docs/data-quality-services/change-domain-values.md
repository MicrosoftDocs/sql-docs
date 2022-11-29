---
title: "Change Domain Values"
description: "Change Domain Values"
author: swinarko
ms.author: sawinark
ms.date: "11/08/2011"
ms.service: sql
ms.subservice: data-quality-services
ms.topic: conceptual
f1_keywords:
  - "sql13.dqs.dm.values.f1"
---
# Change Domain Values

[!INCLUDE [SQL Server - Windows only ASDBMI  ](../includes/applies-to-version/sqlserver.md)]

  This topic describes how to change and augment the metadata in a knowledge base in [!INCLUDE[ssDQSnoversion](../includes/ssdqsnoversion-md.md)] (DQS). After you generate knowledge by knowledge discovery, import knowledge into the knowledge base or domains, or base a knowledge base upon another knowledge base, you can interactively change the data values. Knowledge base generation not only leverages computer-assisted processes, but gives you the means to use your own knowledge to verify data values and change them in the following ways:  
  
-   Add a domain value to the value list, or select a value and delete it from the list  
  
-   Change the status of a domain value from what the DQS discovery process designates it as, changing it to correct, in-error, or not valid  
  
-   Enter a replacement value for a value that is in error or not valid. A value is invalid if it does not belong in the domain, for example, if it does not conform to the domain data type or fails a domain rule. A value is in error if it belongs in the domain, but has a syntax error.  
  
-   Set two or more values as synonyms and change the leading value as set by the discovery process, with the result that the leading value will replace the synonym value if the **Use Leading Value** property was set when you created the domain  
  
-   Import domain values from an Excel file  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Prerequisites"></a> Prerequisites  
 To change a domain value, you must have a knowledge base and a domain opened in the Domain Management activity.  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 You must have the dqs_kb_editor or the dqs_administrator role on the DQS_MAIN database to change domain values.  
  
##  <a name="Change"></a> Change Domain Values  
 The **Value** table displays knowledge added to the knowledge base for a single domain. You can select a different domain in the domain list at any time to display the values for that domain. The columns in the field are the following:  
  
-   The **Value** column displays all values that the discovery process added to the selected domain from a field in the data sample. Any value that is projected as an error will be shown as a synonym to a value that is projected as correct.  
  
-   The **Type** column displays the status of the value, as determined by the discovery process. A green check indicates that the value is correct or corrected; a red cross indicates that the value is in error; and an orange triangle with an exclamation point indicates that the value is not valid. A value that is not valid does not conform to the data requirements for the domain. A value that is in error can be valid, but is not the correct value for data reasons.  
  
-   The **Correct To** column shows a correct value that the original value, marked as in error or not valid, will be changed to. DQS can propose the correct value as a result of the discovery process.  
  
 To change values, proceed as follows:  
  
1.  [!INCLUDE[ssDQSInitialStep](../includes/ssdqsinitialstep-md.md)] [Run the Data Quality Client Application](../data-quality-services/run-the-data-quality-client-application.md).  
  
2.  In the [!INCLUDE[ssDQSClient](../includes/ssdqsclient-md.md)] home screen, open or create a knowledge base. Select **Domain Management** as the activity, and then click **Open** or **Create**. For more information, see [Create a Knowledge Base](../data-quality-services/create-a-knowledge-base.md) or [Open a Knowledge Base](../data-quality-services/open-a-knowledge-base.md).  
  
    > [!NOTE]  
    >  Domain management is performed in a page of the Data Quality Service client that contains five tabs for separate domain management operations. It is not a wizard-driven process; any management operation can be performed separately.  
  
3.  From the **Domain list** on the **Domain Management** page, select the domain that you want to change values in or create a new domain. If you have to create a new domain, see [Create a Domain](../data-quality-services/create-a-domain.md). Click the **Domain Values** tab.  
  
4.  Display the values that you need to modify in the **Value** table. For more information, see [How to Display the Appropriate Values](#Display) below.  
  
5.  To change a value's state, proceed as follows:  
  
    -   **Set selected domain values as corrected**: To change a value's state from Error or Invalid to Correct, select the value, and then click the **Set selected domain values as corrected** (check) from the down-arrow in the icon bar or from the Type drop-down list. If the in-error or invalid value is grouped with a correct value, delete that value after the operation.  
  
    -   **Set selected domain values as errors**: To change a value's state from Correct or Invalid to Error, select the value, and then click the **Set selected domain values as errors** (cross) icon from the down-arrow in the icon bar or from the Type drop-down list. You can either enter a correction in the **Correct to** column, or leave it blank.  
  
    -   **Set selected domain values as invalid**: To change a value's state from Correct or Error to Invalid, select the value, and then click the **Set selected domain values as invalid** (triangle) icon from the down-arrow in the icon bar or from the Type drop-down list. You can either enter a correction in the **Correct to** column, or leave it blank.  
  
    -   **Correct to**: After setting a value as in error or invalid, enter a new value in the **Correct To** column. DQS will add a new row for the replacement value, designate it as correct, and then group the two values. The new value will be shown as the leading value, with the leading value in bold and the in-error or invalid value indented.  
  
6.  To designate values as a group of synonyms, select multiple values that are correct, and then proceed as follows:  
  
    -   **Set selected domain values as synonyms**: To set synonyms, select multiple values that are correct, and then click the **Set selected domain values as synonyms** icon. DQS will group the values and designate one of the values as the leading value that the others will be replaced with. Note that if two values are grouped, but one of the group is in-error or invalid, the values are not synonyms.  
  
        > [!NOTE]  
        >  If you select two or more values in a group and another value outside the group, and then set them as synonyms, you will get an incorrect error message. After closing the error message popup, the values will be set correctly as synonyms.  
  
    -   **Break relation between selected synonyms**: To undo the synonym designation for two or more values, select the values and then click the **Break relation between selected synonyms** icon. The values must be grouped and must both be correct for the ungrouping of synonyms to work.  
  
    -   **Set selected domain value as a leading value of its group**: To change the leading value of the group, select a value in the group that is not designated as the leading value, and then click the **Set selected domain value as a leading value of its group** button. This will set the leading value as a replacement for the other value. This operation works only if you have set two or more values that are group, and you want to change the leading value from the value designated by DQS. Note that the leading value is designated by a blue row with the value in bold.  
  
7.  **Speller**: If a value has a wavy red underscore, the Speller is suggesting a correction to the value. Right-click the value with the underscore, and select a correction if one applies. The value type becomes (or stays as) error, and the correction will be added to the **Correct to** column. Click the down arrow to see additional proposed corrections. Enter a correction manually to add it to the Speller dictionary, and be able to select it as a correction. For more information, see [Use the DQS Speller](../data-quality-services/use-the-dqs-speller.md) and [Set Domain Properties](../data-quality-services/set-domain-properties.md).  
  
    > [!NOTE]  
    >  To use the Speller, you can either enable it in the **Domain Properties** page, or if it is disabled in the **Domain Properties** page, you can click the **Enable/Disable Speller** icon on the **Domain Values** page to enable it on that page.  
  
8.  **Add new domain value**: Click to add a row at the end of the table. After you enter a value, the row will be repositioned in alphabetical order, and will be identified as a new entry by a preceding star symbol.  
  
9. **Import domain values from Excel**: To add new values from an Excel spreadsheet, click the down arrow for the **Import Values** icon, and then select **Import domain values from Excel**. Enter the file name, select **Use first row as header** if appropriate, and then click **OK**. For more information, see [Import Values from an Excel File into a Domain](../data-quality-services/import-values-from-an-excel-file-into-a-domain.md).  
  
10. **Import project values**: To add new values from a Data Quality Project by clicking the down arrow for the **Import Values** icon, and selecting **Import project values**. Enter the file name, select **Use first row as header** if appropriate, and then click **OK**. Select the project that you want to import values from, and then click **OK**. The imported values will be displayed. Click **Finish**. For more information, see Import Project Values into a Domain.  
  
11. **Delete selected domain value(s)**: To remove one or more existing values from the domain, select the values in the Value table, and then click the **Delete selected domain value(s)** icon. An entry of DQS_NULL cannot be deleted, so if you choose multiple values to delete, and an entry of DQS_NULL is one of them, the operation will fail.  
  
12. Click **Finish** to complete the domain management activity, as described in [End the Domain Management Activity](/previous-versions/sql/sql-server-2016/hh510411(v=sql.130)).  
  
##  <a name="FollowUp"></a> Follow Up: After Changing Domain Values  
 After you change domain values, you can perform other domain management tasks on the domain, you can perform knowledge discovery to add knowledge to the domain, or you can add a matching policy to the domain. For more information, see [Perform Knowledge Discovery](../data-quality-services/perform-knowledge-discovery.md), [Managing a Domain](../data-quality-services/managing-a-domain.md), or [Create a Matching Policy](../data-quality-services/create-a-matching-policy.md).  
  
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
  
##  <a name="Null"></a> How to Handle Null Equivalents  
 Each value table in the **Domain Values** tab includes a DQS_NULL value. A null in a data source will appear as SQL_NULL in the value table. You can set one or more null equivalents as synonyms to DQS_NULL. When you do so, all nulls and null equivalents will be processed as DQS_NULL.  
  
