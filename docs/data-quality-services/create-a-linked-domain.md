---
title: "Create a Linked Domain"
description: "Create a Linked Domain"
author: swinarko
ms.author: sawinark
ms.date: "11/08/2011"
ms.service: sql
ms.subservice: data-quality-services
ms.topic: conceptual
f1_keywords:
  - "sql13.dqs.kb.linkeddomain.f1"
---
# Create a Linked Domain

[!INCLUDE [SQL Server - Windows only ASDBMI  ](../includes/applies-to-version/sqlserver.md)]

  This topic describes how to create a linked domain in a knowledge base in [!INCLUDE[ssDQSnoversion](../includes/ssdqsnoversion-md.md)] (DQS). A linked domain is created from another, previously existing domain, and inherits all values, rules, and properties from the domain that it is linked to, with the exception of the name and the description. You can manage a set of linked domains as one. By linking one domain to the other, you create a domain that inherits its contents from another domain.  
  
## Scenarios  
 Linked domains are particularly useful in the following scenarios.  
  
### Mapping multiple fields to domains that share values, rules, and properties  
 You cannot map two fields to the same domain, but you could map one field to a domain and then map a second field to a domain linked to the first domain. Doing so maps the fields to two different domains that have the same contents and properties (except name and description). For more information, see [Map two fields to linked domains](#Map).  
  
### Controlling data flow to composite domains  
 Linked domains enable you to control the data flow between fields and composite domains. You can differentiate when data from one field flows into a composite domain, and when data from another, very similar field does not flow into the composite domain. You do so by specifying that of two linked domains, one is part of a composite domain, and one is not. From a domain perspective, linked domains are identical. They contain the same knowledge. However, from a composite-domain perspective, linked domains are different. One participates in the composite domain; the other does not.  
  
 An example is a record that contains the following fields: Customer First Name, Customer Last Name, and Father's First Name. Suppose you map both customer first name and father's first name to a First Name domain, and make the First Name domain and the Last Name domain a part of a Full Name composite domain. The problem is that the father's first name will be added to the composite domain without a last name. If, however, you link each of the two first name fields to a domain, and link the two domains, then you can add the Customer First Name domain to the Full Name composite domain, and not add the Father's First Name field to the composite domain, thereby preventing the Father's First Name from being added to the composite domain.  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Prerequisites"></a> Prerequisites  
 To create a linked domain, you must have a knowledge base and an existing domain that you want to link to.  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 You must have the dqs_kb_editor or the dqs_administrator role on the DQS_MAIN database to create a linked domain.  
  
##  <a name="Create"></a> Create a Linked Domain  
  
1.  [!INCLUDE[ssDQSInitialStep](../includes/ssdqsinitialstep-md.md)] [Run the Data Quality Client Application](../data-quality-services/run-the-data-quality-client-application.md).  
  
2.  In the [!INCLUDE[ssDQSClient](../includes/ssdqsclient-md.md)] home screen, open or create a knowledge base. Select **Domain Management** as the activity, and then click **Open** or **Create**. For more information, see [Create a Knowledge Base](../data-quality-services/create-a-knowledge-base.md) or [Open a Knowledge Base](../data-quality-services/open-a-knowledge-base.md).  
  
3.  From the **Domain list** on the **Domain Management** page, right-click the domain that you want to link a new domain to, and then click **Create Linked Domain**.  
  
    > [!NOTE]  
    >  There is not an icon dedicated to creating a linked domain. You can only do so using the command in the context menu.  
  
4.  In the **Create Domain** dialog box, enter a name that is unique to the knowledge base and a description of up to 256 characters. Verify that the name of the domain linked to is correct.  
  
5.  Click **OK** to complete creation of the linked domain.  
  
6.  If necessary, you can change the name or description of the linked domain in the Domain Properties tab.  
  
7.  Click **Finish** to complete the domain management activity, as described in [End the Domain Management Activity](/previous-versions/sql/sql-server-2016/hh510411(v=sql.130)).  
  
##  <a name="Map"></a> Map two fields to linked domains  
  
1.  Open a knowledge base to the knowledge discovery activity, and map the knowledge base to the database and table or view.  
  
2.  Map one field to a domain, and then attempt to map a second field to the same domain.  
  
3.  In the popup indicating that the domain is already in use, click Yes to create a linked domain.  
  
4.  In the Create Domain dialog box, enter a domain name and description, and then click OK.  
  
##  <a name="FollowUp"></a> Follow Up: After Creating a Linked Domain  
 After you create a linked domain, you can perform other domain management tasks on the domain, you can perform knowledge discovery to add knowledge to the domain, or you can add a matching policy to the domain. For more information, see [Perform Knowledge Discovery](../data-quality-services/perform-knowledge-discovery.md), [Managing a Domain](../data-quality-services/managing-a-domain.md), or [Create a Matching Policy](../data-quality-services/create-a-matching-policy.md).  
  
##  <a name="Behavior"></a> Behavior of a Linked Domain  
 You can change the settings for a linked domain as follows:  
  
-   You can change the name and description of a linked domain.  
  
-   To change the domain properties for the **Data Type**, **Use Leading Values**, or **Format Output To** properties, select the domain that you linked to, and change those settings in the **Domain Properties** tab for that domain. You cannot change those settings in the properties of the linked domain. For more information, see [Create a Domain](../data-quality-services/create-a-domain.md).  
  
-   Settings in the **Reference Data**, **Domain Rules**, **Domain Values**, and **Term-based Relations** tabs of the Domain Management page can be changed for either the linked domain or the domain that it was linked to, and the changes will be inherited by the other domain.  
  
 Linked domains have the following characteristics:  
  
-   You cannot unlink two domains. To remove the link, delete one of the linked domains.  
  
-   When you select a linked domain in the domain list of the Domain Management page, the string identifying the linked domain in the pane containing the **Value** table contains an indication that the domain is a linked domain.  
  
-   If you delete a domain that another domain is linked to, both domains will be deleted. You can, however, delete a linked domain, and the domain linked to will not be deleted.  
  
-   A linked domain cannot be linked to a domain that itself is linked to another domain.  
  
-   You cannot create a linked domain or a linked composite domain to a composite domain.  
  
-   When you double-click a linked domain in any of the Domain Management tabs, the domain will be opened to editing with an indication in the name string that it is a linked domain.  
  
