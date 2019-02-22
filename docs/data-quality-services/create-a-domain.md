---
title: "Create a Domain | Microsoft Docs"
ms.custom: ""
ms.date: "11/08/2011"
ms.prod: sql
ms.prod_service: "data-quality-services"
ms.reviewer: ""
ms.technology: data-quality-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dqs.kb.createdomain.f1"
ms.assetid: 5c4828f5-bd51-4c29-b3de-87b7d2f2d3e5
author: leolimsft
ms.author: lle
manager: craigg
---
# Create a Domain

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

  This topic describes how to create a domain in [!INCLUDE[ssDQSnoversion](../includes/ssdqsnoversion-md.md)] (DQS). The values in the domain are a semantic representation of the data in a field. For more information on domains, see [Managing a Domain](../data-quality-services/managing-a-domain.md).  
  
 There are two ways to create a new domain. The first is during the Map step of the knowledge discovery activity, when you are in the process of analyzing a data sample to add knowledge to a new or existing knowledge base. The second is during the domain management activity, when instead of changing an existing domain, you create a new one.  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Prerequisites"></a> Prerequisites  
 To create a domain, you must have created and opened a knowledge base.  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 You must have the dqs_kb_editor role or the dqs_administrator on the DQS_MAIN database to create a domain.  
  
##  <a name="Discovery"></a> Create a Domain in the Knowledge Discovery Activity  
  
1.  [!INCLUDE[ssDQSInitialStep](../includes/ssdqsinitialstep-md.md)] [Run the Data Quality Client Application](../data-quality-services/run-the-data-quality-client-application.md).  
  
2.  In the [!INCLUDE[ssDQSClient](../includes/ssdqsclient-md.md)] home screen, click **Open knowledge base** and then select a knowledge base, or click **New knowledge base** and enter properties for the new knowledge base.  
  
3.  Select **Knowledge Discovery** as the activity, and then click **Create** to create the new knowledge base or **Open** to open an existing knowledge base.  
  
4.  On the **Map** page, specify a connection to the data source. For more information, see [Perform Knowledge Discovery](../data-quality-services/perform-knowledge-discovery.md).  
  
5.  In the **Mappings** table, select a source column from the drop-down list for the **Source Column** column of an empty row. If no corresponding domain exists, click the **Create a Domain** icon.  
  
##  <a name="DomainManagement"></a> Create a Domain in the Domain Management Activity  
  
1.  In the [!INCLUDE[ssDQSClient](../includes/ssdqsclient-md.md)] home screen, click **Open knowledge base** and then select a knowledge base, or click **New knowledge base** and enter properties for the new knowledge base.  
  
2.  Select **Domain Management** as the activity, and then click **Create** to create the new knowledge base or **Open** to open an existing knowledge base.  
  
3.  On the **Domain Management** page, click the **Create a Domain** icon above the Domain list.  
  
##  <a name="Properties"></a> Set Domain Properties  
  
1.  In the **Create Domain** dialog box, enter a name that is unique to the knowledge base and a description up to 256 characters.  
  
    > [!NOTE]  
    >  For more information about domain properties, see [Set Domain Properties](../data-quality-services/set-domain-properties.md).  
  
2.  From the **Data Type** list, select a data type for the values in the domain. The data type can be **String** (the default), **Date**, **Integer**, or **Decimal**.  
  
3.  Select **Use Leading Values** to specify that the leading value in a group of synonyms will be output instead of a value that is a synonym to it. Deselect **Use Leading Values** to specify that each synonym value is output in its correct or corrected form, and is not replaced by the leading value for its group.  
  
4.  If the data type is **String**, select **Normalize String** to remove special characters in the domain values, which may improve the likelihood of matches.  
  
5.  From the **Format Output to** drop-down list, select the formatting that will be applied when the data values in the domain are output. The formatting is specific to the data type selected in step 2, as shown in the following list:  
  
    -   For a string value, you can specify that the string be output as upper case, lower case, or capitalized.  
  
    -   For a date value, you can specify the format of the day, month, and year.  
  
    -   For an integer value, you can specify the type of format mask to be applied.  
  
    -   For a decimal value, you can specify the accuracy and the type of format mask to be applied.  
  
     Selecting **None** in the **Format Output to** drop-down list means none of the formats in the list will be applied.  
  
6.  If the data type is **String**, in the **Language** drop-down list, select which language version of the speller you want to apply if you enable the speller.  
  
7.  If the data type is **String**, select **Enable Speller** to run the Speller on all string values when populating the domain.  
  
8.  If the data type is **String**, select **Disable Syntax Error Algorithms** to populate the domain without checking string values for syntax errors.  
  
9. Click **OK**.  
  
10. Click **Finish** to complete the domain management activity, as described in [End the Domain Management Activity](https://msdn.microsoft.com/library/ab6505ad-3090-453b-bb01-58435e7fa7c0).  
  
##  <a name="FollowUp"></a> Follow Up: After Creating a Domain  
 After you create a domain, you can perform other domain management tasks on the domain, you can perform knowledge discovery to add knowledge to the domain, or you can add a matching policy to the domain. For more information, see [Perform Knowledge Discovery](../data-quality-services/perform-knowledge-discovery.md), [Managing a Domain](../data-quality-services/managing-a-domain.md), or [Create a Matching Policy](../data-quality-services/create-a-matching-policy.md).  
  
  
