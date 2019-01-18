---
title: "Create a Composite Domain | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: data-quality-services
ms.topic: conceptual
f1_keywords: 
  - "sql12.dqs.kb.createcd.f1"
  - "sql12.dqs.dm.cdproperties.f1"
ms.assetid: c7f0bd84-a02e-4a81-885d-985e6415c499
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Create a Composite Domain
  This topic describes how to create a composite domain in a knowledge base in [!INCLUDE[ssDQSnoversion](../includes/ssdqsnoversion-md.md)] (DQS). A composite domain consists of one or more single domains that apply to a single data field. For more information on composite domains, see [Managing a Composite Domain](../../2014/data-quality-services/managing-a-composite-domain.md).  
  
 There are two ways to create a new composite domain. The first is during the Map step of the knowledge discovery activity, when you are in the process of analyzing a data sample to add knowledge to a new or existing knowledge base. The second is during the domain management activity, when instead of changing an existing domain, you create a new one. In order to create a composite domain, you must already have created at least two single domains to add to the composite domain. Only those single domains that have already been created and that have not been added to an existing composite domain are available when you create a new composite domain. A single domain cannot be added to more than one composite domain, and a composite domain cannot be added to another composite domain.  
  
 After creating a composite domain, you can change the properties of the composite domain, attach a reference data service to the domain, create cross-domain rules, or create value relations. To do so, select the composite domain in the **Domain** list of the **Domain Management** page, and select the appropriate tab.  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Prerequisites"></a> Prerequisites  
 To create a composite domain, you must have created and opened a knowledge base, and you must have created at least two single domains to add to the composite domain.  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 You must have the dqs_kb_editor or the dqs_administrator role on the DQS_MAIN database to create a composite domain.  
  
##  <a name="ParsingKnowledgeDiscoveryActivity"></a> Create a Composite Domain in the Knowledge Discovery Activity  
  
1.  [!INCLUDE[ssDQSInitialStep](../includes/ssdqsinitialstep-md.md)] [Run the Data Quality Client Application](../../2014/data-quality-services/run-the-data-quality-client-application.md).  
  
2.  In the [!INCLUDE[ssDQSClient](../includes/ssdqsclient-md.md)] home screen, click **Open knowledge base** and then select a knowledge base, or click **New knowledge base** and enter properties for the new knowledge base.  
  
3.  Select **Knowledge Discovery** as the activity, and then click **Create** to create the new knowledge base or **Open** to open an existing knowledge base.  
  
4.  On the **Map** page, specify a connection to the data source. For more information, see [Perform Knowledge Discovery](../../2014/data-quality-services/perform-knowledge-discovery.md).  
  
5.  In the **Mappings** table, select a source column from the drop-down list for the **Source Column** column of an empty row. Make sure that the source column contains composite domain addressed by two existing single domains. If no corresponding single domains exists, click the **Create a Domain** icon.  
  
6.  In the **Mappings** table, select a source column from the drop-down list for the **Source Column** column of an empty row. Ensure that the source column contains composite domain parts of which are addressed by two existing single domains. If no corresponding single domains exist, click the **Create a Domain** icon to create them. For more information, see [Create a Domain](../../2014/data-quality-services/create-a-domain.md).  
  
7.  Click the **Create a Composite** Domain icon.  
  
##  <a name="DomainManagementActivity"></a> Create a Composite Domain in the Domain Management Activity  
  
1.  In the Data Quality Services client home page, click **Open knowledge base** and then select a knowledge base, or click **New knowledge base** and enter properties for the new knowledge base.  
  
2.  Select **Domain Management** as the activity, and then click **Create** to create the new knowledge base or **Open** to open an existing knowledge base.  
  
3.  Ensure that two or more single domains required by the composite domain exist. If not, click the **Create a Domain** icon and create them. For more information, see [Create a Domain](../../2014/data-quality-services/create-a-domain.md).  
  
4.  On the **Domain Management** page, click the **Create a Composite Domain** icon above the Domain list.  
  
5.  Enter a name that is unique to the knowledge base and a description up to 256 characters.  
  
6.  In the **Domains List**, select the domains that will be part of the composite domain, and click the right arrow to move them to the **Domains in Composite Domain** table.  
  
7.  Click **OK**.  
  
##  <a name="CompositeDomainProperties"></a> Set Composite Domain Properties  
  
1.  In the **Create a Composite Domain** dialog box, enter a name that is unique to the knowledge base and a description up to 256 characters.  
  
2.  In the **Domains List**, select the domains that will be part of the composite domain, and click the right arrow to move them to the **Domains in Composite Domain** table. This is a list of single domains that are available to be added to the composite domain that you are creating. Only those single domains that have already been created and that have not been added to an existing composite domain are available. A single domain cannot be added to more than one composite domain in the knowledge base, and a composite domain cannot be added to another composite domain.  
  
3.  Click **Advanced**.  
  
4.  Select one of the following for the **Parsing Method**:  
  
    -   **Reference Data**: Parse the field's values according to how the data is formatted by the Reference Data Service (RDS). Data Quality Services will send the values in the composite domain to the RDS, and the RDS returns the data corrected and parsed according to the domain in the composite domain.  
  
    -   **In Order**: Parse the field's values according to the order of domains in the composite domain. The first value will be included in the first domain, the second value in the second domains, and so on.  
  
    -   **Delimiters**: Parse the field's values based on the delimiter selected from the radio buttons displayed when Delimiters is selected. Can be **Tab**, **Semicolon**, **Comma**, **Space**, or **Other**. If **Other**, enter the value that will serve as the delimiter.  
  
5.  If you selected **Delimiters** for the parsing method, you can also select **Use Knowledge Based Parsing**. For more information, see [Knowledge-Based Parsing](#KnowledgeBaseParsing).  
  
6.  Click **Finish** to complete the domain management activity, as described in [End the Domain Management Activity](../../2014/data-quality-services/end-the-domain-management-activity.md).  
  
##  <a name="FollowUp"></a> Follow Up: After Creating a Composite Domain  
 After you create a composite domain, you can perform other domain management tasks on the domain, you can perform knowledge discovery to add knowledge to the domain, or you can add a matching policy to the domain. For more information, see [Perform Knowledge Discovery](../../2014/data-quality-services/perform-knowledge-discovery.md), [Managing a Domain](../../2014/data-quality-services/managing-a-domain.md), or [Create a Matching Policy](../../2014/data-quality-services/create-a-matching-policy.md).  
  
##  <a name="KnowledgeBaseParsing"></a> Knowledge-Based Parsing  
 Data Quality Services enables you to parse data based on knowledge, not just on delimiter or order. Knowledge-based parsing is used when complex source data is mapped to a composite domain, and you are not using reference data services. You can use knowledge-based parsing to parse the data from the data source into the relevant single domains. With knowledge-based parsing, DQS will first attempt to use knowledge to parse complex data into single domains. If possible, it will identify parts of the string as in one or more domains, and parse the string into its various domains. For example, suppose you have "John B. Doe" as a complex values in a full-name field represented by a Full Name composite domain. If DQS identifies "John" as in the First Name domain, and "Doe" as in the Last Name domain, then DQS will add "B." to the Middle Name domain based on domain knowledge.  
  
 You can use knowledge-based parsing only if you also select delimiter-based parsing. Knowledge-based parsing does not replace delimiter parsing, but enhances it. Only if no knowledge exists to do that will DQS use a delimiter to do the parsing. In some instances, DQS may determine some parsing by knowledge-based parsing, and then determine other parsing by delimiter-based parsing.  
  
 Knowledge-based parsing can be used when the composite domain is comprised of string domains or when the composite domain is comprised of a mix of different types of domains (int, date, time, etc). If the data source is comprised of different types of data, then the parsing should be done first for the non-string data types and then as described above based on domain knowledge for the rest of the data.  
  
 When you are using knowledge-based parsing, and there are fewer values in the source data than there are domains in the composite domain, then DQS will place a null in the missing domain. When there are more values in the source data than there are domains in the composite domain, then DQS will add the extra data to one of the columns. If two or more domains include the same values, the data source will be parsed to the first matched domain.  
  
  
