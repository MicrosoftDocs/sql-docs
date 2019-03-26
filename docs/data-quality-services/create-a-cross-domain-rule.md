---
title: "Create a Cross-Domain Rule | Microsoft Docs"
ms.custom: ""
ms.date: "11/22/2011"
ms.prod: sql
ms.prod_service: "data-quality-services"
ms.reviewer: ""
ms.technology: data-quality-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dqs.dm.testcdrule.f1"
  - "sql13.dqs.dm.cdrules.f1"
ms.assetid: 0f3f5ba4-cc47-4d66-866e-371a042d1f21
author: leolimsft
ms.author: lle
manager: craigg
---
# Create a Cross-Domain Rule

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

  This topic describes how to create a cross-domain rule for a composite domain in a knowledge base in [!INCLUDE[ssDQSnoversion](../includes/ssdqsnoversion-md.md)] (DQS). A cross-domain rule tests the relationship between values in single domains that are included in a composite domain. A cross-domain rule must hold true across a composite domain in order for domain values to be considered accurate and conformant to business requirements. A cross-domain rule is used to validate, correct, and standardize domain values.  
  
 The If clause and Then clause of a cross-domain rule are each defined for one of the single domains in the composite domain. Each clause must be defined for a different single domain. A cross-domain rule must relate to multiple single domains; you cannot define a simple domain rule (for only a single domain) for a composite domain. You would do so by defining a domain rule for a single domain. The If clause and the Then clause can each contain one or more conditions.  
  
 A cross-domain rule that has definitive conditions will apply the rules logic to synonyms of the value in the conditions, as well the values themselves. The definitive conditions for the If and Then clauses are Value is equal to, Value is not equal to, Value is in, or Value is not in. For example, suppose that you have the following cross-domain rule for a composite domain: "For 'City', if Value is equal to 'Los Angeles', then for 'State', Value is equal to 'CA'. "If 'Los Angeles' and 'LA' are synonyms, this rule will return correct for 'Los Angeles CA' and 'LA CA' and in error for 'Los Angeles WA' and 'LA WA'.  
  
 Apart from just letting you know about the validity of a cross-domain rule, the definitive *Then* clause in a cross-domain rule, **Value is equal to**, also corrects the data during the data-cleansing activity. For more information, see [Data Correction using Definitive Cross-Domain Rules](../data-quality-services/cleanse-data-in-a-composite-domain.md#CDCorrection) in [Cleanse Data in a Composite Domain](../data-quality-services/cleanse-data-in-a-composite-domain.md).  
  
 Cross-domain rules are taken into consideration after all simple rules that affect only a single domain. Only if a value passes single domain rules (if they exist) is the cross-domain rule applied. The composite domain and the single domains that a rule is run on must all be defined before the rule can be executed.  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Prerequisites"></a> Prerequisites  
 To create a cross-domain rule, you must have created and opened a composite domain.  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 You must have the dqs_kb_editor or the dqs_administrator role on the DQS_MAIN database to create a cross-domain rule.  
  
##  <a name="Create"></a> Create Cross-Domain Rules  
  
1.  [!INCLUDE[ssDQSInitialStep](../includes/ssdqsinitialstep-md.md)] [Run the Data Quality Client Application](../data-quality-services/run-the-data-quality-client-application.md).  
  
2.  In the [!INCLUDE[ssDQSClient](../includes/ssdqsclient-md.md)] home screen, open or create a knowledge base. Select **Domain Management** as the activity, and then click **Open** or **Create**. For more information, see [Create a Knowledge Base](../data-quality-services/create-a-knowledge-base.md) or [Open a Knowledge Base](../data-quality-services/open-a-knowledge-base.md).  
  
    > [!NOTE]  
    >  Domain management is performed in a page of the Data Quality Service client that contains five tabs for separate domain management operations. It is not a wizard-driven process; any management operation can be performed separately.  
  
3.  From the **Domain list** on the **Domain Management** page, select the composite domain that you want to create a domain rule for, or create a new composite domain. If you have to create a new domain, see [Create a Composite Domain](../data-quality-services/create-a-composite-domain.md).  
  
4.  Click the **CD Rules** tab.  
  
5.  Click **Add a new domain rule**, and then enter a name and description for the rule.  
  
6.  Select **Active** to specify that the rule will be run (the default), and deselect to prevent the rule from running.  
  
7.  Create the If clause as follows:  
  
    1.  In the domain list in the If clause pane, select one of the single domains included in the composite domain to be the subject of the If clause. You can select any single domain in the composite domain.  
  
    2.  Select a condition from the drop-down list for the first condition of the clause.  
  
    3.  If the condition requires a value, enter the value in the text box associated with the condition.  
  
    4.  If the If clause requires another condition, click **Adds a new condition to the selected clause**. Select the operator, select a condition, and enter a value for the condition, if necessary.  
  
    5.  To change the order of the conditions, select a condition by clicking to its left, and then click the up or down arrow.  
  
    6.  To hide the conditions, click the minus sign to the left of the domain name. Click the plus ign to display the conditions.  
  
8.  Create the Then clause by selecting a single domain, other than the subject of the If clause, in the domain list in the Then clause pane. Then build the Then clause using the same steps that you did in building the If clause.  
  
9. Proceed to the testing procedure below.  
  
##  <a name="Test"></a> Test Cross-Domain Rules  
  
1.  Test the cross-domain rule as follows:  
  
    1.  Click the **Run the selected domain rule on test data to** icon in the upper right-hand corner of the composite domain pane.  
  
    2.  In the **Test Domain Rule** dialog box, click the **Adds a New Testing Term for the Domain Rule** icon.  
  
    3.  Enter test values for the single domain associated with the If clause and the single domain associated with the Then clause. The test values entered in the If clause must meet the conditions for that clause, or a question mark will be entered in the **Validity** column indicating that the cross-domain rule does not apply to the test data.  
  
    4.  Click the **Adds a new testing term for the domain rule** icon again to add another set of test values.  
  
    5.  Click the **Test the Domain Rule on All the Terms** icon. If a set of test values is valid, DQS will enter a check in the **Validity** column for the row. If the set of test values is not valid, DQS will enter a triangle with an exclamation point in the Validity column for the row.  
  
    6.  After your testing is complete, click **Close** in the **Test Composite Domain Rule** dialog box.  
  
2.  When you have completed your cross-domain rules, click **Finish** to complete the domain management activity, as described in [End the Domain Management Activity](https://msdn.microsoft.com/library/ab6505ad-3090-453b-bb01-58435e7fa7c0).  
  
##  <a name="FollowUp"></a> Follow Up: After Creating a Cross-Domain Rule  
 After you create a cross-down rule, you can perform other domain management tasks on the domain, you can perform knowledge discovery to add knowledge to the domain, or you can add a matching policy to the domain. For more information, see [Perform Knowledge Discovery](../data-quality-services/perform-knowledge-discovery.md), [Managing a Domain](../data-quality-services/managing-a-domain.md), or [Create a Matching Policy](../data-quality-services/create-a-matching-policy.md).  
  
  
