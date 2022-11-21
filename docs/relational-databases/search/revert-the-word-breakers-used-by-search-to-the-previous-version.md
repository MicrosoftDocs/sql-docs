---
description: "Revert word breakers used by Search to previous version (SQL Server Search)"
title: "Revert word breakers used by Search to previous version"
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: search
ms.topic: conceptual
ms.assetid: 29b4488e-4c6a-4bf0-a64d-19e2fdafa7ae
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
ms.custom: "seo-lt-2019"
---
# Revert word breakers used by Search to previous version (SQL Server Search)

[!INCLUDE [SQL Server Azure SQL Database](../../includes/applies-to-version/sql-asdb.md)]

[!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] installs and enables a version of the word breakers and stemmers for all languages supported by Full-Text Search with the exception of Korean. This article describes how to switch from this version of these components to the previous version, or to switch back from the previous version to the new version.  
  
 This article does not discuss the following languages:  
  
- **English**. To revert or restore the English components, see [Change the Word Breaker Used for US English and UK English](../../relational-databases/search/change-the-word-breaker-used-for-us-english-and-uk-english.md).  
  
- **Danish, Polish, and Turkish**. The third-party word breakers for Danish, Polish, and Turkish that were included with previous releases of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] have been replaced with [!INCLUDE[msCoName](../../includes/msconame-md.md)] components.  
  
- **Czech and Greek**. There are new word breakers for Czech and Greek. Previous releases of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Full-Text Search did not include support for these two languages.  
  
- **Korean**. The word breaker and stemmer for the Korean language are not upgraded in this release.  
  
 For general information about word breakers and stemmers, see [Configure and Manage Word Breakers and Stemmers for Search](../../relational-databases/search/configure-and-manage-word-breakers-and-stemmers-for-search.md).  
  
##  <a name="overview"></a> Overview of reverting and restoring word breakers and stemmers  
 The instructions for reverting and restoring word breakers and stemmers depend on the language. The following table summarizes the three sets of actions that may be required to revert to the previous version of the components.  
  
|Current file|Previous file|Number of affected languages|Action for files|Action for registry entries|  
|------------------|-------------------|----------------------------------|----------------------|---------------------------------|  
|NaturalLanguage6.dll|NaturalLanguage6.dll|34|Obtain and install a previous version of NaturalLanguage6.dll, overwriting the current version of the file.|No action required.<br /><br /> The registry keys and values have not changed for this release.|  
|(Other file name)|NaturalLanguage6.dll|5|Obtain and install a previous version of NaturalLanguage6.dll, overwriting the current version of the file.|Change a set of registry entries to specify the previous version of the components.|  
|(Other file name)|(Other file name)|6|No action required.<br /><br /> [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] setup copies both the current and the previous versions of the components to the Binn folder.|Change a set of registry entries to specify the previous version of the components.|  
  
> [!WARNING]  
>  If you replace the current version of the file NaturalLanguage6.dll with a different version, then the behavior of all the languages that use this file is affected.  
  
 The files described in this article are DLL files that are installed in the `MSSQL\Binn` folder for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance. The full path is typically the following path:  
  
 `C:\Program Files\Microsoft SQL Server\<instance>\MSSQL\Binn`  
  
##  <a name="nl6nl6"></a> Languages for which the file name of both the current and previous word breaker is NaturalLanguage6.dll  
 For the languages in the following table, the file name of both the current and previous word breaker is NaturalLanguage6.dll. To revert or restore these components, you have to overwrite NaturalLanguage6.dll with a different version of the same file. You do not have to change any registry entries, because the registry entries have not changed for this release.  
  
> [!WARNING]  
>  If you replace the current version of the file NaturalLanguage6.dll with a different version, then the behavior of all the languages that use this file is affected.  
  
 **List of affected languages**  
  
|Language|Abbreviation<br />used in the<br />registry|LCID|  
|--------------|---------------------------------------|----------|  
|Bengali|`ben`|1093|  
|Bulgarian|`bgr`|1026|  
|Catalan|`cat`|1027|  
|Spanish|`esn`|3082|  
|French|`fra`|1036|  
|Gujarati|`guj`|1095|  
|Hebrew|`heb`|1037|  
|Hindi|`hin`|1081|  
|Croatian|`hrv`|1050|  
|Indonesian|`ind`|1057|  
|Icelandic|`isl`|1039|  
|Italian|`ita`|1040|  
|Kannada|`kan`|1099|  
|Lithuanian|`lth`|1063|  
|Latvian|`lvi`|1062|  
|Malayalam|`mal`|1100|  
|Marathi|`mar`|1102|  
|Malay|`msl`|1086|  
|Neutral|`Neutral`|0000|  
|Norwegial Bokmaal|`nor`|1044|  
|Punjabi|`pan`|1094|  
|Portuguese (Brazil)|`ptb`|1046|  
|Portuguese|`ptg`|2070|  
|Romanian|`rom`|1048|  
|Slovak|`sky`|1051|  
|Slovenian|`slv`|1060|  
|Serbian - Cyrillic|`srb`|3098|  
|Serbian - Latin|`srl`|2074|  
|Swedish|`sve`|1053|  
|Tamil|`tam`|1097|  
|Telugu|`tel`|1098|  
|Ukrainian|`ukr`|1058|  
|Urdu|`urd`|1056|  
|Vietnamese|`vit`|1066|  
  
 The preceding table is sorted alphabetically on the Abbreviation column.  
  
###  <a name="nl6nl6revert"></a> To revert to the previous components  
  
1.  Navigate to the Binn folder described above.  
  
2.  Back up the [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] version of NaturalLanguage6.dll to another location.  
  
3.  Copy the previous version of NaturalLanguage6.dll from the Binn folder of an instance of [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] or [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] into the Binn folder of the [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] instance.  
  
    > [!WARNING]  
    >  This change affects all the languages that use NaturalLanguage6.dll in both the current and previous version.  
  
4.  Restart [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  

###  <a name="nl6nl6restore"></a> To restore the current components  
  
1.  Navigate to the location where you backed up the [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] version of NaturalLanguage6.dll.  
  
2.  Copy the current version of NaturalLanguage6.dll from the backup location into the Binn folder of the [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] instance.  
  
    > [!WARNING]  
    >  This change affects all the languages that use NaturalLanguage6.dll in both the current and previous version.  
  
3.  Restart [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
##  <a name="newnl6"></a> Languages for which the file name of the previous word breaker only is NaturalLanguage6.dll  
 For the languages in the following table, the file name of the previous word breaker is different from the file name of the new version. The previous file name is NaturalLanguage6.dll. To revert to the previous version, you have to overwrite the current version of NaturalLanguage6.dll with an earlier version of the same file. You also have to change a set of registry entries to specify the previous or current version of the components.  
  
> [!WARNING]  
>  If you replace the current version of the file NaturalLanguage6.dll with a different version, then the behavior of all the languages that use this file is affected.  
  
 **List of affected languages**  
  
|Language|Abbreviation<br />used in the<br />registry|LCID|  
|--------------|---------------------------------------|----------|  
|Arabic|ara|1025|  
|German|deu|1031|  
|Japanese|jpn|1041|  
|Dutch|nld|1043|  
|Russian|rus|1049|  
  
 The preceding table is sorted alphabetically on the Abbreviation column.  
  
 Use the following instructions together with the list of values in the section [File names and registry values for reverting and restoring word breakers and stemmers](#newnl6values).  
  
###  <a name="newnl6revert"></a> To revert to the previous components  
  
1.  Navigate to the Binn folder described above.  
  
2.  Do not remove the files for the current version of the components from the Binn folder.  
  
3.  Back up the [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] version of NaturalLanguage6.dll to another location.  
  
4.  Copy the previous version of NaturalLanguage6.dll from the Binn folder of an instance of [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] or [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] into the Binn folder of the new [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] instance.  
  
    > [!WARNING]  
    >  This change affects all the languages that use NaturalLanguage6.dll in both the current and previous version.  
  
5.  In the registry, navigate to the following node: **HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\<InstanceRoot>\MSSearch\CLSID**.  
  
6.  Use the following steps to add new keys for the COM ClassIDs for the previous word breaker and stemmer interfaces for the selected language:  
  
    1.  Add a new key with the value from the table for the previous word breaker.  
  
    2.  Update the (Default) data of that key value to the file name of the previous word breaker from the table.  
  
    3.  If the selected language uses a stemmer, then add a new key with the value from the table for the previous stemmer.  
  
    4.  If the selected language uses a stemmer, then update the (Default) data of that key value to the file name of the previous stemmer from the table.  
  
7.  In the registry, navigate to the following node: **HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\<InstanceRoot>\MSSearch\Language\<language_key>**. *<language_key>* represents the abbreviation for the language that is used in the registry; for example, "fra" for French and "esn" for Spanish.  
  
8.  Update the **WBreakerClass** key value to the value from the table for the current word breaker.  
  
9. If the selected language uses a stemmer, then update the **StemmerClass** key value to the value from the table for the current stemmer.  
  
10. Restart [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
###  <a name="newnl6restore"></a> To restore the current components  
  
1.  Navigate to the location where you backed up the [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] version of NaturalLanguage6.dll.  
  
2.  Copy the current version of NaturalLanguage6.dll from the backup location into the Binn folder of the [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] instance.  
  
    > [!WARNING]  
    >  This change affects all the languages that use NaturalLanguage6.dll in both the current and previous version.  
  
3.  In the registry, navigate to the following node: **HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\<InstanceRoot>\MSSearch\CLSID**.  
  
4.  If the following keys do not exist, then use the following steps to add new keys for the COM ClassIDs for the current word breaker and stemmer interfaces for the selected language:  
  
    1.  Add a new key with the value from the table for the current word breaker.  
  
    2.  Update the (Default) data of that key value to the file name of the current word breaker from the table.  
  
    3.  If the selected language uses a stemmer, then add a new key with the value from the table for the current stemmer.  
  
    4.  If the selected language uses a stemmer, then update the (Default) data of that key value to the file name of the current stemmer from the table.  
  
5.  In the registry, navigate to the following node: **HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\<InstanceRoot>\MSSearch\Language\<language_key>**. *<language_key>* represents the abbreviation for the language that is used in the registry; for example, "fra" for French and "esn" for Spanish.  
  
6.  Update the **WBreakerClass** key value to the value from the table for the previous word breaker.  
  
7.  If the selected language uses a stemmer, then update the **StemmerClass** key value to the value from the table for the previous stemmer.  
  
8.  Restart [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
###  <a name="newnl6values"></a> File names and registry values for reverting and restoring word breakers and stemmers  
 Use the following list of file names and registry entries together with the instructions in the preceding section. Use the previous values to revert to the previous version, or use the current values to restore the current version of the components.  
  
 The following listed is sorted alphabetically on the abbreviation used for each language.  
  
 **Arabic (ara), LCID 1025**  
  
|Component|Word breaker|Stemmer|  
|---------------|------------------|-------------|  
|Previous CLSID|7EFD3C7E-9E4B-4a93-9503-DECD74C0AC6D|483B0283-25DB-4c92-9C15-A65925CB95CE|  
|Previous file name|NaturalLanguage6.dll|NaturalLanguage6.dll|  
|Current CLSID|04b37e30-c9a9-4a7d-8f20-792fc87ddf71|None|  
|Current file name|MSWB7.dll|None|  
  
 **German (deu), LCID 1031**  
  
|Component|Word breaker|Stemmer|  
|---------------|------------------|-------------|  
|Previous CLSID|45EACA36-DBE9-4e4a-A26D-5C201902346D|65170AE4-0AD2-4fa5-B3BA-7CD73E2DA825|  
|Previous file name|NaturalLanguage6.dll|NaturalLanguage6.dll|  
|Current CLSID|dfa00c33-bf19-482e-a791-3c785b0149b4|8a474d89-6e2f-419c-8dd5-9b50edc8c787|  
|Current file name|MsWb7.dll|MsWb7.dll|  
  
 **Japanese (jpn), LCID 1041**  
  
|Component|Word breaker|Stemmer|  
|---------------|------------------|-------------|  
|Previous CLSID|E1E8F15E-8BEC-45df-83BF-50FF84D0CAB5|3D5DF14F-649F-4cbc-853D-F18FEDE9CF5D|  
|Previous file name|NaturalLanguage6.dll|NaturalLanguage6.dll|  
|Current CLSID|04096682-6ece-4e9e-90c1-52d81f0422ed|None|  
|Current file name|MsWb70011.dll|None|  
  
 **Dutch (nld), LCID 1043**  
  
|Component|Word breaker|Stemmer|  
|---------------|------------------|-------------|  
|Previous CLSID|2C9F6BEB-C5B0-42b6-A5EE-84C24DC0D8EF|F7A465EE-13FB-409a-B878-195B420433AF|  
|Previous file name|NaturalLanguage6.dll|NaturalLanguage6.dll|  
|Current CLSID|69483c30-a9af-4552-8f84-a0796ad5285b|CF923CB5-1187-43ab-B053-3E44BED65FFA|  
|Current file name|MsWb7.dll|MsWb7.dll|  
  
 **Russian (rus), LCID 1049**  
  
|Component|Word breaker|Stemmer|  
|---------------|------------------|-------------|  
|Previous CLSID|2CB6CDA4-1C14-4392-A8EC-81EEF1F2E079|E06A0DDD-E81A-4e93-8A8D-F386C3A1B670|  
|Previous file name|NaturalLanguage6.dll|NaturalLanguage6.dll|  
|Current CLSID|aaa3d3bd-6de7-4317-91a0-d25e7d3babc3|d42c8b70-adeb-4b81-a52f-c09f24f77dfa|  
|Current file name|MsWb7.dll|MsWb7.dll|  
  
##  <a name="newnew"></a> Languages for which neither the previous nor the current file name is NaturalLanguage6.dll  
 For the languages in the following table, the file names of the previous word breakers and stemmers are different from the file names of the new versions. Neither the previous nor the current file name is NaturalLanguage6.dll. You do not have to replace any files, because [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] setup copies both the current and the previous versions of the components to the Binn folder. However you have to change a set of registry entries to specify the previous or current version of the components.  
  
 **List of affected languages**  
  
|Language|Abbreviation<br />used in the<br />registry|LCID|  
|--------------|---------------------------------------|----------|  
|Simplified Chinese|chs|2052|  
|Traditional Chinese|cht|1028|  
|Thai|tha|1054|  
|Chinese Traditional|zh-hk|3076|  
|Chinese Traditional|zh-mo|5124|  
|Chinese Simplified|zh-sg|4100|  
  
 The preceding table is sorted alphabetically on the Abbreviation column.  
  
 Use the following instructions together with the list of values in the section [File names and registry values for reverting and restoring word breakers and stemmers](#newnewvalues).  
  
###  <a name="newnewrevert"></a> To revert to the previous components  
  
1.  Do not remove the files for the current version of the components from the Binn folder.  
  
2.  In the registry, navigate to the following node: **HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\<InstanceRoot>\MSSearch\CLSID**.  
  
3.  Use the following steps to add new keys for the COM ClassIDs for the previous word breaker and stemmer interfaces for the selected language:  
  
    1.  Add a new key with the value from the table for the previous word breaker.  
  
    2.  Update the (Default) data of that key value to the file name of the previous word breaker from the table.  
  
    3.  If the selected language uses a stemmer, then add a new key with the value from the table for the previous stemmer.  
  
    4.  If the selected language uses a stemmer, then update the (Default) data of that key value to the file name of the previous stemmer from the table.  
  
4.  In the registry, navigate to the following node: **HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\<InstanceRoot>\MSSearch\Language\<language_key>**. *<language_key>* represents the abbreviation for the language that is used in the registry; for example, "fra" for French and "esn" for Spanish.  
  
5.  Update the **WBreakerClass** key value to the value from the table for the current word breaker.  
  
6.  If the selected language uses a stemmer, then update the **StemmerClass** key value to the value from the table for the current stemmer.  
  
7.  Restart [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
###  <a name="newnewrestore"></a> To restore the previous components  
  
1.  Do not remove the files for the previous version of the components from the Binn folder.  
  
2.  In the registry, navigate to the following node: **HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\<InstanceRoot>\MSSearch\CLSID**.  
  
3.  If the following keys do not exist, then use the following steps to add new keys for the COM ClassIDs for the current word breaker and stemmer interfaces for the selected language:  
  
    1.  Add a new key with the value from the table for the current word breaker.  
  
    2.  Update the (Default) data of that key value to the file name of the current word breaker from the table.  
  
    3.  If the selected language uses a stemmer, then add a new key with the value from the table for the current stemmer.  
  
    4.  If the selected language uses a stemmer, then update the (Default) data of that key value to the file name of the current stemmer from the table.  
  
4.  In the registry, navigate to the following node: **HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\<InstanceRoot>\MSSearch\Language\<language_key>**. *<language_key>* represents the abbreviation for the language that is used in the registry; for example, "fra" for French and "esn" for Spanish.  
  
5.  Update the **WBreakerClass** key value to the value from the table for the previous word breaker.  
  
6.  If the selected language uses a stemmer, then update the **StemmerClass** key value to the value from the table for the previous stemmer.  
  
7.  Restart [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
###  <a name="newnewvalues"></a> File names and registry values for reverting and restoring word breakers and stemmers  
 Use the following list of file names and registry entries together with the instructions in the preceding section. Use the previous values to revert to the previous version, or use the current values to restore the current version of the components.  
  
 The following listed is sorted alphabetically on the abbreviation used for each language.  
  
 **Simplified Chinese (chs), LCID 2052**  
  
|Component|Word breaker|  
|---------------|------------------|  
|Previous CLSID|12CE94A0-DEFB-11D2-B31D-00600893A857|  
|Previous file name|chsbrkr.dll|  
|Current CLSID|E0831C90-BAB0-4ca5-B9BD-EA254B538DAC|  
|Current file name|MsWb70804.dll|  
  
 **Traditional Chinese (cht), LCID 1028**  
  
|Component|Word breaker|  
|---------------|------------------|  
|Previous CLSID|1680E7C3-9430-4A51-9B82-1E7E7AEE5258|  
|Previous file name|chtbrkr.dll|  
|Current CLSID|E9B1DF65-08F1-438b-8277-EF462B23A792|  
|Current file name|MsWb70404.dll|  
  
 **Thai (tha), LCID 1054**  
  
|Component|Word breaker|Stemmer|  
|---------------|------------------|-------------|  
|Previous CLSID|CCA22CF4-59FE-11D1-BBFF-00C04FB97FDA|CEDC01C7-59FE-11D1-BBFF-00C04FB97FDA|  
|Previous file name|Thawbrkr.dll|Thawbrkr.dll|  
|Current CLSID|F70C0935-6E9F-4ef1-9F06-7876536DB900|None|  
|Current file name|MsWb7001e.dll|None|  
  
 **Chinese Traditional (zh-hk), LCID 3076**  
  
|Component|Word breaker|  
|---------------|------------------|  
|Previous CLSID|1680E7C3-9430-4A51-9B82-1E7E7AEE5258|  
|Previous file name|chtbrkr.dll|  
|Current CLSID|E9B1DF65-08F1-438b-8277-EF462B23A792|  
|Current file name|MsWb70404.dll|  
  
 **Chinese Traditional (zh-mo), LCID 5124**  
  
|Component|Word breaker|  
|---------------|------------------|  
|Previous CLSID|1680E7C3-9430-4A51-9B82-1E7E7AEE5258|  
|Previous file name|chtbrkr.dll|  
|Current CLSID|E9B1DF65-08F1-438b-8277-EF462B23A792|  
|Current file name|MsWb70404.dll|  
  
 **Chinese Simplified (zh-sg), LCID 4100**  
  
|Component|Word breaker|  
|---------------|------------------|  
|Previous CLSID|12CE94A0-DEFB-11D2-B31D-00600893A857|  
|Previous file name|chsbrkr.dll|  
|Current CLSID|E0831C90-BAB0-4ca5-B9BD-EA254B538DAC|  
|Current file name|MsWb70804.dll|  
  
## See Also  
 [Change the Word Breaker Used for US English and UK English](../../relational-databases/search/change-the-word-breaker-used-for-us-english-and-uk-english.md)   
 [Behavior Changes to Full-Text Search](./full-text-search.md)
