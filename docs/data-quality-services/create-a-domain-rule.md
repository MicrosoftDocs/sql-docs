---
title: "Create a Domain Rule | Microsoft Docs"
ms.custom: ""
ms.date: "11/08/2011"
ms.prod: sql
ms.prod_service: "data-quality-services"
ms.reviewer: ""
ms.technology: data-quality-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dqs.dm.testdomainrule.f1"
  - "sql13.dqs.dm.rules.f1"
ms.assetid: 339fa10d-e22c-4468-b366-080c33f1a23f
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Create a Domain Rule

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

  This topic describes how to create a domain rule in [!INCLUDE[ssDQSnoversion](../includes/ssdqsnoversion-md.md)] (DQS). A domain rule is a condition that is used to validate, correct, and standardize domain values. A domain rule must hold true across a domain in order for domain values to be considered accurate and conformant to business requirements. Domain rules can include validation rules that are used to validate domain values, but are not used to correct data in a data quality projects. Rules also include standardization rules that are applied against valid data and are used in data correction.  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Prerequisites"></a> Prerequisites  
 To create a domain rule, you must have a knowledge base and a domain opened in the Domain Management activity.  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 You must have the dqs_kb_editor or the dqs_administrator role on the DQS_MAIN database to create a domain rule.  
  
##  <a name="Build"></a> Build Domain Rules  
  
1.  [!INCLUDE[ssDQSInitialStep](../includes/ssdqsinitialstep-md.md)] [Run the Data Quality Client Application](../data-quality-services/run-the-data-quality-client-application.md).  
  
2.  In the [!INCLUDE[ssDQSClient](../includes/ssdqsclient-md.md)] home screen, open or create a knowledge base. Select **Domain Management** as the activity, and then click **Open** or **Create**. For more information, see [Create a Knowledge Base](../data-quality-services/create-a-knowledge-base.md) or [Open a Knowledge Base](../data-quality-services/open-a-knowledge-base.md).  
  
    > [!NOTE]  
    >  Domain management is performed in a page of the Data Quality Service client that contains five tabs for separate domain management operations. It is not a wizard-driven process; any management operation can be performed separately.  
  
3.  From the **Domain list** on the **Domain Management** page, select the domain that you want to create a domain rule for, or create a new domain. If you have to create a new domain, see [Create a Domain](../data-quality-services/create-a-domain.md).  
  
4.  Click the **Domain Rules** tab.  
  
5.  Click **Add a new domain rule**, and then enter a name that is unique in the knowledge base and a description for the rule.  
  
6.  Select **Active** to specify that the rule will be run (the default), or deselect to prevent the rule from running.  
  
7.  In the **Build a Rule** pane, select a condition from the drop-down list in the rule's clause box.  
  
8.  If the condition requires a value, enter the value in the associated text box.  
  
9. Click **Adds a new condition to the selected clause** icon if another clause is required.  
  
10. Select **AND** or **OR** as the operator.  
  
11. Select a condition from the drop-down list and then enter a value for the operand, if required.  
  
12. To change the order in which the clauses appear in the list, select a clause and then click the up or down arrow. This will change the order in which they are executed, which could affect the results.  
  
13. Add more clauses as required. If needed, delete a clause by selecting it and then clicking **Deletes the selected clause**.  
  
14. Repeat to add new rules, as necessary.  
  
15. To see the impact that a validation rule would have on values if implemented, click the **Analyze the domain rule impact on the domain values** icon.  
  
16. Proceed to the test procedure below.  
  
##  <a name="Test"></a> Test Domain Rules  
  
1.  With one rule selected, click the **Run the selected domain rule on test data** icon.  
  
2.  In the Test Domain Rule dialog box, click the **Add a new testing term for the domain rule** icon. Enter a value to test. Enter other values as required. Select a value and click the **Remove the selected testing term** icon if required.  
  
3.  Click the **Test the domain rule on all the terms** icon.  
  
4.  Check the validity of each term. A check means "correct", a cross means "error", and a triangle means "invalid".  
  
5.  Click **Close** when done in the testing dialog box.  
  
6.  Repeat for other rules, as necessary.  
  
7.  Proceed to the application procedure below.  
  
##  <a name="Apply"></a> Apply Domain Rules  
  
1.  Click **Apply All Rules** to apply the rules to the values in the domain. Apply you click **Apply All Rules**, a popup will be displayed indicating how many values in certain states will be affected by the rule. Click **Yes** if you still want to apply the rule, or **No** if not. If you click **Yes**, click **OK** to close the results popup.  
  
    > [!NOTE]  
    >  When you create or change a rule, you do not need to save the changes. However, you must apply the rule for the changes to take effect.  
  
2.  Click **Discard All Changes** to remove any changes that you have made to domain rules, reverting to the previously applied rules, with the result that any changes made after the last application of the rules will no longer apply. The validity of each value in the domain will be updated to be in accordance with the previously applied rules, not the discarded changes.  
  
3.  Click **Finish** to complete the domain management activity, as described in [End the Domain Management Activity](https://msdn.microsoft.com/library/ab6505ad-3090-453b-bb01-58435e7fa7c0).  
  
##  <a name="FollowUp"></a> Follow Up: After Creating a Domain Rule  
 After you create a domain rule, you can perform other domain management tasks on the domain, you can perform knowledge discovery to add knowledge to the domain, or you can add a matching policy to the domain. For more information, see [Perform Knowledge Discovery](../data-quality-services/perform-knowledge-discovery.md), [Managing a Domain](../data-quality-services/managing-a-domain.md), or [Create a Matching Policy](../data-quality-services/create-a-matching-policy.md).  
  
##  <a name="Conditions"></a> Domain Rule Conditions  
 The table below describes the conditions that can be applied in the domain rule, and provides example to illustrate how the conditions can be applied.  
  
 When a domain rule is applied and a domain value fails the rule, the value is designated Invalid. A value that is designated Invalid will be changed to Correct if the rule causing it to be invalid is deleted, is deactivated, or the rule has been changed such that the value no longer fails the rule. If you have designated a value as Invalid manually (in the Domain Values tab of the Domain Management activity), and a rule that the value fails has been deleted, deactivated, or changed, then the value will still be designated Invalid, in accordance with the manual designation.  
  
 A domain rule that has a definitive condition will apply the rules logic to synonyms of the value in the condition or conditions, as well the values themselves. The definitive conditions are Value is equal to, Value is not equal to, Value is in, or Value is not in. For example, suppose you have the following domain rule: "For 'City', Value is equal to 'Los Angeles'". If 'Los Angeles' and 'LA' are synonyms, both will be correct. On the other hand, if your rule did not contain a definitive condition, such as "For City, Value ends with "s", then "Los Angeles" would be correct, but its synonym "LA" would be in error.  
  
 You have alternatives to choose from in creating a domain rule. For example, to validate whether values begin with the letter A, B, or C, you could create a simple rule with a complex condition (such as a regular expression with pipe characters), or you could create a complex rule that contains several simple conditions. An example of the first rule is "Value contains regular expression (^A|^B|^C)". An example of the second rule is "'Value begins with A' OR 'Value begins with B' OR 'Value begins with C'".  
  
|Condition|Description|Example|  
|---------------|-----------------|-------------|  
|Length is equal to|Only values consisting of the number of characters designated by the operand will be valid.|Example operand: 3<br /><br /> Valid value: BB1<br /><br /> Not valid value: AA|  
|Length is greater than or equal to|Only values consisting of the number of characters designated by the operand, or a greater number of characters, will be valid.|Example operand: 3<br /><br /> Valid values: BB1, BBAA<br /><br /> Not valid value: AA|  
|Length is less than or equal to|Only values consisting of the number of characters designated by the operand, or a lesser number of characters, will be valid.|Example operand: 3<br /><br /> Valid values: BB1, AA<br /><br /> Not valid value: BBAA|  
|Value is equal to|Only values that are identical to the operand will be valid.|Example operand: BB1<br /><br /> Valid value: BB1<br /><br /> Not valid value: BB, BB1#|  
|Value is not equal to|Only values that are not identical to the operand will be valid.|Example operand: BB1<br /><br /> Valid value: BB, BB1#<br /><br /> Not valid value: BB1|  
|Value contains|Only values all of whose characters are contained within the operand, in any order, will be valid.|Example operand: A1<br /><br /> Valid values: A1, AA1<br /><br /> Not valid value: 1A, AA|  
|Value does not contain|Only values that are not contained within the operand will be valid.|Example operand: A1<br /><br /> Valid values: 1A, AA<br /><br /> Not valid values: A1, AA1|  
|Value begins with|Only values that begin with the characters in the operand will be valid.|Example operand: AA<br /><br /> Valid values: AA1<br /><br /> Not valid values: 1AAB|  
|Value ends with|Only values that end with the characters in the operand will be valid.|Example operand: AA<br /><br /> Valid values: 1AA<br /><br /> Not valid values: 1AAB|  
|Value is numeric|Only values that have a SQL Server numeric data type will be valid. This includes int, decimal, float, etc.|Example operand: N/A<br /><br /> Valid values: 1, 25, 345.1234<br /><br /> Not valid values: 2b, bcdef|  
|Value is date/time|Only values that have a SQL Server date/time data type will be valid. This includes datetime, time, date, etc.|Example operand: N/A<br /><br /> Valid values: 1916-06-04; 1916-06-04 18:24:24; March 21, 2001; 5/18/2011; 18:24:24<br /><br /> Not valid values: March 213, 2006|  
|Value is in|Only values that are in the set in the operand will be valid.<br /><br /> To enter the values in the set, click in the operand text box, enter the first value, press Enter, enter the second value, repeat for as many values as you want to enter in the set, and then click again in the operand text box. DQS will add a comma between the values in the set. If you enter a single string with commas and no carriage return (for example, "A1, B1"), DQS will consider that string a single value in the set.|Example operand: [A1, B1]<br /><br /> Valid values: A1, B1<br /><br /> Not valid values: AA, 11|  
|Value is not in|Only values that are not in the set in the operand will be valid.|Example operand: [A1, B1]<br /><br /> Valid values: AA, 11<br /><br /> Not valid values: A1, B1|  
|Value matches pattern|Only values that match the pattern of characters, digits, or special characters in the operand will be valid.<br /><br /> Any letter (A...Z) can be used as a pattern for any letter; case insensitive. Any digit (0...9) can be used as a pattern for any digit. Any special character, except a letter or a digit, can be used as a pattern for itself. Brackets, [], define optional matching.|Example operand: AA:000 (a pattern of *any* two characters followed by a colon (:), which is again followed by *any* three digits.<br /><br /> Valid values: AB:012, df:257<br /><br /> Not valid values: abc:123, FJ-369<br /><br /> For more information about the pattern rules in DQS and examples, see [Pattern Matching in DQS Domain Rules](https://blogs.msdn.com/b/dqs/archive/2012/10/08/pattern-matching-in-dqs-domain-rules.aspx).|  
|Value does not match pattern|Only values that do not match the pattern of characters, digits, or special characters in the operand will be valid.|Example operand: A1 (value must not match a pattern of *any* one character followed by *any* one digit.)<br /><br /> Valid values: AB1, A, A:5<br /><br /> Not valid values: B7, c9|  
|Value contains pattern|Only values that contain the pattern of characters, digits, or special characters in the operand will be valid.|Example operand: AA-12 (value contains a pattern of *any* two characters followed by a hyphen (-), which is again followed by *any* two digits.)<br /><br /> Valid values: AAA-01, ab-975<br /><br /> Not valid value: A7, AA-6, C-45, aa;98|  
|Value does not contain pattern|Only values that do not contain the pattern of characters in the operand will be valid.|Example operand: AB-12 (value must not contain a pattern of *any* two characters followed by a hyphen (-), which is again followed by *any* two digits.)<br /><br /> Valid values: A7, AA-6, C-45, aa;98<br /><br /> Not valid value: AAA-01, ab-975|  
|Value matches regular expression|Only values that equal the regular expression in the operand will be considered valid.<br /><br /> Do not include the "^" anchor or the "$" anchor to the regular expression, because DQS automatically adds those anchors to a clause containing a Value equals regular expression. (Alternatively, you can enclose the regular expression containing "^" and "$" anchors with parentheses.) For more information about regular expressions, see [Regular Expression Language Elements](https://go.microsoft.com/fwlink/?LinkId=225561).|Example operand: [1-5]+ (each character must be a numeric digit from 1 to 5, occurring one or more times)<br /><br /> Valid values: 123, 12345, 14352<br /><br /> Not valid values: 456, ABC|  
|Value does not match a regular expression|Only values that do not match the regular expression in the operand will be considered valid.|Example operand: [1-5]+ (the string must not be only numeric digits from 1 to 5)<br /><br /> Valid values: 456, ABC<br /><br /> Not valid value: 123, 123456, 14352|  
  
  
