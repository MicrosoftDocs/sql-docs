---
description: "Term Extraction Transformation"
title: "Term Extraction Transformation | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.termextractiontrans.f1"
  - "sql13.dts.designer.termextraction.termextraction.f1"
  - "sql13.dts.designer.termextraction.inclusionexclusion.f1"
  - "sql13.dts.designer.termextraction.advanced.f1"
helpviewer_keywords: 
  - "word boundaries [Integration Services]"
  - "extracting data [Integration Services]"
  - "sentence boundaries"
  - "word extractions [Integration Services]"
  - "Term Extraction transformation"
  - "tagging words"
  - "normalized data [Integration Services]"
  - "tokenizing text [Integration Services]"
  - "parts of speech [Integration Services]"
  - "text extraction [Integration Services]"
  - "term extractions [Integration Services]"
  - "stemming words [Integration Services]"
ms.assetid: d0821526-1603-4ea6-8322-2d901568fbeb
author: chugugrace
ms.author: chugu
---
# Term Extraction Transformation

[!INCLUDE[sqlserver-ssis](../../../includes/applies-to-version/sqlserver-ssis.md)]


  The Term Extraction transformation extracts terms from text in a transformation input column, and then writes the terms to a transformation output column. The transformation works only with English text and it uses its own English dictionary and linguistic information about English.  
  
 You can use the Term Extraction transformation to discover the content of a data set. For example, text that contains e-mail messages may provide useful feedback about products, so that you could use the Term Extraction transformation to extract the topics of discussion in the messages, as a way of analyzing the feedback.  
  
## Extracted Terms and Data Types  
 The Term Extraction transformation can extract nouns only, noun phrases only, or both nouns and noun phases. A noun is a single noun; a noun phrases is at least two words, of which one is a noun and the other is a noun or an adjective. For example, if the transformation uses the nouns-only option, it extracts terms like *bicycle* and *landscape*; if the transformation uses the noun phrase option, it extracts terms like *new blue bicycle*, *bicycle helmet*, and *boxed bicycles*.  
  
 Articles and pronouns are not extracted. For example, the Term Extraction transformation extracts the term *bicycle* from the text *the bicycle*, *my bicycle*, and *that bicycle*.  
  
 The Term Extraction transformation generates a score for each term that it extracts. The score can be either a TFIDF value or the raw frequency, meaning the number of times the normalized term appears in the input. In either case, the score is represented by a real number that is greater than 0. For example, the TFIDF score might have the value 0.5, and the frequency would be a value like 1.0 or 2.0.  
  
 The output of the Term Extraction transformation includes only two columns. One column contains the extracted terms and the other column contains the score. The default names of the columns are **Term** and **Score**. Because the text column in the input may contain multiple terms, the output of the Term Extraction transformation typically has more rows than the input.  
  
 If the extracted terms are written to a table, they can be used by other lookup transformation such as the Term Lookup, Fuzzy Lookup, and Lookup transformations.  
  
 The Term Extraction transformation can work only with text in a column that has either the DT_WSTR or the DT_NTEXT data type. If a column contains text but does not have one of these data types, the Data Conversion transformation can be used to add a column with the DT_WSTR or DT_NTEXT data type to the data flow and copy the column values to the new column. The output from the Data Conversion transformation can then be used as the input to the Term Extraction transformation. For more information, see [Data Conversion Transformation](../../../integration-services/data-flow/transformations/data-conversion-transformation.md).  
  
## Exclusion Terms  
 Optionally, the Term Extraction transformation can reference a column in a table that contains exclusion terms, meaning terms that the transformation should skip when it extracts terms from a data set. This is useful when a set of terms has already been identified as inconsequential in a particular business and industry, typically because the term occurs with such high frequency that it becomes a noise word. For example, when extracting terms from a data set that contains customer support information about a particular brand of cars, the brand name itself might be excluded because it is mentioned too frequently to have significance. Therefore, the values in the exclusion list must be customized to the data set you are working with.  
  
 When you add a term to the exclusion list, all the terms-words or noun phrases-that contain the term are also excluded. For example, if the exclusion list includes the single word *data*, then all the terms that contain this word, such as *data*, *data mining*, *data integrity*, and *data validation* will also be excluded. If you want to exclude only compounds that contain the word *data*, you must explicitly add those compound terms to the exclusion list. For example, if you want to extract incidences of *data*, but exclude *data validation*, you would add *data validation* to the exclusion list, and make sure that *data* is removed from the exclusion list.  
  
 The reference table must be a table in a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] or an Access database. The Term Extraction transformation uses a separate OLE DB connection to connect to the reference table. For more information, see [OLE DB Connection Manager](../../../integration-services/connection-manager/ole-db-connection-manager.md).  
  
 The Term Extraction transformation works in a fully precached mode. At run time, the Term Extraction transformation reads the exclusion terms from the reference table and stores them in its private memory before it processes any transformation input rows.  
  
## Extraction of Terms from Text  
 To extract terms from text, the Term Extraction transformation performs the following tasks.  
  
### Identification of Words  
 First, the Term Extraction transformation identifies words by performing the following tasks:  
  
-   Separating text into words by using spaces, line breaks, and other word terminators in the English language. For example, punctuation marks such as *?* and *:* are word-breaking characters.  
  
-   Preserving words that are connected by hyphens or underscores. For example, the words *copy-protected* and *read-only* remain one word.  
  
-   Keeping intact acronyms that include periods. For example, the *A.B.C* Company would be tokenized as **ABC** and **Company**.  
  
-   Splitting words on special characters. For example, the word *date/time* is extracted as *date* and *time*, *(bicycle)* as *bicycle*, and C# is treated as C. Special characters are discarded and cannot be lexicalized.  
  
-   Recognizing when special characters such as the apostrophe should not split words. For example, the word *bicycle's* is not split into two words, and yields the single term *bicycle* (noun).  
  
-   Splitting time expressions, monetary expressions, e-mail addresses, and postal addresses. For example, the date *January 31, 2004* is separated into the three tokens *January*, *31*, and *2004*.  
  
### Tagged Words  
 Second, the Term Extraction transformation tags words as one of the following parts of speech:  
  
-   A noun in the singular form. For example, *bicycle* and *potato*.  
  
-   A noun in the plural form. For example, *bicycles* and *potatoes*. All plural nouns that are not lemmatized are subject to stemming.  
  
-   A proper noun in the singular form. For example, *April* and *Peter*.  
  
-   A proper noun in the plural form. For example *Aprils* and *Peters*. For a proper noun to be subject to stemming, it must be a part of the internal lexicon, which is limited to standard English words.  
  
-   An adjective. For example, *blue*.  
  
-   A comparative adjective that compares two things. For example, *higher* and *taller*.  
  
-   A superlative adjective that identifies a thing that has a quality above or below the level of at least two others. For example, *highest* and *tallest*.  
  
-   A number. For example, *62* and *2004*.  
  
 Words that are not one of these parts of speech are discarded. For example, verbs and pronouns are discarded.  
  
> [!NOTE]  
>  The tagging of parts of speech is based on a statistical model and the tagging may not be completely accurate.  
  
 If the Term Extraction transformation is configured to extract only nouns, only the words that are tagged as singular or plural forms of nouns and proper nouns are extracted.  
  
 If the Term Extraction transformation is configured to extract only noun phrases, words that are tagged as nouns, proper nouns, adjectives, and numbers may be combined to make a noun phrase, but the phrase must include at least one word that is tagged as a singular or plural form of a noun or a proper noun. For example, the noun phrase *highest mountain* combines a word tagged as a superlative adjective (*highest*) and a word tagged as noun (*mountain*).  
  
 If the Term Extraction is configured to extract both nouns and noun phrases, both the rules for nouns and the rules for noun phrases apply. For example, the transformation extracts *bicycle* and *beautiful blue bicycle* from the text *many beautiful blue bicycles*.  
  
> [!NOTE]  
>  The extracted terms remain subject to the maximum term length and frequency threshold that the transformation uses.  
  
### Stemmed Words  
 The Term Extraction transformation also stems nouns to extract only the singular form of a noun. For example, the transformation extracts *man* from *men*, *mouse* from *mice*, and *bicycle* from *bicycles*. The transformation uses its dictionary to stem nouns. Gerunds are treated as nouns if they are in the dictionary.  
  
 The Term Extraction transformation stems words to their dictionary form as shown in these examples by using the dictionary internal to the Term Extraction transformation.  
  
-   Removing *s* from nouns. For example, *bicycles* becomes *bicycle*.  
  
-   Removing *es* from nouns. For example, *stories* becomes *story*.  
  
-   Retrieving the singular form for irregular nouns from the dictionary. For example, *geese* becomes *goose*.  
  
### Normalized Words  
 The Term Extraction transformation normalizes terms that are capitalized only because of their position in a sentence, and uses their non-capitalized form instead. For example, in the phrases *Dogs chase cats* and *Mountain paths are steep*, *Dogs* and *Mountain* would be normalized to *dog* and *mountain*.  
  
 The Term Extraction transformation normalizes words so that the capitalized and noncapitalized versions of words are not treated as different terms. For example, in the text *You see many bicycles in Seattle* and *Bicycles are blue*, *bicycles* and *Bicycles* are recognized as the same term and the transformation keeps only *bicycle*. Proper nouns and words that are not listed in the internal dictionary are not normalized.  
  
### Case-Sensitive Normalization  
 The Term Extraction transformation can be configured to consider lowercase and uppercase words as either distinct terms, or as different variants of the same term.  
  
-   If the transformation is configured to recognize differences in case, terms like *Method* and *method* are extracted as two different terms. Capitalized words that are not the first word in a sentence are never normalized, and are tagged as proper nouns.  
  
-   If the transformation is configured to be case-insensitive, terms like *Method* and *method* are recognized as variants of a single term. The list of extracted terms might include either *Method* or *method*, depending on which word occurs first in the input data set. If *Method* is capitalized only because it is the first word in a sentence, it is extracted in normalized form.  
  
## Sentence and Word Boundaries  
 The Term Extraction transformation separates text into sentences using the following characters as sentence boundaries:  
  
-   ASCII line-break characters 0x0d (carriage return) and 0x0a (line feed). To use this character as a sentence boundary, there must be two or more line-break characters in a row.  
  
-   Hyphens (-). To use this character as a sentence boundary, neither the character to the left nor to the right of the hyphen can be a letter.  
  
-   Underscore (_). To use this character as a sentence boundary, neither the character to the left nor to the right of the hyphen can be a letter.  
  
-   All Unicode characters that are less than or equal to 0x19, or greater than or equal to 0x7b.  
  
-   Combinations of numbers, punctuation marks, and alphabetical characters. For example, *A23B#99* returns the term *A23B*.  
  
-   The characters, %, @, &, $, #, \*, :, ;, ., **,** , !, ?, \<, >, +, =, ^, ~, |, \\, /, (, ), [, ], {, }, ", and '.  
  
    > [!NOTE]  
    >  Acronyms that include one or more periods (.) are not separated into multiple sentences.  
  
 The Term Extraction transformation then separates the sentence into words using the following word boundaries:  
  
-   Space  
  
-   Tab  
  
-   ASCII 0x0d (carriage return)  
  
-   ASCII 0x0a (line feed)  
  
    > [!NOTE]  
    >  If an apostrophe is in a word that is a contraction, such as *we're* or *it's*, the word is broken at the apostrophe; otherwise, the letters following the apostrophe are trimmed. For example, *we're* is split into *we* and *'re*, and *bicycle's* is trimmed to *bicycle*.  
  
## Configuration of the Term Extraction Transformation  
 The Text Extraction transformation uses internal algorithms and statistical models to generate its results. You may have to run the Term Extraction transformation several times and examine the results to configure the transformation to generate the type of results that works for your text mining solution.  
  
 The Term Extraction transformation has one regular input, one output, and one error output.  
  
 You can set properties through [!INCLUDE[ssIS](../../../includes/ssis-md.md)] Designer or programmatically.  
  
 For more information about the properties that you can set in the **Advanced Editor** dialog box or programmatically, click one of the following topics:  
  
-   [Common Properties](../set-the-properties-of-a-data-flow-component.md)  
  
-   [Transformation Custom Properties](../../../integration-services/data-flow/transformations/transformation-custom-properties.md)  
  
 For more information about how to set properties, see [Set the Properties of a Data Flow Component](../../../integration-services/data-flow/set-the-properties-of-a-data-flow-component.md).  
  
## Term Extraction Transformation Editor (Term Extraction Tab)
  Use the **Term Extraction** tab of the **Term Extraction Transformation Editor** dialog box to specify a text column that contains text to be extracted.  
  
### Options  
 **Available Input Columns**  
 Using the check boxes, select a single text column to use for term extraction.  
  
 **Term**  
 Provide a name for the output column that will contain the extracted terms.  
  
 **Score**  
 Provide a name for the output column that will contain the score for each extracted term.  
  
 **Configure Error Output**  
 Use the [Configure Error Output](../error-handling-in-data.md) dialog box to specify error handling for rows that cause errors.  
  
## Term Extraction Transformation Editor (Exclusion Tab)
  Use the **Exclusion** tab of the **Term Extraction Transformation Editor** dialog box to set up a connection to an exclusion table and specify the columns that contain exclusion terms.  
  
### Options  
 **Use exclusion terms**  
 Indicate whether to exclude specific terms during term extraction by specifying a column that contains exclusion terms. You must specify the following source properties if you choose to exclude terms.  
  
 **OLE DB connection manager**  
 Select an existing OLE DB connection manager, or create a new connection by clicking **New**.  
  
 **New**  
 Create a new connection to a database by using the **Configure OLE DB Connection Manager** dialog box.  
  
 **Table or view**  
 Select the table or view that contains the exclusion terms.  
  
 **Column**  
 Select the column in the table or view that contains the exclusion terms.  
  
 **Configure Error Output**  
 Use the [Configure Error Output](../error-handling-in-data.md) dialog box to specify error handling for rows that cause errors.  
  
## Term Extraction Transformation Editor (Advanced Tab)
  Use the **Advanced** tab of the **Term Extraction Transformation Editor** dialog box to specify properties for the extraction such as frequency, length, and whether to extract words or phrases.  
  
### Options  
 **Noun**  
 Specify that the transformation extracts individual nouns only.  
  
 **Noun phrase**  
 Specify that the transformation extracts noun phrases only.  
  
 **Noun and noun phrase**  
 Specify that the transformation extracts both nouns and noun phrases.  
  
 **Frequency**  
 Specify that the score is the frequency of the term.  
  
 **TFIDF**  
 Specify that the score is the TFIDF value of the term. The TFIDF score is the product of Term Frequency and Inverse Document Frequency, defined as: TFIDF of a Term T = (frequency of T) * log( (#rows in Input) / (#rows having T) )  
  
 **Frequency threshold**  
 Specify the number of times a word or phrase must occur before extracting it. The default value is 2.  
  
 **Maximum length of term**  
 Specify the maximum length of a phrase in words. This option affects noun phrases only. The default value is 12.  
  
 **Use case-sensitive term extraction**  
 Specify whether to make the extraction case-sensitive. The default is **False**.  
  
 **Configure Error Output**  
 Use the [Configure Error Output](../error-handling-in-data.md) dialog box to specify error handling for rows that cause errors.  
  
## See Also  
 [Integration Services Error and Message Reference](../../../integration-services/integration-services-error-and-message-reference.md)   
 [Term Lookup Transformation](../../../integration-services/data-flow/transformations/term-lookup-transformation.md)