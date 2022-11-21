---
title: "Create Term-Based Relations"
description: "Create Term-Based Relations"
author: swinarko
ms.author: sawinark
ms.date: "11/08/2011"
ms.service: sql
ms.subservice: data-quality-services
ms.topic: conceptual
f1_keywords:
  - "sql13.dqs.dm.kbtermsbased.f1"
---
# Create Term-Based Relations

[!INCLUDE [SQL Server - Windows only ASDBMI  ](../includes/applies-to-version/sqlserver.md)]

  This topic describes how to create term-based relations for a domain in [!INCLUDE[ssDQSnoversion](../includes/ssdqsnoversion-md.md)] (DQS). A term-based relation (TBR) enables you to make a correction to a term that is part of a value in a domain. It enables multiple values that are identical except for the spelling of a common part of them to be considered identical synonyms. For example, you can set up a term-based relation that changes the term "Inc." to "Incorporated". The term "Inc." will be changed each time it occurs in the domain. Instances of "Contoso, Inc." will be changed "Contoso, Incorporated", and the two values will be considered exact synonyms.  
  
 To use term-based relations, you build a list of Value/Correct To pairs, such as "Inc." and "Incorporated", or "Senior" and "Sr.". Using a term-based relation enables you to change a term throughout the domain without manually setting individual domain values as synonyms. You can specify that a value be corrected even if knowledge discovery has not discovered that value previously. If a term-based relation transformation causes two values to be identical, then DQS will create a synonym relationship between them (in knowledge discovery), a correction relationship between them (in data correction), or an exact match (in matching).  
  
 Term-based relations transformation and symbols transformation (in which special characters are replaced by a space or a null) are both done in a pre-processing stage before analysis. If composite domain parsing is requested, it will be performed before the two transformations, because delimiter parsing requires symbols. Other operations, such as domain rules and domain value changes, will be performed after the transformations. For matching, term-based relations are applied on the source data before the matching activity regardless of whether you run cleansing.  
  
 **Term-Based Relations and Domain Management**  
  
 When you apply a term-based relation in domain management, DQS will apply the changes in the knowledge discovery, cleansing, or matching processes; however, DQS does not change the domain value itself to conform to the term-based relation. In other words, if you enter and accept a term-based relation in the **Term-Based Relations** tab of the **Domain Management** page, the change will not be made in the **Domain Values** tab of the same page. This enables you to change the TBR subsequently.  
  
 **Term-Based Relations and Data Cleaning**  
  
 When you apply a term-based relation in a domain and then run the data cleansing process, DQS applies the changes during cleansing, but does not apply the changes to terms in the knowledge base.  
  
-   If a value as changed by a term-based relation is in the domain, but is not a synonym, will be shown in the **Correct to** column under the **Corrected** tab of the **Manage and View results** page, with the Reason set to Term based relation.  
  
-   If a value as changed by a term-based relation is not in the domain, and DQS finds a matching value, the value will be corrected to it and will appear under the Corrected tab or the Suggested tab, based on the confidence level. If no match is found, the value will appear under New with a TBR correction. This is done because even if you correct the TBR, it does not mean that the value is correct.  
  
-   If a value as changed by a term-based relation is in the domain, but the value is Error/Invalid with existing correction, the value will appear under the Corrected tab with its correction and the reason Domain Value.  
  
-   If a value as changed by a term-based relation is in the domain, but the value is Error/Invalid with no correction, the value will appear under the Invalid tab with the reason Domain Value.  
  
 **Term-Based Relations and Knowledge Discovery**  
  
 When you apply a term-based relation and then run the knowledge discovery process, any value that conforms to the TBR will remain as is and will be identified as a correct value. Any value that is changed by a TBR will be imported as a correct value, and will be identified as a synonym to a value that conforms to the TBR.  
  
 **Term-Based Relations and Import Cleansing Values into a Domain**  
  
 If you import data quality knowledge gathered during the cleansing process into a domain, a value that is changed by a TBR will be imported as a correct value.  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Prerequisites"></a> Prerequisites  
 To create term-based relations, you must have a domain opened in the Domain Management activity.  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 You must have the dqs_kb_editor or the dqs_administrator role on the DQS_MAIN database to create term-based relations.  
  
##  <a name="Create"></a> Create Term-Based Relations  
  
1.  [!INCLUDE[ssDQSInitialStep](../includes/ssdqsinitialstep-md.md)] [Run the Data Quality Client Application](../data-quality-services/run-the-data-quality-client-application.md).  
  
2.  In the [!INCLUDE[ssDQSClient](../includes/ssdqsclient-md.md)] home screen, open or create a knowledge base. Select **Domain Management** as the activity, and then click **Open** or **Create**. For more information, see [Create a Knowledge Base](../data-quality-services/create-a-knowledge-base.md) or [Open a Knowledge Base](../data-quality-services/open-a-knowledge-base.md).  
  
    > [!NOTE]  
    >  Domain management is performed in a page of the Data Quality Service client that contains five tabs for separate domain management operations. It is not a wizard-driven process; any management operation can be performed separately.  
  
3.  From the **Domain list** on the **Domain Management** page, select the domain that you want to create a domain rule for, or create a new domain. If you have to create a new domain, see [Create a Domain](../data-quality-services/create-a-domain.md).  
  
4.  Click the **Term-Based Relations** tab.  
  
5.  Create term-based relations as follows:  
  
    1.  Click **Add New Relation** to add a row to the Relations table.  
  
    2.  To the **Value** column of the added row, enter a term that you want to change each time it occurs in a value in the selected domain.  
  
        > [!NOTE]  
        >  You will get an error if the term exists as a whole value in the domain, or if it already exists as a correcting value in the domain.  
  
    3.  To the **Correct To** column, enter a term that you want to change the term in the **Value** column to.  
  
    4.  Click **Add New Relations** again to add another term-based relation.  
  
    5.  Click **Delete Selected Relations** to delete one or more selected rows from the Relations table. You can select multiple rows by pressing the Ctrl button and clicking an unselected row.  
  
    6.  Find a value in the Relations table by entering one or more digits in the **Find** text box. Matches for the string will be highlighted. Use the up and down arrows to move to different instances of the string in the table.  
  
    7.  **Speller**: If a value in the **Value** or **Correct to** column has a wavy red underscore, the Speller is suggesting a correction to the value. Right-click the value with the underscore, and select one of the proposed values by the Speller. Alternately, you can click **Add** in the shortcut menu tp proceed with the original value. For more information, see [Use the DQS Speller](../data-quality-services/use-the-dqs-speller.md) and [Set Domain Properties](../data-quality-services/set-domain-properties.md).  
  
        > [!NOTE]  
        >  To use the Speller, you can either enable it in the **Domain Properties** page, or if it is disabled in the **Domain Properties** page, you can click the **Enable/Disable Speller** icon on the **Term-Based Relations** page to enable it on this page.  
  
6.  Click **Apply Changes** to apply the term-based relations to the domain.  
  
7.  Click **Finish** to complete the domain management activity, as described in [End the Domain Management Activity](/previous-versions/sql/sql-server-2016/hh510411(v=sql.130)).  
  
##  <a name="FollowUp"></a> Follow Up: After Creating Term-Based Relations  
 After you create term-based relations, you can perform other domain management tasks on the domain, you can perform knowledge discovery to add knowledge to the domain, or you can add a matching policy to the domain. For more information, see [Perform Knowledge Discovery](../data-quality-services/perform-knowledge-discovery.md), [Managing a Domain](../data-quality-services/managing-a-domain.md), or [Create a Matching Policy](../data-quality-services/create-a-matching-policy.md).  
  
