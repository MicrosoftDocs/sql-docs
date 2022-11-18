---
title: "Use the DQS Speller"
description: "Use the DQS Speller"
author: swinarko
ms.author: sawinark
ms.date: "11/08/2011"
ms.service: sql
ms.subservice: data-quality-services
ms.topic: conceptual
---
# Use the DQS Speller

[!INCLUDE [SQL Server - Windows only ASDBMI  ](../includes/applies-to-version/sqlserver.md)]

  The [!INCLUDE[ssDQSnoversion](../includes/ssdqsnoversion-md.md)] (DQS) Speller checks the syntax, spelling, and sentence structure of string values in a domain. The Speller is a standalone, client-side feature that has no integration with server-side engines and no implications on current flows or statuses. The Speller identifies those string values that it considers to be potential errors, and then marks them with a red underscore in the same location in which you make other manual changes to domain values. These locations include:  
  
-   The **Manage Domain Values** page of the **Knowledge Discovery** activity  
  
-   The **Domain Values** page or the **Term-Based Relations** page of the **Domain Management** activity  
  
-   The **Manage and View results** page of the **Cleansing** activity  
  
 The Speller only works on single domains with a data type of string. All values in a single domain that are of a string data type are sent to the speller for validation. The Speller does not work for a composite domain, and it does not work for domains of types other than string, mixed values (such as letters and numbers with no space), Roman numerals, single characters, and values that consist only of upper-case letters.  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Prerequisites"></a> Prerequisites  
 To run the Speller, you must have a knowledge base and a domain opened in the Knowledge Discovery or Domain Management activity; the Speller must be enabled for the domain and in the page where you are going to run it; and the language property must be specified for the domain.  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 You must have the dqs_kb_editor or the dqs_administrator role on the DQS_MAIN database to run the Speller.  
  
##  <a name="Enable"></a> Enable the Speller  
  
1.  To enable the Speller in [!INCLUDE[ssDQSClient](../includes/ssdqsclient-md.md)], open the knowledge base in the **Domain Management** activity, select the desired domain, and click **Enable Speller** on the **Domain Properties** page. In **Language**, select the language to be used with the Speller.  
  
2.  When the Speller is enabled in the domain properties, it is enabled in the **Manage Domain Values** page, the **Domain Values** page or the **Term-Based Relations** page, and the **Manage and View results** page. To disable the Speller on these pages, click the **Enable/Disable Speller** icon. Clicking the icon changes the status of the Speller on the page. Likewise, if the **Enable Speller** property for the domain is disabled, clicking the **Enable/Disable Speller** icon enables the Speller on the page. If you exit the page and then return to it, the button status is again determined by the **Enable Speller** domain property.  
  
##  <a name="Use"></a> Use the Speller  
  
1.  Move to one of the following pages:  
  
    -   The **Manage Domain Values** page of the **Knowledge Discovery** activity  
  
    -   The **Domain Values** page or the **Term-Based Relations** page of the **Domain Management** activity  
  
    -   The **Manage and View results** page of the **Cleansing** activity  
  
2.  Display the appropriate values in the **Value** table by filtering or searching.  
  
3.  Scan the rows in the **Value** table to determine whether any value in the **Value** or **Correct To** columns is marked with a wavy red underscore.  
  
4.  Right-click a value that is marked by a red underscore. Click one of the listed replacement values if it is preferable to the original value.  
  
5.  If no displayed value is preferable, and there is a **More suggestions** button indicating additional values, click it. If one of the additional values is preferable to the original, click it.  
  
6.  If you want to add the value to the dictionary, click **Add to Dictionary**. The red underscore will disappear from the value.  
  
##  <a name="FollowUp"></a> Follow Up: After Using the Speller  
 After you have run the Speller, complete the activity that the domain is in to use the corrections suggested by the Speller. If in the knowledge discovery, domain management, or matching policy activity, publish the knowledge base in order to make the results of the Speller analysis available for use in the knowledge base. For more information, see [Perform Knowledge Discovery](../data-quality-services/perform-knowledge-discovery.md), [Managing a Domain](../data-quality-services/managing-a-domain.md), or [Create a Matching Policy](../data-quality-services/create-a-matching-policy.md).  
  
##  <a name="How"></a> How the Speller Works  
 The DQS Speller marks any potential string value error with a red underscore that is displayed for the entire value. For example, if "New York" is incorrectly spelled as "Neu York", the speller will display a red underscore under "Neu York", and not just "Neu". If you right-click the value, you will see suggested corrections for the whole value. You can also click **More suggestions** if there are more than five suggestions. You can pick one of the suggestions or add a value to the dictionary (at a user account level) to be displayed for the original value. Values added to the dictionary apply to all domains. Only if you explicitly designate a suggestion will the correction be made in the domain. When you select a suggestion from the Speller context menu, the value type becomes (or stays as) an error. The selected suggestion will be added to the correction column. Note that a value can have a **Type** of **Correct** and yet be marked as a potential error by the Speller.  
  
 DQS will provide suggestions for values in both the **Value** column and the **Correct To** column of the **Value** table. When you select a suggestion in the **Value** column, the value type is set to **Error**, and the suggestion is copied to the **Correct To** column, as if you inserted it manually. If there was an existing correction, it becomes a suggestion. In the **Manage and View results** page of the **Cleansing** activity, when you select a suggestion in the **Correct To** column, DQS will replace the currently selected value with the selection, and the currently selected value will become a suggestion. In the **Manage and View results** page of the **Cleansing** activity, no suggestions are made in the record-level (the lower grid).  
  
  
