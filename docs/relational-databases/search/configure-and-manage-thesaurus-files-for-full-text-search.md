---
title: "Configure and Manage Thesaurus Files for Full-Text Search | Microsoft Docs"
ms.custom: ""
ms.date: "12/04/2017"
ms.prod: sql
ms.prod_service: "search, sql-database"
ms.reviewer: ""
ms.technology: search
ms.topic: conceptual
helpviewer_keywords: 
  - "full-text indexes [SQL Server], thesaurus files"
  - "thesaurus [full-text search], configuring"
  - "thesaurus [full-text search]"
ms.assetid: 3ef96a63-8a52-45be-9a1f-265bff400e54
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Configure and Manage Thesaurus Files for Full-Text Search
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Full-Text Search queries can search for synonyms of user-specified terms through the use of a Full-Text Search *thesaurus*. Each thesaurus defines a set of synonyms for a specific language. By developing a thesaurus tailored to your full-text data, you can effectively broaden the scope of full-text queries on that data.

Thesaurus matching occurs for all [FREETEXT](../../t-sql/queries/freetext-transact-sql.md) and [FREETEXTABLE](../../relational-databases/system-functions/freetexttable-transact-sql.md) queries and for any [CONTAINS](../../t-sql/queries/contains-transact-sql.md) and [CONTAINSTABLE](../../relational-databases/system-functions/containstable-transact-sql.md) queries that specify the `FORMSOF THESAURUS` clause.
  
A Full-Text Search thesaurus is an XML text file.
  
##  <a name="tasks"></a> What's in a thesaurus  
 Before full-text search queries can look for synonyms in a given language, you have to define thesaurus mappings (that is, synonyms) for that language. Each thesaurus must be manually configured to define the following:  
  
-   Expansion set  
  
     An expansion set contains a group of synonyms such as "writer", "author", and "journalist" that are substituted for one another by a full-text query. Queries that contain a match for any synonym in an expansion set are expanded to include every other synonym in the expansion set.  
  
     For more information, see [XML Structure of an Expansion Set](#expansion) later in this topic.  
  
-   Replacement set  
  
     A replacement set contains a text pattern to be replaced by a substitution set. For an example, see the section [XML Structure of a Replacement Set](#replacement) later in this topic. 

-   Diacritics setting  
  
     For a given thesaurus, all search patterns are either sensitive or insensitive to diacritical marks such as a tilde (**~**), acute accent mark (**´**), or umlaut (**¨**) (that is, *accent sensitive* or *accent insensitive*). For example, suppose you specify the pattern "café" to be replaced by other patterns in a full-text query. If the thesaurus is accent-insensitive, full-text search replaces the patterns "café" and "cafe". If the thesaurus is accent-sensitive, full-text search replaces only the pattern "café". By default, a thesaurus is accent-insensitive.  
  
##  <a name="initial_thesaurus_files"></a> Default thesaurus files
[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provides a set of XML thesaurus files, one for each supported language. These files are essentially empty. They contain only the top-level XML structure that is common to all [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] thesauruses and a commented-out sample thesaurus.  
  
##  <a name="location"></a> Location of thesaurus files  
 The default location of the thesaurus files is:  
  
     <SQL_Server_data_files_path>\MSSQL13.MSSQLSERVER\MSSQL\FTDATA\  
  
 This default location contains the following files:  
  
-   **Language-specific** thesaurus files  

    Setup installs empty thesaurus files in the above location. A separate file is provided for each supported language. A system administrator can customize these files.  
  
     The default file names of the thesaurus files use following format:  
  
         'ts' + <three-letter language-abbreviation> + '.xml'  
  
     The name of the thesaurus file for a given language is specified in the registry in the following value:
     
        HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\<instance-name>\MSSearch\<language-abbrev>  
  
-   The **global** thesaurus file  
  
     An empty global thesaurus file, tsGlobal.xml.  

### Change the location of a thesaurus file 
You can change the location and names of a thesaurus file by changing its registry key. For each language, the location of the thesaurus file is specified in the following value in the registry:  
  
    HKLM\SOFTWARE\Microsoft\Microsoft SQL Server\<instance name>\MSSearch\Language\<language-abbreviation>\TsaurusFile  
  
 The global thesaurus file corresponds to the Neutral language with LCID 0. This value can be changed by administrators only.  

##  <a name="how_queries_use_tf"></a> How full-text queries use the thesaurus  
A thesaurus query uses both a language-specific thesaurus and the global thesaurus.
1.  First, the query looks up the language-specific file and loads it for processing (unless it is already loaded). The query is expanded to include the language-specific synonyms specified by the expansion set and replacement set rules in the thesaurus file. 
2.  These steps are then repeated for the global thesaurus. However, if a term is already part of a match in the language specific thesaurus file, the term is ineligible for matching in the global thesaurus.  

##  <a name="structure"></a> Structure of a thesaurus file  
 Each thesaurus file defines an XML container whose ID is `Microsoft Search Thesaurus`, and a comment, `<!--` ... `-->`, that contains a sample thesaurus. The thesaurus is defined in a `<thesaurus>` element that contains samples of the child elements that define the diacritics setting, expansion sets, and replacement sets.

A typical empty thesaurus file contains the following XML text:  
  
```xml  
<XML ID="Microsoft Search Thesaurus">  
  
<!--  Commented out  
  
    <thesaurus xmlns="x-schema:tsSchema.xml">  
<diacritics_sensitive>0</diacritics_sensitive>  
        <expansion>  
            <sub>Internet Explorer</sub>  
            <sub>IE</sub>  
            <sub>IE5</sub>  
        </expansion>  
        <replacement>  
            <pat>NT5</pat>  
            <pat>W2K</pat>  
            <sub>Windows 2012</sub>  
        </replacement>  
        <expansion>  
            <sub>run</sub>  
            <sub>jog</sub>  
        </expansion>  
    </thesaurus>  
-->  
</XML>  
```

### <a name="expansion"></a> XML structure of an expansion set  
  
 Each expansion set is enclosed within an `<expansion>` element. Within this element, you specify one or more substitutions in a `<sub>` element. In the expansion set, you can specify a group of substitutions that are synonyms of each other.  
  
 For example, you can edit the expansion section to treat the substitutions "writer", "author", and "journalist" as synonyms. full-text search queries that contain matches in one substitution are expanded to include all other substitutions specified in the expansion set. Therefore, in the preceding example, when you issue a FORMS OF THESAURUS or a FREETEXT query for the word "author", full-text search also returns search results containing the words "writer" and "journalist".  
  
This is what the expansion set section would look like for the above example:  
  
```xml  
<expansion>  
        <sub>writer</sub>  
        <sub>author</sub>  
        <sub>journalist</sub>  
</expansion>  
```  
  
### <a name="replacement"></a> XML structure of a replacement set  
  
Each replacement set is enclosed within a `<replacement>` element. Within this element you can specify one or more patterns in a `<pat>` element and zero or more substitutions in `<sub>` elements, one per synonym. You can specify a pattern to be replaced by a substitution set. Patterns and substitutions can contain a word, or a sequence of words. If there is no substitution specified for a pattern, it has the effect of removing the pattern from the user query.  
  
For example, suppose you want queries for "Win8", the pattern, to be replaced by "Windows Server 2012" or "Windows 8.0", the substitutions. If you run a full-text query for "Win8", full-text search only returns search results containing "Windows Server 2012" or "Windows 8.0". It does not return results containing "Win8". This is because the pattern "Win8" has been "replaced" by the patterns "Windows Server 2012" and "Windows 8.0".  
  
This is what the replacement set section would look like for the above example:  
  
```xml  
<replacement>  
        <pat>Win8</pat>  
        <sub>Windows Server 2012</sub>  
        <sub>Windows 8.0</sub>  
</replacement>  
```  
  
If you have two replacement sets with similar patterns being matched, the longer of the two takes precedence. For example, if you run a FORMS OF THESAURUS query for "Internet Explorer online community" and you have the following replacement sets, the "Internet Explorer" replacement set takes precedence over the "Internet" replacement set. The query will therefore be processed as "IE online community" or "IE 9 online community".  
  
```xml  
<replacement>  
         <pat>Internet</pat>  
         <sub>intranet</sub>  
</replacement>  
```  
  
and  
  
```xml  
<replacement>  
         <pat>Internet Explorer</pat>  
         <sub>IE</sub>  
         <sub>IE 9</sub>  
</replacement>  
```

### XML structure of the diacritics setting  
  
The diacritics setting of a thesaurus is specified in a single `<diacritics_sensitive>` element. This element contains an integer value that controls accent sensitivity, as follows:  
  
|Diacritics Setting|Value|XML|  
|------------------------|-----------|---------|  
|Accent insensitive|0|`<diacritics_sensitive>0</diacritics_sensitive>`|  
|Accent sensitive|1|`<diacritics_sensitive>1</diacritics_sensitive>`|  
  
> [!NOTE]  
>  This setting can only be applied one time in the file, and it applies to all search patterns in the file. This setting cannot be specified for individual patterns.  

##  <a name="editing"></a> Edit a thesaurus file  
You can configure the thesaurus for a given language by editing its thesaurus file (an XML file). During setup, empty thesaurus files that contain only the `<xml>` container and a commented-out sample `<thesaurus`> element are installed. In order for full-text search queries that look for synonyms to work properly, you have to create an actual `<thesaurus`> element that defines a set of synonyms. You can define two forms of synonyms, expansion sets and replacement sets.  

### Edit a thesaurus file  
  
1.  Open the thesaurus file in Notepad or another text editor.  
  
2.  If you are editing the thesaurus file for the first time, remove the following comment lines at the beginning and end of the file, respectively:  
  
    ```xml  
    <!--Commented out  
    -->  
    ```  
  
3.  Add, modify, or delete a replacement set or an expansion set.  
  
4.  Save the file and close Notepad.  
  
5.  Use [sp_fulltext_load_thesaurus_file](../../relational-databases/system-stored-procedures/sp-fulltext-load-thesaurus-file-transact-sql.md) to load the content of the thesaurus file into tempdb, specifying the local identifier (LCID) that corresponds to the language of the thesaurus file. For example, for the English thesaurus file, tsenu.xml, the corresponding LCID is 1033.  
  
    ```sql  
    USE AdventureWorks;  
    EXEC sys.sp_fulltext_load_thesaurus_file 1033;  
    GO
    ```

### Recommendations for editing thesaurus files  
  
 We recommend that entries in the thesaurus file contain no special characters. This is because word breakers have subtle behaviors with respect to special characters. If a thesaurus entry contains any special characters, word breakers used in combination with that entry can have subtle behavioral implications for a full-text query.  
  
 We recommend that `<sub>` entries contain no stopwords since stopwords are omitted from the full-text index. Queries are expanded to include the `<sub>` entries from a thesaurus file, and if a `<sub>` entry contains stopwords, query size increases unnecessarily.  

### Restrictions for editing thesaurus files  
  
 The following restrictions apply to editing a thesaurus file:  
  
-   Only system administrators can update, modify, or delete thesaurus files.  
  
-   When editing thesaurus files using text editor tools, the files must be saved in Unicode format, and Byte Order Marks must be specified.  
  
-   Thesaurus entries cannot be empty or word break to an empty string.  
  
-   Phrases in the thesaurus file must be no longer than 512 characters.  
  
-   A thesaurus must not contain any duplicate entries among the `<sub>` entries of expansion sets and the `<pat>` elements of replacement sets.  

## See Also  
 [CONTAINS &#40;Transact-SQL&#41;](../../t-sql/queries/contains-transact-sql.md)   
 [CONTAINSTABLE &#40;Transact-SQL&#41;](../../relational-databases/system-functions/containstable-transact-sql.md)   
 [FREETEXT &#40;Transact-SQL&#41;](../../t-sql/queries/freetext-transact-sql.md)   
 [FREETEXTTABLE &#40;Transact-SQL&#41;](../../relational-databases/system-functions/freetexttable-transact-sql.md)     
 [sp_fulltext_load_thesaurus_file &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-fulltext-load-thesaurus-file-transact-sql.md)  
 [sys.dm_fts_parser &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-fts-parser-transact-sql.md)
 
