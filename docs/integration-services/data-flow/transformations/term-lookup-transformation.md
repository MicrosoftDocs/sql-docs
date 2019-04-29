---
title: "Term Lookup Transformation | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.termlookuptrans.f1"
  - "sql13.dts.designer.termlookup.termlookup.f1"
  - "sql13.dts.designer.termlookup.referencetable.f1"
  - "sql13.dts.designer.termlookup.advanced.f1"
helpviewer_keywords: 
  - "extracting data [Integration Services]"
  - "match extracted terms [Integration Services]"
  - "text extraction [Integration Services]"
  - "term extractions [Integration Services]"
  - "lookups [Integration Services]"
  - "counting extracted items"
  - "Term Lookup transformation"
ms.assetid: 3c0fa2f8-cb6a-4371-b184-7447be001de1
author: janinezhang
ms.author: janinez
manager: craigg
---
# Term Lookup Transformation
  The Term Lookup transformation matches terms extracted from text in a transformation input column with terms in a reference table. It then counts the number of times a term in the lookup table occurs in the input data set, and writes the count together with the term from the reference table to columns in the transformation output. This transformation is useful for creating a custom word list based on the input text, complete with word frequency statistics.  
  
 Before the Term Lookup transformation performs a lookup, it extracts words from the text in an input column using the same method as the Term Extraction transformation:  
  
-   Text is broken into sentences.  
  
-   Sentences are broken into words.  
  
-   Words are normalized.  
  
 To further customize which terms to match, the Term Lookup transformation can be configured to perform a case-sensitive match.  
  
## Matches  
 The Term Lookup performs a lookup and returns a value using the following rules:  
  
-   If the transformation is configured to perform case-sensitive matches, matches that fail a case-sensitive comparison are discarded. For example, *student* and *STUDENT* are treated as separate words.  
  
    > [!NOTE]  
    >  A non-capitalized word can be matched with a word that is capitalized at the beginning of a sentence. For example, the match between *student* and *Student* succeeds when *Student* is the first word in a sentence.  
  
-   If a plural form of the noun or noun phrase exists in the reference table, the lookup matches only the plural form of the noun or noun phrase. For example, all instances of *students* would be counted separately from the instances of *student*.  
  
-   If only the singular form of the word is found in the reference table, both the singular and the plural forms of the word or phrase are matched to the singular form. For example, if the lookup table contains *student*, and the transformation finds the words *student* and *students*, both words would be counted as a match for the lookup term *student*.  
  
-   If the text in the input column is a lemmatized noun phrase, only the last word in the noun phrase is affected by normalization. For example, the lemmatized version of *doctors appointments* is *doctors appointment*.  
  
 When a lookup item contains terms that overlap in the reference set-that is, a sub-term is found in more than one reference record-the Term Lookup transformation returns only one lookup result. The following example shows the result when a lookup item contains an overlapping sub-term. The overlapping sub-term in this case is *Windows*, which is found within two reference terms. However, the transformation does not return two results, but returns only a single reference term, *Windows*. The second reference term, *Windows 7 Professional*, is not returned.  
  
|Item|Value|  
|----------|-----------|  
|Input term|Windows 7 Professional|  
|Reference terms|Windows, Windows 7 Professional|  
|Output|Windows|  
  
 The Term Lookup transformation can match nouns and noun phrases that contain special characters, and the data in the reference table may include these characters. The special characters are as follows: %, @, &, $, #, \*, :, ;, ., **,** , !, ?, \<, >, +, =, ^, ~, |, \\, /, (, ), [, ], {, }, ", and '.  
  
## Data Types  
 The Term Lookup transformation can only use a column that has either the DT_WSTR or the DT_NTEXT data type. If a column contains text, but does not have one of these data types, the Data Conversion transformation can add a column with the DT_WSTR or DT_NTEXT data type to the data flow and copy the column values to the new column. The output from the Data Conversion transformation can then be used as the input to the Term Lookup transformation. For more information, see [Data Conversion Transformation](../../../integration-services/data-flow/transformations/data-conversion-transformation.md).  
  
## Configuration the Term Lookup Transformation  
 The Term Lookup transformation input columns includes the InputColumnType property, which indicates the use of the column. InputColumnType can contain the following values:  
  
-   The value 0 indicates the column is passed through to the output only and is not used in the lookup.  
  
-   The value 1 indicates the column is used in the lookup only.  
  
-   The value 2 indicates the column is passed through to the output, and is also used in the lookup.  
  
 Transformation output columns whose InputColumnType property is set to 0 or 2 include the CustomLineageID property for a column, which contains the lineage identifier assigned to the column by an upstream data flow component.  
  
 The Term Lookup transformation adds two columns to the transformation output, named by default **Term** and **Frequency**. **Term** contains a term from the lookup table and **Frequency** contains the number of times the term in the reference table occurs in the input data set. These columns do not include the CustomLineageID property.  
  
 The lookup table must be a table in a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] or an Access database. If the output of the Term Extraction transformation is saved to a table, this table can be used as the reference table, but other tables can also be used. Text in flat files, Excel workbooks or other sources must be imported to a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] database or an Access database before you can use the Term Lookup transformation.  
  
 The Term Lookup transformation uses a separate OLE DB connection to connect to the reference table. For more information, see [OLE DB Connection Manager](../../../integration-services/connection-manager/ole-db-connection-manager.md).  
  
 The Term Lookup transformation works in a fully precached mode. At run time, the Term Lookup transformation reads the terms from the reference table and stores them in its private memory before it processes any transformation input rows.  
  
 Because the terms in an input column row may repeat, the output of the Term Lookup transformation typically has more rows than the transformation input.  
  
 The transformation has one input and one output. It does not support error outputs.  
  
 You can set properties through [!INCLUDE[ssIS](../../../includes/ssis-md.md)] Designer or programmatically.  
  
 For more information about the properties that you can set in the **Advanced Editor** dialog box or programmatically, click one of the following topics:  
  
-   [Common Properties](https://msdn.microsoft.com/library/51973502-5cc6-4125-9fce-e60fa1b7b796)  
  
-   [Transformation Custom Properties](../../../integration-services/data-flow/transformations/transformation-custom-properties.md)  
  
 For more information about how to set properties, see [Set the Properties of a Data Flow Component](../../../integration-services/data-flow/set-the-properties-of-a-data-flow-component.md).  
  
## Term Lookup Transformation Editor (Term Lookup Tab)
  Use the **Term Lookup** tab of the **Term Lookup Transformation Editor** dialog box to map an input column to a lookup column in a reference table and to provide an alias for each output column.  
  
### Options  
 **Available Input Columns**  
 Using the check boxes, select input columns to pass through to the output unchanged. Drag an input column to the **Available Reference Columns** list to map it to a lookup column in the reference table. The input and lookup columns must have matching, supported data types, either DT_NTEXT or DT_WSTR. Select a mapping line and right-click to edit the mappings in the [Create Relationships](../../../integration-services/data-flow/transformations/create-relationships.md) dialog box.  
  
 **Available Reference Columns**  
 View the available columns in the reference table. Choose the column that contains the list of terms to match.  
  
 **Pass-Through Column**  
 Select from the list of available input columns. Your selections are reflected in the check box selections in the **Available Input Columns** table.  
  
 **Output Column Alias**  
 Type an alias for each output column. The default is the name of the column; however, you can choose any unique, descriptive name.  
  
 **Configure Error Output**  
 Use the [Configure Error Output](https://msdn.microsoft.com/library/5f8da390-fab5-44f8-b268-d8fa313ce4b9) dialog box to specify error handling options for rows that cause errors.  
  
## Term Lookup Transformation Editor (Reference Table Tab)
  Use the **Reference Table** tab of the **Term Lookup Transformation Editor** dialog box to specify the connection to the reference (lookup) table.  
  
### Options  
 **OLE DB connection manager**  
 Select an existing connection manager from the list, or create a new connection by clicking **New**.  
  
 **New**  
 Create a new connection by using the **Configure OLE DB Connection Manager** dialog box.  
  
 **Reference table name**  
 Select a lookup table or view from the database by selecting an item from the list. The table or view should contain a column with an existing list of terms that the text in the source column can be compared to.  
  
 **Configure Error Output**  
 Use the [Configure Error Output](https://msdn.microsoft.com/library/5f8da390-fab5-44f8-b268-d8fa313ce4b9) dialog box to specify error handling options for rows that cause errors.  
  
## Term Lookup Transformation Editor (Advanced Tab)
  Use the **Advanced** tab of the **Term Lookup Transformation Editor** dialog box to specify whether lookup should be case-sensitive.  
  
### Options  
 **Use case-sensitive term lookup**  
 Indicate whether the lookup is case-sensitive. The default is **False**.  
  
 **Configure Error Output**  
 Use the [Configure Error Output](https://msdn.microsoft.com/library/5f8da390-fab5-44f8-b268-d8fa313ce4b9) dialog box to specify error handling options for rows that cause errors.  
  
## See Also  
 [Integration Services Error and Message Reference](../../../integration-services/integration-services-error-and-message-reference.md)   
 [Term Extraction Transformation](../../../integration-services/data-flow/transformations/term-extraction-transformation.md)  
  
